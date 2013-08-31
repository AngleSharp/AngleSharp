using AngleSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public Dictionary<String, String> DefaultHeaders
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
