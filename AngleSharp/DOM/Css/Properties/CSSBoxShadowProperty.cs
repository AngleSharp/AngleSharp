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
            Length offsetX;
            Length offsetY;
            Length blurRadius;
            Length spreadRadius;
            Color color;

            public InsetBoxShadowMode(CSSLengthValue offsetX, CSSLengthValue offsetY, CSSLengthValue blurRadius = null, CSSLengthValue spreadRadius = null, CSSColorValue color = null)
            {
                this.offsetX = offsetX.Length;
                this.offsetY = offsetY.Length;
                this.blurRadius = blurRadius != null ? blurRadius.Length : new Length();
                this.spreadRadius = spreadRadius != null ? spreadRadius.Length : new Length();
                this.color = color != null ? color.Color : Color.Black;
            }
        }

        class NormalBoxShadowMode : BoxShadowMode
        {
            Length offsetX;
            Length offsetY;
            Length blurRadius;
            Length spreadRadius;
            Color color;

            public NormalBoxShadowMode(CSSLengthValue offsetX, CSSLengthValue offsetY, CSSLengthValue blurRadius = null, CSSLengthValue spreadRadius = null, CSSColorValue color = null)
            {
                this.offsetX = offsetX.Length;
                this.offsetY = offsetY.Length;
                this.blurRadius = blurRadius != null ? blurRadius.Length : new Length();
                this.spreadRadius = spreadRadius != null ? spreadRadius.Length : new Length();
                this.color = color != null ? color.Color : Color.Black;
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
