namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/perspective-origin
    /// </summary>
    sealed class CSSPerspectiveOriginProperty : CSSProperty, ICssPerspectiveOriginProperty
    {
        #region Fields

        IDistance _x;
        IDistance _y;

        #endregion

        #region ctor

        internal CSSPerspectiveOriginProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.PerspectiveOrigin, rule, PropertyFlags.Animatable)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the position of the abscissa of the vanishing point.
        /// </summary>
        public IDistance X
        {
            get { return _x; }
        }

        /// <summary>
        /// Gets the position of the ordinate of the vanishing point.
        /// </summary>
        public IDistance Y
        {
            get { return _y; }
        }

        #endregion

        #region Methods

        public void SetPosition(IDistance x, IDistance y)
        {
            _x = x;
            _y = y;
        }

        internal override void Reset()
        {
            _x = Percent.Fifty;
            _y = Percent.Fifty;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            return this.WithDistance().To(m => new Point(m, m)).Or(
                    this.TakeOne(Keywords.Left, new Point(Percent.Zero, Percent.Fifty)).Or(
                    this.TakeOne(Keywords.Center, new Point(Percent.Fifty, Percent.Fifty))).Or(
                    this.TakeOne(Keywords.Right, new Point(Percent.Hundred, Percent.Fifty))).Or(
                    this.TakeOne(Keywords.Top, new Point(Percent.Fifty, Percent.Zero))).Or(
                    this.TakeOne(Keywords.Bottom, new Point(Percent.Fifty, Percent.Hundred)))).Or(
                this.WithOptions(
                    this.WithDistance().Or(this.TakeOne<IDistance>(Keywords.Left, Percent.Zero)).Or(this.TakeOne<IDistance>(Keywords.Right, Percent.Hundred)).Or(this.TakeOne<IDistance>(Keywords.Center, Percent.Fifty)),
                    this.WithDistance().Or(this.TakeOne<IDistance>(Keywords.Top, Percent.Zero)).Or(this.TakeOne<IDistance>(Keywords.Bottom, Percent.Hundred)).Or(this.TakeOne<IDistance>(Keywords.Center, Percent.Fifty)),
                    Tuple.Create((IDistance)Percent.Fifty, (IDistance)Percent.Fifty)).To(m => new Point(m.Item1, m.Item2))
                ).TryConvert(value, m => SetPosition(m.X, m.Y));
        }

        #endregion
    }
}
