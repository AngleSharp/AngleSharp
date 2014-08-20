namespace AngleSharp.Network
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Useful extensions for IRequester objects.
    /// </summary>
    public static class RequesterExtensions
    {
        /// <summary>
        /// Performs an asynchronous http request with the given options without
        /// taking a custom cancellation token.
        /// </summary>
        /// <param name="requester">The requester to use.</param>
        /// <param name="request">The options to consider.</param>
        /// <returns>The task that will eventually give the response data.</returns>
        public static Task<IResponse> RequestAsync(this IRequester requester, IRequest request)
        {
            return requester.RequestAsync(request, CancellationToken.None);
        }
    }
}
