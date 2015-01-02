using AngleSharp;
using AngleSharp.DOM.Html;
using NUnit.Framework;

namespace UnitTests.Html
{
    /// <summary>
    /// Tests generated according to the W3C-Test.org page:
    /// http://www.w3c-test.org/html/semantics/forms/constraints/form-validation-validity-rangeUnderflow.html
    /// </summary>
    [TestFixture]
	public class ValidityRangeUnderflowTests
	{
		[Test]
		public void TestRangeunderflowInputDatetime1()
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
			element.SetAttribute("min", "");
			element.SetAttribute("value", "2000-01-01T12:00:00Z");
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputDatetime2()
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
			element.SetAttribute("min", "2000-01-01T12:00:00Z");
			element.SetAttribute("value", "");
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputDatetime3()
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
			element.SetAttribute("min", "2001-01-01  12:00:00Z");
			element.SetAttribute("value", "2000-01-01T12:00:00Z");
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputDatetime4()
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
			element.SetAttribute("min", "2000-01-01T11:00:00Z");
			element.SetAttribute("value", "2000-01-01T12:00:00Z");
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputDatetime5()
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
			element.SetAttribute("min", "2001-01-01T23:59:59Z");
			element.SetAttribute("value", "2000-01-01T24:00:00Z");
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputDatetime6()
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
			element.SetAttribute("min", "1980-01-01T12:00Z");
			element.SetAttribute("value", "79-01-01T12:00Z");
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputDatetime7()
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
			element.SetAttribute("min", "2000-01-01T13:00:00Z");
			element.SetAttribute("value", "2000-01-01T12:00:00Z");
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(true, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputDatetime8()
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
			element.SetAttribute("min", "2000-01-01T12:00:00.2Z");
			element.SetAttribute("value", "2000-01-01T12:00:00.1Z");
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(true, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputDatetime9()
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
			element.SetAttribute("min", "2000-01-01T12:00:00.02Z");
			element.SetAttribute("value", "2000-01-01T12:00:00.01Z");
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(true, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputDatetime10()
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
			element.SetAttribute("min", "2000-01-01T12:00:00.002Z");
			element.SetAttribute("value", "2000-01-01T12:00:00.001Z");
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(true, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputDatetime11()
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
			element.SetAttribute("min", "9999-01-01T12:00:00Z");
			element.SetAttribute("value", "2000-01-01T12:00:00Z");
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(true, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputDatetime12()
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
			element.SetAttribute("min", "8593-01-01T02:09+02:09");
			element.SetAttribute("value", "8592-01-01T02:09+02:09");
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(true, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputDate1()
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
			element.SetAttribute("min", "");
			element.SetAttribute("value", "2000-01-01");
			Assert.AreEqual("date", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputDate2()
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
			element.SetAttribute("min", "2000-01-01");
			element.SetAttribute("value", "");
			Assert.AreEqual("date", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputDate3()
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
			element.SetAttribute("min", "2001/01/01");
			element.SetAttribute("value", "2000-01-01");
			Assert.AreEqual("date", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputDate4()
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
			element.SetAttribute("min", "2000-02-02");
			element.SetAttribute("value", "2000-1-1");
			Assert.AreEqual("date", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputDate5()
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
			element.SetAttribute("min", "988-01-01");
			element.SetAttribute("value", "987-01-01");
			Assert.AreEqual("date", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputDate6()
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
			element.SetAttribute("min", "2001-01-01");
			element.SetAttribute("value", "2000-13-01");
			Assert.AreEqual("date", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputDate7()
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
			element.SetAttribute("min", "2001-01-01");
			element.SetAttribute("value", "2000-02-30");
			Assert.AreEqual("date", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputDate8()
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
			element.SetAttribute("min", "2000-01-01");
			element.SetAttribute("value", "2000-12-01");
			Assert.AreEqual("date", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputDate9()
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
			element.SetAttribute("min", "2000-12-01");
			element.SetAttribute("value", "2000-01-01");
			Assert.AreEqual("date", element.Type);
			Assert.AreEqual(true, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputDate10()
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
			element.SetAttribute("min", "9999-01-02");
			element.SetAttribute("value", "9999-01-01");
			Assert.AreEqual("date", element.Type);
			Assert.AreEqual(true, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputMonth1()
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
			element.SetAttribute("min", "");
			element.SetAttribute("value", "2000-01");
			Assert.AreEqual("month", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputMonth2()
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
			element.SetAttribute("min", "2000-01");
			element.SetAttribute("value", "");
			Assert.AreEqual("month", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputMonth3()
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
			element.SetAttribute("min", "2001/01");
			element.SetAttribute("value", "2000-02");
			Assert.AreEqual("month", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputMonth4()
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
			element.SetAttribute("min", "2000-02");
			element.SetAttribute("value", "2000-1");
			Assert.AreEqual("month", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputMonth5()
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
			element.SetAttribute("min", "988-01");
			element.SetAttribute("value", "987-01");
			Assert.AreEqual("month", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputMonth6()
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
			element.SetAttribute("min", "2001-01");
			element.SetAttribute("value", "2000-13");
			Assert.AreEqual("month", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputMonth7()
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
			element.SetAttribute("min", "2000-01");
			element.SetAttribute("value", "2000-12");
			Assert.AreEqual("month", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputMonth8()
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
			element.SetAttribute("min", "2001-01");
			element.SetAttribute("value", "2000-12");
			Assert.AreEqual("month", element.Type);
			Assert.AreEqual(true, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputMonth9()
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
			element.SetAttribute("min", "9999-01");
			element.SetAttribute("value", "2000-01");
			Assert.AreEqual("month", element.Type);
			Assert.AreEqual(true, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputWeek1()
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
			element.SetAttribute("min", "");
			element.SetAttribute("value", "2000-W01");
			Assert.AreEqual("week", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputWeek2()
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
			element.SetAttribute("min", "2000-W01");
			element.SetAttribute("value", "");
			Assert.AreEqual("week", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputWeek3()
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
			element.SetAttribute("min", "2001/W02");
			element.SetAttribute("value", "2000-W01");
			Assert.AreEqual("week", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputWeek4()
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
			element.SetAttribute("min", "2001-W02");
			element.SetAttribute("value", "2000-W1");
			Assert.AreEqual("week", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputWeek5()
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
			element.SetAttribute("min", "2001-W02");
			element.SetAttribute("value", "2000-w01");
			Assert.AreEqual("week", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputWeek6()
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
			element.SetAttribute("min", "988-W01");
			element.SetAttribute("value", "987-W01");
			Assert.AreEqual("week", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputWeek7()
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
			element.SetAttribute("min", "2001-W01");
			element.SetAttribute("value", "2000-W57");
			Assert.AreEqual("week", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputWeek8()
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
			element.SetAttribute("min", "2000-W01");
			element.SetAttribute("value", "2000-W12");
			Assert.AreEqual("week", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputWeek9()
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
			element.SetAttribute("min", "2000-W12");
			element.SetAttribute("value", "2000-W01");
			Assert.AreEqual("week", element.Type);
			Assert.AreEqual(true, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputWeek10()
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
			element.SetAttribute("min", "9999-W01");
			element.SetAttribute("value", "2000-W01");
			Assert.AreEqual("week", element.Type);
			Assert.AreEqual(true, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputTime1()
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
			element.SetAttribute("min", "");
			element.SetAttribute("value", "12:00:00");
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputTime2()
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
			element.SetAttribute("min", "12:00:00");
			element.SetAttribute("value", "");
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputTime3()
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
			element.SetAttribute("min", "12.00.01");
			element.SetAttribute("value", "12:00:00");
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputTime4()
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
			element.SetAttribute("min", "12:00:01");
			element.SetAttribute("value", "12.00.00");
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputTime5()
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
			element.SetAttribute("min", "12:00:00");
			element.SetAttribute("value", "13:00:00");
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputTime6()
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
			element.SetAttribute("min", "13:00:00");
			element.SetAttribute("value", "12");
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputTime7()
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
			element.SetAttribute("min", "12:00:02");
			element.SetAttribute("value", "12:00:00");
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(true, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputTime8()
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
			element.SetAttribute("min", "12:00:00.2");
			element.SetAttribute("value", "12:00:00.1");
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(true, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputTime9()
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
			element.SetAttribute("min", "12:00:00.02");
			element.SetAttribute("value", "12:00:00.01");
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(true, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputTime10()
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
			element.SetAttribute("min", "12:00:00.002");
			element.SetAttribute("value", "12:00:00.001");
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(true, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputTime11()
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
			element.SetAttribute("min", "12:00:00");
			element.SetAttribute("value", "11:59");
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(true, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputNumber1()
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
			element.SetAttribute("min", "");
			element.SetAttribute("value", "10");
			Assert.AreEqual("number", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputNumber2()
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
			element.SetAttribute("min", "5");
			element.SetAttribute("value", "");
			Assert.AreEqual("number", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputNumber3()
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
			element.SetAttribute("min", "4");
			element.SetAttribute("value", "5");
			Assert.AreEqual("number", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputNumber4()
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
			element.SetAttribute("min", "-5.6");
			element.SetAttribute("value", "-5.5");
			Assert.AreEqual("number", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputNumber5()
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
			element.SetAttribute("min", "0");
			element.SetAttribute("value", "-0");
			Assert.AreEqual("number", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputNumber6()
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
			element.SetAttribute("min", "5");
			element.SetAttribute("value", "6abc");
			Assert.AreEqual("number", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputNumber7()
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
			element.SetAttribute("min", "6");
			element.SetAttribute("value", "5");
			Assert.AreEqual("number", element.Type);
			Assert.AreEqual(true, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputNumber8()
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
			element.SetAttribute("min", "-5.4");
			element.SetAttribute("value", "-5.5");
			Assert.AreEqual("number", element.Type);
			Assert.AreEqual(true, element.Validity.IsRangeUnderflow);
		}
		
		[Test]
		public void TestRangeunderflowInputNumber9()
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
			element.SetAttribute("min", "5e+2");
			element.SetAttribute("value", "-5e-1");
			Assert.AreEqual("number", element.Type);
			Assert.AreEqual(true, element.Validity.IsRangeUnderflow);
		}
		
	}
}