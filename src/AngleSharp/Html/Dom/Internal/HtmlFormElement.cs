namespace AngleSharp.Html.Dom
{
    using AngleSharp.Browser;
    using AngleSharp.Dom;
    using AngleSharp.Html;
    using AngleSharp.Html.Dom.Events;
    using AngleSharp.Html.Forms;
    using AngleSharp.Html.Forms.Submitters;
    using AngleSharp.Io;
    using AngleSharp.Text;
    using Construction;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the form element.
    /// </summary>
    sealed class HtmlFormElement : HtmlElement, IHtmlFormElement, IConstructableFormElement
    {
        #region Fields

        private HtmlFormControlsCollection? _elements;

        #endregion

        #region ctor

        public HtmlFormElement(Document owner, String? prefix = null)
            : base(owner, TagNames.Form, prefix, NodeFlags.Special)
        {
        }

        #endregion

        #region Index

        public IElement? this[Int32 index] => Elements[index];

        public IElement? this[String name] => Elements[name];

        #endregion

        #region Properties

        public String? Name
        {
            get => this.GetOwnAttribute(AttributeNames.Name);
            set => this.SetOwnAttribute(AttributeNames.Name, value);
        }

        public Int32 Length => Elements.Length;

        public HtmlFormControlsCollection Elements => _elements ??= new HtmlFormControlsCollection(this);

        IHtmlFormControlsCollection IHtmlFormElement.Elements => Elements;

        public String? AcceptCharset
        {
            get => this.GetOwnAttribute(AttributeNames.AcceptCharset);
            set => this.SetOwnAttribute(AttributeNames.AcceptCharset, value);
        }

        public String Action
        {
            get => this.GetOwnAttribute(AttributeNames.Action) ?? Owner.DocumentUri;
            set => this.SetOwnAttribute(AttributeNames.Action, value);
        }

        public String? Autocomplete
        {
            get => this.GetOwnAttribute(AttributeNames.AutoComplete);
            set => this.SetOwnAttribute(AttributeNames.AutoComplete, value);
        }

        [AllowNull]
        public String Enctype
        {
            get => this.GetOwnAttribute(AttributeNames.Enctype).ToEncodingType() ?? MimeTypeNames.UrlencodedForm;
            set => this.SetOwnAttribute(AttributeNames.Enctype, value);
        }

        public String Encoding
        {
            get => Enctype;
            set => Enctype = value;
        }

        public String Method
        {
            get => this.GetOwnAttribute(AttributeNames.Method).ToFormMethod() ?? FormMethodNames.Get;
            set => this.SetOwnAttribute(AttributeNames.Method, value);
        }

        public Boolean NoValidate
        {
            get => this.GetBoolAttribute(AttributeNames.NoValidate);
            set => this.SetBoolAttribute(AttributeNames.NoValidate, value);
        }

        public String Target
        {
            get => this.GetOwnAttribute(AttributeNames.Target) ?? String.Empty;
            set => this.SetOwnAttribute(AttributeNames.Target, value);
        }

        #endregion

        #region Methods

        public Task<IDocument> SubmitAsync()
        {
            var request = GetSubmission();
            if (request is null)
            {
                return Task.FromResult<IDocument>(null!);
            }

            var context = Context.ResolveTargetContext(Target);
            return context.NavigateToAsync(request);
        }

        public Task<IDocument> SubmitAsync(IHtmlElement sourceElement)
        {
            if (SubmitEvent.IsSubmitButton(sourceElement))
            {
                return HtmlFormMemberExtensions.RequestSubmitAsync(this, sourceElement);
            }

            var request = GetSubmission(sourceElement);
            if (request is null)
            {
                return Task.FromResult<IDocument>(null!);
            }

            var context = Context.ResolveTargetContext(GetTarget(sourceElement));
            return context.NavigateToAsync(request);
        }

        internal Task<IDocument> SubmitRequestAsync(IHtmlElement? submitter)
        {
            var request = SubmitForm(submitter ?? this, true, submitter);
            if (request is null)
            {
                return Task.FromResult<IDocument>(null!);
            }

            var context = Context.ResolveTargetContext(GetTarget(submitter));
            return context.NavigateToAsync(request);
        }

        public DocumentRequest GetSubmission() => SubmitForm(this, true, null)!;

        public DocumentRequest GetSubmission(IHtmlElement sourceElement) => SubmitForm(sourceElement ?? this, false, sourceElement)!;

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

        private DocumentRequest? SubmitForm(IHtmlElement from, Boolean submittedFromSubmitMethod, IHtmlElement? submitter)
        {
            var owner = Owner;

            if ((owner.ActiveSandboxing & Sandboxes.Forms) == Sandboxes.Forms)
            {
                // The request is suppressed, but embedders still need to observe why
                // no document was produced. Context errors are the existing host signal
                // for failures handled internally rather than thrown into the DOM.
                Context.TrackError(new DomException(DomError.Security, "Blocked form submission by the browsing context sandbox."));
            }
            else if (!submittedFromSubmitMethod && !HasFormNoValidate(from) && !NoValidate && !CheckValidity())
            {
                this.FireSimpleEvent(EventNames.Invalid);
            }
            else
            {
                var actionText = GetSubmitterAttribute(submitter, AttributeNames.FormAction) ?? Action;
                var action = String.IsNullOrEmpty(actionText) ? new Url(owner.DocumentUri) : this.HyperReference(actionText);
                var scheme = action.Scheme;
                var methodText = GetSubmitterAttribute(submitter, AttributeNames.FormMethod);
                var method = (methodText ?? Method).ToEnum(HttpMethod.Get);
                var enctype = GetSubmitterEnctype(submitter);
                return SubmitForm(method, scheme, action, submitter ?? this, enctype);
            }

            return null;
        }

        private DocumentRequest SubmitForm(HttpMethod method, String scheme, Url action, IHtmlElement submitter, String enctype)
        {
            if (scheme.IsOneOf(ProtocolNames.Http, ProtocolNames.Https))
            {
                if (method == HttpMethod.Get)
                {
                    return MutateActionUrl(action, submitter);
                }
                else if (method == HttpMethod.Post)
                {
                    return SubmitAsEntityBody(action, submitter, enctype);
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
                    return PostToData(action, submitter, enctype);
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
                    return MailAsBody(action, submitter, enctype);
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
        private DocumentRequest PostToData(Url action, IHtmlElement submitter, String enctype)
        {
            var charset = String.IsNullOrEmpty(AcceptCharset) ? Owner.CharacterSet : AcceptCharset;
            var formDataSet = ConstructDataSet(submitter);
            var htmlEncoder = Owner.Context.GetService<IHtmlEncoder>()!;
            var result = String.Empty;
            var stream = formDataSet.CreateBody(enctype, charset, htmlEncoder);

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
        private DocumentRequest MailAsBody(Url action, IHtmlElement submitter, String enctype)
        {
            var formDataSet = ConstructDataSet(submitter);
            var encoding = TextEncoding.UsAscii;
            var htmlEncoder = Owner.Context.GetService<IHtmlEncoder>()!;
            var stream = formDataSet.CreateBody(enctype, encoding, htmlEncoder);
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
        private DocumentRequest GetActionUrl(Url action) => DocumentRequest.Get(action, source: this, referer: Owner.DocumentUri);

        /// <summary>
        /// Submits the body of the form.
        /// http://www.w3.org/html/wg/drafts/html/master/forms.html#submit-body
        /// </summary>
        private DocumentRequest SubmitAsEntityBody(Url url, IHtmlElement submitter, String enctype)
        {
            var charset = String.IsNullOrEmpty(AcceptCharset) ? Owner.CharacterSet : AcceptCharset;
            var formDataSet = ConstructDataSet(submitter);
            var htmlEncoder = Owner.Context.GetService<IHtmlEncoder>()!;
            var body = formDataSet.CreateBody(enctype, charset, htmlEncoder);

            if (enctype.Isi(MimeTypeNames.MultipartForm))
            {
                return DocumentRequest.PostAsMultipart(url, body, formDataSet.Boundary);
            }

            return DocumentRequest.Post(url, body, enctype, source: this, referer: Owner.DocumentUri);
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
            var fields = this.GetNodes<HtmlFormControlElement>();

            foreach (var field in fields)
            {
                if (!field.IsDisabled && field.ParentElement is IHtmlDataListElement == false && Object.ReferenceEquals(field.Form, this))
                {
                    field.ConstructDataSet(formDataSet, submitter);
                }
            }

            return formDataSet;
        }

        private String GetTarget(IHtmlElement? submitter) =>
            GetSubmitterAttribute(submitter, AttributeNames.FormTarget) ?? Target;

        private String GetSubmitterEnctype(IHtmlElement? submitter)
        {
            if (submitter is not null && SubmitEvent.IsSubmitButton(submitter) && submitter.HasAttribute(AttributeNames.FormEncType))
            {
                return submitter.GetAttribute(AttributeNames.FormEncType).ToEncodingType() ?? MimeTypeNames.UrlencodedForm;
            }

            return Enctype;
        }

        private static Boolean HasFormNoValidate(IHtmlElement element)
        {
            return element switch
            {
                IHtmlButtonElement button => button.FormNoValidate,
                IHtmlInputElement input => input.FormNoValidate,
                _ => element.HasAttribute(AttributeNames.FormNoValidate),
            };
        }

        private static String? GetSubmitterAttribute(IHtmlElement? submitter, String name) =>
            submitter is not null && SubmitEvent.IsSubmitButton(submitter) && submitter.HasAttribute(name) ? submitter.GetAttribute(name) : null;

        #endregion
    }
}
