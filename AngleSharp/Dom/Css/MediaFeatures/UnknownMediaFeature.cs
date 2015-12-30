namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using System;

    sealed class UnknownMediaFeature : MediaFeature
    {
        #region ctor

        public UnknownMediaFeature(String name)
            : base(name)
        {
        }

        #endregion

        #region Internal Properties

        internal override IValueConverter Converter
        {
            get { return Converters.Any; }
        }

        #endregion

        #region Methods

        public override Boolean Validate(RenderDevice device)
        {
            return true;
        }

        #endregion
    }
}
