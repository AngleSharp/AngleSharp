using AngleSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class CommonExtensionsTests
    {
        [TestMethod]
        public void ContainsTestSuccess()
        {
            var list = "abcd[g;.";
            var result = list.Contains('[');
            Assert.IsTrue(result);
        }

        [TestMethod]
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

        [TestMethod]
        public void ContainsTestFail()
        {
            var list = "abcd[g;.";
            var result = list.Contains('e');
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CollapseEmptyString()
        {
            var str = string.Empty;
            var result = str.Collapse();
            Assert.AreEqual(str, result);
        }

        [TestMethod]
        public void CollapseNoSpacesString()
        {
            var str = "ThisIsMyString";
            var result = str.Collapse();
            Assert.AreEqual(str, result);
        }

        [TestMethod]
        public void CollapseSingleSpacesString()
        {
            var str = "This Is My String";
            var result = str.Collapse();
            Assert.AreEqual(str, result);
        }

        [TestMethod]
        public void CollapseMultipleSpacesString()
        {
            var original = "This Is My String";
            var str = "This  Is  My  String";
            var result = str.Collapse();
            Assert.AreEqual(original, result);
        }

        [TestMethod]
        public void CollapseMultipleLeadingSpacesString()
        {
            var original = " This Is My String";
            var str = "  This  Is  My  String";
            var result = str.Collapse();
            Assert.AreEqual(original, result);
        }

        [TestMethod]
        public void CollapseMultipleTrailingSpacesString()
        {
            var original = "This Is My String ";
            var str = "This  Is  My  String  ";
            var result = str.Collapse();
            Assert.AreEqual(original, result);
        }

        [TestMethod]
        public void CollapseMultipleLeadingTrailingSpacesString()
        {
            var original = " This Is My String ";
            var str = "  This  Is  My  String  ";
            var result = str.Collapse();
            Assert.AreEqual(original, result);
        }

        [TestMethod]
        public void CollapseStripEmptyString()
        {
            var str = string.Empty;
            var result = str.CollapseAndStrip();
            Assert.AreEqual(str, result);
        }

        [TestMethod]
        public void CollapseStripNoSpacesString()
        {
            var str = "ThisIsMyString";
            var result = str.CollapseAndStrip();
            Assert.AreEqual(str, result);
        }

        [TestMethod]
        public void CollapseStripSingleSpacesString()
        {
            var str = "This Is My String";
            var result = str.CollapseAndStrip();
            Assert.AreEqual(str, result);
        }

        [TestMethod]
        public void CollapseStripMultipleSpacesString()
        {
            var original = "This Is My String";
            var str = "This  Is  My  String";
            var result = str.CollapseAndStrip();
            Assert.AreEqual(original, result);
        }

        [TestMethod]
        public void CollapseStripMultipleLeadingSpacesString()
        {
            var original = "This Is My String";
            var str = "  This  Is  My  String";
            var result = str.CollapseAndStrip();
            Assert.AreEqual(original, result);
        }

        [TestMethod]
        public void CollapseStripMultipleTrailingSpacesString()
        {
            var original = "This Is My String";
            var str = "This  Is  My  String  ";
            var result = str.CollapseAndStrip();
            Assert.AreEqual(original, result);
        }

        [TestMethod]
        public void CollapseStripMultipleLeadingTrailingSpacesString()
        {
            var original = "This Is My String";
            var str = "  This  Is  My  String  ";
            var result = str.CollapseAndStrip();
            Assert.AreEqual(original, result);
        }

        [TestMethod]
        public void FromHexNumeric()
        {
            var number = '2';
            var result = number.FromHex();
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void FromHexLowercase()
        {
            var number = 'c';
            var result = number.FromHex();
            Assert.AreEqual(12, result);
        }

        [TestMethod]
        public void FromHexUppercase()
        {
            var number = 'F';
            var result = number.FromHex();
            Assert.AreEqual(15, result);
        }

        [TestMethod]
        public void StripLineBreaksWithoutLineBreak()
        {
            var str = "Hi there how are you";
            var result = str.StripLineBreaks();
            Assert.AreEqual(str, result);
        }

        [TestMethod]
        public void StripLineBreaksWithLineBreak()
        {
            var str = "Hi\nthere\thow\r\n\nare you";
            var result = str.StripLineBreaks();
            Assert.AreEqual("Hithere\thoware you", result);
        }

        [TestMethod]
        public void StripLineBreaksOnlyLineBreak()
        {
            var str = "\r\n\r\n\n\n\r\n";
            var result = str.StripLineBreaks();
            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void StripLineBreaksEmptyString()
        {
            var str = "";
            var result = str.StripLineBreaks();
            Assert.AreEqual(str, result);
        }

        [TestMethod]
        public void StripLeadingTailingSpacesEmptyString()
        {
            var str = "";
            var result = str.StripLeadingTailingSpaces();
            Assert.AreEqual(str, result);
        }

        [TestMethod]
        public void StripLeadingTailingSpacesSpaceString()
        {
            var str = "       ";
            var result = str.StripLeadingTailingSpaces();
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void StripLeadingTailingSpacesNormalString()
        {
            var str = "Hello how are you";
            var result = str.StripLeadingTailingSpaces();
            Assert.AreEqual(str, result);
        }

        [TestMethod]
        public void StripLeadingTailingSpacesLeadingSpacesString()
        {
            var str = "   What is that";
            var result = str.StripLeadingTailingSpaces();
            Assert.AreEqual("What is that", result);
        }

        [TestMethod]
        public void StripLeadingTailingSpacesTailingSpacesString()
        {
            var str = "How are you   ";
            var result = str.StripLeadingTailingSpaces();
            Assert.AreEqual("How are you", result);
        }

        [TestMethod]
        public void StripLeadingTailingSpacesBothKindOfSpacesString()
        {
            var str = "   Hello how are you    ";
            var result = str.StripLeadingTailingSpaces();
            Assert.AreEqual("Hello how are you", result);
        }

        [TestMethod]
        public void SplitStringOnSpace()
        {
            var str = "Hi there how are you";
            var result = str.SplitWithoutTrimming(' ');
            Assert.AreEqual(5, result.Length);
        }

        [TestMethod]
        public void SplitStringNothingFound()
        {
            var str = "Hi there how are you";
            var result = str.SplitWithoutTrimming('z');
            Assert.AreEqual(1, result.Length);
        }

        [TestMethod]
        public void SplitStringFinalDelimiter()
        {
            var str = "Hi there how are you ";
            var result = str.SplitWithoutTrimming(' ');
            Assert.AreEqual(5, result.Length);
        }

        [TestMethod]
        public void SplitTrimmingStringOnSpace()
        {
            var str = "Hi there how are you";
            var result = str.SplitWithTrimming(' ');
            Assert.AreEqual(5, result.Length);
        }

        [TestMethod]
        public void SplitTrimmingStringNothingFound()
        {
            var str = "Hi there how are you";
            var result = str.SplitWithTrimming('z');
            Assert.AreEqual(1, result.Length);
        }

        [TestMethod]
        public void SplitTrimmingStringTrimming()
        {
            var str = "Hi;  there how ;are you";
            var result = str.SplitWithTrimming(';');
            Assert.AreEqual("there how", result[1]);
        }

        [TestMethod]
        public void SplitTrimmingStringLength()
        {
            var str = "Hi;  there how ;are you";
            var result = str.SplitWithTrimming(';');
            Assert.AreEqual(3, result.Length);
        }
    }
}
