using System;
using System.Collections.Generic;
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
            ReedSolomon temp = new ReedSolomon();
            string FormatPattern = Encoding.FormatPattern();
            //Console.WriteLine(FormatPattern + "Format pattern");
            string SecondFormatPattern = Encoding.SecondFormatPattern();

            ConvertToBinary convertToBinary = new ConvertToBinary(/*"MatasMatasMatas"*/ "HELLO WORLD");
            string mess = convertToBinary.ReturnMessage();
            //Console.WriteLine(mess);

            convertToBinary.ConvertToBinaryStrings();
            //convertToBinary.PrintContent();

            string [] arr = temp.Run(mess, true);

            List<int> messagePoly = convertToBinary.GetPolynomial();
            messagePoly.ForEach(i => Console.WriteLine(i));

            Console.WriteLine(new string('-', 60));

            List<int> generatorPoly = GeneratorPoly.GeneratorPolynomial(20);
            generatorPoly.ForEach(x => Console.WriteLine(x));

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
            //qrGridFormation.Content(arr);
            //qrGridFormation.Content(convertToBinary.ReturnArray());
            //Console.WriteLine(new string('-', 20) + qrGridFormation.CountLeftSpace());
            qrGridFormation.Masking();


            /*for (int i = 0; i < qrGridFormation.Length(); i++)
            {
                for (int j = 0; j < qrGridFormation.Width(); j++)
                {
                    Console.Write($"{qrGridFormation.Get(i, j),3} ");
                }
                Console.WriteLine();
            }*/

            painting.PlacePixelsOnScreen(qrGridFormation, colorDefault: true);
            painting.RunProgram();

        }
    }
}
