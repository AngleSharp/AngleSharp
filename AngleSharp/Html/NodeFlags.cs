namespace AngleSharp.Html
{
    using System;

    /// <summary>
    /// Defines some properties of a node.
    /// Exclusive maximum is 0x100000000.
    /// Inclusive maximum is 0x0FFFFFFFF.
    /// General range: 0x1 to 0x80
    /// HTML range   : 0x100 to 0x8000
    /// MathML range : 0x10000 to 0x800000
    /// SVG range    : 0x1000000 to 0x80000000
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
        /// The element is implicitely closed.
        /// </summary>
        ImplicitelyClosed = 0x10,
        /// <summary>
        /// The end of the element is implied.
        /// </summary>
        ImpliedEnd = 0x20,
        /// <summary>
        /// The element is opening a scope.
        /// </summary>
        Scoped = 0x40,
        /// <summary>
        /// The element is part of the HTML namespace.
        /// </summary>
        HtmlMember = 0x100,
        /// <summary>
        /// The element is an HTML text integration point.
        /// </summary>
        HtmlTip = 0x200,
        /// <summary>
        /// The element is an HTML formatting element.
        /// </summary>
        HtmlFormatting = 0x800,
        /// <summary>
        /// The element is opening a list scope.
        /// </summary>
        HtmlListScoped = 0x1000,
        /// <summary>
        /// The element is opening a select scope.
        /// </summary>
        HtmlSelectScoped = 0x2000,
        /// <summary>
        /// The element is opening a table section scope.
        /// </summary>
        HtmlTableSectionScoped = 0x4000,
        /// <summary>
        /// The element is opening a table scope.
        /// </summary>
        HtmlTableScoped = 0x8000,
        /// <summary>
        /// The element is part of the MathML namespace.
        /// </summary>
        MathMember = 0x10000,
        /// <summary>
        /// The element is an MathML text integration point.
        /// </summary>
        MathTip = 0x20000,
        /// <summary>
        /// The element is part of the SVG namespace.
        /// </summary>
        SvgMember = 0x1000000,
        /// <summary>
        /// The element is an SVG text integration point.
        /// </summary>
        SvgTip = 0x2000000,
    }
}
