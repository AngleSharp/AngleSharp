namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;

    static class CssUnitFactory
    {
        static readonly Dictionary<String, Func<Single, CSSValue>> values = new Dictionary<String, Func<Single, CSSValue>>(StringComparer.OrdinalIgnoreCase);

        static CssUnitFactory()
        {
            values.Add(Units.Em, data => new CSSPrimitiveValue<Length>(new Length(data, Length.Unit.Em)));
            values.Add(Units.Cm, data => new CSSPrimitiveValue<Length>(new Length(data, Length.Unit.Cm)));
            values.Add(Units.Ex, data => new CSSPrimitiveValue<Length>(new Length(data, Length.Unit.Ex)));
            values.Add(Units.In, data => new CSSPrimitiveValue<Length>(new Length(data, Length.Unit.In)));
            values.Add(Units.Mm, data => new CSSPrimitiveValue<Length>(new Length(data, Length.Unit.Mm)));
            values.Add(Units.Pc, data => new CSSPrimitiveValue<Length>(new Length(data, Length.Unit.Pc)));
            values.Add(Units.Pt, data => new CSSPrimitiveValue<Length>(new Length(data, Length.Unit.Pt)));
            values.Add(Units.Px, data => new CSSPrimitiveValue<Length>(new Length(data, Length.Unit.Px)));
            values.Add(Units.Rem, data => new CSSPrimitiveValue<Length>(new Length(data, Length.Unit.Rem)));
            values.Add(Units.Ch, data => new CSSPrimitiveValue<Length>(new Length(data, Length.Unit.Ch)));
            values.Add(Units.Vw, data => new CSSPrimitiveValue<Length>(new Length(data, Length.Unit.Vw)));
            values.Add(Units.Vh, data => new CSSPrimitiveValue<Length>(new Length(data, Length.Unit.Vh)));
            values.Add(Units.Vmin, data => new CSSPrimitiveValue<Length>(new Length(data, Length.Unit.Vmin)));
            values.Add(Units.Vmax, data => new CSSPrimitiveValue<Length>(new Length(data, Length.Unit.Vmax)));
            values.Add(Units.Ms, data => new CSSPrimitiveValue<Time>(new Time(data, Time.Unit.Ms)));
            values.Add(Units.S, data => new CSSPrimitiveValue<Time>(new Time(data, Time.Unit.S)));
            values.Add(Units.Dpi, data => new CSSPrimitiveValue<Resolution>(new Resolution(data, Resolution.Unit.Dpi)));
            values.Add(Units.Dpcm, data => new CSSPrimitiveValue<Resolution>(new Resolution(data, Resolution.Unit.Dpcm)));
            values.Add(Units.Dppx, data => new CSSPrimitiveValue<Resolution>(new Resolution(data, Resolution.Unit.Dppx)));
            values.Add(Units.Deg, data => new CSSPrimitiveValue<Angle>(new Angle(data, Angle.Unit.Deg)));
            values.Add(Units.Grad, data => new CSSPrimitiveValue<Angle>(new Angle(data, Angle.Unit.Grad)));
            values.Add(Units.Rad, data => new CSSPrimitiveValue<Angle>(new Angle(data, Angle.Unit.Rad)));
            values.Add(Units.Turn, data => new CSSPrimitiveValue<Angle>(new Angle(data, Angle.Unit.Turn)));
            values.Add(Units.Hz, data => new CSSPrimitiveValue<Frequency>(new Frequency(data, Frequency.Unit.Hz)));
            values.Add(Units.Khz, data => new CSSPrimitiveValue<Frequency>(new Frequency(data, Frequency.Unit.Khz)));
        }

        /// <summary>
        /// Creates a new value.
        /// </summary>
        /// <param name="unit">The unit of the value.</param>
        /// <param name="data">The data of the value.</param>
        /// <returns>The created value.</returns>
        public static CSSValue Create(String unit, Single data)
        {
            Func<Single, CSSValue> valueCreator;

            if (values.TryGetValue(unit, out valueCreator))
                return valueCreator(data);

            return null;
        }
    }
}
