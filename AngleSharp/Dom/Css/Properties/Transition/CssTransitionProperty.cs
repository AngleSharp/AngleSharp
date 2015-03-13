namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/transition
    /// </summary>
    sealed class CssTransitionProperty : CssShorthandProperty
    {
        #region Fields

        internal static readonly IValueConverter<Tuple<ICssValue, ICssValue, ICssValue, ICssValue>[]> Converter = 
            Converters.WithAny(
                Converters.AnimatableConverter.Val().Option(),
                Converters.TimeConverter.Val().Option(),
                Converters.TransitionConverter.Val().Option(),
                Converters.TimeConverter.Val().Option()).FromList();

        #endregion

        #region ctor

        internal CssTransitionProperty(CssStyleDeclaration rule)
            : base(PropertyNames.Transition, rule)
        {
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, t =>
            {
                Get<CssTransitionPropertyProperty>().TrySetValue(Transform(t.Select(m => m.Item1)));
                Get<CssTransitionDurationProperty>().TrySetValue(Transform(t.Select(m => m.Item2)));
                Get<CssTransitionTimingFunctionProperty>().TrySetValue(Transform(t.Select(m => m.Item3)));
                Get<CssTransitionDelayProperty>().TrySetValue(Transform(t.Select(m => m.Item4)));
            });
        }

        ICssValue Transform(IEnumerable<ICssValue> values)
        {
            if (values.Count() > 1)
                return new CssValueList(values.ToList());

            return values.FirstOrDefault();
        }

        internal override String SerializeValue(IEnumerable<CssProperty> properties)
        {
            var property = properties.OfType<CssTransitionPropertyProperty>().FirstOrDefault();
            var duration = properties.OfType<CssTransitionDurationProperty>().FirstOrDefault();
            var timingFunction = properties.OfType<CssTransitionTimingFunctionProperty>().FirstOrDefault();
            var delay = properties.OfType<CssTransitionDelayProperty>().FirstOrDefault();

            if (property == null || duration == null)
                return String.Empty;

            var values = new List<String>();
            values.Add(property.SerializeValue());
            values.Add(duration.SerializeValue());

            if (timingFunction != null && timingFunction.HasValue)
                values.Add(timingFunction.SerializeValue());

            if (delay != null && delay.HasValue)
                values.Add(delay.SerializeValue());

            return String.Join(" ", values);
        }

        #endregion
    }
}
