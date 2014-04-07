namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Basis for page-break-before or page-break-after property.
    /// </summary>
    abstract class CSSPageBreakProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, BreakMode> modes = new Dictionary<String, BreakMode>(StringComparer.OrdinalIgnoreCase);
        BreakMode _mode;

        #endregion

        #region ctor

        static CSSPageBreakProperty()
        {
            modes.Add("auto", new AutoBreakMode());
            modes.Add("always", new AlwaysBreakMode());
            modes.Add("avoid", new AvoidBreakMode());
            modes.Add("left", new LeftBreakMode());
            modes.Add("right", new RightBreakMode());
        }

        public CSSPageBreakProperty(String name)
            : base(name)
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
                BreakMode mode;

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

        abstract class BreakMode
        {
            //TODO Add members that make sense
        }
        
        /// <summary>
        /// Initial value. Automatic page breaks (neither forced
        /// nor forbidden).
        /// </summary>
        sealed class AutoBreakMode : BreakMode
        {
        }

        /// <summary>
        /// Always force page breaks before the element.
        /// </summary>
        sealed class AlwaysBreakMode : BreakMode
        {
        }

        /// <summary>
        /// Avoid page breaks before the element.
        /// </summary>
        sealed class AvoidBreakMode : BreakMode
        {
        }

        /// <summary>
        /// Force page breaks before the element so that the next
        /// page is formatted as a left page.
        /// </summary>
        sealed class LeftBreakMode : BreakMode
        {
        }

        /// <summary>
        /// Force page breaks before the element so that the next
        /// page is formatted as a right page.
        /// </summary>
        sealed class RightBreakMode : BreakMode
        {
        }
        
        #endregion
    }
}
