namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/box-shadow
    /// </summary>
    sealed class CSSBoxShadowProperty : CSSProperty
    {
        #region Fields

        static readonly NoneBoxShadowMode _none = new NoneBoxShadowMode();
        BoxShadowMode _mode;

        #endregion

        #region ctor

        public CSSBoxShadowProperty()
            : base(PropertyNames.BOX_SHADOW)
        {
            _mode = _none;
            _inherited = false;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            if (value is CSSIdentifier && ((CSSIdentifier)value).Identifier.Equals("none", StringComparison.OrdinalIgnoreCase))
            {
                _mode = _none;
                return true;
            }
            else if (value is CSSValueList)
            {
                var arguments = (CSSValueList)value;

                if (arguments.Separator == CssValueListSeparator.Space && arguments.Length > 1)
                {

                    return true;
                }
            }
            else if (value == CSSValue.Inherit)
                return true;

            return false;
        }

        #endregion

        #region Modes

        abstract class BoxShadowMode
        {
            //TODO Add members that make sense
        }

        class NoneBoxShadowMode : BoxShadowMode
        {
        }

        class InsetBoxShadowMode : BoxShadowMode
        {
        }

        class NormalBoxShadowMode : BoxShadowMode
        {

        }

        #endregion
    }
}
