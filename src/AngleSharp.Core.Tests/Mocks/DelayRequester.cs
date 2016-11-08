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
        private Int32 _count;

        public DelayRequester(Int32 timeout)
        {
            _timeout = timeout;
            _count = 0;
        }

        public Int32 RequestCount
        {
            get { return _count; }
        }

        public override Boolean SupportsProtocol(String protocol)
        {
            return true;
        }

        protected override async Task<IResponse> PerformRequestAsync(Request request, CancellationToken cancel)
        {
            await Task.Delay(_timeout, cancel);
            _count++;
            return new DefaultResponse
            {
                Address  = request.Address,
                Content = MemoryStream.Null,
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}
