using System;
using AngleSharp.DOM.Collections;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the form element.
    /// </summary>
    public sealed class HTMLFormElement : HTMLElement
    {
        #region Constants

        /// <summary>
        /// The form tag.
        /// </summary>
        internal const string Tag = "form";

        #endregion

        #region Members

        HTMLFormControlsCollection _elements;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML form element.
        /// </summary>
        internal HTMLFormElement()
        {
            _name = Tag;
            _elements = new HTMLFormControlsCollection();
        }

        #endregion

        #region Index

        /// <summary>
        /// Gets the form element at the specified index.
        /// </summary>
        /// <param name="index">The index in the elements collection.</param>
        /// <returns>The element or null.</returns>
        public Element this[int index]
        {
            get { return _elements[index]; }
        }

        /// <summary>
        /// Gets the form element(s) with the specified name.
        /// </summary>
        /// <param name="name">The name or id of the element.</param>
        /// <returns>A collection with elements, an element or null.</returns>
        public object this[string name]
        {
            get { return _elements[name]; }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the form is actually valid.
        /// </summary>
        public bool IsValid
        {
            //TODO
            get { return true; }
        }

        /// <summary>
        /// Gets or sets the value of the name attribute.
        /// </summary>
        public string Name
        {
            get { return GetAttribute("name"); }
            set { SetAttribute("name", value); }
        }

        /// <summary>
        /// Gets the number of elements in the Elements collection.
        /// </summary>
        public int Length
        {
            get { return _elements.Length; }
        }

        /// <summary>
        /// Gets all the form controls belonging to this form element.
        /// </summary>
        public HTMLFormControlsCollection Elements
        {
            get { return _elements; }
        }

        /// <summary>
        /// Gets or sets the character encodings that are to be used for the submission.
        /// </summary>
        public string AcceptCharset
        {
            get { return GetAttribute("acceptCharset"); }
            set { SetAttribute("acceptCharset", value); }
        }

        /// <summary>
        /// Gets or sets the form's name within the forms collection.
        /// </summary>
        public string Action
        {
            get { return GetAttribute("action"); }
            set { SetAttribute("action", value); }
        }

        /// <summary>
        /// Gets or sets if autocomplete is turned on or off.
        /// </summary>
        public PowerState Autocomplete
        {
            get { return ToEnum(GetAttribute("autocomplete"), PowerState.On); }
            set { SetAttribute("autocomplete", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the encoding to use for sending the form.
        /// </summary>
        public string Enctype
        {
            get { return CheckEncType(GetAttribute("enctype")); }
            set { SetAttribute("enctype", CheckEncType(value)); }
        }

        /// <summary>
        /// Gets or sets the encoding to use for sending the form.
        /// </summary>
        public string Encoding
        {
            get { return Enctype; }
            set { Enctype = value; }
        }

        /// <summary>
        /// Gets or sets the method to use for transmitting the form.
        /// </summary>
        public HttpMethod Method
        {
            get { return ToEnum(GetAttribute("method"), HttpMethod.GET); }
            set { SetAttribute("method", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the indicator that the form is not to be validated during submission.
        /// </summary>
        public bool NoValidate
        {
            get { return GetAttribute("novalidate") != null; }
            set { SetAttribute("novalidate", value ? string.Empty : null); }
        }

        /// <summary>
        /// Gets or sets the target name of the response to the request.
        /// </summary>
        public string Target
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
        public HTMLFormElement Submit()
        {
            //TODO
            //http://www.w3.org/html/wg/drafts/html/master/forms.html#dom-form-submit
            return this;
        }

        /// <summary>
        /// Resets the form to the previous (default) state.
        /// </summary>
        /// <returns>The current form element.</returns>
        public HTMLFormElement Reset()
        {
            //TODO
            return this;
        }

        /// <summary>
        /// Checks if the form is valid, i.e. if all fields fulfill their requirements.
        /// </summary>
        /// <returns>True if the form is valid, otherwise false.</returns>
        public bool CheckValidity()
        {
            //TODO
            return true;
        }
        
        #endregion

        #region Helpers

        string CheckEncType(string encType)
        {
            switch (encType)
            {
                case "application/x-www-form-urlencoded":
                case "multipart/form-data":
                case "text/plain":
                    return encType;

                default:
                    return "application/x-www-form-urlencoded";
            }
        }

        #endregion

        #region Internal properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override bool IsSpecial
        {
            get { return true; }
        }

        #endregion
    }
}
