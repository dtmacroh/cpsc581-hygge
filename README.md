# README 
## Prerequisites 
1. Have Unity 2017 installed on your machine.
2. Have a Windows OS on your machine.
3. Kinect 2.0 - have the hardware and install the Kinect 2.0

## Executable
1. Locate workplacerhythm.exe and run with the Kinect plugged in.

## Source code
1. Download workplacerhythm.package and drag it into the Unity Editor. 
2. From the Scenes folder, select Kinect + OpenCV3 + NetworkIt scene and run.

## Creating Executable from Source Code
You will need to setup the following for Unity in the player settings:

Player settings:
* *Resolution and Presentation > Run In Background* should be set to **true**
* *File > Build Settings... > Player Settings... > Other Settings > Configuration> API Compatibility Level* should be set to **.NET 2.0**

Base source code taken from 
https://github.com/kevinta893/NetworkIt/releases/download/1.0.0/NetworkItUnity-KinectV2-OpenCV3-v1.1.zip