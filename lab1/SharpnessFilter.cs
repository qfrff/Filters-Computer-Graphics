using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    internal class SharpnessFilter : Filters
    {
        private static readonly int[,] SharpnessMatrix = new int[,] {
            { 0, -1, 0 },
            { -1, 5, -1 },
            { 0, -1, 0 }
        };

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int resultRed = 0, resultGreen = 0, resultBlue = 0;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int neighborX = Clamp(x + j, 0, sourceImage.Width - 1);
                    int neighborY = Clamp(y + i, 0, sourceImage.Height - 1);

                    Color neighborColor = sourceImage.GetPixel(neighborX, neighborY);

                    int sharpnessValue = SharpnessMatrix[i + 1, j + 1];

                    resultRed += sharpnessValue * neighborColor.R;
                    resultGreen += sharpnessValue * neighborColor.G;
                    resultBlue += sharpnessValue * neighborColor.B;
                }
            }

            resultRed = Clamp(resultRed, 0, 255);
            resultGreen = Clamp(resultGreen, 0, 255);
            resultBlue = Clamp(resultBlue, 0, 255);

            return Color.FromArgb(resultRed, resultGreen, resultBlue);
        }
    }
}