namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/list-style-image
    /// </summary>
    sealed class CSSListStyleImageProperty : CSSProperty, ICssListStyleImageProperty
    {
        #region Fields

        internal static readonly Url Default = null;
        internal static readonly IValueConverter<Url> Converter = TakeOne(Keywords.None, Default).Or(WithUrl().To(m => new Url(m)));
        Url _image;

        #endregion

        #region ctor

        internal CSSListStyleImageProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.ListStyleImage, rule, PropertyFlags.Inherited)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected image.
        /// </summary>
        public Url Image
        {
            get { return _image; }
        }

        #endregion

        #region Methods

        public void SetImage(Url image)
        {
            _image = image;
        }

        internal override void Reset()
        {
            _image = Default;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            return Converter.TryConvert(value, SetImage);
        }

        #endregion
    }
}
