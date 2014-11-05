namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Basis for all properties that have either a length
    /// or percentage value or an auto value - nothing else.
    /// </summary>
    abstract class CSSCoordinateProperty : CSSProperty
    {
        #region Fields

        IDistance _value;

        #endregion

        #region ctor

        internal CSSCoordinateProperty(String name, CSSStyleDeclaration rule)
            : base(name, rule, PropertyFlags.Unitless | PropertyFlags.Animatable)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the position is automatically calculated.
        /// </summary>
        public Boolean IsAuto
        {
            get { return _value == null; }
        }

        /// <summary>
        /// Gets the position if a fixed position has been set.
        /// </summary>
        public IDistance Position
        {
            get { return _value; }
        }

        #endregion

        #region Methods

        internal override void Reset()
        {
            _value = null;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            var distance = value.ToDistance();

            if (distance != null)
                _value = distance;
            else if (value.Is(Keywords.Auto))
                _value = null;
            else
                return false;

            return true;
        }

        #endregion
    }
}
