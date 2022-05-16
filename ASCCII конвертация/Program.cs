using System;
using System.Windows.Forms; //Сначала нужно подключить в обозревателе решений ссылку на эту библиотеку, после чего подключить ее уже тут, введя using
using System.Drawing; //Следом так же понадобится подключить и эту библиотеку
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace ASCCII_конвертация
{
    class Program
    {
        private const int maxH = 160;
        private const int maxW = 630;
        public static char[] ascii_table = { ' ', '.', ',', ':', '!', '*', 'O', '#', '%', '$', '@' };

        [STAThread] //Без этого вызов OpenDialog вызовет ошибку

        static void Main(string[] args)
        {
            Console.SetWindowPosition(0, 0);
            
            #region openFileDialog
            var fileDialog = new OpenFileDialog
            {
                Filter = "Images | *.bmp; *.png; *.jpg; *.JPEG"
                //Filter = "GIF Image | *.gif"
            };
            #endregion

            Console.WriteLine("Press Enter to start...\n");

            #region gif version
            //while (true)
            //{
            //    Console.ReadLine();
            //    if (fileDialog.ShowDialog() != DialogResult.OK)
            //        continue;

            //    Console.Clear();

            //    Image image = Image.FromFile(fileDialog.FileName);
            //    var gif = GetFrames(image);
            //    var bitmap_gif = new List<char[][]>();

            //    foreach (var item in gif)
            //    {
            //        var bitmap = new Bitmap(item);
            //        bitmap = ResizeBitmap(bitmap);
            //        bitmap.ToGrayScale();
            //        var converter = new BitmapToASCIIconverter(bitmap);
            //        var converted_bitmap = converter.Convert();
            //        bitmap_gif.Add(converted_bitmap);
            //    }
            //    while (true)
            //    {
            //        foreach (var giff in bitmap_gif)
            //        {
            //            //Console.Clear();
            //            Console.SetCursorPosition(0, 0);
            //            foreach (var item in giff)
            //                Console.WriteLine(item);
            //            Thread.Sleep(3);
            //        }
            //    }
            //}
            #endregion

            #region picture version 
            while (true)
            {
                Console.ReadLine();
                if (fileDialog.ShowDialog() != DialogResult.OK)
                    continue;

                Console.Clear();

                var bitmap = new Bitmap(fileDialog.FileName);
                bitmap = ResizeBitmap(bitmap);
                bitmap.ToGrayScale();

                var converter = new BitmapToASCIIconverter(bitmap);
                var converted_bitmap = converter.Convert();

                foreach (var item in converted_bitmap)
                    Console.WriteLine(item);

                Console.SetCursorPosition(0, 0); //Размещение курсора в начале, чтобы консоль не прокрутилась вниз

                //File.WriteAllLines("image.txt", converted_bitmap.Select(r => new string(r))); 
                /*Сохранение в файл происходит как - бы в негативе из - за другого фона нужно изменить метод Convert
                 чтобы он принимал в качестве параметра обратный набор символов. , ; @ и пр, тем самым меняя картинку на противоположный "цвет"*/
            }
            #endregion
        }

        private static Bitmap ResizeBitmap(Bitmap bitmap)
        {
            //var newHeight = bitmap.Height / WIDTH_OFFSET * maxWidth / bitmap.Width;
            //if (bitmap.Width > maxWidth  || bitmap.Height > newHeight)
            //    bitmap = new Bitmap(bitmap, new Size(maxWidth, (int)newHeight));
            float ratio = (float)bitmap.Height / bitmap.Width;
            if (bitmap.Width > maxW)// || bitmap.Height > maxH)
                bitmap = new Bitmap(bitmap, new Size(maxW, (int)(maxW * ratio / 2)));
            if (bitmap.Height > maxH)
                bitmap = new Bitmap(bitmap, new Size((int)(maxH / ratio * 2), maxH));
            return bitmap;

        }

        private static Image[] GetFrames(Image img)
        {
            List<Image> imgs = new List<Image>();
            int length = img.GetFrameCount(FrameDimension.Time);
            for (int i = 0; i < length; i++)
            {
                img.SelectActiveFrame(FrameDimension.Time, i);
                imgs.Add(new Bitmap(img));
            }
            return imgs.ToArray();
        }
    }
}
