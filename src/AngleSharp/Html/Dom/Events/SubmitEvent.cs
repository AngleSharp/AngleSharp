namespace AngleSharp.Html.Dom.Events
{
    using AngleSharp.Attributes;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Events;
    using System;

    /// <summary>
    /// Represents the event dispatched when a form is submitted.
    /// </summary>
    [DomName("SubmitEvent")]
    [DomExposed("Window")]
    public class SubmitEvent : Event
    {
        /// <summary>
        /// Creates a new event and initializes it with the members of a SubmitEventInit.
        /// </summary>
        /// <param name="type">The type of the event.</param>
        /// <param name="bubbles">If the event bubbles.</param>
        /// <param name="cancelable">If the event is cancelable.</param>
        /// <param name="submitter">The button that initiated the form submission.</param>
        [DomConstructor]
        [DomInitDict(offset: 1, optional: true)]
        public SubmitEvent(String type, Boolean bubbles = false, Boolean cancelable = false, IHtmlElement? submitter = null)
            : base(type, bubbles, cancelable)
        {
            if (submitter is not null && !IsSubmitButton(submitter))
            {
                throw new ArgumentException("The submitter must be a submit button.", nameof(submitter));
            }

            Submitter = submitter;
        }

        /// <summary>
        /// Gets the button that initiated the form submission, if any.
        /// </summary>
        [DomName("submitter")]
        public IHtmlElement? Submitter
        {
            get;
        }

        internal static Boolean IsSubmitButton(IHtmlElement element) =>
            element is IHtmlButtonElement button && String.Equals(button.Type, InputTypeNames.Submit, StringComparison.OrdinalIgnoreCase) ||
            element is IHtmlInputElement input && (String.Equals(input.Type, InputTypeNames.Submit, StringComparison.OrdinalIgnoreCase) ||
            String.Equals(input.Type, InputTypeNames.Image, StringComparison.OrdinalIgnoreCase));
    }
}
