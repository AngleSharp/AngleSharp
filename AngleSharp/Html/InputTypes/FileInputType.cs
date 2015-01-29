namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.Dom.Html;
    using AngleSharp.Dom.Io;
    using AngleSharp.Network;
    using System;

    class FileInputType : BaseInputType
    {
        #region Fields

        readonly FileList _files;

        #endregion

        #region ctor

        public FileInputType(IHtmlInputElement input, String name)
            : base(input, name, validate: true)
        {
            _files = new FileList();
        }

        #endregion

        #region Properties

        public FileList Files
        {
            get { return _files; }
        }

        #endregion

        #region Methods

        public override void ConstructDataSet(FormDataSet dataSet)
        {
            if (_files.Length == 0)
                dataSet.Append(Input.Name, String.Empty, MimeTypes.Binary);

            foreach (var file in _files)
                dataSet.Append(Input.Name, file, Input.Type);
        }

        #endregion
    }
}
