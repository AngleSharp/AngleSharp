namespace AngleSharp.Core.Tests.Urls
{
    using AngleSharp.Io;
    using NUnit.Framework;
    using System.Linq;

    [TestFixture]
    public class MimeTypeTests
    {
        [Test]
        public void MimeTypeWithOnlyGeneralType()
        {
            var original = "application";
            var mt = new MimeType(original);
            Assert.AreEqual("application", mt.GeneralType);
            Assert.AreEqual("", mt.MediaType);
            Assert.AreEqual("", mt.Suffix);
            Assert.AreEqual(0, mt.Keys.Count());
            Assert.AreEqual(original, mt.ToString());
        }

        [Test]
        public void MimeTypeInCommonForm()
        {
            var original = "application/html";
            var mt = new MimeType(original);
            Assert.AreEqual("application", mt.GeneralType);
            Assert.AreEqual("html", mt.MediaType);
            Assert.AreEqual("", mt.Suffix);
            Assert.AreEqual(0, mt.Keys.Count());
            Assert.AreEqual(original, mt.ToString());
        }

        [Test]
        public void MimeTypeWithSuffix()
        {
            var original = "application/html+xml";
            var mt = new MimeType(original);
            Assert.AreEqual("application", mt.GeneralType);
            Assert.AreEqual("html", mt.MediaType);
            Assert.AreEqual("xml", mt.Suffix);
            Assert.AreEqual(0, mt.Keys.Count());
            Assert.AreEqual(original, mt.ToString());
        }

        [Test]
        public void MimeTypeWithSuffixAndParameter()
        {
            var original = "application/html+xml;foo=bar";
            var mt = new MimeType(original);
            Assert.AreEqual("application", mt.GeneralType);
            Assert.AreEqual("html", mt.MediaType);
            Assert.AreEqual("xml", mt.Suffix);
            Assert.AreEqual(1, mt.Keys.Count());
            Assert.AreEqual("foo", mt.Keys.First());
            Assert.AreEqual("bar", mt.GetParameter("foo"));
            Assert.AreEqual(original, mt.ToString());
        }

        [Test]
        public void MimeTypeWithMultipleParameters()
        {
            var original = "application/html;foo=bar;cool=dude";
            var mt = new MimeType(original);
            Assert.AreEqual("application", mt.GeneralType);
            Assert.AreEqual("html", mt.MediaType);
            Assert.AreEqual("", mt.Suffix);
            Assert.AreEqual(2, mt.Keys.Count());
            Assert.AreEqual("foo", mt.Keys.First());
            Assert.AreEqual("cool", mt.Keys.Last());
            Assert.AreEqual("bar", mt.GetParameter("foo"));
            Assert.AreEqual("dude", mt.GetParameter("cool"));
            Assert.AreEqual(original, mt.ToString());
        }

        [Test]
        public void MimeTypeWithInvalidFormat()
        {
            var original = "app;yo=there";
            var mt = new MimeType(original);
            Assert.AreEqual("app;yo=there", mt.GeneralType);
            Assert.AreEqual("", mt.MediaType);
            Assert.AreEqual("", mt.Suffix);
            Assert.AreEqual(0, mt.Keys.Count());
            Assert.AreEqual(original, mt.ToString());
        }

        [Test]
        public void MimeTypeWithSingleParameter()
        {
            var original = "text/html;yo=there";
            var mt = new MimeType(original);
            Assert.AreEqual("text", mt.GeneralType);
            Assert.AreEqual("html", mt.MediaType);
            Assert.AreEqual("", mt.Suffix);
            Assert.AreEqual(1, mt.Keys.Count());
            Assert.AreEqual("yo", mt.Keys.First());
            Assert.AreEqual("there", mt.GetParameter("yo"));
            Assert.AreEqual(original, mt.ToString());
        }

        [Test]
        public void MimeTypeWithRecoveredIllFormat()
        {
            var original = "text/+xml;yo=there";
            var mt = new MimeType(original);
            Assert.AreEqual("text", mt.GeneralType);
            Assert.AreEqual("", mt.MediaType);
            Assert.AreEqual("xml", mt.Suffix);
            Assert.AreEqual(1, mt.Keys.Count());
            Assert.AreEqual("yo", mt.Keys.First());
            Assert.AreEqual("there", mt.GetParameter("yo"));
            Assert.AreEqual(original, mt.ToString());
        }
    }
}
