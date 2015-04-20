namespace AngleSharp.Core.Tests.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using AngleSharp.Network;
    using AngleSharp.Network.Default;

    public class MockRequester : IRequester
    {
        public IResponse Request(IRequest request)
        {
            return new Response { Address = request.Address, Content = new MemoryStream() };
        }

        public Task<IResponse> RequestAsync(IRequest request)
        {
            return RequestAsync(request, CancellationToken.None);
        }

        public async Task<IResponse> RequestAsync(IRequest request, CancellationToken cancellationToken)
        {
            await Task.Delay(0);
            return Request(request);
        }

        public Dictionary<String, String> Headers
        {
            get;
            set;
        }

        public TimeSpan Timeout
        {
            get;
            set;
        }

        public Boolean SupportsProtocol(String protocol)
        {
            return true;
        }
    }
}
