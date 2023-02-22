namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.Html.Dom;
    using AngleSharp.Io.Dom;
    using System;

    class FileInputType : BaseInputType
    {
        #region Fields

        #endregion

        #region ctor

        public FileInputType(IHtmlInputElement input, String name)
            : base(input, name, validate: true)
        {
            Files = new FileList();
        }

        #endregion

        #region Properties

        public FileList Files { get; }

        #endregion

        #region Methods

        public override void ConstructDataSet(FormDataSet dataSet)
        {
            if (Files.Length == 0)
            {
                dataSet.Append(Input.Name!, default(IFile)!, Input.Type);
            }

            foreach (var file in Files)
            {
                dataSet.Append(Input.Name!, file, Input.Type);
            }
        }

        #endregion
    }
}
