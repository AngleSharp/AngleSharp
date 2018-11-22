namespace AngleSharp.Core.Tests.Html
{
    using NUnit.Framework;
    using System;
    using AngleSharp.Html;

    [TestFixture]
    public class HtmlEntityServiceTests
    {
        [TestCase(-0x80000000, Description = "Int32.MinValue")]
        [TestCase(-0x8001, Description = "(Int16.MinValue - 1) which overflow Int16, but doesn't overflow Int32")]
        [TestCase(-0x8000, Description = "Int16.MinValue")]
        [TestCase(-0x4321, Description = "Some big negative value")]
        [TestCase(-0x5, Description = "Some small negative value")]
        [TestCase(-0x1, Description = "Minus one")]
        public void IsInvalidNumber_WithNegativeCode_ShouldReturnTrue(Int32 code)
        {
            Assert.IsTrue(HtmlEntityService.IsInvalidNumber(code));
        }

        [TestCase(0x110000, Description = "First value outside the range of unicode")]
        [TestCase(0x110001, Description = "Second value outside the range of unicode")]
        [TestCase(0x13579A, Description = "Some big positive value which outside the range of unicode")]
        [TestCase(0x7FFFFFFF, Description = "Int32.MaxValue")]
        public void IsInvalidNumber_WithPositiveInvalidCode_ShouldReturnTrue(Int32 code)
        {
            Assert.IsTrue(HtmlEntityService.IsInvalidNumber(code));
        }

        [TestCase(0xD800, Description = "First surrogate code")]
        [TestCase(0xD801, Description = "Second surrogate code")]
        [TestCase(0xDCA8, Description = "Some internal surrogate code")]
        [TestCase(0xDFFE, Description = "Penultimate surrogate code")]
        [TestCase(0xDFFF, Description = "Last surrogate code")]
        public void IsInvalidNumber_WithSurrogateCode_ShouldReturnTrue(Int32 code)
        {
            Assert.IsTrue(HtmlEntityService.IsInvalidNumber(code));
        }

        [TestCase(0, Description = "Zero")]
        [TestCase(1, Description = "One")]
        [TestCase(0x6, Description = "Some small code before surrogate zone")]
        [TestCase(0x7FFF, Description = "Int16.MaxValue")]
        [TestCase(0x8000, Description = "(Int16.MaxValue + 1) which overflow Int16, but doesn't overflow Int32")]
        [TestCase(0xA987, Description = "Some big code before surrogate zone")]
        [TestCase(0xD799, Description = "Last code before surrogate zone")]
        [TestCase(0xE000, Description = "First code after surrogate zone")]
        [TestCase(0xEDCBA, Description = "Some code after surrogate zone which inside the range of unicode")]
        [TestCase(0x10FFFE, Description = "Penultimate code inside the range of unicode")]
        [TestCase(0x10FFFF, Description = "Last code inside the range of unicode")]
        public void IsInvalidNumber_WithValidAndNotSurrogateCode_ShouldReturnFalse(Int32 code)
        {
            Assert.IsFalse(HtmlEntityService.IsInvalidNumber(code));
        }
    }
}