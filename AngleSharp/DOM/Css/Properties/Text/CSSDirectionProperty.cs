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
            modes.Add("ltr", DirectionMode.Ltr);
            modes.Add("rtl", DirectionMode.Rtl);
        }

        public CSSDirectionProperty()
            : base(PropertyNames.Direction)
        {
            _mode = DirectionMode.Ltr;
            _inherited = true;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected text direction.
        /// </summary>
        public DirectionMode Direction
        {
            get { return _mode; }
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            DirectionMode mode;

            if (value is CSSIdentifierValue && modes.TryGetValue(((CSSIdentifierValue)value).Value, out mode))
                _mode = mode;
            else if (value != CSSValue.Inherit)
                return false;
            
            return true;
        }

        #endregion
    }
}
