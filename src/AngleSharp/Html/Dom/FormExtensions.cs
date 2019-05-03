namespace AngleSharp.Html.Dom
{
    using AngleSharp.Common;
    using AngleSharp.Dom;
    using AngleSharp.Html;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// A set of useful extension methods when dealing with the DOM.
    /// </summary>
    public static class FormExtensions
    {
        /// <summary>
        /// Set the field values of given form by using the dictionary which
        /// contains name value pairs of input fields.
        /// </summary>
        /// <param name="form">The form to set.</param>
        /// <param name="fields">The fields to use as values.</param>
        /// <param name="createMissing">
        /// What to do if some field(s) have not been found in the form. If
        /// true, then new input will be created. Otherwise, an exception will
        /// be thrown.
        /// </param>
        /// <returns>The given form for chaining.</returns>
        public static IHtmlFormElement SetValues(this IHtmlFormElement form, IDictionary<String, String> fields, Boolean createMissing = false)
        {
            if (form == null)
                throw new ArgumentNullException(nameof(form));

            if (fields == null)
                throw new ArgumentNullException(nameof(fields));

            var inputs = form.Elements.OfType<HtmlFormControlElement>();

            foreach (var field in fields)
            {
                var targetInput = inputs.FirstOrDefault(e => e.Name.Is(field.Key));

                if (targetInput != null)
                {
                    if (targetInput is IHtmlInputElement input)
                    {
                        if (input.Type.Is(InputTypeNames.Radio))
                        {
                            var radios = inputs.OfType<IHtmlInputElement>().Where(i => i.Name.Is(targetInput.Name));

                            foreach (var radio in radios)
                            {
                                radio.IsChecked = radio.Value.Is(field.Value);
                            }
                        }
                        else
                        {
                            input.Value = field.Value;
                        }
                    }
                    else if (targetInput is IHtmlTextAreaElement textarea)
                    {
                        textarea.Value = field.Value;
                    }
                    else if (targetInput is IHtmlSelectElement select)
                    {
                        select.Value = field.Value;
                    }
                    else
                    {
                        //Silently ignore unsupported input type, e.g.,
                        //no idea if modifying keygen fields is really
                        //useful or how it is regulated.
                    }
                }
                else if (createMissing)
                {
                    var newInput = form.Owner.CreateElement<IHtmlInputElement>();
                    newInput.Type = InputTypeNames.Hidden;
                    newInput.Name = field.Key;
                    newInput.Value = field.Value;
                    form.AppendChild(newInput);
                }
                else
                {
                    var message = $"Field {field.Key} not found.";
                    throw new KeyNotFoundException(message);
                }
            }

            return form;
        }

        /// <summary>
        /// Submits the given form by decomposing the object into a dictionary
        /// that contains its properties as name value pairs.
        /// </summary>
        /// <param name="form">The form to submit.</param>
        /// <param name="fields">The fields to use as values.</param>
        /// <returns>The task eventually resulting in the response.</returns>
        public static Task<IDocument> SubmitAsync(this IHtmlFormElement form, Object fields) => form.SubmitAsync(fields.ToDictionary());

        /// <summary>
        /// Submits the given form by using the dictionary which contains name
        /// value pairs of input fields to submit.
        /// </summary>
        /// <param name="form">The form to submit.</param>
        /// <param name="fields">The fields to use as values.</param>
        /// <param name="createMissing">
        /// What to do if some field(s) have not been found in the form. If
        /// true, then new input will be created. Otherwise, an exception will
        /// be thrown.
        /// </param>
        /// <returns>The task eventually resulting in the response.</returns>
        public static Task<IDocument> SubmitAsync(this IHtmlFormElement form, IDictionary<String, String> fields, Boolean createMissing = false)
        {
            form.SetValues(fields, createMissing);
            return form.SubmitAsync();
        }

        /// <summary>
        /// Submits the form of the element by decomposing the object into a dictionary
        /// that contains its properties as name value pairs.
        /// </summary>
        /// <param name="element">The element to submit its form.</param>
        /// <param name="fields">The optional fields to use as values.</param>
        /// <returns>The task eventually resulting in the response.</returns>
        public static Task<IDocument> SubmitAsync(this IHtmlElement element, Object fields = null) => element.SubmitAsync(fields.ToDictionary());

        /// <summary>
        /// Submits the form of the element by using the dictionary which contains name
        /// value pairs of input fields to submit.
        /// </summary>
        /// <param name="element">The element to submit its form.</param>
        /// <param name="fields">The fields to use as values.</param>
        /// <param name="createMissing">
        /// What to do if some field(s) have not been found in the form. If
        /// true, then new input will be created. Otherwise, an exception will
        /// be thrown.
        /// </param>
        /// <returns>The task eventually resulting in the response.</returns>
        public static Task<IDocument> SubmitAsync(this IHtmlElement element, IDictionary<String, String> fields, Boolean createMissing = false)
        {
            if (element is HtmlFormControlElement button)
            {
                var form = button.Form;

                if (form != null)
                {
                    form.SetValues(fields, createMissing);
                    return form.SubmitAsync(button);
                }

                return null;
            }

            throw new ArgumentException(nameof(element));
        }
    }
}
