# QRGraphics

Welcome to my first GitHub repository! 🎉  

## About  
This is my personal project where I aim to re-create a QR code generator. Through this journey, I am learning how to:  
- Work with graphical interfaces.  
- Gather information from the internet about QR codes and their creation.  
- Get familiar with Git and GitHub by committing and managing my code.  

## Goals  
- Build a functional QR code generator.  
- Improve my understanding of GUI development.  
- Gain experience with Git and GitHub workflows.

## QR code creation explained

1. Firstly I just create a screen that is all white. The only thing that is important for this step what the dimentions 
of a QR code output are correct to the desired one. Screen needs to be square and the input and the output dimensions differ
a little bit.
![image](https://github.com/user-attachments/assets/f61b7343-b826-413e-9fa2-b837031e354b)

1. Second step is keeping and displaying the QR grid pixel vaues. I store pixel values in a integer matrix. I use it because 
I also use the color to denote the different types on zones and their functionality. For now it is important the the QR grid pixels have some padding
also called quite zone, so that it is eassier for QR readers to detect QR code. Grey pixels display the QR grid area and the white - quite zone.
![image](https://github.com/user-attachments/assets/afae2fc8-3f41-4509-a197-f84425ddb233)

1.


