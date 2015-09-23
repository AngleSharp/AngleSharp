namespace AngleSharp.Html
{
    using AngleSharp.Dom.Io;
    using AngleSharp.Extensions;
    using AngleSharp.Html.Json;
    using AngleSharp.Network;
    using System;
    using System.IO;
    using System.Text;

    sealed class FileDataSetEntry : FormDataSetEntry
    {
        readonly IFile _value;

        public FileDataSetEntry(String name, IFile value, String type)
            : base(name, type)
        {
            _value = value;
        }

        /// <summary>
        /// Gets if the value has been given.
        /// </summary>
        public Boolean HasValue
        {
            get { return _value != null && _value.Name != null; }
        }

        /// <summary>
        /// Gets if the value has a body and type.
        /// </summary>
        public Boolean HasValueBody
        {
            get { return _value != null && _value.Body != null && _value.Type != null; }
        }

        /// <summary>
        /// Gets the entry's value.
        /// </summary>
        public IFile Value
        {
            get { return _value; }
        }

        public String FileName
        {
            get { return _value != null ? _value.Name : String.Empty; }
        }

        public String ContentType
        {
            get { return _value != null ? _value.Type : MimeTypes.Binary; }
        }

        public override Boolean Contains(String boundary, Encoding encoding)
        {
            if (_value == null || _value.Body == null)
                return false;

            //TODO boundary check required?
            return false;
        }

        public override Action<StreamWriter> AsMultipart(Encoding encoding)
        {
            if (HasName)
            {
                return stream =>
                {
                    var hasContent = HasValue && HasValueBody;

                    stream.WriteLine("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"",
                        Name.HtmlEncode(encoding), FileName.HtmlEncode(encoding));

                    stream.WriteLine("Content-Type: " + ContentType);
                    stream.WriteLine();

                    if (hasContent)
                    {
                        stream.Flush();
                        _value.Body.CopyTo(stream.BaseStream);
                    }

                    stream.WriteLine();
                };
            }

            return null;
        }

        public override Tuple<String, String> AsPlaintext()
        {
            if (HasName && HasValue)
                return Tuple.Create(Name, _value.Name);

            return null;
        }

        public override Tuple<String, String> AsUrlEncoded(Encoding encoding)
        {
            if (HasName && HasValue)
            {
                var name = encoding.GetBytes(Name);
                var value = encoding.GetBytes(_value.Name);
                return Tuple.Create(name.UrlEncode(), value.UrlEncode());
            }

            return null;
        }

        public override void AsJson(JsonElement context)
        {
            var stream = HasValueBody ? _value.Body : Stream.Null;
            var content = new MemoryStream();
            stream.CopyTo(content);
            var data = content.ToArray();

            var value = new JsonObject();
            value["type"] = new JsonValue(InputTypeNames.Text, ContentType);
            value["name"] = new JsonValue(InputTypeNames.Text, FileName);
            value["body"] = new JsonValue(InputTypeNames.Text, Convert.ToBase64String(data));

            var steps = JsonStep.Parse(Name);

            foreach (var step in steps)
            {
                context = step.Run(context, value, file: true);
            }
        }
    }
}
