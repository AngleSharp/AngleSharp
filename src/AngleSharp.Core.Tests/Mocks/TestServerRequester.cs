namespace AngleSharp.Core.Tests.Mocks
{
    using AngleSharp.Io;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    sealed class TestServerRequester : BaseRequester
    {
        private readonly IDictionary<String, String> _mapping;

        public TestServerRequester(IDictionary<String, String> mapping)
        {
            _mapping = mapping;
        }

        public override Boolean SupportsProtocol(String protocol)
        {
            return true;
        }

        protected override async Task<IResponse> PerformRequestAsync(Request request, CancellationToken cancel)
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

            return new DefaultResponse
            {
                Address = request.Address,
                Content = content,
                StatusCode = status
            };
        }
    }
}
