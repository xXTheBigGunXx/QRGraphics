using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRGraphics
{
    public class ConvertToBinary
    {
        private string[] binaryStrings;
        private string message {  get; set; }
        public ConvertToBinary()
        {
            message = string.Empty;
            this.GetMessage();
        }
        public ConvertToBinary(string message)
        {
            this.message = message;
        }

        public List<int> GetPolynomial()
        {
            List<int> result = new List<int>(binaryStrings.Length - 1);
            
            for(int i = 0; i < binaryStrings.Length - 1; i++)
            {
                result.Add(Convert.ToInt32(binaryStrings[i], 2));
            }

            return result;
        }

        public string ReturnMessage()
        {
            return this.message;
        }

        public int Length()
        {
            return this.message.Length;
        }

        private void GetMessage()
        {
            message = Console.ReadLine();
        }

        public void ConvertToBinaryStrings()
        {
            if(message != string.Empty)
            {
                binaryStrings = new string[message.Length + 1];

                for(int i = 0; i < message.Length; i++)
                {
                    int ASCIIVal = (int)message[i];
                    string shortBinary = Convert.ToString(ASCIIVal, 2);
                    binaryStrings[i] = this.FillSpace(shortBinary);
                }

                binaryStrings[binaryStrings.Length - 1] = "0000";
            }
        }

        public void PrintContent()
        {
            for(int i = 0; i < binaryStrings.Length; i++)
            {
                Console.WriteLine(binaryStrings[i]);
            }
        }

        private string FillSpace(string binary)
        {
            while(binary.Length < 8)
            {
                binary = '0' + binary;
            }
            return binary;
        }

        public string[] ReturnArray()
        {
            return binaryStrings;
        }
    }
}
