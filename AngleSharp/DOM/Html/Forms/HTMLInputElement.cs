using AngleSharp.DOM.Collections;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents an HTML input element.
    /// </summary>
    [DOM("HTMLInputElement")]
    public sealed class HTMLInputElement : HTMLTextFormControlElement
    {
        #region Members

        UInt32 _imageWidth;
        UInt32 _imageHeight;
        Boolean? _checked;
        FileList _files;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML input element.
        /// </summary>
        internal HTMLInputElement()
        {
            _name = Tags.INPUT;
            WillValidate = true;
            _files = new FileList();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the default value of the input field.
        /// </summary>
        [DOM("defaultValue")]
        public override String DefaultValue
        {
            get { return GetAttribute("value"); }
            set { SetAttribute("value", value); }
        }

        /// <summary>
        /// Gets or sets
        /// </summary>
        [DOM("defaultChecked")]
        public Boolean DefaultChecked
        {
            get { return GetAttribute("checked") != null; }
            set { SetAttribute("checked", value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets or sets if the input element is checked or not.
        /// </summary>
        [DOM("checked")]
        public Boolean Checked
        {
            get { return _checked.HasValue ? _checked.Value : DefaultChecked; }
            set { _checked = value; }
        }

        /// <summary>
        /// Gets or sets the type of the input field.
        /// </summary>
        [DOM("type")]
        public InputType Type
        {
            get { return ToEnum(GetAttribute("type"), InputType.Text); }
            set { SetAttribute("type", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets if the state if indeterminate.
        /// </summary>
        [DOM("indeterminate")]
        public Boolean Indeterminate 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Gets the multiple HTML attribute, whichindicates whether multiple items can be selected.
        /// </summary>
        [DOM("multiple")]
        public Boolean Multiple
        {
            get { return GetAttribute("multiple") != null; }
            set { SetAttribute("multiple", value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets or sets the value of the element, interpreted as a date, or null
        /// if conversion is not possible.
        /// </summary>
        [DOM("valueAsDate")]
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
        [DOM("valueAsNumber")]
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
        [DOM("formAction")]
        public String FormAction
        {
            get { if (Form == null) return String.Empty; return Form.Action; }
            set { if (Form != null) Form.Action = value; }
        }

        /// <summary>
        /// Gets or sets the type of content that is used to submit the form to the server. If specified, this
        /// attribute overrides the enctype attribute of the form element that owns this element.
        /// </summary>
        [DOM("formEncType")]
        public String FormEncType
        {
            get { if (Form == null) return String.Empty; return Form.Enctype; }
            set { if (Form != null) Form.Enctype = value; }
        }

        /// <summary>
        /// Gets or sets the HTTP method that the browser uses to submit the form. If specified, this attribute
        /// overrides the method attribute of the form element that owns this element.
        /// </summary>
        [DOM("formMethod")]
        public HttpMethod FormMethod
        {
            get { if (Form == null) return HttpMethod.POST; return Form.Method; }
            set { if (Form != null) Form.Method = value; }
        }

        /// <summary>
        /// Gets or sets that the form is not to be validated when it is submitted. If specified, this attribute
        /// overrides the enctype attribute of the form element that owns this element.
        /// </summary>
        [DOM("formNoValidate")]
        public Boolean FormNoValidate
        {
            get { if (Form == null) return false; return Form.NoValidate; }
            set { if (Form != null) Form.NoValidate = value; }
        }

        /// <summary>
        /// Gets or sets A name or keyword indicating where to display the response that is received after submitting
        /// the form. If specified, this attribute overrides the target attribute of the form element that owns this element.
        /// </summary>
        [DOM("formTarget")]
        public String FormTarget
        {
            get { if (Form == null) return String.Empty; return Form.Target; }
            set { if (Form != null) Form.Target = value; }
        }

        /// <summary>
        /// Gets or sets the accept HTML attribute, containing comma-separated list of
        /// file types accepted by the server when type is file.
        /// </summary>
        [DOM("accept")]
        public String Accept
        {
            get { return GetAttribute("accept"); }
            set { SetAttribute("accept", value); }
        }

        /// <summary>
        /// Gets or sets the alignment of the element.
        /// </summary>
        [DOM("align")]
        public Alignment Align
        {
            get { return ToEnum(GetAttribute("align"), Alignment.Left); }
            set { SetAttribute("align", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the alt HTML attribute, containing alternative
        /// text to use when type is image.
        /// </summary>
        [DOM("alt")]
        public String Alt
        {
            get { return GetAttribute("alt"); }
            set { SetAttribute("alt", value); }
        }

        /// <summary>
        /// Gets or sets the autocomplete HTML attribute, indicating whether
        /// the value of the control can be automatically completed by the
        /// browser. Ignored if the value of the type attribute is hidden,
        /// checkbox, radio, file, or a button type (button, submit, reset,
        /// image).
        /// </summary>
        [DOM("autocomplete")]
        public PowerState Autocomplete
        {
            get { return ToEnum(GetAttribute("autocomplete"), PowerState.Off); }
            set { SetAttribute("autocomplete", value.ToString()); }
        }

        /// <summary>
        /// Gets a list of selected files.
        /// </summary>
        [DOM("files")]
        public FileList Files
        {
            get { return _files; }
        }

        /// <summary>
        /// Gets or sets the height HTML attribute, which defines the
        /// height of the image displayed for the button, if the value
        /// of type is image.
        /// </summary>
        [DOM("height")]
        public UInt32 Height
        {
            get { return ToInteger(GetAttribute("height"), _imageHeight); }
            set { SetAttribute("height", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the id of a datalist element in the same document.
        /// Only options that are valid values for this input element will
        /// be displayed. /// This attribute is ignored when the type
        /// attribute's value is hidden, checkbox, radio, file, or a button type.
        /// </summary>
        [DOM("list")]
        public String List
        {
            get { return GetAttribute("list"); }
            set { SetAttribute("list", value); }
        }

        /// <summary>
        /// Gets or sets max HTML attribute, containing the maximum (numeric
        /// or date-time) value for this item, which must not be less than its
        /// minimum (min attribute) value.
        /// </summary>
        [DOM("max")]
        public String Max
        {
            get { return GetAttribute("max"); }
            set { SetAttribute("max", value); }
        }

        /// <summary>
        /// Gets or sets the min HTML attribute, containing the minimum (numeric
        /// or date-time) value for this item, which must not be greater than its
        /// maximum (max attribute) value.
        /// </summary>
        [DOM("min")]
        public String Min
        {
            get { return GetAttribute("min"); }
            set { SetAttribute("min", value); }
        }

        /// <summary>
        /// Gets or sets the pattern HTML attribute, containing a regular expression
        /// that the control's value is checked against. The pattern must match the
        /// entire value, not just some subset. This attribute applies when the value
        /// of the type attribute is text, search, tel, url or email; otherwise it is ignored.
        /// </summary>
        [DOM("pattern")]
        public String Pattern
        {
            get { return GetAttribute("pattern"); }
            set { SetAttribute("pattern", value); }
        }

        /// <summary>
        /// Gets or sets the size HTML attribute, containing size of the control. This value
        /// is in pixels unless the value of type is text or password, in which case, it is
        /// an integer number of characters. Applies only when type is set to text, search, tel,
        /// url, email, or password; otherwise it is ignored.
        /// </summary>
        [DOM("size")]
        public UInt32 Size
        {
            get { return ToInteger(GetAttribute("size"), 20u); }
            set { SetAttribute("size", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the src HTML attribute, which specifies a URI for the location of an
        /// image to display on the graphical submit button, if the value of type is image;
        /// otherwise it is ignored.
        /// </summary>
        [DOM("src")]
        public String Src
        {
            get { return GetAttribute("src"); }
            set { SetAttribute("src", value); }
        }

        /// <summary>
        /// Gets or sets the step HTML attribute, which works with min and max to limit the
        /// increments at which a numeric or date-time value can be set. It can be the string
        /// any or a positive floating point number. If this is not set to any, the control
        /// accepts only values at multiples of the step value greater than the minimum.
        /// </summary>
        [DOM("step")]
        public String Step
        {
            get { return GetAttribute("step"); }
            set { SetAttribute("step", value); }
        }

        /// <summary>
        /// Gets or sets a client-side image map.
        /// </summary>
        [DOM("useMap")]
        public String UseMap
        {
            get { return GetAttribute("useMap"); }
            set { SetAttribute("useMap", value); }
        }

        /// <summary>
        /// Gets or sets the width HTML attribute, which defines the width of the image
        /// displayed for the button, if the value of type is image.
        /// </summary>
        [DOM("width")]
        public UInt32 Width
        {
            get { return ToInteger(GetAttribute("width"), _imageWidth); }
            set { SetAttribute("width", value.ToString()); }
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
        [DOM("stepUp")]
        public void StepUp(Int32 n = 1)
        {
            DoStep(n);
        }

        /// <summary>
        /// Decrements the value by (step * n), where n defaults to 1 if not specified. 
        /// </summary>
        /// <param name="n">Optional: The number of steps to take.</param>
        [DOM("stepDown")]
        public void StepDown(Int32 n = 1)
        {
            DoStep(-n);
        }

        #endregion

        #region Internal Properties

        internal Boolean IsMutable 
        {
            get { return !Disabled && !Readonly; }
        }

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return true; }
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
        /// Constucts the data set (called from a form).
        /// </summary>
        /// <param name="dataSet">The dataset to construct.</param>
        /// <param name="submitter">The given submitter.</param>
        internal override void ConstructDataSet(FormDataSet dataSet, HTMLElement submitter)
        {
            switch (Type)
            {
                case InputType.Radio:
                case InputType.Checkbox:
                {
                    if (Checked)
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
                    if(_files.Length == 0)
                        dataSet.Append(Name, String.Empty, "application/octet-stream");

                    foreach (var file in _files)
                        dataSet.Append(Name, file, Type.ToString());

                    break;
                }
                case InputType.Text:
                case InputType.Search:
                {
                    dataSet.Append(Name, Value, Type.ToString());

                    if (_attributes[AttributeNames.DirName] != null)
                    {
                        var dirname = _attributes[AttributeNames.DirName].Value;

                        if (String.IsNullOrEmpty(dirname))
                            break;

                        dataSet.Append(dirname, Dir.ToString().ToLower(), "Direction");
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
            var step = Step;

            if (step != null)
            {
                switch (Type)
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

            throw new DOMException(ErrorCode.InvalidStateError);
        }

        /// <summary>
        /// Checks if the numeric value is between min and max.
        /// </summary>
        /// <param name="value">The value to check for the range-constraint.</param>
        /// <returns>True if the value is between min, max otherwise false.</returns>
        Boolean IsBetween(Double value)
        {
            var t = 0.0;

            if (Min != null && Double.TryParse(Min, NumberStyles.Any, CultureInfo.InvariantCulture, out t) && t > value)
                return false;
            else if (Max != null && Double.TryParse(Max, NumberStyles.Any, CultureInfo.InvariantCulture, out t) && t < value)
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

            if (Min != null && DateTime.TryParse(Min, CultureInfo.InvariantCulture, DateTimeStyles.None, out t) && t > value)
                return false;
            else if (Max != null && DateTime.TryParse(Max, CultureInfo.InvariantCulture, DateTimeStyles.None, out t) && t < value)
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
        protected override void Check(ValidityState state)
        {
            //TODO
        }

        #endregion
    }
}
