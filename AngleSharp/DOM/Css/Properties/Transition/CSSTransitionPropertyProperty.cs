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
            _inherited = false;
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
