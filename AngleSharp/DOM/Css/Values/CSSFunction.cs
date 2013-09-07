using System;
using System.Collections.Generic;

namespace AngleSharp.DOM.Css
{
    abstract class CSSFunction : CSSValue
    {
        List<CSSValue> _args;

        CSSFunction()
        {
        }

        internal static CSSValue Create(String name, List<CSSValue> arguments)
        {
            switch (name)
            {
                case FunctionNames.RGB:
                {
                    if (arguments.Count == 3)
                    {
                        if (CheckNumber(arguments[0]) && CheckNumber(arguments[1]) && CheckNumber(arguments[2]))
                            return new CSSPrimitiveValue(CSSColor.FromRgb(ToByte(arguments[0]), ToByte(arguments[1]), ToByte(arguments[2])));
                    }

                    break;
                }
                case FunctionNames.RGBA:
                {
                    if (arguments.Count == 4)
                    {
                        if (CheckNumber(arguments[0]) && CheckNumber(arguments[1]) && CheckNumber(arguments[2]) && CheckNumber(arguments[3]))
                            return new CSSPrimitiveValue(CSSColor.FromRgba(ToByte(arguments[0]), ToByte(arguments[1]), ToByte(arguments[2]), ToSingle(arguments[3])));
                    }

                    break;
                }
                case FunctionNames.HSL:
                {
                    if (arguments.Count == 3)
                    {
                        if (CheckNumber(arguments[0]) && CheckNumber(arguments[1]) && CheckNumber(arguments[2]))
                            return new CSSPrimitiveValue(CSSColor.FromHsl(ToSingle(arguments[0]), ToSingle(arguments[1]), ToSingle(arguments[2])));
                    }

                    break;
                }
            }

            return new CSSUnknownFunction { _args = arguments };
        }

        #region Helpers

        static Boolean CheckNumber(CSSValue cssValue)
        {
            return (cssValue.CssValueType == CssValueType.PrimitiveValue && ((CSSPrimitiveValue)cssValue).PrimitiveType == CssUnit.Number);
        }

        static Single ToSingle(CSSValue cssValue)
        {
            var f = ((CSSPrimitiveValue)cssValue).GetFloatValue(CssUnit.Number);
            return f.HasValue ? f.Value : 0f;
        }

        static Byte ToByte(CSSValue cssValue)
        {
            return ToByte(((CSSPrimitiveValue)cssValue).GetFloatValue(CssUnit.Number));
        }

        static Byte ToByte(Single? value)
        {
            if (value.HasValue)
                return (Byte)Math.Min(Math.Max(value.Value, 0), 255);

            return 0;
        }

        #endregion

        class CSSUnknownFunction : CSSFunction
        {
        }

        class CSSCalcFunction : CSSFunction
        {

        }

        class CSSAttrFunction : CSSFunction
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
