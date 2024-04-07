using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    internal class RotateFilter : Filters
    {
        private double angle; // угол поворота в радианах
        private int centerX; // координата центра по оси X
        private int centerY; // координата центра по оси Y

        public RotateFilter(double angle, int centerX, int centerY)
        {
            this.angle = angle;
            this.centerX = centerX;
            this.centerY = centerY;
        }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {

            int newX = (int)((x - centerX) * Math.Cos(angle) - (y - centerY) * Math.Sin(angle) + centerX);
            int newY = (int)((x - centerX) * Math.Sin(angle) + (y - centerY) * Math.Cos(angle) + centerY);

            if (newX >= 0 && newX < sourceImage.Width && newY >= 0 && newY < sourceImage.Height)
            {
                return sourceImage.GetPixel(newX, newY);
            }
            else
            {
                return Color.Black;
            }
        }
    }
}