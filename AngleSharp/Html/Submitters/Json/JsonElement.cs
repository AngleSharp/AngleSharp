namespace AngleSharp.Html.Submitters.Json
{
    using System;

    abstract class JsonElement
    {
        public virtual JsonElement this[String key]
        {
            get { throw new InvalidOperationException(); }
            set { throw new InvalidOperationException(); }
        }
    }
}
