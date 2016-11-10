namespace AngleSharp.Css.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Common;
    using System;

    /// <summary>
    /// Represents a CSS declaration block, including its underlying state, where
    /// this underlying state depends upon the source of the CSSStyleDeclaration instance.
    /// </summary>
    [DomName("CSSStyleDeclaration")]
    public interface ICssStyleDeclaration : ICssProperties, IStyleFormattable, IBindable
    {
        /// <summary>
        /// Gets the name of the property with the specified index.
        /// </summary>
        /// <param name="index">The index of the property to retrieve.</param>
        /// <returns>The name of the property at the given index.</returns>
        [DomName("item")]
        [DomAccessor(Accessors.Getter)]
        String this[Int32 index] { get; }

        /// <summary>
        /// Gets or sets the textual representation of the declaration block.
        /// </summary>
        [DomName("cssText")]
        String CssText { get; set; }

        /// <summary>
        /// Gets the containing rule, if any.
        /// </summary>
        [DomName("parentRule")]
        ICssRule Parent { get; }

        /// <summary>
        /// Sets the parent rule.
        /// </summary>
        /// <param name="rule">The rule to use as parent.</param>
        void SetParent(ICssRule rule);
    }
}
