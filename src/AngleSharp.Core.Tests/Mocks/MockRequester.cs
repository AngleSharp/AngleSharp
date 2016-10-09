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
        private Func<IRequest, IResponse> _answer;

        public MockRequester()
        {
            BuildResponse(_ => String.Empty);
        }

        public Action<IRequest> OnRequest
        {
            get;
            set;
        }

        public void BuildResponse(Func<IRequest, String> answer)
        {
            _answer = request =>
            {
                var text = answer.Invoke(request);
                var content = new MemoryStream(Encoding.UTF8.GetBytes(text));
                return new Response { Address = request.Address, Content = content, StatusCode = System.Net.HttpStatusCode.OK };
            };
        }

        public void BuildResponse(Func<IRequest, IResponse> answer)
        {
            _answer = answer;
        }

        public IResponse Request(IRequest request)
        {
            OnRequest?.Invoke(request);
            return _answer.Invoke(request);
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
