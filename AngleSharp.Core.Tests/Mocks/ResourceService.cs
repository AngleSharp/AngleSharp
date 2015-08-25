namespace AngleSharp.Core.Tests.Mocks
{
    using AngleSharp.Network;
    using AngleSharp.Services;
    using AngleSharp.Services.Media;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    sealed class ResourceService<TResource> : IResourceService<TResource>
        where TResource : IResourceInfo
    {
        readonly String _mimeType;
        readonly Func<IResponse, TResource> _creator;

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
