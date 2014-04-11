namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// 
    /// </summary>
    sealed class CSSBackgroundProperty : CSSProperty
    {
        #region Fields

        #endregion

        #region ctor

        public CSSBackgroundProperty()
            : base(PropertyNames.Background)
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
