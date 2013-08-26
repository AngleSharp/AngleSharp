using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents a HTML textarea element.
    /// </summary>
    [DOM("HTMLTextAreaElement")]
    public sealed class HTMLTextAreaElement : HTMLTextFormControlElement
    {
        #region ctor

        /// <summary>
        /// Creates a new HTML textarea element.
        /// </summary>
        internal HTMLTextAreaElement()
        {
            _name = Tags.TEXTAREA;
            WillValidate = true;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the wrap HTML attribute, indicating how the control wraps text.
        /// </summary>
        [DOM("wrap")]
        public WrapType Wrap
        {
            get { return ToEnum(GetAttribute("wrap"), WrapType.Soft); }
            set { SetAttribute("wrap", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the default value of the input field.
        /// </summary>
        [DOM("defaultValue")]
        public override String DefaultValue
        {
            get { return TextContent; }
            set { TextContent = value; }
        }

        /// <summary>
        /// Gets the codepoint length of the control's value.
        /// </summary>
        [DOM("textLength")]
        public Int32 TextLength
        {
            get { return Value.Length; }
        }

        /// <summary>
        /// Gets or sets the rows HTML attribute, indicating
        /// the number of visible text lines for the control.
        /// </summary>
        [DOM("rows")]
        public UInt32 Rows
        {
            get { return ToInteger(GetAttribute("rows"), 2u); }
            set { SetAttribute("rows", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the cols HTML attribute, indicating
        /// the visible width of the text area.
        /// </summary>
        [DOM("cols")]
        public UInt32 Cols
        {
            get { return ToInteger(GetAttribute("cols"), 20u); }
            set { SetAttribute("cols", value.ToString()); }
        }

        /// <summary>
        /// Gets the type of input control (texarea).
        /// </summary>
        [DOM("type")]
        public String Type
        {
            get { return _name; }
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

        internal Boolean IsMutable
        {
            get { return !Disabled && !Readonly; }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Checks the form control for validity.
        /// </summary>
        /// <param name="state">The element's validity state tracker.</param>
        protected override void Check(ValidityState state)
        {
            //TODO
        }

        #endregion

        #region Enumeration
        
        /// <summary>
        /// An enumeration with possible wrap types.
        /// </summary>
        public enum WrapType : ushort
        {
            /// <summary>
            /// The text will be wrapped with tolerance.
            /// </summary>
            Soft,
            /// <summary>
            /// The text will be wrapped without tolerance.
            /// </summary>
            Hard
        }

        #endregion
    }
}
