namespace AngleSharp.DOM.Html
{
    using AngleSharp.DOM.Io;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Network;
    using AngleSharp.Services.Media;
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents an HTML input element.
    /// </summary>
    sealed class HTMLInputElement : HTMLTextFormControlElement, IHtmlInputElement
    {
        #region Fields

        readonly FileList _files;

        Task<IImageInfo> _imageTask;
        Boolean? _checked;

        static readonly Regex email = new Regex("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$");
        static readonly Regex number = new Regex("^\\-?\\d+(\\.\\d+)?([eE][\\-\\+]?\\d+)?$");
        static readonly Regex color = new Regex("^\\#[0-9A-Fa-f]{6}$");

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML input element.
        /// </summary>
        public HTMLInputElement()
            : base(Tags.Input, NodeFlags.SelfClosing)
        {
            _files = new FileList();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the default value of the input field.
        /// </summary>
        public override String DefaultValue
        {
            get { return GetAttribute(AttributeNames.Value); }
            set { SetAttribute(AttributeNames.Value, value); }
        }

        /// <summary>
        /// Gets or sets
        /// </summary>
        public Boolean IsDefaultChecked
        {
            get { return GetAttribute(AttributeNames.Checked) != null; }
            set { SetAttribute(AttributeNames.Checked, value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets or sets if the input element is checked or not.
        /// </summary>
        public Boolean IsChecked
        {
            get { return _checked.HasValue ? _checked.Value : IsDefaultChecked; }
            set { _checked = value; }
        }

        /// <summary>
        /// Gets or sets the type of the input field.
        /// </summary>
        public String Type
        {
            get { return GetAttribute(AttributeNames.Type); }
            set { SetAttribute(AttributeNames.Type, value); }
        }

        /// <summary>
        /// Gets or sets if the state if indeterminate.
        /// </summary>
        public Boolean IsIndeterminate 
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the multiple HTML attribute, whichindicates whether multiple items can be selected.
        /// </summary>
        public Boolean IsMultiple
        {
            get { return GetAttribute(AttributeNames.Multiple) != null; }
            set { SetAttribute(AttributeNames.Multiple, value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets or sets the value of the element, interpreted as a date, or null
        /// if conversion is not possible.
        /// </summary>
        public DateTime? ValueAsDate
        {
            get 
            {
                var converter = GetDateConverter(Type.ToEnum(InputType.Text));
                return converter(Value);
            }
            set { Value = value.HasValue ? value.Value.ToString(CultureInfo.InvariantCulture) : null; }
        }

        /// <summary>
        /// Gets or sets the value of the element, interpreted as one of the following in order:
        /// 1.) Time value 2.) Number 3.) otherwise NaN.
        /// </summary>
        public Double ValueAsNumber
        {
            get 
            {
                var converter = GetNumberConverter(Type.ToEnum(InputType.Text));
                return converter(Value) ?? Double.NaN;
            }
            set { Value = value.ToString(CultureInfo.InvariantCulture); }
        }

        /// <summary>
        /// Gets or sets the URI of a resource that processes information submitted by the button.
        /// If specified, this attribute overrides the action attribute of the form element that owns this element.
        /// </summary>
        public String FormAction
        {
            get { if (Form == null) return String.Empty; return Form.Action; }
            set { if (Form != null) Form.Action = value; }
        }

        /// <summary>
        /// Gets or sets the type of content that is used to submit the form to the server. If specified, this
        /// attribute overrides the enctype attribute of the form element that owns this element.
        /// </summary>
        public String FormEncType
        {
            get { if (Form == null) return String.Empty; return Form.Enctype; }
            set { if (Form != null) Form.Enctype = value; }
        }

        /// <summary>
        /// Gets or sets the HTTP method that the browser uses to submit the form. If specified, this attribute
        /// overrides the method attribute of the form element that owns this element.
        /// </summary>
        public String FormMethod
        {
            get { if (Form == null) return String.Empty; return Form.Method; }
            set { if (Form != null) Form.Method = value; }
        }

        /// <summary>
        /// Gets or sets that the form is not to be validated when it is submitted. If specified, this attribute
        /// overrides the enctype attribute of the form element that owns this element.
        /// </summary>
        public Boolean FormNoValidate
        {
            get { if (Form == null) return false; return Form.NoValidate; }
            set { if (Form != null) Form.NoValidate = value; }
        }

        /// <summary>
        /// Gets or sets A name or keyword indicating where to display the response that is received after submitting
        /// the form. If specified, this attribute overrides the target attribute of the form element that owns this element.
        /// </summary>
        public String FormTarget
        {
            get { if (Form == null) return String.Empty; return Form.Target; }
            set { if (Form != null) Form.Target = value; }
        }

        /// <summary>
        /// Gets or sets the accept HTML attribute, containing comma-separated list of
        /// file types accepted by the server when type is file.
        /// </summary>
        public String Accept
        {
            get { return GetAttribute(AttributeNames.Accept); }
            set { SetAttribute(AttributeNames.Accept, value); }
        }

        /// <summary>
        /// Gets or sets the alignment of the element.
        /// </summary>
        public Alignment Align
        {
            get { return GetAttribute(AttributeNames.Align).ToEnum(Alignment.Left); }
            set { SetAttribute(AttributeNames.Align, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the alt HTML attribute, containing alternative
        /// text to use when type is image.
        /// </summary>
        public String AlternativeText
        {
            get { return GetAttribute(AttributeNames.Alt); }
            set { SetAttribute(AttributeNames.Alt, value); }
        }

        /// <summary>
        /// Gets or sets the autocomplete HTML attribute, indicating whether
        /// the value of the control can be automatically completed by the
        /// browser. Ignored if the value of the type attribute is hidden,
        /// checkbox, radio, file, or a button type (button, submit, reset,
        /// image).
        /// </summary>
        public String Autocomplete
        {
            get { return GetAttribute(AttributeNames.AutoComplete); }
            set { SetAttribute(AttributeNames.AutoComplete, value); }
        }

        /// <summary>
        /// Gets a list of selected files.
        /// </summary>
        public IFileList Files
        {
            get { return _files; }
        }

        /// <summary>
        /// Gets the datalist element in the same document.
        /// Only options that are valid values for this input element will
        /// be displayed. This attribute is ignored when the type
        /// attribute's value is hidden, checkbox, radio, file, or a button type.
        /// </summary>
        public IHtmlElement List
        {
            get 
            {
                var owner = Owner;

                if (owner != null)
                    return owner.GetElementById(GetAttribute(AttributeNames.List)) as IHtmlElement; 

                return null;
            }
        }

        /// <summary>
        /// Gets or sets max HTML attribute, containing the maximum (numeric
        /// or date-time) value for this item, which must not be less than its
        /// minimum (min attribute) value.
        /// </summary>
        public String Maximum
        {
            get { return GetAttribute(AttributeNames.Max); }
            set { SetAttribute(AttributeNames.Max, value); }
        }

        /// <summary>
        /// Gets or sets the min HTML attribute, containing the minimum (numeric
        /// or date-time) value for this item, which must not be greater than its
        /// maximum (max attribute) value.
        /// </summary>
        public String Minimum
        {
            get { return GetAttribute(AttributeNames.Min); }
            set { SetAttribute(AttributeNames.Min, value); }
        }

        /// <summary>
        /// Gets or sets the pattern HTML attribute, containing a regular expression
        /// that the control's value is checked against. The pattern must match the
        /// entire value, not just some subset. This attribute applies when the value
        /// of the type attribute is text, search, tel, url or email; otherwise it is ignored.
        /// </summary>
        public String Pattern
        {
            get { return GetAttribute(AttributeNames.Pattern); }
            set { SetAttribute(AttributeNames.Pattern, value); }
        }

        /// <summary>
        /// Gets or sets the size HTML attribute, containing size of the control. This value
        /// is in pixels unless the value of type is text or password, in which case, it is
        /// an integer number of characters. Applies only when type is set to text, search, tel,
        /// url, email, or password; otherwise it is ignored.
        /// </summary>
        public Int32 Size
        {
            get { return GetAttribute(AttributeNames.Size).ToInteger(20); }
            set { SetAttribute(AttributeNames.Size, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the src HTML attribute, which specifies a URI for the location of an
        /// image to display on the graphical submit button, if the value of type is image;
        /// otherwise it is ignored.
        /// </summary>
        public String Source
        {
            get { return GetAttribute(AttributeNames.Src); }
            set { SetAttribute(AttributeNames.Src, value); }
        }

        /// <summary>
        /// Gets or sets the step HTML attribute, which works with min and max to limit the
        /// increments at which a numeric or date-time value can be set. It can be the string
        /// any or a positive floating point number. If this is not set to any, the control
        /// accepts only values at multiples of the step value greater than the minimum.
        /// </summary>
        public String Step
        {
            get { return GetAttribute(AttributeNames.Step); }
            set { SetAttribute(AttributeNames.Step, value); }
        }

        /// <summary>
        /// Gets or sets a client-side image map.
        /// </summary>
        public String UseMap
        {
            get { return GetAttribute(AttributeNames.UseMap); }
            set { SetAttribute(AttributeNames.UseMap, value); }
        }

        /// <summary>
        /// Gets or sets the width HTML attribute, which defines the width of the image
        /// displayed for the button, if the value of type is image.
        /// </summary>
        public Int32 DisplayWidth
        {
            get { return GetAttribute(AttributeNames.Width).ToInteger(OriginalWidth); }
            set { SetAttribute(AttributeNames.Width, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the height HTML attribute, which defines the
        /// height of the image displayed for the button, if the value
        /// of type is image.
        /// </summary>
        public Int32 DisplayHeight
        {
            get { return GetAttribute(AttributeNames.Height).ToInteger(OriginalHeight); }
            set { SetAttribute(AttributeNames.Height, value.ToString()); }
        }

        /// <summary>
        /// Gets the width of the image.
        /// </summary>
        public Int32 OriginalWidth
        {
            get { return _imageTask != null ? (_imageTask.IsCompleted && _imageTask.Result != null ? _imageTask.Result.Width : 0) : 0; }
        }

        /// <summary>
        /// Gets the height of the image.
        /// </summary>
        public Int32 OriginalHeight
        {
            get { return _imageTask != null ? (_imageTask.IsCompleted && _imageTask.Result != null ? _imageTask.Result.Height : 0) : 0; }
        }

        #endregion

        #region Design properties

        /// <summary>
        /// Gets or sets if the link has been visited.
        /// </summary>
        internal Boolean IsVisited
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if the link is currently active.
        /// </summary>
        internal Boolean IsActive
        {
            get;
            set;
        }

        #endregion

        #region Methods

        internal override FormControlState SaveControlState()
        {
            return new FormControlState(Name, Type, Value);
        }

        internal override void RestoreFormControlState(FormControlState state)
        {
            if (state.Type == Type && state.Name == Name)
                Value = state.Value;
        }

        /// <summary>
        /// Increments the value by (step * n), where n defaults to 1 if not specified.
        /// </summary>
        /// <param name="n">Optional: The number of steps to take.</param>
        public void StepUp(Int32 n = 1)
        {
            DoStep(n);
        }

        /// <summary>
        /// Decrements the value by (step * n), where n defaults to 1 if not specified. 
        /// </summary>
        /// <param name="n">Optional: The number of steps to take.</param>
        public void StepDown(Int32 n = 1)
        {
            DoStep(-n);
        }

        #endregion

        #region Internal Properties

        internal Boolean IsMutable 
        {
            get { return !IsDisabled && !IsReadOnly; }
        }

        #endregion

        #region Internal Methods

        internal override void Close()
        {
            var type = Type.ToEnum(InputType.Text);

            if (type == InputType.Image)
            {
                var src = Source;

                if (src != null)
                {
                    var url = this.HyperRef(src);
                    _imageTask = Owner.Options.LoadResource<IImageInfo>(url);
                    _imageTask.ContinueWith(task => this.FireSimpleEvent(EventNames.Load));
                }
            }
        }

        Double GetStep(InputType type)
        {
            var step = Step;

            if (String.IsNullOrEmpty(step))
                return GetDefaultStep(type) * GetStepScaleFactor(type);
            else if (step.Equals(Keywords.Any, StringComparison.OrdinalIgnoreCase))
                return 0.0;

            var num = ConvertFromNumber(step);

            if (num.HasValue == false || num <= 0.0)
                return GetDefaultStep(type) * GetStepScaleFactor(type);

            return num.Value * GetStepScaleFactor(type);
        }

        Double GetStepBase(InputType type)
        {
            var converter = GetNumberConverter(type);
            var num = converter(Minimum);

            if (num.HasValue)
                return num.Value;

            num = converter(Value);

            if (num.HasValue)
                return num.Value;

            switch (type)
            {
                case InputType.Month:
                case InputType.Time:
                case InputType.Datetime:
                case InputType.Date:
                case InputType.Week:
                    break;
            }

            return 0.0;
        }

        Double GetDefaultStep(InputType type)
        {
            switch (type)
            {
                case InputType.Datetime:
                case InputType.Time:
                    return 60.0;
                case InputType.Month:
                case InputType.Date:
                case InputType.Week:
                default:
                    return 1.0;
            }
        }

        Double GetStepScaleFactor(InputType type)
        {
            switch (type)
            {
                case InputType.Datetime:
                case InputType.Time:
                    return 1000.0;
                case InputType.Date:
                    return 86400000.0;
                case InputType.Week:
                    return 604800000;
                case InputType.Month:
                default:
                    return 1.0;
            }
        }

        #endregion

        #region Enumeration

        /// <summary>
        /// An enumeration with possible input types.
        /// </summary>
        public enum InputType : ushort
        {
            /// <summary>
            /// The input will be hidden.
            /// </summary>
            Hidden,
            /// <summary>
            /// A standard (1-line) text input.
            /// </summary>
            Text,
            /// <summary>
            /// A search input.
            /// </summary>
            Search,
            /// <summary>
            /// A telephone number input.
            /// </summary>
            Tel,
            /// <summary>
            /// An URL input field.
            /// </summary>
            Url,
            /// <summary>
            /// An email input field.
            /// </summary>
            Email,
            /// <summary>
            /// A password input field.
            /// </summary>
            Password,
            /// <summary>
            /// A datetime input field.
            /// </summary>
            Datetime,
            /// <summary>
            /// A date input field.
            /// </summary>
            Date,
            /// <summary>
            /// A month picker input field.
            /// </summary>
            Month,
            /// <summary>
            /// A week picker input field.
            /// </summary>
            Week,
            /// <summary>
            /// A time picker input field.
            /// </summary>
            Time,
            /// <summary>
            /// A number input field.
            /// </summary>
            Number,
            /// <summary>
            /// A range picker.
            /// </summary>
            Range,
            /// <summary>
            /// A color picker input field.
            /// </summary>
            Color,
            /// <summary>
            /// A checkbox.
            /// </summary>
            Checkbox,
            /// <summary>
            /// A radio box.
            /// </summary>
            Radio,
            /// <summary>
            /// A file upload box.
            /// </summary>
            File,
            /// <summary>
            /// A submit button.
            /// </summary>
            Submit,
            /// <summary>
            /// An image input box.
            /// </summary>
            Image,
            /// <summary>
            /// A reset form button.
            /// </summary>
            Reset,
            /// <summary>
            /// A simple button.
            /// </summary>
            Button
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Constructs the data set (called from a form).
        /// </summary>
        /// <param name="dataSet">The dataset to construct.</param>
        /// <param name="submitter">The given submitter.</param>
        internal override void ConstructDataSet(FormDataSet dataSet, HTMLElement submitter)
        {
            var type = Type.ToEnum(InputType.Text);

            switch (type)
            {
                case InputType.Radio:
                case InputType.Checkbox:
                {
                    if (IsChecked)
                    {
                        var value = Keywords.On;

                        if (!String.IsNullOrEmpty(Value))
                            value = Value;

                        dataSet.Append(Name, value, Type);
                    }

                    break;
                }
                case InputType.Image:
                {
                    if (!String.IsNullOrEmpty(Name))
                    {
                        var name = String.Empty;

                        if (!String.IsNullOrEmpty(Value))
                            name = Value + ".";

                        var namex = name + "x";
                        var namey = name + "y";

                        //TODO get x and y of submitter and save those
                        dataSet.Append(namex, "0", Type);
                        dataSet.Append(namey, "0", Type);
                    }

                    break;
                }
                case InputType.File:
                {
                    if(_files.Count == 0)
                        dataSet.Append(Name, String.Empty, MimeTypes.Binary);

                    foreach (var file in _files)
                        dataSet.Append(Name, file, Type);

                    break;
                }
                case InputType.Text:
                case InputType.Search:
                {
                    ConstructDataSet(dataSet, Type);
                    break;
                }
                default:
                {
                    dataSet.Append(Name, Value, Type);
                    break;
                }
            }
        }

        /// <summary>
        /// Takes the given number of steps.
        /// </summary>
        /// <param name="n">The number of steps (pos. or neg.) to take.</param>
        void DoStep(Int32 n)
        {
            var type = Type.ToEnum(InputType.Text);
            var step = Step;

            if (step != null)
            {
                switch (type)
                {
                    case InputType.Date:
                    case InputType.Datetime:
                    case InputType.Week:
                    case InputType.Time:
                    {
                        var t = TimeSpan.Zero;

                        if (ValueAsDate.HasValue && TimeSpan.TryParse(step, CultureInfo.InvariantCulture, out t))
                        {
                            var date = ValueAsDate.Value;

                            for (int i = 0; i < n; i++)
                                date = date.Add(t);

                            for (int i = 0; i > n; i--)
                                date = date.Subtract(t);

                            if (IsBetween(date))
                            {
                                ValueAsDate = date;
                                return;
                            }
                        }

                        break;
                    }

                    case InputType.Range:
                    case InputType.Number:
                    {
                        var t = 0.0;

                        if (!Double.IsNaN(ValueAsNumber) && Double.TryParse(step, NumberStyles.Any, CultureInfo.InvariantCulture, out t))
                        {
                            t = ValueAsNumber + t * n;

                            if (IsBetween(t))
                            {
                                ValueAsNumber = t;
                                return;
                            }
                        }

                        break;
                    }
                }
            }

            throw new DomException(ErrorCode.InvalidState);
        }

        /// <summary>
        /// Checks if the numeric value is between min and max.
        /// </summary>
        /// <param name="value">The value to check for the range-constraint.</param>
        /// <returns>True if the value is between min, max otherwise false.</returns>
        Boolean IsBetween(Double value)
        {
            var t = 0.0;

            if (Minimum != null && Double.TryParse(Minimum, NumberStyles.Any, CultureInfo.InvariantCulture, out t) && t > value)
                return false;
            else if (Maximum != null && Double.TryParse(Maximum, NumberStyles.Any, CultureInfo.InvariantCulture, out t) && t < value)
                return false;

            return true;
        }

        /// <summary>
        /// Checks if the date value is between min and max.
        /// </summary>
        /// <param name="value">The value to check for the range-constraint.</param>
        /// <returns>True if the value is between min, max otherwise false.</returns>
        Boolean IsBetween(DateTime value)
        {
            var t = DateTime.Now;

            if (Minimum != null && DateTime.TryParse(Minimum, CultureInfo.InvariantCulture, DateTimeStyles.None, out t) && t > value)
                return false;
            else if (Maximum != null && DateTime.TryParse(Maximum, CultureInfo.InvariantCulture, DateTimeStyles.None, out t) && t < value)
                return false;

            return true;
        }

        /// <summary>
        /// Resets the form control to its initial value.
        /// </summary>
        internal override void Reset()
        {
            base.Reset();
            _checked = null;
            _files.Clear();
        }

        /// <summary>
        /// Checks the form control for validity.
        /// </summary>
        /// <param name="state">The element's validity state tracker.</param>
        protected override void Check(ValidityState state)
        {
            base.Check(state);
            var value = Value ?? String.Empty;
            var type = Type.ToEnum(InputType.Text);
            state.IsPatternMismatch = IsInvalidPattern(Pattern, value);
            state.IsRangeOverflow = false;
            state.IsRangeUnderflow = false;
            state.IsTypeMismatch = false;

            switch (type)
            {
                case InputType.Range:
                case InputType.Number:
                    EvaluateNumber(state, value, type);
                    break;
                case InputType.Radio:
                case InputType.Checkbox:
                    state.IsValueMissing = IsRequired && IsChecked == false;
                    break;
                case InputType.Time:
                    EvaluateDate(state, value, type);
                    break;
                case InputType.Date:
                    EvaluateDate(state, value, type);
                    break;
                case InputType.Datetime:
                    EvaluateDate(state, value, type);
                    break;
                case InputType.Week:
                    EvaluateDate(state, value, type);
                    break;
                case InputType.Month:
                    EvaluateDate(state, value, type);
                    break;
                case InputType.Email:
                    if (IsInvalidEmail(IsMultiple, value))
                    {
                        state.IsTypeMismatch = !String.IsNullOrEmpty(value);
                        state.IsBadInput = state.IsTypeMismatch;
                    }
                    break;
                case InputType.Url:
                    if (IsInvalidUrl(value))
                    {
                        state.IsTypeMismatch = !String.IsNullOrEmpty(value);
                        state.IsBadInput = state.IsTypeMismatch;
                    }
                    break;
                case InputType.Color:
                    state.IsBadInput = color.IsMatch(value) == false;
                    state.IsValueMissing = IsRequired && state.IsBadInput;
                    break;
            }
        }

        void EvaluateNumber(ValidityState state, String value, InputType type)
        {
            var converter = GetNumberConverter(type);
            var num = converter(value);

            if (num.HasValue)
            {
                var step = GetStep(type);
                var min = converter(Minimum);
                var max = converter(Maximum);

                if (min.HasValue)
                    state.IsRangeUnderflow = num < min.Value;
                
                if (max.HasValue)
                    state.IsRangeOverflow = num > max.Value;

                state.IsStepMismatch = step != 0.0 && GetStepBase(type) % step != 0.0;
            }
            else
            {
                state.IsValueMissing = IsRequired;
            }
        }

        void EvaluateDate(ValidityState state, String value, InputType type)
        {
            var converter = GetDateConverter(type);
            var date = converter(value);

            if (date.HasValue)
            {
                var step = GetStep(type);
                var min = converter(Minimum);
                var max = converter(Maximum);

                if (min.HasValue)
                    state.IsRangeUnderflow = date < min.Value;
                
                if (max.HasValue)
                    state.IsRangeOverflow = date > max.Value;

                state.IsStepMismatch = step != 0.0 && GetStepBase(type) % step != 0.0;
            }
            else
            {
                state.IsValueMissing = IsRequired;
                state.IsBadInput = !String.IsNullOrEmpty(value);
            }
        }

        protected override Boolean CanBeValidated()
        {
            var type = Type.ToEnum(InputType.Text);

            switch (type)
            {
                case InputType.Reset:
                case InputType.Button:
                case InputType.Hidden:
                    return false;
                default:
                    return base.CanBeValidated();
            }
        }

        static Boolean IsInvalidEmail(Boolean multiple, String value)
        {
            if (multiple)
            {
                var mails = value.Split(',');

                foreach (var mail in mails)
                {
                    if (email.IsMatch(mail.Trim()) == false)
                        return true;
                }

                return false;
            }

            return email.IsMatch(value.Trim()) == false;
        }

        static Boolean IsInvalidPattern(String pattern, String value)
        {
            if (!String.IsNullOrEmpty(pattern) && !String.IsNullOrEmpty(value))
            {
                try
                {
                    var regex = new Regex(pattern, RegexOptions.ECMAScript);
                    return regex.IsMatch(value) == false;
                }
                catch { }
            }

            return false;
        }

        static Boolean IsInvalidUrl(String value)
        {
            if (!String.IsNullOrEmpty(value))
            {
                var url = new Url(value);
                return url.IsInvalid || url.IsRelative;
            }

            return false;
        }

        static Double? ConvertFromNumber(String value)
        {
            if (!String.IsNullOrEmpty(value) && number.IsMatch(value))
                return Double.Parse(value);

            return null;
        }
        
        static DateTime? ConvertFromWeek(String value)
        {
            if (String.IsNullOrEmpty(value))
                return null;

            var position = 0;
            var year = 0;
            var week = 0;

            while (position < value.Length)
            {
                if (value[position].IsDigit())
                    position++;
                else
                    break;
            }

            if (position < 4 || 
                position != value.Length - 4 || 
                value[position + 0] != Specification.Minus || 
                value[position + 1] != 'W' || 
                value[position + 2].IsDigit() == false || 
                value[position + 3].IsDigit() == false)
                return null;

            year = Int32.Parse(value.Substring(0, position));
            week = Int32.Parse(value.Substring(position + 2)) - 1;

            if (year < 0 || year > 9999)
                return null;

            var endOfYear = new DateTime(year, 12, 31);
            var cal = CultureInfo.InvariantCulture.Calendar;
            var numOfWeeks = cal.GetWeekOfYear(endOfYear, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            if (week < 0 || week >= numOfWeeks)
                return null;

            var startOfYear = new DateTime(year, 1, 1);
            var day = cal.GetDayOfWeek(startOfYear);

            if (day == DayOfWeek.Sunday)
                startOfYear = startOfYear.AddDays(1);
            else if (day > DayOfWeek.Monday)
                startOfYear = startOfYear.AddDays(8 - (Int32)day);

            return startOfYear.AddDays(7 * week);
        }

        static DateTime? ConvertFromMonth(String value)
        {
            if (String.IsNullOrEmpty(value))
                return null;

            var position = 0;
            var year = 0;
            var month = 0;

            while (position < value.Length)
            {
                if (value[position].IsDigit())
                    position++;
                else
                    break;
            }

            if (position < 4 ||
                position != value.Length - 3 ||
                value[position + 0] != Specification.Minus ||
                value[position + 1].IsDigit() == false ||
                value[position + 2].IsDigit() == false)
                return null;

            year = Int32.Parse(value.Substring(0, position));
            month = Int32.Parse(value.Substring(position + 1));

            if (year < 0 || year > 9999 || month < 1 || month > 12)
                return null;

            return new DateTime(year, month, 1);
        }

        static DateTime? ConvertFromDate(String value)
        {
            if (String.IsNullOrEmpty(value))
                return null;

            var position = 0;
            var year = 0;
            var month = 0;
            var day = 0;

            while (position < value.Length)
            {
                if (value[position].IsDigit())
                    position++;
                else
                    break;
            }

            if (position < 4 ||
                position != value.Length - 6 ||
                value[position + 0] != Specification.Minus ||
                value[position + 1].IsDigit() == false ||
                value[position + 2].IsDigit() == false ||
                value[position + 3] != Specification.Minus ||
                value[position + 4].IsDigit() == false ||
                value[position + 5].IsDigit() == false)
                return null;

            year = Int32.Parse(value.Substring(0, position));
            month = Int32.Parse(value.Substring(position + 1, 2));
            day = Int32.Parse(value.Substring(position + 4, 2));
            var cal = CultureInfo.InvariantCulture.Calendar;

            if (year < 0 || year > 9999 || month < 1 || month > 12 || day < 1 || day > cal.GetDaysInMonth(year, month))
                return null;

            return new DateTime(year, month, day);
        }

        static TimeSpan? ConvertFromTime(String value, ref Int32 position)
        {
            var offset = position;
            var hour = 0;
            var minute = 0;
            var second = 0;
            var ms = 0;

            if (value.Length < 5 + offset || value[position++].IsDigit() == false || value[position++].IsDigit() == false || value[position++] != Specification.Colon)
                return null;

            hour = Int32.Parse(value.Substring(offset, 2));

            if (hour < 0 || hour > 23)
                return null;

            if (value[position++].IsDigit() == false || value[position++].IsDigit() == false)
                return null;

            minute = Int32.Parse(value.Substring(3 + offset, 2));

            if (minute < 0 || minute > 59)
                return null;

            if (value.Length >= 8 + offset && value[position] == Specification.Colon)
            {
                position++;

                if (value[position++].IsDigit() == false || value[position++].IsDigit() == false)
                    return null;

                second = Int32.Parse(value.Substring(6 + offset, 2));

                if (second < 0 || second > 59)
                    return null;

                if (position + 1 < value.Length && value[position] == Specification.Dot)
                {
                    position++;
                    var start = position;

                    while (position < value.Length)
                    {
                        if (value[position].IsDigit())
                            position++;
                        else
                            break;
                    }

                    var fraction = value.Substring(start, position - start);
                    ms = Int32.Parse(fraction) * (Int32)Math.Pow(10, 3 - fraction.Length);
                }
            }

            return new TimeSpan(0, hour, minute, second, ms);
        }

        static DateTime? ConvertFromTime(String value)
        {
            if (String.IsNullOrEmpty(value))
                return null;

            var position = 0;
            var ts = ConvertFromTime(value, ref position);

            if (ts == null || position != value.Length)
                return null;

            return new DateTime().Add(ts.Value);
        }

        static DateTime? ConvertFromDateTime(String value)
        {
            if (String.IsNullOrEmpty(value))
                return null;

            var position = 0;
            var year = 0;
            var month = 0;
            var day = 0;

            while (position < value.Length)
            {
                if (value[position].IsDigit())
                    position++;
                else
                    break;
            }

            if (position < 4 ||
                position > value.Length - 13 ||
                value[position + 0] != Specification.Minus ||
                value[position + 1].IsDigit() == false ||
                value[position + 2].IsDigit() == false ||
                value[position + 3] != Specification.Minus ||
                value[position + 4].IsDigit() == false ||
                value[position + 5].IsDigit() == false)
                return null;

            year = Int32.Parse(value.Substring(0, position));
            month = Int32.Parse(value.Substring(position + 1, 2));
            day = Int32.Parse(value.Substring(position + 4, 2));
            position += 6;
            var cal = CultureInfo.InvariantCulture.Calendar;
            var requireOffset = value[position] == ' ';

            if (year < 0 || year > 9999 || month < 1 || month > 12 || day < 1 || day > cal.GetDaysInMonth(year, month) || (requireOffset == false && value[position] != 'T'))
                return null;

            position++;
            var ts = ConvertFromTime(value, ref position);
            var dt = new DateTime(year, month, day);

            if (ts == null)
                return null;

            dt = dt.Add(ts.Value);

            if (position == value.Length)
            {
                if (requireOffset)
                    return null;

                return dt;
            }

            if (value[position] != 'Z')
            {
                if (position + 6 != value.Length ||
                    value[position + 1].IsDigit() == false ||
                    value[position + 2].IsDigit() == false ||
                    value[position + 3] != Specification.Colon ||
                    value[position + 4].IsDigit() == false ||
                    value[position + 5].IsDigit() == false)
                    return null;

                var hours = Int32.Parse(value.Substring(position + 1, 2));
                var minutes = Int32.Parse(value.Substring(position + 4, 2));
                var offset = new TimeSpan(hours, minutes, 0);

                if (value[position] == '+')
                    dt = dt.Add(offset);
                else if (value[position] == '-')
                    dt = dt.Subtract(offset);
                else
                    return null;
            }
            else if (position + 1 != value.Length)
                return null;
            else
                dt = dt.ToUniversalTime();

            return dt;
        }

        static Func<String, Double?> GetNumberConverter(InputType type)
        {
            switch (type)
            {
                case InputType.Month:
                    return value =>
                    {
                        var dt = ConvertFromMonth(value);

                        if (dt.HasValue)
                            return dt.Value.Month - 1;

                        return null;
                    };
                case InputType.Time:
                    return value =>
                    {
                        var dt = ConvertFromTime(value);

                        if (dt.HasValue)
                            return dt.Value.Subtract(new DateTime()).TotalMilliseconds;

                        return null;
                    };
                case InputType.Datetime:
                    return value =>
                    {
                        var dt = ConvertFromDateTime(value);

                        if (dt.HasValue)
                            return dt.Value.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;

                        return null;
                    };
                case InputType.Date:
                    return value =>
                    {
                        var dt = ConvertFromDate(value);

                        if (dt.HasValue)
                            return dt.Value.Subtract(new DateTime(1970, 1, 1, 0, 0, 0).AddDays(-1)).TotalMilliseconds;

                        return null;
                    };
                case InputType.Week:
                    return value =>
                    {
                        var dt = ConvertFromWeek(value);

                        if (dt.HasValue)
                            return dt.Value.Subtract(new DateTime(1970, 1, 5, 0, 0, 0)).TotalMilliseconds;

                        return null;
                    };
                default:
                    return ConvertFromNumber;
            }
        }

        static Func<String, DateTime?> GetDateConverter(InputType type)
        {
            switch (type)
            {
                case InputType.Month:
                    return ConvertFromMonth;
                case InputType.Time:
                    return ConvertFromTime;
                case InputType.Datetime:
                    return ConvertFromDateTime;
                case InputType.Date:
                    return ConvertFromDate;
                case InputType.Week:
                    return ConvertFromWeek;
                default:
                    return ConvertFromDateTime;
            }
        }

        #endregion
    }
}
