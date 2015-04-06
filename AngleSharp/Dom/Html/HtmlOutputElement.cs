namespace AngleSharp.Dom.Html
{
    using AngleSharp.Dom.Collections;
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents an HTML output element.
    /// </summary>
    sealed class HtmlOutputElement : HtmlFormControlElement, IHtmlOutputElement
    {
        #region Fields

        String _defaultValue;
        String _value;
        SettableTokenList _for;

        #endregion

        #region ctor

        public HtmlOutputElement(Document owner, String prefix = null)
            : base(owner, Tags.Output, prefix)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the default value of the element, initially the empty
        /// string.
        /// </summary>
        public String DefaultValue
        {
            get { return _defaultValue ?? TextContent; }
            set { _defaultValue = value; }
        }

        /// <summary>
        /// Gets or sets the text content of a node and its descendants.
        /// </summary>
        public override String TextContent
        {
            get { return _value ?? _defaultValue ?? base.TextContent; }
            set { base.TextContent = value; }
        }

        /// <summary>
        /// Gets or sets the value of the contents of the elements.
        /// </summary>
        public String Value
        {
            get { return TextContent; }
            set { _value = value; }
        }

        /// <summary>
        /// Gets the IDs of the labeled control. Reflects the for attribute.
        /// </summary>
        public ISettableTokenList HtmlFor
        {
            get
            { 
                if (_for == null)
                {
                    _for = new SettableTokenList(GetOwnAttribute(AttributeNames.For));
                    _for.Changed += (s, ev) => UpdateAttribute(AttributeNames.For, _for.Value);
                }

                return _for; 
            }
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

        protected override Boolean CanBeValidated()
        {
            return true;
        }

        /// <summary>
        /// Resets the form control to its initial value.
        /// </summary>
        internal override void Reset()
        {
            _value = null;
        }

        #endregion
    }
}
