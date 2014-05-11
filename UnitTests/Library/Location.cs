using AngleSharp.DOM;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class LocationTests
    {
        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
        public void AbsoluteLocationIpAddressWithPortAndTelnetScheme()
        {
            var url = "telnet://192.0.2.16:80/";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("/", location.PathName);
            Assert.AreEqual("telnet:", location.Protocol);
            Assert.AreEqual("//192.0.2.16:80/", location.Data);
            Assert.AreEqual("", location.Host);
            Assert.AreEqual("", location.HostName);
            Assert.AreEqual(url, location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [TestMethod]
        public void AbsoluteLocationTelephonePseudo()
        {
            var url = "tel:+1-816-555-1212";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("/", location.PathName);
            Assert.AreEqual("tel:", location.Protocol);
            Assert.AreEqual("+1-816-555-1212", location.Data);
            Assert.AreEqual("", location.Host);
            Assert.AreEqual(url, location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [TestMethod]
        public void AbsoluteLocationNewProtocol()
        {
            var url = "news:comp.infosystems.www.servers.unix";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("/", location.PathName);
            Assert.AreEqual("news:", location.Protocol);
            Assert.AreEqual("comp.infosystems.www.servers.unix", location.Data);
            Assert.AreEqual("", location.Host);
            Assert.AreEqual(url, location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [TestMethod]
        public void AbsoluteLocationMailProtocolQueryMultiple()
        {
            var url = "mailto:?to=addr1@an.example,addr2@an.example";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("?to=addr1@an.example,addr2@an.example", location.Search);
            Assert.AreEqual("/", location.PathName);
            Assert.AreEqual("mailto:", location.Protocol);
            Assert.AreEqual("", location.Data);
            Assert.AreEqual("", location.Host);
            Assert.AreEqual(url, location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [TestMethod]
        public void AbsoluteLocationMailProtocolSimple()
        {
            var url = "mailto:John.Doe@example.com";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("", location.Search);
            Assert.AreEqual("/", location.PathName);
            Assert.AreEqual("mailto:", location.Protocol);
            Assert.AreEqual("John.Doe@example.com", location.Data);
            Assert.AreEqual("", location.Host);
            Assert.AreEqual(url, location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [TestMethod]
        public void AbsoluteLocationLdapWithIpV6Address()
        {
            var url = "ldap://[2001:db8::7]/c=GB?objectClass?one";
            var location = new Location(url);
            Assert.AreEqual("", location.Hash);
            Assert.AreEqual("", location.Port);
            Assert.AreEqual("?objectClass?one", location.Search);
            Assert.AreEqual("/", location.PathName);
            Assert.AreEqual("ldap:", location.Protocol);
            Assert.AreEqual("", location.Host);
            Assert.AreEqual("//[2001:db8::7]/c=GB", location.Data);
            Assert.AreEqual(url, location.Href);
            Assert.IsFalse(location.IsRelative);
        }

        [TestMethod]
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

        // urn:oasis:names:specification:docbook:dtd:xml:4.1.2
        // http://example.com
        // http://www.w3.org/pub/WWW/TheProject.html
        // http://[3ffe:1900:4545:3:200:f8ff:fe21:67cf]/
        // http://[::1]/
        // http://example.com/?
        // http://example.com/#
        // http://example.com/?#
        // http://example.com?
        // http://example.com#
        // http://example.com?#
        // http://example.com/~smith/
        // http://example.com/%E8
        // http://example.com/%25C3%2587
        // http://example.com/?q=string
        // http://example.com:80/
        // http://user:pass@example.com/path/to/
        // http://example.com:%38%30/
        // http://example.com/%2E/
        // http://example.com/../..
        // /a/b/c/./../../g
        // mid/content=5/../6
        // http://www.example.com///../
        // http://example.com/file.txt;parameter
        // tag:example.com,2006-08-18:/path/to/something
        // view-source:http://example.com/
        // http://user:pass@example.com/path/to/resource?query=x#fragment
        // http://example.com/search?q=Q%26A
        // http://example.com/?&&x=b
        // http://example.com/?q=a&&x=b
        // http://:@example.com
        // //example.com/
        // ?one=1&two=2&three=3
        // http://www.詹姆斯.com/atomtests/iri/詹.html
        // http://a/b/c/d;p?q
        // file:///c:/windows/My%20Documents%20100%20/foo.txt
        // c:\\windows\\My Documents 100%20\\foo.txt
        // file:c:\\windows\\My Documents 100%20\\foo.txt
        // file:///c|/windows/My%20Documents%20100%20/foo.txt
    }
}
