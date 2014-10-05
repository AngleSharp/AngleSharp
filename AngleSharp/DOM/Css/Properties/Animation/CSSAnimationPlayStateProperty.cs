namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;   

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-play-state
    /// </summary>
    sealed class CSSAnimationPlayStateProperty : CSSProperty, ICssAnimationPlayStateProperty
    {
        #region Fields

        List<PlayState> _states;

        #endregion

        #region ctor

        internal CSSAnimationPlayStateProperty()
            : base(PropertyNames.AnimationPlayState)
        {
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

        protected override void Reset()
        {
            if (_states == null)
                _states = new List<PlayState>();
            else
                _states.Clear();

            _states.Add(PlayState.Running);
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            var values = value.AsList<CSSPrimitiveValue>();

            if (values != null)
            {
                var states = new List<PlayState>();

                foreach (var item in values)
                {
                    if (item.Is(Keywords.Running))
                        states.Add(PlayState.Running);
                    else if (item.Is(Keywords.Paused))
                        states.Add(PlayState.Paused);
                    else
                        return false;
                }

                _states = states;
                return true;
            }
            
            return false;
        }

        #endregion
    }
}
