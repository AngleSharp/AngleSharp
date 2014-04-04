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
            : base(PropertyNames.Direction)
        {
            _mode = modes["ltr"];
            _inherited = true;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            if (value is CSSIdentifierValue)
            {
                var ident = (CSSIdentifierValue)value;
                DirectionMode mode;

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

        abstract class DirectionMode
        {
            //TODO Add members that make sense
        }

        /// <summary>
        /// The initial value of direction (that is, if not
        /// otherwise specified). Text and other elements go from left to right.
        /// </summary>
        sealed class LtrDirectionMode : DirectionMode
        {
        }

        /// <summary>
        /// Text and other elements go from right to left
        /// </summary>
        sealed class RtlDirectionMode : DirectionMode
        {
        }

        #endregion
    }
}
