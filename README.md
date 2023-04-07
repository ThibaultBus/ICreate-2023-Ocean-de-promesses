# Un océan de promesse

![](public/FinaImageICreate.jpg)

This project is an installation with the aim of presenting **K'Help**, a robot capable of sowing kelp to help recover biodiversity in areas deteriorated by human activity, to increase the capacity of absorption of CO₂ of the oceans and to contribute to their deacidification. More information on the benefits of kelp on the [Kelp Forest Alliance's website](https://kelpforestalliance.com/).

This project requires two screens and a computer with Unity **2021.3.19f1** installed.

The game starts up with a menu explaining general information about K'Help. When you leave the menu, you can control the drone. You should now find zones that are suitable for sowing algae (i.e. zones that have been deteriorated by human activity), such zones are delimited by a thin white outline. When you're in such a zone, check on your radar for blue spots, these spots show a relief on the ocean floor, a perfect area to seed your kelp. When you're on such a spot, press the action button and voilà !


## Arduino

This project is usable with a keyboard only (though you would need to disable the `ArduinoIO` game object and enable the `KeyboardInputManager` component instead of `ArduinoInputManager` on the player). However, it was not intended as such, and a complete installation does require some material. You would need :
- An Arduino UNO or compatible board, and a cable to connect it to your computer
- A 4-way Pacman-style joystick, like the one presented [here](https://projecthub.arduino.cc/ejshea/b5fc2ef3-2378-48bf-9632-2bbcf9b0a2d0) and [here](https://www.creatroninc.com/product/4-way-arcade-joystick/)
- A switch or button
- quite a few cables

You should first wire up every component to your Arduino. `Joystick.ino` in **Arduino/Joystick** should be uploaded to your Arduino, to send and receive data from unity. Ensure every pin in your installation matches the ones in the code, and change the constants' values if needed. You can take a look to Arduino's serial console to ensure data is sent when you move the joystick.

## Unity

Launch the project with Unity **2021.3.19f1**.
Open the Menu scene and run the game from there (or build the project for your desired target).

⚠️ Warning : Check Unity's console for an error message, ensure the communication with your Arduino board is well established if you use one.

### Terrain

The terrain was made with [Unity's Terrain Tools](https://docs.unity3d.com/Packages/com.unity.terrain-tools@2.0/manual/getting-started-with-terrain-tools.html) and [Unity's Terrain Sample Asset](https://assetstore.unity.com/packages/3d/environments/landscapes/terrain-sample-asset-pack-145808).

### Movement

The movement was built upon the `PlayerController` script from a previous project by Thibault Bustarret.

### Fish

For the part related to fish generation and their movement, we used the animatedfish packet created by Quaternius (see credits.txt) which contains the following fish assets:
- Dolphin

- Fish1

- Fish2

- Fish3

- Shark 

- Whale

- Manta Ray

These assets could be found inside **Assets/fish** folder

For generating a sufficient number of these fish we wrote a Spowning script inside the Spowner object in SampleScene (Assets/Scenes/SampleScene). 

In this script, an array of GameObjects is given, where any number of fish could be added to it, then a loop generates for each gameObject 7 identical instants ( **numObjects** variable in the script) distributed randomly in different positions inside the boundaries of our terrain. 

As for the movement of the fish, We implemented a simple movement trajectory where the fish oscilate vertically moving through the terrain, this movement was defined in the FishMovement.cs script. (Found in the Assets/Scripts folder)

##### Possible future work: 

1. Adding more complex and artistic movement to the fish such as jumping from water

2. We also developped an underwater view with a terrain that we worked on using blender (can be found in public/Cool-Surface in the github repository), where the user could get a more immersive experience in discovering the underwater life of algae and fish.


