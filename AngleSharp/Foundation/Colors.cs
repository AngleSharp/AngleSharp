using System;

namespace AngleSharp
{
    static class Colors
    {
        public static HtmlColor FromName(String name)
        {
            switch (name)
            {
                case "aliceblue": return Create(240, 248, 255);
                case "antiquewhite": return Create(250, 235, 215);
                case "aqua": return Create(0, 255, 255);
                case "aquamarine": return Create(127, 255, 212);
                case "azure": return Create(240, 255, 255);
                case "beige": return Create(245, 245, 220);
                case "bisque": return Create(255, 228, 196);
                case "black": return Create(0, 0, 0);
                case "blanchedalmond": return Create(255, 235, 205);
                case "blue": return Create(0, 0, 255);
                case "blueviolet": return Create(138, 43, 226);
                case "brown": return Create(165, 42, 42);
                case "burlywood": return Create(222, 184, 135);
                case "cadetblue": return Create(95, 158, 160);
                case "chartreuse": return Create(127, 255, 0);
                case "chocolate": return Create(210, 105, 30);
                case "coral": return Create(255, 127, 80);
                case "cornflowerblue": return Create(100, 149, 237);
                case "cornsilk": return Create(255, 248, 220);
                case "crimson": return Create(220, 20, 60);
                case "cyan": return Create(0, 255, 255);
                case "darkblue": return Create(0, 0, 139);
                case "darkcyan": return Create(0, 139, 139);
                case "darkgoldenrod": return Create(184, 134, 11);
                case "darkgray": return Create(169, 169, 169);
                case "darkgreen": return Create(0, 100, 0);
                case "darkgrey": return Create(169, 169, 169);
                case "darkkhaki": return Create(189, 183, 107);
                case "darkmagenta": return Create(139, 0, 139);
                case "darkolivegreen": return Create(85, 107, 47);
                case "darkorange": return Create(255, 140, 0);
                case "darkorchid": return Create(153, 50, 204);
                case "darkred": return Create(139, 0, 0);
                case "darksalmon": return Create(233, 150, 122);
                case "darkseagreen": return Create(143, 188, 143);
                case "darkslateblue": return Create(72, 61, 139);
                case "darkslategray": return Create(47, 79, 79);
                case "darkslategrey": return Create(47, 79, 79);
                case "darkturquoise": return Create(0, 206, 209);
                case "darkviolet": return Create(148, 0, 211);
                case "deeppink": return Create(255, 20, 147);
                case "deepskyblue": return Create(0, 191, 255);
                case "dimgray": return Create(105, 105, 105);
                case "dimgrey": return Create(105, 105, 105);
                case "dodgerblue": return Create(30, 144, 255);
                case "firebrick": return Create(178, 34, 34);
                case "floralwhite": return Create(255, 250, 240);
                case "forestgreen": return Create(34, 139, 34);
                case "fuchsia": return Create(255, 0, 255);
                case "gainsboro": return Create(220, 220, 220);
                case "ghostwhite": return Create(248, 248, 255);
                case "gold": return Create(255, 215, 0);
                case "goldenrod": return Create(218, 165, 32);
                case "gray": return Create(128, 128, 128);
                case "green": return Create(0, 128, 0);
                case "greenyellow": return Create(173, 255, 47);
                case "grey": return Create(128, 128, 128);
                case "honeydew": return Create(240, 255, 240);
                case "hotpink": return Create(255, 105, 180);
                case "indianred": return Create(205, 92, 92);
                case "indigo": return Create(75, 0, 130);
                case "ivory": return Create(255, 255, 240);
                case "khaki": return Create(240, 230, 140);
                case "lavender": return Create(230, 230, 250);
                case "lavenderblush": return Create(255, 240, 245);
                case "lawngreen": return Create(124, 252, 0);
                case "lemonchiffon": return Create(255, 250, 205);
                case "lightblue": return Create(173, 216, 230);
                case "lightcoral": return Create(240, 128, 128);
                case "lightcyan": return Create(224, 255, 255);
                case "lightgoldenrodyellow": return Create(250, 250, 210);
                case "lightgray": return Create(211, 211, 211);
                case "lightgreen": return Create(144, 238, 144);
                case "lightgrey": return Create(211, 211, 211);
                case "lightpink": return Create(255, 182, 193);
                case "lightsalmon": return Create(255, 160, 122);
                case "lightseagreen": return Create(32, 178, 170);
                case "lightskyblue": return Create(135, 206, 250);
                case "lightslategray": return Create(119, 136, 153);
                case "lightslategrey": return Create(119, 136, 153);
                case "lightsteelblue": return Create(176, 196, 222);
                case "lightyellow": return Create(255, 255, 224);
                case "lime": return Create(0, 255, 0);
                case "limegreen": return Create(50, 205, 50);
                case "linen": return Create(250, 240, 230);
                case "magenta": return Create(255, 0, 255);
                case "maroon": return Create(128, 0, 0);
                case "mediumaquamarine": return Create(102, 205, 170);
                case "mediumblue": return Create(0, 0, 205);
                case "mediumorchid": return Create(186, 85, 211);
                case "mediumpurple": return Create(147, 112, 219);
                case "mediumseagreen": return Create(60, 179, 113);
                case "mediumslateblue": return Create(123, 104, 238);
                case "mediumspringgreen": return Create(0, 250, 154);
                case "mediumturquoise": return Create(72, 209, 204);
                case "mediumvioletred": return Create(199, 21, 133);
                case "midnightblue": return Create(25, 25, 112);
                case "mintcream": return Create(245, 255, 250);
                case "mistyrose": return Create(255, 228, 225);
                case "moccasin": return Create(255, 228, 181);
                case "navajowhite": return Create(255, 222, 173);
                case "navy": return Create(0, 0, 128);
                case "oldlace": return Create(253, 245, 230);
                case "olive": return Create(128, 128, 0);
                case "olivedrab": return Create(107, 142, 35);
                case "orange": return Create(255, 165, 0);
                case "orangered": return Create(255, 69, 0);
                case "orchid": return Create(218, 112, 214);
                case "palegoldenrod": return Create(238, 232, 170);
                case "palegreen": return Create(152, 251, 152);
                case "paleturquoise": return Create(175, 238, 238);
                case "palevioletred": return Create(219, 112, 147);
                case "papayawhip": return Create(255, 239, 213);
                case "peachpuff": return Create(255, 218, 185);
                case "peru": return Create(205, 133, 63);
                case "pink": return Create(255, 192, 203);
                case "plum": return Create(221, 160, 221);
                case "powderblue": return Create(176, 224, 230);
                case "purple": return Create(128, 0, 128);
                case "red": return Create(255, 0, 0);
                case "rosybrown": return Create(188, 143, 143);
                case "royalblue": return Create(65, 105, 225);
                case "saddlebrown": return Create(139, 69, 19);
                case "salmon": return Create(250, 128, 114);
                case "sandybrown": return Create(244, 164, 96);
                case "seagreen": return Create(46, 139, 87);
                case "seashell": return Create(255, 245, 238);
                case "sienna": return Create(160, 82, 45);
                case "silver": return Create(192, 192, 192);
                case "skyblue": return Create(135, 206, 235);
                case "slateblue": return Create(106, 90, 205);
                case "slategray": return Create(112, 128, 144);
                case "slategrey": return Create(112, 128, 144);
                case "snow": return Create(255, 250, 250);
                case "springgreen": return Create(0, 255, 127);
                case "steelblue": return Create(70, 130, 180);
                case "tan": return Create(210, 180, 140);
                case "teal": return Create(0, 128, 128);
                case "thistle": return Create(216, 191, 216);
                case "tomato": return Create(255, 99, 71);
                case "turquoise": return Create(64, 224, 208);
                case "violet": return Create(238, 130, 238);
                case "wheat": return Create(245, 222, 179);
                case "white": return Create(255, 255, 255);
                case "whitesmoke": return Create(245, 245, 245);
                case "yellow": return Create(255, 255, 0);
                case "yellowgreen": return Create(154, 205, 50);
                default: return Create(0, 0, 0);
            }
        }

        //TODO http://www.w3.org/TR/2011/REC-css3-color-20110607/ (systemcolors)

        static HtmlColor Create(byte r, byte g, byte b)
        {
            return new HtmlColor(r, g, b);
        }
    }
}
