using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace QRGraphics
{
    public class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            string message = "HELLO WORLD";


            Encoding.Version(message);
            Console.WriteLine(Encoding.CVersion);
            string FormatPattern = Encoding.FormatPattern();
            string SecondFormatPattern = Encoding.SecondFormatPattern();

            ConvertToBinary convertToBinary = new ConvertToBinary(message);
            Console.WriteLine(convertToBinary.message);
            convertToBinary.ConvertToBinaryStrings();

            convertToBinary.PaddTillCodeWordsAreFulFilled(Encoding.CodeWordsByVersion(Encoding.CVersion));

            convertToBinary.PrintContent();

            Console.WriteLine(new string('-', 60));

            List<int> messagePoly = convertToBinary.GetPolynomial();

            List<int> messagePolyOnlyData = new List<int>();

            for (int i = 0; i < message.Length; i++)
            {
                messagePolyOnlyData.Add(messagePoly[i]);
            }

            messagePolyOnlyData.ForEach((x) => Console.WriteLine(x + " " + (char)x));

            Console.WriteLine(new string('-', 60));

           

            QRGridFormation qrGridFormation = new QRGridFormation(Encoding.CVersion);

            Painting painting = new Painting(Encoding.CVersion);

            painting.Dimentions(500);
            qrGridFormation.PositionOrientationFinder();
            qrGridFormation.Seperators();
            qrGridFormation.Timing();
            qrGridFormation.PlaceOnePixel();
            qrGridFormation.EncodingMode();
            qrGridFormation.LengthOfBinary(message.Length);
            qrGridFormation.AlignmentPattern();
            qrGridFormation.FormatAndVersion(FormatPattern);
            qrGridFormation.FormatAndVersionTwo(SecondFormatPattern);
            //Console.WriteLine(qrGridFormation.CalculateEmptySpace());

            Encoding.CreateCountForErrorCodeWords(qrGridFormation.CalculateEmptySpace());

            //Console.WriteLine(Encoding.ErrorCodeWordCount);

            List<int> generatorPoly = GeneratorPoly.GeneratorPolynomial(Encoding.ErrorCodeWordCount);
            generatorPoly.ForEach(x => Console.WriteLine(x));

            int n = Encoding.ErrorCodeWordCount;

            List<int> codewords = new List<int>();
            codewords = PolyDivition.DividePolynomials(messagePoly, generatorPoly, n);

            Console.WriteLine(new string('-', 60));

            Console.WriteLine("Generated Codewords: ");
            foreach (int coef in codewords)
            {
                Console.WriteLine(Convert.ToString(coef, 2).PadLeft(8, '0'));
                //Console.WriteLine(coef);
            }


            qrGridFormation.Content(convertToBinary.ReturnArray());
            qrGridFormation.Masking();

            painting.PlacePixelsOnScreen(qrGridFormation, colorDefault: true);
            painting.RunProgram();

        }
    }
}
