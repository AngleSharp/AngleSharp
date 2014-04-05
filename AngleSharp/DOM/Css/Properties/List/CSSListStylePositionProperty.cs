namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/list-style-position
    /// </summary>
    sealed class CSSListStylePositionProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, ListPosition> positions = new Dictionary<String, ListPosition>();
        ListPosition _position;

        #endregion

        #region ctor

        static CSSListStylePositionProperty()
        {
            positions.Add("inside", ListPosition.Inside);
            positions.Add("outside", ListPosition.Outside);
        }

        public CSSListStylePositionProperty()
            : base(PropertyNames.ListStylePosition)
        {
            _inherited = true;
            _position = ListPosition.Outside;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            if (value is CSSIdentifierValue)
            {
                var ident = (CSSIdentifierValue)value;
                ListPosition position;

                if (positions.TryGetValue(ident.Value, out position))
                {
                    _position = position;
                    return true;
                }
            }
            else if (value == CSSValue.Inherit)
                return true;

            return false;
        }

        #endregion

        #region Positions

        enum ListPosition
        {
            /// <summary>
            /// The marker box is the first inline box in the principal
            /// block box, after which the element's content flows.
            /// </summary>
            Inside,
            /// <summary>
            /// The marker box is outside the principal block box.
            /// </summary>
            Outside
        }

        #endregion
    }
}
