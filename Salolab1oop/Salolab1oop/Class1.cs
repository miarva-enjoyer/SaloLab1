using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salolab1oop
{
    internal class Saloclass1
    {
        private int hue;
        private int saturation;
        private int lightness;

        public Saloclass1(int hue, int saturation, int lightness)
        {
            if (hue > 255)
                hue = 255;
            if (hue < 0)
                hue = 0;

            if (saturation > 100)
                saturation = 100;
            if (saturation < 0)
                saturation = 0;

            if (lightness > 100)
                lightness = 100;
            if (lightness < 0)
                lightness = 0;

            this.hue = hue;
            this.saturation = saturation;
            this.lightness = lightness;
        }

        public int Gethue()
        {
            return hue;
        }
        public int Getsaturation()
        {
            return saturation;
        }
        public int Getlightness()
        {
            return lightness;
        }

        public static Saloclass1 operator +(Saloclass1 a, Saloclass1 b)
        {
            Saloclass1 salo = new Saloclass1(a.hue + b.hue, a.saturation + b.saturation, a.lightness + b.lightness);
            return salo;
        }

        public static Saloclass1 operator -(Saloclass1 a, Saloclass1 b)
        {
            Saloclass1 salo = new Saloclass1(a.hue - b.hue, a.saturation - b.saturation, a.lightness - b.lightness);
            return salo;
        }

        public static Saloclass1 operator *(Saloclass1 a, Saloclass1 b)
        {
            Saloclass1 salo = new Saloclass1(a.hue * b.hue, a.saturation * b.saturation, a.lightness * b.lightness);
            return salo;
        }

        public static Saloclass1 operator *(Saloclass1 a, int b)
        {
            Saloclass1 salo = new Saloclass1(a.hue * b, a.saturation * b, a.lightness * b);
            return salo;
        }

        public static Saloclass1 operator /(Saloclass1 a, int b)
        {
            Saloclass1 salo = new Saloclass1(a.hue / b, a.saturation / b, a.lightness / b);
            return salo;
        }

        public static int operator ==(Saloclass1 a, Saloclass1 b)
        {
            if (a.hue == b.hue && a.saturation == b.saturation && a.lightness == b.lightness)
                return 1;
            else
                return 0;
        }
        public static int operator !=(Saloclass1 a, Saloclass1 b)
        {
            if (a.hue == b.hue && a.saturation == b.saturation && a.lightness == b.lightness)
                return 0;
            else
                return 1;
        }
        public override string ToString()
        {
            string result = "HSL(" + hue + "," + saturation + "%," + lightness + "%)";
            return result;
        }

        public RGB convertertToRGB()
        {
            int c;
            int m;
            int x;

            RGB result;

            c = (1 - Math.Abs(2 * lightness - 1)) * saturation;
            m = 1 * (lightness - c / 2);
            x = c * (1 - Math.Abs((hue / 60) % 2 - 1));
if (hue <= 60) { result = new RGB(c + m, x + m, m); }
            if ((hue > 60) && (hue <= 120)) { result = new RGB(x + m, c + m, m); }
            if ((hue > 120) && (hue <= 180)) { result = new RGB(m, c + m, x + m); }
            if ((hue > 180) && (hue <= 240)) { result = new RGB(x + m, c + m, m); }

            result = new RGB(0, 0, 0);
            return result;
        }
    }
}