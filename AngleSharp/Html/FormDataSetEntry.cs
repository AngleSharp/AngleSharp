namespace AngleSharp.Html
{
    using AngleSharp.Html.Json;
    using System;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Encapsulates the data contained in an entry.
    /// </summary>
    abstract class FormDataSetEntry
    {
        readonly String _name;
        readonly String _type;

        public FormDataSetEntry(String name, String type)
        {
            _name = name;
            _type = type;
        }

        /// <summary>
        /// Gets if the name has been given.
        /// </summary>
        public Boolean HasName
        {
            get { return _name != null; }
        }

        /// <summary>
        /// Gets the entry's name.
        /// </summary>
        public String Name
        {
            get { return _name ?? String.Empty; }
        }

        /// <summary>
        /// Gets the entry's type.
        /// </summary>
        public String Type
        {
            get { return _type ?? InputTypeNames.Text; }
        }

        public abstract Action<StreamWriter> AsMultipart(Encoding encoding);

        public abstract Tuple<String, String> AsPlaintext();

        public abstract Tuple<String, String> AsUrlEncoded(Encoding encoding);

        public abstract void AsJson(JsonElement context);

        public abstract Boolean Contains(String boundary, Encoding encoding);
    }
}
