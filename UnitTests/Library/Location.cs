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
    }
}
