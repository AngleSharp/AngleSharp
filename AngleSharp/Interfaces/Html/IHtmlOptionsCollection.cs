namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents a collection of HTML option elements.
    /// </summary>
    [DomName("HTMLOptionsCollection")]
    public interface IHtmlOptionsCollection : IHtmlCollection<IHtmlOptionElement>
    {
        /// <summary>
        /// Gets an HTML option element at the specified index.
        /// </summary>
        /// <param name="index">The index of the element.</param>
        /// <returns>The option at the given position.</returns>
        [DomAccessor(Accessors.Getter)]
        IHtmlOptionElement GetOptionAt(Int32 index);

        /// <summary>
        /// Sets an HTML option element at the specified index.
        /// </summary>
        /// <param name="index">The index of the element.</param>
        /// <param name="option">The option to set at the given position.</param>
        [DomAccessor(Accessors.Setter)]
        void SetOptionAt(Int32 index, IHtmlOptionElement option);

        /// <summary>
        /// Adds an option element to the collection.
        /// </summary>
        /// <param name="element">The element to add.</param>
        /// <param name="before">The optional reference element for inserting.</param>
        [DomName("add")]
        void Add(IHtmlOptionElement element, IHtmlElement before = null);

        /// <summary>
        /// Adds an options group element to the collection.
        /// </summary>
        /// <param name="element">The element to add.</param>
        /// <param name="before">The optional reference element for inserting.</param>
        [DomName("add")]
        void Add(IHtmlOptionsGroupElement element, IHtmlElement before = null);

        /// <summary>
        /// Removes an element from the collection.
        /// </summary>
        /// <param name="index">The index of the element of remove.</param>
        [DomName("remove")]
        void Remove(Int32 index);

        /// <summary>
        /// Gets or sets the selected index.
        /// </summary>
        [DomName("selectedIndex")]
        Int32 SelectedIndex { get; set; }
    }
}
