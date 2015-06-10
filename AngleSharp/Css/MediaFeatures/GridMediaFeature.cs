namespace AngleSharp.Css.MediaFeatures
{
    using System;
    using AngleSharp.Dom.Css;
    using AngleSharp.Extensions;

    sealed class GridMediaFeature : MediaFeature
    {
        #region Fields

        static readonly IValueConverter<Int32> TheConverter = Converters.IntegerConverter.Constraint(m => m == 1 || m == 0);

        #endregion

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
            get { return TheConverter; }
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
