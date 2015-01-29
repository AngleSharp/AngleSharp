namespace AngleSharp.Factories
{
    using AngleSharp.Dom.Html;
    using AngleSharp.Html;
    using AngleSharp.Html.InputTypes;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides string to InputType instance mappings.
    /// </summary>
    sealed class InputTypeFactory
    {
        readonly Dictionary<String, Func<IHtmlInputElement, BaseInputType>> creators = new Dictionary<String, Func<IHtmlInputElement, BaseInputType>>(StringComparer.OrdinalIgnoreCase)
        {
            { InputTypeNames.Text, input => new TextInputType(input, InputTypeNames.Text) },
            { InputTypeNames.Date, input => new DateInputType(input, InputTypeNames.Date) },
            { InputTypeNames.Week, input => new WeekInputType(input, InputTypeNames.Week) },
            { InputTypeNames.Datetime, input => new DatetimeInputType(input, InputTypeNames.Datetime) },
            { InputTypeNames.DatetimeLocal, input => new DatetimeLocalInputType(input, InputTypeNames.DatetimeLocal) },
            { InputTypeNames.Time, input => new TimeInputType(input, InputTypeNames.Time) },
            { InputTypeNames.Month, input => new MonthInputType(input, InputTypeNames.Month) },
            { InputTypeNames.Range, input => new NumberInputType(input, InputTypeNames.Range) },
            { InputTypeNames.Number, input => new NumberInputType(input, InputTypeNames.Number) },
            { InputTypeNames.Hidden, input => new ButtonInputType(input, InputTypeNames.Hidden) },
            { InputTypeNames.Search, input => new TextInputType(input, InputTypeNames.Search) },
            { InputTypeNames.Email, input => new EmailInputType(input, InputTypeNames.Email) },
            { InputTypeNames.Tel, input => new PatternInputType(input, InputTypeNames.Tel) },
            { InputTypeNames.Url, input => new UrlInputType(input, InputTypeNames.Url) },
            { InputTypeNames.Password, input => new PatternInputType(input, InputTypeNames.Password) },
            { InputTypeNames.Color, input => new ColorInputType(input, InputTypeNames.Color) },
            { InputTypeNames.Checkbox, input => new CheckedInputType(input, InputTypeNames.Checkbox) },
            { InputTypeNames.Radio, input => new CheckedInputType(input, InputTypeNames.Radio) },
            { InputTypeNames.File, input => new FileInputType(input, InputTypeNames.File) },
            { InputTypeNames.Submit, input => new SubmitInputType(input, InputTypeNames.Submit) },
            { InputTypeNames.Reset, input => new ButtonInputType(input, InputTypeNames.Reset) },
            { InputTypeNames.Image, input => new ImageInputType(input, InputTypeNames.Image) },
            { InputTypeNames.Button, input => new ButtonInputType(input, InputTypeNames.Button) }
        };

        /// <summary>
        /// Creates an InputType provider for the provided element.
        /// </summary>
        /// <param name="input">The input element.</param>
        /// <param name="type">The current value of the type attribute.</param>
        /// <returns>The InputType provider or text, if the type is unknown.</returns>
        public BaseInputType Create(IHtmlInputElement input, String type)
        {
            Func<IHtmlInputElement, BaseInputType> creator;

            if (String.IsNullOrEmpty(type))
                type = InputTypeNames.Text;

            if (!creators.TryGetValue(type, out creator))
                creator = creators[InputTypeNames.Text];

            return creator(input);
        }
    }
}
