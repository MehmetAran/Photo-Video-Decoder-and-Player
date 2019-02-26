using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

     
        Bitmap[] pictures;
        int sayac = 0;
        int frameSayi=0;
        String dosyaYolu2 = "";
        String dosyaAdi = "";
        
        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("444");
            comboBox1.Items.Add("422");
            comboBox1.Items.Add("420");

            label1.Text = "";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            sayac = 0;
            if (pictures != null)
            {

                timer1.Start();
            }
            else
            {
                MessageBox.Show("Lütfen Değerleri Dogru ve Eksiksiz Giriniz");
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
          
            pictureBox.Image = pictures[sayac];
            label1.Text = "Frame : " + (sayac + 1);
            sayac++;
            if (sayac == frameSayi)
                sayac = 0;
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (pictures != null)
            {
                timer1.Start();
            }
            else
            {
                MessageBox.Show("Lütfen Değerleri Dogru ve Eksiksiz Giriniz");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "YUV Dosyası |*.yuv";
            //open.Title = "Yuv dosyası seciniz.";
            
            if (open.ShowDialog() == DialogResult.OK)
            {
               dosyaAdi = open.SafeFileName;
               dosyaYolu2 = System.IO.Path.GetDirectoryName(open.FileName);
               
               dosyaYolu2 = Path.GetFullPath(dosyaYolu2).Replace(@"\", @"\\");
               dosyaYolu2 = dosyaYolu2+"\\"+dosyaAdi;
                
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            String zaman, yatay, dusey;
            sayac = 0;
            zaman = textBox3.Text;
            yatay = textBox1.Text;
            dusey = textBox2.Text;
            
            String format = comboBox1.Text;
            int width = 0;
            int height=0;
                //Width Height Oynatna Hızı degerleri burda kontrol ediliyor--yanlis islemde mudahale ediliyor
            try {
                 width = int.Parse(yatay);
                 height = int.Parse(dusey);
                timer1.Interval = int.Parse(zaman);
                
            }
            catch(Exception ex )
            {
                MessageBox.Show("Lütfen en-boy-hızı dogru girin");
                return;
             };

            int frameSayisi = 0;
            int pixelAlani = width * height;
            byte[] r = new byte[pixelAlani];
            byte[] g = new byte[pixelAlani];
            byte[] b = new byte[pixelAlani];
            
            Islemler islemler = new Islemler();

            //Dosya secmis mi secmememis mi kontrol ediliyor 
            if (dosyaYolu2.Equals(""))
            {
                MessageBox.Show("Lütfen dosya seçiniz");
                return;
            }
            //Butun dosyaları yuv dosyasından okur ve byte arrayine atar
            byte[] array = islemler.readingFromYuvFile(dosyaYolu2);
            
            if (format.Equals("420"))
            {

                int byteSayisi = (int)(1.5 * width * height);
                frameSayisi = ((array.Length * 2 / 3) / (width * height));
                frameSayi = frameSayisi;
                //width height format degerleri dosyadaki byte sayısına uygun olmalıdır aksi 
                //takdirde hatalı giris yapılmıştır
                //Hatalı giris sonucunda diziden veri taşma sorunu için bu değerler
               // daha başta mod alma işlemi ile kontrol edilir.
                if (array.Length%byteSayisi != 0)
                {
                    MessageBox.Show("Width ve Height değerlerini kontrol ediniz.");
                    return;
                    
                }
                pictures = new Bitmap[frameSayisi];
                RGB rgb = islemler.yuv2RGB(array, width, height, 1.5);
                r = rgb.R;
                g = rgb.G;
                b = rgb.B;

            }
            else if (format.Equals("422"))

            //width height format degerleri dosyadaki byte sayısına uygun olmalıdır aksi 
            //takdirde hatalı giris yapılmıştır
            //Hatalı giris sonucunda diziden veri taşma sorunu için bu değerler
            // daha başta mod alma işlemi ile kontrol edilir.

            {
                int byteSayisi = (int)(2 * width * height);
                frameSayisi = ((array.Length / 2) / (width * height));
                frameSayi = frameSayisi;

                if (array.Length % byteSayisi != 0)
                {
                    MessageBox.Show("Width ve Height değerlerini kontrol ediniz.");
                    return;

                }


                pictures = new Bitmap[frameSayisi];
                RGB rgb = islemler.yuv2RGB(array, width, height,2);
                
                r = rgb.R;
                g = rgb.G;
                b = rgb.B;

            }
            else if (format.Equals("444"))
            {
                frameSayisi = ((array.Length / 3) / (width * height));
                frameSayi = frameSayisi;
                int byteSayisi = (int)(3 * width * height);

                //width height format degerleri dosyadaki byte sayısına uygun olmalıdır aksi 
                //takdirde hatalı giris yapılmıştır
                //Hatalı giris sonucunda diziden veri taşma sorunu için bu değerler
                // daha başta mod alma işlemi ile kontrol edilir.
                
                if (array.Length % byteSayisi != 0)
                {
                    MessageBox.Show("Width ve Height değerlerini kontrol ediniz.");
                    return;

                }

                pictures = new Bitmap[frameSayisi];

                RGB rgb = islemler.yuv2RGB(array, width, height,3);
                r = rgb.R;
                g = rgb.G;
                b = rgb.B;

            }else
            {
                //Bu formatlar dışında herhangi bir veri girildiği takdirde hata mesajı bastırılır ve işlemler durdurulur
                MessageBox.Show("Lutfen formati dogru girin");
                return;
            }
            label1.Text = "Frame : " + (frameSayi);

            for (int i = 0; i < frameSayisi; i++)
            {

                Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                int pixelSayac = 0;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        bmp.SetPixel(x, y, Color.FromArgb(r[pixelSayac + i * pixelAlani], g[pixelSayac + i * pixelAlani], b[pixelSayac + i * pixelAlani]));
                        pixelSayac++;
                    }
  
                }
                //Parse işleminden sonra hemen resimler belirtilen dosya yoluna kaydedilir.
                pictureBox.Image = bmp;
                pictures[i] = bmp;
                bmp.Save("C:\\Users\\warri\\Desktop\\bmpler\\"+dosyaAdi + (i+1) + ".bmp");
            }

        }

        

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
