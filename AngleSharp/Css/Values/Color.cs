namespace AngleSharp.Css.Values
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Represents a color value.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Pack = 1, CharSet = CharSet.Unicode)]
    public struct Color : IEquatable<Color>, IComparable<Color>, IFormattable
    {
        #region Basic colors

        /// <summary>
        /// The color #000000.
        /// </summary>
        public static readonly Color Black = new Color(0, 0, 0);

        /// <summary>
        /// The color #FFFFFF.
        /// </summary>
        public static readonly Color White = new Color(255, 255, 255);

        /// <summary>
        /// The color #FF0000.
        /// </summary>
        public static readonly Color Red = new Color(255, 0, 0);

        /// <summary>
        /// The color #FF00FF.
        /// </summary>
        public static readonly Color Magenta = new Color(255, 0, 255);

        /// <summary>
        /// The color #008000.
        /// </summary>
        public static readonly Color Green = new Color(0, 128, 0);

        /// <summary>
        /// The color #00FF00.
        /// </summary>
        public static readonly Color PureGreen = new Color(0, 255, 0);

        /// <summary>
        /// The color #0000FF.
        /// </summary>
        public static readonly Color Blue = new Color(0, 0, 255);

        /// <summary>
        /// The color #00000000.
        /// </summary>
        public static readonly Color Transparent = new Color(0, 0, 0, 0);

        #endregion

        #region Fields

        [FieldOffset(0)]
        readonly Byte _alpha;
        [FieldOffset(1)]
        readonly Byte _red;
        [FieldOffset(2)]
        readonly Byte _green;
        [FieldOffset(3)]
        readonly Byte _blue;
        [FieldOffset(0)]
        readonly Int32 _hashcode;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a CSS color type without any transparency (alpha = 100%).
        /// </summary>
        /// <param name="r">The red value.</param>
        /// <param name="g">The green value.</param>
        /// <param name="b">The blue value.</param>
        public Color(Byte r, Byte g, Byte b)
        {
            _hashcode = 0;
            _alpha = 255;
            _red = r;
            _blue = b;
            _green = g;
        }

        /// <summary>
        /// Creates a CSS color type.
        /// </summary>
        /// <param name="r">The red value.</param>
        /// <param name="g">The green value.</param>
        /// <param name="b">The blue value.</param>
        /// <param name="a">The alpha value.</param>
        public Color(Byte r, Byte g, Byte b, Byte a)
        {
            _hashcode = 0;
            _alpha = a;
            _red = r;
            _blue = b;
            _green = g;
        }

        #endregion

        #region Static constructors

        /// <summary>
        /// Returns the color from the given primitives.
        /// </summary>
        /// <param name="r">The value for red [0,255].</param>
        /// <param name="g">The value for green [0,255].</param>
        /// <param name="b">The value for blue [0,255].</param>
        /// <param name="a">The value for alpha [0,1].</param>
        /// <returns>The CSS color value.</returns>
        public static Color FromRgba(Byte r, Byte g, Byte b, Single a)
        {
            return new Color(r, g, b, Normalize(a));
        }

        /// <summary>
        /// Returns the color from the given primitives.
        /// </summary>
        /// <param name="r">The value for red [0,1].</param>
        /// <param name="g">The value for green [0,1].</param>
        /// <param name="b">The value for blue [0,1].</param>
        /// <param name="a">The value for alpha [0,1].</param>
        /// <returns>The CSS color value.</returns>
        public static Color FromRgba(Single r, Single g, Single b, Single a)
        {
            return new Color(Normalize(r), Normalize(g), Normalize(b), Normalize(a));
        }

        /// <summary>
        /// Returns the gray color from the given value.
        /// </summary>
        /// <param name="number">The value for each component [0,255].</param>
        /// <param name="alpha">The value for alpha [0,1].</param>
        /// <returns>The CSS color value.</returns>
        public static Color FromGray(Byte number, Single alpha = 1f)
        {
            return new Color(number, number, number, Normalize(alpha));
        }

        /// <summary>
        /// Returns the gray color from the given value.
        /// </summary>
        /// <param name="value">The value for each component [0,1].</param>
        /// <param name="alpha">The value for alpha [0,1].</param>
        /// <returns>The CSS color value.</returns>
        public static Color FromGray(Single value, Single alpha = 1f)
        {
            return FromGray(Normalize(value), alpha);
        }

        /// <summary>
        /// Returns the color with the given name.
        /// </summary>
        /// <param name="name">The name of the color.</param>
        /// <returns>The CSS color value.</returns>
        public static Color? FromName(String name)
        {
            return Colors.GetColor(name);
        }

        /// <summary>
        /// Returns the color from the given primitives without any alpha.
        /// </summary>
        /// <param name="r">The value for red [0,255].</param>
        /// <param name="g">The value for green [0,255].</param>
        /// <param name="b">The value for blue [0,255].</param>
        /// <returns>The CSS color value.</returns>
        public static Color FromRgb(Byte r, Byte g, Byte b)
        {
            return new Color(r, g, b);
        }

        /// <summary>
        /// Returns the color from the given hex string.
        /// </summary>
        /// <param name="color">The hex string like fff or abc123 or AA126B etc.</param>
        /// <returns>The CSS color value.</returns>
        public static Color FromHex(String color)
        {
            int r = 0, g = 0, b = 0, a = 255;

            switch (color.Length)
            {
                case 4:
                    a = 17 * color[3].FromHex();
                    goto case 3;
                case 3:
                    r = 17 * color[0].FromHex();
                    g = 17 * color[1].FromHex();
                    b = 17 * color[2].FromHex();
                    break;
                case 8:
                    a = 16 * color[6].FromHex() + color[7].FromHex();
                    goto case 6;
                case 6:
                    r = 16 * color[0].FromHex() + color[1].FromHex();
                    g = 16 * color[2].FromHex() + color[3].FromHex();
                    b = 16 * color[4].FromHex() + color[5].FromHex();
                    break;
            }

            return new Color((Byte)r, (Byte)g, (Byte)b, (Byte)a);
        }

        /// <summary>
        /// Returns the color from the given hex string if it can be converted, otherwise
        /// the color is not set.
        /// </summary>
        /// <param name="color">The hexadecimal reresentation of the color.</param>
        /// <param name="value">The color value to be created.</param>
        /// <returns>The status if the string can be converted.</returns>
        public static Boolean TryFromHex(String color, out Color value)
        {
            if (color.Length == 6 || color.Length == 3 || color.Length == 8 || color.Length == 4)
            {
                for (int i = 0; i < color.Length; i++)
                {
                    if (!color[i].IsHex())
                        goto fail;
                }

                value = FromHex(color);
                return true;
            }

            fail:
            value = new Color();
            return false;
        }

        /// <summary>
        /// Returns the color of non-CSS colors in a special IE notation known
        /// as "flex hex". Computes the part without the hash and possible color
        /// names. More information can be found at:
        /// http://scrappy-do.blogspot.de/2004/08/little-rant-about-microsoft-internet.html
        /// </summary>
        /// <param name="color">The color string to evaluate.</param>
        /// <returns>The color for the color string.</returns>
        public static Color FromFlexHex(String color)
        {
            var length = Math.Max(color.Length, 3);
            var remaining = length % 3;

            if (remaining != 0)
                length += 3 - remaining;

            var n = length / 3;
            var d = Math.Min(2, n);
            var s = Math.Max(n - 8, 0);
            var chars = new Char[length];

            for (int i = 0; i < color.Length; i++)
                chars[i] = color[i].IsHex() ? color[i] : '0';

            for (int i = color.Length; i < length; i++)
                chars[i] = '0';

            if (d == 1)
            {
                var r = chars[0 * n + s].FromHex();
                var g = chars[1 * n + s].FromHex();
                var b = chars[2 * n + s].FromHex();
                return new Color((Byte)r, (Byte)g, (Byte)b);
            }
            else
            {
                var r = 16 * chars[0 * n + s].FromHex() + chars[0 * n + s + 1].FromHex();
                var g = 16 * chars[1 * n + s].FromHex() + chars[1 * n + s + 1].FromHex();
                var b = 16 * chars[2 * n + s].FromHex() + chars[2 * n + s + 1].FromHex();
                return new Color((Byte)r, (Byte)g, (Byte)b);
            }
        }

        /// <summary>
        /// Returns the color that represents the given HSL values.
        /// </summary>
        /// <param name="h">The color angle [0,1].</param>
        /// <param name="s">The saturation [0,1].</param>
        /// <param name="l">The light value [0,1].</param>
        /// <returns>The CSS color.</returns>
        public static Color FromHsl(Single h, Single s, Single l)
        {
            return FromHsla(h, s, l, 1f);
        }

        /// <summary>
        /// Returns the color that represents the given HSL values.
        /// </summary>
        /// <param name="h">The color angle [0,1].</param>
        /// <param name="s">The saturation [0,1].</param>
        /// <param name="l">The light value [0,1].</param>
        /// <param name="alpha">The alpha value [0,1].</param>
        /// <returns>The CSS color.</returns>
        public static Color FromHsla(Single h, Single s, Single l, Single alpha)
        {
            const Single third = 1f / 3f;

            var m2 = l <= 0.5f ? (l * (s + 1f)) : (l + s - l * s);
            var m1 = 2f * l - m2;
            var r = Convert(HueToRgb(m1, m2, h + third));
            var g = Convert(HueToRgb(m1, m2, h));
            var b = Convert(HueToRgb(m1, m2, h - third));
            return new Color(r, g, b, Normalize(alpha));
        }

        /// <summary>
        /// Returns the color that represents Hue-Whiteness-Blackness.
        /// </summary>
        /// <param name="h">The color angle [0,1].</param>
        /// <param name="w">The whiteness [0,1].</param>
        /// <param name="b">The blackness [0,1].</param>
        /// <returns>The CSS color.</returns>
        public static Color FromHwb(Single h, Single w, Single b)
        {
            return FromHwba(h, w, b, 1f);
        }

        /// <summary>
        /// Returns the color that represents Hue-Whiteness-Blackness.
        /// </summary>
        /// <param name="h">The color angle [0,1].</param>
        /// <param name="w">The whiteness [0,1].</param>
        /// <param name="b">The blackness [0,1].</param>
        /// <param name="alpha">The alpha value [0,1].</param>
        /// <returns>The CSS color.</returns>
        public static Color FromHwba(Single h, Single w, Single b, Single alpha)
        {
            var ratio = 1f / (w + b);
            var red = 0f;
            var green = 0f;
            var blue = 0f;

            if (ratio < 1f) 
            {
                w *= ratio;
                b *= ratio;
            }

            var p = (Int32)(6 * h);
            var f = 6 * h - p;

            if ((p & 0x01) != 0)
                f = 1 - f;

            var v = 1 - b;
            var n = w + f * (v - w);

            switch (p) 
            {
                default:
                case 6:
                case 0: red = v; green = n; blue = w; break;
                case 1: red = n; green = v; blue = w; break;
                case 2: red = w; green = v; blue = n; break;
                case 3: red = w; green = n; blue = v; break;
                case 4: red = n; green = w; blue = v; break;
                case 5: red = v; green = w; blue = n; break;
            }

            return FromRgba(red, green, blue, alpha);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the Int32 value of the color.
        /// </summary>
        public Int32 Value
        {
            get { return _hashcode; }
        }

        /// <summary>
        /// Gets the alpha part of the color.
        /// </summary>
        public Byte A
        {
            get { return _alpha; }
        }

        /// <summary>
        /// Gets the alpha part of the color in percent (0..1).
        /// </summary>
        public Double Alpha
        {
            get { return _alpha / 255.0; }
        }

        /// <summary>
        /// Gets the red part of the color.
        /// </summary>
        public Byte R
        {
            get { return _red; }
        }

        /// <summary>
        /// Gets the green part of the color.
        /// </summary>
        public Byte G
        {
            get { return _green; }
        }

        /// <summary>
        /// Gets the blue part of the color.
        /// </summary>
        public Byte B
        {
            get { return _blue; }
        }

        #endregion

        #region Equality

        /// <summary>
        /// Compares two colors and returns a boolean indicating if the two do match.
        /// </summary>
        /// <param name="a">The first color to use.</param>
        /// <param name="b">The second color to use.</param>
        /// <returns>True if both colors are equal, otherwise false.</returns>
        public static Boolean operator ==(Color a, Color b)
        {
            return a._hashcode == b._hashcode;
        }

        /// <summary>
        /// Compares two colors and returns a boolean indicating if the two do not match.
        /// </summary>
        /// <param name="a">The first color to use.</param>
        /// <param name="b">The second color to use.</param>
        /// <returns>True if both colors are not equal, otherwise false.</returns>
        public static Boolean operator !=(Color a, Color b)
        {
            return a._hashcode != b._hashcode;
        }

        /// <summary>
        /// Checks two colors for equality.
        /// </summary>
        /// <param name="other">The other color.</param>
        /// <returns>True if both colors or equal, otherwise false.</returns>
        public Boolean Equals(Color other)
        {
            return this._hashcode == other._hashcode;
        }

        /// <summary>
        /// Tests if another object is equal to this object.
        /// </summary>
        /// <param name="obj">The object to test with.</param>
        /// <returns>True if the two objects are equal, otherwise false.</returns>
        public override Boolean Equals(Object obj)
        {
            var other = obj as Color?;

            if (other != null)
            {
                return Equals(other.Value);
            }

            return false;
        }

        Int32 IComparable<Color>.CompareTo(Color other)
        {
            return _hashcode - other._hashcode;
        }

        /// <summary>
        /// Returns a hash code that defines the current color.
        /// </summary>
        /// <returns>The integer value of the hashcode.</returns>
        public override Int32 GetHashCode()
        {
            return _hashcode;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Mixes two colors using alpha compositing as described here:
        /// http://en.wikipedia.org/wiki/Alpha_compositing
        /// </summary>
        /// <param name="above">The first color (above) with transparency.</param>
        /// <param name="below">The second color (below the first one) without transparency.</param>
        /// <returns>The outcome in the crossing section.</returns>
        public static Color Mix(Color above, Color below)
        {
            return Mix(above.Alpha, above, below);
        }

        /// <summary>
        /// Mixes two colors using alpha compositing as described here:
        /// http://en.wikipedia.org/wiki/Alpha_compositing
        /// </summary>
        /// <param name="alpha">The mixing parameter.</param>
        /// <param name="above">The first color (above) (no transparency).</param>
        /// <param name="below">The second color (below the first one) (no transparency).</param>
        /// <returns>The outcome in the crossing section.</returns>
        public static Color Mix(Double alpha, Color above, Color below)
        {
            var gamma = 1.0 - alpha;
            var r = gamma * below.R + alpha * above.R;
            var g = gamma * below.G + alpha * above.G;
            var b = gamma * below.B + alpha * above.B;
            return new Color((Byte)r, (Byte)g, (Byte)b);
        }

        #endregion

        #region Helpers

        static Byte Normalize(Single value)
        {
            return (Byte)Math.Max(Math.Min(Math.Round(255 * value), 255), 0);
        }

        static Byte Convert(Single value)
        {
            return (Byte)Math.Round(255f * value);
        }

        static Single HueToRgb(Single m1, Single m2, Single h)
        {
            const Single oneSixth = 1f / 6f;
            const Single twoThird = 2f / 3f;

            if (h < 0f)
                h += 1f;
            else if (h > 1f)
                h -= 1f;

            if (h < oneSixth)
                return m1 + (m2 - m1) * h * 6f;
            else if (h < 0.5)
                return m2;
            else if (h < twoThird)
                return m1 + (m2 - m1) * (twoThird - h) * 6f;

            return m1;
        }

        #endregion

        #region String Representation

        /// <summary>
        /// Returns a string representing the color in ARGB hex.
        /// </summary>
        /// <returns>The ARGB string.</returns>
        public override String ToString()
        {
            if (_alpha == 255)
            {
                var arguments = String.Join(", ", new[]
                {
                    R.ToString(),
                    G.ToString(),
                    B.ToString()
                });
                return FunctionNames.Rgb.CssFunction(arguments);
            }
            else
            {
                var arguments = String.Join(", ", new[]
                {
                    R.ToString(),
                    G.ToString(),
                    B.ToString(),
                    Alpha.ToString()
                });
                return FunctionNames.Rgba.CssFunction(arguments);
            }
        }

        /// <summary>
        /// Returns a formatted string representing the length.
        /// </summary>
        /// <param name="format">The format of the number.</param>
        /// <param name="formatProvider">The provider to use.</param>
        /// <returns>The unit string.</returns>
        public String ToString(String format, IFormatProvider formatProvider)
        {
            if (_alpha == 255)
            {
                var arguments = String.Join(", ", new[]
                {
                    R.ToString(format, formatProvider),
                    G.ToString(format, formatProvider),
                    B.ToString(format, formatProvider)
                });
                return FunctionNames.Rgb.CssFunction(arguments);
            }
            else
            {
                var arguments = String.Join(", ", new[]
                {
                    R.ToString(format, formatProvider),
                    G.ToString(format, formatProvider),
                    B.ToString(format, formatProvider),
                    Alpha.ToString(format, formatProvider)
                });
                return FunctionNames.Rgba.CssFunction(arguments);
            }
        }

        #endregion
    }
}
