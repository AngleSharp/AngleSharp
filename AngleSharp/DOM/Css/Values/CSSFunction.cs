namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;

    abstract class CSSFunction : CSSValue
    {
        protected List<CSSValue> _args;

		internal CSSFunction()
        {
        }

        internal static CSSValue Create(String name, List<CSSValue> arguments)
        {
            if (name == FunctionNames.Rgb && arguments.Count == 3)
            {
                if (IsNumber(arguments[0]) && IsNumber(arguments[1]) && IsNumber(arguments[2]))
                    return new CSSColorValue(Color.FromRgb(ToByte(arguments[0]), ToByte(arguments[1]), ToByte(arguments[2])));
            }
            else if (name == FunctionNames.Rgba && arguments.Count == 4)
            {
                if (IsNumber(arguments[0]) && IsNumber(arguments[1]) && IsNumber(arguments[2]) && IsNumber(arguments[3]))
                    return new CSSColorValue(Color.FromRgba(ToByte(arguments[0]), ToByte(arguments[1]), ToByte(arguments[2]), ToSingle(arguments[3])));
            }
            else if (name == FunctionNames.Hsl && arguments.Count == 3)
            {
                if (IsNumber(arguments[0]) && IsNumber(arguments[1]) && IsNumber(arguments[2]))
                    return new CSSColorValue(Color.FromHsl(ToSingle(arguments[0]), ToSingle(arguments[1]), ToSingle(arguments[2])));
            }
            else if (name == FunctionNames.Rect && arguments.Count == 4)
            {
                if (IsLength(arguments[0]) && IsLength(arguments[1]) && IsLength(arguments[2]) && IsLength(arguments[3]))
                    return new CSSShapeValue(ToLength(arguments[0]), ToLength(arguments[1]), ToLength(arguments[2]), ToLength(arguments[3]));
            }
            else if (name == FunctionNames.Attr && arguments.Count == 1)
            {
                if (arguments[0] is CSSStringValue)
                    return new CSSAttrValue(((CSSStringValue)arguments[0]).Value);
                else if (arguments[0] is CSSIdentifierValue)
                    return new CSSAttrValue(((CSSIdentifierValue)arguments[0]).Value);
            }

            return new CSSUnknownFunction(name) { _args = arguments };
        }

        #region Helpers

        static Boolean IsLength(CSSValue cssValue)
        {
            return cssValue is CSSUnitValue.Length || cssValue == CSSNumberValue.Zero;
        }

        static CSSUnitValue.Length ToLength(CSSValue cssValue)
        {
            if (cssValue is CSSUnitValue.Length)
                return (CSSUnitValue.Length)cssValue;

            return new CSSUnitValue.Length(0f, CssUnit.Px);
        }

        static Boolean IsNumber(CSSValue cssValue)
        {
            return cssValue is CSSNumberValue;
        }

        static Single ToSingle(CSSValue cssValue)
        {
            return ((CSSNumberValue)cssValue).Value;
        }

        static Byte ToByte(CSSValue cssValue)
        {
            return (Byte)Math.Min(Math.Max(((CSSNumberValue)cssValue).Value, 0), 255);
        }

        #endregion

        class CSSUnknownFunction : CSSFunction
        {
            public CSSUnknownFunction(String name)
            {
                _text = name;
            }

            public override String ToCss()
            {
                var sb = Pool.NewStringBuilder().Append(_text);
                sb.Append(Specification.RBO);

                for (int i = 0; i < _args.Count; i++)
                {
                    sb.Append(_args[i].ToCss());

                    if (i != _args.Count - 1)
                        sb.Append(Specification.COMMA).Append(Specification.SPACE);
                }
                
                sb.Append(Specification.RBC);
                return sb.ToPool();
            }
        }

        class CSSCalcFunction : CSSFunction
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
    }
}
