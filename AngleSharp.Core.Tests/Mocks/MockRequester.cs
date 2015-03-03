namespace AngleSharp.Core.Tests.Mocks
{
    using AngleSharp.Network;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    public class MockRequester : IRequester
    {
        public IResponse Request(IRequest request)
        {
            return new DefaultResponse { Address = request.Address, Content = new MemoryStream() };
        }

        public Task<IResponse> RequestAsync(IRequest request)
        {
            return RequestAsync(request, CancellationToken.None);
        }

        public async Task<IResponse> RequestAsync(IRequest request, CancellationToken cancellationToken)
        {
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
