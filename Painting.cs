using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Windows.Forms;

namespace QRGraphics
{
    public sealed class Painting
    {
        public Form form;
        public int cubeSize { get; set; }
        public int pixelsInLen {  get; set; }

        private Dictionary<int, Brush> colors = new Dictionary<int, Brush>()
        {
            { -1, new SolidBrush((Color.FromArgb(128,128,128)))}, // - default
            { 0, new SolidBrush(Color.FromArgb(255, 128, 128))}, // - position
            { 1, new SolidBrush(Color.FromArgb(127, 0, 0))}, // - postion
            { 2, new SolidBrush((Color.FromArgb(255, 170, 204)))},// 2 for alignment patter
            { 3, new SolidBrush((Color.FromArgb(127, 42, 76)))},// alignment
            { 5, new SolidBrush((Color.FromArgb(127, 51, 0)))}, // - timing
            { 4, new SolidBrush((Color.FromArgb(255, 179, 128)))}, // - timing
            { 6, new SolidBrush((Color.FromArgb(137, 234, 223)))}, // Enc mode
            { 7, new SolidBrush((Color.FromArgb(178, 60, 0)))}, // Enc mode
            { 8, new SolidBrush((Color.FromArgb(240, 155, 255)))}, // Length
            { 9, new SolidBrush((Color.FromArgb(127, 42, 76)))}, // Length
            { 10, new SolidBrush((Color.FromArgb(128, 128, 255)))}, // Error correction
            { 11, new SolidBrush((Color.FromArgb(0, 0, 127)))}, // Errorc correction
            { 12, new SolidBrush((Color.FromArgb(178, 189, 181)))}, // Data
            { 13, new SolidBrush((Color.FromArgb(150, 150, 150)))}, // Data
            { 14, new SolidBrush((Color.FromArgb(80, 80, 80)))},
            { 15, new SolidBrush((Color.FromArgb(60, 60, 60)))},
            { 20, new SolidBrush((Color.FromArgb(128, 213, 128))) }, 
        };

        public Painting(int version)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            form = new Form();

            form.BackColor = Color.White;
            form.Text = "QR code window.";

            this.pixelsInLen = Encoding.ConvertToLen(version);
        }
        public void Dimentions(int sizeLen)
        {
            this.cubeSize = (int)(sizeLen / this.pixelsInLen);
            this.WhiteBorder();
        }

        private void WhiteBorder()
        {
            form.Width = (pixelsInLen + 9) * this.cubeSize;
            form.Height = (pixelsInLen + 9) * this.cubeSize + SystemInformation.CaptionHeight;
        }

        public void PlacePixelsOnScreen(QRGridFormation qrGridFormation, bool colorDefault = false)
        {
            form.Paint += (sender, e) =>
            {
                Graphics cube = e.Graphics;
                for (int i = 0; i < qrGridFormation.Length(); i++)
                {
                    for (int j = 0; j < qrGridFormation.Width(); j++)
                    {
                        int code = qrGridFormation.Get(i, j);
                        Brush color;

                        if(!colorDefault)
                        {
                            color = (code % 2 == 1) ? Brushes.Black : Brushes.White;
                        }
                        else
                        {
                            color = colors[code];
                        }
                        
                        cube.FillRectangle(color, j * this.cubeSize + (4 * this.cubeSize), i * this.cubeSize + (4 * this.cubeSize), this.cubeSize, this.cubeSize);
                    }
                }   
            };
        }
        public void RunProgram()
        {
            Application.Run(form);
        }
    }
}
