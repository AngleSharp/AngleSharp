namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.Dom.Html;
    using AngleSharp.Extensions;
    using AngleSharp.Services.Media;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    class ImageInputType : BaseInputType, IDisposable
    {
        #region Fields

        CancellationTokenSource _cts;
        Task<IImageInfo> _imageTask;

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
                _cts = new CancellationTokenSource();
                _imageTask = LoadAsync(inp, url, _cts.Token);
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

        public void Dispose()
        {
            if (_cts != null)
                _cts.Cancel();

            _cts = null;
            _imageTask = null;
        }

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

        #region Helper

        async Task<IImageInfo> LoadAsync(HtmlInputElement inp, Url url, CancellationToken cancel)
        {
            var image = await inp.Owner.Options.LoadResource<IImageInfo>(url, cancel).ConfigureAwait(false);
            inp.FireSimpleEvent(EventNames.Load);
            return image;
        }

        #endregion
    }
}
