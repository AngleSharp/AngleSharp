namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.DOM.Html;
    using System;

    class CheckedInputType : BaseInputType
    {
        #region ctor

        public CheckedInputType()
            : base(validate: true)
        {
        }

        #endregion

        #region Methods

        public override void ConstructDataSet(IHtmlInputElement input, FormDataSet dataSet)
        {
            if (input.IsChecked)
            {
                var value = Keywords.On;

                if (!String.IsNullOrEmpty(input.Value))
                    value = input.Value;

                dataSet.Append(input.Name, value, input.Type);
            }
        }

        #endregion
    }
}
