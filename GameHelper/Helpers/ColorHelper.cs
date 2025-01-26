using System.Windows.Media;

namespace GameHelper.Helpers
{
    public static class ColorHelper
    {
        public static (double H, double S, double L) RgbToHsl(byte r, byte g, byte b)
        {
            double rNorm = r / 255.0;
            double gNorm = g / 255.0;
            double bNorm = b / 255.0;

            double max = Math.Max(rNorm, Math.Max(gNorm, bNorm));
            double min = Math.Min(rNorm, Math.Min(gNorm, bNorm));
            double delta = max - min;

            double h = 0;
            if (delta != 0)
            {
                if (max == rNorm)
                {
                    h = (gNorm - bNorm) / delta + (gNorm < bNorm ? 6 : 0);
                }
                else if (max == gNorm)
                {
                    h = (bNorm - rNorm) / delta + 2;
                }
                else
                {
                    h = (rNorm - gNorm) / delta + 4;
                }
                h /= 6;
            }

            double l = (max + min) / 2;
            double s = delta == 0 ? 0 : delta / (1 - Math.Abs(2 * l - 1));

            return (h * 360, s, l);
        }

        public static (double H, double S, double L) AdjustHsl((double H, double S, double L) hsl, double deltaL)
        {
            double newL = Math.Max(0, Math.Min(1, hsl.L + deltaL));
            return (hsl.H, hsl.S, newL);
        }

        public static (byte R, byte G, byte B) HslToRgb(double h, double s, double l)
        {
            double c = (1 - Math.Abs(2 * l - 1)) * s;
            double x = c * (1 - Math.Abs((h / 60) % 2 - 1));
            double m = l - c / 2;

            double r = 0, g = 0, b = 0;
            if (h >= 0 && h < 60)
            {
                r = c; g = x; b = 0;
            }
            else if (h >= 60 && h < 120)
            {
                r = x; g = c; b = 0;
            }
            else if (h >= 120 && h < 180)
            {
                r = 0; g = c; b = x;
            }
            else if (h >= 180 && h < 240)
            {
                r = 0; g = x; b = c;
            }
            else if (h >= 240 && h < 300)
            {
                r = x; g = 0; b = c;
            }
            else if (h >= 300 && h < 360)
            {
                r = c; g = 0; b = x;
            }

            return ((byte)((r + m) * 255), (byte)((g + m) * 255), (byte)((b + m) * 255));
        }

        public static Color CreateColorVariant(Color baseColor, double deltaL)
        {
            var hsl = RgbToHsl(baseColor.R, baseColor.G, baseColor.B);
            var adjustedHsl = AdjustHsl(hsl, deltaL);
            var rgb = HslToRgb(adjustedHsl.H, adjustedHsl.S, adjustedHsl.L);
            return Color.FromRgb(rgb.R, rgb.G, rgb.B);
        }
    }
}
