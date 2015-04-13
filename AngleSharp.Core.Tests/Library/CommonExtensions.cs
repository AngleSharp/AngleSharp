namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Extensions;
    using NUnit.Framework;

    [TestFixture]
    public class CommonExtensionsTests
    {
        [Test]
        public void ContainsTestSuccess()
        {
            var list = "abcd[g;.";
            var result = list.Contains('[');
            Assert.IsTrue(result);
        }

        [Test]
        public void HexadecimalNumbersWorking()
        {
            for (int i = 0; i < 256; i++)
            {
                var num = (byte)i;
                var a = num.ToHex();
                var b = i.ToString("X").PadLeft(2, '0');
                Assert.AreEqual(b, a);
            }
        }

        [Test]
        public void ContainsTestFail()
        {
            var list = "abcd[g;.";
            var result = list.Contains('e');
            Assert.IsFalse(result);
        }

        [Test]
        public void CollapseEmptyString()
        {
            var str = string.Empty;
            var result = str.Collapse();
            Assert.AreEqual(str, result);
        }

        [Test]
        public void CollapseNoSpacesString()
        {
            var str = "ThisIsMyString";
            var result = str.Collapse();
            Assert.AreEqual(str, result);
        }

        [Test]
        public void CollapseSingleSpacesString()
        {
            var str = "This Is My String";
            var result = str.Collapse();
            Assert.AreEqual(str, result);
        }

        [Test]
        public void CollapseMultipleSpacesString()
        {
            var original = "This Is My String";
            var str = "This  Is  My  String";
            var result = str.Collapse();
            Assert.AreEqual(original, result);
        }

        [Test]
        public void CollapseMultipleLeadingSpacesString()
        {
            var original = " This Is My String";
            var str = "  This  Is  My  String";
            var result = str.Collapse();
            Assert.AreEqual(original, result);
        }

        [Test]
        public void CollapseMultipleTrailingSpacesString()
        {
            var original = "This Is My String ";
            var str = "This  Is  My  String  ";
            var result = str.Collapse();
            Assert.AreEqual(original, result);
        }

        [Test]
        public void CollapseMultipleLeadingTrailingSpacesString()
        {
            var original = " This Is My String ";
            var str = "  This  Is  My  String  ";
            var result = str.Collapse();
            Assert.AreEqual(original, result);
        }

        [Test]
        public void CollapseStripEmptyString()
        {
            var str = string.Empty;
            var result = str.CollapseAndStrip();
            Assert.AreEqual(str, result);
        }

        [Test]
        public void CollapseStripNoSpacesString()
        {
            var str = "ThisIsMyString";
            var result = str.CollapseAndStrip();
            Assert.AreEqual(str, result);
        }

        [Test]
        public void CollapseStripSingleSpacesString()
        {
            var str = "This Is My String";
            var result = str.CollapseAndStrip();
            Assert.AreEqual(str, result);
        }

        [Test]
        public void CollapseStripMultipleSpacesString()
        {
            var original = "This Is My String";
            var str = "This  Is  My  String";
            var result = str.CollapseAndStrip();
            Assert.AreEqual(original, result);
        }

        [Test]
        public void CollapseStripMultipleLeadingSpacesString()
        {
            var original = "This Is My String";
            var str = "  This  Is  My  String";
            var result = str.CollapseAndStrip();
            Assert.AreEqual(original, result);
        }

        [Test]
        public void CollapseStripMultipleTrailingSpacesString()
        {
            var original = "This Is My String";
            var str = "This  Is  My  String  ";
            var result = str.CollapseAndStrip();
            Assert.AreEqual(original, result);
        }

        [Test]
        public void CollapseStripMultipleLeadingTrailingSpacesString()
        {
            var original = "This Is My String";
            var str = "  This  Is  My  String  ";
            var result = str.CollapseAndStrip();
            Assert.AreEqual(original, result);
        }

        [Test]
        public void FromHexNumeric()
        {
            var number = '2';
            var result = number.FromHex();
            Assert.AreEqual(2, result);
        }

        [Test]
        public void FromHexLowercase()
        {
            var number = 'c';
            var result = number.FromHex();
            Assert.AreEqual(12, result);
        }

        [Test]
        public void FromHexUppercase()
        {
            var number = 'F';
            var result = number.FromHex();
            Assert.AreEqual(15, result);
        }

        [Test]
        public void StripLineBreaksWithoutLineBreak()
        {
            var str = "Hi there how are you";
            var result = str.StripLineBreaks();
            Assert.AreEqual(str, result);
        }

        [Test]
        public void StripLineBreaksWithLineBreak()
        {
            var str = "Hi\nthere\thow\r\n\nare you";
            var result = str.StripLineBreaks();
            Assert.AreEqual("Hithere\thoware you", result);
        }

        [Test]
        public void StripLineBreaksOnlyLineBreak()
        {
            var str = "\r\n\r\n\n\n\r\n";
            var result = str.StripLineBreaks();
            Assert.AreEqual("", result);
        }

        [Test]
        public void StripLineBreaksEmptyString()
        {
            var str = "";
            var result = str.StripLineBreaks();
            Assert.AreEqual(str, result);
        }

        [Test]
        public void StripLeadingTailingSpacesEmptyString()
        {
            var str = "";
            var result = str.StripLeadingTailingSpaces();
            Assert.AreEqual(str, result);
        }

        [Test]
        public void StripLeadingTailingSpacesSpaceString()
        {
            var str = "       ";
            var result = str.StripLeadingTailingSpaces();
            Assert.AreEqual(0, result.Length);
        }

        [Test]
        public void StripLeadingTailingSpacesNormalString()
        {
            var str = "Hello how are you";
            var result = str.StripLeadingTailingSpaces();
            Assert.AreEqual(str, result);
        }

        [Test]
        public void StripLeadingTailingSpacesLeadingSpacesString()
        {
            var str = "   What is that";
            var result = str.StripLeadingTailingSpaces();
            Assert.AreEqual("What is that", result);
        }

        [Test]
        public void StripLeadingTailingSpacesTailingSpacesString()
        {
            var str = "How are you   ";
            var result = str.StripLeadingTailingSpaces();
            Assert.AreEqual("How are you", result);
        }

        [Test]
        public void StripLeadingTailingSpacesBothKindOfSpacesString()
        {
            var str = "   Hello how are you    ";
            var result = str.StripLeadingTailingSpaces();
            Assert.AreEqual("Hello how are you", result);
        }

        [Test]
        public void SplitStringOnSpace()
        {
            var str = "Hi there how are you";
            var result = str.SplitWithoutTrimming(' ');
            Assert.AreEqual(5, result.Length);
        }

        [Test]
        public void SplitStringNothingFound()
        {
            var str = "Hi there how are you";
            var result = str.SplitWithoutTrimming('z');
            Assert.AreEqual(1, result.Length);
        }

        [Test]
        public void SplitStringFinalDelimiter()
        {
            var str = "Hi there how are you ";
            var result = str.SplitWithoutTrimming(' ');
            Assert.AreEqual(5, result.Length);
        }

        [Test]
        public void SplitTrimmingStringOnSpace()
        {
            var str = "Hi there how are you";
            var result = str.SplitWithTrimming(' ');
            Assert.AreEqual(5, result.Length);
        }

        [Test]
        public void SplitTrimmingStringNothingFound()
        {
            var str = "Hi there how are you";
            var result = str.SplitWithTrimming('z');
            Assert.AreEqual(1, result.Length);
        }

        [Test]
        public void SplitTrimmingStringTrimming()
        {
            var str = "Hi;  there how ;are you";
            var result = str.SplitWithTrimming(';');
            Assert.AreEqual("there how", result[1]);
        }

        [Test]
        public void SplitTrimmingStringLength()
        {
            var str = "Hi;  there how ;are you";
            var result = str.SplitWithTrimming(';');
            Assert.AreEqual(3, result.Length);
        }
    }
}
