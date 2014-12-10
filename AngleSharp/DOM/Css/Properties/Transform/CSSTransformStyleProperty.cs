namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/transform-style
    /// </summary>
    sealed class CSSTransformStyleProperty : CSSProperty, ICssTransformStyleProperty
    {
        #region Fields

        internal static readonly IValueConverter<Boolean> Converter = Converters.Toggle(Keywords.Flat, Keywords.Preserve3d);
        internal static readonly Boolean Default = true;
        Boolean _flat;

        #endregion

        #region ctor

        internal CSSTransformStyleProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.TransformStyle, rule)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the children of the element are lying in the plane of
        /// the element itself. Otherwise the children of the element should
        /// be positioned in the 3D-space.
        /// </summary>
        public Boolean IsFlat
        {
            get { return _flat; }
        }

        #endregion

        #region Methods

        public void SetFlat(Boolean flat)
        {
            _flat = flat;
        }

        internal override void Reset()
        {
            _flat = Default;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetFlat);
        }

        #endregion
    }
}
