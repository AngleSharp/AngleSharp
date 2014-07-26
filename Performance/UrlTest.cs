using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;

namespace Performance
{
    sealed class UrlTest : ITest
    {
        static readonly HttpClient http = new HttpClient();

        public UrlTest(Uri source)
        {
            Name = source.Host.Replace("www.", "").Replace(".com", "").Replace(".de", "").Replace(".org", "");
            var fileName = Name + ".html";

            if (!UseBuffer || !File.Exists(fileName))
            {
                Debug.Write("Download page from " + source + " ... ");
                Source = http.GetStringAsync(source).Result;
                Debug.WriteLine("done!");

                if (UseBuffer)
                    File.WriteAllText(fileName, Source);
            }
            else
                Source = File.ReadAllText(fileName);
        }

        public static Boolean UseBuffer
        {
            get;
            set;
        }

        internal static UrlTest For(String url)
        {
            return new UrlTest(new Uri(url));
        }

        public String Name
        {
            get;
            set;
        }

        public String Source
        {
            get;
            private set;
        }
    }
}
