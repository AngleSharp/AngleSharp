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

        [TestMethod]
        public void ColorMixBlackOnWhite50Percent()
        {
            var color1 = HtmlColor.Black;
            var color2 = HtmlColor.White;
            var mix = HtmlColor.Mix(0.5, color1, color2);
            Assert.AreEqual(HtmlColor.FromRgb(127, 127, 127), mix);
        }

        [TestMethod]
        public void ColorMixRedOnWhite75Percent()
        {
            var color1 = HtmlColor.Red;
            var color2 = HtmlColor.White;
            var mix = HtmlColor.Mix(0.75, color1, color2);
            Assert.AreEqual(HtmlColor.FromRgb(255, 63, 63), mix);
        }

        [TestMethod]
        public void ColorMixBlueOnWhite10Percent()
        {
            var color1 = HtmlColor.Blue;
            var color2 = HtmlColor.White;
            var mix = HtmlColor.Mix(0.1, color1, color2);
            Assert.AreEqual(HtmlColor.FromRgb(229, 229, 255), mix);
        }

        [TestMethod]
        public void ColorMixGreenOnRed30Percent()
        {
            var color1 = HtmlColor.Green;
            var color2 = HtmlColor.Red;
            var mix = HtmlColor.Mix(0.3, color1, color2);
            Assert.AreEqual(HtmlColor.FromRgb(178, 76, 0), mix);
        }

        [TestMethod]
        public void ColorMixWhiteOnBlack20Percent()
        {
            var color1 = HtmlColor.White;
            var color2 = HtmlColor.Black;
            var mix = HtmlColor.Mix(0.2, color1, color2);
            Assert.AreEqual(HtmlColor.FromRgb(51, 51, 51), mix);
        }
    }
}
