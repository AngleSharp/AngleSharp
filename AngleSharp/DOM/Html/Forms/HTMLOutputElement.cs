namespace AngleSharp.DOM.Html
{
    using AngleSharp.DOM.Collections;
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents an HTML output element.
    /// </summary>
    sealed class HTMLOutputElement : HTMLFormControlElement, IHtmlOutputElement
    {
        #region Fields

        Boolean isDefaultValue;
        String _defaultValue;
        ISettableTokenList _for;

        #endregion

        #region ctor

        internal HTMLOutputElement()
            : base(Tags.Output)
        {
            isDefaultValue = true;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the default value of the element, initially the empty string.
        /// </summary>
        public String DefaultValue
        {
            get { return _defaultValue ?? TextContent; }
            set { _defaultValue = value; }
        }

        /// <summary>
        /// Gets or sets the value of the contents of the elements.
        /// </summary>
        public String Value
        {
            get { return TextContent; }
            set 
            {
                if (isDefaultValue)
                {
                    _defaultValue = Value;
                    isDefaultValue = false;
                }

                TextContent = value; 
            }
        }

        /// <summary>
        /// Gets the IDs of the labeled control. Reflects the for attribute.
        /// </summary>
        public ISettableTokenList HtmlFor
        {
            get { return _for ?? (_for = new SettableTokenList(this, AttributeNames.For)); }
        }

        /// <summary>
        /// Gets the type of input control (output).
        /// </summary>
        public String Type
        {
            get { return Tags.Output; }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Resets the form control to its initial value.
        /// </summary>
        internal override void Reset()
        {
            if (!isDefaultValue)
            {
                TextContent = _defaultValue;
                isDefaultValue = true;
            }
        }

        #endregion
    }
}
