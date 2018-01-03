namespace AngleSharp.Core.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class UrlTests
    {
        [Test]
        public void UrlWithHttpAsResourceIsARelativeUrl()
        {
            var address = "http";
            var result = new Url(address);
            Assert.That(result.IsInvalid, Is.False);
            Assert.That(result.Href, Is.EqualTo("http"));
            Assert.That(result.IsRelative, Is.True);
        }

        [Test]
        public void UrlWithHttpAndColonIsAValidUrl()
        {
            var address = "http:";
            var result = new Url(address);
            Assert.That(result.IsInvalid, Is.False);
            Assert.That(result.Href, Is.EqualTo("http:///"));
            Assert.That(String.IsNullOrEmpty(result.Path), Is.True);
            Assert.That(String.IsNullOrEmpty(result.Query), Is.True);
        }

        [Test]
        public void UrlWithSchemeOnlyIsAnInvalidUrl()
        {
            var address = "http://";
            var result = new Url(address);
            Assert.That(result.IsInvalid, Is.True);
            Assert.That(result.Href, Is.EqualTo("http:///"));
            Assert.That(String.IsNullOrEmpty(result.Path), Is.True);
            Assert.That(String.IsNullOrEmpty(result.Query), Is.True);
        }


        [Test]
        public void ValidityOfValidGoogleHostnameAddress()
        {
            var address = "http://www.google.de";
            var result = new Url(address);
            Assert.IsFalse(result.IsInvalid);
        }

        [Test]
        public void ValidityOfValidGoogleHostnameAndPathEmptyAddress()
        {
            var address = "http://www.google.de/";
            var result = new Url(address);
            Assert.IsFalse(result.IsInvalid);
        }

        [Test]
        public void ValidityOfValidGoogleHostnameAndPathAddress()
        {
            var address = "http://www.google.de/some-path";
            var result = new Url(address);
            Assert.IsFalse(result.IsInvalid);
        }

        [Test]
        public void ValidityOfValidGoogleHostnameAndPathAndFragmentEmptyAddress()
        {
            var address = "http://www.google.de/some-path#";
            var result = new Url(address);
            Assert.IsFalse(result.IsInvalid);
        }

        [Test]
        public void ValidityOfValidGoogleHostnameAndPathAndQueryAndFragmentAddress()
        {
            var address = "http://www.google.de/some-path?a=b#header";
            var result = new Url(address);
            Assert.IsFalse(result.IsInvalid);
        }

        [Test]
        public void ValidityOfInvalidFragmentEmptyHostnameAddress()
        {
            var address = "http://#foo";
            var result = new Url(address);
            Assert.IsTrue(result.IsInvalid);
        }

        [Test]
        public void OriginOfGitHubAddressShouldBeGitHubCom()
        {
            var address = "https://github.com/FlorianRappl/AngleSharp";
            var result = new Url(address);
            Assert.IsFalse(result.IsInvalid);
            Assert.AreEqual("https://github.com", result.Origin);
        }

        [Test]
        public void UrlWithSpecialCharacterDash()
        {
            var address = "http://example-domain.com/image.jpg";
            var result = new Url(address);
            Assert.IsFalse(result.IsInvalid);
            Assert.AreEqual(address, result.Href);
            Assert.AreEqual("example-domain.com", result.HostName);
        }

        [Test]
        public void UrlQueryWithEmDashCharacter()
        {
            var address = "http://test/?hi—there";
            var result = new Url(address);
            Assert.IsFalse(result.IsInvalid);
            Assert.AreEqual("http://test/?hi%E2%80%94there", result.Href);
            Assert.AreEqual("hi%E2%80%94there", result.Query);
        }

        [Test]
        public void RelativeUrlWithBaseUrlOverridesPortWhenHostIsSpecified()
        {
            var baseUrl = new Url("http://localhost:12345/account/login");
            var relative = "https://hosted-domain.com/signin";
            var url = new Url(baseUrl, relative);
            Assert.AreEqual("https://hosted-domain.com/signin", url.ToString());
        }

        [Test]
        public void RelativeUrlWithBaseUrlDoesNotOverridePortIfNoHostIsSpecified()
        {
            var baseUrl = new Url("http://localhost:12345/account/login");
            var relative = "/signin";
            var url = new Url(baseUrl, relative);
            Assert.AreEqual("http://localhost:12345/signin", url.ToString());
        }
    }
}
