using AngleSharp.Dom.Css;
using NUnit.Framework;

namespace AngleSharp.Core.Tests
{
    [TestFixture]
    public class ColorTests
    {
        [Test]
        public void ColorInvalidHexDigitString()
        {
            Color hc;
            var color = "BCDEFG";
            var result = Color.TryFromHex(color, out hc);
            Assert.IsFalse(result);
        }

        [Test]
        public void ColorInvalidLengthString()
        {
            Color hc;
            var color = "abcd";
            var result = Color.TryFromHex(color, out hc);
            Assert.IsFalse(result);
        }

        [Test]
        public void ColorValidLengthShortString()
        {
            Color hc;
            var color = "fff";
            var result = Color.TryFromHex(color, out hc);
            Assert.IsTrue(result);
        }

        [Test]
        public void ColorValidLengthLongString()
        {
            Color hc;
            var color = "fffabc";
            var result = Color.TryFromHex(color, out hc);
            Assert.IsTrue(result);
        }

        [Test]
        public void ColorWhiteShortString()
        {
            var color = "fff";
            var result = Color.FromHex(color);
            Assert.AreEqual(Color.FromRgb(255, 255, 255), result);
        }

        [Test]
        public void ColorRedShortString()
        {
            var color = "f00";
            var result = Color.FromHex(color);
            Assert.AreEqual(Color.FromRgb(255, 0, 0), result);
        }

        [Test]
        public void ColorFromRedName()
        {
            var color = "red";
            var result = Color.FromName(color);
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(Color.Red, result);
        }

        [Test]
        public void ColorFromWhiteName()
        {
            var color = "white";
            var result = Color.FromName(color);
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(Color.White, result);
        }

        [Test]
        public void ColorFromUnknownName()
        {
            var color = "bla";
            var result = Color.FromName(color);
            Assert.IsFalse(result.HasValue);
        }

        [Test]
        public void ColorMixedLongString()
        {
            var color = "facc36";
            var result = Color.FromHex(color);
            Assert.AreEqual(Color.FromRgb(250, 204, 54), result);
        }

        [Test]
        public void ColorMixBlackOnWhite50Percent()
        {
            var color1 = Color.Black;
            var color2 = Color.White;
            var mix = Color.Mix(0.5, color1, color2);
            Assert.AreEqual(Color.FromRgb(127, 127, 127), mix);
        }

        [Test]
        public void ColorMixRedOnWhite75Percent()
        {
            var color1 = Color.Red;
            var color2 = Color.White;
            var mix = Color.Mix(0.75, color1, color2);
            Assert.AreEqual(Color.FromRgb(255, 63, 63), mix);
        }

        [Test]
        public void ColorMixBlueOnWhite10Percent()
        {
            var color1 = Color.Blue;
            var color2 = Color.White;
            var mix = Color.Mix(0.1, color1, color2);
            Assert.AreEqual(Color.FromRgb(229, 229, 255), mix);
        }

        [Test]
        public void ColorMixGreenOnRed30Percent()
        {
            var color1 = Color.PureGreen;
            var color2 = Color.Red;
            var mix = Color.Mix(0.3, color1, color2);
            Assert.AreEqual(Color.FromRgb(178, 76, 0), mix);
        }

        [Test]
        public void ColorMixWhiteOnBlack20Percent()
        {
            var color1 = Color.White;
            var color2 = Color.Black;
            var mix = Color.Mix(0.2, color1, color2);
            Assert.AreEqual(Color.FromRgb(51, 51, 51), mix);
        }

        [Test]
        public void ColorHslBlackMixed()
        {
            var color = Color.FromHsl(0, 1, 0);
            Assert.AreEqual(Color.Black, color);
        }

        [Test]
        public void ColorHslBlackMixed1()
        {
            var color = Color.FromHsl(0, 1, 0);
            Assert.AreEqual(Color.Black, color);
        }

        [Test]
        public void ColorHslBlackMixed2()
        {
            var color = Color.FromHsl(0.5f, 1, 0);
            Assert.AreEqual(Color.Black, color);
        }

        [Test]
        public void ColorHslRedPure()
        {
            var color = Color.FromHsl(0, 1, 0.5f);
            Assert.AreEqual(Color.Red, color);
        }

        [Test]
        public void ColorHslGreenPure()
        {
            var color = Color.FromHsl(1f / 3f, 1, 0.5f);
            Assert.AreEqual(Color.PureGreen, color);
        }

        [Test]
        public void ColorHslBluePure()
        {
            var color = Color.FromHsl(2f / 3f, 1, 0.5f);
            Assert.AreEqual(Color.Blue, color);
        }

        [Test]
        public void ColorHslBlackPure()
        {
            var color = Color.FromHsl(0, 0, 0);
            Assert.AreEqual(Color.Black, color);
        }

        [Test]
        public void ColorHslMagentaPure()
        {
            var color = Color.FromHsl(300f / 360f, 1, 0.5f);
            Assert.AreEqual(Color.Magenta, color);
        }

        [Test]
        public void ColorHslYellowGreenMixed()
        {
            var color = Color.FromHsl(1f / 4f, 0.75f, 0.63f);
            Assert.AreEqual(Color.FromRgb(161, 231, 90), color);
        }

        [Test]
        public void ColorHslGrayBlueMixed()
        {
            var color = Color.FromHsl(210f / 360f, 0.25f, 0.25f);
            Assert.AreEqual(Color.FromRgb(48, 64, 80), color);
        }
    }
}
