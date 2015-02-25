namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/list-style
    /// </summary>
    sealed class CssListStyleProperty : CssShorthandProperty, ICssListStyleProperty
    {
        #region Fields

        internal static readonly IValueConverter<Tuple<ListStyle, ListPosition, IImageSource>> Converter = Converters.WithAny(
            CssListStyleTypeProperty.Converter.Option(CssListStyleTypeProperty.Default),
            CssListStylePositionProperty.Converter.Option(CssListStylePositionProperty.Default),
            CssListStyleImageProperty.Converter.Option(CssListStyleImageProperty.Default));

        readonly CssListStyleTypeProperty _type;
        readonly CssListStyleImageProperty _image;
        readonly CssListStylePositionProperty _position;

        #endregion

        #region ctor

        internal CssListStyleProperty(CssStyleDeclaration rule)
            : base(PropertyNames.ListStyle, rule, PropertyFlags.Inherited)
        {
            _type = Get<CssListStyleTypeProperty>();
            _image = Get<CssListStyleImageProperty>();
            _position = Get<CssListStylePositionProperty>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected list-style type.
        /// </summary>
        public ListStyle Style
        {
            get { return _type.Style; }
        }

        /// <summary>
        /// Gets the selected image for the list.
        /// </summary>
        public IImageSource Image
        {
            get { return _image.Image; }
        }

        /// <summary>
        /// Gets the selected position for the list-style.
        /// </summary>
        public ListPosition Position
        {
            get { return _position.Position; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, m =>
                {
                    _type.SetStyle(m.Item1);
                    _position.SetPosition(m.Item2);
                    _image.SetImage(m.Item3);
                });
        }

        internal override String SerializeValue(IEnumerable<CssProperty> properties)
        {
            if (!IsComplete(properties))
                return String.Empty;

            return String.Format("{0} {1} {2}", _type.SerializeValue(), _image.SerializeValue(), _position.SerializeValue());
        }

        #endregion
    }
}
