namespace AngleSharp.Core.Tests.Urls
{
    using AngleSharp.Dom;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;

    [TestFixture]
    public class WptUrlTests
    {
        private const string WptTestDataUrl =
            "https://raw.githubusercontent.com/web-platform-tests/wpt/refs/heads/master/url/resources/urltestdata.json";

        private static readonly Lazy<List<WptUrlTestEntry>> _testData = new(LoadTestData);

        private static List<WptUrlTestEntry> LoadTestData()
        {
            using var client = new HttpClient();
            var json = client.GetStringAsync(WptTestDataUrl).GetAwaiter().GetResult();
            var array = JArray.Parse(json);
            var entries = new List<WptUrlTestEntry>();

            foreach (var token in array)
            {
                if (token.Type == JTokenType.String)
                    continue;

                var entry = token.ToObject<WptUrlTestEntry>();
                entries.Add(entry);
            }

            return entries;
        }

        public static IEnumerable<TestCaseData> TestCases()
        {
            foreach (var entry in _testData.Value)
            {
                var name = entry.Input?.Length > 60
                    ? entry.Input.Substring(0, 60) + "..."
                    : entry.Input;
                yield return new TestCaseData(entry).SetName($"{{m}}(\"{name}\")");
            }
        }

        [TestCaseSource(nameof(TestCases))]
        public void WptUrlParsing(WptUrlTestEntry entry)
        {
            Url result;

            if (entry.Base != null)
            {
                var baseUrl = new Url(entry.Base);
                result = new Url(baseUrl, entry.Input);
            }
            else
            {
                result = new Url(entry.Input);
            }

            if (entry.Failure)
            {
                Warn.If(!result.IsInvalid,
                    $"Expected failure for input: {entry.Input}");
            }
            else
            {
                Warn.If(result.IsInvalid,
                    $"Expected success for input: {entry.Input}");

                if (!result.IsInvalid)
                {
                    Warn.Unless(result.Href == entry.Href,
                        $"Href mismatch: expected \"{entry.Href}\", got \"{result.Href}\"");
                    Warn.Unless(result.Protocol == entry.Protocol,
                        $"Protocol mismatch: expected \"{entry.Protocol}\", got \"{result.Protocol}\"");
                    Warn.Unless(result.UserName == entry.Username,
                        $"Username mismatch: expected \"{entry.Username}\", got \"{result.UserName}\"");
                    Warn.Unless(result.Password == entry.Password,
                        $"Password mismatch: expected \"{entry.Password}\", got \"{result.Password}\"");
                    Warn.Unless(result.Host == entry.Host,
                        $"Host mismatch: expected \"{entry.Host}\", got \"{result.Host}\"");
                    Warn.Unless(result.HostName == entry.Hostname,
                        $"Hostname mismatch: expected \"{entry.Hostname}\", got \"{result.HostName}\"");
                    Warn.Unless(result.Port == entry.Port,
                        $"Port mismatch: expected \"{entry.Port}\", got \"{result.Port}\"");
                    Warn.Unless(result.PathName == entry.Pathname,
                        $"Pathname mismatch: expected \"{entry.Pathname}\", got \"{result.PathName}\"");
                    Warn.Unless(result.Search == entry.Search,
                        $"Search mismatch: expected \"{entry.Search}\", got \"{result.Search}\"");
                    Warn.Unless(result.Hash == entry.Hash,
                        $"Hash mismatch: expected \"{entry.Hash}\", got \"{result.Hash}\"");
                }
            }
        }

        public class WptUrlTestEntry
        {
            [JsonProperty("input")]
            public string Input { get; set; }

            [JsonProperty("base")]
            public string Base { get; set; }

            [JsonProperty("failure")]
            public bool Failure { get; set; }

            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("origin")]
            public string Origin { get; set; }

            [JsonProperty("protocol")]
            public string Protocol { get; set; }

            [JsonProperty("username")]
            public string Username { get; set; }

            [JsonProperty("password")]
            public string Password { get; set; }

            [JsonProperty("host")]
            public string Host { get; set; }

            [JsonProperty("hostname")]
            public string Hostname { get; set; }

            [JsonProperty("port")]
            public string Port { get; set; }

            [JsonProperty("pathname")]
            public string Pathname { get; set; }

            [JsonProperty("search")]
            public string Search { get; set; }

            [JsonProperty("hash")]
            public string Hash { get; set; }

            public override string ToString() => Input ?? "(null)";
        }
    }
}
