namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/box-shadow
    /// </summary>
    sealed class CSSBoxShadowProperty : CSSProperty, ICssBoxShadowProperty
    {
        #region Fields

        List<Shadow> _shadows;

        #endregion

        #region ctor

        internal CSSBoxShadowProperty()
            : base(PropertyNames.BoxShadow)
        {
            _shadows = new List<Shadow>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets an enumeration over all the set shadows.
        /// </summary>
        public IEnumerable<Shadow> Shadows
        {
            get { return _shadows; }
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
            if (value.Is(Keywords.None))
                _shadows.Clear();
            else if (value is CSSValueList)
                return Evaluate((CSSValueList)value);
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        Boolean Evaluate(CSSValueList values)
        {
            var shadows = new List<Shadow>();
            var items = values.ToList();
            
            foreach (var item in items)
            {
                var shadow = item.ToShadow();

                if (shadow == null)
                    return false;

                shadows.Add(shadow);
            }

            _shadows = shadows;
            return true;
        }

        #endregion
    }
}
