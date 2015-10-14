namespace AngleSharp.Core.Tests.Mocks
{
    using AngleSharp.Network;
    using AngleSharp.Network.Default;
    using System;
    using System.IO;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    sealed class DelayRequester : IRequester
    {
        readonly Int32 _timeout;
        Int32 _count;

        public DelayRequester(Int32 timeout)
        {
            _timeout = timeout;
            _count = 0;
        }

        public Int32 RequestCount
        {
            get { return _count; }
        }

        public Boolean SupportsProtocol(String protocol)
        {
            return true;
        }

        public async Task<IResponse> RequestAsync(IRequest request, CancellationToken cancel)
        {
            await Task.Delay(_timeout, cancel);
            _count++;
            return new Response
            {
                Address  = request.Address,
                Content = MemoryStream.Null,
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}
