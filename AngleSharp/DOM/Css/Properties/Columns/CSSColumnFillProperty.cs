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
            if (value is CSSIdentifierValue)
            {
                var ident = ((CSSIdentifierValue)value).Value;

                //Is a keyword indicating that columns are filled sequentially.
                if (ident.Equals("auto", StringComparison.OrdinalIgnoreCase))
                    _balanced = false;
                //Is a keyword indicating that content is equally divided between columns.
                else if (ident.Equals("balance", StringComparison.OrdinalIgnoreCase))
                    _balanced = true;
                else
                    return false;
            }
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
