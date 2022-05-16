using System;
using System.Drawing;

namespace ASCCII_конвертация
{
    public static class Extensions
    {
        public static void ToGrayScale(this Bitmap bitmap)
        {
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    var pixel = bitmap.GetPixel(x, y);
                    var avg = (pixel.R + pixel.G + pixel.B) / 3; //Усредненное значение от RGB цвета
                    bitmap.SetPixel(x, y, Color.FromArgb(pixel.A, avg, avg, avg)); 
                    //Размещаем новый пиксель с новым цсредненным цветом. 
                    //Первая перегрузка означает прозрачность, ее можно не трогать
                }
            }
        }
    }
}
