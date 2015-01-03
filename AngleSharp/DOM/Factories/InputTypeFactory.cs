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
            Add(new TextInputType("text"));
            Add(new DateInputType("date"));
            Add(new WeekInputType("week"));
            Add(new DatetimeInputType("datetime"));
            Add(new TimeInputType("time"));
            Add(new MonthInputType("month"));
            Add(new NumberInputType("range"));
            Add(new NumberInputType("number"));
            Add(new ButtonInputType("hidden"));
            Add(new TextInputType("search"));
            Add(new EmailInputType("email"));
            Add(new PatternInputType("tel"));
            Add(new UrlInputType("url"));
            Add(new PatternInputType("password"));
            Add(new ColorInputType("color"));
            Add(new CheckedInputType("checkbox"));
            Add(new CheckedInputType("radio"));
            Add(new FileInputType("file"));
            Add(new SubmitInputType("submit"));
            Add(new ButtonInputType("reset"));
            Add(new ImageInputType("image"));
            Add(new ButtonInputType("button"));
        }

        static void Add(BaseInputType value)
        {
            values.Add(value.Name, value);
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
