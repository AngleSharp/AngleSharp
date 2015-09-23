namespace AngleSharp.Html
{
    using AngleSharp.Extensions;
    using AngleSharp.Html.Json;
    using System;
    using System.IO;
    using System.Text;

    sealed class TextDataSetEntry : FormDataSetEntry
    {
        readonly String _value;

        public TextDataSetEntry(String name, String value, String type)
            : base(name, type)
        {
            _value = value;
        }

        /// <summary>
        /// Gets if the value has been given.
        /// </summary>
        public Boolean HasValue
        {
            get { return _value != null; }
        }

        /// <summary>
        /// Gets the entry's value.
        /// </summary>
        public String Value
        {
            get { return _value; }
        }

        public override Boolean Contains(String boundary, Encoding encoding)
        {
            if (_value == null)
                return false;

            return _value.Contains(boundary);
        }

        public override Action<StreamWriter> AsMultipart(Encoding encoding)
        {
            if (HasName && HasValue)
            {
                return stream =>
                {
                    stream.WriteLine(String.Concat("Content-Disposition: form-data; name=\"", Name.HtmlEncode(encoding), "\""));
                    stream.WriteLine();
                    stream.WriteLine(_value.HtmlEncode(encoding));
                };
            }

            return null;
        }

        public override Tuple<String, String> AsPlaintext()
        {
            if (HasName && HasValue)
                return Tuple.Create(Name, _value);

            return null;
        }

        public override Tuple<String, String> AsUrlEncoded(Encoding encoding)
        {
            if (HasName && HasValue)
            {
                var name = encoding.GetBytes(Name);
                var value = encoding.GetBytes(_value);
                return Tuple.Create(name.UrlEncode(), value.UrlEncode());
            }

            return null;
        }

        public override void AsJson(JsonElement context)
        {
            var value = new JsonValue(Type, Value);
            var steps = JsonStep.Parse(Name);

            foreach (var step in steps)
            {
                context = step.Run(context, value, file: false);
            }
        }
    }
}
