namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// An integer indicating which type of unit applies to the value.
    /// </summary>
    public enum CssUnit : ushort
    {
        /// <summary>
        /// The value is not a recognized CSS value.
        /// </summary>
        Unknown,
        /// <summary>
        /// The value is a length (ems).
        /// </summary>
        Ems,
        /// <summary>
        /// The value is a length (exs). Usually about 0.5em for most fonts.
        /// </summary>
        Exs,
        /// <summary>
        /// The value is a length (px).
        /// </summary>
        Px,
        /// <summary>
        /// The value is a length (cm).
        /// </summary>
        Cm,
        /// <summary>
        /// The value is a length (mm).
        /// </summary>
        Mm,
        /// <summary>
        /// The value is a length (in).
        /// </summary>
        In,
        /// <summary>
        /// The value is a length (pt).
        /// </summary>
        Pt,
        /// <summary>
        /// The value is a length (pc).
        /// </summary>
        Pc,
        /// <summary>
        /// The value is a length (width of the 0-character).
        /// </summary>
        Ch,
        /// <summary>
        /// The value is the relative to the font-size of the root element (in ems).
        /// </summary>
        Rems,
        /// <summary>
        /// The value is relative to the viewport width.
        /// </summary>
        Vw,
        /// <summary>
        /// The value is relative to the viewport height.
        /// </summary>
        Vh,
        /// <summary>
        /// The value is relative to the minimum of viewport width and height.
        /// </summary>
        Vmin,
        /// <summary>
        /// The value is relative to the maximum of viewport width and height.
        /// </summary>
        Vmax,
        /// <summary>
        /// The value is an angle (deg). There are 360 degrees in a full circle.
        /// </summary>
        Deg,
        /// <summary>
        /// The value is an angle (rad). There are 2*pi radians in a full circle.
        /// </summary>
        Rad,
        /// <summary>
        /// The value is an angle (grad). There are 400 gradians in a full circle.
        /// </summary>
        Grad,
        /// <summary>
        /// The value is a turn. There is 1 turn in a full circle.
        /// </summary>
        Turn,
        /// <summary>
        /// The value is a time (ms).
        /// </summary>
        Ms,
        /// <summary>
        /// The value is a time (s).
        /// </summary>
        S,
        /// <summary>
        /// The value is a frequency (Hz).
        /// </summary>
        Hz,
        /// <summary>
        /// The value is a frequency (kHz).
        /// </summary>
        Khz,
        /// <summary>
        /// The value is a resolution (dots per in).
        /// </summary>
        Dpi,
        /// <summary>
        /// The value is a resolution (dots per cm).
        /// </summary>
        Dpcm,
        /// <summary>
        /// The value is a resolution (dots per px).
        /// </summary>
        Dppx,
    }
}
