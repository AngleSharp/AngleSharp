namespace AngleSharp.Core.Tests.Mocks
{
    using AngleSharp.Io;
    using AngleSharp.Media;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    sealed class ResourceService<TResource> : IResourceService<TResource>
        where TResource : IResourceInfo
    {
        private readonly String _mimeType;
        private readonly Func<IResponse, TResource> _creator;

        public ResourceService(String mimeType, Func<IResponse, TResource> creator)
        {
            _mimeType = mimeType;
            _creator = creator;
        }

        public Boolean SupportsType(String mimeType)
        {
            return mimeType.Equals(_mimeType, StringComparison.OrdinalIgnoreCase);
        }

        public Task<TResource> CreateAsync(IResponse response, CancellationToken cancel)
        {
            var result = _creator(response);
            return Task.FromResult(result);
        }
    }
}
