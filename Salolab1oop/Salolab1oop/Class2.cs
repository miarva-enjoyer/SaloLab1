using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salolab1oop
{
    internal class RGB
    {
        public int r;
        public int g;
        public int b;

        public RGB(int red, int grean, int blue)
        {
            if (red > 255)
                red = 255;
            if (red < 0)
                red = 0;

            if (grean > 255)
                grean = 255;
            if (grean < 0)
                grean = 0;

            if (blue > 255)
                blue = 255;
            if (blue < 0)
                blue = 0;

            this.r = red;
            this.g = grean;
            this.b = blue;
        }
    }
}

