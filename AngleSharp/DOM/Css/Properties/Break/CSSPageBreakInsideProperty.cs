namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/page-break-inside
    /// </summary>
    sealed class CSSPageBreakInsideProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, BreakInsideMode> modes = new Dictionary<String, BreakInsideMode>(StringComparer.OrdinalIgnoreCase);
        BreakInsideMode _mode;

        #endregion

        #region ctor

        static CSSPageBreakInsideProperty()
        {
            modes.Add("auto", new AutoBreakInsideMode());
            modes.Add("avoid", new AvoidBreakInsideMode());
        }

        public CSSPageBreakInsideProperty()
            : base(PropertyNames.PageBreakInside)
        {
            _mode = modes["auto"];
            _inherited = false;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            if (value is CSSIdentifierValue)
            {
                var ident = (CSSIdentifierValue)value;
                BreakInsideMode mode;

                if (modes.TryGetValue(ident.Value, out mode))
                {
                    _mode = mode;
                    return true;
                }
            }
            else if (value == CSSValue.Inherit)
                return true;

            return false;
        }

        #endregion

        #region Modes

        abstract class BreakInsideMode
        {
            //TODO Add members that make sense
        }
        
        /// <summary>
        /// Initial value. Automatic page breaks
        /// (neither forced nor forbidden).
        /// </summary>
        sealed class AutoBreakInsideMode : BreakInsideMode
        {
        }

        /// <summary>
        /// Avoid page breaks inside the element.
        /// </summary>
        sealed class AvoidBreakInsideMode : BreakInsideMode
        {
        }
        
        #endregion
    }
}
