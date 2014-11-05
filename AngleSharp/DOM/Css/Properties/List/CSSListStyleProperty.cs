namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;
    using System.Collections.Generic;

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
        public Object Image
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
            var list = value as CSSValueList ?? new CSSValueList(value);
            CSSValue type = null;
            CSSValue image = null;
            CSSValue position = null;

            if (list.Length > 3)
                return false;

            for (int i = 0; i < list.Length; i++)
            {
                if (!_type.CanStore(list[i], ref type) &&
                    !_image.CanStore(list[i], ref image) &&
                    !_position.CanStore(list[i], ref position))
                    return false;
            }

            return _type.TrySetValue(type) && _image.TrySetValue(image) && _position.TrySetValue(position);
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
