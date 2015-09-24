namespace AngleSharp.Html.Submitters
{
    using AngleSharp.Dom.Io;
    using AngleSharp.Html.Json;
    using System;
    using System.IO;

    sealed class JsonFormDataSetVisitor : IFormSubmitter
    {
        readonly JsonObject _context;

        public JsonFormDataSetVisitor()
        {
            _context = new JsonObject();
        }

        public void Text(FormDataSetEntry entry, String value)
        {
            var item = new JsonValue(entry.Type, value);
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
            var value = new JsonObject();

            value["type"] = new JsonValue(InputTypeNames.Text, contentType);
            value["name"] = new JsonValue(InputTypeNames.Text, fileName);
            value["body"] = new JsonValue(InputTypeNames.Text, Convert.ToBase64String(data));

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
    }
}
