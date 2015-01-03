namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.DOM.Html;
    using AngleSharp.DOM.Io;
    using AngleSharp.Network;
    using System;

    class FileInputType : BaseInputType
    {
        #region ctor

        public FileInputType()
            : base(validate: true)
        {
        }

        #endregion

        #region Methods

        public override void ConstructDataSet(IHtmlInputElement input, FormDataSet dataSet)
        {
            var files = input.Files;

            if (files.Length == 0)
                dataSet.Append(input.Name, String.Empty, MimeTypes.Binary);

            for (var i = 0; i < files.Length; i++)
                dataSet.Append(input.Name, files[i] as FileEntry, input.Type);
        }

        #endregion
    }
}
