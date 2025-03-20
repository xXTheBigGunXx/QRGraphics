using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlTypes;
using System.Drawing.Text;
namespace QRGraphics
{
    public sealed class QRGridFormation
    {
        private int[,] matrix;
        // Odd number - black pixel, even - white.
        private int pixelsInLen {  get; set; }

        private readonly int version;

        public QRGridFormation(int version)
        {
            this.version = version;
            this.pixelsInLen = Encoding.ConvertToLen(version);
            matrix = new int[this.pixelsInLen, this.pixelsInLen];

            for(int i = 0; i < this.pixelsInLen; i++)
            {
                for(int j = 0; j < this.pixelsInLen; j++)
                {
                    matrix[i, j] = -1;
                }
            }
        }

        public int CalculateEmptySpace()
        {
            int count = 0;

            for (int i = 0; i < this.pixelsInLen; i++)
            {
                for (int j = 0; j < this.pixelsInLen; j++)
                {
                    if (matrix[i, j] == -1) count++;
                }
            }

            return count;
        }
        public int Get(int i, int j)
        {
            if(i < 0 || i > matrix.GetLength(0))
            {
                return int.MinValue;
            }
            else if (j < 0 || j > matrix.GetLength(1))
            {
                return int.MaxValue;
            }
            return matrix[i, j];
        }

        public void Put(int i, int j, int value)
        {
            matrix[j, i] = value;
        }

        public int Length()
        {
            return matrix.GetLength(0);
        }

        public int Width()
        {
            return matrix.GetLength(1);
        }

        public void PositionOrientationFinder()
        {
            string[] codes = {  "1111111",
                                "1000001",
                                "1011101",
                                "1011101",
                                "1011101",
                                "1000001",
                                "1111111",};

            for(int i = 0; i < 7; i++)
            {
                for(int j = 0; j < 7; j++)
                {
                    int value = codes[i][j] == '1' ? 1 : 0;
                    int incrament = this.pixelsInLen - codes[i].Length;

                    matrix[i, j] = value;
                    matrix[i + incrament, j] = value;
                    matrix[i, j + incrament] = value;
                }
            }
        }

        public void Seperators()
        {
            for(int i = 0; i < 8; i++)
            {
                matrix[i, 7] = 20;
                matrix[i,this.pixelsInLen - 8] = 20;
                matrix[i + this.pixelsInLen - 8, 7] = 20;
            }

            for(int i = 0; i < 7; i++)
            {
                matrix[7,i] = 20;
                matrix[7,this.pixelsInLen - 7 + i] = 20;
                matrix[this.pixelsInLen - 8, i] = 20;
            }
        }
        public void Timing()
        {
            for(int i = 8; i < this.pixelsInLen - 8; i++)
            {
                matrix[i,6] = (i % 2 == 0) ? 5 : 4;
                matrix[6, i] = matrix[i, 6];
            }
        }

        public void PlaceOnePixel()
        {
            matrix[this.pixelsInLen - 8, 8] = 1;
        }

        public void EncodingMode()
        {
            matrix[this.pixelsInLen - 1, this.pixelsInLen - 1] = 6;
            matrix[this.pixelsInLen - 1, this.pixelsInLen - 2] = 7;
            matrix[this.pixelsInLen - 2, this.pixelsInLen - 1] = 6;
            matrix[this.pixelsInLen - 2, this.pixelsInLen - 2] = 6;
        }

        public void LengthOfBinary(int length=0)
        {
            string binaryRepresentation = Convert.ToString(length, 2);
            
            int lengthBytes = (this.version < 10) ? 8 : 16;

            while(binaryRepresentation.Length < lengthBytes)
            {
                binaryRepresentation = '0' + binaryRepresentation;
            }

            //Console.WriteLine(binaryRepresentation);

            for(int i = 0; i < lengthBytes / 2; i++)
            {
                for(int j = 0; j < 2; j++)
                {
                    int y = this.pixelsInLen - 3 - i;
                    int x = this.pixelsInLen - 1 - j;
                    int strIndex = (i * 2) + j;

                    matrix[y, x] = Convert.ToInt32(binaryRepresentation[strIndex]) - '0' + 8;
                }
            }
        }

        public void AlignmentPattern()
        {
            List<int> list = this.GenerateCoordinates();

            for(int i = 0; i < list.Count; i++)
            {
                for(int j = 0; j < list.Count; j++)
                {
                    bool overlap = this.Overlapping(list[i] - 2, list[j] - 2);
                    if(!overlap)
                    {
                        this.PlaceAlignamenPattern(list[i] - 2, list[j] - 2);
                    }
                }
            }
        }

