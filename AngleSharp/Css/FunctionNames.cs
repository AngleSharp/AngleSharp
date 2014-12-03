namespace AngleSharp.Css
{
    using System;

    /// <summary>
    /// Contains a list of known CSS functions.
    /// </summary>
    static class FunctionNames
    {
        /// <summary>
        /// The url function.
        /// </summary>
        public static readonly String Url = "url";

        /// <summary>
        /// The url-prefix function.
        /// </summary>
        public static readonly String Url_Prefix = "url-prefix";

        /// <summary>
        /// The domain function.
        /// </summary>
        public static readonly String Domain = "domain";

        /// <summary>
        /// The regexp function.
        /// </summary>
        public static readonly String Regexp = "regexp";

        /// <summary>
        /// The rgba function.
        /// </summary>
        public static readonly String Rgba = "rgba";

        /// <summary>
        /// The rgb function.
        /// </summary>
        public static readonly String Rgb = "rgb";

        /// <summary>
        /// The hsl function.
        /// </summary>
        public static readonly String Hsl = "hsl";

        /// <summary>
        /// The hsla function.
        /// </summary>
        public static readonly String Hsla = "hsla";

        /// <summary>
        /// The rect function.
        /// </summary>
        public static readonly String Rect = "rect";

        /// <summary>
        /// The attr function.
        /// </summary>
        public static readonly String Attr = "attr";

        /// <summary>
        /// The linear-gradient function.
        /// </summary>
        public static readonly String LinearGradient = "linear-gradient";

        /// <summary>
        /// The radial-gradient function.
        /// </summary>
        public static readonly String RadialGradient = "radial-gradient";

        /// <summary>
        /// The repeating-linear-gradient function.
        /// </summary>
        public static readonly String RepeatingLinearGradient = "repeating-linear-gradient";

        /// <summary>
        /// The repeating-radial-gradient function.
        /// </summary>
        public static readonly String RepeatingRadialGradient = "repeating-radial-gradient";

        /// <summary>
        /// The image function.
        /// </summary>
        public static readonly String Image = "image";

        /// <summary>
        /// The counter function.
        /// </summary>
        public static readonly String Counter = "counter";

        /// <summary>
        /// The counters function.
        /// </summary>
        public static readonly String Counters = "counters";

        /// <summary>
        /// The calc function.
        /// </summary>
        public static readonly String Calc = "calc";

        /// <summary>
        /// The toggle function.
        /// </summary>
        public static readonly String Toggle = "toggle";

        /// <summary>
        /// The translate function.
        /// </summary>
        public static readonly String Translate = "translate";

        /// <summary>
        /// The translatex function.
        /// </summary>
        public static readonly String TranslateX = "translateX";

        /// <summary>
        /// The translatey function.
        /// </summary>
        public static readonly String TranslateY = "translateY";

        /// <summary>
        /// The translatez function.
        /// </summary>
        public static readonly String TranslateZ = "translateZ";

        /// <summary>
        /// The translate3d function.
        /// </summary>
        public static readonly String Translate3d = "translate3d";

        /// <summary>
        /// The matrix function.
        /// </summary>
        public static readonly String Matrix = "matrix";

        /// <summary>
        /// The matrix3d function.
        /// </summary>
        public static readonly String Matrix3d = "matrix3d";

        /// <summary>
        /// The rotate function.
        /// </summary>
        public static readonly String Rotate = "rotate";

        /// <summary>
        /// The rotate3d function.
        /// </summary>
        public static readonly String Rotate3d = "rotate3d";

        /// <summary>
        /// The rotatex function.
        /// </summary>
        public static readonly String RotateX = "rotateX";

        /// <summary>
        /// The rotatey function.
        /// </summary>
        public static readonly String RotateY = "rotateY";

        /// <summary>
        /// The rotatez function.
        /// </summary>
        public static readonly String RotateZ = "rotateZ";

        /// <summary>
        /// The skew function.
        /// </summary>
        public static readonly String Skew = "skew";

        /// <summary>
        /// The skewx function.
        /// </summary>
        public static readonly String SkewX = "skewX";

        /// <summary>
        /// The skewy function.
        /// </summary>
        public static readonly String SkewY = "skewY";

        /// <summary>
        /// The scale function.
        /// </summary>
        public static readonly String Scale = "scale";

        /// <summary>
        /// The scale3d function.
        /// </summary>
        public static readonly String Scale3d = "scale3d";

        /// <summary>
        /// The scalez function.
        /// </summary>
        public static readonly String ScaleX = "scaleX";

        /// <summary>
        /// The scaley function.
        /// </summary>
        public static readonly String ScaleY = "scaleY";

        /// <summary>
        /// The scalex function.
        /// </summary>
        public static readonly String ScaleZ = "scaleZ";

        /// <summary>
        /// The steps function.
        /// </summary>
        public static readonly String Steps = "steps";

        /// <summary>
        /// The cubic-bezier function.
        /// </summary>
        public static readonly String CubicBezier = "cubic-bezier";

        /// <summary>
        /// The perspective function.
        /// </summary>
        public static readonly String Perspective = "perspective";


        /// <summary>
        /// Creates a function call expression from the given strings.
        /// </summary>
        /// <param name="function">The name of the function.</param>
        /// <param name="arguments">The arguments of the function.</param>
        /// <returns>The CSS function call expression.</returns>
        public static String Build(String function, params String[] arguments)
        {
            return String.Concat(function, "(", String.Join(", ", arguments), ")");
        }
    }
}
