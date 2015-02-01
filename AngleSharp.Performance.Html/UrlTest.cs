namespace AngleSharp.Performance.Html
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;

    sealed class UrlTest : ITest
    {
        static readonly HttpClient http = new HttpClient();

        private UrlTest(String name, String source)
        {
            Name = name;
            Source = source;
        }

        public static Boolean UseBuffer
        {
            get;
            set;
        }

        internal static async Task<UrlTest> For(String url)
        {
            try
            {
                var source = String.Empty;
                var uri = new Uri(url);
                var name = uri.Host.Replace("www.", "").Replace(".com", "").Replace(".de", "").Replace(".org", "");
                var fileName = name + ".html";

                if (!UseBuffer || !File.Exists(fileName))
                {
                    http.DefaultRequestHeaders.UserAgent.Clear();
                    http.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.27 Safari/537.36 OPR/26.0.1656.8 (Edition beta)");
                    var content = await http.GetAsync(uri);
                    source = await content.Content.ReadAsStringAsync();

                    if (UseBuffer)
                        File.WriteAllText(fileName, source);
                }
                else
                    source = File.ReadAllText(fileName);

                return new UrlTest(name, source);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading \"{0}\": {1}", url, ex.Message);
                return null;
            }
        }

        public String Name
        {
            get;
            private set;
        }

        public String Source
        {
            get;
            private set;
        }
    }
}
