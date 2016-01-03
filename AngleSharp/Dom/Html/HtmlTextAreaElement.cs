namespace AngleSharp.Dom.Html
{
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents a HTML textarea element.
    /// </summary>
    sealed class HtmlTextAreaElement : HtmlTextFormControlElement, IHtmlTextAreaElement
    {
        #region ctor

        /// <summary>
        /// Creates a new HTML textarea element.
        /// </summary>
        public HtmlTextAreaElement(Document owner, String prefix = null)
            : base(owner, TagNames.Textarea, prefix, NodeFlags.LineTolerance)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the wrap HTML attribute, indicating how the control wraps text.
        /// </summary>
        public String Wrap
        {
            get { return this.GetOwnAttribute(AttributeNames.Wrap); }
            set { this.SetOwnAttribute(AttributeNames.Wrap, value); }
        }

        /// <summary>
        /// Gets or sets the default value of the input field.
        /// </summary>
        public override String DefaultValue
        {
            get { return TextContent; }
            set { TextContent = value; }
        }

        /// <summary>
        /// Gets the codepoint length of the control's value.
        /// </summary>
        public Int32 TextLength
        {
            get { return Value.Length; }
        }

        /// <summary>
        /// Gets or sets the rows HTML attribute, indicating
        /// the number of visible text lines for the control.
        /// </summary>
        public Int32 Rows
        {
            get { return this.GetOwnAttribute(AttributeNames.Rows).ToInteger(2); }
            set { this.SetOwnAttribute(AttributeNames.Rows, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the cols HTML attribute, indicating
        /// the visible width of the text area.
        /// </summary>
        public Int32 Columns
        {
            get { return this.GetOwnAttribute(AttributeNames.Cols).ToInteger(20); }
            set { this.SetOwnAttribute(AttributeNames.Cols, value.ToString()); }
        }

        /// <summary>
        /// Gets the type of input control (texarea).
        /// </summary>
        public String Type
        {
            get { return TagNames.Textarea; }
        }

        #endregion

        #region Internal properties

        internal Boolean IsMutable
        {
            get { return !IsDisabled && !IsReadOnly; }
        }

        #endregion

        #region Helpers

        internal override void ConstructDataSet(FormDataSet dataSet, IHtmlElement submitter)
        {
            ConstructDataSet(dataSet, Type);
        }

        internal override FormControlState SaveControlState()
        {
            return new FormControlState(Name, Type, Value);
        }

        internal override void RestoreFormControlState(FormControlState state)
        {
            if (state.Type.Is(Type) && state.Name.Is(Name))
            {
                Value = state.Value;
            }
        }

        #endregion
    }
}
