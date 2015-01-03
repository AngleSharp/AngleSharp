namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.DOM.Html;
    using System;

    class ImageInputType : BaseInputType
    {
        #region ctor

        public ImageInputType()
            : base(validate: true)
        {
        }

        #endregion

        #region Methods

        public override void ConstructDataSet(IHtmlInputElement input, FormDataSet dataSet)
        {
            if (!String.IsNullOrEmpty(input.Name))
            {
                var name = String.Empty;

                if (!String.IsNullOrEmpty(input.Value))
                    name = input.Value + ".";

                var namex = name + "x";
                var namey = name + "y";

                //TODO get x and y of submitter and save those
                dataSet.Append(namex, "0", input.Type);
                dataSet.Append(namey, "0", input.Type);
            }
        }

        #endregion
    }
}
