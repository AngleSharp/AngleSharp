namespace AngleSharp
{
    using AngleSharp.Network;
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a helper to construct objects with externally
    /// defined classes and libraries.
    /// </summary>
    static class ConfigurationExtensions
    {
        public static Task<Stream> LoadAsync(this IConfiguration configuration, Uri url)
        {
            return configuration.LoadAsync(url, CancellationToken.None);
        }

        public static async Task<Stream> LoadAsync(this IConfiguration configuration, Uri url, CancellationToken cancel)
        {
            if (!configuration.AllowHttpRequests)
                return Stream.Null;

            var requester = configuration.CreateHttpRequest();

            if (requester == null)
                throw new NullReferenceException("No HTTP requester has been set up. Configure one by adding an entry to the current DependencyResolver.");

            var request = DependencyResolver.Current.GetService<IHttpRequest>();
            request.Address = url;
            request.Method = HttpMethod.GET;
            var response = await requester.RequestAsync(request, cancel);
            return response.Content;
        }

        public static Task<Stream> SendAsync(this IConfiguration configuration, Uri url, String content, String mimeType, HttpMethod method = HttpMethod.POST)
        {
            return configuration.SendAsync(url, content, mimeType, method, CancellationToken.None);
        }

        public static async Task<Stream> SendAsync(this IConfiguration configuration, Uri url, String content, String mimeType, HttpMethod method, CancellationToken cancel)
        {
            if (!configuration.AllowHttpRequests)
                return Stream.Null;

            var requester = configuration.CreateHttpRequest();

            if (requester == null)
                throw new NullReferenceException("No HTTP requester has been set up. Configure one by adding an entry to the current DependencyResolver.");

            var request = DependencyResolver.Current.GetService<IHttpRequest>();
            request.Address = url;
            request.Headers[HeaderNames.Content_Type] = mimeType;
            request.Method = method;
            var response = await requester.RequestAsync(request, cancel);
            return response.Content;
        }
    }
}
