namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.DOM.Html;
    using AngleSharp.Extensions;
    using AngleSharp.Services.Media;
    using System;
    using System.Threading.Tasks;

    class ImageInputType : BaseInputType
    {
        #region Fields

        readonly Task<IImageInfo> _imageTask;

        #endregion

        #region ctor

        public ImageInputType(IHtmlInputElement input, String name)
            : base(input, name, validate: true)
        {
            var inp = input as HTMLInputElement;
            var src = input.Source;

            if (src != null && inp != null)
            {
                var url = inp.HyperRef(src);
                _imageTask = inp.Owner.Options.LoadResource<IImageInfo>(url);
                _imageTask.ContinueWith(task => inp.FireSimpleEvent(EventNames.Load));
            }
        }

        #endregion

        #region Properties

        public Int32 Width
        {
            get { return _imageTask.IsCompleted && _imageTask.Result != null ? _imageTask.Result.Width : 0; }
        }

        public Int32 Height
        {
            get { return _imageTask.IsCompleted && _imageTask.Result != null ? _imageTask.Result.Height : 0; }
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
