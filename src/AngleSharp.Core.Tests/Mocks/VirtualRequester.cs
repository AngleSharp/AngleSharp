namespace AngleSharp.Core.Tests.Mocks
{
    using AngleSharp.Io;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    sealed class VirtualRequester : IRequester
    {
        private readonly Func<Request, IResponse> _onRequest;

        public VirtualRequester(Func<Request, IResponse> onRequest)
        {
            _onRequest = onRequest;
        }

        public Task<IResponse> RequestAsync(Request request, CancellationToken cancel)
        {
            var response = _onRequest.Invoke(request);
            return Task.FromResult(response);
        }

        public Boolean SupportsProtocol(String protocol)
        {
            return true;
        }
    }
}
