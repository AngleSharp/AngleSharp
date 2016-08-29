namespace AngleSharp.Html.Submitters
{
    using AngleSharp.Dom.Io;
    using AngleSharp.Extensions;
    using AngleSharp.Html.Submitters.Json;
    using System;
    using System.IO;

    sealed class JsonFormDataSetVisitor : IFormSubmitter
    {
        #region Fields

        readonly JsonObject _context;

        #endregion

        #region ctor

        public JsonFormDataSetVisitor()
        {
            _context = new JsonObject();
        }

        #endregion

        #region Methods

        public void Text(FormDataSetEntry entry, String value)
        {
            var item = CreateValue(entry.Type, value);
            var steps = JsonStep.Parse(entry.Name);
            var context = (JsonElement)_context;

            foreach (var step in steps)
            {
                context = step.Run(context, item, file: false);
            }
        }

        public void File(FormDataSetEntry entry, String fileName, String contentType, IFile file)
        {
            var context = (JsonElement)_context;
            var stream = file != null && file.Body != null && file.Type != null ? file.Body : Stream.Null;
            var content = new MemoryStream();
            stream.CopyTo(content);
            var data = content.ToArray();
            var steps = JsonStep.Parse(entry.Name);
            var value = new JsonObject
                            {
                                [AttributeNames.Type] = new JsonValue(contentType),
                                [AttributeNames.Name] = new JsonValue(fileName),
                                [AttributeNames.Body] = new JsonValue(Convert.ToBase64String(data))
                            };


            foreach (var step in steps)
            {
                context = step.Run(context, value, file: true);
            }
        }

        public void Serialize(StreamWriter stream)
        {
            var content = _context.ToString();
            stream.Write(content);
        }

        #endregion

        #region Helpers

        static JsonValue CreateValue(String type, String value)
        {
            if (type.Is(InputTypeNames.Checkbox))
                return new JsonValue(value.Is(Keywords.On));
            else if (type.Is(InputTypeNames.Number))
                return new JsonValue(value.ToDouble());

            return new JsonValue(value);
        }

        #endregion
    }
}
