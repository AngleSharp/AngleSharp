namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-direction
    /// </summary>
    public sealed class CSSAnimationDirectionProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, AnimationDirection> modes = new Dictionary<String, AnimationDirection>(StringComparer.OrdinalIgnoreCase);
        List<AnimationDirection> _directions;

        #endregion

        #region ctor

        static CSSAnimationDirectionProperty()
        {
            modes.Add("normal", AnimationDirection.Normal);
            modes.Add("reverse", AnimationDirection.Reverse);
            modes.Add("alternate", AnimationDirection.Alternate);
            modes.Add("alternate-reverse", AnimationDirection.AlternateReverse);
        }

        internal CSSAnimationDirectionProperty()
            : base(PropertyNames.AnimationDirection)
        {
            _inherited = false;
            _directions = new List<AnimationDirection>();
            _directions.Add(AnimationDirection.Normal);
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

        protected override Boolean IsValid(CSSValue value)
        {
            var values = value.AsList<CSSIdentifierValue>();

            if (values != null)
            {
                var fillModes = new List<AnimationDirection>();

                foreach (var item in values)
                {
                    AnimationDirection mode;

                    if (!modes.TryGetValue(item.Value, out mode))
                        return false;

                    fillModes.Add(mode);
                }

                _directions = fillModes;
            }
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
