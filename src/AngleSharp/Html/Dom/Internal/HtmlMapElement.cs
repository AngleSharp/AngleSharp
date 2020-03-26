namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents the HTML map element.
    /// </summary>
    sealed class HtmlMapElement : HtmlElement, IHtmlMapElement
    {
        #region Fields

        private HtmlCollection<IHtmlAreaElement> _areas;
        private HtmlCollection<IHtmlImageElement> _images;

        #endregion

        #region ctor

        public HtmlMapElement(Document owner, String prefix = null)
            : base(owner, TagNames.Map, prefix)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of the href attribute.
        /// </summary>
        public String Name
        {
            get => this.GetOwnAttribute(AttributeNames.Name);
            set => this.SetOwnAttribute(AttributeNames.Name, value);
        }

        /// <summary>
        /// Gets a collection representing the area elements
        /// associated to this map.
        /// </summary>
        public IHtmlCollection<IHtmlAreaElement> Areas => _areas ?? (_areas = new HtmlCollection<IHtmlAreaElement>(this, deep: false));

        /// <summary>
        /// Gets a collection representing the img and object
        /// elements associated to this element.
        /// </summary>
        public IHtmlCollection<IHtmlImageElement> Images => _images ?? (_images = new HtmlCollection<IHtmlImageElement>(Owner.DocumentElement, predicate: IsAssociatedImage));

        #endregion

        #region Helper

        private Boolean IsAssociatedImage(IHtmlImageElement image)
        {
            var usemap = image.UseMap;

            if (!String.IsNullOrEmpty(usemap))
            {
                var name = usemap.Has(Symbols.Num) ? "#" + Name : Name;
                return usemap.Is(name);
            }

            return false;
        }

        #endregion
    }
}
