namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Dom;
    using NUnit.Framework;

    [TestFixture]
    public class LocationTests
    {
        [Test]
        public void AbsoluteLocationWithoutPathGoogle()
        {
            var url = "http://www.google.de";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("/", location.PathName);
            Assert.AreEqual("http:", location.Protocol);
            Assert.AreEqual("www.google.de", location.Host);
            Assert.AreEqual(url + "/", location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void AbsoluteLocationWithPathGoogle()
        {
            var url = "https://www.google.de/mypath";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("/mypath", location.PathName);
            Assert.AreEqual("https:", location.Protocol);
            Assert.AreEqual("www.google.de", location.Host);
            Assert.AreEqual(url, location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void ProtocolRelativeLocationWithPathGoogleApis()
        {
            var url = "//ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.js";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("/ajax/libs/jquery/1.4.2/jquery.js", location.PathName);
            Assert.AreEqual("", location.Protocol);
            Assert.AreEqual("ajax.googleapis.com", location.Host);
            Assert.AreEqual(url, location.Href);
            Assert.IsTrue(location.IsRelative);
        }

        [Test]
        public void AbsoluteLocationWithPortAndQueryLocalhost()
        {
            var url = "http://localhost:8080/?mytest";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("8080", location.Port);
            Assert.AreEqual("?mytest", location.Search);
            Assert.AreEqual("/", location.PathName);
            Assert.AreEqual("http:", location.Protocol);
            Assert.AreEqual("localhost:8080", location.Host);
            Assert.AreEqual("localhost", location.HostName);
            Assert.AreEqual(url, location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void DataLocationSchemeOnly()
        {
            var url = "data:";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("/", location.PathName);
            Assert.AreEqual("data:", location.Protocol);
            Assert.AreEqual("", location.Host);
            Assert.AreEqual("", location.HostName);
            Assert.AreEqual(url, location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void DataLocationComplete()
        {
            var data = "image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAEcAAAAcCAMAAAAEJ1IZAAAABGdBTUEAALGPC/xhBQAAVAI/VAI/VAI/VAI/VAI/VAI/VAAAA////AI/VRZ0U8AAAAFJ0Uk5TYNV4S2UbgT/Gk6uQt585w2wGXS0zJO2lhGttJK6j4YqZSobH1AAAAAElFTkSuQmCC";
            var scheme = "data:";
            var url = scheme + data;
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual(data, location.PathName);
            Assert.AreEqual(scheme, location.Protocol);
            Assert.AreEqual("", location.Host);
            Assert.AreEqual("", location.HostName);
            Assert.AreEqual(url, location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void AbsoluteLocationUsernamePasswordWithPort()
        {
            var url = "http://user:password@example.com:8080";
            var location = new Location(url);
            Assert.AreEqual("user", location.UserName);
            Assert.AreEqual("password", location.Password);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("8080", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("/", location.PathName);
            Assert.AreEqual("http:", location.Protocol);
            Assert.AreEqual("example.com:8080", location.Host);
            Assert.AreEqual("example.com", location.HostName);
            Assert.AreEqual(url + "/", location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void AbsoluteLocationUsernamePasswordWithPortPathQueryAndFragment()
        {
            var url = "https://user:password@example.com:8080/path?query=value#fragment";
            var location = new Location(url);
            Assert.AreEqual("user", location.UserName);
            Assert.AreEqual("password", location.Password);
            Assert.AreEqual("#fragment", location.Hash);
            Assert.AreEqual("8080", location.Port);
            Assert.AreEqual("?query=value", location.Search);
            Assert.AreEqual("/path", location.PathName);
            Assert.AreEqual("https:", location.Protocol);
            Assert.AreEqual("example.com:8080", location.Host);
            Assert.AreEqual("example.com", location.HostName);
            Assert.AreEqual(url, location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void PathRelativeLocationAbsoluteDirectory()
        {
            var url = "/path";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("/path", location.PathName);
            Assert.AreEqual("", location.Protocol);
            Assert.AreEqual("", location.Host);
            Assert.AreEqual(url.Substring(1), location.Href);
            Assert.IsTrue(location.IsRelative);
        }

        [Test]
        public void QueryRelativeLocation()
        {
            var url = "?query=value";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("?query=value", location.Search);
            Assert.AreEqual("/", location.PathName);
            Assert.AreEqual("", location.Protocol);
            Assert.AreEqual("", location.Host);
            Assert.AreEqual(url, location.Href);
            Assert.IsTrue(location.IsRelative);
        }

        [Test]
        public void PathRelativeLocationRelativeSimple()
        {
            var url = "picture.png";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("/picture.png", location.PathName);
            Assert.AreEqual("", location.Protocol);
            Assert.AreEqual("", location.Host);
            Assert.AreEqual(url, location.Href);
            Assert.IsTrue(location.IsRelative);
        }

        [Test]
        public void PathRelativeLocationAbsoluteFile()
        {
            var url = "/hello.css";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("/hello.css", location.PathName);
            Assert.AreEqual("", location.Protocol);
            Assert.AreEqual("", location.Host);
            Assert.AreEqual(url.Substring(1), location.Href);
            Assert.IsTrue(location.IsRelative);
        }

        [Test]
        public void PathRelativeLocationRelativeDirectoryFile()
        {
            var url = "scripts/js/jquery.js";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("/scripts/js/jquery.js", location.PathName);
            Assert.AreEqual("", location.Protocol);
            Assert.AreEqual("", location.Host);
            Assert.AreEqual(url, location.Href);
            Assert.IsTrue(location.IsRelative);
        }

        [Test]
        public void FragmentRelativeLocation()
        {
            var url = "#example";
            var location = new Location(url);
            Assert.AreEqual("#example", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("/", location.PathName);
            Assert.AreEqual("", location.Protocol);
            Assert.AreEqual("", location.Host);
            Assert.AreEqual(url, location.Href);
            Assert.IsTrue(location.IsRelative);
        }

        [Test]
        public void PathRelativeLocationAbsoluteDirectoryFile()
        {
            var url = "/absolute/resource.jpg";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("/absolute/resource.jpg", location.PathName);
            Assert.AreEqual("", location.Protocol);
            Assert.AreEqual("", location.Host);
            Assert.AreEqual(url.Substring(1), location.Href);
            Assert.IsTrue(location.IsRelative);
        }

        [Test]
        public void PathRelativeLocationRelativeHtml()
        {
            var url = "index.html";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("/index.html", location.PathName);
            Assert.AreEqual("", location.Protocol);
            Assert.AreEqual("", location.Host);
            Assert.AreEqual(url, location.Href);
            Assert.IsTrue(location.IsRelative);
        }

        [Test]
        public void PathRelativeLocationRelativeHtmlWithQuery()
        {
            var url = "index.html?id=5";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("?id=5", location.Search);
            Assert.AreEqual("/index.html", location.PathName);
            Assert.AreEqual("", location.Protocol);
            Assert.AreEqual("", location.Host);
            Assert.AreEqual(url, location.Href);
            Assert.IsTrue(location.IsRelative);
        }

        [Test]
        public void PathRelativeLocationRelativeWithQueryAndFragment()
        {
            var url = "default.aspx?word#first";
            var location = new Location(url);
            Assert.AreEqual("#first", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("?word", location.Search);
            Assert.AreEqual("/default.aspx", location.PathName);
            Assert.AreEqual("", location.Protocol);
            Assert.AreEqual("", location.Host);
            Assert.AreEqual(url, location.Href);
            Assert.IsTrue(location.IsRelative);
        }

        [Test]
        public void AbsoluteLocationIpAddressWithPortAndTelnetScheme()
        {
            var url = "telnet://192.0.2.16:80/";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("telnet:", location.Protocol);
            Assert.AreEqual("//192.0.2.16:80/", location.PathName);
            Assert.AreEqual("", location.Host);
            Assert.AreEqual("", location.HostName);
            Assert.AreEqual(url, location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void AbsoluteLocationTelephonePseudo()
        {
            var url = "tel:+1-816-555-1212";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("tel:", location.Protocol);
            Assert.AreEqual("+1-816-555-1212", location.PathName);
            Assert.AreEqual("", location.Host);
            Assert.AreEqual(url, location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void AbsoluteLocationNewProtocol()
        {
            var url = "news:comp.infosystems.www.servers.unix";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("news:", location.Protocol);
            Assert.AreEqual("comp.infosystems.www.servers.unix", location.PathName);
            Assert.AreEqual("", location.Host);
            Assert.AreEqual(url, location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void AbsoluteLocationMailProtocolQueryMultiple()
        {
            var url = "mailto:?to=addr1@an.example,addr2@an.example";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("?to=addr1@an.example,addr2@an.example", location.Search);
            Assert.AreEqual("/", location.PathName);
            Assert.AreEqual("mailto:", location.Protocol);
            Assert.AreEqual("", location.Host);
            Assert.AreEqual(url, location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void AbsoluteLocationMailProtocolSimple()
        {
            var url = "mailto:John.Doe@example.com";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("mailto:", location.Protocol);
            Assert.AreEqual("John.Doe@example.com", location.PathName);
            Assert.AreEqual("", location.Host);
            Assert.AreEqual(url, location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void AbsoluteLocationLdapWithIpV6Address()
        {
            var url = "ldap://[2001:db8::7]/c=GB?objectClass?one";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("?objectClass?one", location.Search);
            Assert.AreEqual("ldap:", location.Protocol);
            Assert.AreEqual("", location.Host);
            Assert.AreEqual("//[2001:db8::7]/c=GB", location.PathName);
            Assert.AreEqual(url, location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void AbsoluteLocationWithPercentEncodedAndUppercase()
        {
            var url = "HTTP://example.com.:80/%70a%74%68?a=%31#1%323";
            var location = new Location(url);
            Assert.AreEqual("#1%323", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("?a=%31", location.Search);
            Assert.AreEqual("/%70a%74%68", location.PathName);
            Assert.AreEqual("http:", location.Protocol);
            Assert.AreEqual("example.com.", location.Host);
            Assert.AreEqual("http://example.com./%70a%74%68?a=%31#1%323", location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void AbsoluteLocationUrn()
        {
            var url = "urn:oasis:names:specification:docbook:dtd:xml:4.1.2";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("urn:", location.Protocol);
            Assert.AreEqual("", location.Host);
            Assert.AreEqual("oasis:names:specification:docbook:dtd:xml:4.1.2", location.PathName);
            Assert.AreEqual(url, location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void AbsoluteLocationSimpleExample()
        {
            var url = "http://example.com";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("/", location.PathName);
            Assert.AreEqual("http:", location.Protocol);
            Assert.AreEqual("example.com", location.Host);
            Assert.AreEqual(url + "/", location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void AbsoluteLocationIpExample()
        {
            var url = "http://127.0.0.1:8080/mypath";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("8080", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("/mypath", location.PathName);
            Assert.AreEqual("http:", location.Protocol);
            Assert.AreEqual("127.0.0.1:8080", location.Host);
            Assert.AreEqual("127.0.0.1", location.HostName);
            Assert.AreEqual(url, location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void AbsoluteLocationW3Project()
        {
            var url = "http://www.w3.org/pub/WWW/TheProject.html";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("/pub/WWW/TheProject.html", location.PathName);
            Assert.AreEqual("http:", location.Protocol);
            Assert.AreEqual("www.w3.org", location.Host);
            Assert.AreEqual(url, location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void AbsoluteLocationIpV6LongAddress()
        {
            var url = "http://[3ffe:1900:4545:3:200:f8ff:fe21:67cf]/";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("/", location.PathName);
            Assert.AreEqual("http:", location.Protocol);
            Assert.AreEqual("[3ffe:1900:4545:3:200:f8ff:fe21:67cf]", location.Host);
            Assert.AreEqual(url, location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void AbsoluteLocationIpV6AbbrAddress()
        {
            var url = "http://[::1]/";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("/", location.PathName);
            Assert.AreEqual("http:", location.Protocol);
            Assert.AreEqual("[::1]", location.Host);
            Assert.AreEqual(url, location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void AbsoluteLocationExampleQueryEmpty()
        {
            var url = "http://example.com/?";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("/", location.PathName);
            Assert.AreEqual("http:", location.Protocol);
            Assert.AreEqual("example.com", location.Host);
            Assert.AreEqual("http://example.com/?", location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void AbsoluteLocationExampleFragmentEmpty()
        {
            var url = "http://example.com/#";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("/", location.PathName);
            Assert.AreEqual("http:", location.Protocol);
            Assert.AreEqual("example.com", location.Host);
            Assert.AreEqual("http://example.com/#", location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void AbsoluteLocationExampleQueryAndFragmentEmpty()
        {
            var url = "http://example.com/?#";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("/", location.PathName);
            Assert.AreEqual("http:", location.Protocol);
            Assert.AreEqual("example.com", location.Host);
            Assert.AreEqual("http://example.com/?#", location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void AbsoluteLocationExampleNoPathFragmentEmpty()
        {
            var url = "http://example.com#";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("/", location.PathName);
            Assert.AreEqual("http:", location.Protocol);
            Assert.AreEqual("example.com", location.Host);
            Assert.AreEqual("http://example.com/#", location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void AbsoluteLocationExampleNoPathQueryEmpty()
        {
            var url = "http://example.com?";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("/", location.PathName);
            Assert.AreEqual("http:", location.Protocol);
            Assert.AreEqual("example.com", location.Host);
            Assert.AreEqual("http://example.com/?", location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void AbsoluteLocationExampleNoPathQueryAndFragmentEmpty()
        {
            var url = "http://example.com?#";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("/", location.PathName);
            Assert.AreEqual("http:", location.Protocol);
            Assert.AreEqual("example.com", location.Host);
            Assert.AreEqual("http://example.com/?#", location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void AbsoluteLocationExampleTildeDirectory()
        {
            var url = "http://example.com/~smith/";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("/~smith/", location.PathName);
            Assert.AreEqual("http:", location.Protocol);
            Assert.AreEqual("example.com", location.Host);
            Assert.AreEqual(url, location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void AbsoluteLocationGoingUpToRoot()
        {
            var url = "http://example.com/../..";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("/", location.PathName);
            Assert.AreEqual("http:", location.Protocol);
            Assert.AreEqual("example.com", location.Host);
            Assert.AreEqual("http://example.com/", location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void RelativeLocationGoingUpAndDown()
        {
            var url = "/a/b/c/./../../g";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("/a/g", location.PathName);
            Assert.AreEqual("", location.Protocol);
            Assert.AreEqual("", location.Host);
            Assert.AreEqual("a/g", location.Href);
            Assert.IsTrue(location.IsRelative);
        }

        [Test]
        public void AbsoluteLocationOnWindows()
        {
            var url = "file:c:\\windows\\My Documents 100%20\\foo.txt";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("/c:/windows/My%20Documents%20100%20/foo.txt", location.PathName);
            Assert.AreEqual("file:", location.Protocol);
            Assert.AreEqual("", location.Host);
            Assert.AreEqual("file:///c:/windows/My%20Documents%20100%20/foo.txt", location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void AbsoluteLocationPercentPathSingle()
        {
            var url = "https://example.com/%E8";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("/%E8", location.PathName);
            Assert.AreEqual("https:", location.Protocol);
            Assert.AreEqual("example.com", location.Host);
            Assert.AreEqual(url, location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void AbsoluteLocationPercentPathDouble()
        {
            var url = "http://example.com/%25C3%2587";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("/%25C3%2587", location.PathName);
            Assert.AreEqual("http:", location.Protocol);
            Assert.AreEqual("example.com", location.Host);
            Assert.AreEqual(url, location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void AbsoluteLocationQueryString()
        {
            var url = "http://example.com/?q=string";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("?q=string", location.Search);
            Assert.AreEqual("/", location.PathName);
            Assert.AreEqual("http:", location.Protocol);
            Assert.AreEqual("example.com", location.Host);
            Assert.AreEqual(url, location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void AbsoluteLocationPortIgnored()
        {
            var url = "http://example.com:80/";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("/", location.PathName);
            Assert.AreEqual("http:", location.Protocol);
            Assert.AreEqual("example.com", location.Host);
            Assert.AreEqual("http://example.com/", location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void AbsoluteLocationCurrentDirectory()
        {
            var url = "http://example.com/%2E/";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("/", location.PathName);
            Assert.AreEqual("http:", location.Protocol);
            Assert.AreEqual("example.com", location.Host);
            Assert.AreEqual("http://example.com/", location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void RelativeLocationOneUp()
        {
            var url = "mid/content=5/../6";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("/mid/6", location.PathName);
            Assert.AreEqual("", location.Protocol);
            Assert.AreEqual("", location.Host);
            Assert.AreEqual("mid/6", location.Href);
            Assert.IsTrue(location.IsRelative);
        }

        [Test]
        public void AbsoluteLocationOneUp()
        {
            var url = "http://www.example.com///../";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("//", location.PathName);
            Assert.AreEqual("http:", location.Protocol);
            Assert.AreEqual("www.example.com", location.Host);
            Assert.AreEqual("http://www.example.com//", location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void AbsoluteLocationFilenameWithSemicolon()
        {
            var url = "http://example.com/file.txt;parameter";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("/file.txt;parameter", location.PathName);
            Assert.AreEqual("http:", location.Protocol);
            Assert.AreEqual("example.com", location.Host);
            Assert.AreEqual(url, location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void AbsoluteLocationUnknownTagScheme()
        {
            var url = "tag:example.com,2006-08-18:/path/to/something";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("tag:", location.Protocol);
            Assert.AreEqual("", location.Host);
            Assert.AreEqual("example.com,2006-08-18:/path/to/something", location.PathName);
            Assert.AreEqual(url, location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void AbsoluteLocationAuthorityAndPath()
        {
            var url = "http://user:pass@example.com/path/to/";
            var location = new Location(url);
            Assert.AreEqual("user", location.UserName);
            Assert.AreEqual("pass", location.Password);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("/path/to/", location.PathName);
            Assert.AreEqual("http:", location.Protocol);
            Assert.AreEqual("example.com", location.Host);
            Assert.AreEqual(url, location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void AbsoluteLocationViewSourceProtocol()
        {
            var url = "view-source:http://example.com/";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("view-source:", location.Protocol);
            Assert.AreEqual("", location.Host);
            Assert.AreEqual("http://example.com/", location.PathName);
            Assert.AreEqual(url, location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void AbsoluteLocationAuthorityAndPathWithQueryAndFragment()
        {
            var url = "http://user:pass@example.com/path/to/resource?query=x#fragment";
            var location = new Location(url);
            Assert.AreEqual("user", location.UserName);
            Assert.AreEqual("pass", location.Password);
            Assert.AreEqual("#fragment", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("?query=x", location.Search);
            Assert.AreEqual("/path/to/resource", location.PathName);
            Assert.AreEqual("http:", location.Protocol);
            Assert.AreEqual("example.com", location.Host);
            Assert.AreEqual(url, location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void AbsoluteLocationWithQueryAndPercent()
        {
            var url = "http://example.com/search?q=Q%26A";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("?q=Q%26A", location.Search);
            Assert.AreEqual("/search", location.PathName);
            Assert.AreEqual("http:", location.Protocol);
            Assert.AreEqual("example.com", location.Host);
            Assert.AreEqual(url, location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void AbsoluteLocationWithAmpersandQuerySingleValue()
        {
            var url = "http://example.com/?&&x=b";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("?&&x=b", location.Search);
            Assert.AreEqual("/", location.PathName);
            Assert.AreEqual("http:", location.Protocol);
            Assert.AreEqual("example.com", location.Host);
            Assert.AreEqual(url, location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void AbsoluteLocationWithAmpersandQueryTwoValues()
        {
            var url = "http://example.com/?q=a&&x=b";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("?q=a&&x=b", location.Search);
            Assert.AreEqual("/", location.PathName);
            Assert.AreEqual("http:", location.Protocol);
            Assert.AreEqual("example.com", location.Host);
            Assert.AreEqual(url, location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void AbsoluteLocationFileWithBackslashes()
        {
            var url = "file:c:\\windows\\My Documents 100%20\\foo.txt";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("/c:/windows/My%20Documents%20100%20/foo.txt", location.PathName);
            Assert.AreEqual("file:", location.Protocol);
            Assert.AreEqual("", location.Host);
            Assert.AreEqual("file:///c:/windows/My%20Documents%20100%20/foo.txt", location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void AbsoluteLocationFileEverythingFine()
        {
            var url = "file:///c:/windows/My%20Documents%20100%20/foo.txt";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("/c:/windows/My%20Documents%20100%20/foo.txt", location.PathName);
            Assert.AreEqual("file:", location.Protocol);
            Assert.AreEqual("", location.Host);
            Assert.AreEqual(url, location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void AbsoluteLocationFileWithPipe()
        {
            var url = "file:///c|/windows/My%20Documents%20100%20/foo.txt";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("/c:/windows/My%20Documents%20100%20/foo.txt", location.PathName);
            Assert.AreEqual("file:", location.Protocol);
            Assert.AreEqual("", location.Host);
            Assert.AreEqual("file:///c:/windows/My%20Documents%20100%20/foo.txt", location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void AbsoluteLocationAuthorityNothing()
        {
            var url = "http://:@example.com";
            var location = new Location(url);
            Assert.AreEqual("", location.UserName);
            Assert.AreEqual("", location.Password);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("/", location.PathName);
            Assert.AreEqual("http:", location.Protocol);
            Assert.AreEqual("example.com", location.Host);
            Assert.AreEqual("http://:@example.com/", location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void RelativeLocationWithoutProtocol()
        {
            var url = "//example.com/";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("/", location.PathName);
            Assert.AreEqual("", location.Protocol);
            Assert.AreEqual("example.com", location.Host);
            Assert.AreEqual(url, location.Href);
            Assert.IsTrue(location.IsRelative);
        }

        [Test]
        public void RelativeLocationOnlyQuery()
        {
            var url = "?one=1&two=2&three=3";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("?one=1&two=2&three=3", location.Search);
            Assert.AreEqual("/", location.PathName);
            Assert.AreEqual("", location.Protocol);
            Assert.AreEqual("", location.Host);
            Assert.AreEqual(url, location.Href);
            Assert.IsTrue(location.IsRelative);
        }

        [Test]
        public void AbsoluteLocationUnicodeLetters()
        {
            var url = "http://www.詹姆斯.com/atomtests/iri/詹.html";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("/atomtests/iri/%E8%A9%B9.html", location.PathName);
            Assert.AreEqual("http:", location.Protocol);
            Assert.AreEqual("www.詹姆斯.com", location.Host);
            Assert.AreEqual("http://www.詹姆斯.com/atomtests/iri/%E8%A9%B9.html", location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void AbsoluteLocationFtpShortPath()
        {
            var url = "ftp://a/b/c/d;p?q";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("?q", location.Search);
            Assert.AreEqual("/b/c/d;p", location.PathName);
            Assert.AreEqual("ftp:", location.Protocol);
            Assert.AreEqual("a", location.Host);
            Assert.AreEqual(url, location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void RelativeLocationEmpty()
        {
            var url = "";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("/", location.PathName);
            Assert.AreEqual("", location.Protocol);
            Assert.AreEqual("", location.Host);
            Assert.AreEqual(url, location.Href);
            Assert.IsTrue(location.IsRelative);
        }

        [Test]
        public void RelativeLocationSlash()
        {
            var url = "/";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("/", location.PathName);
            Assert.AreEqual("", location.Protocol);
            Assert.AreEqual("", location.Host);
            Assert.AreEqual("", location.Href);
            Assert.IsTrue(location.IsRelative);
        }

        [Test]
        public void AbsoluteLocationNutsUrl()
        {
            var url = "http://example.com/:@-._~!$&'()*+,=;:@-._~!$&'()*+,=:@-._~!$&'()*+,==?/?:@-._~!$'()*+,;=/?:@-._~!$'()*+,;==#/?:@-._~!$&'()*+,;=";
            var location = new Location(url);
            Assert.AreEqual("#/?:@-._~!$&'()*+,;=", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("?/?:@-._~!$'()*+,;=/?:@-._~!$'()*+,;==", location.Search);
            Assert.AreEqual("/:@-._~!$&'()*+,=;:@-._~!$&'()*+,=:@-._~!$&'()*+,==", location.PathName);
            Assert.AreEqual("http:", location.Protocol);
            Assert.AreEqual("example.com", location.Host);
            Assert.AreEqual(url, location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [Test]
        public void AbsoluteLocationDecodedPath()
        {
            var url = "http://example.com/blue%2Fred%3Fand+green";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("/blue%2Fred%3Fand+green", location.PathName);
            Assert.AreEqual("http:", location.Protocol);
            Assert.AreEqual("example.com", location.Host);
            Assert.AreEqual(url, location.Href);
            Assert.IsFalse(location.IsRelative);
        }
    }
}
