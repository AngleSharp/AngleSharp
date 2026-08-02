
namespace AngleSharp.Dom.Geometry
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the IDomQuad interface.
    /// </summary>
    [DomName("DOMQuad")]
    public interface IDomQuad
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        [DomName("p1")]
        IDomPoint P1 { get; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        [DomName("p2")]
        IDomPoint P2 { get; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        [DomName("p3")]
        IDomPoint P3 { get; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        [DomName("p4")]
        IDomPoint P4 { get; }

        /// <summary>
        /// Executes GetBounds and returns a value.
        /// </summary>
        [DomName("getBounds")]
        IDomRect GetBounds();
    }
}
