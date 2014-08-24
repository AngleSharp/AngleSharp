namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;

    static class CssUnitFactory
    {
        static readonly Dictionary<String, Func<Single, CSSValue>> values = new Dictionary<String, Func<Single, CSSValue>>(StringComparer.OrdinalIgnoreCase);

        static CssUnitFactory()
        {
            values.Add("em", data => new CSSPrimitiveValue<Length>(new Length(data, Length.Unit.Em)));
            values.Add("cm", data => new CSSPrimitiveValue<Length>(new Length(data, Length.Unit.Cm)));
            values.Add("ex", data => new CSSPrimitiveValue<Length>(new Length(data, Length.Unit.Ex)));
            values.Add("in", data => new CSSPrimitiveValue<Length>(new Length(data, Length.Unit.In)));
            values.Add("mm", data => new CSSPrimitiveValue<Length>(new Length(data, Length.Unit.Mm)));
            values.Add("pc", data => new CSSPrimitiveValue<Length>(new Length(data, Length.Unit.Pc)));
            values.Add("pt", data => new CSSPrimitiveValue<Length>(new Length(data, Length.Unit.Pt)));
            values.Add("px", data => new CSSPrimitiveValue<Length>(new Length(data, Length.Unit.Px)));
            values.Add("rem", data => new CSSPrimitiveValue<Length>(new Length(data, Length.Unit.Rem)));
            values.Add("ch", data => new CSSPrimitiveValue<Length>(new Length(data, Length.Unit.Ch)));
            values.Add("vw", data => new CSSPrimitiveValue<Length>(new Length(data, Length.Unit.Vw)));
            values.Add("vh", data => new CSSPrimitiveValue<Length>(new Length(data, Length.Unit.Vh)));
            values.Add("vmin", data => new CSSPrimitiveValue<Length>(new Length(data, Length.Unit.Vmin)));
            values.Add("vmax", data => new CSSPrimitiveValue<Length>(new Length(data, Length.Unit.Vmax)));
            values.Add("ms", data => new CSSPrimitiveValue<Time>(new Time(data, Time.Unit.Ms)));
            values.Add("s", data => new CSSPrimitiveValue<Time>(new Time(data, Time.Unit.S)));
            values.Add("dpi", data => new CSSPrimitiveValue<Resolution>(new Resolution(data, Resolution.Unit.Dpi)));
            values.Add("dpcm", data => new CSSPrimitiveValue<Resolution>(new Resolution(data, Resolution.Unit.Dpcm)));
            values.Add("dppx", data => new CSSPrimitiveValue<Resolution>(new Resolution(data, Resolution.Unit.Dppx)));
            values.Add("deg", data => new CSSPrimitiveValue<Angle>(new Angle(data, Angle.Unit.Deg)));
            values.Add("grad", data => new CSSPrimitiveValue<Angle>(new Angle(data, Angle.Unit.Grad)));
            values.Add("rad", data => new CSSPrimitiveValue<Angle>(new Angle(data, Angle.Unit.Rad)));
            values.Add("turn", data => new CSSPrimitiveValue<Angle>(new Angle(data, Angle.Unit.Turn)));
            values.Add("hz", data => new CSSPrimitiveValue<Frequency>(new Frequency(data, Frequency.Unit.Hz)));
            values.Add("khz", data => new CSSPrimitiveValue<Frequency>(new Frequency(data, Frequency.Unit.Khz)));
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
