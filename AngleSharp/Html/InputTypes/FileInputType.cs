namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.DOM.Html;
    using AngleSharp.DOM.Io;
    using AngleSharp.Network;
    using System;

    class FileInputType : BaseInputType
    {
        #region ctor

        public FileInputType(IHtmlInputElement input, String name)
            : base(input, name, validate: true)
        {
        }

        #endregion

        #region Methods

        public override void ConstructDataSet(FormDataSet dataSet)
        {
            var files = Input.Files;

            if (files.Length == 0)
                dataSet.Append(Input.Name, String.Empty, MimeTypes.Binary);

            for (var i = 0; i < files.Length; i++)
                dataSet.Append(Input.Name, files[i] as FileEntry, Input.Type);
        }

        #endregion
    }
}
