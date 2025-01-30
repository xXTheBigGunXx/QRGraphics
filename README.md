# QRGraphics

Welcome to my first GitHub repository! 🎉  

## *About*  
This is my personal project where I aim to re-create a QR code generator. Through this journey, I am learning how to:  
- Work with graphical interfaces.  
- Gather information from the internet about QR codes and their creation.  
- Get familiar with Git and GitHub by committing and managing my code.  

## Goals  
- Build a functional QR code generator.  
- Improve my understanding of GUI development.  
- Gain experience with Git and GitHub workflows.

## Color meaning
  - Grey - are not filled with information
  - Red - finder patterns.
  - Green - seperators.

## *QR code creation explained*

Most expamples will use the version 1 QR grid fromation - 21x21 pixel. Formula to calculate the length of a QR grid formation - N = 4*V + 17.
V - version, N - length in pixels.

## 1. Window
Firstly I just create a screen that is all white. The only thing that is important for this step what the dimentions 
of a QR code output are correct to the desired one. Screen needs to be square and the input and the output dimensions differ
a little bit.

![image](https://github.com/user-attachments/assets/a200bc4e-fc07-44eb-9187-add399e50599)

## 2. Grid values
Second step is keeping and displaying the QR grid pixel vaues. I store pixel values in a integer matrix. I use it because 
I also use the color to denote the different types on zones and their functionality. For now it is important the the QR grid pixels have some padding
also called quite zone, so that it is eassier for QR readers to detect QR code. Grey pixels display the QR grid area and the white - quite zone.

![image](https://github.com/user-attachments/assets/afae2fc8-3f41-4509-a197-f84425ddb233)

## 3. Position finders
Position and orietation finders. Those are used to know the rotation of a QR code, also used to alignment. Those are just three squares on the top 
right and leftcorners and the bottom left. Those squares ar 7 pixels length and width. Postion findrs to not change or move over difference version.
As I mentioned I use the colored pixel to demote the meaning or the use case of a specific area of a QR code.

![image](https://github.com/user-attachments/assets/e9859aa8-de59-4318-867c-7a63d41b43f9)

## 4. Seperators
The seperators are just used for better QR code decoding and are used to seperate the postion areas from other areas.

![image](https://github.com/user-attachments/assets/e7685331-cc3d-4efb-b7f5-f63924c6ca42)

## 5. Timing pattern
Timing patterns are used to specify the that version is the QR code. The timing patterns are located between the top left and bottom left postion finders and also between the
top left and top right positions finders. The length of a timing pattern is N - 16. The first and last pixel of a timing pattern will always be black and then the patter
continues - black pixel, white pixel, black pixel, white pixel. 

![image](https://github.com/user-attachments/assets/13980e4e-5376-4f02-9f5e-0cd7bfacc397)

## 6. Alignmnt pattern
Alignment pattern is used to correctly interpret the QR code at a angle. The alignment pattern shoes up from the second version and above - the first version doesn't have one. 
In this expample I use the version 1 QR code that doesn't have the aligment patter.
![image](https://github.com/user-attachments/assets/00e52e22-3747-424d-805a-bd5749f6d9fb)


