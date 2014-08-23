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

        static readonly Dictionary<String, BackgroundRepeat> _modes = new Dictionary<String, BackgroundRepeat>(StringComparer.OrdinalIgnoreCase);
        List<Repeat> _repeats;

        #endregion

        #region ctor

        static CSSBackgroundRepeatProperty()
        {
            _modes.Add(Keywords.NoRepeat, BackgroundRepeat.NoRepeat);
            _modes.Add(Keywords.Repeat, BackgroundRepeat.Repeat);
            _modes.Add(Keywords.Round, BackgroundRepeat.Round);
            _modes.Add(Keywords.Space, BackgroundRepeat.Space);
        }

        internal CSSBackgroundRepeatProperty()
            : base(PropertyNames.BackgroundRepeat)
        {
            _repeats = new List<Repeat>();
            _repeats.Add(new Repeat { Horizontal = BackgroundRepeat.Repeat, Vertical = BackgroundRepeat.Repeat });
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets an enumeration with the horizontal repeat modes.
        /// </summary>
        public IEnumerable<BackgroundRepeat> HorizontalRepeats
        {
            get { return _repeats.Select(m => m.Horizontal); }
        }

        /// <summary>
        /// Gets an enumeration with the vertical repeat modes.
        /// </summary>
        public IEnumerable<BackgroundRepeat> VerticalRepeats
        {
            get { return _repeats.Select(m => m.Vertical); }
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
                    repeat.Horizontal = BackgroundRepeat.Repeat;
                    repeat.Vertical = BackgroundRepeat.NoRepeat;
                }
                else if (ident.Equals("repeat-y", StringComparison.OrdinalIgnoreCase))
                {
                    repeat.Horizontal = BackgroundRepeat.NoRepeat;
                    repeat.Vertical = BackgroundRepeat.Repeat;
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
            public BackgroundRepeat Horizontal;
            public BackgroundRepeat Vertical;
        }

        #endregion
    }
}
