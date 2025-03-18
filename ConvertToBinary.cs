using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRGraphics
{
    public class ConvertToBinary
    {
        public List<string> binaryStrings;
        public string message {  get; set; }
        public ConvertToBinary(string message)
        {
            binaryStrings = new List<string>();
            this.message = message;
        }

        public List<int> GetPolynomial()
        {
            List<int> result = new List<int>(binaryStrings.Count - 1);
            
            for(int i = 0; i < binaryStrings.Count - 1; i++)
            {
                result.Add(Convert.ToInt32(binaryStrings[i], 2));
            }

            return result;
        }

        public string[] ReturnArray()
        {
            return binaryStrings.ToArray();
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
                binaryStrings = new List<string>(message.Count() + 1);

                for(int i = 0; i < message.Length; i++)
                {
                    int ASCIIVal = (int)message[i];
                    string shortBinary = Convert.ToString(ASCIIVal, 2);
                    binaryStrings.Add(this.FillSpace(shortBinary));
                }

                binaryStrings[binaryStrings.Count - 1] = "0000";
            }
        }

        public void PrintContent()
        {
            foreach(string i in binaryStrings)
            {
                Console.WriteLine(i);
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
        public void PaddTillCodeWordsAreFulFilled(int length)
        {
            length += 2;
            Console.WriteLine(length.ToString() + " " + message.Length.ToString());
            //length += 2;
            for (int i = 0; i < (length - message.Length - 1 -1) / 2; i++)
            {
                binaryStrings.Add("11101100");
                binaryStrings.Add("00010001");
            }

            if ((length - message.Length) % 2 == 1)
                binaryStrings.Add("11101100");
        }

        /*public List<string> ReturnArray()
        {
            return binaryStrings;
        }*/
    }
}
