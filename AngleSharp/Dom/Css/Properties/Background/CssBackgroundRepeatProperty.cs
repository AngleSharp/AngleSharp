namespace AngleSharp.Dom.Css
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
    sealed class CssBackgroundRepeatProperty : CssProperty
    {
        #region Fields

        internal static readonly Repeat Default = new Repeat { Horizontal = BackgroundRepeat.Repeat, Vertical = BackgroundRepeat.Repeat };
        internal static readonly IValueConverter<Repeat> SingleConverter = Map.BackgroundRepeats.ToConverter().To(m => new Repeat { Horizontal = m, Vertical = m }).Or(
            Keywords.RepeatX, new Repeat { Horizontal = BackgroundRepeat.Repeat, Vertical = BackgroundRepeat.NoRepeat }).Or(
            Keywords.RepeatY, new Repeat { Horizontal = BackgroundRepeat.NoRepeat, Vertical = BackgroundRepeat.Repeat }).Or(
            Converters.WithOrder(Map.BackgroundRepeats.ToConverter().Required(), Map.BackgroundRepeats.ToConverter().Required()).To(m => new Repeat { Horizontal = m.Item1, Vertical = m.Item2 }));
        internal static readonly IValueConverter<Repeat[]> Converter = SingleConverter.FromList();
        readonly List<Repeat> _repeats;

        #endregion

        #region ctor

        internal CssBackgroundRepeatProperty(CssStyleDeclaration rule)
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
            _repeats.Add(Default);
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetRepeats);
        }

        #endregion

        #region Structure

        internal struct Repeat
        {
            public BackgroundRepeat Horizontal;
            public BackgroundRepeat Vertical;
        }

        #endregion
    }
}
