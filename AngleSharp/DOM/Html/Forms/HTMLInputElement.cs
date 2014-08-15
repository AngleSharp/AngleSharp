namespace AngleSharp.DOM.Html
{
    using AngleSharp.DOM.Io;
    using System;
    using System.Globalization;

    /// <summary>
    /// Represents an HTML input element.
    /// </summary>
    sealed class HTMLInputElement : HTMLTextFormControlElement, IHtmlInputElement
    {
        #region Fields

        Int32 _imageWidth;
        Int32 _imageHeight;
        Boolean? _checked;
        FileList _files;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML input element.
        /// </summary>
        public HTMLInputElement()
            : base(Tags.Input, NodeFlags.SelfClosing)
        {
            WillValidate = true;
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
                var date = DateTime.Now;

                if (DateTime.TryParse(Value, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                    return date;

                return null;
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
                var date = ValueAsDate;
                var test = 0.0;

                if (date.HasValue)
                    Value = date.Value.ToBinary().ToString();
                else if (Double.TryParse(Value, NumberStyles.Any, CultureInfo.InvariantCulture, out test))
                    return test;

                return Double.NaN;
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
            get { if (Form == null) return "post"; return Form.Method; }
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
        /// Gets or sets the height HTML attribute, which defines the
        /// height of the image displayed for the button, if the value
        /// of type is image.
        /// </summary>
        public Int32 DisplayHeight
        {
            get { return GetAttribute(AttributeNames.Height).ToInteger(_imageHeight); }
            set { SetAttribute(AttributeNames.Height, value.ToString()); }
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
            get { return GetAttribute(AttributeNames.Width).ToInteger(_imageWidth); }
            set { SetAttribute(AttributeNames.Width, value.ToString()); }
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
                        var value = "on";

                        if (!String.IsNullOrEmpty(Value))
                            value = Value;

                        dataSet.Append(Name, value, Type.ToString());
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
                        dataSet.Append(namex, "0", Type.ToString());
                        dataSet.Append(namey, "0", Type.ToString());
                    }

                    break;
                }
                case InputType.File:
                {
                    if(_files.Count == 0)
                        dataSet.Append(Name, String.Empty, MimeTypes.Binary);

                    foreach (var file in _files)
                        dataSet.Append(Name, file, Type.ToString());

                    break;
                }
                case InputType.Text:
                case InputType.Search:
                {
                    dataSet.Append(Name, Value, Type.ToString());

                    if (HasAttribute(AttributeNames.DirName))
                    {
                        var dirname = GetAttribute(AttributeNames.DirName);

                        if (String.IsNullOrEmpty(dirname))
                            break;

                        dataSet.Append(dirname, Direction.ToString().ToLower(), "Direction");
                    }

                    break;
                }
                default:
                {
                    dataSet.Append(Name, Value, Type.ToString());
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
            _files = new FileList();
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
    }
}
