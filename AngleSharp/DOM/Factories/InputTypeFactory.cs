namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;
    using AngleSharp.Html.InputTypes;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides string to InputType instance mappings.
    /// </summary>
    static class InputTypeFactory
    {
        static readonly Dictionary<String, Func<IHtmlInputElement, BaseInputType>> values = new Dictionary<String, Func<IHtmlInputElement, BaseInputType>>(StringComparer.OrdinalIgnoreCase);

        static InputTypeFactory()
        {
            values.Add(InputTypeNames.Text, input => new TextInputType(input, InputTypeNames.Text));
            values.Add(InputTypeNames.Date, input => new DateInputType(input, InputTypeNames.Date));
            values.Add(InputTypeNames.Week, input => new WeekInputType(input, InputTypeNames.Week));
            values.Add(InputTypeNames.Datetime, input => new DatetimeInputType(input, InputTypeNames.Datetime));
            values.Add(InputTypeNames.DatetimeLocal, input => new DatetimeLocalInputType(input, InputTypeNames.DatetimeLocal));
            values.Add(InputTypeNames.Time, input => new TimeInputType(input, InputTypeNames.Time));
            values.Add(InputTypeNames.Month, input => new MonthInputType(input, InputTypeNames.Month));
            values.Add(InputTypeNames.Range, input => new NumberInputType(input, InputTypeNames.Range));
            values.Add(InputTypeNames.Number, input => new NumberInputType(input, InputTypeNames.Number));
            values.Add(InputTypeNames.Hidden, input => new ButtonInputType(input, InputTypeNames.Hidden));
            values.Add(InputTypeNames.Search, input => new TextInputType(input, InputTypeNames.Search));
            values.Add(InputTypeNames.Email, input => new EmailInputType(input, InputTypeNames.Email));
            values.Add(InputTypeNames.Tel, input => new PatternInputType(input, InputTypeNames.Tel));
            values.Add(InputTypeNames.Url, input => new UrlInputType(input, InputTypeNames.Url));
            values.Add(InputTypeNames.Password, input => new PatternInputType(input, InputTypeNames.Password));
            values.Add(InputTypeNames.Color, input => new ColorInputType(input, InputTypeNames.Color));
            values.Add(InputTypeNames.Checkbox, input => new CheckedInputType(input, InputTypeNames.Checkbox));
            values.Add(InputTypeNames.Radio, input => new CheckedInputType(input, InputTypeNames.Radio));
            values.Add(InputTypeNames.File, input => new FileInputType(input, InputTypeNames.File));
            values.Add(InputTypeNames.Submit, input => new SubmitInputType(input, InputTypeNames.Submit));
            values.Add(InputTypeNames.Reset, input => new ButtonInputType(input, InputTypeNames.Reset));
            values.Add(InputTypeNames.Image, input => new ImageInputType(input, InputTypeNames.Image));
            values.Add(InputTypeNames.Button, input => new ButtonInputType(input, InputTypeNames.Button));
        }

        /// <summary>
        /// Creates an InputType provider for the provided element.
        /// </summary>
        /// <param name="input">The input element.</param>
        /// <param name="type">The current value of the type attribute.</param>
        /// <returns>The InputType provider or text, if the type is unknown.</returns>
        public static BaseInputType Create(IHtmlInputElement input, String type)
        {
            Func<IHtmlInputElement, BaseInputType> creator;

            if (String.IsNullOrEmpty(type))
                type = InputTypeNames.Text;

            if (!values.TryGetValue(type, out creator))
                creator = values[InputTypeNames.Text];

            return creator(input);
        }
    }
}
