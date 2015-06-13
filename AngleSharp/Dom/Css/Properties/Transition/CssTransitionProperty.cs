namespace AngleSharp.Dom.Css
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/transition
    /// </summary>
    sealed class CssTransitionProperty : CssShorthandProperty
    {
        #region Fields

        //TODO Convert instead of validate
        /*
            Get<CssTransitionPropertyProperty>().TrySetValue(Transform(t.Select(m => m.Item1)));
            Get<CssTransitionDurationProperty>().TrySetValue(Transform(t.Select(m => m.Item2)));
            Get<CssTransitionTimingFunctionProperty>().TrySetValue(Transform(t.Select(m => m.Item3)));
            Get<CssTransitionDelayProperty>().TrySetValue(Transform(t.Select(m => m.Item4)));
        */
        internal static readonly IValueConverter ListConverter = Converters.WithAny(
            Converters.AnimatableConverter.Option(),
            Converters.TimeConverter.Option(),
            Converters.TransitionConverter.Option(),
            Converters.TimeConverter.Option()).FromList();

        #endregion

        #region ctor

        internal CssTransitionProperty()
            : base(PropertyNames.Transition)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return ListConverter; }
        }

        #endregion

        #region Methods

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
