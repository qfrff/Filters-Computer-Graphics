using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    internal class SobelFilter : Filters
    {
        // Оператор Собеля по оси Y
        private static readonly int[,] SobelY = new int[,] {
            { -1, -2, -1 },
            { 0, 0, 0 },
            { 1, 2, 1 }
        };

        // Оператор Собеля по оси X
        private static readonly int[,] SobelX = new int[,] {
            { -1, 0, 1 },
            { -2, 0, 2 },
            { -1, 0, 1 }
        };

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int resultXRed = 0, resultXGreen = 0, resultXBlue = 0;
            int resultYRed = 0, resultYGreen = 0, resultYBlue = 0;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int neighborX = Clamp(x + j, 0, sourceImage.Width - 1);
                    int neighborY = Clamp(y + i, 0, sourceImage.Height - 1);

                    Color neighborColor = sourceImage.GetPixel(neighborX, neighborY);

                    int sobelX = SobelX[i + 1, j + 1];
                    int sobelY = SobelY[i + 1, j + 1];

                    resultXRed += sobelX * neighborColor.R;
                    resultXGreen += sobelX * neighborColor.G;
                    resultXBlue += sobelX * neighborColor.B;

                    resultYRed += sobelY * neighborColor.R;
                    resultYGreen += sobelY * neighborColor.G;
                    resultYBlue += sobelY * neighborColor.B;
                }
            }

            // Общее значение градиента
            int resultRed = Clamp((int)Math.Sqrt(resultXRed * resultXRed + resultYRed * resultYRed), 0, 255);
            int resultGreen = Clamp((int)Math.Sqrt(resultXGreen * resultXGreen + resultYGreen * resultYGreen), 0, 255);
            int resultBlue = Clamp((int)Math.Sqrt(resultXBlue * resultXBlue + resultYBlue * resultYBlue), 0, 255);

            return Color.FromArgb(resultRed, resultGreen, resultBlue);
        }
    }
}