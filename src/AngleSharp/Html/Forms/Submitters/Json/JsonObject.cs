namespace AngleSharp.Html.Forms.Submitters.Json
{
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    sealed class JsonObject : JsonElement
    {
        private readonly Dictionary<String, JsonElement> _properties = new Dictionary<String, JsonElement>();

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
            var sb = StringBuilderPool.Obtain().Append(Symbols.CurlyBracketOpen);
            var needsComma = false;

            foreach (var property in _properties)
            {
                if (needsComma)
                {
                    sb.Append(Symbols.Comma);
                }

                sb.Append(Symbols.DoubleQuote).Append(property.Key).Append(Symbols.DoubleQuote);
                sb.Append(Symbols.Colon).Append(property.Value.ToString());
                needsComma = true;
            }

            return sb.Append(Symbols.CurlyBracketClose).ToPool();
        }
    }
}
