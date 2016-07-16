namespace AngleSharp.Core.Tests.Mocks
{
    using AngleSharp.Services;
    using System;

    sealed class MockEntityResolver : IEntityProvider
    {
        readonly Func<String, String> _resolver;

        public MockEntityResolver(Func<String, String> resolver)
        {
            _resolver = resolver;
        }

        public String GetSymbol(String name)
        {
            return _resolver.Invoke(name);
        }
    }
}
