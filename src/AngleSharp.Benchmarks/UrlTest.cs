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

        private UrlTest(string name, string source)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Name = name;
            Source = source;
        }

        internal static async Task<UrlTest> For(string url, string extension, bool withBuffer)
        {
            try
            {
                var source = string.Empty;
                var uri = new Uri(url);
                var name = uri.Host.Replace("www.", "").Replace(".com", "").Replace(".de", "").Replace(".org", "");
                if (!Directory.Exists("temp"))
                {
                    Directory.CreateDirectory("temp");
                }
                var fileName = Path.Combine("temp", name + extension);

                if (!withBuffer || !File.Exists(fileName))
                {
                    http.DefaultRequestHeaders.UserAgent.Clear();
                    http.DefaultRequestHeaders.UserAgent.ParseAdd(
                        "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.27 Safari/537.36 OPR/26.0.1656.8 (Edition beta)");
                    var content = await http.GetAsync(uri);
                    source = await content.Content.ReadAsStringAsync();

                    if (withBuffer)
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

        public string Name { get; }

        public string Source { get; }

        public override string ToString()
        {
            return Name;
        }
    }
}