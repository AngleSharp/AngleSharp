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
            _functions.Add(FunctionNames.Hsla, Hsla);
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
            Byte? r, g, b;

            if (arguments.Count == 3 && (r = arguments[0].ToByte()).HasValue && (g = arguments[1].ToByte()).HasValue && (b = arguments[2].ToByte()).HasValue)
                return new CSSColorValue(Color.FromRgb(r.Value, g.Value, b.Value));

            return null;
        }

        static CSSColorValue Rgba(List<CSSValue> arguments)
        {
            Byte? r, g, b;
            Single? a;

            if (arguments.Count == 4 && (r = arguments[0].ToByte()).HasValue && (g = arguments[1].ToByte()).HasValue && (b = arguments[2].ToByte()).HasValue && (a = arguments[3].ToNumber()).HasValue)
                return new CSSColorValue(Color.FromRgba(r.Value, g.Value, b.Value, a.Value));

            return null;
        }

        static CSSColorValue Hsl(List<CSSValue> arguments)
        {
            const Single hnorm = 1f / 360f;
            Single? h;

            if (arguments.Count == 3 && (h = arguments[0].ToNumber()).HasValue && arguments[1] is CSSPercentValue && arguments[2] is CSSPercentValue)
                return new CSSColorValue(Color.FromHsl(h.Value * hnorm, ((CSSPercentValue)arguments[1]).Value, ((CSSPercentValue)arguments[2]).Value));

            return null;
        }

        static CSSColorValue Hsla(List<CSSValue> arguments)
        {
            const Single hnorm = 1f / 360f;
            Single? h, a;

            if (arguments.Count == 4 && (h = arguments[0].ToNumber()).HasValue && arguments[1] is CSSPercentValue && arguments[2] is CSSPercentValue && (a = arguments[3].ToNumber()).HasValue)
                return new CSSColorValue(Color.FromHsla(h.Value * hnorm, ((CSSPercentValue)arguments[1]).Value, ((CSSPercentValue)arguments[2]).Value, a.Value));

            return null;
        }

        static CSSShapeValue Rect(List<CSSValue> arguments)
        {
            Length? top, right, bottom, left;

            //Required for backwards-compatibility
            if (arguments.Count == 1 && arguments[0] is CSSValueList) 
                arguments = new List<CSSValue>((CSSValueList)arguments[0]);

            if (arguments.Count == 4 && (top = arguments[0].ToLength()).HasValue && (right = arguments[1].ToLength()).HasValue && (bottom = arguments[2].ToLength()).HasValue && (left = arguments[3].ToLength()).HasValue)
                return new CSSShapeValue(top.Value, right.Value, bottom.Value, left.Value);

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
