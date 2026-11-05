namespace AngleSharp.Html.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Events;
    using AngleSharp.Html.Dom.Events;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Members of forms added to the standard later. An extension, as what implements the
    /// interface would break otherwise.
    /// </summary>
    [DomExposed("HTMLFormElement")]
    public static class HtmlFormMemberExtensions
    {
        /// <summary>
        /// Submits the form as a click on a submit button does, i.e. after validating it and
        /// firing its submit event, which can prevent it.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="submitter">The button doing it, if any.</param>
        [DomName("requestSubmit")]
        public static void RequestSubmit(this IHtmlFormElement form, IHtmlElement? submitter = null)
        {
            _ = RequestSubmitAsync(form, submitter);
        }

        internal static Task<IDocument> RequestSubmitAsync(IHtmlFormElement form, IHtmlElement? submitter)
        {
            if (submitter is not null)
            {
                if (!SubmitEvent.IsSubmitButton(submitter))
                {
                    throw new ArgumentException("The submitter must be a submit button.", nameof(submitter));
                }

                var submitterForm = submitter switch
                {
                    IHtmlButtonElement button => button.Form,
                    IHtmlInputElement input => input.Form,
                    _ => null,
                };

                if (!Object.ReferenceEquals(form, submitterForm))
                {
                    throw new DomException(DomError.NotFound);
                }
            }

            var formNoValidate = submitter switch
            {
                IHtmlButtonElement button => button.FormNoValidate,
                IHtmlInputElement input => input.FormNoValidate,
                _ => false,
            };

            if (!form.NoValidate && !formNoValidate && !form.CheckValidity())
            {
                return Task.FromResult<IDocument>(null!);
            }

            var ev = new SubmitEvent(EventNames.Submit, true, true, submitter);

            if (!form.Dispatch(ev))
            {
                return Task.FromResult<IDocument>(null!);
            }

            if (form is HtmlFormElement htmlForm)
            {
                return htmlForm.SubmitRequestAsync(submitter);
            }

            return submitter is null ? form.SubmitAsync() : form.SubmitAsync(submitter);
        }
    }

    /// <summary>
    /// Members of inputs added to the standard later.
    /// </summary>
    [DomExposed("HTMLInputElement")]
    public static class HtmlInputMemberExtensions
    {
        /// <summary>
        /// Tells if the input is valid; nothing is shown to the user here.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>True if it is valid.</returns>
        [DomName("reportValidity")]
        public static Boolean ReportValidity(this IHtmlInputElement input) => input.CheckValidity();
    }

    /// <summary>
    /// Members of selects added to the standard later.
    /// </summary>
    [DomExposed("HTMLSelectElement")]
    public static class HtmlSelectMemberExtensions
    {
        /// <summary>
        /// Gets the option at the position.
        /// </summary>
        /// <param name="select">The select.</param>
        /// <param name="index">The position.</param>
        /// <returns>The option, or null if there is none.</returns>
        [DomName("item")]
        public static IHtmlOptionElement? Item(this IHtmlSelectElement select, Int32 index) =>
            index >= 0 && index < select.Options.Length ? select.Options[index] : null;
    }

    /// <summary>
    /// Members of images added to the standard later.
    /// </summary>
    [DomExposed("HTMLImageElement")]
    public static class HtmlImageMemberExtensions
    {
        /// <summary>
        /// Gets a task for the image being ready, which it is for what is painted here.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns>The task.</returns>
        [DomName("decode")]
        public static Task Decode(this IHtmlImageElement image) => Task.CompletedTask;

        /// <summary>
        /// Gets how the image is decoded.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns>The value of the attribute.</returns>
        [DomName("decoding")]
        [DomAccessor(Accessors.Getter)]
        public static String GetDecoding(this IHtmlImageElement image) => image.GetAttribute("decoding") ?? "auto";

        /// <summary>
        /// Sets how the image is decoded.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="value">The value of the attribute.</param>
        [DomName("decoding")]
        [DomAccessor(Accessors.Setter)]
        public static void SetDecoding(this IHtmlImageElement image, String value) => image.SetAttribute("decoding", value);

        /// <summary>
        /// Gets when the image is loaded.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns>The value of the attribute.</returns>
        [DomName("loading")]
        [DomAccessor(Accessors.Getter)]
        public static String GetLoading(this IHtmlImageElement image) => image.GetAttribute("loading") ?? "eager";

        /// <summary>
        /// Sets when the image is loaded.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="value">The value of the attribute.</param>
        [DomName("loading")]
        [DomAccessor(Accessors.Setter)]
        public static void SetLoading(this IHtmlImageElement image, String value) => image.SetAttribute("loading", value);

        /// <summary>
        /// Gets the referrer policy of the image.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns>The value of the attribute.</returns>
        [DomName("referrerPolicy")]
        [DomAccessor(Accessors.Getter)]
        public static String GetReferrerPolicy(this IHtmlImageElement image) => image.GetAttribute("referrerpolicy") ?? String.Empty;

        /// <summary>
        /// Sets the referrer policy of the image.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="value">The value of the attribute.</param>
        [DomName("referrerPolicy")]
        [DomAccessor(Accessors.Setter)]
        public static void SetReferrerPolicy(this IHtmlImageElement image, String value) => image.SetAttribute("referrerpolicy", value);
    }
    
    /// <summary>
    /// Members of HTML dialog elements exposed for convenience.
    /// </summary>
    public static class HtmlDialogMemberExtensions
    {
        /// <summary>
        /// Gets if the dialog is a modal. Only relevant if "open" is true.
        /// </summary>
        /// <param name="dialog">The dialog element.</param>
        /// <returns>True if the dialog has been opened as a modal, false otherwise.</returns>
        public static Boolean GetIsModal(this IHtmlDialogElement dialog)
        {
            if (dialog is HtmlDialogElement d)
            {
                return d.IsModal;
            }

            return false;
        }

        /// <summary>
        /// Gets the element, where the dialog is anchored.
        /// Only relevant if "open" is true.
        /// </summary>
        /// <param name="dialog">The dialog element.</param>
        /// <returns>The element, where the dialog is anchored, or null if not applicable.</returns>
        public static IElement? GetAnchor(this IHtmlDialogElement dialog)
        {
            if (dialog is HtmlDialogElement d)
            {
                return d.Anchor;
            }

            return null;
        }
    }
}
