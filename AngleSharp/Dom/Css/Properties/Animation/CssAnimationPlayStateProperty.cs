namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;   

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-play-state
    /// </summary>
    sealed class CssAnimationPlayStateProperty : CssProperty
    {
        #region Fields

        internal static readonly IValueConverter<PlayState> SingleConverter = Converters.Assign(Keywords.Running, PlayState.Running).Or(Keywords.Paused, PlayState.Paused);
        internal static readonly IValueConverter<PlayState[]> Converter = SingleConverter.FromList();
        internal static readonly PlayState Default = PlayState.Running;
        readonly List<PlayState> _states;

        #endregion

        #region ctor

        internal CssAnimationPlayStateProperty(CssStyleDeclaration rule)
            : base(PropertyNames.AnimationPlayState, rule)
        {
            _states = new List<PlayState>();
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets an enumerable over the defined play states.
        /// </summary>
        public IEnumerable<PlayState> States
        {
            get { return _states; }
        }

        #endregion

        #region Methods

        public void SetStates(IEnumerable<PlayState> states)
        {
            _states.Clear();
            _states.AddRange(states);
        }

        internal override void Reset()
        {
            _states.Clear();
            _states.Add(Default);
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetStates);
        }

        #endregion
    }
}
