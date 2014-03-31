namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

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
            if (value is CSSIdentifierValue && ((CSSIdentifierValue)value).Value.Equals("none", StringComparison.OrdinalIgnoreCase))
                _mode = _none;
            else if (value is CSSValueList)
            {
                var arguments = (CSSValueList)value;

                if (arguments.Separator == CssValueListSeparator.Comma)
                {
                    var modes = new List<BoxShadowMode>();

                    for (var i = 0; i < arguments.Length; i++)
                    {
                        if (arguments[i] is CSSValueList)
                        {
                            var mode = ParseMode((CSSValueList)arguments[i]);
                            modes.Add(mode);

                            if (mode == null)
                                return false;
                        }
                        else
                            return false;
                    }

                    _mode = new MultiBoxShadowMode(modes);
                }
                else
                {
                    var mode = ParseMode(arguments);

                    if (mode == null)
                        return false;

                    _mode = mode;
                }
            }
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        BoxShadowMode ParseMode(CSSValueList arguments)
        {
            if (arguments.Separator != CssValueListSeparator.Space)
                return null;

            var inset = arguments.Length > 0 && arguments[0] is CSSIdentifierValue && ((CSSIdentifierValue)arguments[0]).Value.Equals("inset", StringComparison.OrdinalIgnoreCase);
            var offset = inset ? 1 : 0;
            var offsetX = arguments.ToLength(offset++);

            if (offsetX == null)
                return null;

            var offsetY = arguments.ToLength(offset++);

            if (offsetY == null)
                return null;

            var blurRadius = arguments.ToLength(offset, false);
            offset += blurRadius != null ? 1 : 0;
            var spreadRadius = arguments.ToLength(offset, false);
            offset += spreadRadius != null ? 1 : 0;
            var color = arguments.ToColor(offset, false);

            if (inset)
                return new InsetBoxShadowMode(offsetX, offsetY, blurRadius, spreadRadius, color);

            return new NormalBoxShadowMode(offsetX, offsetY, blurRadius, spreadRadius, color);
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
            CSSUnitValue.Length offsetX;
            CSSUnitValue.Length offsetY;
            CSSUnitValue.Length blurRadius;
            CSSUnitValue.Length spreadRadius;
            CSSColorValue color;

            public InsetBoxShadowMode(CSSUnitValue.Length offsetX, CSSUnitValue.Length offsetY, CSSUnitValue.Length blurRadius, CSSUnitValue.Length spreadRadius, CSSColorValue color)
            {
                this.offsetX = offsetX;
                this.offsetY = offsetY;
                this.blurRadius = blurRadius;
                this.spreadRadius = spreadRadius;
                this.color = color;
            }
        }

        class NormalBoxShadowMode : BoxShadowMode
        {
            CSSUnitValue.Length offsetX;
            CSSUnitValue.Length offsetY;
            CSSUnitValue.Length blurRadius;
            CSSUnitValue.Length spreadRadius;
            CSSColorValue color;

            public NormalBoxShadowMode(CSSUnitValue.Length offsetX, CSSUnitValue.Length offsetY, CSSUnitValue.Length blurRadius, CSSUnitValue.Length spreadRadius, CSSColorValue color)
            {
                this.offsetX = offsetX;
                this.offsetY = offsetY;
                this.blurRadius = blurRadius;
                this.spreadRadius = spreadRadius;
                this.color = color;
            }
        }

        class MultiBoxShadowMode : BoxShadowMode
        {
            BoxShadowMode top;
            BoxShadowMode right;
            BoxShadowMode bottom;
            BoxShadowMode left;

            public MultiBoxShadowMode(List<BoxShadowMode> modes)
            {
                var count = modes.Count;
                top = modes[0];
                right = modes[1 % count];
                bottom = modes[2 % count];
                left = modes[3 % count];
            }

        }

        #endregion
    }
}
