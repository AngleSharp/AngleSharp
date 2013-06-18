using System;
using System.Runtime.InteropServices;

namespace AngleSharp
{
    /// <summary>
    /// Represents a color value.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Pack = 1, CharSet = CharSet.Unicode)]
    struct HtmlColor : IEquatable<HtmlColor>
    {
        //TODO
        //http://en.wikipedia.org/wiki/Alpha_compositing

        #region Members

        [FieldOffset(0)]
        Byte alpha;
        [FieldOffset(1)]
        Byte red;
        [FieldOffset(2)]
        Byte green;
        [FieldOffset(3)]
        Byte blue;
        [FieldOffset(0)]
        Int32 hashcode;

        #endregion

        #region ctor

        /// <summary>
        /// Creates an Html color type without any transparency (alpha = 100%).
        /// </summary>
        /// <param name="r">The red value.</param>
        /// <param name="g">The green value.</param>
        /// <param name="b">The blue value.</param>
        public HtmlColor(Byte r, Byte g, Byte b)
        {
            hashcode = 0;
            alpha = 255;
            red = r;
            blue = b;
            green = g;
        }

        /// <summary>
        /// Creates an Html color type.
        /// </summary>
        /// <param name="a">The alpha value.</param>
        /// <param name="r">The red value.</param>
        /// <param name="g">The green value.</param>
        /// <param name="b">The blue value.</param>
        public HtmlColor(Byte a, Byte r, Byte g, Byte b)
        {
            hashcode = 0;
            alpha = a;
            red = r;
            blue = b;
            green = g;
        }

        /// <summary>
        /// Creates an Html color type.
        /// </summary>
        /// <param name="a">The alpha value between 0 and 1.</param>
        /// <param name="r">The red value.</param>
        /// <param name="g">The green value.</param>
        /// <param name="b">The blue value.</param>
        public HtmlColor(Double a, Byte r, Byte g, Byte b)
        {
            hashcode = 0;
            alpha = (Byte)Math.Max(Math.Min(Math.Ceiling(255 * a), 255), 0);
            red = r;
            blue = b;
            green = g;
        }

        #endregion

        #region Static constructors

        /// <summary>
        /// Returns the color from the given primitives.
        /// </summary>
        /// <param name="r">The value for red.</param>
        /// <param name="g">The value for green.</param>
        /// <param name="b">The value for blue.</param>
        /// <param name="a">The value for alpha.</param>
        /// <returns>The HTML color value.</returns>
        public static HtmlColor FromRgba(Byte r, Byte g, Byte b, Single a)
        {
            return new HtmlColor(a, r, g, b);
        }

        /// <summary>
        /// Returns the color from the given primitives.
        /// </summary>
        /// <param name="r">The value for red.</param>
        /// <param name="g">The value for green.</param>
        /// <param name="b">The value for blue.</param>
        /// <param name="a">The value for alpha.</param>
        /// <returns>The HTML color value.</returns>
        public static HtmlColor FromRgba(Byte r, Byte g, Byte b, Double a)
        {
            return new HtmlColor(a, r, g, b);
        }

        /// <summary>
        /// Returns the color from the given primitives without any alpha (non-transparent).
        /// </summary>
        /// <param name="r">The value for red.</param>
        /// <param name="g">The value for green.</param>
        /// <param name="b">The value for blue.</param>
        /// <returns>The HTML color value.</returns>
        public static HtmlColor FromRgb(Byte r, Byte g, Byte b)
        {
            return new HtmlColor(r, g, b);
        }

        /// <summary>
        /// Returns the color from the given hex string.
        /// </summary>
        /// <param name="color">The hex string like fff or abc123 or AA126B etc.</param>
        /// <returns>The HTML color value.</returns>
        public static HtmlColor FromHex(String color)
        {
            if (color.Length == 3)
            {
                int r = color[0].FromHex();
                r += r * 16;
                int g = color[1].FromHex();
                g += g * 16;
                int b = color[2].FromHex();
                b += b * 16;

                return new HtmlColor((byte)r, (byte)g, (byte)b);
            }
            else if (color.Length == 6)
            {
                int r = 16 * color[0].FromHex();
                int g = 16 * color[2].FromHex();
                int b = 16 * color[4].FromHex();
                r += color[1].FromHex();
                g += color[3].FromHex();
                b += color[5].FromHex();

                return new HtmlColor((byte)r, (byte)g, (byte)b);
            }

            return new HtmlColor();
        }

