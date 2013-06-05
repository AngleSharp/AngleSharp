using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngleSharp;

namespace UnitTests
{
    [TestClass]
    public class Color
    {
        [TestMethod]
        public void ColorInvalidHexDigitString()
        {
            HtmlColor hc;
            var color = "BCDEFG";
            var result = HtmlColor.TryFromHex(color, out hc);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ColorInvalidLengthString()
        {
            HtmlColor hc;
            var color = "abcd";
            var result = HtmlColor.TryFromHex(color, out hc);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ColorValidLengthShortString()
        {
            HtmlColor hc;
            var color = "fff";
            var result = HtmlColor.TryFromHex(color, out hc);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ColorValidLengthLongString()
        {
            HtmlColor hc;
            var color = "fffabc";
            var result = HtmlColor.TryFromHex(color, out hc);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ColorWhiteShortString()
        {
            var color = "fff";
            var result = HtmlColor.FromHex(color);
            Assert.AreEqual(HtmlColor.FromRgb(255, 255, 255), result);
        }

        [TestMethod]
        public void ColorRedShortString()
        {
            var color = "f00";
            var result = HtmlColor.FromHex(color);
            Assert.AreEqual(HtmlColor.FromRgb(255, 0, 0), result);
        }

        [TestMethod]
        public void ColorMixedLongString()
        {
            var color = "facc36";
            var result = HtmlColor.FromHex(color);
            Assert.AreEqual(HtmlColor.FromRgb(250, 204, 54), result);
        }
    }
}
