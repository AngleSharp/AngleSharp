namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-repeat
    /// </summary>
    public sealed class CSSBackgroundRepeatProperty : CSSProperty
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

        internal CSSBackgroundRepeatProperty()
            : base(PropertyNames.BackgroundRepeat)
        {
            _inherited = false;
            _repeats = new List<Repeat>();
            _repeats.Add(new Repeat { Horizontal = RepeatMode.Repeat, Vertical = RepeatMode.Repeat });
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets an enumeration with the horizontal repeat modes.
        /// </summary>
        public IEnumerable<RepeatMode> HorizontalRepeats
        {
            get { return _repeats.Select(m => m.Horizontal); }
        }

        /// <summary>
        /// Gets an enumeration with the vertical repeat modes.
        /// </summary>
        public IEnumerable<RepeatMode> VerticalRepeats
        {
            get { return _repeats.Select(m => m.Vertical); }
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            if (value == CSSValue.Inherit)
                return true;

            var values = value as CSSValueList ?? new CSSValueList(value);
            var repeats = new List<Repeat>();

            for (int i = 0; i < values.Length; i++)
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

                if (++i < values.Length && values[i] != CSSValue.Separator)
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
    }
}
