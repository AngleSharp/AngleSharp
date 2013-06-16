using System;

namespace AngleSharp.DOM.Css
{
    abstract class CSSFunction : CSSValue
    {
        private CSSFunction()
        {
        }

        internal static CSSFunction Create(String name, CSSValueList arguments)
        {
            //TODO
            return null;
        }

        class CSSCalcFunction : CSSFunction
        {

        }

        class CSSAttrFunction : CSSFunction
        {

        }

        class CSSRgbFunction : CSSFunction
        {

        }

        class CSSRgbaFunction : CSSFunction
        {

        }

        class CSSHslFunction : CSSFunction
        {

        }

        class CSSToggleFunction : CSSFunction
        {

        }

        class CSSRotateFunction : CSSFunction
        {

        }

        class CSSTransformFunction : CSSFunction
        {

        }

        class CSSSkewFunction : CSSFunction
        {

        }

        class CSSLinearGradientFunction : CSSFunction
        {

        }
    }
}
