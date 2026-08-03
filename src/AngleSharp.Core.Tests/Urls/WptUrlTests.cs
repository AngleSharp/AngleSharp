namespace AngleSharp.Core.Tests.Urls
{
    using Dom;
    using Newtonsoft.Json.Linq;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Runs the URL parser against the web-platform-tests url test data.
    ///
    /// The data in Resources\urltestdata.json is a pinned copy of
    /// https://github.com/web-platform-tests/wpt/blob/master/url/resources/urltestdata.json
    /// taken from commit 181476aa16e8b28a07698bef3a0275fa53dd22e5 (2026-07-05). Refresh it
    /// deliberately - re-downloading changes the set of entries, and therefore the baseline below.
    /// </summary>
    [TestFixture]
    public class WptUrlTests
    {
        /// <summary>
        /// The number of entries from the WPT url test data that are currently parsed correctly.
        /// This is a ratchet: raise it whenever the parser improves, never lower it.
        /// </summary>
        private const Int32 MinimumPassing = 521;

        /// <summary>
        /// How many failing entries to include in the message of a failing run.
        /// </summary>
        private const Int32 MaxReportedFailures = 25;

        private static readonly Lazy<List<WptUrlTestEntry>> TestData = new(LoadTestData);

        private static List<WptUrlTestEntry> LoadTestData()
        {
            var array = JArray.Parse(Assets.urltestdata);
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

        [Test]
        public void WptUrlParsing()
        {
            var entries = TestData.Value;
            var failures = new List<String>();

            foreach (var entry in entries)
            {
                var mismatches = GetMismatches(entry);

                if (mismatches.Count > 0)
                {
                    failures.Add($"[{Describe(entry)}] {String.Join("; ", mismatches)}");
                }
            }

            var passing = entries.Count - failures.Count;
            var percentage = entries.Count == 0 ? 0.0 : 100.0 * passing / entries.Count;
            TestContext.WriteLine($"WPT url tests: {passing} of {entries.Count} pass ({percentage:F1}%), {failures.Count} fail, baseline is {MinimumPassing}.");

            if (passing < MinimumPassing)
            {
                Assert.Fail($"Regression: only {passing} of {entries.Count} entries pass, down from the baseline of {MinimumPassing}.{Environment.NewLine}{Report(failures)}");
            }
        }

        private static List<String> GetMismatches(WptUrlTestEntry entry)
        {
            var mismatches = new List<String>();
            var result = entry.Base != null
                ? new Url(new Url(entry.Base), entry.Input)
                : new Url(entry.Input);

            void Check(Boolean condition, String message)
            {
                if (!condition)
                {
                    mismatches.Add(message);
                }
            }

            if (entry.Failure)
            {
                Check(result.IsInvalid, "Expected failure, but parsing succeeded");
            }
            else if (result.IsInvalid)
            {
                mismatches.Add("Expected success, but parsing failed");
            }
            else
            {
                Check(result.Href == entry.Href, $"Href: expected \"{entry.Href}\", got \"{result.Href}\"");
                Check(result.Protocol == entry.Protocol, $"Protocol: expected \"{entry.Protocol}\", got \"{result.Protocol}\"");
                Check(result.UserName == entry.Username, $"Username: expected \"{entry.Username}\", got \"{result.UserName}\"");
                Check(result.Password == entry.Password, $"Password: expected \"{entry.Password}\", got \"{result.Password}\"");
                Check(result.Host == entry.Host, $"Host: expected \"{entry.Host}\", got \"{result.Host}\"");
                Check(result.HostName == entry.Hostname, $"Hostname: expected \"{entry.Hostname}\", got \"{result.HostName}\"");
                Check(result.Port == entry.Port, $"Port: expected \"{entry.Port}\", got \"{result.Port}\"");
                Check(result.PathName == entry.Pathname, $"Pathname: expected \"{entry.Pathname}\", got \"{result.PathName}\"");
                Check(result.Search == entry.Search, $"Search: expected \"{entry.Search}\", got \"{result.Search}\"");
                Check(result.Hash == entry.Hash, $"Hash: expected \"{entry.Hash}\", got \"{result.Hash}\"");
            }

            return mismatches;
        }

        private static String Describe(WptUrlTestEntry entry)
        {
            var input = entry.Input?.Length > 60 ? entry.Input.Substring(0, 60) + "..." : entry.Input;
            return entry.Base != null ? $"{input} against {entry.Base}" : input;
        }

        private static String Report(List<String> failures)
        {
            var builder = new StringBuilder();
            var count = Math.Min(failures.Count, MaxReportedFailures);

            for (var i = 0; i < count; i++)
            {
                builder.AppendLine(failures[i]);
            }

            if (failures.Count > count)
            {
                builder.AppendLine($"... and {failures.Count - count} more.");
            }

            return builder.ToString();
        }
    }
}
