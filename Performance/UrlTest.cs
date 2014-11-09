namespace Performance
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
