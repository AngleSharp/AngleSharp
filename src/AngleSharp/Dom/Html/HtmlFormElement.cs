namespace AngleSharp.Dom.Html
{
    using AngleSharp.Dom.Collections;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Network;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the form element.
    /// </summary>
    sealed class HtmlFormElement : HtmlElement, IHtmlFormElement
    {
        #region Fields

        private HtmlFormControlsCollection _elements;

        #endregion

        #region ctor

        public HtmlFormElement(Document owner, String prefix = null)
            : base(owner, TagNames.Form, prefix, NodeFlags.Special)
        {
        }

        #endregion

        #region Index

        public IElement this[Int32 index]
        {
            get { return Elements[index]; }
        }

        public IElement this[String name]
        {
            get { return Elements[name]; }
        }

        #endregion

        #region Properties

        public String Name
        {
            get { return this.GetOwnAttribute(AttributeNames.Name); }
            set { this.SetOwnAttribute(AttributeNames.Name, value); }
        }

        public Int32 Length
        {
            get { return Elements.Length; }
        }

        public HtmlFormControlsCollection Elements
        {
            get { return _elements ?? (_elements = new HtmlFormControlsCollection(this)); }
        }

        IHtmlFormControlsCollection IHtmlFormElement.Elements
        {
            get { return Elements; }
        }

        public String AcceptCharset
        {
            get { return this.GetOwnAttribute(AttributeNames.AcceptCharset); }
            set { this.SetOwnAttribute(AttributeNames.AcceptCharset, value); }
        }

        public String Action
        {
            get { return this.GetOwnAttribute(AttributeNames.Action); }
            set { this.SetOwnAttribute(AttributeNames.Action, value); }
        }

        public String Autocomplete
        {
            get { return this.GetOwnAttribute(AttributeNames.AutoComplete); }
            set { this.SetOwnAttribute(AttributeNames.AutoComplete, value); }
        }

        public String Enctype
        {
            get { return this.GetOwnAttribute(AttributeNames.Enctype).ToEncodingType(); }
            set { this.SetOwnAttribute(AttributeNames.Enctype, value.ToEncodingType()); }
        }

        public String Encoding
        {
            get { return Enctype; }
            set { Enctype = value; }
        }

        public String Method
        {
            get { return this.GetOwnAttribute(AttributeNames.Method) ?? String.Empty; }
            set { this.SetOwnAttribute(AttributeNames.Method, value); }
        }

        public Boolean NoValidate
        {
            get { return this.GetBoolAttribute(AttributeNames.NoValidate); }
            set { this.SetBoolAttribute(AttributeNames.NoValidate, value); }
        }

        public String Target
        {
            get { return this.GetOwnAttribute(AttributeNames.Target); }
            set { this.SetOwnAttribute(AttributeNames.Target, value); }
        }

        #endregion

        #region Methods

        public Task<IDocument> SubmitAsync()
        {
            var request = GetSubmission();
            return this.NavigateToAsync(request);
        }

        public Task<IDocument> SubmitAsync(IHtmlElement sourceElement)
        {
            var request = GetSubmission(sourceElement);
            return this.NavigateToAsync(request);
        }

        public DocumentRequest GetSubmission()
        {
            return SubmitForm(this, true);
        }

        public DocumentRequest GetSubmission(IHtmlElement sourceElement)
        {
            return SubmitForm(sourceElement ?? this, false);
        }

        public void Reset()
        {
            foreach (var element in Elements)
            {
                element.Reset();
            }
        }

        public Boolean CheckValidity()
        {
            var controls = GetInvalidControls();
            var result = true;

            foreach (var control in controls)
            {
                if (!control.FireSimpleEvent(EventNames.Invalid, false, true))
                {
                    result = false;
                }
            }

            return result;
        }

        private IEnumerable<HtmlFormControlElement> GetInvalidControls()
        {
            foreach (var element in Elements)
            {
                if (element.WillValidate && !element.CheckValidity())
                {
                    yield return element;
                }
            }
        }

        public Boolean ReportValidity()
        {
            var controls = GetInvalidControls();
            var result = true;
            var hasfocused = false;

            foreach (var control in controls)
            {
                if (!control.FireSimpleEvent(EventNames.Invalid, false, true))
                {
                    if (!hasfocused)
                    {
                        control.DoFocus();
                        hasfocused = true;
                    }

                    result = false;
                }
            }

            return result;
        }

        public void RequestAutocomplete()
        {
            //TODO see:
            //http://www.whatwg.org/specs/web-apps/current-work/multipage/association-of-controls-and-forms.html#dom-form-requestautocomplete
        }

        #endregion

        #region Helpers

        private DocumentRequest SubmitForm(IHtmlElement from, Boolean submittedFromSubmitMethod)
        {
            var owner = Owner;

            if ((owner.ActiveSandboxing & Sandboxes.Forms) == Sandboxes.Forms)
            {
                //Do nothing.
            }
            else if (!submittedFromSubmitMethod && !from.HasAttribute(AttributeNames.FormNoValidate) && !NoValidate && !CheckValidity())
            {
                this.FireSimpleEvent(EventNames.Invalid);
            }
            else
            {
                var action = String.IsNullOrEmpty(Action) ? new Url(owner.DocumentUri) : this.HyperReference(Action);
                var createdBrowsingContext = false;
                var targetBrowsingContext = owner.Context;
                var target = Target;
                var replace = owner.ReadyState != DocumentReadyState.Complete;

                if (!String.IsNullOrEmpty(target))
                {
                    targetBrowsingContext = owner.GetTarget(target);
                    createdBrowsingContext = targetBrowsingContext == null;
                }

                if (createdBrowsingContext)
                {
                    targetBrowsingContext = owner.CreateTarget(target);
                    replace = true;
                }

                var scheme = action.Scheme;
                var method = Method.ToEnum(HttpMethod.Get);
                return SubmitForm(method, scheme, action, from);
            }

            return null;
        }

        private DocumentRequest SubmitForm(HttpMethod method, String scheme, Url action, IHtmlElement submitter)
        {
            if (scheme.IsOneOf(ProtocolNames.Http, ProtocolNames.Https))
            {
                if (method == HttpMethod.Get)
                {
                    return MutateActionUrl(action, submitter);
                }
                else if (method == HttpMethod.Post)
                {
                    return SubmitAsEntityBody(action, submitter);
                }
            }
            else if (scheme.Is(ProtocolNames.Data))
            {
                if (method == HttpMethod.Get)
                {
                    return GetActionUrl(action);
                }
                else if (method == HttpMethod.Post)
                {
                    return PostToData(action, submitter);
                }
            }
            else if (scheme.Is(ProtocolNames.Mailto))
            {
                if (method == HttpMethod.Get)
                {
                    return MailWithHeaders(action, submitter);
                }
                else if (method == HttpMethod.Post)
                {
                    return MailAsBody(action, submitter);
                }
            }
            else if (scheme.IsOneOf(ProtocolNames.Ftp, ProtocolNames.JavaScript))
            {
                return GetActionUrl(action);
            }

            return MutateActionUrl(action, submitter);
        }

        /// <summary>
        /// More information can be found at:
        /// http://www.w3.org/html/wg/drafts/html/master/forms.html#submit-data-post
        /// </summary>
        private DocumentRequest PostToData(Url action, IHtmlElement submitter)
        {
            var encoding = String.IsNullOrEmpty(AcceptCharset) ? Owner.CharacterSet : AcceptCharset;
            var formDataSet = ConstructDataSet(submitter);
            var enctype = Enctype;
            var result = String.Empty;
            var stream = formDataSet.CreateBody(enctype, encoding);

            using (var sr = new StreamReader(stream))
            {
                result = sr.ReadToEnd();
            }

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
        private DocumentRequest MailWithHeaders(Url action, IHtmlElement submitter)
        {
            var formDataSet = ConstructDataSet(submitter);
            var result = formDataSet.AsUrlEncoded(TextEncoding.UsAscii);
            var headers = String.Empty;

            using (var sr = new StreamReader(result))
            {
                headers = sr.ReadToEnd();
            }

            action.Query = headers.Replace("+", "%20");
            return GetActionUrl(action);
        }

        /// <summary>
        /// More information can be found at:
        /// http://www.w3.org/html/wg/drafts/html/master/forms.html#submit-mailto-body
        /// </summary>
        private DocumentRequest MailAsBody(Url action, IHtmlElement submitter)
        {
            var formDataSet = ConstructDataSet(submitter);
            var enctype = Enctype;
            var encoding = TextEncoding.UsAscii;
            var stream = formDataSet.CreateBody(enctype, encoding);
            var body = String.Empty;

            using (var sr = new StreamReader(stream))
            {
                body = sr.ReadToEnd();
            }

            action.Query = "body=" + encoding.GetBytes(body).UrlEncode();
            return GetActionUrl(action);
        }

        /// <summary>
        /// More information can be found at:
        /// http://www.w3.org/html/wg/drafts/html/master/forms.html#submit-get-action
        /// </summary>
        private DocumentRequest GetActionUrl(Url action)
        {
            return DocumentRequest.Get(action, source: this, referer: Owner.DocumentUri);
        }

        /// <summary>
        /// Submits the body of the form.
        /// http://www.w3.org/html/wg/drafts/html/master/forms.html#submit-body
        /// </summary>
        private DocumentRequest SubmitAsEntityBody(Url target, IHtmlElement submitter)
        {
            var encoding = String.IsNullOrEmpty(AcceptCharset) ? Owner.CharacterSet : AcceptCharset;
            var formDataSet = ConstructDataSet(submitter);
            var enctype = Enctype;
            var body = formDataSet.CreateBody(enctype, encoding);

            if (enctype.Isi(MimeTypeNames.MultipartForm))
            {
                enctype = String.Concat(MimeTypeNames.MultipartForm, "; boundary=", formDataSet.Boundary);
            }

            return DocumentRequest.Post(target, body, enctype, source: this, referer: Owner.DocumentUri);
        }

        /// <summary>
        /// More information can be found at:
        /// http://www.w3.org/html/wg/drafts/html/master/forms.html#submit-mutate-action
        /// </summary>
        private DocumentRequest MutateActionUrl(Url action, IHtmlElement submitter)
        {
            var charset = String.IsNullOrEmpty(AcceptCharset) ? Owner.CharacterSet : AcceptCharset;
            var formDataSet = ConstructDataSet(submitter);
            var encoding = TextEncoding.Resolve(charset);
            var result = formDataSet.AsUrlEncoded(encoding);

            using (var sr = new StreamReader(result))
            {
                action.Query = sr.ReadToEnd();
            }

            return GetActionUrl(action);
        }

        private FormDataSet ConstructDataSet(IHtmlElement submitter)
        {
            var formDataSet = new FormDataSet();
            var fields = this.GetElements<HtmlFormControlElement>();

            foreach (var field in fields)
            {
                if (!field.IsDisabled && field.ParentElement is IHtmlDataListElement == false && Object.ReferenceEquals(field.Form, this))
                {
                    field.ConstructDataSet(formDataSet, submitter);
                }
            }

            return formDataSet;
        }

        #endregion
    }
}
