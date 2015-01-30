namespace AngleSharp.Factories
{
    using AngleSharp.Css;
    using AngleSharp.Dom.Css;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Factory to create unit attached CSS objects.
    /// </summary>
    sealed class CssUnitFactory
    {
        readonly Dictionary<String, Func<Single, ICssValue>> creators = new Dictionary<String, Func<Single, ICssValue>>(StringComparer.OrdinalIgnoreCase)
        {
            { Units.Em, data => new Length(data, Length.Unit.Em) },
            { Units.Cm, data => new Length(data, Length.Unit.Cm) },
            { Units.Ex, data => new Length(data, Length.Unit.Ex) },
            { Units.In, data => new Length(data, Length.Unit.In) },
            { Units.Mm, data => new Length(data, Length.Unit.Mm) },
            { Units.Pc, data => new Length(data, Length.Unit.Pc) },
            { Units.Pt, data => new Length(data, Length.Unit.Pt) },
            { Units.Px, data => new Length(data, Length.Unit.Px) },
            { Units.Rem, data => new Length(data, Length.Unit.Rem) },
            { Units.Ch, data => new Length(data, Length.Unit.Ch) },
            { Units.Vw, data => new Length(data, Length.Unit.Vw) },
            { Units.Vh, data => new Length(data, Length.Unit.Vh) },
            { Units.Vmin, data => new Length(data, Length.Unit.Vmin) },
            { Units.Vmax, data => new Length(data, Length.Unit.Vmax) },
            { Units.Ms, data => new Time(data, Time.Unit.Ms) },
            { Units.S, data => new Time(data, Time.Unit.S) },
            { Units.Dpi, data => new Resolution(data, Resolution.Unit.Dpi) },
            { Units.Dpcm, data => new Resolution(data, Resolution.Unit.Dpcm) },
            { Units.Dppx, data => new Resolution(data, Resolution.Unit.Dppx) },
            { Units.Deg, data => new Angle(data, Angle.Unit.Deg) },
            { Units.Grad, data => new Angle(data, Angle.Unit.Grad) },
            { Units.Rad, data => new Angle(data, Angle.Unit.Rad) },
            { Units.Turn, data => new Angle(data, Angle.Unit.Turn) },
            { Units.Hz, data => new Frequency(data, Frequency.Unit.Hz) },
            { Units.Khz, data => new Frequency(data, Frequency.Unit.Khz) }
        };

        /// <summary>
        /// Creates a new CSS value from the given quantity (value with unit).
        /// </summary>
        /// <param name="value">The value of the quantity.</param>
        /// <param name="unit">The unit of the quantity.</param>
        /// <returns>The created CSS value.</returns>
        public ICssValue Create(Single value, String unit)
        {
            Func<Single, ICssValue> creator;

            if (creators.TryGetValue(unit, out creator))
                return creator(value);

            return null;
        }
    }
}
