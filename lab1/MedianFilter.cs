using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    class MedianFilter : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int radius = 1;
            int size = 2 * radius + 1;
            int n = size * size;

            int[] redValues = new int[n];
            int[] greenValues = new int[n];
            int[] blueValues = new int[n];

            for (int i = -radius; i <= radius; i++)
            {
                for (int j = -radius; j <= radius; j++)
                {
                    int idx = (i + radius) * size + (j + radius);
                    Color pixel = sourceImage.GetPixel(Clamp(x + j, 0, sourceImage.Width - 1), Clamp(y + i, 0, sourceImage.Height - 1));
                    redValues[idx] = pixel.R;
                    greenValues[idx] = pixel.G;
                    blueValues[idx] = pixel.B;
                }
            }

            Array.Sort(redValues);
            Array.Sort(greenValues);
            Array.Sort(blueValues);

            int medianRed = redValues[n / 2];
            int medianGreen = greenValues[n / 2];
            int medianBlue = blueValues[n / 2];

            return Color.FromArgb(medianRed, medianGreen, medianBlue);
        }

    }
}