using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    internal class SepiaFilter : Filters
    {

        private const float SepiaIntensity = 20.0f;

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);

            int intensity = (int)(0.299 * sourceColor.R + 0.587 * sourceColor.G + 0.114 * sourceColor.B);

            int newRed = Clamp((int)(intensity + 2 * SepiaIntensity), 0, 255);
            int newGreen = Clamp((int)(intensity + 0.5 * SepiaIntensity), 0, 255);
            int newBlue = Clamp((int)(intensity - SepiaIntensity), 0, 255);

            return Color.FromArgb(newRed, newGreen, newBlue);
        }
    }
}