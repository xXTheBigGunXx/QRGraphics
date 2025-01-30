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
Alignment pattern is used to correctly interpret the QR code at a angle. The alignment pattern shows up from the second version and above - the first version doesn't have one. 
In this expample I use the version 1 QR code that doesn't have the aligment patter.

![image](https://github.com/user-attachments/assets/00e52e22-3747-424d-805a-bd5749f6d9fb)

This is version 2 with the aligment pattern:

![image](https://github.com/user-attachments/assets/bd100bdc-dca3-42d1-aec0-c46ce836c9f3)

This is version 7:

![image](https://github.com/user-attachments/assets/78c9702f-02a3-4eaf-9ff6-708f1f82a318)

For every seventh version (starting from version 1) the aligment patterns are added. Version 1 - 0 aligment patterns; Version 2 - 6 : 1 aligment pattern; 
Version 7 - 13 : 6 aligment patterns; Version 14 - 20 : 13 alignment patterns and so on.
The count of aligment patterns are calculated with the formula - C = N // 7 + 2. And the step or the spaces in pixels between the aligment patters - D =  N - 13 // (C-1). C = count and D = distance.
The first aligment pattern will always be the coordinates x = N - 8 and y = N - 8. The coordinates start at 0. Then we create I create a list of coordinates in the range of 0 and the N with quantity count and the 6 + distance * i 
(i starts at 0 and end at count -1):
![image](https://github.com/user-attachments/assets/66bc999f-b9cc-4b0e-80c8-8f870050d760)

Then I loop throut the list two times with two loop and the every single coordinte of a top left corner of a aligment pattern. Then I need to check if a aligment pattern touch seperators, position finders and if they do - discarde them. 
That the reason why there are no aligment patterns near 3 position finders of QR grid.

## 7. One single pixel
The pixel at the postion (8, N - 8) is black.

![image](https://github.com/user-attachments/assets/964925a3-770f-4c38-bcbf-683bac88fab3)

## 8. Encoding mode
QR code has 4 encoding modes : numeric, alphanumeric, byte and kanji.
Numeric - values of 0-9 and the half byte encoding in binary: 0b0001
Alphanumeric - 0–9, A–Z (upper-case only), space $, %, *, +, -, ., /, and half byte encoding in binary : 0b0010
Byte - extended ASCII simbols converted from integer to binary and half byte encoding in binary: ob0100
Kanji - chines simbols used in japanese language and half byte encoding in binary: 0b1000
In this example the encoding in byte with the half byte of 0b0100. The half byte in placed in the bottom right corner and 
placing the bytes like a Z letter:
![image](https://github.com/user-attachments/assets/94e7b489-8dd0-4b99-9203-e77ca59246f0)

![image](https://github.com/user-attachments/assets/a58e2035-c23e-40b4-a8f9-d751cbd34d00)


