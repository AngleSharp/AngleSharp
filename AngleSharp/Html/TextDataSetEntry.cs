namespace AngleSharp.Html
{
    using System;
    using System.Text;

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
            if (_value == null)
                return false;

            return _value.Contains(boundary);
        }

        public override void Accept(IFormDataSetVisitor visitor)
        {
            visitor.Text(this, _value);
        }
    }
}
