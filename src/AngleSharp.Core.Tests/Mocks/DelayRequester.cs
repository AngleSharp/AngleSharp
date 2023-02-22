namespace AngleSharp.Core.Tests.Mocks
{
    using AngleSharp.Io;
    using System;
    using System.IO;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    sealed class DelayRequester : BaseRequester
    {
        private readonly Int32 _timeout;

        public DelayRequester(Int32 timeout)
        {
            _timeout = timeout;
            RequestCount = 0;
        }

        public Int32 RequestCount { get; private set; }

        public override Boolean SupportsProtocol(String protocol)
        {
            return true;
        }

        protected override async Task<IResponse> PerformRequestAsync(Request request, CancellationToken cancel)
        {
            await Task.Delay(_timeout, cancel);
            RequestCount++;
            return new DefaultResponse
            {
                Address  = request.Address,
                Content = MemoryStream.Null,
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}
