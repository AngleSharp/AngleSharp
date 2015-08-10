namespace AngleSharp.Dom.Html
{
    using AngleSharp.Dom.Collections;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Network;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the form element.
    /// </summary>
    sealed class HtmlFormElement : HtmlElement, IHtmlFormElement
    {
        #region Fields

        HtmlFormControlsCollection _elements;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML form element.
        /// </summary>
        public HtmlFormElement(Document owner, String prefix = null)
            : base(owner, Tags.Form, prefix, NodeFlags.Special)
        {
        }

        #endregion

        #region Index

        /// <summary>
        /// Gets the form element at the specified index.
        /// </summary>
        /// <param name="index">The index in the elements collection.</param>
        /// <returns>The element or null.</returns>
        public IElement this[Int32 index]
        {
            get { return Elements[index]; }
        }

        /// <summary>
        /// Gets the form element(s) with the specified name.
        /// </summary>
        /// <param name="name">The name or id of the element.</param>
        /// <returns>A collection with elements, an element or null.</returns>
        public IElement this[String name]
        {
            get { return Elements[name]; }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of the name attribute.
        /// </summary>
        public String Name
        {
            get { return GetOwnAttribute(AttributeNames.Name); }
            set { SetOwnAttribute(AttributeNames.Name, value); }
        }

        /// <summary>
        /// Gets the number of elements in the Elements collection.
        /// </summary>
        public Int32 Length
        {
            get { return Elements.Length; }
        }

        /// <summary>
        /// Gets all the form controls belonging to this form element.
        /// </summary>
        public HtmlFormControlsCollection Elements
        {
            get { return _elements ?? (_elements = new HtmlFormControlsCollection(this)); }
        }

        /// <summary>
        /// Gets all the form controls belonging to this form element.
        /// </summary>
        IHtmlFormControlsCollection IHtmlFormElement.Elements
        {
            get { return Elements; }
        }

        /// <summary>
        /// Gets or sets the character encodings that are to be used for the submission.
        /// </summary>
        public String AcceptCharset
        {
            get { return GetOwnAttribute(AttributeNames.AcceptCharset); }
            set { SetOwnAttribute(AttributeNames.AcceptCharset, value); }
        }

        /// <summary>
        /// Gets or sets the form's name within the forms collection.
        /// </summary>
        public String Action
        {
            get { return GetOwnAttribute(AttributeNames.Action); }
            set { SetOwnAttribute(AttributeNames.Action, value); }
        }

        /// <summary>
        /// Gets or sets if autocomplete is turned on or off.
        /// </summary>
        public String Autocomplete
        {
            get { return GetOwnAttribute(AttributeNames.AutoComplete); }
            set { SetOwnAttribute(AttributeNames.AutoComplete, value); }
        }

        /// <summary>
        /// Gets or sets the encoding to use for sending the form.
        /// </summary>
        public String Enctype
        {
            get { return CheckEncType(GetOwnAttribute(AttributeNames.Enctype)); }
            set { SetOwnAttribute(AttributeNames.Enctype, CheckEncType(value)); }
        }

        /// <summary>
        /// Gets or sets the encoding to use for sending the form.
        /// </summary>
        public String Encoding
        {
            get { return Enctype; }
            set { Enctype = value; }
        }

        /// <summary>
        /// Gets or sets the method to use for transmitting the form.
        /// </summary>
        public String Method
        {
            get { return GetOwnAttribute(AttributeNames.Method) ?? String.Empty; }
            set { SetOwnAttribute(AttributeNames.Method, value); }
        }

        /// <summary>
        /// Gets or sets the indicator that the form is not to be validated during submission.
        /// </summary>
        public Boolean NoValidate
        {
            get { return HasOwnAttribute(AttributeNames.NoValidate); }
            set { SetOwnAttribute(AttributeNames.NoValidate, value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets or sets the target name of the response to the request.
        /// </summary>
        public String Target
        {
            get { return GetOwnAttribute(AttributeNames.Target); }
            set { SetOwnAttribute(AttributeNames.Target, value); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Submits the form element from the form element itself.
        /// </summary>
        public Task<IDocument> Submit()
        {
            return SubmitForm(this, true);
        }

        /// <summary>
        /// Submits the form element from another element.
        /// </summary>
        public Task<IDocument> Submit(IHtmlElement sourceElement)
        {
            return SubmitForm(sourceElement ?? this, false);
        }

        /// <summary>
        /// Resets the form to the previous (default) state.
        /// </summary>
        public void Reset()
        {
            foreach (var element in Elements)
                element.Reset();
        }

        /// <summary>
        /// Checks if the form is valid, i.e. if all fields fulfill their requirements.
        /// </summary>
        /// <returns>True if the form is valid, otherwise false.</returns>
        public Boolean CheckValidity()
        {
            var controls = GetInvalidControls();
            var result = true;

            foreach (var control in controls)
            {
                if (control.FireSimpleEvent(EventNames.Invalid, false, true) == false)
                    result = false;
            }

            return result;
        }

        /// <summary>
        /// Evaluates the form controls according to the spec:
        /// https://html.spec.whatwg.org/multipage/forms.html#statically-validate-the-constraints
        /// </summary>
        /// <returns>A list over all invalid controls.</returns>
        IEnumerable<HtmlFormControlElement> GetInvalidControls()
        {
            foreach (var element in Elements)
            {
                if (element.WillValidate && element.CheckValidity() == false)
                    yield return element;
            }
        }

        public Boolean ReportValidity()
        {
            var controls = GetInvalidControls();
            var result = true;
            var hasfocused = false;

            foreach (var control in controls)
            {
                if (control.FireSimpleEvent(EventNames.Invalid, false, true))
                    continue;

                if (hasfocused == false)
                {
                    //TODO Report Problems (interactively, e.g. via UI specific event)
                    control.DoFocus();
                    hasfocused = true;
                }

                result = false;
            }

            return result;
        }

        /// <summary>
        /// Requests the input fields to be automatically filled with previous entries.
        /// </summary>
        public void RequestAutocomplete()
        {
            //TODO see:
            //http://www.whatwg.org/specs/web-apps/current-work/multipage/association-of-controls-and-forms.html#dom-form-requestautocomplete
        }

        #endregion

        #region Helpers

        Task<IDocument> SubmitForm(IHtmlElement from, Boolean submittedFromSubmitMethod)
        {
            var owner = Owner;

            if (owner.ActiveSandboxing.HasFlag(Sandboxes.Forms))
            {
                //Do nothing.
            }
            else if (!submittedFromSubmitMethod && !from.HasAttribute(AttributeNames.FormNoValidate) && NoValidate && !CheckValidity())
            {
                this.FireSimpleEvent(EventNames.Invalid);
            }
            else
            {
                var action = String.IsNullOrEmpty(Action) ? new Url(owner.DocumentUri) : this.HyperReference(Action);
                var createdBrowsingContext = false;
                var targetBrowsingContext = owner.Context;
                var target = Target;

                if (!String.IsNullOrEmpty(target))
                {
                    targetBrowsingContext = owner.GetTarget(target);

                    if (createdBrowsingContext = (targetBrowsingContext == null))
                        targetBrowsingContext = owner.CreateTarget(target);
                }

                var replace = createdBrowsingContext || owner.ReadyState != DocumentReadyState.Complete;
                var scheme = action.Scheme;
                var method = Method.ToEnum(HttpMethod.Get);
                return SubmitForm(method, scheme, action, from);
            }

            return TaskEx.FromResult<IDocument>(owner);
        }

        Task<IDocument> SubmitForm(HttpMethod method, String scheme, Url action, IHtmlElement submitter)
        {
            if (scheme == KnownProtocols.Http || scheme == KnownProtocols.Https)
            {
                if (method == HttpMethod.Get)
                    return MutateActionUrl(action, submitter);
                else if (method == HttpMethod.Post)
                    return SubmitAsEntityBody(action, submitter);
            }
            else if (scheme == KnownProtocols.Data)
            {
                if (method == HttpMethod.Get)
                    return GetActionUrl(action);
                else if (method == HttpMethod.Post)
                    return PostToData(action, submitter);
            }
            else if (scheme == KnownProtocols.Mailto)
            {
                if (method == HttpMethod.Get)
                    return MailWithHeaders(action, submitter);
                else if (method == HttpMethod.Post)
                    return MailAsBody(action, submitter);
            }
            else if (scheme == KnownProtocols.Ftp || scheme == KnownProtocols.JavaScript)
            {
                return GetActionUrl(action);
            }

            return MutateActionUrl(action, submitter);
        }

        /// <summary>
        /// More information can be found at:
        /// http://www.w3.org/html/wg/drafts/html/master/forms.html#submit-data-post
        /// </summary>
        Task<IDocument> PostToData(Url action, IHtmlElement submitter)
        {
            var encoding = String.IsNullOrEmpty(AcceptCharset) ? Owner.CharacterSet : AcceptCharset;
            var formDataSet = ConstructDataSet(submitter);
            var enctype = Enctype;
            var result = String.Empty;
            var stream = CreateBody(enctype, TextEncoding.Resolve(encoding), formDataSet);

            using (var sr = new StreamReader(stream))
                result = sr.ReadToEnd();

            if (action.Href.Contains("%%%%"))
            {
                result = TextEncoding.UsAscii.GetBytes(result).UrlEncode();
                action.Href = action.Href.ReplaceFirst("%%%%", result);
            }
            else if (action.Href.Contains("%%"))
            {
                result = TextEncoding.Utf8.GetBytes(result).UrlEncode();
                action.Href = action.Href.ReplaceFirst("%%", result);
            }

            return GetActionUrl(action);
        }

        /// <summary>
        /// More information can be found at:
        /// http://www.w3.org/html/wg/drafts/html/master/forms.html#submit-mailto-headers
        /// </summary>
        Task<IDocument> MailWithHeaders(Url action, IHtmlElement submitter)
        {
            var formDataSet = ConstructDataSet(submitter);
            var result = formDataSet.AsUrlEncoded(TextEncoding.UsAscii);
            var headers = String.Empty;

            using (var sr = new StreamReader(result))
                headers = sr.ReadToEnd();

            action.Query = headers.Replace("+", "%20");
            return GetActionUrl(action);
        }

        /// <summary>
        /// More information can be found at:
        /// http://www.w3.org/html/wg/drafts/html/master/forms.html#submit-mailto-body
        /// </summary>
        Task<IDocument> MailAsBody(Url action, IHtmlElement submitter)
        {
            var formDataSet = ConstructDataSet(submitter);
            var enctype = Enctype;
            var encoding = TextEncoding.UsAscii;
            var stream = CreateBody(enctype, encoding, formDataSet);
            var body = String.Empty;

            using (var sr = new StreamReader(stream))
                body = sr.ReadToEnd();

            action.Query = "body=" + encoding.GetBytes(body).UrlEncode();
            return GetActionUrl(action);
        }

        /// <summary>
        /// More information can be found at:
        /// http://www.w3.org/html/wg/drafts/html/master/forms.html#submit-get-action
        /// </summary>
        Task<IDocument> GetActionUrl(Url action)
        {
            return NavigateTo(DocumentRequest.Get(action, source: this, referer: Owner.DocumentUri));
        }

        /// <summary>
        /// Submits the body of the form.
        /// http://www.w3.org/html/wg/drafts/html/master/forms.html#submit-body
        /// </summary>
        Task<IDocument> SubmitAsEntityBody(Url target, IHtmlElement submitter)
        {
            var encoding = String.IsNullOrEmpty(AcceptCharset) ? Owner.CharacterSet : AcceptCharset;
            var formDataSet = ConstructDataSet(submitter);
            var enctype = Enctype;
            var body = CreateBody(enctype, TextEncoding.Resolve(encoding), formDataSet);

            if (enctype.Equals(MimeTypes.MultipartForm, StringComparison.OrdinalIgnoreCase))
                enctype = String.Concat(MimeTypes.MultipartForm, "; boundary=", formDataSet.Boundary);

            var request = DocumentRequest.Post(target, body, enctype, source: this, referer: Owner.DocumentUri);
            return NavigateTo(request);
        }

        /// <summary>
        /// Plan to navigate to an action using the specified method with the given
        /// entity body of the mime type.
        /// http://www.w3.org/html/wg/drafts/html/master/forms.html#plan-to-navigate
        /// </summary>
        /// <param name="request">The request to issue.</param>
        Task<IDocument> NavigateTo(DocumentRequest request)
        {
            this.CancelTasks();
            return this.CreateTask(cancel => Owner.Context.OpenAsync(request, cancel));
        }

        /// <summary>
        /// More information can be found at:
        /// http://www.w3.org/html/wg/drafts/html/master/forms.html#submit-mutate-action
        /// </summary>
        Task<IDocument> MutateActionUrl(Url action, IHtmlElement submitter)
        {
            var encoding = String.IsNullOrEmpty(AcceptCharset) ? Owner.CharacterSet : AcceptCharset;
            var formDataSet = ConstructDataSet(submitter);
            var result = formDataSet.AsUrlEncoded(TextEncoding.Resolve(encoding));

            using (var sr = new StreamReader(result))
                action.Query = sr.ReadToEnd();

            return GetActionUrl(action);
        }

        FormDataSet ConstructDataSet(IHtmlElement submitter)
        {
            var formDataSet = new FormDataSet();

            foreach (var field in Elements)
            {
                if (field.ParentElement is IHtmlDataListElement == false && field.IsDisabled == false)
                    field.ConstructDataSet(formDataSet, submitter);
            }

            return formDataSet;
        }

        static Stream CreateBody(String enctype, Encoding encoding, FormDataSet formDataSet)
        {
            if (enctype.Equals(MimeTypes.UrlencodedForm, StringComparison.OrdinalIgnoreCase))
                return formDataSet.AsUrlEncoded(encoding);
            else if (enctype.Equals(MimeTypes.MultipartForm, StringComparison.OrdinalIgnoreCase))
                return formDataSet.AsMultipart(encoding);
            else if (enctype.Equals(MimeTypes.Plain, StringComparison.OrdinalIgnoreCase))
                return formDataSet.AsPlaintext(encoding);

            return MemoryStream.Null;
        }

        static String CheckEncType(String encType)
        {
            if (!String.IsNullOrEmpty(encType) && (encType.Equals(MimeTypes.Plain, StringComparison.OrdinalIgnoreCase) ||
                                                   encType.Equals(MimeTypes.MultipartForm, StringComparison.OrdinalIgnoreCase)))
            {
                return encType;
            }

            return MimeTypes.UrlencodedForm;
        }

        #endregion
    }
}
