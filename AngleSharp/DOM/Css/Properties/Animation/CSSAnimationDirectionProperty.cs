namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-direction
    /// </summary>
    sealed class CSSAnimationDirectionProperty : CSSProperty, ICssAnimationDirectionProperty
    {
        #region Fields

        List<AnimationDirection> _directions;

        #endregion

        #region ctor

        internal CSSAnimationDirectionProperty()
            : base(PropertyNames.AnimationDirection)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets an iteration over all defined directions.
        /// </summary>
        public IEnumerable<AnimationDirection> Directions
        {
            get { return _directions; }
        }

        #endregion

        #region Methods

        internal override void Reset()
        {
            if (_directions == null)
                _directions = new List<AnimationDirection>();
            else
                _directions.Clear();

            _directions.Add(AnimationDirection.Normal);
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
                var fillModes = new List<AnimationDirection>();

                foreach (var item in values)
                {
                    var direction = item.ToDirection();

                    if (direction == null)
                        return false;

                    fillModes.Add(direction.Value);
                }

                _directions = fillModes;
                return true;
            }
            
            return false;
        }

        #endregion
    }
}
