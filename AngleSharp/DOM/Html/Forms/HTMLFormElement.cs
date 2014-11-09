namespace AngleSharp.DOM.Html
{
    using AngleSharp.DOM.Collections;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Network;
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the form element.
    /// </summary>
    sealed class HTMLFormElement : HTMLElement, IHtmlFormElement
    {
        #region Fields

        readonly HtmlFormControlsCollection _elements;

        Task _plannedNavigation;
        CancellationTokenSource _cancel;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML form element.
        /// </summary>
        public HTMLFormElement()
            : base(Tags.Form, NodeFlags.Special)
        {
            _cancel = new CancellationTokenSource();
            _elements = new HtmlFormControlsCollection(this);
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
            get { return _elements[index]; }
        }

        /// <summary>
        /// Gets the form element(s) with the specified name.
        /// </summary>
        /// <param name="name">The name or id of the element.</param>
        /// <returns>A collection with elements, an element or null.</returns>
        public IElement this[String name]
        {
            get { return _elements[name]; }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of the name attribute.
        /// </summary>
        public String Name
        {
            get { return GetAttribute(AttributeNames.Name); }
            set { SetAttribute(AttributeNames.Name, value); }
        }

        /// <summary>
        /// Gets the number of elements in the Elements collection.
        /// </summary>
        public Int32 Length
        {
            get { return _elements.Length; }
        }

        /// <summary>
        /// Gets all the form controls belonging to this form element.
        /// </summary>
        public IHtmlFormControlsCollection Elements
        {
            get { return _elements; }
        }

        /// <summary>
        /// Gets or sets the character encodings that are to be used for the submission.
        /// </summary>
        public String AcceptCharset
        {
            get { return GetAttribute(AttributeNames.AcceptCharset); }
            set { SetAttribute(AttributeNames.AcceptCharset, value); }
        }

        /// <summary>
        /// Gets or sets the form's name within the forms collection.
        /// </summary>
        public String Action
        {
            get { return GetAttribute(AttributeNames.Action); }
            set { SetAttribute(AttributeNames.Action, value); }
        }

        /// <summary>
        /// Gets or sets if autocomplete is turned on or off.
        /// </summary>
        public String Autocomplete
        {
            get { return GetAttribute(AttributeNames.AutoComplete); }
            set { SetAttribute(AttributeNames.AutoComplete, value); }
        }

        /// <summary>
        /// Gets or sets the encoding to use for sending the form.
        /// </summary>
        public String Enctype
        {
            get { return CheckEncType(GetAttribute(AttributeNames.Enctype)); }
            set { SetAttribute(AttributeNames.Enctype, CheckEncType(value)); }
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
            get { return GetAttribute(AttributeNames.Method) ?? "post"; }
            set { SetAttribute(AttributeNames.Method, value); }
        }

        /// <summary>
        /// Gets or sets the indicator that the form is not to be validated during submission.
        /// </summary>
        public Boolean NoValidate
        {
            get { return GetAttribute(AttributeNames.NoValidate) != null; }
            set { SetAttribute(AttributeNames.NoValidate, value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets or sets the target name of the response to the request.
        /// </summary>
        public String Target
        {
            get { return GetAttribute(AttributeNames.Target); }
            set { SetAttribute(AttributeNames.Target, value); }
        }

        /// <summary>
        /// Gets the planned navigation task, if any.
        /// </summary>
        public Task PlannedNavigation
        {
            get { return _plannedNavigation; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Submits the form element from the form element itself.
        /// </summary>
        public void Submit()
        {
            SubmitForm(this, true);
        }

        /// <summary>
        /// Resets the form to the previous (default) state.
        /// </summary>
        public void Reset()
        {
            foreach (var element in _elements)
                element.Reset();
        }

        /// <summary>
        /// Checks if the form is valid, i.e. if all fields fulfill their requirements.
        /// </summary>
        /// <returns>True if the form is valid, otherwise false.</returns>
        public Boolean CheckValidity()
        {
            foreach (var element in _elements)
            {
                if (!element.CheckValidity())
                    return false;
            }

            return true;
        }

        public Boolean ReportValidity()
        {
            //TODO see:
            //http://www.whatwg.org/specs/web-apps/current-work/multipage/forms.html#dom-form-reportvalidity
            return CheckValidity();
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

        void SubmitForm(HTMLElement from, Boolean submittedFromSubmitMethod)
        {
            var formDocument = Owner;

            //TODO
            //If form document has no associated browsing context or its active
            //sandboxing flag set has its sandboxed forms browsing context flag
            //set, then abort these steps without doing anything.

            //TODO
            //var browsingContext = new object();

            if (!submittedFromSubmitMethod && !from.Attributes.Any(m => m.Name == AttributeNames.FormNoValidate) && NoValidate)
            {
                if (!CheckValidity())
                {
                    FireSimpleEvent(EventNames.Invalid);
                    return;
                }
            }

            var action = String.IsNullOrEmpty(Action) ? new Url(formDocument.DocumentUri) : new Url(new Url(from.BaseUri), Action);

            //TODO
            //If the user indicated a specific browsing context to use when submitting
            //the form, then let target browsing context be that browsing context.
            //Otherwise, apply the rules for choosing a browsing context given a browsing
            //context name using target as the name and form browsing context as the
            //context in which the algorithm is executed, and let target browsing context
            //be the resulting browsing context.

            //TODO
            //var replace = false;
            //If target browsing context was created in the previous step, or, alternatively,
            //if the form document has not yet completely loaded and the submitted from
            //submit() method is set, then let replace be true. Otherwise, let it be false

            var scheme = action.Scheme;
            var method = Method.ToEnum(HttpMethod.Get);

            if (scheme == KnownProtocols.Http || scheme == KnownProtocols.Https)
            {
                if (method == HttpMethod.Get)
                    MutateActionUrl(action);
                else if (method == HttpMethod.Post)
                    SubmitAsEntityBody(action);
            }
            else if (scheme == KnownProtocols.Data)
            {
                if (method == HttpMethod.Get)
                    GetActionUrl(action);
                else if (method == HttpMethod.Post)
                    PostToData(action);
            }
            else if (scheme == KnownProtocols.Mailto)
            {
                if (method == HttpMethod.Get)
                    MailWithHeaders(action);
                else if (method == HttpMethod.Post)
                    MailAsBody(action);
            }
            else if (scheme == KnownProtocols.Ftp || scheme == KnownProtocols.JavaScript)
                GetActionUrl(action);
            else
                MutateActionUrl(action);
        }

        /// <summary>
        /// More information can be found at:
        /// http://www.w3.org/html/wg/drafts/html/master/forms.html#submit-data-post
        /// </summary>
        void PostToData(Url action)
        {
            var encoding = String.IsNullOrEmpty(AcceptCharset) ? Owner.CharacterSet : AcceptCharset;
            var formDataSet = ConstructDataSet();
            var enctype = Enctype;
            var result = String.Empty;

            if (enctype.Equals(MimeTypes.StandardForm, StringComparison.OrdinalIgnoreCase))
            {
                using (var sr = new StreamReader(formDataSet.AsUrlEncoded(DocumentEncoding.Resolve(encoding))))
                    result = sr.ReadToEnd();
            }
            else if (enctype.Equals(MimeTypes.MultipartForm, StringComparison.OrdinalIgnoreCase))
            {
                using (var sr = new StreamReader(formDataSet.AsMultipart(DocumentEncoding.Resolve(encoding))))
                    result = sr.ReadToEnd();
            }
            else if (enctype.Equals(MimeTypes.Plain, StringComparison.OrdinalIgnoreCase))
            {
                using (var sr = new StreamReader(formDataSet.AsPlaintext(DocumentEncoding.Resolve(encoding))))
                    result = sr.ReadToEnd();
            }

            if (action.Href.Contains("%%%%"))
            {
                result = result.UrlEncode(DocumentEncoding.Resolve("us-ascii"));
                action.Href = action.Href.ReplaceFirst("%%%%", result);
            }
            else if (action.Href.Contains("%%"))
            {
                result = result.UrlEncode(System.Text.Encoding.UTF8);
                action.Href = action.Href.ReplaceFirst("%%", result);
            }

            _plannedNavigation = NavigateTo(action, HttpMethod.Get);
        }

        /// <summary>
        /// More information can be found at:
        /// http://www.w3.org/html/wg/drafts/html/master/forms.html#submit-mailto-headers
        /// </summary>
        void MailWithHeaders(Url action)
        {
            var formDataSet = ConstructDataSet();
            var result = formDataSet.AsUrlEncoded(DocumentEncoding.Resolve("us-ascii"));
            var headers = String.Empty;

            using (var sr = new StreamReader(result))
                headers = sr.ReadToEnd();

            action.Query = headers.Replace("+", "%20");
            GetActionUrl(action);
        }

        /// <summary>
        /// More information can be found at:
        /// http://www.w3.org/html/wg/drafts/html/master/forms.html#submit-mailto-body
        /// </summary>
        void MailAsBody(Url action)
        {
            var formDataSet = ConstructDataSet();
            var enctype = Enctype;
            var encoding = DocumentEncoding.Resolve("us-ascii");
            var body = String.Empty;

            if (enctype.Equals(MimeTypes.StandardForm, StringComparison.OrdinalIgnoreCase))
            {
                using (var sr = new StreamReader(formDataSet.AsUrlEncoded(encoding)))
                    body = sr.ReadToEnd();
            }
            else if (enctype.Equals(MimeTypes.MultipartForm, StringComparison.OrdinalIgnoreCase))
            {
                using (var sr = new StreamReader(formDataSet.AsMultipart(encoding)))
                    body = sr.ReadToEnd();
            }
            else if (enctype.Equals(MimeTypes.Plain, StringComparison.OrdinalIgnoreCase))
            {
                using (var sr = new StreamReader(formDataSet.AsPlaintext(encoding)))
                    body = sr.ReadToEnd();
            }

            action.Query = "body=" + body.UrlEncode(encoding);
            GetActionUrl(action);
        }

        /// <summary>
        /// More information can be found at:
        /// http://www.w3.org/html/wg/drafts/html/master/forms.html#submit-get-action
        /// </summary>
        void GetActionUrl(Url action)
        {
            _plannedNavigation = NavigateTo(action, HttpMethod.Get);
        }

        /// <summary>
        /// Submits the body of the form.
        /// http://www.w3.org/html/wg/drafts/html/master/forms.html#submit-body
        /// </summary>
        void SubmitAsEntityBody(Url action)
        {
            var encoding = String.IsNullOrEmpty(AcceptCharset) ? Owner.CharacterSet : AcceptCharset;
            var formDataSet = ConstructDataSet();
            var enctype = Enctype;
            var mimeType = String.Empty;
            Stream result = null;

            if (enctype.Equals(MimeTypes.StandardForm, StringComparison.OrdinalIgnoreCase))
            {
                result = formDataSet.AsUrlEncoded(DocumentEncoding.Resolve(encoding));
                mimeType = MimeTypes.StandardForm;
            }
            else if (enctype.Equals(MimeTypes.MultipartForm, StringComparison.OrdinalIgnoreCase))
            {
                result = formDataSet.AsMultipart(DocumentEncoding.Resolve(encoding));
                mimeType = String.Concat(MimeTypes.MultipartForm, "; boundary=", formDataSet.Boundary);
            }
            else if (enctype.Equals(MimeTypes.Plain, StringComparison.OrdinalIgnoreCase))
            {
                result = formDataSet.AsPlaintext(DocumentEncoding.Resolve(encoding));
                mimeType = MimeTypes.Plain;
            }

            _plannedNavigation = NavigateTo(action, HttpMethod.Post, result, mimeType);
        }

        /// <summary>
        /// Plan to navigate to an action using the specified method with the given
        /// entity body of the mime type.
        /// http://www.w3.org/html/wg/drafts/html/master/forms.html#plan-to-navigate
        /// </summary>
        /// <param name="action">The action to use.</param>
        /// <param name="method">The HTTP method.</param>
        /// <param name="body">The entity body of the request.</param>
        /// <param name="mime">The MIME type of the entity body.</param>
        async Task NavigateTo(Url action, HttpMethod method, Stream body = null, String mime = null)
        {
            if (Owner != null)
            {
                if (_plannedNavigation != null)
                {
                    _cancel.Cancel();
                    _plannedNavigation = null;
                    _cancel = new CancellationTokenSource();
                }

                var requester = Owner.Options.GetRequester(action.Scheme);

                if (requester == null)
                    return;

                using (var response = await requester.SendAsync(action, body, mime, method, _cancel.Token).ConfigureAwait(false))
                    await Owner.LoadAsync(response, _cancel.Token).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// More information can be found at:
        /// http://www.w3.org/html/wg/drafts/html/master/forms.html#submit-mutate-action
        /// </summary>
        void MutateActionUrl(Url action)
        {
            var encoding = String.IsNullOrEmpty(AcceptCharset) ? Owner.CharacterSet : AcceptCharset;
            var formDataSet = ConstructDataSet();
            var result = formDataSet.AsUrlEncoded(DocumentEncoding.Resolve(encoding));

            using (var sr = new StreamReader(result))
                action.Query = sr.ReadToEnd();

            GetActionUrl(action);
        }

        /// <summary>
        /// Constructs the form data set with the given submitter.
        /// </summary>
        /// <param name="submitter">[Optional] The submitter to use.</param>
        /// <returns>The constructed form data set.</returns>
        FormDataSet ConstructDataSet(HTMLElement submitter = null)
        {
            var formDataSet = new FormDataSet();

            foreach (var field in _elements)
            {
                if (field.ParentElement is IHtmlDataListElement)
                    continue;
                else if (field.IsDisabled)
                    continue;

                field.ConstructDataSet(formDataSet, submitter);
            }

            return formDataSet;
        }

        /// <summary>
        /// Checks the encoding type of the form and returns the appropriate
        /// encoding type, which is either the given one, or the default one.
        /// </summary>
        /// <param name="encType">The encoding type used by the form.</param>
        /// <returns>A valid encoding type.</returns>
        String CheckEncType(String encType)
        {
            if (!String.IsNullOrEmpty(encType) && (encType.Equals(MimeTypes.Plain, StringComparison.OrdinalIgnoreCase) || encType.Equals(MimeTypes.MultipartForm, StringComparison.OrdinalIgnoreCase)))
                return encType;

            return MimeTypes.StandardForm;
        }

        #endregion
    }
}