        /// <summary>
        /// Returns the color from the given hex string if it can be converted, otherwise
        /// the color is not set.
        /// </summary>
        /// <param name="color">The hexadecimal reresentation of the color.</param>
        /// <param name="htmlColor">The color value to be created.</param>
        /// <returns>The status if the string can be converted.</returns>
        public static Boolean TryFromHex(String color, out HtmlColor htmlColor)
        {
            htmlColor = new HtmlColor();
            htmlColor.alpha = 255;

            if (color.Length == 3)
            {
                if (!Specification.IsHex(color[0]) || !Specification.IsHex(color[1]) || !Specification.IsHex(color[2]))
                    return false;

                var r = color[0].FromHex();
                r += r * 16;
                var g = color[1].FromHex();
                g += g * 16;
                var b = color[2].FromHex();
                b += b * 16;

                htmlColor.red = (Byte)r;
                htmlColor.green = (Byte)g;
                htmlColor.blue = (Byte)b;
                return true;
            }
            else if (color.Length == 6)
            {
                if (!Specification.IsHex(color[0]) || !Specification.IsHex(color[1]) || !Specification.IsHex(color[2]) ||
                    !Specification.IsHex(color[3]) || !Specification.IsHex(color[4]) || !Specification.IsHex(color[5]))
                    return false;

                var r = 16 * color[0].FromHex();
                var g = 16 * color[2].FromHex();
                var b = 16 * color[4].FromHex();
                r += color[1].FromHex();
                g += color[3].FromHex();
                b += color[5].FromHex();

                htmlColor.red = (Byte)r;
                htmlColor.green = (Byte)g;
                htmlColor.blue = (Byte)b;
                return true;
            }

            return false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the Int32 value of the color.
        /// </summary>
        public Int32 Value
        {
            get { return hashcode; }
        }

        /// <summary>
        /// Gets the alpha part of the color.
        /// </summary>
        public Byte A
        {
            get { return alpha; }
        }

        /// <summary>
        /// Gets the alpha part of the color in percent (0..1).
        /// </summary>
        public Double Alpha
        {
            get { return alpha / 255.0; }
        }

        /// <summary>
        /// Gets the red part of the color.
        /// </summary>
        public Byte R
        {
            get { return red; }
        }

        /// <summary>
        /// Gets the green part of the color.
        /// </summary>
        public Byte G
        {
            get { return green; }
        }

        /// <summary>
        /// Gets the blue part of the color.
        /// </summary>
        public Byte B
        {
            get { return blue; }
        }

        #endregion

        #region Operators

        public static Boolean operator ==(HtmlColor a, HtmlColor b)
        {
            return a.hashcode == b.hashcode;
        }

        public static Boolean operator !=(HtmlColor a, HtmlColor b)
        {
            return a.hashcode != b.hashcode;
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Tests if another object is equal to this object.
        /// </summary>
        /// <param name="obj">The object to test with.</param>
        /// <returns>True if the two objects are equal, otherwise false.</returns>
        public override Boolean Equals(Object obj)
        {
            if (obj is HtmlColor)
                return this.Equals((HtmlColor)obj);

            return false;
        }

        /// <summary>
        /// Returns a hash code that defines the current color.
        /// </summary>
        /// <returns>The integer value of the hashcode.</returns>
        public override Int32 GetHashCode()
        {
            return hashcode;
        }

        /// <summary>
        /// Returns a string representing the color.
        /// </summary>
        /// <returns>The RGBA string.</returns>
        public override String ToString()
        {
            return String.Format("rgba({0}, {1}, {2}, {3})", red, green, blue, alpha / 255.0);
        }

        #endregion

        #region Implementing Interface

        public Boolean Equals(HtmlColor other)
        {
            return this.hashcode == other.hashcode;
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns a string containing the CSS code.
        /// </summary>
        /// <returns>The string with the rgb or rgba code.</returns>
        public String ToCss()
        {
            if (alpha == 255)
                return "rgb(" + red.ToString() + ", " + green.ToString() + ", " + blue.ToString() + ")";

            return "rgba(" + red.ToString() + ", " + green.ToString() + ", " + blue.ToString() + ", " + Alpha.ToString() + ")";
        }

        /// <summary>
        /// Returns a string containing the HTML (hex) code.
        /// </summary>
        /// <returns>The string with the hex code color.</returns>
        public String ToHtml()
        {
            return "#" + red.ToHex() + green.ToHex() + blue.ToHex();
        }

        #endregion
    }
}
