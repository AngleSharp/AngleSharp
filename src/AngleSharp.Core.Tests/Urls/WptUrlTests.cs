namespace AngleSharp.Core.Tests.Urls
{
    using Dom;
    using Newtonsoft.Json.Linq;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;

    [TestFixture]
    public class WptUrlTests
    {
        private const String WptTestDataUrl =
            "https://raw.githubusercontent.com/web-platform-tests/wpt/refs/heads/master/url/resources/urltestdata.json";

        private static readonly Lazy<List<WptUrlTestEntry>> TestData = new(LoadTestData);

        private static List<WptUrlTestEntry> LoadTestData()
        {
            using var client = new HttpClient();
            var json = client.GetStringAsync(WptTestDataUrl).GetAwaiter().GetResult();
            var array = JArray.Parse(json);
            var entries = new List<WptUrlTestEntry>();

            foreach (var token in array)
            {
                if (token.Type == JTokenType.String)
                {
                    continue;
                }

                var entry = token.ToObject<WptUrlTestEntry>();
                entries.Add(entry);
            }

            return entries;
        }

        public static IEnumerable<TestCaseData> TestCases()
        {
            foreach (var entry in TestData.Value)
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
            var mismatches = 0;
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

            void Check(bool condition, string message)
            {
                if (!condition)
                {
                    mismatches++;
                    TestContext.WriteLine(message);
                }
            }

            if (entry.Failure)
            {
                Check(result.IsInvalid, $"Expected failure for input: {entry.Input}");
            }
            else
            {
                if (result.IsInvalid)
                {
                    TestContext.WriteLine($"Expected success for input: {entry.Input}");
                    Assert.Warn($"1 mismatch for input: {entry.Input}");
                    return;
                }

                Check(result.Href == entry.Href, $"Href mismatch: expected \"{entry.Href}\", got \"{result.Href}\"");
                Check(result.Protocol == entry.Protocol, $"Protocol mismatch: expected \"{entry.Protocol}\", got \"{result.Protocol}\"");
                Check(result.UserName == entry.Username, $"Username mismatch: expected \"{entry.Username}\", got \"{result.UserName}\"");
                Check(result.Password == entry.Password, $"Password mismatch: expected \"{entry.Password}\", got \"{result.Password}\"");
                Check(result.Host == entry.Host, $"Host mismatch: expected \"{entry.Host}\", got \"{result.Host}\"");
                Check(result.HostName == entry.Hostname, $"Hostname mismatch: expected \"{entry.Hostname}\", got \"{result.HostName}\"");
                Check(result.Port == entry.Port, $"Port mismatch: expected \"{entry.Port}\", got \"{result.Port}\"");
                Check(result.PathName == entry.Pathname, $"Pathname mismatch: expected \"{entry.Pathname}\", got \"{result.PathName}\"");
                Check(result.Search == entry.Search, $"Search mismatch: expected \"{entry.Search}\", got \"{result.Search}\"");
                Check(result.Hash == entry.Hash, $"Hash mismatch: expected \"{entry.Hash}\", got \"{result.Hash}\"");
            }

            if (mismatches > 0)
                Assert.Warn($"{mismatches} mismatch(es) for input: {entry.Input}");
        }

    }
}
