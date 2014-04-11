namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-repeat
    /// </summary>
    sealed class CSSBackgroundRepeatProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, RepeatMode> _modes = new Dictionary<String, RepeatMode>(StringComparer.OrdinalIgnoreCase);
        List<Repeat> _repeats;

        #endregion

        #region ctor

        static CSSBackgroundRepeatProperty()
        {
            _modes.Add("no-repeat", RepeatMode.NoRepeat);
            _modes.Add("repeat", RepeatMode.Repeat);
            _modes.Add("round", RepeatMode.Round);
            _modes.Add("space", RepeatMode.Space);
        }

        public CSSBackgroundRepeatProperty()
            : base(PropertyNames.BackgroundRepeat)
        {
            _inherited = false;
            _repeats = new List<Repeat>();
            _repeats.Add(new Repeat { Horizontal = RepeatMode.Repeat, Vertical = RepeatMode.Repeat });
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            if (value == CSSValue.Inherit)
                return true;

            var values = value as CSSValueList ?? new CSSValueList(value);
            var repeats = new List<Repeat>();

            for (int i = 0; i < values.Length; i+=2)
            {
                if (values[i] is CSSIdentifierValue == false)
                    return false;

                var ident = ((CSSIdentifierValue)values[i]).Value;
                var repeat = new Repeat();

                if (ident.Equals("repeat-x", StringComparison.OrdinalIgnoreCase))
                {
                    repeat.Horizontal = RepeatMode.Repeat;
                    repeat.Vertical = RepeatMode.NoRepeat;
                }
                else if (ident.Equals("repeat-y", StringComparison.OrdinalIgnoreCase))
                {
                    repeat.Horizontal = RepeatMode.NoRepeat;
                    repeat.Vertical = RepeatMode.Repeat;
                }
                else if (_modes.TryGetValue(ident, out repeat.Horizontal))
                {
                    if (i + 1 < values.Length && values[i + 1] is CSSIdentifierValue && _modes.TryGetValue(((CSSIdentifierValue)values[i + 1]).Value, out repeat.Vertical))
                        i++;
                    else
                        repeat.Vertical = repeat.Horizontal;
                }
                else
                    return false;

                if (i + 1 < values.Length && values[i] != CSSValue.Separator)
                    return false;

                repeats.Add(repeat);
            }

            _repeats = repeats;
            return true;
        }

        #endregion

        #region Repeat Structure

        struct Repeat
        {
            public RepeatMode Horizontal;
            public RepeatMode Vertical;
        }

        #endregion

        #region Repeat Enumeration

        enum RepeatMode : ushort
        {
            /// <summary>
            /// The image is repeated in the given direction as much as needed to cover the whole
            /// background image painting area. The last image may be clipped if the whole thing
            /// won't fit in the remaining area.
            /// </summary>
            Repeat,
            /// <summary>
            /// The image is repeated in the given direction as much as needed to cover most of
            /// the background image painting area, without clipping an image. The remaining
            /// non-covered space is spaced out evenly between the images. The first and last
            /// images touches the edge of the element. The value of the background-position CSS
            /// property is ignored for the concerned direction, except if one single image is
            /// greater than the background image painting area, which is the only case where an
            /// image can be clipped when the space value is used.
            /// </summary>
            Space,
            /// <summary>
            /// The image is repeated in the given direction as much as needed to cover most of
            /// the background image painting area, without clipping an image. If it doesn't cover
            /// exactly the area, the tiles are resized in that direction in order to match it.
            /// </summary>
            Round,
            /// <summary>
            /// The image is not repeated (and hence the background image painting area will not
            /// necessarily been entirely covered). The position of the non-repeated background
            /// image is defined by the background-position CSS property.
            /// </summary>
            NoRepeat
        }

        #endregion
    }
}
