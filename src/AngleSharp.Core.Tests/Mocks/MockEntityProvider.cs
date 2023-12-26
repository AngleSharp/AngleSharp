namespace AngleSharp.Core.Tests.Mocks
{
    using AngleSharp.Dom;
    using System;
    using Common;

    sealed class MockEntityProvider : IEntityProvider, IEntityProviderExtended
    {
        readonly Func<String, String> _resolver;

        public MockEntityProvider(Func<String, String> resolver)
        {
            _resolver = resolver;
        }

        public String GetSymbol(String name)
        {
            return _resolver.Invoke(name);
        }

        public String GetSymbol(StringOrMemory name)
        {
            return _resolver.Invoke(name.ToString());
        }
    }
}
