namespace AngleSharp
{
    using System;

    /// <summary>
    /// Defines some properties of a node.
    /// Exclusive maximum is 0x100000000.
    /// Inclusive maximum is 0x0FFFFFFFF.
    /// </summary>
    [Flags]
    enum NodeFlags : uint
    {
        /// <summary>
        /// No special properties.
        /// </summary>
        None = 0x0,
        /// <summary>
        /// The element is self-closing.
        /// </summary>
        SelfClosing = 0x1,
        /// <summary>
        /// The element is special.
        /// </summary>
        Special = 0x2,
        /// <summary>
        /// The element has literal text.
        /// </summary>
        LiteralText = 0x4,
        /// <summary>
        /// The element may start with an additional free line.
        /// </summary>
        LineTolerance = 0x8,
        /// <summary>
        /// The element is part of the HTML namespace.
        /// </summary>
        HtmlMember = 0x10,
        /// <summary>
        /// The element is an HTML text integration point.
        /// </summary>
        HtmlTip = 0x20,
        /// <summary>
        /// The element is part of the MathML namespace.
        /// </summary>
        MathMember = 0x100,
        /// <summary>
        /// The element is an MathML text integration point.
        /// </summary>
        MathTip = 0x200,
        /// <summary>
        /// The element is part of the SVG namespace.
        /// </summary>
        SvgMember = 0x1000,
        /// <summary>
        /// The element is an SVG text integration point.
        /// </summary>
        SvgTip = 0x2000,
    }
}
