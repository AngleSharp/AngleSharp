namespace AngleSharp.Html.InputTypes
{
    using System;
    using System.Threading.Tasks;
    using AngleSharp.Dom.Html;
    using AngleSharp.Extensions;
    using AngleSharp.Services.Media;

    class ImageInputType : BaseInputType
    {
        #region Fields

        readonly Task<IImageInfo> _current;

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
                _current = inp.Owner.LoadResource<IImageInfo>(request);
                _current.ContinueWith(m => inp.FireSimpleEvent(EventNames.Load));
            }
        }

        #endregion

        #region Properties

        public Int32 Width
        {
            get { return _current.IsCompleted && _current.Result != null ? _current.Result.Width : 0; }
        }

        public Int32 Height
        {
            get { return _current.IsCompleted && _current.Result != null ? _current.Result.Height : 0; }
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
