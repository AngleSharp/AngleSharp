namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Html;
    using AngleSharp.Text;
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Represents the base class for all HTML text form controls.
    /// </summary>
    abstract class HtmlTextFormControlElement : HtmlFormControlElementWithState
    {
        #region Fields

        private String? _value;
        private SelectionType _direction;
        private Int32 _start;
        private Int32 _end;

        #endregion

        #region ctor

        public HtmlTextFormControlElement(Document owner, String name, String? prefix, NodeFlags flags = NodeFlags.None)
            : base(owner, name, prefix, flags)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets if the value has been modified.
        /// </summary>
        public Boolean IsDirty { get; set; }

        /// <summary>
        /// Gets or sets the dirname HTML attribute.
        /// </summary>
        public String? DirectionName
        {
            get => this.GetOwnAttribute(AttributeNames.DirName);
            set => this.SetOwnAttribute(AttributeNames.DirName, value);
        }

        /// <summary>
        /// Gets or sets the maxlength HTML attribute, indicating
        /// the maximum number of characters the user can enter.
        /// This constraint is evaluated only when the value changes.
        /// </summary>
        public Int32 MaxLength
        {
            get => this.GetOwnAttribute(AttributeNames.MaxLength).ToInteger(-1);
            set => this.SetOwnAttribute(AttributeNames.MaxLength, value.ToString());
        }

        /// <summary>
        /// Gets or sets the minlength HTML attribute, indicating
        /// the minimum number of characters the user can enter.
        /// This constraint is evaluated only when the value changes.
        /// </summary>
        public Int32 MinLength
        {
            get => this.GetOwnAttribute(AttributeNames.MinLength).ToInteger(0);
            set => this.SetOwnAttribute(AttributeNames.MinLength, value.ToString());
        }

        /// <summary>
        /// Gets or sets the default value of the input field.
        /// </summary>
        public abstract String DefaultValue
        {
            get;
            set;
        }

        /// <summary>
        /// Gets if the input field has a value (via attribute or directly).
        /// </summary>
        public Boolean HasValue => _value != null || this.HasOwnAttribute(AttributeNames.Value);

        /// <summary>
        /// Gets or sets the current value in the control.
        /// </summary>
        [AllowNull]
        public String Value
        {
            get => _value ?? DefaultValue;
            set => _value = value;
        }

        /// <summary>
        /// Gets or sets the placeholder HTML attribute, containing a hint to
        /// the user about what to enter in the control.
        /// </summary>
        public String? Placeholder
        {
            get => this.GetOwnAttribute(AttributeNames.Placeholder);
            set => this.SetOwnAttribute(AttributeNames.Placeholder, value);
        }

        /// <summary>
        /// Gets or sets if the field is required.
        /// </summary>
        public Boolean IsRequired
        {
            get => this.GetBoolAttribute(AttributeNames.Required);
            set => this.SetBoolAttribute(AttributeNames.Required, value);
        }

        /// <summary>
        /// Gets or sets if the textarea field is read-only.
        /// </summary>
        public Boolean IsReadOnly
        {
            get => this.GetBoolAttribute(AttributeNames.Readonly);
            set => this.SetBoolAttribute(AttributeNames.Readonly, value);
        }

        /// <summary>
        /// Gets or sets the index of the beginning of selected text.
        /// If no text is selected, contains the index of the character
        /// that follows the input cursor. On being set, the control behaves
        /// as if setSelectionRange() had been called with this as the first
        /// argument, and selectionEnd as the second argument.
        /// </summary>
        public Int32 SelectionStart
        {
            get => _start;
            set => SetSelectionRange(value, _end, _direction);
        }

        /// <summary>
        /// Gets or sets the index of the end of selected text. If no text
        /// is selected, contains the index of the character that follows
        /// the input cursor. On being set, the control behaves as if
        /// setSelectionRange() had been called with this as the second
        /// argument, and selectionStart as the first argument.
        /// </summary>
        public Int32 SelectionEnd
        {
            get => _end;
            set => SetSelectionRange(_start, value, _direction);
        }

        /// <summary>
        /// Gets the direction in which selection occurred. This
        /// is "forward" if selection was performed in the start-to-end
        /// direction of the current locale, or "backward" for the opposite
        /// direction.
        /// </summary>
        public String? SelectionDirection => _direction.ToString().ToLowerInvariant();

        #endregion

        #region Methods

        public override Node Clone(Document owner, Boolean deep)
        {
            var node = (HtmlTextFormControlElement)base.Clone(owner, deep);
            node.IsDirty = IsDirty;
            node._value = _value;
            node._direction = _direction;
            node._start = _start;
            node._end = _end;
            return node;
        }

        /// <summary>
        /// Selects a range of text, and sets selectionStart and selectionEnd.
        /// If either argument is greater than the length of the value, it is treated
        /// as pointing to the end of the value. If end is less than start, then
        /// both are treated as the value of end.
        /// </summary>
        /// <param name="selectionStart">The start of the selection.</param>
        /// <param name="selectionEnd">The end of the selection.</param>
        /// <param name="selectionDirection">Optional: The direction of the selection.</param>
        public void Select(Int32 selectionStart, Int32 selectionEnd, String? selectionDirection = null)
        {
            SetSelectionRange(selectionStart, selectionEnd, selectionDirection.ToEnum(SelectionType.Forward));
        }
        
        /// <summary>
        /// Selects the contents of the control.
        /// </summary>
        public void SelectAll()
        {
            SetSelectionRange(0, Value.Length, SelectionType.Forward);
        }

        #endregion

        #region Enumeration

        /// <summary>
        /// An enumeration with possible selection directions.
        /// </summary>
        public enum SelectionType : byte
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

        protected override void Check(ValidityState state)
        {
            var value = Value ?? String.Empty;
            var length = value.Length;
            var maxlength = MaxLength;
            var minlength = MinLength;
            state.IsValueMissing = IsRequired && length == 0;
            state.IsTooLong = IsDirty && maxlength > -1 && length > maxlength;
            state.IsTooShort = IsDirty && length > 0 && length < minlength;
            base.Check(state);
        }

        protected override Boolean CanBeValidated()
        {
            return !IsReadOnly && !this.HasDataListAncestor();
        }

        protected void ConstructDataSet(FormDataSet dataSet, String type)
        {
            dataSet.Append(Name!, Value, type);
            var dirname = this.GetOwnAttribute(AttributeNames.DirName);

            if (dirname is { Length: > 0 })
            {
                dataSet.Append(dirname, Direction!.ToString().ToLowerInvariant(), "Direction");
            }
        }

        private void SetSelectionRange(Int32 selectionStart, Int32 selectionEnd, SelectionType selectionType)
        {
            var length = (Value ?? String.Empty).Length;

            if (selectionEnd > length)
            {
                selectionEnd = length;
            }

            if (selectionEnd < selectionStart)
            {
                selectionStart = selectionEnd;
            }

            _start = selectionStart;
            _end = selectionEnd;
            _direction = selectionType;
        }

        /// <summary>
        /// Resets the form control to its initial value.
        /// </summary>
        internal override void Reset()
        {
            Value = null;
            Select(Int32.MaxValue, Int32.MaxValue);
        }

        #endregion
    }
}
