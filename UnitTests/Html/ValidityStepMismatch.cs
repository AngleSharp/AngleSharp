using AngleSharp;
using AngleSharp.DOM.Html;
using NUnit.Framework;

namespace UnitTests.Html
{
    /// <summary>
    /// Tests generated according to the W3C-Test.org page:
    /// http://www.w3c-test.org/html/semantics/forms/constraints/form-validation-validity-stepMismatch.html
    /// </summary>
    [TestFixture]
	public class ValidityStepMismatchTests
	{
		[Test]
		public void TestStepmismatchInputDatetime1()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("value", "2000-01-01T12:00:00Z");
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(false, element.Validity.IsStepMismatch);
		}
		
		[Test]
		public void TestStepmismatchInputDatetime2()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("value", "");
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(false, element.Validity.IsStepMismatch);
		}
		
		[Test]
		public void TestStepmismatchInputDatetime3()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("value", "2000-01-01T12:58Z");
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(false, element.Validity.IsStepMismatch);
		}
		
		[Test]
		public void TestStepmismatchInputDatetime4()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("value", "2000-01-01T12:59Z");
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(true, element.Validity.IsStepMismatch);
		}
		
		[Test]
		public void TestStepmismatchInputDate1()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("value", "2000-01-01");
			Assert.AreEqual("date", element.Type);
			Assert.AreEqual(false, element.Validity.IsStepMismatch);
		}
		
		[Test]
		public void TestStepmismatchInputDate2()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("value", "");
			Assert.AreEqual("date", element.Type);
			Assert.AreEqual(false, element.Validity.IsStepMismatch);
		}
		
		[Test]
		public void TestStepmismatchInputDate3()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("value", "1970-01-02");
			Assert.AreEqual("date", element.Type);
			Assert.AreEqual(false, element.Validity.IsStepMismatch);
		}
		
		[Test]
		public void TestStepmismatchInputDate4()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("value", "1970-01-03");
			Assert.AreEqual("date", element.Type);
			Assert.AreEqual(true, element.Validity.IsStepMismatch);
		}
		
		[Test]
		public void TestStepmismatchInputMonth1()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("value", "2000-01");
			Assert.AreEqual("month", element.Type);
			Assert.AreEqual(false, element.Validity.IsStepMismatch);
		}
		
		[Test]
		public void TestStepmismatchInputMonth2()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("value", "");
			Assert.AreEqual("month", element.Type);
			Assert.AreEqual(false, element.Validity.IsStepMismatch);
		}
		
		[Test]
		public void TestStepmismatchInputMonth3()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("value", "1970-03");
			Assert.AreEqual("month", element.Type);
			Assert.AreEqual(false, element.Validity.IsStepMismatch);
		}
		
		[Test]
		public void TestStepmismatchInputMonth4()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("value", "1970-04");
			Assert.AreEqual("month", element.Type);
			Assert.AreEqual(true, element.Validity.IsStepMismatch);
		}
		
		[Test]
		public void TestStepmismatchInputWeek1()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("value", "1970-W01");
			Assert.AreEqual("week", element.Type);
			Assert.AreEqual(false, element.Validity.IsStepMismatch);
		}
		
		[Test]
		public void TestStepmismatchInputWeek2()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("value", "");
			Assert.AreEqual("week", element.Type);
			Assert.AreEqual(false, element.Validity.IsStepMismatch);
		}
		
		[Test]
		public void TestStepmismatchInputWeek3()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("value", "1970-W03");
			Assert.AreEqual("week", element.Type);
			Assert.AreEqual(false, element.Validity.IsStepMismatch);
		}
		
		[Test]
		public void TestStepmismatchInputWeek4()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("value", "1970-W04");
			Assert.AreEqual("week", element.Type);
			Assert.AreEqual(true, element.Validity.IsStepMismatch);
		}
		
		[Test]
		public void TestStepmismatchInputTime1()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("value", "12:00:00");
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(false, element.Validity.IsStepMismatch);
		}
		
		[Test]
		public void TestStepmismatchInputTime2()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("value", "");
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(false, element.Validity.IsStepMismatch);
		}
		
		[Test]
		public void TestStepmismatchInputTime3()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("value", "12:02:00");
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(false, element.Validity.IsStepMismatch);
		}
		
		[Test]
		public void TestStepmismatchInputTime4()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("value", "12:03:00");
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(true, element.Validity.IsStepMismatch);
		}
		
		[Test]
		public void TestStepmismatchInputNumber1()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("value", "1");
			Assert.AreEqual("number", element.Type);
			Assert.AreEqual(false, element.Validity.IsStepMismatch);
		}
		
		[Test]
		public void TestStepmismatchInputNumber2()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("value", "");
			Assert.AreEqual("number", element.Type);
			Assert.AreEqual(false, element.Validity.IsStepMismatch);
		}
		
		[Test]
		public void TestStepmismatchInputNumber3()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("value", "2");
			Assert.AreEqual("number", element.Type);
			Assert.AreEqual(false, element.Validity.IsStepMismatch);
		}
		
		[Test]
		public void TestStepmismatchInputNumber4()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("value", "3");
			Assert.AreEqual("number", element.Type);
			Assert.AreEqual(true, element.Validity.IsStepMismatch);
		}
	}
}