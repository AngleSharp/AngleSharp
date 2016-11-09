namespace AngleSharp.Html.Forms.Submitters.Json
{
    using AngleSharp.Text;
    using System;
    using System.Globalization;

    sealed class JsonValue : JsonElement
    {
        private readonly String _value;

        public JsonValue(String value)
        {
            _value = value.CssString();
        }

        public JsonValue(Double value)
        {
            _value = value.ToString(CultureInfo.InvariantCulture);
        }

        public JsonValue(Boolean value)
        {
            _value = value ? "true" : "false";
        }

        public override String ToString()
        {
            return _value;
        }
    }
}
