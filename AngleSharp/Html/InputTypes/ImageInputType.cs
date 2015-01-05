namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.DOM.Html;
    using System;

    class ImageInputType : BaseInputType
    {
        #region ctor

        public ImageInputType(IHtmlInputElement input, String name)
            : base(input, name, validate: true)
        {
        }

        #endregion

        #region Methods

        public override void ConstructDataSet(FormDataSet dataSet)
        {
            if (!String.IsNullOrEmpty(Input.Name))
            {
                var name = String.Empty;

                if (!String.IsNullOrEmpty(Input.Value))
                    name = Input.Value + ".";

                var namex = name + "x";
                var namey = name + "y";

                //TODO get x and y of submitter and save those
                dataSet.Append(namex, "0", Input.Type);
                dataSet.Append(namey, "0", Input.Type);
            }
        }

        #endregion
    }
}
