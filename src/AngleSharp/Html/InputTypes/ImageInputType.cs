namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using AngleSharp.Io.Processors;
    using System;

    class ImageInputType : BaseInputType
    {
        #region Fields

        private readonly ImageRequestProcessor _request;

        #endregion

        #region ctor

        public ImageInputType(IHtmlInputElement input, String name)
            : base(input, name, validate: true)
        {
            var inp = input as HtmlInputElement;
            var src = input.Source;


            if (src != null && inp != null)
            {
                var url = inp.HyperReference(src);
                _request = new ImageRequestProcessor(inp.Context);
                inp.Process(_request, url);
            }
        }

        #endregion

        #region Properties

        public Int32 Width => _request?.Width ?? 0;

        public Int32 Height =>  _request?.Height ?? 0;

        #endregion

        #region Methods

        public override Boolean IsAppendingData(IHtmlElement submitter)
        {
            return Object.ReferenceEquals(submitter, Input) && !String.IsNullOrEmpty(Input.Name);
        }

        public override void ConstructDataSet(FormDataSet dataSet)
        {
            var name = Input.Name;
            var value = Input.Value;

            dataSet.Append(name + ".x", "0", Input.Type);
            dataSet.Append(name + ".y", "0", Input.Type);

            if (!String.IsNullOrEmpty(value))
            {
                dataSet.Append(name, value, Input.Type);
            }
        }

        #endregion
    }
}
