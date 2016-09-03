namespace AngleSharp.Html
{
    using System;
    using System.Text;

    /// <summary>
    /// A text entry in a form.
    /// </summary>
    sealed class TextDataSetEntry : FormDataSetEntry
    {
        readonly String _value;

        public TextDataSetEntry(String name, String value, String type)
            : base(name, type)
        {
            _value = value;
        }

        public override Boolean Contains(String boundary, Encoding encoding)
        {
            return _value != null && _value.Contains(boundary);
        }

        public override void Accept(IFormDataSetVisitor visitor)
        {
            visitor.Text(this, _value);
        }
    }
}
