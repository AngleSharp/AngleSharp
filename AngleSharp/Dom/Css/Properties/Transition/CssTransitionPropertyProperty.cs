namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/transition-property
    /// </summary>
    sealed class CssTransitionPropertyProperty : CssProperty
    {
        #region Fields

        internal static readonly IValueConverter<String> SingleConverter = Converters.AnimatableConverter;
        internal static readonly IValueConverter<String[]> Converter = SingleConverter.FromList().Or(Keywords.None, new String[0]);
        internal static readonly String Default = Keywords.All;
        readonly List<String> _properties;
        
        #endregion

        #region ctor

        internal CssTransitionPropertyProperty(CssStyleDeclaration rule)
            : base(PropertyNames.TransitionProperty, rule)
        {
            _properties = new List<String>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the names of the selected properties.
        /// </summary>
        public IEnumerable<String> Properties
        {
            get { return _properties; }
        }

        #endregion

        #region Methods

        public void SetProperties(IEnumerable<String> properties)
        {
            _properties.Clear();
            _properties.AddRange(properties);
        }

        internal override void Reset()
        {
            _properties.Clear();
            _properties.Add(Default);
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetProperties);
        }

        #endregion
    }
}
