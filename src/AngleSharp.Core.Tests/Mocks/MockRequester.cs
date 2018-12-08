namespace AngleSharp.Core.Tests.Mocks
{
    using AngleSharp.Io;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class MockRequester : BaseRequester
    {
        private Func<Request, IResponse> _answer;

        public MockRequester()
        {
            BuildResponse(_ => String.Empty);
        }

        public Action<Request> OnRequest
        {
            get;
            set;
        }

        public void BuildResponse(Func<Request, String> answer)
        {
            _answer = request =>
            {
                var text = answer.Invoke(request);
                var content = new MemoryStream(Encoding.UTF8.GetBytes(text));
                return new DefaultResponse { Address = request.Address, Content = content, StatusCode = System.Net.HttpStatusCode.OK };
            };
        }

        public void BuildResponse(Func<Request, IResponse> answer)
        {
            _answer = answer;
        }

        public void BuildResponses(IEnumerable<Func<Request, IResponse>> answers)
        {
            var enumerator = answers.GetEnumerator();
            _answer = req =>
            {
                if (!enumerator.MoveNext())
                {
                    var content = new MemoryStream(Encoding.UTF8.GetBytes(String.Empty));
                    return new DefaultResponse { Address = req.Address, Content = content, StatusCode = System.Net.HttpStatusCode.NotFound };
                }

                return enumerator.Current(req);
            };
        }

        public IResponse Request(Request request)
        {
            OnRequest?.Invoke(request);
            return _answer.Invoke(request);
        }

        public Task<IResponse> RequestAsync(Request request)
        {
            return RequestAsync(request, CancellationToken.None);
        }

        protected override async Task<IResponse> PerformRequestAsync(Request request, CancellationToken cancellationToken)
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

        public override Boolean SupportsProtocol(String protocol)
        {
            return true;
        }
    }
}
