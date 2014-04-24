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

        static readonly Dictionary<String, RepeatMode> _modes = new Dictionary<String, RepeatMode>(StringComparer.OrdinalIgnoreCase);
        RepeatMode _horizontal;
        RepeatMode _vertical;

        #endregion

        #region ctor

        static CSSBorderImageRepeatProperty()
        {
            _modes.Add("stretch", RepeatMode.Stretch);
            _modes.Add("repeat", RepeatMode.Repeat);
            _modes.Add("round", RepeatMode.Round);
        }

        internal CSSBorderImageRepeatProperty()
            : base(PropertyNames.BorderImageRepeat)
        {
            _inherited = false;
            _horizontal = RepeatMode.Stretch;
            _vertical = RepeatMode.Stretch;
        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            RepeatMode mode;

            if (value is CSSIdentifierValue && _modes.TryGetValue(((CSSIdentifierValue)value).Value, out mode))
                _horizontal = _vertical = mode;
            else if (value is CSSValueList)
            {
                var list = (CSSValueList)value;
                var modes = new RepeatMode[2];

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

        #region Repeat Enumeration

        enum RepeatMode : ushort
        {
            /// <summary>
            /// Keyword indicating that the image must be stretched to fill
            /// the gap between the two borders.
            /// </summary>
            Stretch,
            /// <summary>
            /// Keyword indicating that the image must be repeated until it
            /// fills the gap between the two borders.
            /// </summary>
            Repeat,
            /// <summary>
            /// Keyword indicating that the image must be repeated until it
            /// fills the gap between the two borders. If the image doesn't fit
            /// after being repeated an integral number of times, the image is
            /// rescaled to fit.
            /// </summary>
            Round
        }

        #endregion
    }
}
