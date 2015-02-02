using AngleSharp;
using AngleSharp.Css;
using AngleSharp.Css.MediaFeatures;
using AngleSharp.Dom.Css;
using NUnit.Framework;
using System.Collections.Generic;

namespace AngleSharp.Core.Tests.Css
{
    [TestFixture]
    public class CssMediaFeaturesTests
    {
        [Test]
        public void CssMediaFeatureFactory()
        {
            var aspectRatio = Factory.MediaFeatures.Create(FeatureNames.AspectRatio);
            Assert.IsNotNull(aspectRatio);
            Assert.IsInstanceOf<AspectRatioMediaFeature>(aspectRatio);
            Assert.IsFalse(aspectRatio.IsMaximum);
            Assert.IsFalse(aspectRatio.IsMinimum);

            var colorIndex = Factory.MediaFeatures.Create(FeatureNames.ColorIndex);
            Assert.IsNotNull(colorIndex);
            Assert.IsInstanceOf<ColorIndexMediaFeature>(colorIndex);
            Assert.IsFalse(colorIndex.IsMaximum);
            Assert.IsFalse(colorIndex.IsMinimum);

            var deviceWidth = Factory.MediaFeatures.Create(FeatureNames.DeviceWidth);
            Assert.IsNotNull(deviceWidth);
            Assert.IsInstanceOf<DeviceWidthMediaFeature>(deviceWidth);
            Assert.IsFalse(deviceWidth.IsMaximum);
            Assert.IsFalse(deviceWidth.IsMinimum);

            var monochrome = Factory.MediaFeatures.Create(FeatureNames.MaxMonochrome);
            Assert.IsNotNull(monochrome);
            Assert.IsInstanceOf<MonochromeMediaFeature>(monochrome);
            Assert.IsTrue(monochrome.IsMaximum);
            Assert.IsFalse(monochrome.IsMinimum);

            var illegal = Factory.MediaFeatures.Create("illegal");
            Assert.IsNull(illegal);
        }

        [Test]
        public void CssMediaWidthValidation()
        {
            var width = new WidthMediaFeature(FeatureNames.Width);
            var check = width.TrySetValue(new Length(100, Length.Unit.Px));
            var valid = width.Validate(new RenderDevice(100, 0));
            var invalid = width.Validate(new RenderDevice(0, 0));
            Assert.IsTrue(check);
            Assert.IsTrue(valid);
            Assert.IsFalse(invalid);
        }

        [Test]
        public void CssMediaMaxHeightValidation()
        {
            var height = new HeightMediaFeature(FeatureNames.MaxHeight);
            var check = height.TrySetValue(new Length(100, Length.Unit.Px));
            var valid = height.Validate(new RenderDevice(0, 99));
            var invalid = height.Validate(new RenderDevice(0, 101));
            Assert.IsTrue(check);
            Assert.IsTrue(valid);
            Assert.IsFalse(invalid);
        }

        [Test]
        public void CssMediaMinDeviceWidthValidation()
        {
            var devwidth = new DeviceWidthMediaFeature(FeatureNames.MinDeviceWidth);
            var check = devwidth.TrySetValue(new Length(100, Length.Unit.Px));
            var valid = devwidth.Validate(new RenderDevice(101, 0));
            var invalid = devwidth.Validate(new RenderDevice(99, 0));
            Assert.IsTrue(check);
            Assert.IsTrue(valid);
            Assert.IsFalse(invalid);
        }

        [Test]
        public void CssMediaAspectRatio()
        {
            var ratio = new AspectRatioMediaFeature(FeatureNames.AspectRatio);
            var check = ratio.TrySetValue(new CssValueList(new List<ICssValue>(new ICssValue[] {
                new Number(1f, Number.Unit.Integer),
                CssValue.Delimiter,
                new Number(1f, Number.Unit.Integer)
            })));
            var valid = ratio.Validate(new RenderDevice(100, 100));
            var invalid = ratio.Validate(new RenderDevice(16, 9));
            Assert.IsTrue(check);
            Assert.IsTrue(valid);
            Assert.IsFalse(invalid);
        }
    }
}
