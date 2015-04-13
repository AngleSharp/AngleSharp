namespace AngleSharp.Core.Tests.Library
{
    using System;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Html;
    using NUnit.Framework;

    [TestFixture]
    public class MeterElementTests
    {
        static IDocument Html(String code)
        {
            return code.ToHtmlDocument();
        }

        [Test]
        public void MeterDefaultValues()
        {
            var document = Html("");
            var meter = document.CreateElement<IHtmlMeterElement>();
            meter.Value = 0;
            Assert.AreEqual(0.0, meter.Value);
            Assert.AreEqual(0.0, meter.Minimum);
            Assert.AreEqual(1.0, meter.Maximum);
            Assert.AreEqual(0.0, meter.Low);
            Assert.AreEqual(1.0, meter.High);
            Assert.AreEqual(0.5, meter.Optimum);
        }

        [Test]
        public void MeterSettingValuesToMinMaxLowHighAndOpt()
        {
            var document = Html("");
            var meter = document.CreateElement<IHtmlMeterElement>();
            meter.Value = 3;
            meter.Minimum = -10.1;
            meter.Maximum = 10.1;
            meter.Low = -9.1;
            meter.High = 9.1;
            meter.Optimum = 3.0;
            Assert.AreEqual(3.0, meter.Value);
            Assert.AreEqual(-10.1, meter.Minimum);
            Assert.AreEqual(10.1, meter.Maximum);
            Assert.AreEqual(-9.1, meter.Low);
            Assert.AreEqual(9.1, meter.High);
            Assert.AreEqual(3.0, meter.Optimum);
        }

        [Test]
        public void MeterInvalidFloatingPointNumberValues()
        {
            var document = Html("");
            var meter = document.CreateElement<IHtmlMeterElement>();
            meter.SetAttribute("value", "foobar");
            meter.SetAttribute("min", "foobar");
            meter.SetAttribute("max", "foobar");
            meter.SetAttribute("low", "foobar");
            meter.SetAttribute("high", "foobar");
            meter.SetAttribute("optimum", "foobar");
            Assert.AreEqual(0.0, meter.Value);
            Assert.AreEqual(0.0, meter.Minimum);
            Assert.AreEqual(1.0, meter.Maximum);
            Assert.AreEqual(0.0, meter.Low);
            Assert.AreEqual(1.0, meter.High);
            Assert.AreEqual(0.5, meter.Optimum);
        }

        [Test]
        public void MeterMaxLessThanMin()
        {
            var document = Html("");
            var meter = document.CreateElement<IHtmlMeterElement>();
            meter.Value = 0.0;
            meter.Minimum = 0.0;
            meter.Maximum = -1.0;
            Assert.AreEqual(0.0, meter.Value);
            Assert.AreEqual(0.0, meter.Minimum);
            Assert.AreEqual(0.0, meter.Maximum);
            Assert.AreEqual(0.0, meter.Low);
            Assert.AreEqual(0.0, meter.High);
            Assert.AreEqual(0.0, meter.Optimum);
        }

        [Test]
        public void MeterValueLessThanMin()
        {
            var document = Html("");
            var meter = document.CreateElement<IHtmlMeterElement>();
            meter.Value = 0.0;
            meter.Minimum = 10.0;
            meter.Maximum = 20.0;
            Assert.AreEqual(10.0, meter.Value);
            Assert.AreEqual(10.0, meter.Minimum);
            Assert.AreEqual(20.0, meter.Maximum);
            Assert.AreEqual(10.0, meter.Low);
            Assert.AreEqual(20.0, meter.High);
            Assert.AreEqual(15.0, meter.Optimum);
        }

        [Test]
        public void MeterValueGreaterThanMax()
        {
            var document = Html("");
            var meter = document.CreateElement<IHtmlMeterElement>();
            meter.Value = 30.0;
            meter.Minimum = 10.0;
            meter.Maximum = 20.0;
            Assert.AreEqual(20.0, meter.Value);
            Assert.AreEqual(10.0, meter.Minimum);
            Assert.AreEqual(20.0, meter.Maximum);
            Assert.AreEqual(10.0, meter.Low);
            Assert.AreEqual(20.0, meter.High);
            Assert.AreEqual(15.0, meter.Optimum);
        }

        [Test]
        public void MeterLowLessThanMin()
        {
            var document = Html("");
            var meter = document.CreateElement<IHtmlMeterElement>();
            meter.Value = 15.0;
            meter.Minimum = 10.0;
            meter.Maximum = 20.0;
            meter.Low = 5.0;
            Assert.AreEqual(15.0, meter.Value);
            Assert.AreEqual(10.0, meter.Minimum);
            Assert.AreEqual(20.0, meter.Maximum);
            Assert.AreEqual(10.0, meter.Low);
            Assert.AreEqual(20.0, meter.High);
            Assert.AreEqual(15.0, meter.Optimum);
        }

        [Test]
        public void MeterLowGreaterThanMax()
        {
            var document = Html("");
            var meter = document.CreateElement<IHtmlMeterElement>();
            meter.Value = 15.0;
            meter.Minimum = 10.0;
            meter.Maximum = 20.0;
            meter.Low = 25.0;
            Assert.AreEqual(15.0, meter.Value);
            Assert.AreEqual(10.0, meter.Minimum);
            Assert.AreEqual(20.0, meter.Maximum);
            Assert.AreEqual(20.0, meter.Low);
            Assert.AreEqual(20.0, meter.High);
            Assert.AreEqual(15.0, meter.Optimum);
        }

        [Test]
        public void MeterHighLessThanLow()
        {
            var document = Html("");
            var meter = document.CreateElement<IHtmlMeterElement>();
            meter.Value = 15.0;
            meter.Minimum = 10.0;
            meter.Maximum = 20.0;
            meter.Low = 12.0;
            meter.High = 10.0;
            Assert.AreEqual(15.0, meter.Value);
            Assert.AreEqual(10.0, meter.Minimum);
            Assert.AreEqual(20.0, meter.Maximum);
            Assert.AreEqual(12.0, meter.Low);
            Assert.AreEqual(12.0, meter.High);
            Assert.AreEqual(15.0, meter.Optimum);
        }

        [Test]
        public void MeterHighGreaterThanMax()
        {
            var document = Html("");
            var meter = document.CreateElement<IHtmlMeterElement>();
            meter.Value = 15.0;
            meter.Minimum = 10.0;
            meter.Maximum = 20.0;
            meter.Low = 10.0;
            meter.High = 22.0;
            Assert.AreEqual(15.0, meter.Value);
            Assert.AreEqual(10.0, meter.Minimum);
            Assert.AreEqual(20.0, meter.Maximum);
            Assert.AreEqual(10.0, meter.Low);
            Assert.AreEqual(20.0, meter.High);
            Assert.AreEqual(15.0, meter.Optimum);
        }

        [Test]
        public void MeterOptimumLessThanMin()
        {
            var document = Html("");
            var meter = document.CreateElement<IHtmlMeterElement>();
            meter.Value = 15.0;
            meter.Minimum = 10.0;
            meter.Maximum = 20.0;
            meter.Low = 10.0;
            meter.High = 20.0;
            meter.Optimum = 9.0;
            Assert.AreEqual(15.0, meter.Value);
            Assert.AreEqual(10.0, meter.Minimum);
            Assert.AreEqual(20.0, meter.Maximum);
            Assert.AreEqual(10.0, meter.Low);
            Assert.AreEqual(20.0, meter.High);
            Assert.AreEqual(10.0, meter.Optimum);
        }

        [Test]
        public void MeterOptimumGreaterThanMax()
        {
            var document = Html("");
            var meter = document.CreateElement<IHtmlMeterElement>();
            meter.Value = 15.0;
            meter.Minimum = 10.0;
            meter.Maximum = 20.0;
            meter.Low = 10.0;
            meter.High = 20.0;
            meter.Optimum = 21.0;
            Assert.AreEqual(15.0, meter.Value);
            Assert.AreEqual(10.0, meter.Minimum);
            Assert.AreEqual(20.0, meter.Maximum);
            Assert.AreEqual(10.0, meter.Low);
            Assert.AreEqual(20.0, meter.High);
            Assert.AreEqual(20.0, meter.Optimum);
        }

        [Test]
        public void MeterValueMustBeZeroWhenAStringIsGiven()
        {
            var document = Html("<meter value=abc></meter>");
            var meter = document.QuerySelector("meter") as IHtmlMeterElement;
            Assert.AreEqual(0.0, meter.Value);
        }

        [Test]
        public void MeterDefaultValueOfMinIsZero()
        {
            var document = Html("<meter value=-10></meter>");
            var meter = document.QuerySelector("meter") as IHtmlMeterElement;
            Assert.AreEqual(0.0, meter.Minimum);
            Assert.AreEqual(0.0, meter.Value);
        }

        [Test]
        public void MeterDefaultValueOfMaxIsOne()
        {
            var document = Html("<meter value=10></meter>");
            var meter = document.QuerySelector("meter") as IHtmlMeterElement;
            Assert.AreEqual(1.0, meter.Maximum);
            Assert.AreEqual(1.0, meter.Value);
        }

        [Test]
        public void MeterValueSmallerThanOneGivenMinAndMaxNotSpecifiedSameAsDefaultMax()
        {
            var document = Html("<meter value=10 min=-3.1></meter>");
            var meter = document.QuerySelector("meter") as IHtmlMeterElement;
            Assert.AreEqual(1.0, meter.Maximum);
            Assert.AreEqual(1.0, meter.Value);
        }

        [Test]
        public void MeterValueLargerThanOrEqualToOneGivenToMinAndMaxNotSpecified()
        {
            var document = Html("<meter value=210 min=12.1></meter>");
            var meter = document.QuerySelector("meter") as IHtmlMeterElement;
            Assert.AreEqual(12.1, meter.Maximum);
            Assert.AreEqual(12.1, meter.Value);
        }

        [Test]
        public void MeterValueSmallerThanZeroGivenToMaxAndMinNotSpecifiedSameAsDefault()
        {
            var document = Html("<meter value=-10 max=-5342.55></meter>");
            var meter = document.QuerySelector("meter") as IHtmlMeterElement;
            Assert.AreEqual(0.0, meter.Value);
            Assert.AreEqual(0.0, meter.Minimum);
            Assert.AreEqual(0.0, meter.Maximum);
        }

        [Test]
        public void MeterValueLargerThanOrEqualToZeroGivenToMaxAndMinNoSpecifiedSameAsDefault()
        {
            var document = Html("<meter value=210 max=-9.9></meter>");
            var meter = document.QuerySelector("meter") as IHtmlMeterElement;
            Assert.AreEqual(0.0, meter.Value);
            Assert.AreEqual(0.0, meter.Minimum);
            Assert.AreEqual(0.0, meter.Maximum);
        }

        [Test]
        public void MeterMinMustBeZeroWhenAStringIsGiven()
        {
            var document = Html("<meter value=-2 min=hugfe></meter>");
            var meter = document.QuerySelector("meter") as IHtmlMeterElement;
            Assert.AreEqual(0.0, meter.Minimum);
            Assert.AreEqual(0.0, meter.Value);
        }

        [Test]
        public void MeterMaxMustBeOneWhenAStringIsGiven()
        {
            var document = Html("<meter value=2.4 max=min></meter>");
            var meter = document.QuerySelector("meter") as IHtmlMeterElement;
            Assert.AreEqual(1.0, meter.Maximum);
            Assert.AreEqual(1.0, meter.Value);
        }

        [Test]
        public void MeterIllegalLowWithMinNotAffectTheActualValue()
        {
            var document = Html("<meter value=-20 min=-10.3 low=ahuge></meter>");
            var meter = document.QuerySelector("meter") as IHtmlMeterElement;
            Assert.AreEqual(-10.3, meter.Low);
        }

        [Test]
        public void MeterIllegalHighWithMaxNotAffectTheActualValue()
        {
            var document = Html("<meter value=2.4 high=old max=1.5></meter>");
            var meter = document.QuerySelector("meter") as IHtmlMeterElement;
            Assert.AreEqual(1.5, meter.High);
            Assert.AreEqual(1.5, meter.Value);
        }
    }
}
