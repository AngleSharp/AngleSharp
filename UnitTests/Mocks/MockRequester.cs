using AngleSharp.Network;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace UnitTests.Mocks
{
    public class MockRequester : IHttpRequester
    {
        public IHttpResponse Request(IHttpRequest request)
        {
            return null;
        }

        public Task<IHttpResponse> RequestAsync(IHttpRequest request)
        {
            return null;
        }

        public Task<IHttpResponse> RequestAsync(IHttpRequest request, CancellationToken cancellationToken)
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
    }
}
