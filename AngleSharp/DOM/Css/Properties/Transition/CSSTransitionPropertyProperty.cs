namespace AngleSharp.DOM.Css
{
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/transition-property
    /// </summary>
    sealed class CSSTransitionPropertyProperty : CSSProperty, ICssTransitionPropertyProperty
    {
        #region Fields

        readonly List<String> _properties;
        
        #endregion

        #region ctor

        internal CSSTransitionPropertyProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.TransitionProperty, rule)
        {
            _properties = new List<String>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the names of the selected properties.
        /// </summary>
        public IEnumerable<String> Properties
        {
            get { return _properties; }
        }

        #endregion

        #region Methods

        internal override void Reset()
        {
            _properties.Clear();
            _properties.Add(Keywords.All);
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            if (value.Is(Keywords.None))
                _properties.Clear();
            else if (value is CSSPrimitiveValue)
            {
                var property = value.ToAnimatableIdentifier();

                if (property == null)
                    return false;

                _properties.Clear();
                _properties.Add(property);
            }
            else if (value is CSSValueList)
            {
                var properties = value.AsList(ValueExtensions.ToAnimatableIdentifier);

                if (properties == null)
                    return false;

                _properties.Clear();
                _properties.AddRange(properties);
            }
            else 
                return false;

            return true;
        }

        #endregion
    }
}
