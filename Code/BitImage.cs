using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Media.Imaging;

namespace TileMaker.Code
{
    public class BitImage
    {

        private readonly Bitmap img;

        private byte[] data;
        
        private Rectangle rect;

        private IntPtr ptr;
        private IntPtr hptr;
        private BitmapData bitdata;
        private int length;

        private bool locked;

        public BitImage(Bitmap img)
        {
            this.img = img;
            ReadData();
        }

        public BitImage(string path)
        {
            this.img = (Bitmap)Bitmap.FromFile(path);
            ReadData();
        }

        public void ReadData()
        {
            rect = new Rectangle(0, 0, img.Width, img.Height);
            try
            {
                bitdata = img.LockBits(rect, ImageLockMode.ReadWrite, img.PixelFormat);
            }
            catch (ArgumentException e)
            {
                Debug.WriteLine("Error with: " + e.ParamName);
                Debug.WriteLine(e.Message);
                throw;
            }

            int bitDepth = Bitmap.GetPixelFormatSize(bitdata.PixelFormat);

            ptr = bitdata.Scan0;
            hptr = img.GetHbitmap();

            length = bitdata.Width * bitdata.Height * bitDepth / 8;

            data = new byte[length];

            Marshal.Copy(ptr, data, 0, data.Length);

            locked = true;
        }

        public void Change()
        {
            for (int i = 0; i < bitdata.Width; i++)
            {
                for (int j = 0; j < bitdata.Height; j++)
                {
                    int pixel = GetPixel(i, j);
                    SetPixel(i, j, pixel);
                }
            }
        }

        public int GetPixelSize()
        {
            return Bitmap.GetPixelFormatSize(bitdata.PixelFormat) / 8;
        }

        private int PixelPos(int x, int y)
        {
            int pixelSize = GetPixelSize();
            int width = bitdata.Width;
            return (y * width + x) * pixelSize;
        }
        public int GetPixel(int x, int y)
        {

            int pixelSize = GetPixelSize();
            int pixelPos = PixelPos(x, y);

            int pixel = 0;
            for (int i = 0; i < pixelSize; i++)
            {
                pixel = (pixel | data[pixelPos + i] << (i * 8));
            }

            return pixel;
        }

        public void SetPixel(int x, int y, int color)
        {
            int pixelSize = GetPixelSize();
            int pixelPos = PixelPos(x, y);

            for (int i = 0; i < pixelSize; i++)
            {

                data[pixelPos + i] = (byte) (color >> (i * 8));
            }
        }

        private void WriteData()
        {
            Marshal.Copy(data, 0, ptr, length);
            img.UnlockBits(bitdata);
            locked = false;
        }

        public Bitmap LoadBitmap()
        {
            WriteData();
            return Bitmap.FromHbitmap(hptr);
        }

        public BitmapImage LoadBitmapImage()
        {
            WriteData();
            

            BitmapImage bmi = new BitmapImage();
            MemoryStream stream = new MemoryStream();
            img.Save(stream, ImageFormat.Png);

            stream.Seek(0, SeekOrigin.Begin);

            bmi.BeginInit();
            bmi.CacheOption = BitmapCacheOption.OnLoad;
            bmi.StreamSource = stream;
            bmi.EndInit();

            return bmi;
        }




    }
}