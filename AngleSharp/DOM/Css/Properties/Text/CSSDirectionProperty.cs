namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/direction
    /// </summary>
    public sealed class CSSDirectionProperty : CSSProperty
    {
        #region Fields

        DirectionMode _mode;

        #endregion

        #region ctor

        internal CSSDirectionProperty()
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

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            if (value.Is("ltr"))
                _mode = DirectionMode.Ltr;
            else if (value.Is("rtl"))
                _mode = DirectionMode.Rtl;
            else if (value != CSSValue.Inherit)
                return false;
            
            return true;
        }

        #endregion
    }
}
