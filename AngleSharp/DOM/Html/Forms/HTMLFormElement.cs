using System;
using AngleSharp.DOM.Collections;
using System.Collections.Generic;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the form element.
    /// </summary>
    [DOM("HTMLFormElement")]
    public sealed class HTMLFormElement : HTMLElement
    {
        #region Members

        HTMLLiveCollection<HTMLFormControlElement> _elements;
        HTMLFormControlsCollection _formControls;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML form element.
        /// </summary>
        internal HTMLFormElement()
        {
            _name = Tags.FORM;
            _elements = new HTMLLiveCollection<HTMLFormControlElement>(this);
            _formControls = new HTMLFormControlsCollection(_elements);
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
            get { return _formControls; }
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
            get { return ToEnum(GetAttribute("method"), HttpMethod.GET); }
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

        #endregion

        #region Methods

        /// <summary>
        /// Submits the form element from the form element itself.
        /// </summary>
        /// <returns>The current form element.</returns>
        [DOM("submit")]
        public HTMLFormElement Submit()
        {
            SubmitForm(this, true);
            return this;
        }

        /// <summary>
        /// Resets the form to the previous (default) state.
        /// </summary>
        /// <returns>The current form element.</returns>
        [DOM("reset")]
        public HTMLFormElement Reset()
        {
            foreach (var element in _elements.Elements)
                element.Reset();

            return this;
        }

        /// <summary>
        /// Checks if the form is valid, i.e. if all fields fulfill their requirements.
        /// </summary>
        /// <returns>True if the form is valid, otherwise false.</returns>
        [DOM("checkValidity")]
        public Boolean CheckValidity()
        {
            foreach (var element in _elements.Elements)
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

            var browsingContext = new object();//TODO

            if (!submittedFromSubmitMethod && from.Attributes[AttributeNames.FORMNOVALIDATE] == null && NoValidate)
            {
                if (!CheckValidity())
                {
                    FireSimpleEvent(EventNames.INVALID);
                    return;
                }
            }

            var formDataSet = ConstructDataSet();
            var action = Action;

            if (String.IsNullOrEmpty(action))
                action = formDocument.DocumentURI;

            if (!Location.IsAbsolute(action))
                action = Location.MakeAbsolute(from.BaseURI, action);

            //Enctype
            //Method
            //Target

            //TODO
            //If the user indicated a specific browsing context to use when submitting
            //the form, then let target browsing context be that browsing context.
            //Otherwise, apply the rules for choosing a browsing context given a browsing
            //context name using target as the name and form browsing context as the
            //context in which the algorithm is executed, and let target browsing context
            //be the resulting browsing context.

            //TODO
            //If target browsing context was created in the previous step, or, alternatively,
            //if the form document has not yet completely loaded and the submitted from
            //submit() method is set, then let replace be true. Otherwise, let it be false

            var replace = false;
            var location = new Location(action);

            switch (location.Protocol)
            {
                case "http:":
                case "https:":
                    if (Method == HttpMethod.GET)
                        MutateActionUrl();
                    else if (Method == HttpMethod.POST)
                        SubmitAsEntityBody();
                    break;

                case "ftp:":
                case "javascript:":
                    GetActionUrl();
                    break;

                case "data:":
                    if (Method == HttpMethod.GET)
                        GetActionUrl();
                    else if (Method == HttpMethod.POST)
                        PostToData();
                    break;

                case "mailto:":
                    if (Method == HttpMethod.GET)
                        MailWithHeaders();
                    else if (Method == HttpMethod.POST)
                        MailAsBody();
                    break;
            }

            //TODO
        }

        /// <summary>
        /// http://www.w3.org/html/wg/drafts/html/master/forms.html#submit-data-post
        /// </summary>
        void PostToData()
        {
        }

        /// <summary>
        /// http://www.w3.org/html/wg/drafts/html/master/forms.html#submit-mailto-headers
        /// </summary>
        void MailWithHeaders()
        {
        }

        /// <summary>
        /// http://www.w3.org/html/wg/drafts/html/master/forms.html#submit-mailto-body
        /// </summary>
        void MailAsBody()
        {
        }

        /// <summary>
        /// http://www.w3.org/html/wg/drafts/html/master/forms.html#submit-get-action
        /// </summary>
        void GetActionUrl()
        {
        }

        /// <summary>
        /// http://www.w3.org/html/wg/drafts/html/master/forms.html#submit-body
        /// </summary>
        void SubmitAsEntityBody()
        {
        }

        /// <summary>
        /// http://www.w3.org/html/wg/drafts/html/master/forms.html#submit-mutate-action
        /// </summary>
        void MutateActionUrl()
        {
        }

        FormDataSet ConstructDataSet(HTMLElement submitter = null)
        {
            var formDataSet = new FormDataSet();
            var fields = _elements.Elements;

            foreach (var field in fields)
            {
                if (field.ParentElement is HTMLDataListElement)
                    continue;
                else if (field.Disabled)
                    continue;

                field.ConstructDataSet(formDataSet, submitter);
            }

            return formDataSet;
        }

        String CheckEncType(String encType)
        {
            if (encType == MimeTypes.Plain || encType == MimeTypes.MultipartForm)
                return encType;

            return MimeTypes.StandardForm;
        }

        #endregion

        #region Internal properties

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
