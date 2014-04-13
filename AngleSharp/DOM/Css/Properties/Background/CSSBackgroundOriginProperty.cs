namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-origins
    /// </summary>
    sealed class CSSBackgroundOriginProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, Origin> _modes = new Dictionary<String, Origin>(StringComparer.OrdinalIgnoreCase);
        List<Origin> _origins;

        #endregion

        #region ctor

        static CSSBackgroundOriginProperty()
        {
            _modes.Add("border-box", Origin.BorderBox);
            _modes.Add("padding-box", Origin.PaddingBox);
            _modes.Add("content-box", Origin.ContentBox);
        }

        public CSSBackgroundOriginProperty()
            : base(PropertyNames.BackgroundOrigin)
        {
            _inherited = false;
            _origins = new List<Origin>();
            _origins.Add(Origin.PaddingBox);
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            if (value == CSSValue.Inherit)
                return true;

            var values = value as CSSValueList ?? new CSSValueList(value);
            var origins = new List<Origin>();

            for (int i = 0; i < values.Length; i++)
            {
                Origin origin;

                if (values[i] is CSSIdentifierValue && _modes.TryGetValue(((CSSIdentifierValue)values[i]).Value, out origin))
                    origins.Add(origin);
                else
                    return false;

                if (++i < values.Length && values[i] != CSSValue.Separator)
                    return false;
            }

            _origins = origins;
            return true;
        }

        #endregion

        #region Modes

        enum Origin
        {
            /// <summary>
            /// The background extends to the outside edge of the border (but underneath the border in z-ordering).
            /// </summary>
            BorderBox,
            /// <summary>
            /// No background is drawn below the border (background extends to the outside edge of the padding).
            /// </summary>
            PaddingBox,
            /// <summary>
            /// The background is painted within (clipped to) the content box.
            /// </summary>
            ContentBox,
        }

        #endregion
    }
}
