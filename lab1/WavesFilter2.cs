using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    internal class WavesFilter2 : Filters
    {
        private int amplitude; // Амплитуда волны

        // Конструктор, принимающий амплитуду в качестве параметра
        public WavesFilter2(int amplitude)
        {
            this.amplitude = amplitude;
        }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            // Вычисляем новые координаты с использованием волновой функции
            int newX = x;
            int newY = (int)(y + amplitude * Math.Sin(2 * Math.PI * x / 60));

            // Проверяем, что новые координаты находятся в пределах изображения
            if (newX >= 0 && newX < sourceImage.Width && newY >= 0 && newY < sourceImage.Height)
            {
                // Берем цвет из исходного изображения для новых координат
                Color newColor = sourceImage.GetPixel(newX, newY);
                return newColor;
            }
            else
            {
                // Возвращаем черный цвет для пикселей, которые выходят за границы изображения
                return Color.Black;
            }
        }
    }
}
