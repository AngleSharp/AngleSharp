using AngleSharp.Css;
using AngleSharp.Css.MediaFeatures;
using AngleSharp.DOM.Css;
using NUnit.Framework;

namespace UnitTests.Css
{
    [TestFixture]
    public class CssMediaFeaturesTests
    {
        [Test]
        public void CssMediaFeatureFactory()
        {
            var aspectRatio = MediaFeatureFactory.Create(FeatureNames.AspectRatio);
            Assert.IsNotNull(aspectRatio);
            Assert.IsInstanceOf<AspectRatioMediaFeature>(aspectRatio);
            Assert.IsFalse(aspectRatio.IsMaximum);
            Assert.IsFalse(aspectRatio.IsMinimum);

            var colorIndex = MediaFeatureFactory.Create(FeatureNames.ColorIndex);
            Assert.IsNotNull(colorIndex);
            Assert.IsInstanceOf<ColorIndexMediaFeature>(colorIndex);
            Assert.IsFalse(colorIndex.IsMaximum);
            Assert.IsFalse(colorIndex.IsMinimum);

            var deviceWidth = MediaFeatureFactory.Create(FeatureNames.DeviceWidth);
            Assert.IsNotNull(deviceWidth);
            Assert.IsInstanceOf<DeviceWidthMediaFeature>(deviceWidth);
            Assert.IsFalse(deviceWidth.IsMaximum);
            Assert.IsFalse(deviceWidth.IsMinimum);

            var monochrome = MediaFeatureFactory.Create(FeatureNames.MaxMonochrome);
            Assert.IsNotNull(monochrome);
            Assert.IsInstanceOf<MonochromeMediaFeature>(monochrome);
            Assert.IsTrue(monochrome.IsMaximum);
            Assert.IsFalse(monochrome.IsMinimum);

            var illegal = MediaFeatureFactory.Create("illegal");
            Assert.IsNull(illegal);
        }
    }
}
