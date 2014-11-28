namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;


    /// <summary>
    /// More information available at
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/list-style
    /// </summary>
    sealed class CSSListStyleProperty : CSSShorthandProperty, ICssListStyleProperty
    {
        #region Fields

        readonly CSSListStyleTypeProperty _type;
        readonly CSSListStyleImageProperty _image;
        readonly CSSListStylePositionProperty _position;

        #endregion

        #region ctor

        internal CSSListStyleProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.ListStyle, rule, PropertyFlags.Inherited)
        {
            _type = Get<CSSListStyleTypeProperty>();
            _image = Get<CSSListStyleImageProperty>();
            _position = Get<CSSListStylePositionProperty>();
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
        public Url Image
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
        protected override Boolean IsValid(CSSValue value)
        {
            return this.WithOptions(
                        this.From(Map.ListStyles),
                        this.From(Map.ListPositions),
                        this.WithUrl().To(m => new Url(m)),
                    Tuple.Create(ListStyle.Disc, ListPosition.Outside, (Url)null)).TryConvert(value, m =>
                {
                    _type.SetStyle(m.Item1);
                    _position.SetPosition(m.Item2);
                    _image.SetImage(m.Item3);
                });
        }

        internal override String SerializeValue(IEnumerable<CSSProperty> properties)
        {
            if (!IsComplete(properties))
                return String.Empty;

            return String.Format("{0} {1} {2}", _type.SerializeValue(), _image.SerializeValue(), _position.SerializeValue());
        }

        #endregion
    }
}
