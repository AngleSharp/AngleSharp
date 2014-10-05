namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Basis for all elementary margin properties.
    /// </summary>
    abstract class CSSMarginPartProperty : CSSProperty
    {
        #region Fields

        IDistance _margin;

        #endregion

        #region ctor

        internal CSSMarginPartProperty(String name)
            : base(name, PropertyFlags.Unitless | PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the margin is automatically determined.
        /// </summary>
        public Boolean IsAuto
        {
            get { return _margin == null; }
        }

        /// <summary>
        /// Gets the margin relative to the width of the containing block or
        /// a fixed width, if any.
        /// </summary>
        internal IDistance Margin
        {
            get { return _margin; }
        }

        #endregion

        #region Methods

        protected override void Reset()
        {
            _margin = Percent.Zero;
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
                _margin = distance;
            else if (value.Is(Keywords.Auto))
                _margin = null;
            else
                return false;

            return true;
        }

        #endregion
    }
}
