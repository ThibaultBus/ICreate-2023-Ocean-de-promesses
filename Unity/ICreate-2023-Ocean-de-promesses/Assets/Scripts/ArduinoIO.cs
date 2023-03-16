using System;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.Events;

public class ArduinoIO : MonoBehaviour
{
    private static ArduinoIO _instance = null;
    public static ArduinoIO Instance => _instance;
    
    private SerialPort _serialPort;
    // FR  : Dans l'editeur, ecrivez le nom du port serial de votre arduino
    // EN : Into the editor, write your arduino serial port name 
    [SerializeField] private string portName = "COM3";
    [SerializeField] private int baudrate = 9600;
    
    public struct DataInput
    {
        public string type;
        
        /* Input */
        public int x;
        public int y;

        public DataInput(int x, int y, string type = "input")
        {
            this.type = type;
            this.x = x;
            this.y = y;
        }
        public static DataInput Deserialize(string input)
        {
            string[] inputValues = input.Split(',');
            DataInput data = new DataInput
            (
                int.Parse(inputValues[1]),
                int.Parse(inputValues[2]),
                inputValues[0]
            );

            return data;
        }
    }

    public struct DataOutput
    {
        public string type;
        
        /* Output */
        public int light;
        
        public DataOutput(int light, string type = "output")
        {
            this.type = type;
            this.light = light;
        }
        
        public string Serialize()
        {
            return this.type + "," + this.light;
        }
    }

    [System.Serializable]
    public class ArduinoInputEvent : UnityEvent<DataInput> {}
    
    public ArduinoInputEvent onInput;

    private void Start()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this;
        }
        
        DontDestroyOnLoad(this.gameObject);
        
        _serialPort = new SerialPort(portName, baudrate);
        
        try {
            _serialPort.Open();
        }
        catch {
            Debug.Log("Arduino not connected");
        }

        try {
            _serialPort.ReadTimeout = 10;
        }
        catch {} 
    }
    
    void Update()
    {
        if (!_serialPort.IsOpen)
            return;
        try
        {
            try
            {
                string line = _serialPort.ReadLine();
                Debug.Log("input : " + line);
                DataInput data = DataInput.Deserialize(line);
                //Debug.Log(data.x + "\t" + data.y);
                if (data.type != "input")
                    return;
                   
                onInput.Invoke(data);
            }
            catch (ArgumentException e)
            {
            }
        }
        catch (System.Exception e)
        {
            // ignored
        }
    }
    
    private void OnDisable()
    {
        _serialPort.Close();
    }

    public void SendOutput(DataOutput data)
    {
        if (!_serialPort.IsOpen)
            return;
        
        string output = data.Serialize();
        _serialPort.WriteLine(output);
        Debug.Log(output);
        
    }
}
