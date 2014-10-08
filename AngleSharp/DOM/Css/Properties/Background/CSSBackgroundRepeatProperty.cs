namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-repeat
    /// </summary>
    sealed class CSSBackgroundRepeatProperty : CSSProperty, ICssBackgroundRepeatProperty
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

        protected override void Reset()
        {
            if (_repeats == null)
                _repeats = new List<Repeat>();
            else
                _repeats.Clear();

            _repeats.Add(new Repeat { Horizontal = BackgroundRepeat.Repeat, Vertical = BackgroundRepeat.Repeat });
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            var values = value as CSSValueList ?? new CSSValueList(value);
            var repeats = new List<Repeat>();

            for (int i = 0; i < values.Length; i++)
            {
                var primitive = values[i] as CSSPrimitiveValue;

                if (primitive == null || primitive.Unit != UnitType.Ident)
                    return false;

                var ident = primitive.GetString();
                var repeat = new Repeat();

                if (ident.Equals(Keywords.RepeatX, StringComparison.OrdinalIgnoreCase))
                {
                    repeat.Horizontal = BackgroundRepeat.Repeat;
                    repeat.Vertical = BackgroundRepeat.NoRepeat;
                }
                else if (ident.Equals(Keywords.RepeatY, StringComparison.OrdinalIgnoreCase))
                {
                    repeat.Horizontal = BackgroundRepeat.NoRepeat;
                    repeat.Vertical = BackgroundRepeat.Repeat;
                }
                else if (_modes.TryGetValue(ident, out repeat.Horizontal))
                {
                    if (i + 1 < values.Length && _modes.TryGetValue(values[i + 1], out repeat.Vertical))
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
