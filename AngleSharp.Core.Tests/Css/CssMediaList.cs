using System;
using NUnit.Framework;
using AngleSharp.Parser.Css;
using AngleSharp.Dom.Css;

namespace AngleSharp.Core.Tests.Css
{
    [TestFixture]
    public class CssMediaListTests
    {
        [Test]
        public void SimpleScreenMediaList()
        {
            var source = @"@media screen {
    h1 { color: green }
}";
            var sheet = CssParser.ParseStyleSheet(source);
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssMediaRule>(sheet.Rules[0]);
            var media = (CssMediaRule)sheet.Rules[0];
            Assert.AreEqual("screen", media.Media.MediaText);
            var list = media.Media;
            Assert.AreEqual(1, list.Length);
            Assert.AreEqual(1, media.Rules.Length);
        }

        [Test]
        public void MediaListAtIllegal()
        {
            var source = @"@media @screen {
    h1 { color: green }
}";
            var sheet = CssParser.ParseStyleSheet(source);
            Assert.AreEqual(0, sheet.Rules.Length);
        }

        [Test]
        public void MediaListInterrupted()
        {
            var source = @"@media screen; h1 { color: green }";
            var sheet = CssParser.ParseStyleSheet(source);
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<ICssStyleRule>(sheet.Rules[0]);
            var h1 = (ICssStyleRule)sheet.Rules[0];
            Assert.AreEqual("h1", h1.SelectorText);
            var style = h1.Style;
            Assert.AreEqual("green", style.Color);
        }

        [Test]
        public void SimpleScreenTvMediaList()
        {
            var source = @"@media screen,tv {
    h1 { color: green }
}";
            var sheet = CssParser.ParseStyleSheet(source);
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssMediaRule>(sheet.Rules[0]);
            var media = (CssMediaRule)sheet.Rules[0];
            Assert.AreEqual("screen, tv", media.Media.MediaText);
            var list = media.Media;
            Assert.AreEqual(2, list.Length);
            Assert.AreEqual(1, media.Rules.Length);
        }

        [Test]
        public void SimpleScreenTvSpacesMediaList()
        {
            var source = @"@media              screen ,          tv {
    h1 { color: green }
}";
            var sheet = CssParser.ParseStyleSheet(source);
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssMediaRule>(sheet.Rules[0]);
            var media = (CssMediaRule)sheet.Rules[0];
            Assert.AreEqual("screen, tv", media.Media.MediaText);
            var list = media.Media;
            Assert.AreEqual(2, list.Length);
            Assert.AreEqual(1, media.Rules.Length);
        }

        [Test]
        public void OnlyScreenTvMediaList()
        {
            var source = @"@media only screen,tv {
    h1 { color: green }
}";
            var sheet = CssParser.ParseStyleSheet(source);
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssMediaRule>(sheet.Rules[0]);
            var media = (CssMediaRule)sheet.Rules[0];
            Assert.AreEqual("only screen, tv", media.Media.MediaText);
            var list = media.Media;
            Assert.AreEqual(2, list.Length);
            Assert.AreEqual(1, media.Rules.Length);
        }

        [Test]
        public void NotScreenTvMediaList()
        {
            var source = @"@media not screen,tv {
    h1 { color: green }
}";
            var sheet = CssParser.ParseStyleSheet(source);
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssMediaRule>(sheet.Rules[0]);
            var media = (CssMediaRule)sheet.Rules[0];
            Assert.AreEqual("not screen, tv", media.Media.MediaText);
            var list = media.Media;
            Assert.AreEqual(2, list.Length);
            Assert.AreEqual(1, media.Rules.Length);
        }

        [Test]
        public void FeatureMinWidthMediaList()
        {
            var source = @"@media (min-width:30px) {
    h1 { color: green }
}";
            var sheet = CssParser.ParseStyleSheet(source);
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssMediaRule>(sheet.Rules[0]);
            var media = (CssMediaRule)sheet.Rules[0];
            Assert.AreEqual("(min-width: 30px)", media.Media.MediaText);
            var list = media.Media;
            Assert.AreEqual(1, list.Length);
            Assert.AreEqual(1, media.Rules.Length);
        }

        [Test]
        public void OnlyFeatureWidthMediaList()
        {
            var source = @"@media only (width: 640px) {
    h1 { color: green }
}";
            var sheet = CssParser.ParseStyleSheet(source);
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssMediaRule>(sheet.Rules[0]);
            var media = (CssMediaRule)sheet.Rules[0];
            Assert.AreEqual("only (width: 640px)", media.Media.MediaText);
            var list = media.Media;
            Assert.AreEqual(1, list.Length);
            Assert.AreEqual(1, media.Rules.Length);
        }

        [Test]
        public void NotFeatureDeviceWidthMediaList()
        {
            var source = @"@media not (device-width: 640px) {
    h1 { color: green }
}";
            var sheet = CssParser.ParseStyleSheet(source);
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssMediaRule>(sheet.Rules[0]);
            var media = (CssMediaRule)sheet.Rules[0];
            Assert.AreEqual("not (device-width: 640px)", media.Media.MediaText);
            var list = media.Media;
            Assert.AreEqual(1, list.Length);
            Assert.AreEqual(1, media.Rules.Length);
        }

        [Test]
        public void AllFeatureMaxWidthMediaListMissingAnd()
        {
            var source = @"@media all (max-width:30px) {
    h1 { color: red }
}";
            var sheet = CssParser.ParseStyleSheet(source);
            Assert.AreEqual(0, sheet.Rules.Length);
        }

        [Test]
        public void NoMediaQueryGivenSkip()
        {
            var source = @"@media {
    h1 { color: red }
}";
            var sheet = CssParser.ParseStyleSheet(source);
            Assert.AreEqual(0, sheet.Rules.Length);
        }

        [Test]
        public void NotNoMediaTypeOrExpressionSkip()
        {
            var source = @"@media not {
    h1 { color: red }
}";
            var sheet = CssParser.ParseStyleSheet(source);
            Assert.AreEqual(0, sheet.Rules.Length);
        }

        [Test]
        public void OnlyNoMediaTypeOrExpressionSkip()
        {
            var source = @"@media only {
    h1 { color: red }
}";
            var sheet = CssParser.ParseStyleSheet(source);
            Assert.AreEqual(0, sheet.Rules.Length);
        }

        [Test]
        public void MediaFeatureMissingSkip()
        {
            var source = @"@media () {
    h1 { color: red }
}";

            var sheet = CssParser.ParseStyleSheet(source);
            Assert.AreEqual(0, sheet.Rules.Length);
        }

        [Test]
        public void MediaFeatureMissingSkipReadNext()
        {
            var source = @"@media () {
    h1 { color: red }
}
h1 { color: green }";
            var sheet = CssParser.ParseStyleSheet(source);
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<ICssStyleRule>(sheet.Rules[0]);
            var style = (ICssStyleRule)sheet.Rules[0];
            Assert.AreEqual("green", style.Style.Color);
            Assert.AreEqual("h1", style.SelectorText);
        }

        [Test]
        public void FeatureMaxWidthMediaListMissingConnectedAnd()
        {
            var source = @"@media (max-width:30px) (min-width:10px) {
    h1 { color: red }
}";
            var sheet = CssParser.ParseStyleSheet(source);
            Assert.AreEqual(0, sheet.Rules.Length);
        }

        [Test]
        public void TvScreenMediaListMissingComma()
        {
            var source = @"@media tv screen {
    h1 { color: red }
}";
            var sheet = CssParser.ParseStyleSheet(source);
            Assert.AreEqual(0, sheet.Rules.Length);
        }

        [Test]
        public void AllFeatureMaxWidthMediaListWithAndKeyword()
        {
            var source = @"@media all and (max-width:30px) {
    h1 { color: green }
}";
            var sheet = CssParser.ParseStyleSheet(source);
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssMediaRule>(sheet.Rules[0]);
            var media = (CssMediaRule)sheet.Rules[0];
            Assert.AreEqual("all and (max-width: 30px)", media.Media.MediaText);
            var list = media.Media;
            Assert.AreEqual(1, list.Length);
            Assert.AreEqual(1, media.Rules.Length);
        }

        [Test]
        public void FeatureAspectRatioMediaList()
        {
            var source = @"@media (aspect-ratio: 16/9) {
    h1 { color: green }
}";
            var sheet = CssParser.ParseStyleSheet(source);
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssMediaRule>(sheet.Rules[0]);
            var media = (CssMediaRule)sheet.Rules[0];
            Assert.AreEqual("(aspect-ratio: 16 / 9)", media.Media.MediaText);
            var list = media.Media;
            Assert.AreEqual(1, list.Length);
            Assert.AreEqual(1, media.Rules.Length);
        }

        [Test]
        public void PrintFeatureMaxWidthAndMinDeviceWidthMediaList()
        {
            var source = @"@media print and (max-width:30px) and (min-device-width:100px) {
    h1 { color: green }
}";
            var sheet = CssParser.ParseStyleSheet(source);
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssMediaRule>(sheet.Rules[0]);
            var media = (CssMediaRule)sheet.Rules[0];
            Assert.AreEqual("print and (max-width: 30px) and (min-device-width: 100px)", media.Media.MediaText);
            var list = media.Media;
            Assert.AreEqual(1, list.Length);
            Assert.AreEqual(1, media.Rules.Length);
        }

        [Test]
        public void AllFeatureMinWidthAndMinDeviceWidthScreenMediaList()
        {
            var source = @"@media all and (min-width:0) and (min-device-width:100px), screen {
    h1 { color: green }
}";
            var sheet = CssParser.ParseStyleSheet(source);
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssMediaRule>(sheet.Rules[0]);
            var media = (CssMediaRule)sheet.Rules[0];
            Assert.AreEqual("all and (min-width: 0) and (min-device-width: 100px), screen", media.Media.MediaText);
            var list = media.Media;
            Assert.AreEqual(2, list.Length);
            Assert.AreEqual(1, media.Rules.Length);
        }

        [Test]
        public void ImplicitAllFeatureResolutionMediaList()
        {
            var source = @"@media (resolution:72dpi) {
    h1 { color: green }
}";
            var sheet = CssParser.ParseStyleSheet(source);
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssMediaRule>(sheet.Rules[0]);
            var media = (CssMediaRule)sheet.Rules[0];
            Assert.AreEqual("(resolution: 72dpi)", media.Media.MediaText);
            var list = media.Media;
            Assert.AreEqual(1, list.Length);
            Assert.AreEqual(1, media.Rules.Length);
        }

        [Test]
        public void ImplicitAllFeatureMinResolutionAndMaxResolutionMediaList()
        {
            var source = @"@media (min-resolution:72dpi) and (max-resolution:140dpi) {
    h1 { color: green }
}";
            var sheet = CssParser.ParseStyleSheet(source);
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssMediaRule>(sheet.Rules[0]);
            var media = (CssMediaRule)sheet.Rules[0];
            Assert.AreEqual("(min-resolution: 72dpi) and (max-resolution: 140dpi)", media.Media.MediaText);
            var list = media.Media;
            Assert.AreEqual(1, list.Length);
            Assert.AreEqual(1, media.Rules.Length);
        }
    }
}
