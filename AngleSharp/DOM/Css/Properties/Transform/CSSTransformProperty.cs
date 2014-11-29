namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/transform
    /// </summary>
    sealed class CSSTransformProperty : CSSProperty, ICssTransformProperty
    {
        #region Fields

        internal static readonly ITransform[] Default = new ITransform[0];
        internal static readonly IValueConverter<ITransform[]> Converter = TakeOne(Keywords.None, Default).Or(TakeMany(WithTransform()));
        readonly List<ITransform> _transforms;

        #endregion

        #region ctor

        internal CSSTransformProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.Transform, rule, PropertyFlags.Animatable)
        {
            _transforms = new List<ITransform>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the enumeration over all transformations.
        /// </summary>
        public IEnumerable<ITransform> Transforms
        {
            get { return _transforms; }
        }

        #endregion

        #region Methods

        public void SetTransforms(IEnumerable<ITransform> transforms)
        {
            _transforms.Clear();
            _transforms.AddRange(transforms);
        }

        internal override void Reset()
        {
            _transforms.Clear();
            _transforms.AddRange(Default);
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            return Converter.TryConvert(value, SetTransforms);
        }

        #endregion
    }
}
