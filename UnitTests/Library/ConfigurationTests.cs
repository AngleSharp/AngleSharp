using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngleSharp;
using System.Globalization;
using UnitTests.Mocks;

namespace UnitTests.Library
{
    [TestClass]
    public class ConfigurationTests
    {
        [TestCleanup]
        public void ResetChanges()
        {
            Configuration.Reset();
        }

        [TestMethod]
        public void TestResetConfiguration()
        {
            var culture = new CultureInfo("en-gb");
            var language = Configuration.Language;

            Configuration.Culture = culture;

            Assert.AreEqual(culture, Configuration.Culture);
            Assert.AreNotEqual(language, Configuration.Language);

            Configuration.Reset();

            Assert.AreNotEqual(culture, Configuration.Culture);
            Assert.AreEqual(language, Configuration.Language);
        }

        [TestMethod]
        public void TestSaveConfiguration()
        {
            var culture = new CultureInfo("en-gb");
            var language = Configuration.Language;

            Configuration.Culture = culture;

            Assert.AreEqual(culture, Configuration.Culture);
            Assert.AreNotEqual(language, Configuration.Language);

            var config = Configuration.Save();
            Configuration.Reset();

            Assert.AreNotEqual(culture, Configuration.Culture);
            Assert.AreEqual(language, Configuration.Language);
            Assert.IsNotNull(config);
        }

        [TestMethod]
        public void TestLoadConfiguration()
        {
            var culture = new CultureInfo("en-gb");
            var language = Configuration.Language;

            Configuration.Culture = culture;

            Assert.AreEqual(culture, Configuration.Culture);
            Assert.AreNotEqual(language, Configuration.Language);

            var config = Configuration.Save();
            Configuration.Reset();

            Assert.AreNotEqual(culture, Configuration.Culture);
            Assert.AreEqual(language, Configuration.Language);

            Configuration.Load(config);

            Assert.AreEqual(culture, Configuration.Culture);
            Assert.AreNotEqual(language, Configuration.Language);
        }

        [TestMethod]
        public void TestRegisterRequester()
        {
            Assert.IsFalse(Configuration.HasHttpRequester);
            Configuration.RegisterHttpRequester<MockRequester>();
            Assert.IsTrue(Configuration.HasHttpRequester);
            Configuration.UnregisterHttpRequester<MockRequester>();
            Assert.IsFalse(Configuration.HasHttpRequester);
        }
    }
}
