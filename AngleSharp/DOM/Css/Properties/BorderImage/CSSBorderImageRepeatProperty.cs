namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-image-repeat
    /// </summary>
    public sealed class CSSBorderImageRepeatProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, BorderRepeat> _modes = new Dictionary<String, BorderRepeat>(StringComparer.OrdinalIgnoreCase);
        BorderRepeat _horizontal;
        BorderRepeat _vertical;

        #endregion

        #region ctor

        static CSSBorderImageRepeatProperty()
        {
            _modes.Add(Keywords.Stretch, BorderRepeat.Stretch);
            _modes.Add(Keywords.Repeat, BorderRepeat.Repeat);
            _modes.Add(Keywords.Round, BorderRepeat.Round);
        }

        internal CSSBorderImageRepeatProperty()
            : base(PropertyNames.BorderImageRepeat)
        {
            _horizontal = BorderRepeat.Stretch;
            _vertical = BorderRepeat.Stretch;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the horizontal repeat value.
        /// </summary>
        public BorderRepeat Horizontal
        {
            get { return _horizontal; }
        }

        /// <summary>
        /// Gets the vertical repeat value.
        /// </summary>
        public BorderRepeat Vertical
        {
            get { return _vertical; }
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
            BorderRepeat mode;

            if (value is CSSIdentifierValue && _modes.TryGetValue(((CSSIdentifierValue)value).Value, out mode))
                _horizontal = _vertical = mode;
            else if (value is CSSValueList)
            {
                var list = (CSSValueList)value;
                var modes = new BorderRepeat[2];

                if (list.Length > 2)
                    return false;

                for (int i = 0; i < 2; i++)
			    {
                    if (list[i] is CSSIdentifierValue == false || !_modes.TryGetValue(((CSSIdentifierValue)list[i]).Value, out modes[i]))
                        return false;
			    }

                _horizontal = modes[0];
                _vertical = modes[1];               
            }
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
