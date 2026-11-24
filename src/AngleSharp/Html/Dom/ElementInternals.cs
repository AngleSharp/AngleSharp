namespace AngleSharp.Html.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Events;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// The internals of a custom element (HTML 4.13.7), which make it a form-associated element:
    /// it gets a form owner, a value that is submitted with the form, and validity.
    /// </summary>
    [DomName("ElementInternals")]
    public sealed class ElementInternals
    {
        private static readonly ConditionalWeakTable<IHtmlElement, ElementInternals> _attached = new();

        private readonly IHtmlElement _target;
        private readonly ValidityState _validity = new();
        private readonly CustomStateSet _states = new();
        private String? _value;
        private String? _state;
        private String _validationMessage = String.Empty;

        private ElementInternals(IHtmlElement target)
        {
            _target = target;
        }

        /// <summary>
        /// Gets the form owner of the element: the form its form attribute names, otherwise the
        /// closest form it is in.
        /// </summary>
        [DomName("form")]
        public IHtmlFormElement? Form
        {
            get
            {
                var id = _target.GetAttribute(AttributeNames.Form);

                if (id is not null)
                {
                    return (_target.Owner?.GetElementById(id) as IHtmlFormElement);
                }

                return _target.GetAncestor<IHtmlFormElement>();
            }
        }

        /// <summary>
        /// Gets the shadow root of the element, if it has one.
        /// </summary>
        [DomName("shadowRoot")]
        public IShadowRoot? ShadowRoot => _target.ShadowRoot;

        /// <summary>
        /// Gets the custom states of the element, which :state() matches.
        /// </summary>
        [DomName("states")]
        public CustomStateSet States => _states;

        /// <summary>
        /// Gets the validity of the element.
        /// </summary>
        [DomName("validity")]
        public IValidityState Validity => _validity;

        /// <summary>
        /// Gets the message of the validity, if any.
        /// </summary>
        [DomName("validationMessage")]
        public String ValidationMessage => _validity.IsValid ? String.Empty : _validationMessage;

        /// <summary>
        /// Gets if the element is a candidate for constraint validation.
        /// </summary>
        [DomName("willValidate")]
        public Boolean WillValidate => !_target.HasAttribute(AttributeNames.Disabled) && !_target.HasAttribute(AttributeNames.Readonly);

        /// <summary>
        /// Gets the value submitted with the form, if any.
        /// </summary>
        public String? SubmissionValue => _value;

        /// <summary>
        /// Gets the state given with the value, if any.
        /// </summary>
        public String? State => _state;

        /// <summary>
        /// Sets the value submitted with the form; null submits nothing.
        /// </summary>
        /// <param name="value">The value to submit.</param>
        /// <param name="state">The state to restore the element with.</param>
        [DomName("setFormValue")]
        public void SetFormValue(String? value, String? state = null)
        {
            _value = value;
            _state = state ?? value;
        }

        /// <summary>
        /// Sets the validity of the element; without a flag set it is valid.
        /// </summary>
        /// <param name="flags">The constraints that are not met.</param>
        /// <param name="message">The message describing what is not met.</param>
        /// <param name="anchor">The element to report the problem at.</param>
        [DomName("setValidity")]
        public void SetValidity(ValidityStateFlags? flags = null, String? message = null, IHtmlElement? anchor = null)
        {
            var errors = flags?.ToErrors() ?? ValidationErrors.None;

            if (errors != ValidationErrors.None && String.IsNullOrEmpty(message))
            {
                throw new DomException(DomError.TypeMismatch);
            }

            _validity.Reset(errors);
            _validationMessage = errors == ValidationErrors.None ? String.Empty : message!;
        }

        /// <summary>
        /// Checks the validity, firing invalid at the element if it is not valid.
        /// </summary>
        /// <returns>True if the element is valid, otherwise false.</returns>
        [DomName("checkValidity")]
        public Boolean CheckValidity()
        {
            if (!WillValidate || _validity.IsValid)
            {
                return true;
            }

            _target.Dispatch(new Event(EventNames.Invalid, false, true));
            return false;
        }

        /// <summary>
        /// Checks the validity and reports a problem to the user.
        /// </summary>
        /// <returns>True if the element is valid, otherwise false.</returns>
        [DomName("reportValidity")]
        public Boolean ReportValidity() => CheckValidity();

        /// <summary>
        /// Gets the internals attached to the element, if any.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The internals or null.</returns>
        public static ElementInternals? Of(IElement element) =>
            element is IHtmlElement html && _attached.TryGetValue(html, out var internals) ? internals : null;

        /// <summary>
        /// Gets the value the element submits with the given form: what its internals hold if
        /// it is associated with the form and not disabled, otherwise null.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="form">The form that is submitted.</param>
        /// <returns>The value to submit, if any.</returns>
        public static String? GetSubmissionValue(IElement element, IHtmlFormElement form) =>
            Of(element) is { } internals && internals.Form == form && !element.HasAttribute(AttributeNames.Disabled)
                ? internals._value
                : null;

        internal static ElementInternals Attach(IHtmlElement element)
        {
            if (_attached.TryGetValue(element, out _))
            {
                throw new DomException(DomError.NotSupported);
            }

            var internals = new ElementInternals(element);
            _attached.Add(element, internals);
            return internals;
        }
    }

    /// <summary>
    /// The custom states of a custom element (HTML 4.13.7.6), e.g. matched by :state(checked).
    /// </summary>
    [DomName("CustomStateSet")]
    public sealed class CustomStateSet : IEnumerable<String>
    {
        private readonly List<String> _states = new();

        internal CustomStateSet()
        {
        }

        /// <summary>
        /// Gets the number of states.
        /// </summary>
        [DomName("size")]
        public Int32 Size => _states.Count;

        /// <summary>
        /// Adds a state.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>The set.</returns>
        [DomName("add")]
        public CustomStateSet Add(String state)
        {
            if (!_states.Contains(state))
            {
                _states.Add(state);
            }

            return this;
        }

        /// <summary>
        /// Removes a state.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>True if it was there.</returns>
        [DomName("delete")]
        public Boolean Delete(String state) => _states.Remove(state);

        /// <summary>
        /// Checks for a state.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>True if it is there.</returns>
        [DomName("has")]
        public Boolean Has(String state) => _states.Contains(state);

        /// <summary>
        /// Removes all states.
        /// </summary>
        [DomName("clear")]
        public void Clear() => _states.Clear();

        /// <inheritdoc />
        public IEnumerator<String> GetEnumerator() => _states.ToList().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    /// <summary>
    /// The constraints a custom element does not meet (HTML 4.13.7.4), given to setValidity.
    /// </summary>
    public sealed class ValidityStateFlags
    {
        /// <summary>Gets or sets if a required value is missing.</summary>
        [DomName("valueMissing")]
        public Boolean ValueMissing { get; set; }

        /// <summary>Gets or sets if the value is of the wrong type.</summary>
        [DomName("typeMismatch")]
        public Boolean TypeMismatch { get; set; }

        /// <summary>Gets or sets if the value does not match the pattern.</summary>
        [DomName("patternMismatch")]
        public Boolean PatternMismatch { get; set; }

        /// <summary>Gets or sets if the value is too long.</summary>
        [DomName("tooLong")]
        public Boolean TooLong { get; set; }

        /// <summary>Gets or sets if the value is too short.</summary>
        [DomName("tooShort")]
        public Boolean TooShort { get; set; }

        /// <summary>Gets or sets if the value is below the minimum.</summary>
        [DomName("rangeUnderflow")]
        public Boolean RangeUnderflow { get; set; }

        /// <summary>Gets or sets if the value is above the maximum.</summary>
        [DomName("rangeOverflow")]
        public Boolean RangeOverflow { get; set; }

        /// <summary>Gets or sets if the value does not fit the step.</summary>
        [DomName("stepMismatch")]
        public Boolean StepMismatch { get; set; }

        /// <summary>Gets or sets if the input can't be converted.</summary>
        [DomName("badInput")]
        public Boolean BadInput { get; set; }

        /// <summary>Gets or sets if a custom constraint is not met.</summary>
        [DomName("customError")]
        public Boolean CustomError { get; set; }

        internal ValidationErrors ToErrors() =>
            (ValueMissing ? ValidationErrors.ValueMissing : 0) |
            (TypeMismatch ? ValidationErrors.TypeMismatch : 0) |
            (PatternMismatch ? ValidationErrors.PatternMismatch : 0) |
            (TooLong ? ValidationErrors.TooLong : 0) |
            (TooShort ? ValidationErrors.TooShort : 0) |
            (RangeUnderflow ? ValidationErrors.RangeUnderflow : 0) |
            (RangeOverflow ? ValidationErrors.RangeOverflow : 0) |
            (StepMismatch ? ValidationErrors.StepMismatch : 0) |
            (BadInput ? ValidationErrors.BadInput : 0) |
            (CustomError ? ValidationErrors.Custom : 0);
    }

    /// <summary>
    /// Gives custom elements their internals.
    /// </summary>
    [DomExposed("HTMLElement")]
    public static class HtmlElementInternalsExtensions
    {
        /// <summary>
        /// Attaches the internals to a custom element, which is possible once.
        /// </summary>
        /// <param name="element">The custom element.</param>
        /// <returns>The internals of the element.</returns>
        [DomName("attachInternals")]
        public static ElementInternals AttachInternals(this IHtmlElement element)
        {
            // only an autonomous custom element has internals (its name has a hyphen)
            if (element.GetAttribute("is") is not null || element.LocalName.IndexOf('-') < 0)
            {
                throw new DomException(DomError.NotSupported);
            }

            return ElementInternals.Attach(element);
        }
    }
}
