using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    internal class GlassEffectFilter : Filters
    {
        private Random random;

        public GlassEffectFilter()
        {
            random = new Random();
        }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int xOffset = (int)((random.NextDouble() - 0.5) * 10);
            int yOffset = (int)((random.NextDouble() - 0.5) * 10);

            int newX = Clamp(x + xOffset, 0, sourceImage.Width - 1);
            int newY = Clamp(y + yOffset, 0, sourceImage.Height - 1);


            Color newColor = sourceImage.GetPixel(newX, newY);

            return newColor;
        }
    }
}