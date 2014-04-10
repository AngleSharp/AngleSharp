namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/columns
    /// </summary>
    sealed class CSSColumnsProperty : CSSProperty
    {
        #region Fields

        #endregion

        #region ctor

        public CSSColumnsProperty()
            : base(PropertyNames.Columns)
        {
            _inherited = false;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            return base.IsValid(value);
        }

        #endregion
    }
}
