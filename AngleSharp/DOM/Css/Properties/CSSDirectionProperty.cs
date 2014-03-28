namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/direction
    /// </summary>
    sealed class CSSDirectionProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, DirectionMode> modes = new Dictionary<String, DirectionMode>(StringComparer.OrdinalIgnoreCase);
        DirectionMode _mode;

        #endregion

        #region ctor

        static CSSDirectionProperty()
        {
            modes.Add("ltr", new LtrDirectionMode());
            modes.Add("rtl", new RtlDirectionMode());
        }

        public CSSDirectionProperty()
            : base(PropertyNames.DIRECTION)
        {
            _mode = modes["ltr"];
            _inherited = true;
        }

        #endregion

        #region Methods

        protected override CSSValue CheckValue(CSSValue value)
        {
            if (value is CSSIdent)
            {
                var ident = (CSSIdent)value;
                DirectionMode mode;

                if (modes.TryGetValue(ident.Token, out mode))
                {
                    _mode = mode;
                    return value;
                }
            }
            else if (value == CSSValue.Inherit)
                return value;

            return null;
        }

        #endregion

        #region Modes

        abstract class DirectionMode
        {
            //TODO Add members that make sense
        }

        class LtrDirectionMode : DirectionMode
        {
        }

        class RtlDirectionMode : DirectionMode
        {
        }

        #endregion
    }
}
