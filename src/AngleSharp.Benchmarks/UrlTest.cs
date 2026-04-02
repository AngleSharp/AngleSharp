using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AngleSharp.Benchmarks
{
    public sealed class UrlTest
    {
        private static readonly HttpClient http = new();

        private UrlTest(String name, String source)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            Name = name;
            Source = source;
        }

        internal static async Task<UrlTest> For(String url, String extension, Boolean withBuffer)
        {
            try
            {
                var source = String.Empty;
                var uri = new Uri(url);
                var name = uri.Host.Replace("www.", "").Replace(".com", "").Replace(".de", "").Replace(".org", "");
                if (!Directory.Exists("temp"))
                {
                    Directory.CreateDirectory("temp");
                }
                var fileName = Path.Combine("temp", name + extension);

                if (!withBuffer || !File.Exists(fileName))
                {
                    const Int32 maxRetries = 3;
                    const Int32 minValidLength = 128;

                    for (var attempt = 1; attempt <= maxRetries; attempt++)
                    {
                        using var request = new HttpRequestMessage(HttpMethod.Get, uri);
                        request.Headers.UserAgent.ParseAdd(
                            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.0.0 Safari/537.36");
                        using var response = await http.SendAsync(request);

                        if (!response.IsSuccessStatusCode)
                        {
                            Console.WriteLine("Attempt {0}/{1} for \"{2}\" returned HTTP {3}, retrying...",
                                attempt, maxRetries, url, (Int32)response.StatusCode);

                            if (attempt < maxRetries)
                            {
                                await Task.Delay(1000 * attempt);
                            }

                            continue;
                        }

                        source = await response.Content.ReadAsStringAsync();

                        if (source.Length >= minValidLength)
                        {
                            break;
                        }

                        Console.WriteLine("Attempt {0}/{1} for \"{2}\" returned only {3} chars, retrying...",
                            attempt, maxRetries, url, source.Length);

                        if (attempt < maxRetries)
                        {
                            await Task.Delay(1000 * attempt);
                        }
                    }

                    if (source.Length < minValidLength)
                    {
                        Console.WriteLine("Warning: \"{0}\" returned only {1} chars after {2} attempts",
                            url, source.Length, maxRetries);
                    }

                    if (withBuffer && source.Length >= minValidLength)
                    {
                        File.WriteAllText(fileName, source);
                    }
                }
                else
                {
                    source = File.ReadAllText(fileName);
                }

                return new UrlTest(name, source);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading \"{0}\": {1}", url, ex.Message);
                return null;
            }
        }

        public String Name { get; }

        public String Source { get; }

        public override String ToString()
        {
            return Name;
        }
    }
}