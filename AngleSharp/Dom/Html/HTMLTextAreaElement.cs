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
        public HtmlTextAreaElement(Document owner)
            : base(owner, Tags.Textarea, NodeFlags.LineTolerance)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the wrap HTML attribute, indicating how the control wraps text.
        /// </summary>
        public String Wrap
        {
            get { return GetAttribute(AttributeNames.Wrap); }
            set { SetAttribute(AttributeNames.Wrap, value); }
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
            get { return GetAttribute(AttributeNames.Rows).ToInteger(2); }
            set { SetAttribute(AttributeNames.Rows, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the cols HTML attribute, indicating
        /// the visible width of the text area.
        /// </summary>
        public Int32 Columns
        {
            get { return GetAttribute(AttributeNames.Cols).ToInteger(20); }
            set { SetAttribute(AttributeNames.Cols, value.ToString()); }
        }

        /// <summary>
        /// Gets the type of input control (texarea).
        /// </summary>
        public String Type
        {
            get { return Tags.Textarea; }
        }

        #endregion

        #region Internal properties

        internal Boolean IsMutable
        {
            get { return !IsDisabled && !IsReadOnly; }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Constucts the data set (called from a form).
        /// </summary>
        /// <param name="dataSet">The dataset to construct.</param>
        /// <param name="submitter">The given submitter.</param>
        internal override void ConstructDataSet(FormDataSet dataSet, HtmlElement submitter)
        {
            ConstructDataSet(dataSet, Type);
        }

        internal override FormControlState SaveControlState()
        {
            return new FormControlState(Name, Type, Value);
        }

        internal override void RestoreFormControlState(FormControlState state)
        {
            if (state.Type == Type && state.Name == Name)
                Value = state.Value;
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
