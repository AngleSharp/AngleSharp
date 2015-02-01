namespace UnitTests.Mocks
{
    using AngleSharp.Network;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class MockRequester : IRequester
    {
        public IResponse Request(IRequest request)
        {
            return null;
        }

        public Task<IResponse> RequestAsync(IRequest request)
        {
            return null;
        }

        public Task<IResponse> RequestAsync(IRequest request, CancellationToken cancellationToken)
        {
            return null;
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
