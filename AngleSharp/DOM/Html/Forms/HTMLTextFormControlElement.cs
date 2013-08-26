using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the base class for all HTML text form controls.
    /// </summary>
    public abstract class HTMLTextFormControlElement : HTMLFormControlElementWithState
    {
        #region Members

        String _value;
        SelectionType _direction;
        UInt32 _start;
        UInt32 _end;

        #endregion

        #region ctor

        internal HTMLTextFormControlElement()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the maxlength HTML attribute, indicating
        /// the maximum number of characters the user can enter.
        /// This constraint is evaluated only when the value changes.
        /// </summary>
        [DOM("maxLength")]
        public Int32 MaxLength
        {
            get { return ToInteger(GetAttribute("maxlength"), -1); }
            set { SetAttribute("maxlength", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the default value of the input field.
        /// </summary>
        [DOM("defaultValue")]
        public abstract String DefaultValue
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the current value in the control.
        /// </summary>
        [DOM("value")]
        public String Value
        {
            get { return _value ?? DefaultValue; }
            set { _value = value; }
        }

        /// <summary>
        /// Gets or sets the placeholder HTML attribute, containing a hint to
        /// the user about what to enter in the control.
        /// </summary>
        [DOM("placeholder")]
        public String Placeholder
        {
            get { return GetAttribute("placeholder"); }
            set { SetAttribute("placeholder", value); }
        }

        /// <summary>
        /// Gets or sets the accesskey HTML attribute.
        /// </summary>
        [DOM("accessKey")]
        public String AccessKey
        {
            get { return GetAttribute("accesskey"); }
            set { SetAttribute("accesskey", value); }
        }

        /// <summary>
        /// Gets or sets if the field is required.
        /// </summary>
        [DOM("required")]
        public Boolean Required
        {
            get { return GetAttribute("required") != null; }
            set { SetAttribute("required", value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets or sets if the textarea field is read-only.
        /// </summary>
        [DOM("readOnly")]
        public Boolean Readonly
        {
            get { return GetAttribute("readonly") != null; }
            set { SetAttribute("readonly", value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets or sets the index of the beginning of selected text.
        /// If no text is selected, contains the index of the character
        /// that follows the input cursor. On being set, the control behaves
        /// as if setSelectionRange() had been called with this as the first
        /// argument, and selectionEnd as the second argument.
        /// </summary>
        [DOM("selectionStart")]
        public UInt32 SelectionStart
        {
            get { return _start; }
            set { SetSelectionRange(value, _end, _direction); }
        }

        /// <summary>
        /// Gets or sets the index of the end of selected text. If no text
        /// is selected, contains the index of the character that follows
        /// the input cursor. On being set, the control behaves as if
        /// setSelectionRange() had been called with this as the second
        /// argument, and selectionStart as the first argument.
        /// </summary>
        [DOM("selectionEnd")]
        public UInt32 SelectionEnd
        {
            get { return _end; }
            set { SetSelectionRange(_start, value, _direction); }
        }

        /// <summary>
        /// Gets the direction in which selection occurred. This
        /// is "forward" if selection was performed in the start-to-end
        /// direction of the current locale, or "backward" for the opposite
        /// direction.
        /// </summary>
        [DOM("selectionDirection")]
        public SelectionType SelectionDirection
        {
            get { return _direction; }
        }

        #endregion

        #region Members

        /// <summary>
        /// Selects a range of text, and sets selectionStart and selectionEnd.
        /// If either argument is greater than the length of the value, it is treated
        /// as pointing to the end of the value. If end is less than start, then
        /// both are treated as the value of end.
        /// </summary>
        /// <param name="selectionStart">The start of the selection.</param>
        /// <param name="selectionEnd">The end of the selection.</param>
        /// <param name="selectionDirection">Optional: The direction of the selection.</param>
        [DOM("setSelectionRange")]
        public void SetSelectionRange(UInt32 selectionStart, UInt32 selectionEnd, SelectionType selectionDirection = SelectionType.None)
        {
            if (selectionEnd > (UInt32)Value.Length)
                selectionEnd = (UInt32)Value.Length;

            if (selectionEnd < selectionStart)
                selectionStart = selectionEnd;

            _start = selectionStart;
            _end = selectionEnd;
            _direction = selectionDirection;
        }
        
        /// <summary>
        /// Selects the contents of the control.
        /// </summary>
        [DOM("select")]
        public void Select()
        {
            SetSelectionRange(0u, (UInt32)Value.Length, SelectionType.Forward);
        }

        #endregion

        #region Enumeration

        /// <summary>
        /// An enumeration with possible selection directions.
        /// </summary>
        public enum SelectionType : ushort
        {
            /// <summary>
            /// The text selection direction is unknown.
            /// </summary>
            None,
            /// <summary>
            /// The text is selected in forward direction.
            /// </summary>
            Forward,
            /// <summary>
            /// The text is selected in backward direction.
            /// </summary>
            Backward
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Resets the form control to its initial value.
        /// </summary>
        internal override void Reset()
        {
            _value = null;
            SetSelectionRange(UInt32.MaxValue, UInt32.MaxValue);
        }

        #endregion
    }
}
