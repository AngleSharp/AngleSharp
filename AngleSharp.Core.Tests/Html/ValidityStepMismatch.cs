using System;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using NUnit.Framework;

namespace AngleSharp.Core.Tests.Html
{
    /// <summary>
    /// Tests generated according to the W3C-Test.org page:
    /// http://www.w3c-test.org/html/semantics/forms/constraints/form-validation-validity-stepMismatch.html
    /// </summary>
    [TestFixture]
	public class ValidityStepMismatchTests
    {
        static IDocument Html(String code)
        {
            return code.ToHtmlDocument();
        }

        [Test]
		public void TestStepmismatchInputDatetime1()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "datetime";
			element.RemoveAttribute("required");
			element.RemoveAttribute("pattern");
			element.RemoveAttribute("step");
			element.RemoveAttribute("max");
			element.RemoveAttribute("min");
			element.RemoveAttribute("maxlength");
			element.RemoveAttribute("value");
			element.RemoveAttribute("multiple");
			element.RemoveAttribute("checked");
			element.RemoveAttribute("selected");
			element.SetAttribute("step", "");
			element.Value = "2000-01-01T12:00:00Z";
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(false, element.Validity.IsStepMismatch);
		}
		
		[Test]
		public void TestStepmismatchInputDatetime2()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "datetime";
			element.RemoveAttribute("required");
			element.RemoveAttribute("pattern");
			element.RemoveAttribute("step");
			element.RemoveAttribute("max");
			element.RemoveAttribute("min");
			element.RemoveAttribute("maxlength");
			element.RemoveAttribute("value");
			element.RemoveAttribute("multiple");
			element.RemoveAttribute("checked");
			element.RemoveAttribute("selected");
			element.SetAttribute("step", "120");
			element.Value = "";
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(false, element.Validity.IsStepMismatch);
		}
		
		[Test]
		public void TestStepmismatchInputDatetime3()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "datetime";
			element.RemoveAttribute("required");
			element.RemoveAttribute("pattern");
			element.RemoveAttribute("step");
			element.RemoveAttribute("max");
			element.RemoveAttribute("min");
			element.RemoveAttribute("maxlength");
			element.RemoveAttribute("value");
			element.RemoveAttribute("multiple");
			element.RemoveAttribute("checked");
			element.RemoveAttribute("selected");
			element.SetAttribute("step", "120");
			element.Value = "2000-01-01T12:58Z";
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(false, element.Validity.IsStepMismatch);
		}
		
		[Test]
		public void TestStepmismatchInputDatetime4()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "datetime";
			element.RemoveAttribute("required");
			element.RemoveAttribute("pattern");
			element.RemoveAttribute("step");
			element.RemoveAttribute("max");
			element.RemoveAttribute("min");
			element.RemoveAttribute("maxlength");
			element.RemoveAttribute("value");
			element.RemoveAttribute("multiple");
			element.RemoveAttribute("checked");
			element.RemoveAttribute("selected");
			element.SetAttribute("step", "120");
			element.Value = "2000-01-01T12:59Z";
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(true, element.Validity.IsStepMismatch);
		}
	
		[Test]
		public void TestStepmismatchInputDatetimeLocal1()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "datetime-local";
			element.RemoveAttribute("required");
			element.RemoveAttribute("pattern");
			element.RemoveAttribute("step");
			element.RemoveAttribute("max");
			element.RemoveAttribute("min");
			element.RemoveAttribute("maxlength");
			element.RemoveAttribute("value");
			element.RemoveAttribute("multiple");
			element.RemoveAttribute("checked");
			element.RemoveAttribute("selected");
			element.SetAttribute("step", "");
			element.Value = "2000-01-01T12:00:00";
			Assert.AreEqual("datetime-local", element.Type);
			Assert.AreEqual(false, element.Validity.IsStepMismatch);
		}
	
		[Test]
		public void TestStepmismatchInputDatetimeLocal2()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "datetime-local";
			element.RemoveAttribute("required");
			element.RemoveAttribute("pattern");
			element.RemoveAttribute("step");
			element.RemoveAttribute("max");
			element.RemoveAttribute("min");
			element.RemoveAttribute("maxlength");
			element.RemoveAttribute("value");
			element.RemoveAttribute("multiple");
			element.RemoveAttribute("checked");
			element.RemoveAttribute("selected");
			element.SetAttribute("step", "120000");
			element.Value = "";
			Assert.AreEqual("datetime-local", element.Type);
			Assert.AreEqual(false, element.Validity.IsStepMismatch);
		}
	
		[Test]
		public void TestStepmismatchInputDatetimeLocal3()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "datetime-local";
			element.RemoveAttribute("required");
			element.RemoveAttribute("pattern");
			element.RemoveAttribute("step");
			element.RemoveAttribute("max");
			element.RemoveAttribute("min");
			element.RemoveAttribute("maxlength");
			element.RemoveAttribute("value");
			element.RemoveAttribute("multiple");
			element.RemoveAttribute("checked");
			element.RemoveAttribute("selected");
			element.SetAttribute("step", "120000");
			element.Value = "1970-01-01T12:02:00";
			Assert.AreEqual("datetime-local", element.Type);
			Assert.AreEqual(true, element.Validity.IsStepMismatch);
		}
	
		[Test]
		public void TestStepmismatchInputDatetimeLocal4()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "datetime-local";
			element.RemoveAttribute("required");
			element.RemoveAttribute("pattern");
			element.RemoveAttribute("step");
			element.RemoveAttribute("max");
			element.RemoveAttribute("min");
			element.RemoveAttribute("maxlength");
			element.RemoveAttribute("value");
			element.RemoveAttribute("multiple");
			element.RemoveAttribute("checked");
			element.RemoveAttribute("selected");
			element.SetAttribute("step", "120000");
			element.Value = "1970-01-01T12:03:00";
			Assert.AreEqual("datetime-local", element.Type);
			Assert.AreEqual(true, element.Validity.IsStepMismatch);
		}
		
		[Test]
		public void TestStepmismatchInputDate1()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "date";
			element.RemoveAttribute("required");
			element.RemoveAttribute("pattern");
			element.RemoveAttribute("step");
			element.RemoveAttribute("max");
			element.RemoveAttribute("min");
			element.RemoveAttribute("maxlength");
			element.RemoveAttribute("value");
			element.RemoveAttribute("multiple");
			element.RemoveAttribute("checked");
			element.RemoveAttribute("selected");
			element.SetAttribute("step", "");
			element.Value = "2000-01-01";
			Assert.AreEqual("date", element.Type);
			Assert.AreEqual(false, element.Validity.IsStepMismatch);
		}
		
		[Test]
		public void TestStepmismatchInputDate2()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "date";
			element.RemoveAttribute("required");
			element.RemoveAttribute("pattern");
			element.RemoveAttribute("step");
			element.RemoveAttribute("max");
			element.RemoveAttribute("min");
			element.RemoveAttribute("maxlength");
			element.RemoveAttribute("value");
			element.RemoveAttribute("multiple");
			element.RemoveAttribute("checked");
			element.RemoveAttribute("selected");
			element.SetAttribute("step", "2");
			element.Value = "";
			Assert.AreEqual("date", element.Type);
			Assert.AreEqual(false, element.Validity.IsStepMismatch);
		}
		
		[Test]
		public void TestStepmismatchInputDate3()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "date";
			element.RemoveAttribute("required");
			element.RemoveAttribute("pattern");
			element.RemoveAttribute("step");
			element.RemoveAttribute("max");
			element.RemoveAttribute("min");
			element.RemoveAttribute("maxlength");
			element.RemoveAttribute("value");
			element.RemoveAttribute("multiple");
			element.RemoveAttribute("checked");
			element.RemoveAttribute("selected");
			element.SetAttribute("step", "2");
			element.Value = "1970-01-02";
			Assert.AreEqual("date", element.Type);
			Assert.AreEqual(true, element.Validity.IsStepMismatch);
		}
		
		[Test]
		public void TestStepmismatchInputDate4()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "date";
			element.RemoveAttribute("required");
			element.RemoveAttribute("pattern");
			element.RemoveAttribute("step");
			element.RemoveAttribute("max");
			element.RemoveAttribute("min");
			element.RemoveAttribute("maxlength");
			element.RemoveAttribute("value");
			element.RemoveAttribute("multiple");
			element.RemoveAttribute("checked");
			element.RemoveAttribute("selected");
			element.SetAttribute("step", "2");
			element.Value = "1970-01-03";
			Assert.AreEqual("date", element.Type);
			Assert.AreEqual(false, element.Validity.IsStepMismatch);
		}
		
		[Test]
		public void TestStepmismatchInputMonth1()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "month";
			element.RemoveAttribute("required");
			element.RemoveAttribute("pattern");
			element.RemoveAttribute("step");
			element.RemoveAttribute("max");
			element.RemoveAttribute("min");
			element.RemoveAttribute("maxlength");
			element.RemoveAttribute("value");
			element.RemoveAttribute("multiple");
			element.RemoveAttribute("checked");
			element.RemoveAttribute("selected");
			element.SetAttribute("step", "");
			element.Value = "2000-01";
			Assert.AreEqual("month", element.Type);
			Assert.AreEqual(false, element.Validity.IsStepMismatch);
		}
		
		[Test]
		public void TestStepmismatchInputMonth2()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "month";
			element.RemoveAttribute("required");
			element.RemoveAttribute("pattern");
			element.RemoveAttribute("step");
			element.RemoveAttribute("max");
			element.RemoveAttribute("min");
			element.RemoveAttribute("maxlength");
			element.RemoveAttribute("value");
			element.RemoveAttribute("multiple");
			element.RemoveAttribute("checked");
			element.RemoveAttribute("selected");
			element.SetAttribute("step", "2");
			element.Value = "";
			Assert.AreEqual("month", element.Type);
			Assert.AreEqual(false, element.Validity.IsStepMismatch);
		}
		
		[Test]
		public void TestStepmismatchInputMonth3()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "month";
			element.RemoveAttribute("required");
			element.RemoveAttribute("pattern");
			element.RemoveAttribute("step");
			element.RemoveAttribute("max");
			element.RemoveAttribute("min");
			element.RemoveAttribute("maxlength");
			element.RemoveAttribute("value");
			element.RemoveAttribute("multiple");
			element.RemoveAttribute("checked");
			element.RemoveAttribute("selected");
			element.SetAttribute("step", "2");
			element.Value = "1970-03";
			Assert.AreEqual("month", element.Type);
			Assert.AreEqual(false, element.Validity.IsStepMismatch);
		}
		
		[Test]
		public void TestStepmismatchInputMonth4()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "month";
			element.RemoveAttribute("required");
			element.RemoveAttribute("pattern");
			element.RemoveAttribute("step");
			element.RemoveAttribute("max");
			element.RemoveAttribute("min");
			element.RemoveAttribute("maxlength");
			element.RemoveAttribute("value");
			element.RemoveAttribute("multiple");
			element.RemoveAttribute("checked");
			element.RemoveAttribute("selected");
			element.SetAttribute("step", "2");
			element.Value = "1970-04";
			Assert.AreEqual("month", element.Type);
			Assert.AreEqual(true, element.Validity.IsStepMismatch);
		}
		
		[Test]
		public void TestStepmismatchInputWeek1()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "week";
			element.RemoveAttribute("required");
			element.RemoveAttribute("pattern");
			element.RemoveAttribute("step");
			element.RemoveAttribute("max");
			element.RemoveAttribute("min");
			element.RemoveAttribute("maxlength");
			element.RemoveAttribute("value");
			element.RemoveAttribute("multiple");
			element.RemoveAttribute("checked");
			element.RemoveAttribute("selected");
			element.SetAttribute("step", "");
			element.Value = "1970-W01";
			Assert.AreEqual("week", element.Type);
			Assert.AreEqual(false, element.Validity.IsStepMismatch);
		}
		
		[Test]
		public void TestStepmismatchInputWeek2()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "week";
			element.RemoveAttribute("required");
			element.RemoveAttribute("pattern");
			element.RemoveAttribute("step");
			element.RemoveAttribute("max");
			element.RemoveAttribute("min");
			element.RemoveAttribute("maxlength");
			element.RemoveAttribute("value");
			element.RemoveAttribute("multiple");
			element.RemoveAttribute("checked");
			element.RemoveAttribute("selected");
			element.SetAttribute("step", "2");
			element.Value = "";
			Assert.AreEqual("week", element.Type);
			Assert.AreEqual(false, element.Validity.IsStepMismatch);
		}
		
		[Test]
		public void TestStepmismatchInputWeek3()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "week";
			element.RemoveAttribute("required");
			element.RemoveAttribute("pattern");
			element.RemoveAttribute("step");
			element.RemoveAttribute("max");
			element.RemoveAttribute("min");
			element.RemoveAttribute("maxlength");
			element.RemoveAttribute("value");
			element.RemoveAttribute("multiple");
			element.RemoveAttribute("checked");
			element.RemoveAttribute("selected");
			element.SetAttribute("step", "2");
			element.Value = "1970-W03";
			Assert.AreEqual("week", element.Type);
			Assert.AreEqual(false, element.Validity.IsStepMismatch);
		}
		
		[Test]
		public void TestStepmismatchInputWeek4()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "week";
			element.RemoveAttribute("required");
			element.RemoveAttribute("pattern");
			element.RemoveAttribute("step");
			element.RemoveAttribute("max");
			element.RemoveAttribute("min");
			element.RemoveAttribute("maxlength");
			element.RemoveAttribute("value");
			element.RemoveAttribute("multiple");
			element.RemoveAttribute("checked");
			element.RemoveAttribute("selected");
			element.SetAttribute("step", "2");
			element.Value = "1970-W04";
			Assert.AreEqual("week", element.Type);
			Assert.AreEqual(true, element.Validity.IsStepMismatch);
		}
		
		[Test]
		public void TestStepmismatchInputTime1()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "time";
			element.RemoveAttribute("required");
			element.RemoveAttribute("pattern");
			element.RemoveAttribute("step");
			element.RemoveAttribute("max");
			element.RemoveAttribute("min");
			element.RemoveAttribute("maxlength");
			element.RemoveAttribute("value");
			element.RemoveAttribute("multiple");
			element.RemoveAttribute("checked");
			element.RemoveAttribute("selected");
			element.SetAttribute("step", "");
			element.Value = "12:00:00";
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(false, element.Validity.IsStepMismatch);
		}
		
		[Test]
		public void TestStepmismatchInputTime2()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "time";
			element.RemoveAttribute("required");
			element.RemoveAttribute("pattern");
			element.RemoveAttribute("step");
			element.RemoveAttribute("max");
			element.RemoveAttribute("min");
			element.RemoveAttribute("maxlength");
			element.RemoveAttribute("value");
			element.RemoveAttribute("multiple");
			element.RemoveAttribute("checked");
			element.RemoveAttribute("selected");
			element.SetAttribute("step", "120");
			element.Value = "";
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(false, element.Validity.IsStepMismatch);
		}
		
		[Test]
		public void TestStepmismatchInputTime3()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "time";
			element.RemoveAttribute("required");
			element.RemoveAttribute("pattern");
			element.RemoveAttribute("step");
			element.RemoveAttribute("max");
			element.RemoveAttribute("min");
			element.RemoveAttribute("maxlength");
			element.RemoveAttribute("value");
			element.RemoveAttribute("multiple");
			element.RemoveAttribute("checked");
			element.RemoveAttribute("selected");
			element.SetAttribute("step", "120");
			element.Value = "12:02:00";
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(false, element.Validity.IsStepMismatch);
		}
		
		[Test]
		public void TestStepmismatchInputTime4()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "time";
			element.RemoveAttribute("required");
			element.RemoveAttribute("pattern");
			element.RemoveAttribute("step");
			element.RemoveAttribute("max");
			element.RemoveAttribute("min");
			element.RemoveAttribute("maxlength");
			element.RemoveAttribute("value");
			element.RemoveAttribute("multiple");
			element.RemoveAttribute("checked");
			element.RemoveAttribute("selected");
			element.SetAttribute("step", "120");
			element.Value = "12:03:00";
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(true, element.Validity.IsStepMismatch);
		}
		
		[Test]
		public void TestStepmismatchInputNumber1()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "number";
			element.RemoveAttribute("required");
			element.RemoveAttribute("pattern");
			element.RemoveAttribute("step");
			element.RemoveAttribute("max");
			element.RemoveAttribute("min");
			element.RemoveAttribute("maxlength");
			element.RemoveAttribute("value");
			element.RemoveAttribute("multiple");
			element.RemoveAttribute("checked");
			element.RemoveAttribute("selected");
			element.SetAttribute("step", "");
			element.Value = "1";
			Assert.AreEqual("number", element.Type);
			Assert.AreEqual(false, element.Validity.IsStepMismatch);
		}
		
		[Test]
		public void TestStepmismatchInputNumber2()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "number";
			element.RemoveAttribute("required");
			element.RemoveAttribute("pattern");
			element.RemoveAttribute("step");
			element.RemoveAttribute("max");
			element.RemoveAttribute("min");
			element.RemoveAttribute("maxlength");
			element.RemoveAttribute("value");
			element.RemoveAttribute("multiple");
			element.RemoveAttribute("checked");
			element.RemoveAttribute("selected");
			element.SetAttribute("step", "2");
			element.Value = "";
			Assert.AreEqual("number", element.Type);
			Assert.AreEqual(false, element.Validity.IsStepMismatch);
		}
		
		[Test]
		public void TestStepmismatchInputNumber3()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "number";
			element.RemoveAttribute("required");
			element.RemoveAttribute("pattern");
			element.RemoveAttribute("step");
			element.RemoveAttribute("max");
			element.RemoveAttribute("min");
			element.RemoveAttribute("maxlength");
			element.RemoveAttribute("value");
			element.RemoveAttribute("multiple");
			element.RemoveAttribute("checked");
			element.RemoveAttribute("selected");
			element.SetAttribute("step", "2");
			element.Value = "2";
			Assert.AreEqual("number", element.Type);
			Assert.AreEqual(false, element.Validity.IsStepMismatch);
		}
		
		[Test]
		public void TestStepmismatchInputNumber4()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "number";
			element.RemoveAttribute("required");
			element.RemoveAttribute("pattern");
			element.RemoveAttribute("step");
			element.RemoveAttribute("max");
			element.RemoveAttribute("min");
			element.RemoveAttribute("maxlength");
			element.RemoveAttribute("value");
			element.RemoveAttribute("multiple");
			element.RemoveAttribute("checked");
			element.RemoveAttribute("selected");
			element.SetAttribute("step", "2");
			element.Value = "3";
			Assert.AreEqual("number", element.Type);
			Assert.AreEqual(true, element.Validity.IsStepMismatch);
		}
	}
}