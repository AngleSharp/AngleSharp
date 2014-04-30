namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-name
    /// </summary>
    public sealed class CSSAnimationNameProperty : CSSProperty
    {
        #region Fields

        List<String> _names;

        #endregion

        #region ctor

        internal CSSAnimationNameProperty()
            : base(PropertyNames.AnimationName)
        {
            _inherited = false;
            _names = new List<String>();
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

        protected override Boolean IsValid(CSSValue value)
        {
            if (value.Is("none"))
                _names.Clear();
            else if (value is CSSIdentifierValue)
            {
                _names.Clear();
                _names.Add(((CSSIdentifierValue)value).Value);
            }
            else if (value is CSSValueList)
            {
                var values = value.AsList<CSSIdentifierValue>();

                if (values == null)
                    return false;

                _names.Clear();

                foreach (var ident in values)
                    _names.Add(ident.Value);
            }
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
