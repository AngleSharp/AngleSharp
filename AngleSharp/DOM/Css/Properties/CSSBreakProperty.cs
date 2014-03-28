namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Basis for break-before or break-after property.
    /// </summary>
    abstract class CSSBreakProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, BreakMode> modes = new Dictionary<String, BreakMode>(StringComparer.OrdinalIgnoreCase);
        BreakMode _mode;

        #endregion

        #region ctor

        static CSSBreakProperty()
        {
            modes.Add("auto", new AutoBreakMode());
            modes.Add("always", new AlwaysBreakMode());
            modes.Add("avoid", new AvoidPageBreakMode());
            modes.Add("left", new LeftBreakMode());
            modes.Add("right", new RightBreakMode());
            modes.Add("page", new PageBreakMode());
            modes.Add("column", new ColumnBreakMode());
            modes.Add("avoid-page", new AvoidPageBreakMode());
            modes.Add("avoid-colum", new AvoidColumnBreakMode());
        }

        public CSSBreakProperty(String name)
            : base(name)
        {
            _mode = modes["auto"];
            _inherited = false;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            if (value is CSSIdentifier)
            {
                var ident = (CSSIdentifier)value;
                BreakMode mode;

                if (modes.TryGetValue(ident.Identifier, out mode))
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
        
        class AutoBreakMode : BreakMode
        {
        }

        class AlwaysBreakMode : BreakMode
        {
        }

        class AvoidBreakMode : BreakMode
        {
        }

        class LeftBreakMode : BreakMode
        {
        }

        class RightBreakMode : BreakMode
        {
        }

        class PageBreakMode : BreakMode
        {
        }

        class ColumnBreakMode : BreakMode
        {
        }

        class AvoidPageBreakMode : BreakMode
        {
        }

        class AvoidColumnBreakMode : BreakMode
        {
        }
        
        #endregion
    }
}
