namespace AngleSharp.Html
{
    using AngleSharp.Html.Dom;
    using AngleSharp.Html.InputTypes;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides string to InputType instance mappings.
    /// </summary>
    public class DefaultInputTypeFactory : IInputTypeFactory
    {
        /// <summary>
        /// Represents a creator delegate for creating input type providers.
        /// </summary>
        /// <param name="input">The input to create the provider for.</param>
        /// <returns>The created input type provider.</returns>
        public delegate BaseInputType Creator(IHtmlInputElement input);

        private readonly Dictionary<String, Creator> _creators = new Dictionary<String, Creator>(StringComparer.OrdinalIgnoreCase)
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
        /// Registers a new creator for the specified input type.
        /// Throws an exception if another creator for the given
        /// input type is already added.
        /// </summary>
        /// <param name="type">The input type value.</param>
        /// <param name="creator">The creator to invoke.</param>
        public void Register(String type, Creator creator)
        {
            _creators.Add(type, creator);
        }

        /// <summary>
        /// Unregisters an existing creator for the given input type.
        /// </summary>
        /// <param name="type">The input type value.</param>
        /// <returns>The registered creator, if any.</returns>
        public Creator Unregister(String type)
        {
            if (_creators.TryGetValue(type, out var creator))
            {
                _creators.Remove(type);
            }

            return creator;
        }

        /// <summary>
        /// Creates the default InputType provider for the given input element
        /// and input type. By default this is the text input type.
        /// </summary>
        /// <param name="input">The input element.</param>
        /// <param name="type">The current value of the type attribute.</param>
        /// <returns>The InputType provider instance.</returns>
        protected virtual BaseInputType CreateDefault(IHtmlInputElement input, String type)
        {
            var creator = _creators[InputTypeNames.Text];
            return creator.Invoke(input);
        }

        /// <summary>
        /// Creates an InputType provider for the provided element.
        /// </summary>
        /// <param name="input">The input element.</param>
        /// <param name="type">The current value of the type attribute.</param>
        /// <returns>The InputType provider instance.</returns>
        public BaseInputType Create(IHtmlInputElement input, String type)
        {
            if (!String.IsNullOrEmpty(type) && _creators.TryGetValue(type, out var creator))
            {
                return creator.Invoke(input);
            }

            return CreateDefault(input, type);
        }
    }
}
