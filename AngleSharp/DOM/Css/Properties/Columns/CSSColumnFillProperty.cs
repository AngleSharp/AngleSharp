namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-fill
    /// </summary>
    public sealed class CSSColumnFillProperty : CSSProperty
    {
        #region Fields

        Boolean _balanced;

        #endregion

        #region ctor

        internal CSSColumnFillProperty()
            : base(PropertyNames.ColumnFill)
        {
            _balanced = true;
            _inherited = false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the columns should be filled uniformly.
        /// </summary>
        public Boolean IsBalanced
        {
            get { return _balanced; }
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
