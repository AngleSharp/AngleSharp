namespace AngleSharp.Core.Tests.Mocks
{
    using AngleSharp.Network;
    using AngleSharp.Network.Default;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    sealed class TestServerRequester : IRequester
    {
        readonly IDictionary<String, String> _mapping;

        public TestServerRequester(IDictionary<String, String> mapping)
        {
            _mapping = mapping;
        }

        public Boolean SupportsProtocol(String protocol)
        {
            return true;
        }

        public async Task<IResponse> RequestAsync(IRequest request, CancellationToken cancel)
        {
            var value = default(String);
            var status = HttpStatusCode.NotFound;
            var content = Stream.Null;

            if (_mapping.TryGetValue(request.Address.Path, out value))
            {
                status = HttpStatusCode.OK;
                content = new MemoryStream(Encoding.UTF8.GetBytes(value));
            }

            await Task.Delay(1);

            return new Response
            {
                Address = request.Address,
                Content = content,
                StatusCode = status
            };
        }
    }
}