        public void FormatAndVersion(string FormatPattern)
        {
            int index = 0;

            for(int i = 0; index < 7; i++)
            {
                if (matrix[8, i] == -1)
                {
                    matrix[8, i] = FormatPattern[index++] - '0' + 10;
                }
            }

            for(int i = 0; i < 8; i++)
            {
                matrix[8, this.pixelsInLen - 8 + i] = FormatPattern[index++] - '0' + 10;
            }

            index = 0;
            for(int i = 0; i < 7; i++)
            {
                matrix[this.pixelsInLen - 1 - i, 8] = FormatPattern[index++] - '0' + 10;
            }

            for(int i = 0; index < 15; i++)
            {
                if (matrix[8 - i, 8] == -1)
                {
                    matrix[8 - i, 8] = FormatPattern[index++] - '0' + 10;
                }
            }
        }
        /// <summary>
        /// For versions 7 and above
        /// </summary>
        /// <param name="FormatPattern"></param>
        public void FormatAndVersionTwo(string FormatPattern)
        {
            //Console.WriteLine(FormatPattern);
            if(Encoding.CVersion < 7)
            {
                return;
            }
            
            for(int i = 0; i < 6; i++)
            { 
                for(int j = 0; j < 3; j++)
                {
                    matrix[this.pixelsInLen - 11 + j, i] = FormatPattern[FormatPattern.Length - i * 3 - j - 1] - '0' + 10;
                }
            }

            for(int i = 0; i < 6; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    matrix[i, this.pixelsInLen - 11 + j] = FormatPattern[FormatPattern.Length -  i * 3 - j - 1] - '0' + 10;
                }
            }
        }

        private void PlaceAlignamenPattern(int x, int y)
        {
            string[] codes =
            {
                "11111",
                "10001",
                "10101",
                "10001",
                "11111"
            };

            for(int i = 0; i < codes.Length; i++)
            {
                for(int j = 0; j < codes[0].Length; j++)
                {
                    int value = (codes[i][j] == '1') ? 3: 2;
                    matrix[x + i,y + j] = value;
                }
            }
        }

        private bool Overlapping(int x, int y)
        {
            for(int i = 0; i < 5; i++)
            {
                for(int j = 0; j < 5; j++)
                {
                    if (matrix[x + i, y + j] != -1)
                    {
                        if(!(matrix[x + i, y + j] == 4 || matrix[x + i, y + j] == 5))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public int CountLeftSpace()
        {
            int count = 0;

            for(int i = 0; i < this.pixelsInLen; i++)
            {
                for(int j = 0; j < this.pixelsInLen; j++)
                {
                    if (matrix[i,j] == -1)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public void Content(string[] binaryStrings)
        {
            string binary = String.Join("", binaryStrings);
            int index = 0;
            int xOffset = this.pixelsInLen - 1;

            for (int i = 0; i < this.pixelsInLen / 4 && index != binary.Length; i++)
            {
                Up(xOffset, binary, ref index);
                xOffset -= 2;
                Down(xOffset, binary, ref index);
                xOffset -= 2;
            }

            Up(xOffset, binary, ref index);
        }

        private void Up(int xOffset, string binary, ref int index)
        {
            for(int i = 0; i < this.pixelsInLen; i++)
            {
                for(int j = 0; j < 2; j++)
                {
                    if (index == binary.Length)
                    {
                        return;
                    }
                    else if (xOffset - j > 0 && matrix[this.pixelsInLen - i - 1, xOffset - j] == -1)
                    {
                        matrix[this.pixelsInLen - i - 1, xOffset - j] = binary[index++] - '0' + 12;
                    }
                }
            }
        }

        private void Down(int xOffset, string binary, ref int index)
        {
            for (int i = 0; i < this.pixelsInLen; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (index == binary.Length)
                    {
                        return;
                    }
                    else if (matrix[i, xOffset - j] == -1)
                    {
                        matrix[i, xOffset - j] = binary[index++] - '0' + 12;
                    }
                }
            }
        }

        public void Masking()
        {
            for(int i = 0; i < this.pixelsInLen; i+=3)
            {
                for(int j = 0; j < this.pixelsInLen; j++)
                {
                    if (matrix[j,i] == 13)
                    {
                        matrix[j, i] = 12;
                    }
                }
            }
        }

        private List<int> GenerateCoordinates()
        {
            List<int> coordinates = new List<int>();

            if(this.version == 1)
            {
                return coordinates;
            }

            int count = (int)Math.Floor(this.version / 7.0M) + 2;
            int step = (int)Math.Floor((this.pixelsInLen - 13) / (decimal)(count - 1));

            for(int i = 0; i < count; i++)
            {
                int coord = 6 + step * i;
                coordinates.Add(coord);
            }

            return coordinates;
        }

        public void PlaceAll()
        {
            this.PositionOrientationFinder();
            this.Seperators();
            this.Timing();
            this.PlaceOnePixel();
            this.EncodingMode();
            this.AlignmentPattern();
        }
    }
}
