namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Represents a CSS primitive value.
    /// </summary>
    sealed class CSSPrimitiveValue : CSSValue, ICssPrimitiveValue
    {
        #region Fields

        ICssObject _value;
        UnitType _unit;

        #endregion

        #region ctor

        public CSSPrimitiveValue(UnitType unit, ICssObject value)
            : base(CssValueType.Primitive)
        {
            _unit = unit;
            _value = value;
        }

        public CSSPrimitiveValue(CssString value)
            : this(UnitType.String, value)
        {
        }

        public CSSPrimitiveValue(CssAttr value)
            : this(UnitType.Attr, value)
        {
        }

        public CSSPrimitiveValue(Url url)
            : this(UnitType.Uri, url)
        {
        }

        public CSSPrimitiveValue(Color color)
            : this(UnitType.RgbColor, color)
        {
        }

        public CSSPrimitiveValue(Length length)
            : this(length.Type.Generalize(), length)
        {
        }

        public CSSPrimitiveValue(Frequency frequency)
            : this(frequency.Type.Generalize(), frequency)
        {
        }

        public CSSPrimitiveValue(Time time)
            : this(time.Type.Generalize(), time)
        {
        }

        public CSSPrimitiveValue(Angle angle)
            : this(angle.Type.Generalize(), angle)
        {
        }

        public CSSPrimitiveValue(Resolution resolution)
            : this(resolution.Type.Generalize(), resolution)
        {
        }

        public CSSPrimitiveValue(Percent percent)
            : this(UnitType.Percent, percent)
        {
        }

        public CSSPrimitiveValue(Number number)
            : this(UnitType.Number, number)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value of the primitive container.
        /// </summary>
        public ICssObject Value
        {
            get { return _value; }
        }

        public UnitType Unit
        {
            get { return _unit; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a CSS code representation of the stylesheet.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public override String ToCss()
        {
            return _value.ToCss();
        }

        public void SetNumber(UnitType unit, Single value)
        {
            switch (unit)
            {
                case UnitType.Deg:
                    _value = new Angle(value, Angle.Unit.Deg);
                    break;
                case UnitType.Dimension:
                    _value = new Number(value);
                    break;
                case UnitType.Em:
                    _value = new Length(value, Length.Unit.Em);
                    break;
                case UnitType.Ex:
                    _value = new Length(value, Length.Unit.Ex);
                    break;
                case UnitType.Grad:
                    _value = new Angle(value, Angle.Unit.Grad);
                    break;
                case UnitType.Hz:
                    _value = new Frequency(value, Frequency.Unit.Hz);
                    break;
                case UnitType.In:
                    _value = new Length(value, Length.Unit.In);
                    break;
                case UnitType.Khz:
                    _value = new Frequency(value, Frequency.Unit.Khz);
                    break;
                case UnitType.Mm:
                    _value = new Length(value, Length.Unit.Mm);
                    break;
                case UnitType.Ms:
                    _value = new Time(value, Time.Unit.S);
                    break;
                case UnitType.Number:
                    _value = new Number(value);
                    break;
                case UnitType.Pc:
                    _value = new Length(value, Length.Unit.Pc);
                    break;
                case UnitType.Percent:
                    _value = new Percent(value);
                    break;
                case UnitType.Pt:
                    _value = new Length(value, Length.Unit.Pt);
                    break;
                case UnitType.Px:
                    _value = new Length(value, Length.Unit.Px);
                    break;
                case UnitType.Rad:
                    _value = new Angle(value, Angle.Unit.Rad);
                    break;
                case UnitType.S:
                    _value = new Time(value, Time.Unit.S);
                    break;
                default:
                    throw new DomException(ErrorCode.NotSupported);
            }

            _unit = unit;
        }

        public Single GetNumber(UnitType unit)
        {
            if (unit == UnitType.Number && _unit == UnitType.Number)
                return ((Number)_value).Value;

            throw new NotImplementedException();
        }

        public void SetString(UnitType unit, String value)
        {
            switch (unit)
            {
                case UnitType.String:
                    _value = new CssString(value);
                    break;
                case UnitType.Attr:
                    _value = new CssAttr(value);
                    break;
                case UnitType.Uri:
                    _value = new Url(value);
                    break;
                default:
                    throw new DomException(ErrorCode.NotSupported);
            }

            _unit = unit;
        }

        public String GetString()
        {
            switch (_unit)
            {
                case UnitType.Attr:
                    return (CssAttr)_value;
                case UnitType.String:
                    return (CssString)_value;
                case UnitType.Uri:
                    return ((Url)_value).Href;
                default:
                    return null;
            }
        }

        public Color GetColor()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
