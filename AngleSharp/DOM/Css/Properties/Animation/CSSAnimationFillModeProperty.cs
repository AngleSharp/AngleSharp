namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-fill-mode
    /// </summary>
    sealed class CSSAnimationFillModeProperty : CSSProperty, ICssAnimationFillModeProperty
    {
        #region Fields

        internal static readonly IValueConverter<AnimationFillStyle> SingleConverter = From(Map.AnimationFillStyles);
        internal static readonly IValueConverter<AnimationFillStyle[]> Converter = TakeList(SingleConverter);
        internal static readonly AnimationFillStyle Default = AnimationFillStyle.None;
        readonly List<AnimationFillStyle> _fillModes;

        #endregion

        #region ctor

        internal CSSAnimationFillModeProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.AnimationFillMode, rule)
        {
            _fillModes = new List<AnimationFillStyle>();
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets an iteration over all defined fill modes.
        /// </summary>
        public IEnumerable<AnimationFillStyle> FillModes
        {
            get { return _fillModes; }
        }

        #endregion

        #region Methods

        public void SetFillModes(IEnumerable<AnimationFillStyle> fillModes)
        {
            _fillModes.Clear();
            _fillModes.AddRange(fillModes);
        }

        internal override void Reset()
        {
            _fillModes.Clear();
            _fillModes.Add(Default);
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetFillModes);
        }

        #endregion
    }
}
