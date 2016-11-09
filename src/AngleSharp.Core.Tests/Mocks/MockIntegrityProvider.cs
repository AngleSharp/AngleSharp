namespace AngleSharp.Core.Tests.Mocks
{
    using AngleSharp.Io;
    using System;

    sealed class MockIntegrityProvider : IIntegrityProvider
    {
        private readonly Func<Byte[], String, Boolean> _validator;

        public MockIntegrityProvider(Func<Byte[], String, Boolean> validator)
        {
            _validator = validator;
        }

        public Boolean IsSatisfied(Byte[] content, String integrity)
        {
            return _validator.Invoke(content, integrity);
        }
    }
}
