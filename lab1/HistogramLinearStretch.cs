using System;
using System.ComponentModel;
using System.Drawing;

namespace lab1
{
    class AutoLevelsFilter : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);

            // Получаем яркость текущего пикселя
            int brightness = (int)(0.299 * sourceColor.R + 0.587 * sourceColor.G + 0.114 * sourceColor.B);

            // Применяем автоуровни: растягиваем гистограмму
            int newBrightness = (int)(255 * ((double)(brightness - minBrightness)) / (maxBrightness - minBrightness));
            newBrightness = Clamp(newBrightness, 0, 255);

            // Создаем новый цвет с обновленной яркостью
            Color newColor = Color.FromArgb(newBrightness, newBrightness, newBrightness);
            return newColor;
        }

        // Минимальное и максимальное значения яркости
        private int minBrightness = 255;
        private int maxBrightness = 0;

        // Переопределяем метод processImage для поддержки вычисления минимального и максимального значений яркости перед обработкой изображения
        public new Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            CalculateMinMaxBrightness(sourceImage);
            return base.processImage(sourceImage, worker);
        }

        // Вычисляем минимальное и максимальное значения яркости
        private void CalculateMinMaxBrightness(Bitmap sourceImage)
        {
            minBrightness = 255;
            maxBrightness = 0;

            for (int i = 0; i < sourceImage.Width; i++)
            {
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    Color pixel = sourceImage.GetPixel(i, j);
                    int pixelBrightness = (int)(0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B);

                    minBrightness = Math.Min(minBrightness, pixelBrightness);
                    maxBrightness = Math.Max(maxBrightness, pixelBrightness);
                }
            }
        }
    }
}
