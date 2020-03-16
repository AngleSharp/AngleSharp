namespace AngleSharp.Core.Tests.Library
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class BrowsingContextTests
    {
        [Test]
        public void BrowsingContextAbstractionShouldBeDisposable()
        {
            Assert.Contains(typeof(IDisposable), typeof(IBrowsingContext).GetInterfaces());
        }
    }
}