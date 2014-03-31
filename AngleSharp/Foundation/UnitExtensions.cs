namespace AngleSharp.DOM.Css
{
    using System;

    static class UnitExtensions
    {
        /// <summary>
        /// Converts the given string to a CssUnit.
        /// </summary>
        /// <param name="unit">The string representing a unit.</param>
        /// <returns>The CssUnit value.</returns>
        public static CssUnit ToCssUnit(this String unit)
        {
            switch (unit)
            {
                case "em":   return CssUnit.Ems;
                case "cm":   return CssUnit.Cm;
                case "deg":  return CssUnit.Deg;
                case "grad": return CssUnit.Grad;
                case "rad":  return CssUnit.Rad;
                case "turn": return CssUnit.Turn;
                case "ex":   return CssUnit.Exs;
                case "hz":   return CssUnit.Hz;
                case "in":   return CssUnit.In;
                case "khz":  return CssUnit.Khz;
                case "mm":   return CssUnit.Mm;
                case "ms":   return CssUnit.Ms;
                case "s":    return CssUnit.S;
                case "pc":   return CssUnit.Pc;
                case "pt":   return CssUnit.Pt;
                case "px":   return CssUnit.Px;
                case "rem":  return CssUnit.Rems;
                case "ch":   return CssUnit.Ch;
                case "vw":   return CssUnit.Vw;
                case "vh":   return CssUnit.Vh;
                case "vmin": return CssUnit.Vmin;
                case "vmax": return CssUnit.Vmax;
                case "dpi":  return CssUnit.Dpi;
                case "dpcm": return CssUnit.Dpcm;
                case "dppx": return CssUnit.Dppx;
            }

            return CssUnit.Unknown;
        }

        /// <summary>
        /// Converts the given unit to a string.
        /// </summary>
        /// <param name="unit">The unit that should be converted to a string.</param>
        /// <returns>The string representing the CssUnit.</returns>
        public static String ToCssString(this CssUnit unit)
        {
            switch (unit)
            {
                case CssUnit.Ems:        return "em";
                case CssUnit.Cm:         return "cm";
                case CssUnit.Deg:        return "deg";
                case CssUnit.Grad:       return "grad";
                case CssUnit.Rad:        return "rad";
                case CssUnit.Turn:       return "turn";
                case CssUnit.Exs:        return "ex";
                case CssUnit.Hz:         return "hz";
                case CssUnit.In:         return "in";
                case CssUnit.Khz:        return "khz";
                case CssUnit.Mm:         return "mm";
                case CssUnit.Ms:         return "ms";
                case CssUnit.S:          return "s";
                case CssUnit.Pc:         return "pc";
                case CssUnit.Pt:         return "pt";
                case CssUnit.Px:         return "px";
                case CssUnit.Vw:         return "vw";
                case CssUnit.Vh:         return "vh";
                case CssUnit.Vmin:       return "vmin";
                case CssUnit.Vmax:       return "vmax";
                case CssUnit.Dpi:        return "dpi";
                case CssUnit.Dpcm:       return "dpcm";
                case CssUnit.Dppx:       return "dppx";
                case CssUnit.Rems:        return "rem";
                case CssUnit.Ch:         return "ch";
            }

            return String.Empty;
        }
    }
}
