namespace AngleSharp.Core.Tests.Urls
{
    using Newtonsoft.Json;
    using System;

    public class WptUrlTestEntry
    {
        [JsonProperty("input")]
        public String Input { get; set; }

        [JsonProperty("base")]
        public String Base { get; set; }

        [JsonProperty("failure")]
        public Boolean Failure { get; set; }

        [JsonProperty("href")]
        public String Href { get; set; }

        [JsonProperty("origin")]
        public String Origin { get; set; }

        /// <summary>
        /// Whether the entry carries an "origin" key at all. Only about half of the WPT
        /// entries do, and the ones that do use the literal string "null" for an opaque
        /// origin, so an absent key cannot be distinguished from a null value otherwise.
        /// </summary>
        [JsonIgnore]
        public Boolean HasOrigin { get; set; }

        [JsonProperty("protocol")]
        public String Protocol { get; set; }

        [JsonProperty("username")]
        public String Username { get; set; }

        [JsonProperty("password")]
        public String Password { get; set; }

        [JsonProperty("host")]
        public String Host { get; set; }

        [JsonProperty("hostname")]
        public String Hostname { get; set; }

        [JsonProperty("port")]
        public String Port { get; set; }

        [JsonProperty("pathname")]
        public String Pathname { get; set; }

        [JsonProperty("search")]
        public String Search { get; set; }

        [JsonProperty("hash")]
        public String Hash { get; set; }

        public override String ToString() => Input ?? "(null)";
    }
}
