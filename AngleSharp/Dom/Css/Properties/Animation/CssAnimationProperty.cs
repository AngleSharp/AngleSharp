namespace AngleSharp.Dom.Css
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation
    /// </summary>
    sealed class CssAnimationProperty : CssShorthandProperty
    {
        #region Fields

        //TODO Convert instead of validate
        /*
            Get<CssAnimationDurationProperty>().TrySetValue(Transform(t.Select(m => m.Item1)));
            Get<CssAnimationTimingFunctionProperty>().TrySetValue(Transform(t.Select(m => m.Item2)));
            Get<CssAnimationDelayProperty>().TrySetValue(Transform(t.Select(m => m.Item3)));
            Get<CssAnimationIterationCountProperty>().TrySetValue(Transform(t.Select(m => m.Item4)));
            Get<CssAnimationDirectionProperty>().TrySetValue(Transform(t.Select(m => m.Item5)));
            Get<CssAnimationFillModeProperty>().TrySetValue(Transform(t.Select(m => m.Item6)));
            Get<CssAnimationPlayStateProperty>().TrySetValue(Transform(t.Select(m => m.Item7)));
            Get<CssAnimationNameProperty>().TrySetValue(Transform(t.Select(m => m.Rest.Item1)));
         */
        internal static readonly IValueConverter ListConverter = Converters.WithAny(
            Converters.TimeConverter.Option(),
            Converters.TransitionConverter.Option(),
            Converters.TimeConverter.Option(),
            Converters.PositiveOrInfiniteNumberConverter.Option(),
            Converters.AnimationDirectionConverter.Option(),
            Converters.AnimationFillStyleConverter.Option(),
            Converters.PlayStateConverter.Option(),
            Converters.IdentifierConverter.Option()).FromList();

        #endregion

        #region ctor

        internal CssAnimationProperty()
            : base(PropertyNames.Animation)
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
            var name = properties.OfType<CssAnimationNameProperty>().FirstOrDefault();
            var duration = properties.OfType<CssAnimationDurationProperty>().FirstOrDefault();
            var timingFunction = properties.OfType<CssAnimationTimingFunctionProperty>().FirstOrDefault();
            var delay = properties.OfType<CssAnimationDelayProperty>().FirstOrDefault();
            var iterationCount = properties.OfType<CssAnimationIterationCountProperty>().FirstOrDefault();
            var direction = properties.OfType<CssAnimationDirectionProperty>().FirstOrDefault();
            var fillMode = properties.OfType<CssAnimationFillModeProperty>().FirstOrDefault();
            var playState = properties.OfType<CssAnimationPlayStateProperty>().FirstOrDefault();

            if (name == null || duration == null)
                return String.Empty;

            var values = new List<String>();
            values.Add(name.SerializeValue());
            values.Add(duration.SerializeValue());

            if (timingFunction != null && timingFunction.HasValue)
                values.Add(timingFunction.SerializeValue());

            if (delay != null && delay.HasValue)
                values.Add(delay.SerializeValue());

            if (iterationCount != null && iterationCount.HasValue)
                values.Add(iterationCount.SerializeValue());

            if (direction != null && direction.HasValue)
                values.Add(direction.SerializeValue());

            if (fillMode != null && fillMode.HasValue)
                values.Add(fillMode.SerializeValue());

            if (playState != null && playState.HasValue)
                values.Add(playState.SerializeValue());
            
            return String.Join(" ", values);
        }

        #endregion
    }
}
