namespace AngleSharp.Core.Tests
{
    using AngleSharp.Network;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Requests a DTD file from an embedded resource.
    /// </summary>
    public class DtdRequester : IRequester
    {
        public IResponse Request(IRequest request)
        {
            var name = request.Address.Path;
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("UnitTests.Resources." + name);

            if (stream == null)
                throw new ArgumentException("The DTD " + name + " could not be found! Check the name and the availability of this DTD.");

            return new DefaultResponse { Content = stream };
        }

        public Task<IResponse> RequestAsync(IRequest request)
        {
            return RequestAsync(request, CancellationToken.None);
        }

        public Task<IResponse> RequestAsync(IRequest request, CancellationToken cancellationToken)
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

        public Boolean SupportsProtocol(String protocol)
        {
            return true;
        }
    }
}
