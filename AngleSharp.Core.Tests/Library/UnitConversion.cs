namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Css.Values;
    using NUnit.Framework;

    [TestFixture]
    public class UnitConversionTests
    {
        [Test]
        public void LengthParseCorrectPxValue()
        {
            var s = "12px";
            var v = default(Length);
            var r = Length.TryParse(s, out v);
            Assert.IsTrue(r);
            Assert.AreEqual(12f, v.Value);
            Assert.AreEqual(Length.Unit.Px, v.Type);
        }

        [Test]
        public void LengthParseIncorrectValue()
        {
            var s = "123.5";
            var v = default(Length);
            var r = Length.TryParse(s, out v);
            Assert.IsFalse(r);
        }

        [Test]
        public void LengthParseCorrectVwValue()
        {
            var s = "12.2vw";
            var v = default(Length);
            var r = Length.TryParse(s, out v);
            Assert.IsTrue(r);
            Assert.AreEqual(12.2f, v.Value);
            Assert.AreEqual(Length.Unit.Vw, v.Type);
        }

    }
}
