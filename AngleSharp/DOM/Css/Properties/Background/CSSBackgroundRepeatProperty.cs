namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// 
    /// </summary>
    sealed class CSSBackgroundRepeatProperty : CSSProperty
    {
        #region Fields

        #endregion

        #region ctor

        public CSSBackgroundRepeatProperty()
            : base(PropertyNames.BackgroundRepeat)
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
