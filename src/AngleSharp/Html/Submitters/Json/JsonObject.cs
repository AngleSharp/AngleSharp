namespace AngleSharp.Html.Submitters.Json
{
    using System;
    using System.Collections.Generic;

    sealed class JsonObject : JsonElement
    {
        readonly Dictionary<String, JsonElement> _properties;

        public JsonObject()
        {
            _properties = new Dictionary<String, JsonElement>();
        }

        public override JsonElement this[String key]
        {
            get 
            { 
                var tmp = default(JsonElement);
                _properties.TryGetValue(key.ToString(), out tmp); 
                return tmp; 
            }
            set 
            {
                _properties[key] = value; 
            }
        }

        public override String ToString()
        {
            var sb = Pool.NewStringBuilder().Append(Symbols.CurlyBracketOpen);
            var needsComma = false;

            foreach (var property in _properties)
            {
                if (needsComma)
                    sb.Append(Symbols.Comma);

                sb.Append(Symbols.DoubleQuote).Append(property.Key).Append(Symbols.DoubleQuote);
                sb.Append(Symbols.Colon).Append(property.Value.ToString());
                needsComma = true;
            }

            return sb.Append(Symbols.CurlyBracketClose).ToPool();
        }
    }
}
