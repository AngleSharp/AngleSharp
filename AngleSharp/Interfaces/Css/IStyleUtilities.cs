namespace AngleSharp.Dom.Css
{
    using AngleSharp.Attributes;

    /// <summary>
    /// A set of useful methods for retrieving style information.
    /// </summary>
    [DomName("GetStyleUtils")]
    public interface IStyleUtilities
    {
        /// <summary>
        /// Gets a live CSS declaration block with properties
        /// that have a cascaded value for the context object.
        /// </summary>
        [DomName("cascadedStyle")]
        ICssStyleDeclaration CascadedStyle { get; }

        /// <summary>
        /// Gets a live CSS declaration block with only the default
        /// properties representing the value for the context object.
        /// </summary>
        [DomName("defaultStyle")]
        ICssStyleDeclaration DefaultStyle { get; }

        /// <summary>
        /// Gets a live CSS declaration block with properties
        /// that represent the value computed for the context object.
        /// </summary>
        [DomName("rawComputedStyle")]
        ICssStyleDeclaration RawComputedStyle { get; }

        /// <summary>
        /// Gets a live CSS declaration block with properties,
        /// whcih are the used values computed for the context object.
        /// </summary>
        [DomName("UsedStyle")]
        ICssStyleDeclaration UsedStyle { get; }
    }
}
