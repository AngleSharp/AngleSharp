namespace AngleSharp.Core.Tests.Mocks
{
    using AngleSharp.Network;
    using AngleSharp.Network.Default;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class MockRequester : IRequester
    {
        public Action<IRequest> OnRequest
        {
            get;
            set;
        }

        public Func<IRequest, String> BuildResponse
        {
            get;
            set;
        }

        public IResponse Request(IRequest request)
        {
            if (OnRequest != null)
            {
                OnRequest(request);
            }

            var builder = BuildResponse ?? (_ => String.Empty);
            var text = builder(request);
            var content = new MemoryStream(Encoding.UTF8.GetBytes(text));
            return new Response { Address = request.Address, Content = content };
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
