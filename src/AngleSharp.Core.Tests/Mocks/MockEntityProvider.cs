namespace AngleSharp.Core.Tests.Mocks
{
    using AngleSharp.Dom;
    using System;

    sealed class MockEntityProvider : IEntityProvider
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
    }
}
