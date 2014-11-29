namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/transform-origin
    /// </summary>
    sealed class CSSTransformOriginProperty : CSSProperty, ICssTransformOriginProperty
    {
        #region Fields

        IDistance _x;
        IDistance _y;
        Length _z;

        #endregion

        #region ctor

        internal CSSTransformOriginProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.TransformOrigin, rule, PropertyFlags.Animatable)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets how far from the left edge of the box the origin of the transform is set.
        /// </summary>
        public IDistance X
        {
            get { return _x; }
        }

        /// <summary>
        /// Gets how far from the top edge of the box the origin of the transform is set.
        /// </summary>
        public IDistance Y
        {
            get { return _y; }
        }

        /// <summary>
        /// Gets how far from the user eye the z = 0 origin is set.
        /// </summary>
        public Length Z
        {
            get { return _z; }
        }

        #endregion

        #region Methods

        public void SetPosition(IDistance x, IDistance y, Length z)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        internal override void Reset()
        {
            _x = Percent.Fifty;
            _y = Percent.Fifty;
            _z = Length.Zero;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            return WithDistance().To(m => Tuple.Create(m, m, Length.Zero)).Or(
                    TakeOne(Keywords.Left, Tuple.Create<IDistance, IDistance, Length>(Percent.Zero, Percent.Fifty, Length.Zero)).Or(
                    TakeOne(Keywords.Center, Tuple.Create<IDistance, IDistance, Length>(Percent.Fifty, Percent.Fifty, Length.Zero))).Or(
                    TakeOne(Keywords.Right, Tuple.Create<IDistance, IDistance, Length>(Percent.Hundred, Percent.Fifty, Length.Zero))).Or(
                    TakeOne(Keywords.Top, Tuple.Create<IDistance, IDistance, Length>(Percent.Fifty, Percent.Zero, Length.Zero))).Or(
                    TakeOne(Keywords.Bottom, Tuple.Create<IDistance, IDistance, Length>(Percent.Fifty, Percent.Hundred, Length.Zero)))).Or(
                WithOptions(
                    WithDistance().Or(TakeOne<IDistance>(Keywords.Left, Percent.Zero)).Or(TakeOne<IDistance>(Keywords.Right, Percent.Hundred)).Or(TakeOne<IDistance>(Keywords.Center, Percent.Fifty)),
                    WithDistance().Or(TakeOne<IDistance>(Keywords.Top, Percent.Zero)).Or(TakeOne<IDistance>(Keywords.Bottom, Percent.Hundred)).Or(TakeOne<IDistance>(Keywords.Center, Percent.Fifty)),
                    WithLength(),
                    Tuple.Create<IDistance, IDistance, Length>(Percent.Fifty, Percent.Fifty, Length.Zero))
                ).TryConvert(value, m => SetPosition(m.Item1, m.Item2, m.Item3));
        }

        #endregion
    }
}
