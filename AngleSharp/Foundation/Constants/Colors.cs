namespace AngleSharp.DOM
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// This class contains information about colors like their
    /// given names or assignments of names to colors.
    /// Most names are derived from
    /// http://en.wikipedia.org/wiki/X11_color_names
    /// </summary>
    static class Colors
    {
        #region Fields

        static Dictionary<String, Color> _colors;

        #endregion

        #region ctor

        static Colors()
        {
            _colors = new Dictionary<String, Color>();

            Add("aliceblue", 240, 248, 255);
            Add("antiquewhite", 250, 235, 215);
            Add("aqua", 0, 255, 255);
            Add("aquamarine", 127, 255, 212);
            Add("azure", 240, 255, 255);
            Add("beige", 245, 245, 220);
            Add("bisque", 255, 228, 196);
            Add("black", 0, 0, 0);
            Add("blanchedalmond", 255, 235, 205);
            Add("blue", 0, 0, 255);
            Add("blueviolet", 138, 43, 226);
            Add("brown", 165, 42, 42);
            Add("burlywood", 222, 184, 135);
            Add("cadetblue", 95, 158, 160);
            Add("chartreuse", 127, 255, 0);
            Add("chocolate", 210, 105, 30);
            Add("coral", 255, 127, 80);
            Add("cornflowerblue", 100, 149, 237);
            Add("cornsilk", 255, 248, 220);
            Add("crimson", 220, 20, 60);
            Add("cyan", 0, 255, 255);
            Add("darkblue", 0, 0, 139);
            Add("darkcyan", 0, 139, 139);
            Add("darkgoldenrod", 184, 134, 11);
            Add("darkgray", 169, 169, 169);
            Add("darkgreen", 0, 100, 0);
            Add("darkgrey", 169, 169, 169);
            Add("darkkhaki", 189, 183, 107);
            Add("darkmagenta", 139, 0, 139);
            Add("darkolivegreen", 85, 107, 47);
            Add("darkorange", 255, 140, 0);
            Add("darkorchid", 153, 50, 204);
            Add("darkred", 139, 0, 0);
            Add("darksalmon", 233, 150, 122);
            Add("darkseagreen", 143, 188, 143);
            Add("darkslateblue", 72, 61, 139);
            Add("darkslategray", 47, 79, 79);
            Add("darkslategrey", 47, 79, 79);
            Add("darkturquoise", 0, 206, 209);
            Add("darkviolet", 148, 0, 211);
            Add("deeppink", 255, 20, 147);
            Add("deepskyblue", 0, 191, 255);
            Add("dimgray", 105, 105, 105);
            Add("dimgrey", 105, 105, 105);
            Add("dodgerblue", 30, 144, 255);
            Add("firebrick", 178, 34, 34);
            Add("floralwhite", 255, 250, 240);
            Add("forestgreen", 34, 139, 34);
            Add("fuchsia", 255, 0, 255);
            Add("gainsboro", 220, 220, 220);
            Add("ghostwhite", 248, 248, 255);
            Add("gold", 255, 215, 0);
            Add("goldenrod", 218, 165, 32);
            Add("gray", 128, 128, 128);
            Add("green", 0, 128, 0);
            Add("greenyellow", 173, 255, 47);
            Add("grey", 128, 128, 128);
            Add("honeydew", 240, 255, 240);
            Add("hotpink", 255, 105, 180);
            Add("indianred", 205, 92, 92);
            Add("indigo", 75, 0, 130);
            Add("ivory", 255, 255, 240);
            Add("khaki", 240, 230, 140);
            Add("lavender", 230, 230, 250);
            Add("lavenderblush", 255, 240, 245);
            Add("lawngreen", 124, 252, 0);
            Add("lemonchiffon", 255, 250, 205);
            Add("lightblue", 173, 216, 230);
            Add("lightcoral", 240, 128, 128);
            Add("lightcyan", 224, 255, 255);
            Add("lightgoldenrodyellow", 250, 250, 210);
            Add("lightgray", 211, 211, 211);
            Add("lightgreen", 144, 238, 144);
            Add("lightgrey", 211, 211, 211);
            Add("lightpink", 255, 182, 193);
            Add("lightsalmon", 255, 160, 122);
            Add("lightseagreen", 32, 178, 170);
            Add("lightskyblue", 135, 206, 250);
            Add("lightslategray", 119, 136, 153);
            Add("lightslategrey", 119, 136, 153);
            Add("lightsteelblue", 176, 196, 222);
            Add("lightyellow", 255, 255, 224);
            Add("lime", 0, 255, 0);
            Add("limegreen", 50, 205, 50);
            Add("linen", 250, 240, 230);
            Add("magenta", 255, 0, 255);
            Add("maroon", 128, 0, 0);
            Add("mediumaquamarine", 102, 205, 170);
            Add("mediumblue", 0, 0, 205);
            Add("mediumorchid", 186, 85, 211);
            Add("mediumpurple", 147, 112, 219);
            Add("mediumseagreen", 60, 179, 113);
            Add("mediumslateblue", 123, 104, 238);
            Add("mediumspringgreen", 0, 250, 154);
            Add("mediumturquoise", 72, 209, 204);
            Add("mediumvioletred", 199, 21, 133);
            Add("midnightblue", 25, 25, 112);
            Add("mintcream", 245, 255, 250);
            Add("mistyrose", 255, 228, 225);
            Add("moccasin", 255, 228, 181);
            Add("navajowhite", 255, 222, 173);
            Add("navy", 0, 0, 128);
            Add("oldlace", 253, 245, 230);
            Add("olive", 128, 128, 0);
            Add("olivedrab", 107, 142, 35);
            Add("orange", 255, 165, 0);
            Add("orangered", 255, 69, 0);
            Add("orchid", 218, 112, 214);
            Add("palegoldenrod", 238, 232, 170);
            Add("palegreen", 152, 251, 152);
            Add("paleturquoise", 175, 238, 238);
            Add("palevioletred", 219, 112, 147);
            Add("papayawhip", 255, 239, 213);
            Add("peachpuff", 255, 218, 185);
            Add("peru", 205, 133, 63);
            Add("pink", 255, 192, 203);
            Add("plum", 221, 160, 221);
            Add("powderblue", 176, 224, 230);
            Add("purple", 128, 0, 128);
            Add("red", 255, 0, 0);
            Add("rosybrown", 188, 143, 143);
            Add("royalblue", 65, 105, 225);
            Add("saddlebrown", 139, 69, 19);
            Add("salmon", 250, 128, 114);
            Add("sandybrown", 244, 164, 96);
            Add("seagreen", 46, 139, 87);
            Add("seashell", 255, 245, 238);
            Add("sienna", 160, 82, 45);
            Add("silver", 192, 192, 192);
            Add("skyblue", 135, 206, 235);
            Add("slateblue", 106, 90, 205);
            Add("slategray", 112, 128, 144);
            Add("slategrey", 112, 128, 144);
            Add("snow", 255, 250, 250);
            Add("springgreen", 0, 255, 127);
            Add("steelblue", 70, 130, 180);
            Add("tan", 210, 180, 140);
            Add("teal", 0, 128, 128);
            Add("thistle", 216, 191, 216);
            Add("tomato", 255, 99, 71);
            Add("turquoise", 64, 224, 208);
            Add("violet", 238, 130, 238);
            Add("wheat", 245, 222, 179);
            Add("white", 255, 255, 255);
            Add("whitesmoke", 245, 245, 245);
            Add("yellow", 255, 255, 0);
            Add("yellowgreen", 154, 205, 50);
            Add("transparent", Color.Transparent);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a color from the specified name.
        /// </summary>
        /// <param name="name">The name of the color</param>
        /// <returns>The color or transparent if no color was found.</returns>
        public static Color? FromName(String name)
        {
            Color color;

            if (_colors.TryGetValue(name.ToLower(), out color))
                return color;

            return null;
        }

        /// <summary>
        /// Gets the name of the given color.
        /// </summary>
        /// <param name="color">The color that searches its name.</param>
        /// <returns>The name of the given color or null.</returns>
        public static String GetNameFromColor(Color color)
        {
            foreach (var pair in _colors)
                if (pair.Value.Equals(color))
                    return pair.Key;

            return null;
        }

        #endregion

        #region Helpers

        static void Add(String name, Byte r, Byte g, Byte b)
        {
            _colors.Add(name, Color.FromRgb(r, g, b));
        }

        static void Add(String name, Color value)
        {
            _colors.Add(name, value);
        }

        #endregion
    }
}
