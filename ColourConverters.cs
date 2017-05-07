using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace SoloProjects.Dudhit.Utilities
{
    public static class ColourConverters
    {
        // Summary:
        // Converts an RGB color to an HSV color.
        //
        // Parameters:
        //   r:
        //     red value 0-255
        //   g:
        //     green value 0-255
        //   b:
        //     blue value 0-255
      public static StandardHSV ConvertRgbToHsv(int ri, int gi, int bi)
        {
            double r, g, b;
            r = ri / 255; g = gi / 255; b = bi / 255;
            double delta, min;
            double h = 0, s, v;

            min = Math.Min(Math.Min(r, g), b);
            v = Math.Max(Math.Max(r, g), b);
            delta = v - min;

            if (v <= 0.00001)
            {
                s = 0;
            }
            else
                s = delta / v;
            if (s == 0)
                h = 0.0;
            else
            {
                if (r == v)
                    h = (g - b) / delta;
                else if (g == v)
                    h = 2 + (b - r) / delta;
                else if (b == v)
                    h = 4 + (r - g) / delta;

                h *= 60;
                if (h < 0.0)
                    h += 360;
            }
                   return new StandardHSV((float)h, (float)s, (float)v);

        }


        // Summary:
        // Converts an HSV color to an RGB color.
        //
        // Parameters:
        //   h:
        //     Hue value 0-360
        //   s:
        //     saturation value 0-100
        //   v:
        //     brightness value 0-100
        public static Color ConvertHsvToRgb(double h, double s, double v)
        {
            double r = 0, g = 0, b = 0;

            if (s == 0)
            {
                r = v;
                g = v;
                b = v;
            }
            else
            {
                int i;
                double f, p, q, t;
                if (h == 360)
                    h = 0;
                else
                    h = h / 60;

                i = (int)Math.Truncate(h);
                f = h - i;

                p = v * (1.0 - s);
                q = v * (1.0 - (s * f));
                t = v * (1.0 - (s * (1.0 - f)));

                switch (i)
                {
                    case 0:
                        r = v;
                        g = t;
                        b = p;
                        break;

                    case 1:
                        r = q;
                        g = v;
                        b = p;
                        break;

                    case 2:
                        r = p;
                        g = v;
                        b = t;
                        break;

                    case 3:
                        r = p;
                        g = q;
                        b = v;
                        break;

                    case 4:
                        r = t;
                        g = p;
                        b = v;
                        break;

                    default:
                        r = v;
                        g = p;
                        b = q;
                        break;
                }
            }
            return Color.FromArgb(255, (byte)(r * 255), (byte)(g * 255), (byte)(b * 255));

        }

        // Summary:
        //provide a hex colour string and the a,r,g,b to return
        //
        // Parameters:
        //   hexString:
        //     9 character string of hex based argb colour values
        //   channel:
        //     first letter of channel to extract
        public static string StripHexWordToHexByte(string hexString, char channel)
        {
            if (!string.IsNullOrWhiteSpace(hexString) && hexString.Length == 9)
            {
                if (char.ToLower(channel) == 'a') { return hexString.Substring(1, 2); }
                if (char.ToLower(channel) == 'r') { return hexString.Substring(3, 2); }
                if (char.ToLower(channel) == 'g') { return hexString.Substring(5, 2); }
                if (char.ToLower(channel) == 'b') { return hexString.Substring(7, 2); }
            }
            return "00";
        }

        // Summary:
        //takes individual strings for ARGB values and join into one hex string 
        //
        // Parameters:
        //   alpha:
        //     alpha value 00-ff
        //   red:
        //     red value  00-ff
        //   green:
        //     green value  00-ff
        //   blue:
        //     blue value  00-ff
        public static string MakeColourHexString(string alpha, string red, string green, string blue)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("#");
            sb.Append(alpha);
            sb.Append(red);
            sb.Append(green);
            sb.Append(blue);

            return sb.ToString();
        }

        //converts a double to hexadecimal string
        public static string ConvertValueToHex(double num)
        {
            int value = Convert.ToInt32(num);
            return value.ToString("X2");
        }

        //pass hex argb colour string to make a brush
        public static Brush MakeBrushFill(string brushString)
        {
            BrushConverter converter = new BrushConverter();
            return (Brush)converter.ConvertFromString(brushString);

        }

        // Summary:
        //pass #ARGB hex string to get a Color 
        public static Color MakeAColourFromString(string hex)
        {
            int a = ConvertHexStringToInt(StripHexWordToHexByte(hex, 'a'));
            int r = ConvertHexStringToInt(StripHexWordToHexByte(hex, 'r'));
            int g = ConvertHexStringToInt(StripHexWordToHexByte(hex, 'g'));
            int b = ConvertHexStringToInt(StripHexWordToHexByte(hex, 'b'));
          return MakeColour ( (byte)a,(byte)r,(byte)g,(byte)b);
        
        }

        // Summary:
        //pass individual ARGB int values to get a Color 
        //
        // Parameters:
        //   a:
        //     alpha value 0-255
        //   r:
        //     red value  0-255
        //   g:
        //     green value  0-255
        //   b:
        //     blue value  0-255
        public static Color MakeColour(int a, int r, int g, int b)
        {
            Color c = new Color();
            if (a >= 0 && a <= 255 && r >= 0 && r <= 255 && g >= 0 && g <= 255 && b >= 0 && b <= 255)
            {
                c.A = (byte)a;
                c.R = (byte)r;
                c.G = (byte)g;
                c.B = (byte)b;
            }
            return c;
        }

        // Summary:
        //pass hex string to get int value 
        //
        // Parameters:
        //   hex: 
        public static int ConvertHexStringToInt(string hex)
        {
            int val;
            return val = IsValidHex(hex) ? Convert.ToInt32(hex, 16) : 0;
        }

        //Summary
        //Turn a Colour into a SolidBrush
        public static SolidColorBrush FillingColour(Color myColour)
        {
            return new SolidColorBrush(myColour);
        }

        // Summary:
        //Check if string is a valid hexadecimal
        //
        // Parameters:
        //   hexval:
        public static bool IsValidHex(string hexVal)
        {
            IEnumerable<char> chars = hexVal.ToCharArray();
            bool isHex;
            foreach (var c in chars)
            {
                isHex = ((c >= '0' && c <= '9') || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F'));

                if (!isHex)
                    return false;
            }
            return true;
        }

        // Summary:
        //Converts Space engineers HSV ingame colour picker values to blue print equivalent
        //
        // Parameters:
        //   h:
        //   s:
        //   v:
        public static BlueprintHSV ConvertSEFormatHSVtoBluePrintFormat(float h, float s, float v)
        {
          return new BlueprintHSV(h / 360, s / 100, v / 100);
        }

        // Summary:
        //Converts 0-100 S V values to Space engineers ingame colour picker -100 to 100 S V 
        //
        // Parameters:
        //   h:
        //   s:
        //   v:
 
        public static SeHSV ConvertStandardHSVtoSEFormat(float h, float s, float v)
        {
          // return new Point3D(h * 360, (s / 100) * 200 - 100, (v / 100) * 200 - 100);
          return new SeHSV(h, (s / 100) * 200 - 100, (v / 100) * 200 - 100);
        }
        /// Summary:
        //Converts Space engineers ingame colour picker -100 to 100 S V to standard 0-100 values
        //
        // Parameters:
        //   h:
        //   s:
        //   v:
        public static StandardHSV ConvertSEFormatHSVToStandard(float h, float s, float v)
        {

          return new StandardHSV(h, ((s + 100) / 200) * 100, ((v + 100) / 200) * 100);
        }


    }

}
