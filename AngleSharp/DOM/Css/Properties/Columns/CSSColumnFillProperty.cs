namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-fill
    /// </summary>
    sealed class CSSColumnFillProperty : CSSProperty
    {
        #region Fields

        Boolean _balanced;

        #endregion

        #region ctor

        public CSSColumnFillProperty()
            : base(PropertyNames.ColumnFill)
        {
            _balanced = true;
            _inherited = false;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            //Is a keyword indicating that columns are filled sequentially.
            if (value.Is("auto"))
                _balanced = false;
            //Is a keyword indicating that content is equally divided between columns.
            else if (value.Is("balance"))
                _balanced = true;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
