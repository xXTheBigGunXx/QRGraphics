using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRGraphics
{
    public static class Encoding
    {
        private static string CFormatAndPattern = "01" + "010";
        private static string CPolynomial = "10100110111";
        private static string CFinalPattern = "101010000010010";
        private static StringBuilder biteMask = new StringBuilder(CFormatAndPattern + new string('0',10));

        private static string CSecondPattern = "1111100100101";
        private static StringBuilder versionBinary  = new StringBuilder(Convert.ToString(CVersion, 2) + new string('0', 12));

        public static int CVersion;
        public static int ErrorCodeWordCount;

        public static void CreateCountForErrorCodeWords(int length)
        {
            ErrorCodeWordCount = (int)Math.Ceiling(((double)length - ((CodeWordsByVersion(CVersion) + 2) * 8)) / 8);
            if(CVersion == 1)
                ErrorCodeWordCount++;   
        }

        public static void Version(string message)
        {
            if (message.Length <= 17)
                CVersion = 1;
            else
            {
                CVersion = 2;
                for (int i = 2; i < 40; i++)
                {
                    if (message.Length > CodeWordsByVersion(i))
                        CVersion = i + 1;
                    else
                        break;
                }
            }
        }

        public static int CodeWordsByVersion(int Version)
        {
            switch (Version)
            {
                case 1: return 17;
                case 2: return 32;
                case 3: return 53;
                case 4: return 78;
                case 5: return 106;
                case 6: return 134;
                case 7: return 154;
                case 8: return 192;
                case 9: return 230;
                case 10: return 271;
                default: return 0;
            }
        }
        public static string FormatPattern()
        {
            do
            {
                biteMask = NewFormatString(biteMask);
            }
            while (biteMask.Length > 10);

            if (biteMask.Length < 10)
            {
                biteMask.Insert(0, new string('0', 10 - biteMask.Length));
            }

            biteMask.Insert(0, CFormatAndPattern);

            biteMask = XOR(biteMask, new StringBuilder(CFinalPattern));

            return biteMask.ToString();
        }

        public static string SecondFormatPattern()
        {
            do
            {
                versionBinary = SecondNewFormatString();
            }
            while (versionBinary.Length > 12);

            if(versionBinary.Length < 12)
            {
                versionBinary.Insert(0, new string('0', 12 - versionBinary.Length));
            }
            StringBuilder temp = new StringBuilder(Convert.ToString(CVersion, 2));
            temp.Insert(0, new string('0', 18 - temp.Length - versionBinary.Length));
            versionBinary.Insert(0, temp.ToString());

            return versionBinary.ToString();
        }

        private static StringBuilder SecondNewFormatString()
        {
            StringBuilder polyCopy = new StringBuilder(CSecondPattern);
            polyCopy.Append(new string('0', versionBinary.Length - polyCopy.Length));

            StringBuilder result = XOR(versionBinary, polyCopy);
            Console.WriteLine(result.ToString());
            RemoveZeros(result);

            return result;
        }

        private static StringBuilder NewFormatString(StringBuilder biteMask)
        {
            StringBuilder polyCopy = new StringBuilder(CPolynomial);

            RemoveZeros(biteMask);
            polyCopy.Append(new string('0', biteMask.Length - polyCopy.Length));

            StringBuilder result = XOR(biteMask, polyCopy);
            RemoveZeros(result);

            return result;
        }

        private static StringBuilder NewFormatStringTwo(StringBuilder versionBinary)
        {
            return new StringBuilder(versionBinary.ToString());
        }

        private static StringBuilder XOR(StringBuilder first, StringBuilder second)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < first.Length; i++)
            {
                int tempOne = first[i] - '0';
                int tempTwo = second[i] - '0';

                if (tempOne + tempTwo == 1)
                {
                    result.Append(1);
                }
                else
                {
                    result.Append(0);
                }
            }
            return result;
        }

        private static void RemoveZeros(StringBuilder strBuilder)
        {
            while (strBuilder[0] == '0' && strBuilder.Length > 1)
            {
                strBuilder.Remove(0, 1);
            }
        }

        public static int ConvertToLen(int version)
        {
            return 21 + ((version - 1) * 4);
        }
    }
}
