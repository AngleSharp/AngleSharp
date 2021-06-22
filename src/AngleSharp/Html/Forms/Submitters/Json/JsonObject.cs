namespace AngleSharp.Html.Forms.Submitters.Json
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    sealed class JsonObject : JsonElement
    {
        private readonly Dictionary<String, JsonElement> _properties = new ();

        public override JsonElement this[String key]
        {
            get
            {
                _properties.TryGetValue(key.ToString(), out var tmp);
                return tmp;
            }
            set => _properties[key] = value;
        }

        public override String ToString()
        {
            var sb = new ValueStringBuilder(_properties.Count * 20);

            var needsComma = false;

            sb.Append('{');

            foreach (var property in _properties)
            {
                if (needsComma)
                {
                    sb.Append(',');
                }

                sb.Append('"');
                sb.Append(property.Key);
                sb.Append('"');

                sb.Append(':');
                sb.Append(property.Value.ToString());
                needsComma = true;
            }

            sb.Append('}');

            return sb.ToString();
        }
    }
}
