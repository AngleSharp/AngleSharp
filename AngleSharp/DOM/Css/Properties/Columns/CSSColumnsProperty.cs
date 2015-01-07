namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/columns
    /// </summary>
    sealed class CSSColumnsProperty : CSSShorthandProperty, ICssColumnsProperty
    {
        #region Fields

        internal static readonly IValueConverter<Tuple<Length?, Int32?>> Converter = Converters.WithAny(
            CSSColumnWidthProperty.Converter.Option(CSSColumnWidthProperty.Default),
            CSSColumnCountProperty.Converter.Option(CSSColumnCountProperty.Default));

        readonly CSSColumnCountProperty _count;
        readonly CSSColumnWidthProperty _width;

        #endregion

        #region ctor

        internal CSSColumnsProperty(CssStyleDeclaration rule)
            : base(PropertyNames.Columns, rule, PropertyFlags.Animatable)
        {
            _count = Get<CSSColumnCountProperty>();
            _width = Get<CSSColumnWidthProperty>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the width for the columns, if set.
        /// </summary>
        public Length? Width
        {
            get { return _width.Width; }
        }

        /// <summary>
        /// Gets the count for the columns, if set.
        /// </summary>
        public Int32? Count
        {
            get { return _count.Count; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, m =>
            {
                _width.SetWidth(m.Item1);
                _count.SetCount(m.Item2);
            });
        }

        internal override String SerializeValue(IEnumerable<CSSProperty> properties)
        {
            if (!IsComplete(properties))
                return String.Empty;

            return String.Concat(_width.SerializeValue(), " ", _count.SerializeValue());
        }

        #endregion
    }
}
