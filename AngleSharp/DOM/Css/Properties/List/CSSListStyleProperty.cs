namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/list-style
    /// </summary>
    sealed class CSSListStyleProperty : CSSProperty, ICssListStyleProperty
    {
        #region Fields

        ListStyle _type;
        CSSImageValue _image;
        ListPosition _position;

        #endregion

        #region ctor

        internal CSSListStyleProperty()
            : base(PropertyNames.ListStyle, PropertyFlags.Inherited)
        {
            _type = ListStyle.Disc;
            _image = null;
            _position = ListPosition.Outside;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected list-style type.
        /// </summary>
        public ListStyle Style
        {
            get { return _type; }
        }

        /// <summary>
        /// Gets the selected image for the list.
        /// </summary>
        internal CSSImageValue Image
        {
            get { return _image; }
        }

        /// <summary>
        /// Gets the selected position for the list-style.
        /// </summary>
        public ListPosition Position
        {
            get { return _position; }
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
            if (value == CSSValue.Inherit)
                return true;

            var list = value as CSSValueList ?? new CSSValueList(value);
            ListStyle? type = null;
            CSSImageValue image = null;
            ListPosition? position = null;

            if (list.Length > 3)
                return false;

            for (int i = 0; i < list.Length; i++)
            {
                if (!type.HasValue && (type = list[i].ToListStyle()).HasValue)
                    continue;
                else if (image == null && (image = list[i].AsImage()) != null)
                    continue;
                else if (!position.HasValue && (position = list[i].ToListPosition()).HasValue)
                    continue;

                return false;
            }

            _type = type.HasValue ? type.Value : ListStyle.Disc;
            _image = image;
            _position = position.HasValue ? position.Value : ListPosition.Outside;
            return true;
        }

        #endregion
    }
}
