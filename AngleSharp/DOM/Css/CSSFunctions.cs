namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    static class CSSFunctions
    {
        #region Functions

        static readonly Dictionary<String, Func<List<CSSValue>, CSSValue>> _functions;

        static CSSFunctions()
        {
            _functions = new Dictionary<String, Func<List<CSSValue>, CSSValue>>(StringComparer.OrdinalIgnoreCase);
            _functions.Add(FunctionNames.Rgb, Rgb);
            _functions.Add(FunctionNames.Rgba, Rgba);
            _functions.Add(FunctionNames.Hsl, Hsl);
            _functions.Add(FunctionNames.Hsla, Hsla);
            _functions.Add(FunctionNames.Rect, Rect);
            _functions.Add(FunctionNames.Attr, Attr);
            _functions.Add(FunctionNames.LinearGradient, LinearGradient);
            _functions.Add(FunctionNames.RadialGradient, RadialGradient);
            _functions.Add(FunctionNames.RepeatingLinearGradient, RepeatingLinearGradient);
            _functions.Add(FunctionNames.RepeatingRadialGradient, RepeatingRadialGradient);
            _functions.Add(FunctionNames.Image, Image);
            _functions.Add(FunctionNames.Matrix, Matrix);
            _functions.Add(FunctionNames.Matrix3d, Matrix3d);
            _functions.Add(FunctionNames.Translate, Translate);
            _functions.Add(FunctionNames.Translate3d, Translate3d);
            _functions.Add(FunctionNames.TranslateX, TranslateX);
            _functions.Add(FunctionNames.TranslateY, TranslateY);
            _functions.Add(FunctionNames.TranslateZ, TranslateZ);
            _functions.Add(FunctionNames.Scale, Scale);
            _functions.Add(FunctionNames.Scale3d, Scale3d);
            _functions.Add(FunctionNames.ScaleX, ScaleX);
            _functions.Add(FunctionNames.ScaleY, ScaleY);
            _functions.Add(FunctionNames.ScaleZ, ScaleZ);
            _functions.Add(FunctionNames.Rotate, Rotate);
            _functions.Add(FunctionNames.Rotate3d, Rotate3d);
            _functions.Add(FunctionNames.RotateX, RotateX);
            _functions.Add(FunctionNames.RotateY, RotateY);
            _functions.Add(FunctionNames.RotateZ, RotateZ);
            _functions.Add(FunctionNames.Skew, Skew);
            _functions.Add(FunctionNames.SkewX, SkewX);
            _functions.Add(FunctionNames.SkewY, SkewY);
            _functions.Add(FunctionNames.Counter, Counter);
            _functions.Add(FunctionNames.Counters, Counters);
            _functions.Add(FunctionNames.Steps, Steps);
            _functions.Add(FunctionNames.CubicBezier, CubicBezier);
        }

        #endregion

        #region Methods

        public static CSSValue Create(String name, List<CSSValue> arguments)
        {
            CSSValue result;
            Func<List<CSSValue>, CSSValue> creator;

            if (!_functions.TryGetValue(name, out creator) || (result = creator(arguments)) == null)
            {
                var text = String.Format("{0}({1})", name, String.Join(", ", arguments.Select(m => m.CssText)));
                return new CSSUnknownValue(text);
            }

            return result;
        }

        #endregion

        #region Colors

        static CSSPrimitiveValue Rgb(List<CSSValue> arguments)
        {
            Byte? r, g, b;

            if (arguments.Count == 3 && (r = arguments[0].ToByte()).HasValue && (g = arguments[1].ToByte()).HasValue && (b = arguments[2].ToByte()).HasValue)
                return new CSSPrimitiveValue(Color.FromRgb(r.Value, g.Value, b.Value));

            return null;
        }

        static CSSPrimitiveValue Rgba(List<CSSValue> arguments)
        {
            Byte? r, g, b;
            Single? a;

            if (arguments.Count == 4 && (r = arguments[0].ToByte()).HasValue && (g = arguments[1].ToByte()).HasValue && (b = arguments[2].ToByte()).HasValue && (a = arguments[3].ToSingle()).HasValue)
                return new CSSPrimitiveValue(Color.FromRgba(r.Value, g.Value, b.Value, a.Value));

            return null;
        }

        static CSSPrimitiveValue Hsl(List<CSSValue> arguments)
        {
            const Single hnorm = 1f / 360f;

            if (arguments.Count == 3)
            {
                var h = arguments[0].ToSingle();
                var s = arguments[1].ToPercent();
                var l = arguments[2].ToPercent();

                if (h.HasValue && s.HasValue && l.HasValue)
                    return new CSSPrimitiveValue(Color.FromHsl(h.Value * hnorm, s.Value.NormalizedValue, l.Value.NormalizedValue));
            }

            return null;
        }

        static CSSPrimitiveValue Hsla(List<CSSValue> arguments)
        {
            const Single hnorm = 1f / 360f;

            if (arguments.Count == 4)
            {
                var h = arguments[0].ToSingle();
                var s = arguments[1].ToPercent();
                var l = arguments[2].ToPercent();
                var a = arguments[3].ToSingle();

                if (h.HasValue && s.HasValue && l.HasValue && a.HasValue)
                    return new CSSPrimitiveValue(Color.FromHsla(h.Value * hnorm, s.Value.NormalizedValue, l.Value.NormalizedValue, a.Value));
            }

            return null;
        }

        #endregion

        #region Images

        static CSSImageValue LinearGradient(List<CSSValue> arguments)
        {
            return GeneralLinearGradient(arguments, false);
        }

        static CSSImageValue RepeatingLinearGradient(List<CSSValue> arguments)
        {
            return GeneralLinearGradient(arguments, true);
        }

        static CSSImageValue GeneralLinearGradient(List<CSSValue> arguments, Boolean repeating)
        {
            if (arguments.Count > 1)
            {
                var direction = Angle.Zero;
                var angle = arguments[0].ToAngle();
                var offset = 0;

                if (angle.HasValue)
                {
                    direction = angle.Value;
                    offset++;
                }

                var stops = new CSSImageValue.GradientStop[arguments.Count - offset];

                if (stops.Length > 1)
                {
                    var perStop = 100f / (arguments.Count - offset - 1);

                    for (int i = offset, k = 0; i < arguments.Count; i++, k++)
                    {
                        Color? color = null;
                        var location = CSSCalcValue.FromPercent(new Percent(perStop * k));

                        if (arguments[i] is CSSValueList)
                        {
                            var list = (CSSValueList)arguments[i];

                            if (list.Length != 2)
                                return null;

                            color = list[0].ToColor();
                            location = list[1].AsCalc();
                        }
                        else
                            color = arguments[i].ToColor();

                        if (color == null || location == null)
                            return null;

                        stops[k] = new CSSImageValue.GradientStop(color.Value, location);
                    }

                    return CSSImageValue.FromLinearGradient(direction, repeating, stops);
                }
            }

            return null;
        }

        static CSSImageValue RadialGradient(List<CSSValue> arguments)
        {
            return GeneralRadialGradient(arguments, false);
        }

        static CSSImageValue RepeatingRadialGradient(List<CSSValue> arguments)
        {
            return GeneralRadialGradient(arguments, true);
        }

        static CSSImageValue GeneralRadialGradient(List<CSSValue> arguments, Boolean repeating)
        {
            if (arguments.Count > 1)
            {
                //TODO
                //CSSImageValue.FromRadialGradient(CSSCalcValue.Center, CSSCalcValue.Center, repeating);
            }

            return null;
        }

        static CSSImageValue Image(List<CSSValue> arguments)
        {
            if (arguments.Count == 0)
                return null;

            var imageList = new List<Url>();

            foreach (var argument in arguments)
            {
                var uri = argument.ToUri();

                if (uri == null)
                {
                    var s = argument.ToCssString();

                    if (s != null)
                        uri = new Url(s);
                    else
                        return null;
                }

                imageList.Add(uri);
            }

            return CSSImageValue.FromUrls(imageList);
        }

        #endregion

        #region Timing

        static CSSValue Steps(List<CSSValue> arguments)
        {
            if (arguments.Count > 0 && arguments.Count < 3)
            {
                var intervals = arguments[0].ToInteger();
                
                if (intervals.HasValue)
                {
                    if (arguments.Count > 1)
                    {
                        if (arguments[1].Is(Keywords.Start))
                            return new CSSPrimitiveValue(UnitType.Timing, new TransformSteps(intervals.Value, true));
                        else if (arguments[1].Is(Keywords.End))
                            return new CSSPrimitiveValue(UnitType.Timing, new TransformSteps(intervals.Value, false));
                    }
                    else
                        return new CSSPrimitiveValue(UnitType.Timing, new TransformSteps(intervals.Value));
                }
            }

            return null;
        }

        static CSSValue CubicBezier(List<CSSValue> arguments)
        {
            if (arguments.Count == 4)
            {
                var args = new Single[4];

                for (int i = 0; i < arguments.Count; i++)
                {
                    var arg = arguments[i].ToSingle();

                    if (!arg.HasValue)
                        return null;

                    args[i] = arg.Value;
                }

                return new CSSPrimitiveValue(UnitType.Timing, new TransformCubicBezier(args[0], args[1], args[2], args[3]));
            }

            return null;
        }

        #endregion

        #region Misc

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

        static CSSPrimitiveValue Attr(List<CSSValue> arguments)
        {
            if (arguments.Count == 1)
            {
                var primitive = arguments[0] as CSSPrimitiveValue;

                if (primitive != null && (primitive.Unit == UnitType.String || primitive.Unit == UnitType.Ident))
                    return new CSSPrimitiveValue(new CssAttr(primitive.GetString()));
            }

            return null;
        }

        #endregion

        #region Transformation

        static CSSTransformValue Matrix(List<CSSValue> arguments)
        {
            if (arguments.Count == 6)
            {
                var numbers = new Single[6];

                for (var i = 0; i < arguments.Count; i++)
                {
                    var num = arguments[i].ToSingle();

                    if (!num.HasValue)
                        return null;

                    numbers[i] = num.Value;
                }

                return new CSSTransformValue.Matrix(numbers[0], numbers[1], numbers[2], numbers[3], numbers[4], numbers[5]);
            }

            return null;
        }

        static CSSTransformValue Matrix3d(List<CSSValue> arguments)
        {
            if (arguments.Count == 12)
            {
                var numbers = new Single[12];

                for (var i = 0; i < arguments.Count; i++)
                {
                    var num = arguments[i].ToSingle();

                    if (!num.HasValue)
                        return null;

                    numbers[i] = num.Value;
                }

                return new CSSTransformValue.Matrix3D(numbers[0], numbers[1], numbers[2], numbers[3], numbers[4], numbers[5],
                    numbers[6], numbers[7], numbers[8], numbers[9], numbers[10], numbers[11]);
            }

            return null;
        }

        static CSSTransformValue Translate(List<CSSValue> arguments)
        {
            if (arguments.Count == 1)
                arguments.Add(new CSSPrimitiveValue(Length.Zero));

            if (arguments.Count == 2)
            {
                var dx = arguments[0].AsCalc();
                var dy = arguments[1].AsCalc();

                if (dx != null && dy != null)
                    return new CSSTransformValue.Translate(dx, dy);
            }

            return null;
        }

        static CSSTransformValue Translate3d(List<CSSValue> arguments)
        {
            if (arguments.Count == 1)
                arguments.Add(new CSSPrimitiveValue(Length.Zero));

            if (arguments.Count == 2)
                arguments.Add(new CSSPrimitiveValue(Length.Zero));

            if (arguments.Count == 3)
            {
                var dx = arguments[0].AsCalc();
                var dy = arguments[1].AsCalc();
                var dz = arguments[2].AsCalc();

                if (dx != null && dy != null && dz != null)
                    return new CSSTransformValue.Translate3D(dx, dy, dz);
            }

            return null;
        }

        static CSSTransformValue TranslateX(List<CSSValue> arguments)
        {
            if (arguments.Count == 1)
            {
                var dx = arguments[0].AsCalc();

                if (dx != null)
                    return new CSSTransformValue.TranslateX(dx);
            }

            return null;
        }

        static CSSTransformValue TranslateY(List<CSSValue> arguments)
        {
            if (arguments.Count == 1)
            {
                var dy = arguments[0].AsCalc();

                if (dy != null)
                    return new CSSTransformValue.TranslateY(dy);
            }

            return null;
        }

        static CSSTransformValue TranslateZ(List<CSSValue> arguments)
        {
            if (arguments.Count == 1)
            {
                var dz = arguments[0].AsCalc();

                if (dz != null)
                    return new CSSTransformValue.TranslateZ(dz);
            }

            return null;
        }

        static CSSTransformValue Scale(List<CSSValue> arguments)
        {
            if (arguments.Count == 1)
            {
                var scale = arguments[0].ToSingle();

                if (scale.HasValue)
                    return new CSSTransformValue.Scale(scale.Value);
            }
            else if (arguments.Count == 2)
            {
                var sx = arguments[0].ToSingle();
                var sy = arguments[1].ToSingle();

                if (sx.HasValue && sy.HasValue)
                    return new CSSTransformValue.Scale(sx.Value, sy.Value);
            }

            return null;
        }

        static CSSTransformValue Scale3d(List<CSSValue> arguments)
        {
            if (arguments.Count == 2)
                return null;

            if (arguments.Count == 1)
            {
                arguments.Add(arguments[0]);
                arguments.Add(arguments[0]);
            }

            if (arguments.Count == 3)
            {
                var sx = arguments[0].ToSingle();
                var sy = arguments[1].ToSingle();
                var sz = arguments[2].ToSingle();

                if (sx.HasValue && sy.HasValue)
                    return new CSSTransformValue.Scale3D(sx.Value, sy.Value, sz.Value);
            }

            return null;
        }

        static CSSTransformValue ScaleX(List<CSSValue> arguments)
        {
            if (arguments.Count == 1)
            {
                var sx = arguments[0].ToSingle();

                if (sx.HasValue)
                    return new CSSTransformValue.ScaleX(sx.Value);
            }

            return null;
        }

        static CSSTransformValue ScaleY(List<CSSValue> arguments)
        {
            if (arguments.Count == 1)
            {
                var dy = arguments[0].ToSingle();

                if (dy.HasValue)
                    return new CSSTransformValue.ScaleY(dy.Value);
            }

            return null;
        }

        static CSSTransformValue ScaleZ(List<CSSValue> arguments)
        {
            if (arguments.Count == 1)
            {
                var sz = arguments[0].ToSingle();

                if (sz.HasValue)
                    return new CSSTransformValue.ScaleZ(sz.Value);
            }

            return null;
        }

        static CSSTransformValue Rotate(List<CSSValue> arguments)
        {
            if (arguments.Count == 1)
            {
                var angle = arguments[0].ToAngle();

                if (angle.HasValue)
                    return new CSSTransformValue.Rotate(angle.Value);
            }

            return null;
        }

        static CSSTransformValue Rotate3d(List<CSSValue> arguments)
        {
            if (arguments.Count == 4)
            {
                var x = arguments[0].ToSingle();
                var y = arguments[1].ToSingle();
                var z = arguments[2].ToSingle();
                var angle = arguments[3].ToAngle();

                if (x.HasValue && y.HasValue && z.HasValue && angle.HasValue)
                    return new CSSTransformValue.Rotate3D(x.Value, y.Value, z.Value, angle.Value);
            }

            return null;
        }

        static CSSTransformValue RotateX(List<CSSValue> arguments)
        {
            if (arguments.Count == 1)
            {
                var angle = arguments[0].ToAngle();

                if (angle.HasValue)
                    return CSSTransformValue.Rotate3D.RotateX(angle.Value);
            }

            return null;
        }

        static CSSTransformValue RotateY(List<CSSValue> arguments)
        {
            if (arguments.Count == 1)
            {
                var angle = arguments[0].ToAngle();

                if (angle.HasValue)
                    return CSSTransformValue.Rotate3D.RotateY(angle.Value);
            }

            return null;
        }

        static CSSTransformValue RotateZ(List<CSSValue> arguments)
        {
            if (arguments.Count == 1)
            {
                var angle = arguments[0].ToAngle();

                if (angle.HasValue)
                    return CSSTransformValue.Rotate3D.RotateZ(angle.Value);
            }

            return null;
        }

        static CSSCounter Counter(List<CSSValue> arguments)
        {
            if (arguments.Count > 0 && arguments.Count < 3)
            {
                var primitive = arguments[0] as CSSPrimitiveValue;

                if (primitive != null && primitive.Unit == UnitType.Ident)
                {
                    var identifier = primitive.GetString();
                    var listStyle = Keywords.Decimal;

                    if (arguments.Count > 1)
                    {
                        primitive = arguments[1] as CSSPrimitiveValue;

                        if (primitive != null && primitive.Unit == UnitType.Ident)
                            listStyle = primitive.GetString();
                        else
                            return null;
                    }

                    return new CSSCounter(identifier, listStyle, null);
                }
            }

            return null;
        }

        static CSSCounter Counters(List<CSSValue> arguments)
        {
            if (arguments.Count > 1 && arguments.Count < 4)
            {
                var primitive = arguments[0] as CSSPrimitiveValue;

                if (primitive != null && primitive.Unit == UnitType.Ident)
                {
                    var identifier = primitive.GetString();
                    var separator = arguments[1].ToCssString();
                    var listStyle = Keywords.Decimal;

                    if (separator == null)
                        return null;

                    if (arguments.Count > 2)
                    {
                        primitive = arguments[2] as CSSPrimitiveValue;

                        if (primitive != null && primitive.Unit == UnitType.Ident)
                            listStyle = primitive.GetString();
                        else
                            return null;
                    }

                    return new CSSCounter(identifier, listStyle, separator);
                }
            }

            return null;
        }

        static CSSTransformValue Skew(List<CSSValue> arguments)
        {
            if (arguments.Count == 2)
            {
                var alpha = arguments[0].ToAngle();
                var beta = arguments[1].ToAngle();

                if (alpha.HasValue && beta.HasValue)
                    return new CSSTransformValue.Skew(alpha.Value, beta.Value);
            }

            return null;
        }

        static CSSTransformValue SkewX(List<CSSValue> arguments)
        {
            if (arguments.Count == 1)
            {
                var angle = arguments[0].ToAngle();

                if (angle.HasValue)
                    return new CSSTransformValue.SkewX(angle.Value);
            }

            return null;
        }

        static CSSTransformValue SkewY(List<CSSValue> arguments)
        {
            if (arguments.Count == 1)
            {
                var angle = arguments[0].ToAngle();

                if (angle.HasValue)
                    return new CSSTransformValue.SkewY(angle.Value);
            }

            return null;
        }

        #endregion
    }
}
