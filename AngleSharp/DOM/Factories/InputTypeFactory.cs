namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html.InputTypes;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides string to InputType instance mappings.
    /// </summary>
    static class InputTypeFactory
    {
        static readonly Dictionary<String, BaseInputType> values = new Dictionary<String, BaseInputType>(StringComparer.OrdinalIgnoreCase);

        static InputTypeFactory()
        {
            values.Add("text", new TextInputType());
            values.Add("date", new DateInputType());
            values.Add("week", new WeekInputType());
            values.Add("datetime", new DatetimeInputType());
            values.Add("time", new TimeInputType());
            values.Add("month", new MonthInputType());
            values.Add("range", new NumberInputType());
            values.Add("number", new NumberInputType());
            values.Add("hidden", new ButtonInputType());
            values.Add("search", new TextInputType());
            values.Add("email", new EmailInputType());
            values.Add("tel", new PatternInputType());
            values.Add("url", new UrlInputType());
            values.Add("password", new PatternInputType());
            values.Add("color", new ColorInputType());
            values.Add("checkbox", new CheckedInputType());
            values.Add("radio", new CheckedInputType());
            values.Add("file", new FileInputType());
            values.Add("submit", new SubmitInputType());
            values.Add("reset", new ButtonInputType());
            values.Add("image", new ImageInputType());
            values.Add("button", new ButtonInputType());
        }

        /// <summary>
        /// Returns a InputType provider for the element.
        /// </summary>
        /// <param name="type">The type of the input element.</param>
        /// <returns>The InputType provider or text, if the type is unknown.</returns>
        public static BaseInputType Create(String type)
        {
            BaseInputType instance;

            if (values.TryGetValue(type, out instance))
                return instance;

            return values[Keywords.Text];
        }
    }
}
