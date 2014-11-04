namespace AngleSharp.Extensions
{
    using AngleSharp.DOM.Css;
    using System.Diagnostics;

    /// <summary>
    /// Extensions for CSS unit types.
    /// </summary>
    [DebuggerStepThrough]
    static class UnitExtensions
    {
        public static UnitType Generalize(this Length.Unit unit)
        {
            switch (unit)
            {
                case Length.Unit.Em:
                    return UnitType.Em;
                case Length.Unit.Ex:
                    return UnitType.Ex;
                case Length.Unit.In:
                    return UnitType.In;
                case Length.Unit.Mm:
                    return UnitType.Mm;
                case Length.Unit.Pc:
                    return UnitType.Pc;
                case Length.Unit.Pt:
                    return UnitType.Pt;
                case Length.Unit.Px:
                    return UnitType.Px;
                case Length.Unit.Cm:
                    return UnitType.Cm;
                case Length.Unit.Ch:
                case Length.Unit.Rem:
                case Length.Unit.Vh:
                case Length.Unit.Vmax:
                case Length.Unit.Vmin:
                case Length.Unit.Vw:
                default:
                    return UnitType.Unknown;
            }
        }

        public static UnitType Generalize(this Angle.Unit unit)
        {
            switch (unit)
            {
                case Angle.Unit.Deg:
                    return UnitType.Deg;
                case Angle.Unit.Grad:
                    return UnitType.Grad;
                case Angle.Unit.Rad:
                    return UnitType.Rad;
                case Angle.Unit.Turn:
                default:
                    return UnitType.Unknown;
            }
        }

        public static UnitType Generalize(this Resolution.Unit unit)
        {
            switch (unit)
            {
                case Resolution.Unit.Dpcm:
                case Resolution.Unit.Dpi:
                case Resolution.Unit.Dppx:
                default:
                    return UnitType.Unknown;
            }
        }

        public static UnitType Generalize(this Frequency.Unit unit)
        {
            switch (unit)
            {
                case Frequency.Unit.Hz:
                    return UnitType.Hz;
                case Frequency.Unit.Khz:
                    return UnitType.Khz;
                default:
                    return UnitType.Unknown;
            }
        }

        public static UnitType Generalize(this Time.Unit unit)
        {
            switch (unit)
            {
                case Time.Unit.Ms:
                    return UnitType.Ms;
                case Time.Unit.S:
                    return UnitType.S;
                default:
                    return UnitType.Unknown;
            }
        }
    }
}
