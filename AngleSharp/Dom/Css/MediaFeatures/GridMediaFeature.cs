namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using System;

    sealed class GridMediaFeature : MediaFeature
    {
        #region ctor

        public GridMediaFeature()
            : base(FeatureNames.Grid)
        {
        }

        #endregion

        #region Internal Properties

        internal override IValueConverter Converter
        {
            // Default: Allowed
            get { return Converters.BinaryConverter; }
        }

        #endregion

        #region Methods

        public override Boolean Validate(RenderDevice device)
        {
            var grid = false;
            var desired = grid;
            var available = device.IsGrid;
            return desired == available;
        }

        #endregion
    }
}
