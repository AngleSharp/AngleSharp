using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngleSharp;
using AngleSharp.DOM.Css;

namespace UnitTests
{
    [TestClass]
    public class Color
    {
        [TestMethod]
        public void ColorInvalidHexDigitString()
        {
            CSSColor hc;
            var color = "BCDEFG";
            var result = CSSColor.TryFromHex(color, out hc);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ColorInvalidLengthString()
        {
            CSSColor hc;
            var color = "abcd";
            var result = CSSColor.TryFromHex(color, out hc);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ColorValidLengthShortString()
        {
            CSSColor hc;
            var color = "fff";
            var result = CSSColor.TryFromHex(color, out hc);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ColorValidLengthLongString()
        {
            CSSColor hc;
            var color = "fffabc";
            var result = CSSColor.TryFromHex(color, out hc);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ColorWhiteShortString()
        {
            var color = "fff";
            var result = CSSColor.FromHex(color);
            Assert.AreEqual(CSSColor.FromRgb(255, 255, 255), result);
        }

        [TestMethod]
        public void ColorRedShortString()
        {
            var color = "f00";
            var result = CSSColor.FromHex(color);
            Assert.AreEqual(CSSColor.FromRgb(255, 0, 0), result);
        }

        [TestMethod]
        public void ColorFromRedName()
        {
            var color = "red";
            var result = CSSColor.FromName(color);
            Assert.AreEqual(CSSColor.Red, result);
        }

        [TestMethod]
        public void ColorFromWhiteName()
        {
            var color = "white";
            var result = CSSColor.FromName(color);
            Assert.AreEqual(CSSColor.White, result);
        }

        [TestMethod]
        public void ColorFromUnknownName()
        {
            var color = "bla";
            var result = CSSColor.FromName(color);
            Assert.AreEqual(CSSColor.Transparent, result);
        }

        [TestMethod]
        public void ColorMixedLongString()
        {
            var color = "facc36";
            var result = CSSColor.FromHex(color);
            Assert.AreEqual(CSSColor.FromRgb(250, 204, 54), result);
        }

        [TestMethod]
        public void ColorMixBlackOnWhite50Percent()
        {
            var color1 = CSSColor.Black;
            var color2 = CSSColor.White;
            var mix = CSSColor.Mix(0.5, color1, color2);
            Assert.AreEqual(CSSColor.FromRgb(127, 127, 127), mix);
        }

        [TestMethod]
        public void ColorMixRedOnWhite75Percent()
        {
            var color1 = CSSColor.Red;
            var color2 = CSSColor.White;
            var mix = CSSColor.Mix(0.75, color1, color2);
            Assert.AreEqual(CSSColor.FromRgb(255, 63, 63), mix);
        }

        [TestMethod]
        public void ColorMixBlueOnWhite10Percent()
        {
            var color1 = CSSColor.Blue;
            var color2 = CSSColor.White;
            var mix = CSSColor.Mix(0.1, color1, color2);
            Assert.AreEqual(CSSColor.FromRgb(229, 229, 255), mix);
        }

        [TestMethod]
        public void ColorMixGreenOnRed30Percent()
        {
            var color1 = CSSColor.Green;
            var color2 = CSSColor.Red;
            var mix = CSSColor.Mix(0.3, color1, color2);
            Assert.AreEqual(CSSColor.FromRgb(178, 76, 0), mix);
        }

        [TestMethod]
        public void ColorMixWhiteOnBlack20Percent()
        {
            var color1 = CSSColor.White;
            var color2 = CSSColor.Black;
            var mix = CSSColor.Mix(0.2, color1, color2);
            Assert.AreEqual(CSSColor.FromRgb(51, 51, 51), mix);
        }

        [TestMethod]
        public void ColorHslBlackMixed()
        {
            var color = CSSColor.FromHsl(0, 1, 0);
            Assert.AreEqual(CSSColor.Black, color);
        }

        [TestMethod]
        public void ColorHslBlackMixed1()
        {
            var color = CSSColor.FromHsl(0, 1, 0);
            Assert.AreEqual(CSSColor.Black, color);
        }

        [TestMethod]
        public void ColorHslBlackMixed2()
        {
            var color = CSSColor.FromHsl(0.5f, 1, 0);
            Assert.AreEqual(CSSColor.Black, color);
        }

        [TestMethod]
        public void ColorHslRedPure()
        {
            var color = CSSColor.FromHsl(0, 1, 0.5f);
            Assert.AreEqual(CSSColor.Red, color);
        }

        [TestMethod]
        public void ColorHslGreenPure()
        {
            var color = CSSColor.FromHsl(1f / 3f, 1, 0.5f);
            Assert.AreEqual(CSSColor.Green, color);
        }

        [TestMethod]
        public void ColorHslBluePure()
        {
            var color = CSSColor.FromHsl(2f / 3f, 1, 0.5f);
            Assert.AreEqual(CSSColor.Blue, color);
        }

        [TestMethod]
        public void ColorHslBlackPure()
        {
            var color = CSSColor.FromHsl(0, 0, 0);
            Assert.AreEqual(CSSColor.Black, color);
        }

        [TestMethod]
        public void ColorHslMagentaPure()
        {
            var color = CSSColor.FromHsl(300f / 360f, 1, 0.5f);
            Assert.AreEqual(CSSColor.Magenta, color);
        }

        [TestMethod]
        public void ColorHslYellowGreenMixed()
        {
            var color = CSSColor.FromHsl(1f / 4f, 0.75f, 0.63f);
            Assert.AreEqual(CSSColor.FromRgb(161, 231, 90), color);
        }

        [TestMethod]
        public void ColorHslGrayBlueMixed()
        {
            var color = CSSColor.FromHsl(210f / 360f, 0.25f, 0.25f);
            Assert.AreEqual(CSSColor.FromRgb(48, 64, 80), color);
        }
    }
}
