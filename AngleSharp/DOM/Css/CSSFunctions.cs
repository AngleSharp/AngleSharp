namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
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

        static CSSPrimitiveValue LinearGradient(List<CSSValue> arguments)
        {
            return GeneralLinearGradient(arguments, false);
        }

        static CSSPrimitiveValue RepeatingLinearGradient(List<CSSValue> arguments)
        {
            return GeneralLinearGradient(arguments, true);
        }

        static CSSPrimitiveValue GeneralLinearGradient(List<CSSValue> arguments, Boolean repeating)
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

                var stops = new GradientStop[arguments.Count - offset];

                if (stops.Length > 1)
                {
                    var perStop = 100f / (arguments.Count - offset - 1);

                    for (int i = offset, k = 0; i < arguments.Count; i++, k++)
                    {
                        Color? color = null;
                        Percent? location = new Percent(perStop * k);//TODO allow Length

                        if (arguments[i] is CSSValueList)
                        {
                            var list = (CSSValueList)arguments[i];

                            if (list.Length != 2)
                                return null;

                            color = list[0].ToColor();
                            location = list[1].ToPercent();
                        }
                        else
                            color = arguments[i].ToColor();

                        if (color == null || location == null)
                            return null;

                        stops[k] = new GradientStop(color.Value, location.Value);
                    }

                    return new CSSPrimitiveValue(UnitType.Gradient, new LinearGradient(direction, stops, repeating));
                }
            }

            return null;
        }

        static CSSPrimitiveValue RadialGradient(List<CSSValue> arguments)
        {
            return GeneralRadialGradient(arguments, false);
        }

        static CSSPrimitiveValue RepeatingRadialGradient(List<CSSValue> arguments)
        {
            return GeneralRadialGradient(arguments, true);
        }

        static CSSPrimitiveValue GeneralRadialGradient(List<CSSValue> arguments, Boolean repeating)
        {
            if (arguments.Count > 1)
            {
                //TODO
                //CSSImageValue.FromRadialGradient(CSSCalcValue.Center, CSSCalcValue.Center, repeating);
            }

            return null;
        }

        static CSSPrimitiveValue Image(List<CSSValue> arguments)
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

            return new CSSPrimitiveValue(UnitType.ImageList, new CssImages(imageList));
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
                            return new CSSPrimitiveValue(UnitType.Transition, new StepsTransitionFunction(intervals.Value, true));
                        else if (arguments[1].Is(Keywords.End))
                            return new CSSPrimitiveValue(UnitType.Transition, new StepsTransitionFunction(intervals.Value, false));
                    }
                    else
                        return new CSSPrimitiveValue(UnitType.Transition, new StepsTransitionFunction(intervals.Value));
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

                return new CSSPrimitiveValue(UnitType.Transition, new CubicBezierTransitionFunction(args[0], args[1], args[2], args[3]));
            }

            return null;
        }

        #endregion

        #region Misc

        static CSSPrimitiveValue Rect(List<CSSValue> arguments)
        {
            Length? top, right, bottom, left;

            //Required for backwards-compatibility
            if (arguments.Count == 1 && arguments[0] is CSSValueList)
                arguments = new List<CSSValue>((CSSValueList)arguments[0]);

            if (arguments.Count == 4 && (top = arguments[0].ToLength()).HasValue && (right = arguments[1].ToLength()).HasValue && (bottom = arguments[2].ToLength()).HasValue && (left = arguments[3].ToLength()).HasValue)
                return new CSSPrimitiveValue(UnitType.Rect, new Shape(top.Value, right.Value, bottom.Value, left.Value));

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

        #region Counter

        static CSSPrimitiveValue Counter(List<CSSValue> arguments)
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

                    return new CSSPrimitiveValue(UnitType.Counter, new Counter(identifier, listStyle, null));
                }
            }

            return null;
        }

        static CSSPrimitiveValue Counters(List<CSSValue> arguments)
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

                    return new CSSPrimitiveValue(UnitType.Counter, new Counter(identifier, listStyle, separator));
                }
            }

            return null;
        }

        #endregion

        #region Transformation Functions

        static CSSPrimitiveValue Matrix(List<CSSValue> arguments)
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

                return new CSSPrimitiveValue(UnitType.Transform, 
                    new MatrixTransform(numbers[0], numbers[1], numbers[2], numbers[3], numbers[4], numbers[5]));
            }

            return null;
        }

        static CSSPrimitiveValue Matrix3d(List<CSSValue> arguments)
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

                return new CSSPrimitiveValue(UnitType.Transform, 
                    new Matrix3DTransform(numbers[0], numbers[1], numbers[2], numbers[3], numbers[4], numbers[5],
                        numbers[6], numbers[7], numbers[8], numbers[9], numbers[10], numbers[11]));
            }

            return null;
        }

        static CSSPrimitiveValue Translate(List<CSSValue> arguments)
        {
            if (arguments.Count == 1)
                arguments.Add(new CSSPrimitiveValue(Length.Zero));

            if (arguments.Count == 2)
            {
                var dx = arguments[0].ToDistance();
                var dy = arguments[1].ToDistance();

                if (dx != null && dy != null)
                    return new CSSPrimitiveValue(UnitType.Transform, 
                        new TranslateTransform(dx, dy));
            }

            return null;
        }

        static CSSPrimitiveValue Translate3d(List<CSSValue> arguments)
        {
            if (arguments.Count == 1)
                arguments.Add(new CSSPrimitiveValue(Length.Zero));

            if (arguments.Count == 2)
                arguments.Add(new CSSPrimitiveValue(Length.Zero));

            if (arguments.Count == 3)
            {
                var dx = arguments[0].ToDistance();
                var dy = arguments[1].ToDistance();
                var dz = arguments[2].ToDistance();

                if (dx != null && dy != null && dz != null)
                    return new CSSPrimitiveValue(UnitType.Transform, 
                        new Translate3DTransform(dx, dy, dz));
            }

            return null;
        }

        static CSSPrimitiveValue TranslateX(List<CSSValue> arguments)
        {
            if (arguments.Count == 1)
            {
                var dx = arguments[0].ToDistance();

                if (dx != null)
                    return new CSSPrimitiveValue(UnitType.Transform, 
                        new TranslateXTransform(dx));
            }

            return null;
        }

        static CSSPrimitiveValue TranslateY(List<CSSValue> arguments)
        {
            if (arguments.Count == 1)
            {
                var dy = arguments[0].ToDistance();

                if (dy != null)
                    return new CSSPrimitiveValue(UnitType.Transform, 
                        new TranslateYTransform(dy));
            }

            return null;
        }

        static CSSPrimitiveValue TranslateZ(List<CSSValue> arguments)
        {
            if (arguments.Count == 1)
            {
                var dz = arguments[0].ToDistance();

                if (dz != null)
                    return new CSSPrimitiveValue(UnitType.Transform, 
                        new TranslateZTransform(dz));
            }

            return null;
        }

        static CSSPrimitiveValue Scale(List<CSSValue> arguments)
        {
            if (arguments.Count == 1)
            {
                var scale = arguments[0].ToSingle();

                if (scale.HasValue)
                    return new CSSPrimitiveValue(UnitType.Transform, 
                        new ScaleTransform(scale.Value));
            }
            else if (arguments.Count == 2)
            {
                var sx = arguments[0].ToSingle();
                var sy = arguments[1].ToSingle();

                if (sx.HasValue && sy.HasValue)
                    return new CSSPrimitiveValue(UnitType.Transform, 
                        new ScaleTransform(sx.Value, sy.Value));
            }

            return null;
        }

        static CSSPrimitiveValue Scale3d(List<CSSValue> arguments)
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
                    return new CSSPrimitiveValue(UnitType.Transform, 
                        new Scale3DTransform(sx.Value, sy.Value, sz.Value));
            }

            return null;
        }

        static CSSPrimitiveValue ScaleX(List<CSSValue> arguments)
        {
            if (arguments.Count == 1)
            {
                var sx = arguments[0].ToSingle();

                if (sx.HasValue)
                    return new CSSPrimitiveValue(UnitType.Transform, 
                        new ScaleXTransform(sx.Value));
            }

            return null;
        }

        static CSSPrimitiveValue ScaleY(List<CSSValue> arguments)
        {
            if (arguments.Count == 1)
            {
                var dy = arguments[0].ToSingle();

                if (dy.HasValue)
                    return new CSSPrimitiveValue(UnitType.Transform, 
                        new ScaleYTransform(dy.Value));
            }

            return null;
        }

        static CSSPrimitiveValue ScaleZ(List<CSSValue> arguments)
        {
            if (arguments.Count == 1)
            {
                var sz = arguments[0].ToSingle();

                if (sz.HasValue)
                    return new CSSPrimitiveValue(UnitType.Transform, 
                        new ScaleZTransform(sz.Value));
            }

            return null;
        }

        static CSSPrimitiveValue Rotate(List<CSSValue> arguments)
        {
            if (arguments.Count == 1)
            {
                var angle = arguments[0].ToAngle();

                if (angle.HasValue)
                    return new CSSPrimitiveValue(UnitType.Transform, 
                        new RotateTransform(angle.Value));
            }

            return null;
        }

        static CSSPrimitiveValue Rotate3d(List<CSSValue> arguments)
        {
            if (arguments.Count == 4)
            {
                var x = arguments[0].ToSingle();
                var y = arguments[1].ToSingle();
                var z = arguments[2].ToSingle();
                var angle = arguments[3].ToAngle();

                if (x.HasValue && y.HasValue && z.HasValue && angle.HasValue)
                    return new CSSPrimitiveValue(UnitType.Transform, 
                        new Rotate3DTransform(x.Value, y.Value, z.Value, angle.Value));
            }

            return null;
        }

        static CSSPrimitiveValue RotateX(List<CSSValue> arguments)
        {
            if (arguments.Count == 1)
            {
                var angle = arguments[0].ToAngle();

                if (angle.HasValue)
                    return new CSSPrimitiveValue(UnitType.Transform, 
                        Rotate3DTransform.RotateX(angle.Value));
            }

            return null;
        }

        static CSSPrimitiveValue RotateY(List<CSSValue> arguments)
        {
            if (arguments.Count == 1)
            {
                var angle = arguments[0].ToAngle();

                if (angle.HasValue)
                    return new CSSPrimitiveValue(UnitType.Transform, 
                        Rotate3DTransform.RotateY(angle.Value));
            }

            return null;
        }

        static CSSPrimitiveValue RotateZ(List<CSSValue> arguments)
        {
            if (arguments.Count == 1)
            {
                var angle = arguments[0].ToAngle();

                if (angle.HasValue)
                    return new CSSPrimitiveValue(UnitType.Transform, 
                        Rotate3DTransform.RotateY(angle.Value));
            }

            return null;
        }

        static CSSPrimitiveValue Skew(List<CSSValue> arguments)
        {
            if (arguments.Count == 2)
            {
                var alpha = arguments[0].ToAngle();
                var beta = arguments[1].ToAngle();

                if (alpha.HasValue && beta.HasValue)
                    return new CSSPrimitiveValue(UnitType.Transform, 
                        new SkewTransform(alpha.Value, beta.Value));
            }

            return null;
        }

        static CSSPrimitiveValue SkewX(List<CSSValue> arguments)
        {
            if (arguments.Count == 1)
            {
                var angle = arguments[0].ToAngle();

                if (angle.HasValue)
                    return new CSSPrimitiveValue(UnitType.Transform, 
                        new SkewXTransform(angle.Value));
            }

            return null;
        }

        static CSSPrimitiveValue SkewY(List<CSSValue> arguments)
        {
            if (arguments.Count == 1)
            {
                var angle = arguments[0].ToAngle();

                if (angle.HasValue)
                    return new CSSPrimitiveValue(UnitType.Transform, 
                        new SkewYTransform(angle.Value));
            }

            return null;
        }

        #endregion

        #region Transformation Classes

        /// <summary>
        /// Represents the matrix transformation.
        /// </summary>
        sealed class MatrixTransform : ITransform, ICssObject
        {
            readonly TransformMatrix _matrix;

            internal MatrixTransform(Single m11, Single m12, Single m21, Single m22, Single tx, Single ty)
            {
                _matrix = new TransformMatrix(m11, m12, 0f, m21, m22, 0f, 0f, 0f, 1f, tx, ty, 0f);
            }

            /// <summary>
            /// Returns the matrix transformation.
            /// </summary>
            /// <returns>The stored matrix.</returns>
            public TransformMatrix ComputeMatrix()
            {
                return _matrix;
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public String ToCss()
            {
                return FunctionNames.Build(FunctionNames.Matrix, _matrix.M11.ToString(CultureInfo.InvariantCulture), _matrix.M12.ToString(CultureInfo.InvariantCulture), _matrix.M21.ToString(CultureInfo.InvariantCulture), _matrix.M22.ToString(CultureInfo.InvariantCulture), _matrix.Tx.ToString(CultureInfo.InvariantCulture), _matrix.Ty.ToString(CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        /// Represents the matrix3d transformation.
        /// </summary>
        sealed class Matrix3DTransform : ITransform, ICssObject
        {
            readonly TransformMatrix _matrix;

            internal Matrix3DTransform(Single m11, Single m12, Single m13, Single m21, Single m22, Single m23, Single m31, Single m32, Single m33, Single tx, Single ty, Single tz)
            {
                _matrix = new TransformMatrix(m11, m12, m13, m21, m22, m23, m31, m32, m33, tx, ty, tz);
            }

            /// <summary>
            /// Returns the stored matrix.
            /// </summary>
            /// <returns>The current transformation.</returns>
            public TransformMatrix ComputeMatrix()
            {
                return _matrix;
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public String ToCss()
            {
                return _matrix.ToCss();
            }
        }

        /// <summary>
        /// Represents the translate transformation.
        /// </summary>
        sealed class TranslateTransform : ITransform, ICssObject
        {
            readonly IDistance _y;
            readonly IDistance _x;

            internal TranslateTransform(IDistance x, IDistance y)
            {
                _x = x;
                _y = y;
            }

            /// <summary>
            /// Computes the matrix for the given transformation.
            /// </summary>
            /// <returns>The transformation matrix representation.</returns>
            public TransformMatrix ComputeMatrix()
            {
                var dx = _x.ToPixel();
                var dy = _y.ToPixel();
                return new TransformMatrix(1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f, dx, dy, 0f);
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public String ToCss()
            {
                return FunctionNames.Build(FunctionNames.Translate, _x.ToCss(), _y.ToCss());
            }
        }

        /// <summary>
        /// Represents the translate-x transformation.
        /// </summary>
        sealed class TranslateXTransform : ITransform, ICssObject
        {
            readonly IDistance _x;

            internal TranslateXTransform(IDistance x)
            {
                _x = x;
            }

            /// <summary>
            /// Computes the matrix for the given transformation.
            /// </summary>
            /// <returns>The transformation matrix representation.</returns>
            public TransformMatrix ComputeMatrix()
            {
                var dx = _x.ToPixel();
                return new TransformMatrix(1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f, dx, 0f, 0f);
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public String ToCss()
            {
                return FunctionNames.Build(FunctionNames.TranslateX, _x.ToCss());
            }
        }

        /// <summary>
        /// Represents the translate-y transformation.
        /// </summary>
        sealed class TranslateYTransform : ITransform, ICssObject
        {
            readonly IDistance _y;

            internal TranslateYTransform(IDistance y)
            {
                _y = y;
            }

            /// <summary>
            /// Computes the matrix for the given transformation.
            /// </summary>
            /// <returns>The transformation matrix representation.</returns>
            public TransformMatrix ComputeMatrix()
            {
                var dy = _y.ToPixel();
                return new TransformMatrix(1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f, 0f, dy, 0f);
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public String ToCss()
            {
                return FunctionNames.Build(FunctionNames.TranslateY, _y.ToCss());
            }
        }

        /// <summary>
        /// Represents the translate-z transformation.
        /// </summary>
        sealed class TranslateZTransform : ITransform, ICssObject
        {
            readonly IDistance _z;

            internal TranslateZTransform(IDistance z)
            {
                _z = z;
            }

            /// <summary>
            /// Computes the matrix for the given transformation.
            /// </summary>
            /// <returns>The transformation matrix representation.</returns>
            public TransformMatrix ComputeMatrix()
            {
                var dz = _z.ToPixel();
                return new TransformMatrix(1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f, 0f, 0f, dz);
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public String ToCss()
            {
                return FunctionNames.Build(FunctionNames.TranslateZ, _z.ToCss());
            }
        }

        /// <summary>
        /// Represents the translate3d transformation.
        /// </summary>
        sealed class Translate3DTransform : ITransform, ICssObject
        {
            readonly IDistance _x;
            readonly IDistance _y;
            readonly IDistance _z;

            internal Translate3DTransform(IDistance x, IDistance y, IDistance z)
            {
                _x = x;
                _y = y;
                _z = z;
            }

            /// <summary>
            /// Computes the matrix for the given transformation.
            /// </summary>
            /// <returns>The transformation matrix representation.</returns>
            public TransformMatrix ComputeMatrix()
            {
                var dx = _x.ToPixel();
                var dy = _y.ToPixel();
                var dz = _z.ToPixel();
                return new TransformMatrix(1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f, dx, dy, dz);
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public String ToCss()
            {
                return FunctionNames.Build(FunctionNames.Translate3d, _x.ToCss(), _y.ToCss(), _z.ToCss());
            }
        }

        /// <summary>
        /// Represents the rotate transformation.
        /// </summary>
        sealed class RotateTransform : ITransform, ICssObject
        {
            readonly Angle _angle;

            internal RotateTransform(Angle angle)
            {
                _angle = angle;
            }

            /// <summary>
            /// Computes the matrix for the given transformation.
            /// </summary>
            /// <returns>The transformation matrix representation.</returns>
            public TransformMatrix ComputeMatrix()
            {
                var sina = _angle.Sin();
                var cosa = _angle.Cos();
                return new TransformMatrix(cosa, sina, 0f, -sina, cosa, 0f, 0f, 0f, 1f, 0f, 0f, 0f);
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public String ToCss()
            {
                return FunctionNames.Build(FunctionNames.Rotate, _angle.ToCss());
            }
        }

        /// <summary>
        /// Represents the rotate3d transformation.
        /// </summary>
        sealed class Rotate3DTransform : ITransform, ICssObject
        {
            readonly Single _x;
            readonly Single _y;
            readonly Single _z;
            readonly Angle _angle;

            internal Rotate3DTransform(Single x, Single y, Single z, Angle angle)
            {
                _x = x;
                _y = y;
                _z = z;
                _angle = angle;
            }

            /// <summary>
            /// Constructs a rotate 3D transformation around the x-axis.
            /// </summary>
            /// <param name="angle">The angle to rotate.</param>
            /// <returns>The rotate 3D transformation.</returns>
            public static Rotate3DTransform RotateX(Angle angle)
            {
                return new Rotate3DTransform(1f, 0f, 0f, angle);
            }

            /// <summary>
            /// Constructs a rotate 3D transformation around the y-axis.
            /// </summary>
            /// <param name="angle">The angle to rotate.</param>
            /// <returns>The rotate 3D transformation.</returns>
            public static Rotate3DTransform RotateY(Angle angle)
            {
                return new Rotate3DTransform(0f, 1f, 0f, angle);
            }

            /// <summary>
            /// Constructs a rotate 3D transformation around the z-axis.
            /// </summary>
            /// <param name="angle">The angle to rotate.</param>
            /// <returns>The rotate 3D transformation.</returns>
            public static Rotate3DTransform RotateZ(Angle angle)
            {
                return new Rotate3DTransform(0f, 0f, 1f, angle);
            }

            /// <summary>
            /// Computes the matrix for the given transformation.
            /// </summary>
            /// <returns>The transformation matrix representation.</returns>
            public TransformMatrix ComputeMatrix()
            {
                var norm = 1f / (Single)Math.Sqrt(_x * _x + _y * _y + _z * _z);
                var sina = _angle.Sin();
                var cosa = _angle.Cos();
                var l = _x * norm;
                var m = _y * norm;
                var n = _z * norm;
                var omc = (1f - cosa);
                return new TransformMatrix(
                    l * l * omc + cosa, m * l * omc - n * sina, n * l * omc + m * sina,
                    l * m * omc + n * sina, m * m * omc + cosa, n * m * omc - l * sina,
                    l * n * omc - m * sina, m * n * omc + l * sina, n * n * omc + cosa,
                    0f, 0f, 0f);
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public String ToCss()
            {
                return FunctionNames.Build(FunctionNames.Rotate3d, _x.ToString(CultureInfo.InvariantCulture), _y.ToString(CultureInfo.InvariantCulture), _z.ToString(CultureInfo.InvariantCulture), _angle.ToCss());
            }
        }

        /// <summary>
        /// Represents the scale transformation.
        /// </summary>
        sealed class ScaleTransform : ITransform, ICssObject
        {
            readonly Single _sx;
            readonly Single _sy;

            internal ScaleTransform(Single scale)
            {
                _sx = scale;
                _sy = scale;
            }

            internal ScaleTransform(Single sx, Single sy)
            {
                _sx = sx;
                _sy = sy;
            }

            /// <summary>
            /// Computes the matrix for the given transformation.
            /// </summary>
            /// <returns>The transformation matrix representation.</returns>
            public TransformMatrix ComputeMatrix()
            {
                return new TransformMatrix(_sx, 0f, 0f, 0f, _sy, 0f, 0f, 0f, 1f, 0f, 0f, 0f);
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public String ToCss()
            {
                if (_sx == _sy)
                    return FunctionNames.Build(FunctionNames.Scale, _sx.ToString(CultureInfo.InvariantCulture));

                return FunctionNames.Build(FunctionNames.Scale, _sx.ToString(CultureInfo.InvariantCulture), _sy.ToString(CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        /// Represents the scale-x transformation.
        /// </summary>
        sealed class ScaleXTransform : ITransform, ICssObject
        {
            readonly Single _scale;

            internal ScaleXTransform(Single scale)
            {
                _scale = scale;
            }

            /// <summary>
            /// Computes the matrix for the given transformation.
            /// </summary>
            /// <returns>The transformation matrix representation.</returns>
            public TransformMatrix ComputeMatrix()
            {
                return new TransformMatrix(_scale, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f);
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public String ToCss()
            {
                return FunctionNames.Build(FunctionNames.ScaleX, _scale.ToString(CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        /// Represents the scale-y transformation.
        /// </summary>
        sealed class ScaleYTransform : ITransform, ICssObject
        {
            readonly Single _scale;

            internal ScaleYTransform(Single scale)
            {
                _scale = scale;
            }

            /// <summary>
            /// Computes the matrix for the given transformation.
            /// </summary>
            /// <returns>The transformation matrix representation.</returns>
            public TransformMatrix ComputeMatrix()
            {
                return new TransformMatrix(1f, 0f, 0f, 0f, _scale, 0f, 0f, 0f, 1f, 0f, 0f, 0f);
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public String ToCss()
            {
                return FunctionNames.Build(FunctionNames.ScaleY, _scale.ToString(CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        /// Represents the scale-z transformation.
        /// </summary>
        sealed class ScaleZTransform : ITransform, ICssObject
        {
            readonly Single _scale;

            internal ScaleZTransform(Single scale)
            {
                _scale = scale;
            }

            /// <summary>
            /// Computes the matrix for the given transformation.
            /// </summary>
            /// <returns>The transformation matrix representation.</returns>
            public TransformMatrix ComputeMatrix()
            {
                return new TransformMatrix(1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, _scale, 0f, 0f, 0f);
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public String ToCss()
            {
                return FunctionNames.Build(FunctionNames.ScaleZ, _scale.ToString(CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        /// Represents the scale3d transformation.
        /// </summary>
        sealed class Scale3DTransform : ITransform, ICssObject
        {
            readonly Single _sx;
            readonly Single _sy;
            readonly Single _sz;

            internal Scale3DTransform(Single sx, Single sy, Single sz)
            {
                _sx = sx;
                _sy = sy;
                _sz = sz;
            }

            /// <summary>
            /// Computes the matrix for the given transformation.
            /// </summary>
            /// <returns>The transformation matrix representation.</returns>
            public TransformMatrix ComputeMatrix()
            {
                return new TransformMatrix(_sx, 0f, 0f, 0f, _sy, 0f, 0f, 0f, _sz, 0f, 0f, 0f);
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public String ToCss()
            {
                return FunctionNames.Build(FunctionNames.Scale3d, _sx.ToString(CultureInfo.InvariantCulture), _sy.ToString(CultureInfo.InvariantCulture), _sz.ToString(CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        /// Represents the skew transformation.
        /// </summary>
        sealed class SkewTransform : ITransform, ICssObject
        {
            readonly Angle _alpha;
            readonly Angle _beta;

            internal SkewTransform(Angle alpha, Angle beta)
            {
                _alpha = alpha;
                _beta = beta;
            }

            /// <summary>
            /// Computes the matrix for the given transformation.
            /// </summary>
            /// <returns>The transformation matrix representation.</returns>
            public TransformMatrix ComputeMatrix()
            {
                var a = _alpha.Tan();
                var b = _beta.Tan();
                return new TransformMatrix(1f, a, 0f, b, 1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f);
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public String ToCss()
            {
                return FunctionNames.Build(FunctionNames.Skew, _alpha.ToCss(), _beta.ToCss());
            }
        }

        /// <summary>
        /// Represents the skew-x transformation.
        /// </summary>
        sealed class SkewXTransform : ITransform, ICssObject
        {
            readonly Angle _angle;

            internal SkewXTransform(Angle alpha)
            {
                _angle = alpha;
            }

            /// <summary>
            /// Computes the matrix for the given transformation.
            /// </summary>
            /// <returns>The transformation matrix representation.</returns>
            public TransformMatrix ComputeMatrix()
            {
                var a = _angle.Tan();
                return new TransformMatrix(1f, a, 0f, 0f, 1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f);
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public String ToCss()
            {
                return FunctionNames.Build(FunctionNames.SkewX, _angle.ToCss());
            }
        }

        /// <summary>
        /// Represents the skew-y transformation.
        /// </summary>
        sealed class SkewYTransform : ITransform, ICssObject
        {
            readonly Angle _angle;

            internal SkewYTransform(Angle beta)
            {
                _angle = beta;
            }

            /// <summary>
            /// Computes the matrix for the given transformation.
            /// </summary>
            /// <returns>The transformation matrix representation.</returns>
            public TransformMatrix ComputeMatrix()
            {
                var b = _angle.Tan();
                return new TransformMatrix(1f, 0f, 0f, b, 1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f);
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public String ToCss()
            {
                return FunctionNames.Build(FunctionNames.SkewY, _angle.ToCss());
            }
        }

        #endregion
    }
}
