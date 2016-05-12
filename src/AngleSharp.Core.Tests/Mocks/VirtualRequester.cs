namespace AngleSharp.Core.Tests.Mocks
{
    using AngleSharp.Network;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    sealed class VirtualRequester : IRequester
    {
        private readonly Func<IRequest, IResponse> _onRequest;

        public VirtualRequester(Func<IRequest, IResponse> onRequest)
        {
            _onRequest = onRequest;
        }

        public Task<IResponse> RequestAsync(IRequest request, CancellationToken cancel)
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
