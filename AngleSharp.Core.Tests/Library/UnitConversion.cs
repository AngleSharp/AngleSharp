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

        [Test]
        public void AngleParseCorrectDegValue()
        {
            var s = "1.35e2deg";
            var v = default(Angle);
            var r = Angle.TryParse(s, out v);
            Assert.IsTrue(r);
            Assert.AreEqual(135f, v.Value);
            Assert.AreEqual(Angle.Unit.Deg, v.Type);
        }

        [Test]
        public void ResolutionParseCorrectDpiValue()
        {
            var s = "-24.0dpi";
            var v = default(Resolution);
            var r = Resolution.TryParse(s, out v);
            Assert.IsTrue(r);
            Assert.AreEqual(-24f, v.Value);
            Assert.AreEqual(Resolution.Unit.Dpi, v.Type);
        }

        [Test]
        public void FrequencyParseCorrectKhzValue()
        {
            var s = "17.123khz";
            var v = default(Frequency);
            var r = Frequency.TryParse(s, out v);
            Assert.IsTrue(r);
            Assert.AreEqual(17.123f, v.Value);
            Assert.AreEqual(Frequency.Unit.Khz, v.Type);
        }

        [Test]
        public void TimeParseCorrectSecondsValue()
        {
            var s = "0s";
            var v = default(Time);
            var r = Time.TryParse(s, out v);
            Assert.IsTrue(r);
            Assert.AreEqual(0f, v.Value);
            Assert.AreEqual(Time.Unit.S, v.Type);
        }

        [Test]
        public void AngleParseIncorrectValue()
        {
            var s = "123.deg";
            var v = default(Angle);
            var r = Angle.TryParse(s, out v);
            Assert.IsFalse(r);
        }
    }
}
