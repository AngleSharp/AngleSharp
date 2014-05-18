namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;   

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-play-state
    /// </summary>
    public sealed class CSSAnimationPlayStateProperty : CSSProperty
    {
        #region Fields

        List<PlayState> _states;

        #endregion

        #region ctor

        internal CSSAnimationPlayStateProperty()
            : base(PropertyNames.AnimationPlayState)
        {
            _inherited = false;
            _states = new List<PlayState>();
            _states.Add(PlayState.Running);
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

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            var values = value.AsList<CSSIdentifierValue>();

            if (values != null)
            {
                var states = new List<PlayState>();

                foreach (var item in values)
                {
                    if (item.Is("running"))
                        states.Add(PlayState.Running);
                    else if (item.Is("paused"))
                        states.Add(PlayState.Paused);
                    else
                        return false;
                }

                _states = states;
            }
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
