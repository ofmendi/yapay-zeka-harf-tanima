using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HarfTanıma
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private Image Crop(Image img, Rectangle rect)
        {
            Bitmap bmpOrj = new Bitmap(img);
            Bitmap bmpCrop = bmpOrj.Clone(rect, bmpOrj.PixelFormat);
            return (Image)(bmpCrop);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            veri = "";
            openFileDialog1.Filter = "Resim Dosyaları |" + "*.bmp;*.jpg;*.gif;*.png;";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
            }
        }
        byte renk;
        int toplam = 0;
        string veri = "";
        private void button2_Click(object sender, EventArgs e)
        {
            veri = "";
            int[,] harf = new int[8, 6];
            Bitmap resim = new Bitmap(pictureBox1.Image);
            int kare = 40;
            int dikeyParca = resim.Height / kare;
            int yatayParca = resim.Width / kare;
            Rectangle cropAlani = new Rectangle(0, 0, kare, kare);
            for (int i = 0; i < dikeyParca; i++)
            {
                for (int j = 0; j < yatayParca; j++)
                {
                    cropAlani.Y = i * kare;
                    cropAlani.X = j * kare;

                    Image parcaResim = Crop(resim, cropAlani);
                    //parcaResim.Save(Application.StartupPath + @"\" + i + "x" + j + ".jpg", ImageFormat.Jpeg);
                    Bitmap re = new Bitmap(parcaResim);
                    for (int y = 0; y < re.Height; y++)
                    {
                        for (int x = 0; x < re.Width; x++)
                        {
                            renk = re.GetPixel(x, y).R;
                            toplam += renk;
                            Application.DoEvents();
                        }
                    }
                    toplam = toplam / (re.Width*re.Height);
                    if (toplam>200)//175
                    {
                        harf[i, j] = 0;
                    }
                    else
                    {
                        harf[i, j] = 1;
                    }
                    //MessageBox.Show(toplam.ToString());
                    
                }
            }
            for (int i = 0; i < harf.GetLength(0); i++)
            {
                for (int j = 0; j < harf.GetLength(1); j++)
                {
                    veri += harf[i, j].ToString()+" ";
                }
                veri += "\n";
            }
            label1.Text = veri;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

    }
}
