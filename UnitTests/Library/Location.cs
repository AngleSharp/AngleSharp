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
            Assert.IsTrue(location.IsRelative);
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

        // /absolute/resource.jpg
        // index.html
        // index.php?id=5
        // default.aspx?word#first
        // HTTP://example.com.:%38%30/%70a%74%68?a=%31#1%323
        // ldap://[2001:db8::7]/c=GB?objectClass?one
        // mailto:John.Doe@example.com
        // mailto:?to=addr1@an.example,addr2@an.example
        // news:comp.infosystems.www.servers.unix
        // tel:+1-816-555-1212
        // telnet://192.0.2.16:80/
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
