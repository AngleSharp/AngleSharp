namespace AngleSharp.Core.Tests.Urls
{
    using AngleSharp.Dom;
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
        public void UrlWithSpecialCharacterUnderscoreDomain()
        {
            var address = "http://example_domain.com/image.jpg";
            var result = new Url(address);
            Assert.IsFalse(result.IsInvalid);
            Assert.AreEqual(address, result.Href);
            Assert.AreEqual("example_domain.com", result.HostName);
        }

        [Test]
        public void UrlWithSpecialCharacterUnderscoreSubDomain()
        {
            var address = "https://loony_picture.dirty.ru/";
            var result = new Url(address);
            Assert.IsFalse(result.IsInvalid);
            Assert.AreEqual(address, result.Href);
            Assert.AreEqual("loony_picture.dirty.ru", result.HostName);
        }

        [Test]
        public void ExtendUrlWithPortWithAbsoluteUrlWithoutPortShouldHaveStandardPort()
        {
            var baseAddress = new Url("https://localhost:5000/foo");
            var newAddress = "http://example.com/bar";
            var result = new Url(baseAddress, newAddress);
            Assert.AreEqual("", result.Port);
            Assert.AreEqual(newAddress, result.Href);
        }

        [Test]
        public void ExtendUrlWithPortWithRelativeUrlWithoutSchemeAndPortShouldHaveStandardPort()
        {
            var baseAddress = new Url("https://localhost:5000/foo");
            var newAddress = "//example.com/bar";
            var result = new Url(baseAddress, newAddress);
            Assert.AreEqual("", result.Port);
            Assert.AreEqual("https:" + newAddress, result.Href);
        }

        [Test]
        public void ExtendUrlWithPortWithRelativeUrlShouldHaveThatPort()
        {
            var baseAddress = new Url("https://localhost:5000/foo");
            var newAddress = "/bar";
            var result = new Url(baseAddress, newAddress);
            Assert.AreEqual("5000", result.Port);
            Assert.AreEqual("https://localhost:5000" + newAddress, result.Href);
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

        [Test]
        public void FragmentWithBaseUrlDoesIncludeUsernamePasswordQuery()
        {
            var baseUrl = new Url("http://username:password@localhost:12345/account/login?name=value#fragment");
            var relative = "#newfragment";
            var url = new Url(baseUrl, relative);
            Assert.AreEqual("http://username:password@localhost:12345/account/login?name=value#newfragment", url.ToString());
        }

        [Test]
        public void NoIndexOutOfRangeExceptionParseSchemeIssue711()
        {
            var baseUrl = new Url("http://some.domain.com");
            var relative = "http:";
            var url = new Url(baseUrl, relative);
            Assert.IsTrue(url.IsInvalid);
            Assert.AreEqual("http://some.domain.com/", url.ToString());
        }

        [Test]
        public void PunycodeSquareReplacement_Issue797_Valid()
        {
            var url = new Url("http://ec².com");
            Assert.IsFalse(url.IsInvalid);
            Assert.AreEqual("http://ec2.com/", url.ToString());
        }

        [Test]
        public void PunycodeSquareReplacement_Issue797_Invalid()
        {
            var url = new Url("http://www.example.c؀om/");
            Assert.IsTrue(url.IsInvalid);
        }

        [Test]
        public void PunycodeSquareReplacement_Issue797_InvalidAfterMapping()
        {
            var url = new Url("http://www.examp？le.com/");
            Assert.IsTrue(url.IsInvalid);
        }

        [Test]
        public void InvalidRelativeUrlAsDifferentProtocolScheme()
        {
            var baseUrl = new Url("http://some.domain.com");
            var relative = "https:";
            var url = new Url(baseUrl, relative);
            Assert.IsFalse(url.IsInvalid);
            Assert.AreEqual("https:///", url.ToString());
        }

        [TestCase("http://localhost:12345/signin")]
        [TestCase("https://loony_picture.dirty.ru/")]
        [TestCase("http://www.google.de/some-path?a=b#header")]
        public void SameUrlsHaveSameHashCode(string url)
        {
            var url1 = new Url(url);
            var url2 = new Url(url);
            Assert.AreEqual(url1.GetHashCode(), url2.GetHashCode());
        }
    }
}
