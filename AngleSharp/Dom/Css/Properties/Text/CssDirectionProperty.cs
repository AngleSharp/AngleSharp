namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/direction
    /// </summary>
    sealed class CssDirectionProperty : CssProperty, ICssDirectionProperty
    {
        #region Fields

        internal static readonly DirectionMode Default = DirectionMode.Ltr;
        internal static readonly IValueConverter<DirectionMode> Converter = Converters.Assign(Keywords.Ltr, DirectionMode.Ltr).Or(Keywords.Rtl, DirectionMode.Rtl);
        DirectionMode _mode;

        #endregion

        #region ctor

        internal CssDirectionProperty(CssStyleDeclaration rule)
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

        public void SetState(DirectionMode mode)
        {
            _mode = mode;
        }

        internal override void Reset()
        {
            _mode = Default;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetState);
        }

        #endregion
    }
}
