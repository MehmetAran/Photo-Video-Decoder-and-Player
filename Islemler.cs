using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace WindowsFormsApp3
{

    class Islemler
    {
        //Bir dosyadaki butun bytlerı okur ve byte arrayine atar bu arrayi dondurur
        public byte[] readingFromYuvFile(string dosyaYolu)
        {
            byte[] array = File.ReadAllBytes(dosyaYolu);
                return array;
        }
        //YUV formatından alınmış bytelarla oluşturulmuş
        //byte dizisini r g b dizilerine donusturme işlemi uygulanır
        public RGB yuv2RGB(byte[] yuvData, int width, int height,double katSayi)
        {
            int yMiktari = width * height;
            int toplam = yuvData.Length;
            int birFrame =(int) (width * height * katSayi);
            int frameSayisi = toplam / birFrame;
            int rgbElemanSayisi = yMiktari * frameSayisi;
            int Y=0;
            byte[] r = new byte[rgbElemanSayisi];
            byte[] g = new byte[rgbElemanSayisi];
            byte[] b = new byte[rgbElemanSayisi];

            for (int i = 0; i < frameSayisi; i++)
            {
                for (int j = 0; j < yMiktari; j++)
                {
                    Y = yuvData[i * birFrame + j];
                    
                    int R = (byte)(Y );
                    int G = (byte)(Y);
                    int B = (byte)(Y );

                    r[j + i * yMiktari] = (byte)R;
                    g[j + i * yMiktari] = (byte)G;
                    b[j + i * yMiktari] = (byte)B;
                }
            }
            return new RGB(r, g, b);
        }
        
    }
}
