namespace AngleSharp.Core.Tests.External
{
    using AngleSharp.Network;
    using AngleSharp.Network.Default;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    class PageRequester : IRequester
    {
        readonly static HttpRequester _default = new HttpRequester();
        readonly static String _directory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "..", "..", "Resources");
        readonly static SiteMapping _mapping = new SiteMapping(Path.Combine(_directory, "content.xml"));

        public static IEnumerable<IRequester> All
        {
            get { yield return new PageRequester(); }
        }

        public Boolean SupportsProtocol(String protocol)
        {
            return _default.SupportsProtocol(protocol);
        }

        public async Task<IResponse> RequestAsync(IRequest request, CancellationToken cancel)
        {
            var url = request.Address.Href;

            if (_mapping.Contains(url) == false)
            {
                var response = await _default.RequestAsync(request, cancel).ConfigureAwait(false);
                var fileName = await AddResourceAsync(url, response).ConfigureAwait(false);
                _mapping.Add(url, fileName);
            }

            return await ReadResourceAsync(url).ConfigureAwait(false);
        }

        Task<IResponse> ReadResourceAsync(String url)
        {
            var fileName = _mapping[url];
            var path = Path.Combine(_directory, fileName);
            var content = File.ReadAllBytes(path);
            return GetResponseAsync(content);
        }

        async Task<String> AddResourceAsync(String url, IResponse response)
        {
            var counter = 1;
            var file = default(String);
            var path = default(String);

            do
            {
                file = String.Format("{0}-{1}.res", response.Address.HostName, counter++);
                path = Path.Combine(_directory, file);
            }
            while (File.Exists(path));

            var content = await GetContentAsync(response).ConfigureAwait(false);
            File.WriteAllBytes(path, content);
            return file;
        }

        async Task<Byte[]> GetContentAsync(IResponse response)
        {
            var code = (int)response.StatusCode;
            var addr = response.Address.Href;
            var hdrs = response.Headers;
            var ctnt = response.Content;
            var ms = new MemoryStream();
            ms.Write(code);
            ms.Write(addr);
            ms.Write(hdrs);
            await ctnt.CopyToAsync(ms).ConfigureAwait(false);
            return ms.ToArray();
        }

        async Task<IResponse> GetResponseAsync(Byte[] content)
        {
            var ms = new MemoryStream(content);
            var code = ms.ReadInt();
            var addr = ms.ReadString();
            var hdrs = ms.ReadDictionary();
            var ctnt = new MemoryStream();
            await ms.CopyToAsync(ctnt).ConfigureAwait(false);
            ctnt.Position = 0;
            return new Response
            {
                StatusCode = (HttpStatusCode)code,
                Address = new Url(addr),
                Headers = hdrs,
                Content = ctnt
            };
        }
    }
}
