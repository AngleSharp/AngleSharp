namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.Dom.Html;
    using AngleSharp.Extensions;
    using AngleSharp.Services.Media;
    using System;

    class ImageInputType : BaseInputType
    {
        #region Fields

        IImageInfo _img;

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
                var request = inp.CreateRequestFor(url);
                inp.LoadResource<IImageInfo>(request).ContinueWith(m =>
                {
                    _img = m.Result;
                    inp.FireLoadOrErrorEvent(m);
                });
            }
        }

        #endregion

        #region Properties

        public Int32 Width
        {
            get { return _img != null ? _img.Width : 0; }
        }

        public Int32 Height
        {
            get { return _img != null ? _img.Height : 0; }
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

                dataSet.Append(name + "x", "0", Input.Type);
                dataSet.Append(name + "y", "0", Input.Type);
            }
        }

        #endregion
    }
}
