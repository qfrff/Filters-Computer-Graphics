using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    internal class MotionBlurFilter : Filters
    {
        private int blurRadius;

        public MotionBlurFilter(int radius)
        {
            blurRadius = radius;
        }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int resultR = 0;
            int resultG = 0;
            int resultB = 0;

            // Проход по всем пикселям в пределах радиуса размытия
            for (int i = -blurRadius; i <= blurRadius; i++)
            {
                int newX = Clamp(x + i, 0, sourceImage.Width - 1);

                // Получение цвета из исходного изображения
                Color sourceColor = sourceImage.GetPixel(newX, y);

                // Суммирование цветов
                resultR += sourceColor.R;
                resultG += sourceColor.G;
                resultB += sourceColor.B;
            }

            // Вычисление среднего значения цветов
            resultR /= (2 * blurRadius + 1);
            resultG /= (2 * blurRadius + 1);
            resultB /= (2 * blurRadius + 1);

            return Color.FromArgb(resultR, resultG, resultB);
        }
    }
}