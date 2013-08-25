using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents an HTML output element.
    /// </summary>
    public sealed class HTMLOutputElement : HTMLFormControlElement
    {
        #region Constant

        /// <summary>
        /// The output tag.
        /// </summary>
        internal const String Tag = "output";

        #endregion

        #region Members

        Boolean isDefaultValue;
        String defValue;

        #endregion

        #region ctor

        internal HTMLOutputElement()
        {
            _name = Tag;
            defValue = String.Empty;
            isDefaultValue = true;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the default value of the element, initially the empty string.
        /// </summary>
        [DOM("defaultValue")]
        public String DefaultValue
        {
            get { return defValue; }
        }

        /// <summary>
        /// Gets or sets the value of the contents of the elements.
        /// </summary>
        [DOM("value")]
        public String Value
        {
            get { return TextContent; }
            set 
            {
                if (isDefaultValue)
                {
                    defValue = Value;
                    isDefaultValue = false;
                }

                TextContent = value; 
            }
        }

        /// <summary>
        /// Gets or sets the ID of the labeled control. Reflects the for attribute.
        /// </summary>
        [DOM("htmlFor")]
        public String HtmlFor
        {
            get { return GetAttribute("for"); }
            set { SetAttribute("for", value); }
        }

        /// <summary>
        /// Gets the type of input control (output).
        /// </summary>
        [DOM("type")]
        public String Type
        {
            get { return Tag; }
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
                TextContent = defValue;
                isDefaultValue = true;
            }
        }

        #endregion
    }
}
