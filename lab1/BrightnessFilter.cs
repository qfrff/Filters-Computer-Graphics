using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    internal class BrightnessFilter : Filters
    {
        private const int BrightnessIncrease = 30;

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);

            int newRed = Clamp(sourceColor.R + BrightnessIncrease, 0, 255);
            int newGreen = Clamp(sourceColor.G + BrightnessIncrease, 0, 255);
            int newBlue = Clamp(sourceColor.B + BrightnessIncrease, 0, 255);

            return Color.FromArgb(newRed, newGreen, newBlue);
        }
    }
}