using lab1;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    internal class EmbossFilter : Filters
    {
        private static readonly int[,] EmbossMatrix = new int[,] {
            { 0, 1, 0 },
            { 1, 0, -1 },
            { 0, -1, 0 }
        };

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            double intensity = 0;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int neighborX = Clamp(x + j, 0, sourceImage.Width - 1);
                    int neighborY = Clamp(y + i, 0, sourceImage.Height - 1);

                    Color neighborColor = sourceImage.GetPixel(neighborX, neighborY);

                    int embossValue = EmbossMatrix[i + 1, j + 1];

                    intensity += embossValue * (neighborColor.R * 0.299 + neighborColor.G * 0.587 + neighborColor.B * 0.114);
                }
            }

            intensity += 255;

            intensity /= 2;

            return Color.FromArgb(Clamp((int)intensity, 0, 255), Clamp((int)intensity, 0, 255), Clamp((int)intensity, 0, 255));
        }
    }
}
