namespace AngleSharp.Html.Json
{
    using AngleSharp.Extensions;
    using System;

    sealed class JsonValue : JsonElement
    {
        public JsonValue(String type, String value)
        {
            Type = type;
            Value = value;
        }

        public String Type
        {
            get;
            private set;
        }

        public String Value
        {
            get;
            private set;
        }

        public override String ToString()
        {
            if (Type.Is(InputTypeNames.Checkbox))
                return Value.Is(Keywords.On) ? "true" : "false";
            else if (Type.Is(InputTypeNames.Number))
                return Value;

            return Value.CssString();
        }
    }
}
