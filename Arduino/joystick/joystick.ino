#define joyDown 4
#define joyUp 8
#define joyLeft 7
#define joyRight 2

#define switchPin 12

#define ledPin LED_BUILTIN

#define joystickMin -1
#define joystickMax 1

#define deadzoneDefault 0
#define deadzoneInterval 0

#define maxchars 64

// Comma delimiter to separate consecutive data if using more than 1 sensor
const char delimiter = ',';    
// Interval between serial writes
const int serialInterval = 5;   
// Timestamp to track serial interval
unsigned long serialPreviousTime; 

char* incomingData[maxchars];
bool newData = false;

int prevX = 0;
int prevY = 0;

int x = 0;
int y = 0;

int switchStatus = 0;

void setup() {
  // Initialize Serial Communication
  Serial.begin(9600);

  pinMode(ledPin, OUTPUT);
  pinMode(switchStatus, INPUT);

  pinMode(joyUp, INPUT_PULLUP);
  pinMode(joyDown, INPUT_PULLUP);
  pinMode(joyLeft, INPUT_PULLUP);
  pinMode(joyRight, INPUT_PULLUP);
}

// START OF MAIN LOOP --------------------------------------------------------- 
void loop()
{
  processSensors();

  processIncomingSerial();

  processOutgoingSerial();

  if (newData) {
    newData = false;
    if (strcmp("output", incomingData[0]) == 0)
    { 
      if (atoi(incomingData[1])) 
      {
        digitalWrite(ledPin, HIGH);
      } 
      else 
      {
        digitalWrite(ledPin, LOW);
      }
    }    
  }
}

/*
* Return a mapped input of the raw one, between mapMin and mapMax and applying deadzone
*/
int mappedInput(int rawInput, int mapMin = -100, int mapMax = 100) {
  int input = 0;
  if (abs(rawInput - deadzoneDefault) < deadzoneInterval) {
    input = deadzoneDefault;
  } else {
    input = rawInput;
  }

  input = map(input, joystickMin, joystickMax, mapMin, mapMax);

  return input;
}

// SENSOR INPUT CODE-----------------------------------------------------------
void processSensors() 
{

  x = mappedInput(digitalRead(joyLeft) - digitalRead(joyRight));    
  y = mappedInput(digitalRead(joyUp) - digitalRead(joyDown));

  switchStatus = digitalRead(switchPin);
}

// OUTGOING SERIAL DATA PROCESSING CODE----------------------------------------
void sendDataToSerial()
{
  if (prevX == x && prevY == y && switchStatus == 0) {
    return;
  }

  prevX = x;
  prevY = y;

  Serial.print("input");
  Serial.print(delimiter);
  Serial.print(x);
  Serial.print(delimiter);
  Serial.print(y);
  Serial.print(delimiter);
  Serial.print(switchStatus);
  Serial.println(); // Add final line ending character only once
}

// OUTGOING SERIAL DATA PROCESSING CODE----------------------------------------
void processOutgoingSerial()
{
   // Enter into this only when serial interval has elapsed
  if((millis() - serialPreviousTime) > serialInterval
) 
  {
    // Reset serial interval timestamp
    serialPreviousTime = millis(); 
    sendDataToSerial(); 
  }
}

// INCOMING SERIAL DATA PROCESSING CODE----------------------------------------
void processIncomingSerial()
{
  if(Serial.available()){
    parseData(GetSerialData());
    newData = true;
  }
}

// Gathers bytes from serial port to build inputString
char* GetSerialData()
{
  static char inputString[maxchars]; // Create a char array to store incoming data
  memset(inputString, 0, sizeof(inputString)); // Clear the memory from a pervious reading
  while (Serial.available()){
    Serial.readBytesUntil('\n', inputString, maxchars); //Read every byte in Serial buffer until line end or 64 bytes
  }
  return inputString;
}

// Seperate the data at each delimeter
void parseData(char data[])
{
    char *token = strtok(data, ","); // Find the first delimeter and return the token before it
    int index = 0; // Index to track storage in the array
    while (token != NULL){ // Char* strings terminate w/ a Null character. We'll keep running the command until we hit it
      incomingData[index] = token; // Assign the token to an array
      token = strtok(NULL, ","); // Conintue to the next delimeter
      index++; // incremenet index to store next value
    }
}