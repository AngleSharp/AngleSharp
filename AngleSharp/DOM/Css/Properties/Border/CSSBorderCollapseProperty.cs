namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-collapse
    /// </summary>
    public sealed class CSSBorderCollapseProperty : CSSProperty
    {
        #region Fields

        Boolean _separate;

        #endregion

        #region ctor

        internal CSSBorderCollapseProperty()
            : base(PropertyNames.BorderCollapse, PropertyFlags.Inherited)
        {
            _separate = true;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the use of the separated-border table rendering model.
        /// Otherwise the collapsed-border table rendering model is used.
        /// </summary>
        public Boolean IsSeparated
        {
            get { return _separate; }
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
            if (value.Is("separate"))
                _separate = true;
            else if (value.Is("collapse"))
                _separate = false;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
