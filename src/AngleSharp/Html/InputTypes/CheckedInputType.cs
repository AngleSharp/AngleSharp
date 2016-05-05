namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.Dom.Html;
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

        public override void Check(ValidityState state)
        {
            state.IsValueMissing = Input.IsRequired && !Input.IsChecked;
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
