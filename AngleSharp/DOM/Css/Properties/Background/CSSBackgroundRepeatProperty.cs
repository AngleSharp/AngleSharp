namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
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

        static readonly Repeat RepeatX = new Repeat { Horizontal = BackgroundRepeat.Repeat, Vertical = BackgroundRepeat.NoRepeat };
        static readonly Repeat RepeatY = new Repeat { Horizontal = BackgroundRepeat.NoRepeat, Vertical = BackgroundRepeat.Repeat };
        static readonly Dictionary<String, BackgroundRepeat> _modes = new Dictionary<String, BackgroundRepeat>(StringComparer.OrdinalIgnoreCase);
        readonly List<Repeat> _repeats;

        #endregion

        #region ctor

        static CSSBackgroundRepeatProperty()
        {
            _modes.Add(Keywords.NoRepeat, BackgroundRepeat.NoRepeat);
            _modes.Add(Keywords.Repeat, BackgroundRepeat.Repeat);
            _modes.Add(Keywords.Round, BackgroundRepeat.Round);
            _modes.Add(Keywords.Space, BackgroundRepeat.Space);
        }

        internal CSSBackgroundRepeatProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.BackgroundRepeat, rule)
        {
            _repeats = new List<Repeat>();
            Reset();
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

        private void SetRepeats(IEnumerable<Repeat> repeats)
        {
            _repeats.Clear();
            _repeats.AddRange(repeats);
        }

        internal override void Reset()
        {
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
            var ModeConverter = this.From(_modes);
            return this.TakeList(ModeConverter.To(m => new Repeat { Horizontal = m, Vertical = m }).Or(
                   this.TakeOne(Keywords.RepeatX, RepeatX)).Or(
                   this.TakeOne(Keywords.RepeatY, RepeatY)).Or(
                   this.WithArgs(
                       ModeConverter, 
                       ModeConverter, 
                       m => new Repeat { Horizontal = m.Item1, Vertical = m.Item2 }))).TryConvert(value, SetRepeats);
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
