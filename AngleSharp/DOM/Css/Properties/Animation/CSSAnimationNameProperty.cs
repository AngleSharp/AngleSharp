namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-name
    /// </summary>
    sealed class CSSAnimationNameProperty : CSSProperty, ICssAnimationNameProperty
    {
        #region Fields

        List<String> _names;

        #endregion

        #region ctor

        internal CSSAnimationNameProperty()
            : base(PropertyNames.AnimationName)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the names of the animations to trigger.
        /// </summary>
        public IEnumerable<String> Names
        {
            get { return _names; }
        }

        #endregion

        #region Methods

        protected override void Reset()
        {
            if (_names == null)
                _names = new List<String>();
            else
                _names.Clear();
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            if (value.Is(Keywords.None))
                _names.Clear();
            else if (value is CSSPrimitiveValue)
            {
                var primitive = (CSSPrimitiveValue)value;

                if (primitive.Unit != UnitType.Ident)
                    return false;

                _names.Clear();
                _names.Add(primitive.GetString());
            }
            else if (value is CSSValueList)
            {
                var values = value.AsList<CSSPrimitiveValue>();

                if (values == null)
                    return false;

                _names.Clear();

                foreach (var ident in values)
                    _names.Add(ident.GetString());
            }
            else
                return false;

            return true;
        }

        #endregion
    }
}
