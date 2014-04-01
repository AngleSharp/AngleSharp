namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;

    abstract class CSSFunction : CSSValue
    {
        #region Functions

        static readonly Dictionary<String, Func<List<CSSValue>, CSSValue>> _functions;

        static CSSFunction()
        {
            _functions = new Dictionary<String, Func<List<CSSValue>, CSSValue>>(StringComparer.OrdinalIgnoreCase);
            _functions.Add(FunctionNames.Rgb, Rgb);
            _functions.Add(FunctionNames.Rgba, Rgba);
            _functions.Add(FunctionNames.Hsl, Hsl);
            _functions.Add(FunctionNames.Rect, Rect);
            _functions.Add(FunctionNames.Attr, Attr);
            _functions.Add(FunctionNames.Counter, Counter);
            _functions.Add(FunctionNames.Counters, Counters);
        }

        #endregion

        #region Fields

        protected List<CSSValue> _args;

        #endregion

        #region Methods

        public static CSSValue Create(String name, List<CSSValue> arguments)
        {
            CSSValue result;
            Func<List<CSSValue>, CSSValue> creator;

            if (!_functions.TryGetValue(name, out creator) || (result = creator(arguments)) == null)
                return new CSSUnknownFunction(name) { _args = arguments };

            return result;
        }

        #endregion

        #region Creators

        static CSSColorValue Rgb(List<CSSValue> arguments)
        {
            if (arguments.Count == 3 && IsNumber(arguments[0]) && IsNumber(arguments[1]) && IsNumber(arguments[2]))
                return new CSSColorValue(Color.FromRgb(ToByte(arguments[0]), ToByte(arguments[1]), ToByte(arguments[2])));

            return null;
        }

        static CSSColorValue Rgba(List<CSSValue> arguments)
        {
            if (arguments.Count == 4 && IsNumber(arguments[0]) && IsNumber(arguments[1]) && IsNumber(arguments[2]) && IsNumber(arguments[3]))
                return new CSSColorValue(Color.FromRgba(ToByte(arguments[0]), ToByte(arguments[1]), ToByte(arguments[2]), ToSingle(arguments[3])));

            return null;
        }

        static CSSColorValue Hsl(List<CSSValue> arguments)
        {
            if (arguments.Count == 3 && IsNumber(arguments[0]) && IsNumber(arguments[1]) && IsNumber(arguments[2]))
                return new CSSColorValue(Color.FromHsl(ToSingle(arguments[0]), ToSingle(arguments[1]), ToSingle(arguments[2])));

            return null;
        }

        static CSSShapeValue Rect(List<CSSValue> arguments)
        {
            //Required for backwards-compatibility
            if (arguments.Count == 1 && arguments[0] is CSSValueList)
                arguments = ((CSSValueList)arguments[0]).List;

            if (arguments.Count == 4 && IsLength(arguments[0]) && IsLength(arguments[1]) && IsLength(arguments[2]) && IsLength(arguments[3]))
                return new CSSShapeValue(ToLength(arguments[0]), ToLength(arguments[1]), ToLength(arguments[2]), ToLength(arguments[3]));

            return null;
        }

        static CSSAttrValue Attr(List<CSSValue> arguments)
        {
            if (arguments.Count == 1)
            {
                if (arguments[0] is CSSStringValue)
                    return new CSSAttrValue(((CSSStringValue)arguments[0]).Value);
                else if (arguments[0] is CSSIdentifierValue)
                    return new CSSAttrValue(((CSSIdentifierValue)arguments[0]).Value);
            }

            return null;
        }

        static CSSCounter Counter(List<CSSValue> arguments)
        {
            if (arguments.Count > 0 && arguments.Count < 3 && arguments[0] is CSSIdentifierValue)
            {
                var identifier = ((CSSIdentifierValue)arguments[0]).Value;
                var listStyle = "decimal";

                if (arguments.Count > 1)
                {
                    if (arguments[1] is CSSIdentifierValue)
                        listStyle = ((CSSIdentifierValue)arguments[1]).Value;
                    else
                        return null;
                }

                return new CSSCounter(identifier, listStyle, null);
            }

            return null;
        }

        static CSSCounter Counters(List<CSSValue> arguments)
        {
            if (arguments.Count > 1 && arguments.Count < 4 && arguments[0] is CSSIdentifierValue && arguments[1] is CSSStringValue)
            {
                var identifier = ((CSSIdentifierValue)arguments[0]).Value;
                var separator = ((CSSStringValue)arguments[1]).Value;
                var listStyle = "decimal";

                if (arguments.Count > 2)
                {
                    if (arguments[2] is CSSIdentifierValue)
                        listStyle = ((CSSIdentifierValue)arguments[2]).Value;
                    else
                        return null;
                }

                return new CSSCounter(identifier, listStyle, separator);
            }

            return null;
        }

        #endregion

        #region Helpers

        static Boolean IsLength(CSSValue cssValue)
        {
            return cssValue is CSSLengthValue || cssValue == CSSNumberValue.Zero;
        }

        static Length ToLength(CSSValue cssValue)
        {
            if (cssValue is CSSLengthValue)
                return ((CSSLengthValue)cssValue).Length;

            return new Length();
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

        #region Nested // Actually TODO here ...

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

        #endregion
    }
}
