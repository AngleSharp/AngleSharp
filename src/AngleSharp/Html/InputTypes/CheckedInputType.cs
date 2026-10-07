namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.Common;
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using System;
    using System.Collections.Generic;

    class CheckedInputType : BaseInputType
    {
        #region ctor

        public CheckedInputType(IHtmlInputElement input, String name)
            : base(input, name, validate: true)
        {
        }

        #endregion

        #region Methods

        public override ValidationErrors Check(IValidityState current)
        {
            var result = GetErrorsFrom(current);
            result &= ~ValidationErrors.ValueMissing;

            var isRequired = Input.IsRequired;

            if (!Input.IsChecked)
            {
                var name = Input.Name;

                if (Name == InputTypeNames.Radio && !String.IsNullOrEmpty(name))
                {
                    var form = Input.Form;
                    var root = Input.GetRoot();
                    IEnumerable<INode> controls = form is not null && root is IDocument ?
                        form.Elements : root.GetDescendantsAndSelf();

                    foreach (var node in controls)
                    {
                        if (node is IHtmlInputElement other && other.Type == InputTypeNames.Radio &&
                            String.Equals(name, other.Name, StringComparison.Ordinal) && ReferenceEquals(form, other.Form))
                        {
                            if (other.IsChecked)
                            {
                                return result;
                            }

                            isRequired |= other.IsRequired;
                        }
                    }
                }

                if (isRequired)
                {
                    result |= ValidationErrors.ValueMissing;
                }
            }

            return result;
        }

        public override void ConstructDataSet(FormDataSet dataSet)
        {
            if (Input.IsChecked)
            {
                var value = Input.HasValue ? Input.Value : Keywords.On;
                dataSet.Append(Input.Name!, value, Input.Type);
            }
        }

        #endregion
    }
}
