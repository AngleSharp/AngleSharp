namespace AngleSharp.Core.Tests
{
    using AngleSharp.Io;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Requests a DTD file from an embedded resource.
    /// </summary>
    public class DtdRequester : BaseRequester
    {
        public IResponse Request(Request request)
        {
            var name = request.Address.Path;
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("AngleSharp.Core.Tests.Resources." + name);

            if (stream == null)
                throw new ArgumentException("The DTD " + name + " could not be found! Check the name and the availability of this DTD.");

            return new DefaultResponse { Content = stream };
        }

        public Task<IResponse> RequestAsync(Request request)
        {
            return RequestAsync(request, CancellationToken.None);
        }

        protected override Task<IResponse> PerformRequestAsync(Request request, CancellationToken cancellationToken)
        {
            return Task.Run(() => Request(request));
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
