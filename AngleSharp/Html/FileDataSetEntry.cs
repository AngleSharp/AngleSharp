namespace AngleSharp.Html
{
    using AngleSharp.Dom.Io;
    using AngleSharp.Network;
    using System;
    using System.Text;

    /// <summary>
    /// A file entry in a form.
    /// </summary>
    sealed class FileDataSetEntry : FormDataSetEntry
    {
        readonly IFile _value;

        public FileDataSetEntry(String name, IFile value, String type)
            : base(name, type)
        {
            _value = value;
        }

        public String FileName
        {
            get { return _value != null ? _value.Name : String.Empty; }
        }

        public String ContentType
        {
            get { return _value != null ? _value.Type : MimeTypeNames.Binary; }
        }

        public override Boolean Contains(String boundary, Encoding encoding)
        {
            if (_value == null || _value.Body == null)
                return false;

            //TODO boundary check required?
            return false;
        }

        public override void Accept(IFormDataSetVisitor visitor)
        {
            visitor.File(this, FileName, ContentType, _value);
        }
    }
}
