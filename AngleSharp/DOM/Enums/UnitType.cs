using System;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// An integer indicating which type of unit applies to the value.
    /// </summary>
    public enum UnitType : ushort
    {
        /// <summary>
        /// The value is not a recognized CSS value. The value can only be obtained by using the cssText attribute.
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// The value is a simple number. The value can be obtained by using the getFloatValue method.
        /// </summary>
        Number = 1,
        /// <summary>
        /// The value is a percentage. The value can be obtained by using the getFloatValue method.
        /// </summary>
        Percentage = 2,
        /// <summary>
        /// The value is a length (ems). The value can be obtained by using the getFloatValue method.
        /// </summary>
        Ems = 3,
        /// <summary>
        /// The value is a length (exs). The value can be obtained by using the getFloatValue method.
        /// </summary>
        Exs = 4,
        /// <summary>
        /// The value is a length (px). The value can be obtained by using the getFloatValue method.
        /// </summary>
        Px = 5,
        /// <summary>
        /// The value is a length (cm). The value can be obtained by using the getFloatValue method.
        /// </summary>
        Cm = 6,
        /// <summary>
        /// The value is a length (mm). The value can be obtained by using the getFloatValue method.
        /// </summary>
        Mm = 7,
        /// <summary>
        /// The value is a length (in). The value can be obtained by using the getFloatValue method.
        /// </summary>
        In = 8,
        /// <summary>
        /// The value is a length (pt). The value can be obtained by using the getFloatValue method.
        /// </summary>
        Pt = 9,
        /// <summary>
        /// The value is a length (pc). The value can be obtained by using the getFloatValue method.
        /// </summary>
        Pc = 10,
        /// <summary>
        /// The value is an angle (deg). The value can be obtained by using the getFloatValue method.
        /// There are 360 degrees in a full circle.
        /// </summary>
        Deg = 11,
        /// <summary>
        /// The value is an angle (rad). The value can be obtained by using the getFloatValue method.
        /// There are 2*pi radians in a full circle.
        /// </summary>
        Rad = 12,
        /// <summary>
        /// The value is an angle (grad). The value can be obtained by using the getFloatValue method.
        /// There are 400 gradians in a full circle.
        /// </summary>
        Grad = 13,
        /// <summary>
        /// The value is a time (ms). The value can be obtained by using the getFloatValue method.
        /// </summary>
        Ms = 14,
        /// <summary>
        /// The value is a time (s). The value can be obtained by using the getFloatValue method.
        /// </summary>
        S = 15,
        /// <summary>
        /// The value is a frequency (Hz). The value can be obtained by using the getFloatValue method.
        /// </summary>
        Hz = 16,
        /// <summary>
        /// The value is a frequency (kHz). The value can be obtained by using the getFloatValue method.
        /// </summary>
        Khz = 17,
        /// <summary>
        /// The value is a number with an unknown dimension. The value can be obtained by using the getFloatValue method.
        /// </summary>
        Dimension = 18,
        /// <summary>
        /// The value is a STRING. The value can be obtained by using the getStringValue method.
        /// </summary>
        String = 19,
        /// <summary>
        /// The value is a URI. The value can be obtained by using the getStringValue method.
        /// </summary>
        Uri = 20,
        /// <summary>
        /// The value is an identifier. The value can be obtained by using the getStringValue method.
        /// </summary>
        Ident = 21,
        /// <summary>
        /// The value is a attribute function. The value can be obtained by using the getStringValue method.
        /// </summary>
        Attr = 22,
        /// <summary>
        /// The value is a counter or counters function. The value can be obtained by using the getCounterValue method.
        /// </summary>
        Counter = 23,
        /// <summary>
        /// The value is a rect function. The value can be obtained by using the getRectValue method.
        /// </summary>
        Rect = 24,
        /// <summary>
        /// The value is a RGB color. The value can be obtained by using the getRGBColorValue method.
        /// </summary>
        Rgbcolor = 25,
        /// <summary>
        /// The value is relative to the viewport width.
        /// </summary>
        Vw = 26,
        /// <summary>
        /// The value is relative to the viewport height.
        /// </summary>
        Vh = 28,
        /// <summary>
        /// The value is relative to the minimum of viewport width and height.
        /// </summary>
        Vmin = 29,
        /// <summary>
        /// The value is relative to the maximum of viewport width and height.
        /// </summary>
        Vmax = 30,
        /// <summary>
        /// The value is a turn. The value can be obtained by using the getFloatValue method.
        /// There is 1 turn in a full circle.
        /// </summary>
        Turn = 31,
    }
}
