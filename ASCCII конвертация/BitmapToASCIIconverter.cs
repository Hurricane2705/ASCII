using System;
using System.Drawing;

namespace ASCCII_конвертация
{
    public class BitmapToASCIIconverter
    {
        private readonly char[] ascii_table = { '.', ',', ':', '+', '*', '?', '%', 'S', '#', '@' };
        private readonly Bitmap _bitmap;
        public BitmapToASCIIconverter(Bitmap bitmap)
        {
        _bitmap = bitmap;
        }

        public char[][] Convert()
        {
            var result = new char[_bitmap.Height][];

            for (int y = 0; y < _bitmap.Height; y++)
            {
                result[y] = new char[_bitmap.Width];
                for (int x = 0; x < _bitmap.Width; x++)
                {
                    int index_map = (int)Map(_bitmap.GetPixel(x, y).R, 0, 255, 0, ascii_table.Length - 1);
                    result[y][x] = ascii_table[index_map];
                }
            }
            return result;
        }

        public static float Map(float valueToMap, float start1, float stop1, float start2, float stop2)
        /*Метод, предназначенный чтобы мапить значение одного диапазона в другой диапазон, по сути аналог формулы выведения процента от числа
         Перегрузки метода: 
         1) значение, которое нужно перенести в другой диапазон 
         2 и 3) начало и конец диапазона из которого переносим значение
         4 и 5) начало и конец диапазона в который перенсим значение*/
        {
            return ((valueToMap - start1) / (stop1 - start1)) * (stop2 - start2) + start2;
        }
    }
}
