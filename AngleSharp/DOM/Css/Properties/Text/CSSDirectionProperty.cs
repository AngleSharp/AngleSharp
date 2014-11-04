namespace AngleSharp.DOM.Css
{
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/direction
    /// </summary>
    sealed class CSSDirectionProperty : CSSProperty, ICssDirectionProperty
    {
        #region Fields

        DirectionMode _mode;

        #endregion

        #region ctor

        internal CSSDirectionProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.Direction, rule, PropertyFlags.Inherited)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected text direction.
        /// </summary>
        public DirectionMode State
        {
            get { return _mode; }
        }

        #endregion

        #region Methods

        internal override void Reset()
        {
            _mode = DirectionMode.Ltr;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            if (value.Is(Keywords.Ltr))
                _mode = DirectionMode.Ltr;
            else if (value.Is(Keywords.Rtl))
                _mode = DirectionMode.Rtl;
            else
                return false;
            
            return true;
        }

        #endregion
    }
}
