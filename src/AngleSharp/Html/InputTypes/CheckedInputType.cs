namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.Common;
    using AngleSharp.Html.Dom;
    using System;

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

            if (Input.IsRequired && !Input.IsChecked)
            {
                result ^= ValidationErrors.ValueMissing;
            }

            return result;
        }

        public override void ConstructDataSet(FormDataSet dataSet)
        {
            if (Input.IsChecked)
            {
                var value = Input.HasValue ? Input.Value : Keywords.On;
                dataSet.Append(Input.Name, value, Input.Type);
            }
        }

        #endregion
    }
}
