using System;

namespace AngleSharp
{
    /// <summary>
    /// Represents a color value.
    /// </summary>
    struct HtmlColor : IEquatable<HtmlColor>
    {
        //TODO
        //http://en.wikipedia.org/wiki/Alpha_compositing

        #region Members

        byte alpha;
        byte red;
        byte green;
        byte blue;

        #endregion

        #region ctor

        /// <summary>
        /// Creates an Html color type without any transparency (alpha = 100%).
        /// </summary>
        /// <param name="r">The red value.</param>
        /// <param name="g">The green value.</param>
        /// <param name="b">The blue value.</param>
        public HtmlColor(byte r, byte g, byte b)
        {
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
        public HtmlColor(byte a, byte r, byte g, byte b)
        {
            alpha = a;
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
        public static HtmlColor FromRgba(byte r, byte g, byte b, float a)
        {
            return new HtmlColor((byte)Math.Floor(255 * a), r, g, b);
        }

        /// <summary>
        /// Returns the color from the given primitives without any alpha (non-transparent).
        /// </summary>
        /// <param name="r">The value for red.</param>
        /// <param name="g">The value for green.</param>
        /// <param name="b">The value for blue.</param>
        /// <returns>The HTML color value.</returns>
        public static HtmlColor FromRgb(byte r, byte g, byte b)
        {
            return new HtmlColor(r, g, b);
        }

        /// <summary>
        /// Returns the color from the given hex string.
        /// </summary>
        /// <param name="color">The hex string like fff or abc123 or AA126B etc.</param>
        /// <returns>The HTML color value.</returns>
        public static HtmlColor FromHex(string color)
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
        public static bool TryFromHex(string color, out HtmlColor htmlColor)
        {
            htmlColor = new HtmlColor();
            htmlColor.alpha = 255;

            if (color.Length == 3)
            {
                if (!Specification.IsHex(color[0]) || !Specification.IsHex(color[1]) || !Specification.IsHex(color[2]))
                    return false;

                int r = color[0].FromHex();
                r += r * 16;
                int g = color[1].FromHex();
                g += g * 16;
                int b = color[2].FromHex();
                b += b * 16;

                htmlColor.red = (byte)r;
                htmlColor.green = (byte)g;
                htmlColor.blue = (byte)b;
                return true;
            }
            else if (color.Length == 6)
            {
                if (!Specification.IsHex(color[0]) || !Specification.IsHex(color[1]) || !Specification.IsHex(color[2]) ||
                    !Specification.IsHex(color[3]) || !Specification.IsHex(color[4]) || !Specification.IsHex(color[5]))
                    return false;

                int r = 16 * color[0].FromHex();
                int g = 16 * color[2].FromHex();
                int b = 16 * color[4].FromHex();
                r += color[1].FromHex();
                g += color[3].FromHex();
                b += color[5].FromHex();

                htmlColor.red = (byte)r;
                htmlColor.green = (byte)g;
                htmlColor.blue = (byte)b;
                return true;
            }

            return false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the alpha part of the color.
        /// </summary>
        public byte A
        {
            get { return alpha; }
        }

        /// <summary>
        /// Gets the red part of the color.
        /// </summary>
        public byte R
        {
            get { return red; }
        }

        /// <summary>
        /// Gets the green part of the color.
        /// </summary>
        public byte G
        {
            get { return green; }
        }

        /// <summary>
        /// Gets the blue part of the color.
        /// </summary>
        public byte B
        {
            get { return blue; }
        }

        #endregion

        #region Operators

        public static bool operator ==(HtmlColor a, HtmlColor b)
        {
            return a.alpha == b.alpha && a.blue == b.blue && a.green == b.green && a.red == b.red;
        }

        public static bool operator !=(HtmlColor a, HtmlColor b)
        {
            return a.alpha != b.alpha || a.blue != b.blue || a.green != b.green || a.red != b.red;
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Tests if another object is equal to this object.
        /// </summary>
        /// <param name="obj">The object to test with.</param>
        /// <returns>True if the two objects are equal, otherwise false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is HtmlColor)
                return this == (HtmlColor)obj;

            return false;
        }

        /// <summary>
        /// Returns a hash code that defines the current color.
        /// </summary>
        /// <returns>The integer value of the hashcode.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Returns a string representing the color.
        /// </summary>
        /// <returns>The RGBA string.</returns>
        public override string ToString()
        {
            return string.Format("rgba({0}, {1}, {2}, {3})", red, green, blue, alpha / 255.0);
        }

        #endregion

        #region Implementing Interface

        public bool Equals(HtmlColor other)
        {
            return this == other;
        }

        #endregion
    }
}
