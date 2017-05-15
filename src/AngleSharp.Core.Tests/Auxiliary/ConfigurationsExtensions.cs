using System;
using System.Linq;
using AngleSharp.Io;
using NUnit.Framework;

namespace AngleSharp.Core.Tests.Auxiliary
{
    [TestFixture]
    public class ConfigurationsExtensionsTests
    {
        [Test]
        public void WithoutTServiceRemovesItems()
        {
            var config = new Configuration(new object[0])
                .With((IRequester)new DefaultHttpRequester())
                .Without<IRequester>();

            Assert.AreEqual(0, config.Services.Count());
        }
    }
}
