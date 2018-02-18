This is the arduino code that controls the Bionic uHand. 
It receives messages from its hardware serial port and issues commands to the bionic uhand via a software serial.
Make sure you copy the LobotServoController.[h|cpp] files into the Arduino Libraries directory under 
a new directory called LobotServoController and remove the .lib extension. The .lib extension is only there to 
force the arduino ide to ignore those files.