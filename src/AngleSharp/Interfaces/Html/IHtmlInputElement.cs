namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using AngleSharp.Dom.Io;
    using System;

    /// <summary>
    /// Represents the input HTML element.
    /// </summary>
    [DomName("HTMLInputElement")]
    public interface IHtmlInputElement : IHtmlTextFormControlElement
    {
        
        /// <summary>
        /// Gets or sets the accept HTML attribute, containing comma-separated 
        /// list of file types accepted by the server when type is file.
        /// </summary>
        [DomName("accept")]
        String Accept { get; set; }
        
        /// <summary>
        /// Gets or sets the autocomplete HTML attribute, indicating whether
        /// the value of the control can be automatically completed by the
        /// browser. Ignored if the value of the type attribute is hidden,
        /// checkbox, radio, file, or a button type (button, submit, reset,
        /// image).
        /// </summary>
        [DomName("autocomplete")]
        String Autocomplete { get; set; }
        
        /// <summary>
        /// Gets a list of selected files.
        /// </summary>
        [DomName("files")]
        IFileList Files { get; }
        
        /// <summary>
        /// Gets or sets the type of input control.
        /// </summary>
        [DomName("type")]
        String Type { get; set; }
        
        /// <summary>
        /// Gets or sets the alternative text.
        /// </summary>
        [DomName("alt")]
        String AlternativeText { get; set; }

        /// <summary>
        /// Gets or sets the image source.
        /// </summary>
        [DomName("src")]
        String Source { get; set; }

        /// <summary>
        /// Gets or sets max HTML attribute, containing the maximum (numeric
        /// or date-time) value for this item, which must not be less than its
        /// minimum (min attribute) value.
        /// </summary>
        [DomName("max")]
        String Maximum { get; set; }

        /// <summary>
        /// Gets or sets the min HTML attribute, containing the minimum (numeric
        /// or date-time) value for this item, which must not be greater than
        /// its maximum (max attribute) value.
        /// </summary>
        [DomName("min")]
        String Minimum { get; set; }

        /// <summary>
        /// Gets or sets the pattern HTML attribute, containing a regular 
        /// expression that the control's value is checked against. The pattern
        /// must match the entire value, not just some subset. This attribute
        /// applies when the value of the type attribute is text, search, tel, 
        /// url or email; otherwise it is ignored.
        /// </summary>
        [DomName("pattern")]
        String Pattern { get; set; }
        
        /// <summary>
        /// Gets or sets the step HTML attribute, which works with min and max 
        /// to limit the increments at which a numeric or date-time value can be
        /// set. It can be the string any or a positive floating point number. 
        /// If this is not set to any, the control accepts only values at 
        /// multiples of the step value greater than the minimum.
        /// </summary>
        [DomName("step")]
        String Step { get; set; }

        /// <summary>
        /// Increments the value by (step * n), where n defaults to 1 if not
        /// specified.
        /// </summary>
        /// <param name="n">Optional: The number of steps to take.</param>
        [DomName("stepUp")]
        void StepUp(Int32 n = 1);

        /// <summary>
        /// Decrements the value by (step * n), where n defaults to 1 if not 
        /// specified. 
        /// </summary>
        /// <param name="n">Optional: The number of steps to take.</param>
        [DomName("stepDown")]
        void StepDown(Int32 n = 1);

        /// <summary>
        /// Gets the datalist element in the same document. Only options that
        /// are valid values for this input element will be displayed. This 
        /// attribute is ignored when the type attribute's value is hidden, 
        /// checkbox, radio, file, or a button type.
        /// </summary>
        [DomName("list")]
        IHtmlDataListElement List { get; }

        /// <summary>
        /// Gets or sets the URI of a resource that processes information 
        /// submitted by the button. If specified, this attribute overrides the
        /// action attribute of the form element that owns this element.
        /// </summary>
        [DomName("formAction")]
        String FormAction { get; set; }

        /// <summary>
        /// Gets or sets the type of content that is used to submit the form to
        /// the server. If specified, this attribute overrides the enctype
        /// attribute of the form element that owns this element.
        /// </summary>
        [DomName("formEncType")]
        String FormEncType { get; set; }

        /// <summary>
        /// Gets or sets the HTTP method that the browser uses to submit the
        /// form. If specified, this attribute overrides the method attribute
        /// of the form element that owns this element.
        /// </summary>
        [DomName("formMethod")]
        String FormMethod { get; set; }

        /// <summary>
        /// Gets or sets that the form is not to be validated when it is
        /// submitted. If specified, this attribute overrides the enctype 
        /// attribute of the form element that owns this element.
        /// </summary>
        [DomName("formNoValidate")]
        Boolean FormNoValidate { get; set; }

        /// <summary>
        /// Gets or sets A name or keyword indicating where to display the 
        /// response that is received after submitting the form. If specified,
        /// this attribute overrides the target attribute of the form element
        /// that owns this element.
        /// </summary>
        [DomName("formTarget")]
        String FormTarget { get; set; }
        
        /// <summary>
        /// Gets or sets the value of the element, interpreted as one of the
        /// following in order:
        /// 1.) Time value 
        /// 2.) Number 
        /// 3.) otherwise NaN.
        /// </summary>
        [DomName("valueAsNumber")]
        Double ValueAsNumber { get; set; }
        
        /// <summary>
        /// Gets or sets the value of the element, interpreted as a date, or
        /// null if conversion is not possible.
        /// </summary>
        [DomName("valueAsDate")]
        DateTime? ValueAsDate { get; set; }

        /// <summary>
        /// Gets or sets if the state if indeterminate.
        /// </summary>
        [DomName("indeterminate")]
        Boolean IsIndeterminate { get; set; }

        /// <summary>
        /// Gets or sets
        /// </summary>
        [DomName("defaultChecked")]
        Boolean IsDefaultChecked { get; set; }

        /// <summary>
        /// Gets or sets if the input element is checked or not.
        /// </summary>
        [DomName("checked")]
        Boolean IsChecked { get; set; }

        /// <summary>
        /// Gets or sets the display size of the element.
        /// </summary>
        [DomName("size")]
        Int32 Size { get; set; }

        /// <summary>
        /// Gets or sets the multiple HTML attribute, whichindicates whether
        /// multiple items can be selected.
        /// </summary>
        [DomName("multiple")]
        Boolean IsMultiple { get; set; }
        
        /// <summary>
        /// Gets or sets the display width of the input element.
        /// </summary>
        [DomName("width")]
        Int32 DisplayWidth { get; set; }

        /// <summary>
        /// Gets or sets the display height of the input element.
        /// </summary>
        [DomName("height")]
        Int32 DisplayHeight { get; set; }
        
    }
}
