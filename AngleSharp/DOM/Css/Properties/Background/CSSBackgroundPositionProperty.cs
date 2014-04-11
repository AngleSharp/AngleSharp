namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// 
    /// </summary>
    sealed class CSSBackgroundPositionProperty : CSSProperty
    {
        #region Fields

        #endregion

        #region ctor

        public CSSBackgroundPositionProperty()
            : base(PropertyNames.BackgroundPosition)
        {
            _inherited = false;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            //TODO
            return base.IsValid(value);
        }

        #endregion
    }
}
