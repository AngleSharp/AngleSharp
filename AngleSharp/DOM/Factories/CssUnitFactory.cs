namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Factory to create unit attached CSS objects.
    /// </summary>
    static class CssUnitFactory
    {
        static readonly Dictionary<String, Func<Single, ICssObject>> values = new Dictionary<String, Func<Single, ICssObject>>(StringComparer.OrdinalIgnoreCase);

        static CssUnitFactory()
        {
            values.Add(Units.Em, data => new Length(data, Length.Unit.Em));
            values.Add(Units.Cm, data => new Length(data, Length.Unit.Cm));
            values.Add(Units.Ex, data => new Length(data, Length.Unit.Ex));
            values.Add(Units.In, data => new Length(data, Length.Unit.In));
            values.Add(Units.Mm, data => new Length(data, Length.Unit.Mm));
            values.Add(Units.Pc, data => new Length(data, Length.Unit.Pc));
            values.Add(Units.Pt, data => new Length(data, Length.Unit.Pt));
            values.Add(Units.Px, data => new Length(data, Length.Unit.Px));
            values.Add(Units.Rem, data => new Length(data, Length.Unit.Rem));
            values.Add(Units.Ch, data => new Length(data, Length.Unit.Ch));
            values.Add(Units.Vw, data => new Length(data, Length.Unit.Vw));
            values.Add(Units.Vh, data => new Length(data, Length.Unit.Vh));
            values.Add(Units.Vmin, data => new Length(data, Length.Unit.Vmin));
            values.Add(Units.Vmax, data => new Length(data, Length.Unit.Vmax));
            values.Add(Units.Ms, data => new Time(data, Time.Unit.Ms));
            values.Add(Units.S, data => new Time(data, Time.Unit.S));
            values.Add(Units.Dpi, data => new Resolution(data, Resolution.Unit.Dpi));
            values.Add(Units.Dpcm, data => new Resolution(data, Resolution.Unit.Dpcm));
            values.Add(Units.Dppx, data => new Resolution(data, Resolution.Unit.Dppx));
            values.Add(Units.Deg, data => new Angle(data, Angle.Unit.Deg));
            values.Add(Units.Grad, data => new Angle(data, Angle.Unit.Grad));
            values.Add(Units.Rad, data => new Angle(data, Angle.Unit.Rad));
            values.Add(Units.Turn, data => new Angle(data, Angle.Unit.Turn));
            values.Add(Units.Hz, data => new Frequency(data, Frequency.Unit.Hz));
            values.Add(Units.Khz, data => new Frequency(data, Frequency.Unit.Khz));
        }

        /// <summary>
        /// Creates a new value.
        /// </summary>
        /// <param name="unit">The unit of the value.</param>
        /// <param name="data">The data of the value.</param>
        /// <returns>The created value.</returns>
        public static ICssObject Create(String unit, Single data)
        {
            Func<Single, ICssObject> valueCreator;

            if (values.TryGetValue(unit, out valueCreator))
                return valueCreator(data);

            return null;
        }
    }
}
