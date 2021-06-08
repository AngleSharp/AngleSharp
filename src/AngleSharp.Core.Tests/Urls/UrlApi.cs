namespace AngleSharp.Core.Tests.Urls
{
    using AngleSharp.Dom;
    using NUnit.Framework;

    [TestFixture]
    public class UrlApiTests
    {
        [Test]
        public void UrlSearchWithoutQueryIsEmpty()
        {
            var url = new Url("https://florian-rappl.de/foo/bar");
            Assert.AreEqual("", url.Search);
            Assert.AreEqual(null, url.Query);
        }

        [Test]
        public void UrlSearchWithQueryIsNotEmpty()
        {
            var url = new Url("https://florian-rappl.de/foo/bar?qxz=baz");
            Assert.AreEqual("?qxz=baz", url.Search);
            Assert.AreEqual("qxz=baz", url.Query);
        }

        [Test]
        public void UrlHashhWithoutFragmentIsEmpty()
        {
            var url = new Url("https://florian-rappl.de/foo/bar");
            Assert.AreEqual("", url.Hash);
            Assert.AreEqual(null, url.Fragment);
        }

        [Test]
        public void UrlHashhWithFragmentIsNotEmpty()
        {
            var url = new Url("https://florian-rappl.de/foo/bar#baz");
            Assert.AreEqual("#baz", url.Hash);
            Assert.AreEqual("baz", url.Fragment);
        }

        [Test]
        public void UrlPathnameIncludesSlash()
        {
            var url = new Url("https://florian-rappl.de/foo/bar");
            Assert.AreEqual("/foo/bar", url.PathName);
            Assert.AreEqual("foo/bar", url.Path);
        }

        [Test]
        public void UrlPathnameIsNeverEmpty()
        {
            var url = new Url("https://florian-rappl.de");
            Assert.AreEqual("/", url.PathName);
            Assert.AreEqual("", url.Path);
        }

        [Test]
        public void UrlUsernameAndPasswordAreEmptyIfNotGiven()
        {
            var url = new Url("https://florian-rappl.de/foo/bar");
            Assert.AreEqual("", url.UserName);
            Assert.AreEqual("", url.Password);
        }

        [Test]
        public void UrlParamsAreLive()
        {
            var url = new Url("https://florian-rappl.de?qxz=bar");
            Assert.AreEqual("bar", url.SearchParams.Get("qxz"));
            Assert.AreEqual(true, url.SearchParams.Has("qxz"));
            Assert.AreEqual(null, url.SearchParams.Get("foo"));
            url.Query = "foo=bar";
            Assert.AreEqual(null, url.SearchParams.Get("qxz"));
            Assert.AreEqual(false, url.SearchParams.Has("qxz"));
            Assert.AreEqual("bar", url.SearchParams.Get("foo"));
            Assert.AreEqual("foo=bar", url.Query);
        }

        [Test]
        public void UrlParamsAreConnectedWhenAppend()
        {
            var url = new Url("https://florian-rappl.de?qxz=bar");
            Assert.AreEqual("qxz=bar", url.Query);
            url.SearchParams.Append("foo", "bar");
            Assert.AreEqual("qxz=bar&foo=bar", url.Query);
        }

        [Test]
        public void UrlParamsAreConnectedWhenDelete()
        {
            var url = new Url("https://florian-rappl.de?qxz=bar");
            Assert.AreEqual("qxz=bar", url.Query);
            url.SearchParams.Delete("qxz");
            Assert.AreEqual("", url.Query);
        }

        [Test]
        public void UrlParamsResolveValuesDecoded()
        {
            var url = new Url("https://florian-rappl.de?qxz=%20foo%20yo");
            Assert.AreEqual(" foo yo", url.SearchParams.Get("qxz"));
            Assert.AreEqual("?qxz=%20foo%20yo", url.Search);
            Assert.AreEqual("qxz=%20foo%20yo", url.SearchParams.ToString());
        }

        [Test]
        public void UrlParamsResolveValuesDecodedAlsoWhenAdded()
        {
            var url = new Url("https://florian-rappl.de?qxz=%20foo%20yo");
            url.SearchParams.Set("qxz", "foo");
            Assert.AreEqual("foo", url.SearchParams.Get("qxz"));
            url.SearchParams.Set("bar", "crazy / shit ?");
            Assert.AreEqual("?qxz=foo&bar=crazy%20%2F%20shit%20%3F", url.Search);
            Assert.AreEqual("crazy / shit ?", url.SearchParams.Get("bar"));
            Assert.AreEqual("qxz=foo&bar=crazy%20%2F%20shit%20%3F", url.SearchParams.ToString());
        }
    }
}
