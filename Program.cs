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
            const string message = "HELLO WORLD";

            Encoding.Version(message);
            Console.WriteLine(Encoding.CVersion);
            string FormatPattern = Encoding.FormatPattern();
            string SecondFormatPattern = Encoding.SecondFormatPattern();

            ConvertToBinary convertToBinary = new ConvertToBinary(message);
            Console.WriteLine(convertToBinary.message);
            convertToBinary.ConvertToBinaryStrings();

            convertToBinary.PaddTillCodeWordsAreFulFilled(Encoding.CodeWordsByVersion(Encoding.CVersion));

            convertToBinary.PrintContent();


            List<int> messagePoly = convertToBinary.GetPolynomial();

            Console.WriteLine(new string('-', 60));

            List<int> generatorPoly = GeneratorPoly.GeneratorPolynomial(10);
            generatorPoly.ForEach(x => Console.WriteLine(x));

            const int n = 7;


            List<int> codewords = PolyDivition.DividePolynomials(messagePoly, generatorPoly, n);

            Console.WriteLine(new string('-', 60));

            Console.WriteLine("Generated Codewords: ");
            foreach (var coef in codewords)
            {
                Console.WriteLine(Convert.ToString(coef, 2).PadLeft(8, '0'));
            }

            QRGridFormation qrGridFormation = new QRGridFormation(Encoding.CVersion);

            Painting painting = new Painting(Encoding.CVersion);

            painting.Dimentions(500);
            qrGridFormation.PositionOrientationFinder();
            qrGridFormation.Seperators();
            qrGridFormation.Timing();
            qrGridFormation.PlaceOnePixel();
            qrGridFormation.EncodingMode();
            qrGridFormation.LengthOfBinary(convertToBinary.Length());
            qrGridFormation.AlignmentPattern();
            qrGridFormation.FormatAndVersion(FormatPattern);
            qrGridFormation.FormatAndVersionTwo(SecondFormatPattern);
            qrGridFormation.Content(convertToBinary.ReturnArray());
            qrGridFormation.Masking();

            painting.PlacePixelsOnScreen(qrGridFormation, colorDefault: true);
            painting.RunProgram();

        }
    }
}
