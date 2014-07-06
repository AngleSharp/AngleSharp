namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents a HTML textarea element.
    /// </summary>
    [DomName("HTMLTextAreaElement")]
    public sealed class HTMLTextAreaElement : HTMLTextFormControlElement
    {
        #region ctor

        /// <summary>
        /// Creates a new HTML textarea element.
        /// </summary>
        internal HTMLTextAreaElement()
        {
            _name = Tags.Textarea;
            WillValidate = true;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the wrap HTML attribute, indicating how the control wraps text.
        /// </summary>
        [DomName("wrap")]
        public WrapType Wrap
        {
            get { return ToEnum(GetAttribute("wrap"), WrapType.Soft); }
            set { SetAttribute("wrap", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the default value of the input field.
        /// </summary>
        [DomName("defaultValue")]
        public override String DefaultValue
        {
            get { return TextContent; }
            set { TextContent = value; }
        }

        /// <summary>
        /// Gets the codepoint length of the control's value.
        /// </summary>
        [DomName("textLength")]
        public Int32 TextLength
        {
            get { return Value.Length; }
        }

        /// <summary>
        /// Gets or sets the rows HTML attribute, indicating
        /// the number of visible text lines for the control.
        /// </summary>
        [DomName("rows")]
        public UInt32 Rows
        {
            get { return ToInteger(GetAttribute("rows"), 2u); }
            set { SetAttribute("rows", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the cols HTML attribute, indicating
        /// the visible width of the text area.
        /// </summary>
        [DomName("cols")]
        public UInt32 Cols
        {
            get { return ToInteger(GetAttribute("cols"), 20u); }
            set { SetAttribute("cols", value.ToString()); }
        }

        /// <summary>
        /// Gets the type of input control (texarea).
        /// </summary>
        [DomName("type")]
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
            get { return !IsDisabled && !Readonly; }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Constucts the data set (called from a form).
        /// </summary>
        /// <param name="dataSet">The dataset to construct.</param>
        /// <param name="submitter">The given submitter.</param>
        internal override void ConstructDataSet(FormDataSet dataSet, HTMLElement submitter)
        {
            dataSet.Append(Name, Value, Type.ToString());

            if (HasAttribute(AttributeNames.DirName))
            {
                var dirname = GetAttribute(AttributeNames.DirName);

                if (String.IsNullOrEmpty(dirname))
                    return;

                dataSet.Append(dirname, Dir.ToString().ToLower(), "Direction");
            }
        }

        /// <summary>
        /// Checks the form control for validity.
        /// </summary>
        /// <param name="state">The element's validity state tracker.</param>
        protected override void Check(IValidityState state)
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
