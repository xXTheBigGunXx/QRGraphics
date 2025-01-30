using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using STH1123.ReedSolomon;

namespace QRGraphics
{
    public class ReedSolomon
    {
        public string[] Run(string mess)
        {
            byte[] data = new byte[mess.Length];

            for(int i = 0; i < mess.Length; i++)
            {
                byte temp = (byte)mess[i];
                data[i] = temp;
            }

            GenericGF field = new GenericGF(285, 256, 0);
            ReedSolomonEncoder encoder = new ReedSolomonEncoder(field);

            int ecCodewords = mess.Length;

            // Create int array for encoding
            int[] intArray = new int[data.Length + ecCodewords];
            for (int i = 0; i < data.Length; i++)
            {
                intArray[i] = data[i];
            }

            Console.WriteLine("Input Data:");
            for (int i = 0; i < data.Length; i++)
            {
                Console.WriteLine($"Byte {i + 1}: {intArray[i]}");
            }

            // Encode the data
            encoder.Encode(intArray, ecCodewords);

            string[] arr = new string[intArray.Length];

            Console.WriteLine("\nFinal Encoded Data (Data + Error Correction):");
            for (int i = 0; i < intArray.Length; i++)
            {
                Console.WriteLine($"Byte {i + 1}: {intArray[i]} (Binary: {Convert.ToString(intArray[i], 2).PadLeft(8, '0')})");
                arr[i] = Convert.ToString(intArray[i], 2).PadLeft(8, '0');
            }

            return arr;
        }

        public string[] Run(string mess, bool smt)
        {
            byte[] data = new byte[mess.Length];

            for (int i = 0; i < mess.Length; i++)
            {
                byte temp = (byte)mess[i];
                data[i] = temp;
            }

            GenericGF field = new GenericGF(285, 256, 0);
            ReedSolomonEncoder encoder = new ReedSolomonEncoder(field);

            int ecCodewords = 1;

            int[] intArray = new int[data.Length * 3];

            for(int i = 0; i < data.Length; i++)
            {
                intArray[i] = data[i];
            }

            int count = data.Length;

            for(int i = 0; i < data.Length; i++)
            {
                int[] temp = new int[2];
                temp[0] = intArray[i];

                encoder.Encode(temp, ecCodewords);

                for(int j = 0; j < 2; j++)
                {
                    intArray[count++] = temp[j];
                }
            }

            string[] strings = new string[intArray.Length];

            for(int i = 0; i < intArray.Length; i++)
            {
                //Console.WriteLine($"Byte {i + 1}: {intArray[i]} (Binary: {Convert.ToString(intArray[i], 2).PadLeft(8, '0')})");
                strings[i] = Convert.ToString(intArray[i], 2).PadLeft(8, '0');
            }
            return strings;
        }
    }
}
