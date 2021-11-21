namespace AngleSharp.Html.Forms.Submitters.Json
{
    using AngleSharp.Text;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    sealed class JsonArray : JsonElement, IEnumerable<JsonElement?>
    {
        private readonly List<JsonElement?> _elements;

        public Int32 Length => _elements.Count;

        public JsonArray()
        {
            _elements = new List<JsonElement?>(); 
        }

        public JsonArray(int capacity)
        {
            _elements = new List<JsonElement?>(capacity);
        }

        public void Push(JsonElement element)
        {
            _elements.Add(element);
        }

        public void Add(JsonElement element)
        {
            _elements.Add(element);
        }

        public JsonElement? this[Int32 key]
        {
            get => _elements.ElementAtOrDefault(key);
            set
            {
                for (var i = _elements.Count; i <= key; i++)
                {
                    _elements.Add(null!);
                }

                _elements[key] = value;
            }
        }

        public override String ToString()
        {
            var sb = StringBuilderPool.Obtain().Append(Symbols.SquareBracketOpen);
            var needsComma = false;

            foreach (var element in _elements)
            {
                if (needsComma)
                    sb.Append(Symbols.Comma);

                sb.Append(element?.ToString() ?? "null");
                needsComma = true;
            }

            return sb.Append(Symbols.SquareBracketClose).ToPool();
        }

        public IEnumerator<JsonElement> GetEnumerator()
        {
            return _elements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
