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
  - Orange - timing pattern.
  - Cyan - encoding mode.
  - Pink - Length of a message.

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
placing the bytes like a Z letter: (1 - represent the black pixel and 0 - white)

![image](https://github.com/user-attachments/assets/94e7b489-8dd0-4b99-9203-e77ca59246f0)

![image](https://github.com/user-attachments/assets/4e9d4853-8899-47f5-b872-69fc6107c2eb)

## 9. Message length
The top byte near the encoding mode half byte in for storing the messages length. For version 1-9 the length is stored in one bye and in version 10 and more in two bytes.
And the byte or two bytes for the length is stored in the same Z pattern.
![image](https://github.com/user-attachments/assets/7b3fe7da-733f-4e02-885d-e49db228afb7)

## 10. Format information and the error correction
The format information shows the error correction level of a QR code and what mask does the QR use.
Error correction:
Low: bites - 01; integer - 1
Medium: bites 00; integer - 0
Quartile: bites 11; integer - 3
High: bites 10; integer - 2

For masking information:

![image](https://github.com/user-attachments/assets/ba97a422-23fa-4622-8230-1ca233d2aa6e)
Information from: https://observablehq.com/@zavierhenry/encoding-qr-codes#versionErrorCorrectionBits

The pattern index should be represented in a 3 long bites. Example: pattern 2 (which I use for the examples) in binary form is 010.

There are two areas were the format information and the error correction 10 bites stores: the areas showned in blue

![image](https://github.com/user-attachments/assets/0fbb4497-e4cd-48f0-a2c6-85ff71e2ce21)

What is how the format and error correction information wrap around the position finders:

![image](https://github.com/user-attachments/assets/afe6ffd5-5aa2-41bf-ab2c-43cb84bc20ad)

Information from: https://observablehq.com/@zavierhenry/encoding-qr-codes#versionErrorCorrectionBits

Addicional information is added then the version of a QR code is 7 and above. 

![image](https://github.com/user-attachments/assets/45f60cbd-4fcb-4bb6-aa9b-501786994d6f)
The new information is all showed in the blue color near the top right and bottom left postion finders.

## 11. The data itself
The data length on the character cont and the  encoding (Byte, Kanji) mode. For examples I use the byte encoding that stores the information as the ACSII values converted to a binary string. 

![image](https://github.com/user-attachments/assets/511777d5-6cfa-474f-8546-a045b4d84628)
The data just shows the 255 character in the ASCII table - it is just a binary string of 11111111. At the end the 2x2 white square is placed to show the end of a message.

The information in bites should be represented as the Z pattern:

![image](https://github.com/user-attachments/assets/867f473d-55ba-42bd-b2b2-d1af60e6d746)
Information from: https://www.youtube.com/watch?v=142TGhaTMtI&t=199s&ab_channel=JamesExplains

## 12. Masking pattern
Masking is used to improve the readabily for the QR scanners and scanning accuracy. It is used the make shore that the scanner could seperate the white and the black pixels.
The pattern number should be converted to a 3 long bites in the integer form. Those are the patterns:
![image](https://github.com/user-attachments/assets/ea10b6ec-82fe-45b0-a0bb-b1edd5d22dcc)
Information from: https://dev.to/maxart2501/let-s-develop-a-qr-code-generator-part-v-masking-30dl

## 13. Error correction for the data
