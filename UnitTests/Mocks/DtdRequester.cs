using AngleSharp;
using AngleSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace UnitTests
{
    /// <summary>
    /// Requests a DTD file from an embedded resource.
    /// </summary>
    public class DtdRequester : IHttpRequester
    {
        public IHttpResponse Request(IHttpRequest request)
        {
            var name = request.Address.Segments[1];
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("UnitTests.Resources." + name);

            if (stream == null)
                throw new ArgumentException("The DTD " + name + " could not be found! Check the name and the availability of this DTD.");

            return new DefaultHttpResponse { Content = stream };
        }

        public Task<IHttpResponse> RequestAsync(IHttpRequest request)
        {
            return RequestAsync(request, CancellationToken.None);
        }

        public Task<IHttpResponse> RequestAsync(IHttpRequest request, CancellationToken cancellationToken)
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
    }
}
