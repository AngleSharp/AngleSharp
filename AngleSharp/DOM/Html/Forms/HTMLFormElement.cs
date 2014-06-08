namespace AngleSharp.DOM.Html
{
    using AngleSharp.DOM.Collections;
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Linq;

    /// <summary>
    /// Represents the form element.
    /// </summary>
    [DOM("HTMLFormElement")]
    public sealed class HTMLFormElement : HTMLElement
    {
        #region Fields

        Task _plannedNavigation;
        CancellationTokenSource _cancel;
        HTMLFormControlsCollection _elements;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML form element.
        /// </summary>
        internal HTMLFormElement()
        {
            _cancel = new CancellationTokenSource();
            _name = Tags.Form;
            _elements = new HTMLFormControlsCollection(this);
        }

        #endregion

        #region Index

        /// <summary>
        /// Gets the form element at the specified index.
        /// </summary>
        /// <param name="index">The index in the elements collection.</param>
        /// <returns>The element or null.</returns>
        public Element this[Int32 index]
        {
            get { return _elements[index]; }
        }

        /// <summary>
        /// Gets the form element(s) with the specified name.
        /// </summary>
        /// <param name="name">The name or id of the element.</param>
        /// <returns>A collection with elements, an element or null.</returns>
        public Object this[String name]
        {
            get { return _elements[name]; }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of the name attribute.
        /// </summary>
        [DOM("name")]
        public String Name
        {
            get { return GetAttribute("name"); }
            set { SetAttribute("name", value); }
        }

        /// <summary>
        /// Gets the number of elements in the Elements collection.
        /// </summary>
        [DOM("length")]
        public Int32 Length
        {
            get { return _elements.Length; }
        }

        /// <summary>
        /// Gets all the form controls belonging to this form element.
        /// </summary>
        [DOM("elements")]
        public HTMLFormControlsCollection Elements
        {
            get { return _elements; }
        }

        /// <summary>
        /// Gets or sets the character encodings that are to be used for the submission.
        /// </summary>
        [DOM("acceptCharset")]
        public String AcceptCharset
        {
            get { return GetAttribute("acceptCharset"); }
            set { SetAttribute("acceptCharset", value); }
        }

        /// <summary>
        /// Gets or sets the form's name within the forms collection.
        /// </summary>
        [DOM("action")]
        public String Action
        {
            get { return GetAttribute("action"); }
            set { SetAttribute("action", value); }
        }

        /// <summary>
        /// Gets or sets if autocomplete is turned on or off.
        /// </summary>
        [DOM("autocomplete")]
        public PowerState Autocomplete
        {
            get { return ToEnum(GetAttribute("autocomplete"), PowerState.On); }
            set { SetAttribute("autocomplete", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the encoding to use for sending the form.
        /// </summary>
        [DOM("enctype")]
        public String Enctype
        {
            get { return CheckEncType(GetAttribute("enctype")); }
            set { SetAttribute("enctype", CheckEncType(value)); }
        }

        /// <summary>
        /// Gets or sets the encoding to use for sending the form.
        /// </summary>
        [DOM("encoding")]
        public String Encoding
        {
            get { return Enctype; }
            set { Enctype = value; }
        }

        /// <summary>
        /// Gets or sets the method to use for transmitting the form.
        /// </summary>
        [DOM("method")]
        public HttpMethod Method
        {
            get { return ToEnum(GetAttribute("method"), HttpMethod.Get); }
            set { SetAttribute("method", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the indicator that the form is not to be validated during submission.
        /// </summary>
        [DOM("noValidate")]
        public Boolean NoValidate
        {
            get { return GetAttribute("novalidate") != null; }
            set { SetAttribute("novalidate", value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets or sets the target name of the response to the request.
        /// </summary>
        [DOM("target")]
        public String Target
        {
            get { return GetAttribute("target"); }
            set { SetAttribute("target", value); }
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
        [DOM("submit")]
        public void Submit()
        {
            SubmitForm(this, true);
        }

        /// <summary>
        /// Resets the form to the previous (default) state.
        /// </summary>
        [DOM("reset")]
        public void Reset()
        {
            foreach (var element in _elements)
                element.Reset();
        }

        /// <summary>
        /// Checks if the form is valid, i.e. if all fields fulfill their requirements.
        /// </summary>
        /// <returns>True if the form is valid, otherwise false.</returns>
        [DOM("checkValidity")]
        public Boolean CheckValidity()
        {
            foreach (var element in _elements)
                if (!element.CheckValidity())
                    return false;

            return true;
        }
        
        #endregion

        #region Helpers

        void SubmitForm(HTMLElement from, Boolean submittedFromSubmitMethod)
        {
            var formDocument = OwnerDocument;

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

            var action = Action;

            if (String.IsNullOrEmpty(action))
                action = formDocument.DocumentUri;

            if (!Location.IsAbsolute(action))
                action = Location.MakeAbsolute(from.BaseURI, action);

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

            var location = new Location(action);
            var scheme = location.Protocol.TrimEnd(new [] { ':' });

            if (scheme == KnownProtocols.Http || scheme == KnownProtocols.Https)
            {
                if (Method == HttpMethod.Get)
                    MutateActionUrl(location);
                else if (Method == HttpMethod.Post)
                    SubmitAsEntityBody(location);
            }
            else if (scheme == KnownProtocols.Data)
            {
                if (Method == HttpMethod.Get)
                    GetActionUrl(location);
                else if (Method == HttpMethod.Post)
                    PostToData(location);
            }
            else if (scheme == KnownProtocols.Mailto)
            {
                if (Method == HttpMethod.Get)
                    MailWithHeaders(location);
                else if (Method == HttpMethod.Post)
                    MailAsBody(location);
            }
            else if (scheme == KnownProtocols.Ftp || scheme == KnownProtocols.JavaScript)
                GetActionUrl(location);
            else
                MutateActionUrl(location);
        }

        /// <summary>
        /// More information can be found at:
        /// http://www.w3.org/html/wg/drafts/html/master/forms.html#submit-data-post
        /// </summary>
        void PostToData(Location action)
        {
            var encoding = String.IsNullOrEmpty(AcceptCharset) ? OwnerDocument.CharacterSet : AcceptCharset;
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
                result = result.UrlEncode(System.Text.Encoding.GetEncoding("us-ascii"));
                action.Href = action.Href.ReplaceFirst("%%%%", result);
            }
            else if (action.Href.Contains("%%"))
            {
                result = result.UrlEncode(System.Text.Encoding.UTF8);
                action.Href = action.Href.ReplaceFirst("%%", result);
            }

            _plannedNavigation = NavigateTo(action.ToUri(), HttpMethod.Get);
        }

        /// <summary>
        /// More information can be found at:
        /// http://www.w3.org/html/wg/drafts/html/master/forms.html#submit-mailto-headers
        /// </summary>
        void MailWithHeaders(Location action)
        {
            var formDataSet = ConstructDataSet();
            var result = formDataSet.AsUrlEncoded(System.Text.Encoding.GetEncoding("us-ascii"));
            var headers = String.Empty;

            using (var sr = new StreamReader(result))
                headers = sr.ReadToEnd();

            action.Search = headers.Replace("+", "%20");
            GetActionUrl(action);
        }

        /// <summary>
        /// More information can be found at:
        /// http://www.w3.org/html/wg/drafts/html/master/forms.html#submit-mailto-body
        /// </summary>
        void MailAsBody(Location action)
        {
            var formDataSet = ConstructDataSet();
            var enctype = Enctype;
            var encoding = System.Text.Encoding.GetEncoding("us-ascii");
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

            action.Search = "body=" + body.UrlEncode(encoding);
            GetActionUrl(action);
        }

        /// <summary>
        /// More information can be found at:
        /// http://www.w3.org/html/wg/drafts/html/master/forms.html#submit-get-action
        /// </summary>
        void GetActionUrl(Location action)
        {
            _plannedNavigation = NavigateTo(action.ToUri(), HttpMethod.Get);
        }

        /// <summary>
        /// Submits the body of the form.
        /// http://www.w3.org/html/wg/drafts/html/master/forms.html#submit-body
        /// </summary>
        void SubmitAsEntityBody(Location action)
        {
            var encoding = String.IsNullOrEmpty(AcceptCharset) ? OwnerDocument.CharacterSet : AcceptCharset;
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

            _plannedNavigation = NavigateTo(action.ToUri(), HttpMethod.Post, result, mimeType);
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
        async Task NavigateTo(Uri action, HttpMethod method, Stream body = null, String mime = null)
        {
            if (_plannedNavigation != null)
            {
                _cancel.Cancel();
                _plannedNavigation = null;
                _cancel = new CancellationTokenSource();
            }

            var stream = await _owner.Options.SendAsync(action, body, mime, method, _cancel.Token);
            var html = _owner as HTMLDocument;

            if (html != null)
                html.Load(stream);
        }

        /// <summary>
        /// More information can be found at:
        /// http://www.w3.org/html/wg/drafts/html/master/forms.html#submit-mutate-action
        /// </summary>
        void MutateActionUrl(Location action)
        {
            var encoding = String.IsNullOrEmpty(AcceptCharset) ? OwnerDocument.CharacterSet : AcceptCharset;
            var formDataSet = ConstructDataSet();
            var result = formDataSet.AsUrlEncoded(DocumentEncoding.Resolve(encoding));

            using (var sr = new StreamReader(result))
                action.Search = sr.ReadToEnd();

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
                if (field.ParentElement is HTMLDataListElement)
                    continue;
                else if (field.Disabled)
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

        #region Internal Properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return true; }
        }

        #endregion
    }
}
