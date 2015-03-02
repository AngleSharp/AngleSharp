using System;
using NUnit.Framework;
using AngleSharp.Dom.Html;

namespace AngleSharp.Core.Tests.Library
{
    [TestFixture]
    public class MeterElementTests
    {
        [Test]
        public void MeterDefaultValues()
        {
            var document = DocumentBuilder.Html("");
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
            var document = DocumentBuilder.Html("");
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
            var document = DocumentBuilder.Html("");
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
            var document = DocumentBuilder.Html("");
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
            var document = DocumentBuilder.Html("");
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
            var document = DocumentBuilder.Html("");
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
            var document = DocumentBuilder.Html("");
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
            var document = DocumentBuilder.Html("");
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
            var document = DocumentBuilder.Html("");
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
            var document = DocumentBuilder.Html("");
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
            var document = DocumentBuilder.Html("");
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
            var document = DocumentBuilder.Html("");
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
    }
}
