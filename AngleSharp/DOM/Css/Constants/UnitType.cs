namespace AngleSharp.DOM.Css
{
    using AngleSharp.Attributes;

    /// <summary>
    /// Representation of the various primitive types.
    /// </summary>
    public enum UnitType : ushort
    {
        /// <summary>
        /// An unknown unit.
        /// </summary>
        [DomName("CSS_UNKNOWN")]
        Unknown = 0,
        /// <summary>
        /// A number.
        /// </summary>
        [DomName("CSS_NUMBER")]
        Number = 1,
        /// <summary>
        /// A relative (percent) unit.
        /// </summary>
        [DomName("CSS_PERCENTAGE")]
        Percent = 2,
        /// <summary>
        /// Unit is relative to the size of a M.
        /// </summary>
        [DomName("CSS_EMS")]
        Em = 3,
        /// <summary>
        /// Unit is relative to the size of a X.
        /// </summary>
        [DomName("CSS_EXS")]
        Ex = 4,
        /// <summary>
        /// Unit is pixels.
        /// </summary>
        [DomName("CSS_PX")]
        Px = 5,
        /// <summary>
        /// Unit is centimeters. Computed via dpi. 2.54cm = 1in
        /// </summary>
        [DomName("CSS_CM")]
        Cm = 6,
        /// <summary>
        /// Unit is millimeters. Computed via dpi. 25.4mm = 1in
        /// </summary>
        [DomName("CSS_MM")]
        Mm = 7,
        /// <summary>
        /// Unit is inches. Computed via dpi. 1in * dpi = px
        /// </summary>
        [DomName("CSS_IN")]
        In = 8,
        /// <summary>
        /// Unit is points. 72pt = 1in.
        /// </summary>
        [DomName("CSS_PT")]
        Pt = 9,
        /// <summary>
        /// Unit is pica. 6pc = 1in.
        /// </summary>
        [DomName("CSS_PC")]
        Pc = 10,
        /// <summary>
        /// Unit is degrees. 360deg = 1turn.
        /// </summary>
        [DomName("CSS_DEG")]
        Deg = 11,
        /// <summary>
        /// Unit is radiants. 2PIrad = 1turn.
        /// </summary>
        [DomName("CSS_RAD")]
        Rad = 12,
        /// <summary>
        /// Unit is gradians. 1grad = 400turn.
        /// </summary>
        [DomName("CSS_GRAD")]
        Grad = 13,
        /// <summary>
        /// Unit is milliseconds.
        /// </summary>
        [DomName("CSS_MS")]
        Ms = 14,
        /// <summary>
        /// Unit is seconds.
        /// </summary>
        [DomName("CSS_S")]
        S = 15,
        /// <summary>
        /// Unit is hertz.
        /// </summary>
        [DomName("CSS_HZ")]
        Hz = 16,
        /// <summary>
        /// Unit is kilohertz.
        /// </summary>
        [DomName("CSS_KHZ")]
        Khz = 17,
        /// <summary>
        /// An unknown dimension value.
        /// </summary>
        [DomName("CSS_DIMENSION")]
        Dimension = 18,
        /// <summary>
        /// A string value.
        /// </summary>
        [DomName("CSS_STRING")]
        String = 19,
        /// <summary>
        /// An uri value.
        /// </summary>
        [DomName("CSS_URI")]
        Uri = 20,
        /// <summary>
        /// An identifier.
        /// </summary>
        [DomName("CSS_IDENT")]
        Ident = 21,
        /// <summary>
        /// An attribute name.
        /// </summary>
        [DomName("CSS_ATTR")]
        Attr = 22,
        /// <summary>
        /// A counter value.
        /// </summary>
        [DomName("CSS_COUNTER")]
        Counter = 23,
        /// <summary>
        /// A rectangle value.
        /// </summary>
        [DomName("CSS_RECT")]
        Rect = 24,
        /// <summary>
        /// A RGB color definition.
        /// </summary>
        [DomName("CSS_RGBCOLOR")]
        RgbColor = 25,
        /// <summary>
        /// A transition function.
        /// </summary>
        Transition,
        /// <summary>
        /// A gradient function.
        /// </summary>
        Gradient,
        /// <summary>
        /// A list of image urls.
        /// </summary>
        ImageList,
        /// <summary>
        /// A transformation rule.
        /// </summary>
        Transform
    }
}
