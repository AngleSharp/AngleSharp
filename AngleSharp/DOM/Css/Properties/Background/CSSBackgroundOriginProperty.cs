namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-origins
    /// </summary>
    sealed class CssBackgroundOriginProperty : CssProperty, ICssBackgroundOriginProperty
    {
        #region Fields

        internal static readonly BoxModel Default = BoxModel.PaddingBox;
        internal static readonly IValueConverter<BoxModel> SingleConverter = Map.BoxModels.ToConverter();
        internal static readonly IValueConverter<BoxModel[]> Converter = SingleConverter.FromList();
        readonly List<BoxModel> _origins;

        #endregion

        #region ctor

        internal CssBackgroundOriginProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BackgroundOrigin, rule)
        {
            _origins = new List<BoxModel>();
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets an enumeration with the desired origin settings.
        /// </summary>
        public IEnumerable<BoxModel> Origins
        {
            get { return _origins; }
        }

        #endregion

        #region Methods

        public void SetOrigins(IEnumerable<BoxModel> origins)
        {
            _origins.Clear();
            _origins.AddRange(origins);
        }

        internal override void Reset()
        {
            _origins.Clear();
            _origins.Add(Default);
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetOrigins);
        }

        #endregion
    }
}
