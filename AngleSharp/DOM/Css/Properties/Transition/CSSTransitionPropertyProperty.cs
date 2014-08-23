namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/transition-property
    /// </summary>
    public sealed class CSSTransitionPropertyProperty : CSSProperty
    {
        #region Fields

        List<String> _properties;
        
        #endregion

        #region ctor

        internal CSSTransitionPropertyProperty()
            : base(PropertyNames.TransitionProperty)
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

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            if (value.Is("none"))
                _properties.Clear();
            else if (value is CSSIdentifierValue)
            {
                _properties.Clear();
                _properties.Add(((CSSIdentifierValue)value).Value);
            }
            else if (value is CSSValueList)
            {
                var values = value.AsList<CSSIdentifierValue>();

                if (values == null)
                    return false;

                _properties.Clear();

                foreach (var ident in values)
                    _properties.Add(ident.Value);
            }
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
