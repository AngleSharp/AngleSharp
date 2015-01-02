using AngleSharp;
using AngleSharp.DOM.Html;
using NUnit.Framework;

namespace UnitTests.Html
{
    /// <summary>
    /// Tests generated according to the W3C-Test.org page:
    /// http://www.w3c-test.org/html/semantics/forms/constraints/form-validation-validity-rangeOverflow.html
    /// </summary>
    [TestFixture]
	public class ValidityRangeOverflowTests
	{
		[Test]
		public void TestRangeoverflowInputDatetime1()
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
			element.SetAttribute("max", "");
			element.SetAttribute("value", "2000-01-01T12:00:00Z");
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputDatetime2()
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
			element.SetAttribute("max", "2000-01-01T12:00:00Z");
			element.SetAttribute("value", "");
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputDatetime3()
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
			element.SetAttribute("max", "2000-01-01  12:00:00Z");
			element.SetAttribute("value", "2001-01-01T12:00:00Z");
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputDatetime4()
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
			element.SetAttribute("max", "2000-01-01T12:00:00Z");
			element.SetAttribute("value", "2000-01-01T11:00:00Z");
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputDatetime5()
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
			element.SetAttribute("max", "2000-01-01T23:59:59Z");
			element.SetAttribute("value", "2001-01-01T24:00:00Z");
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputDatetime6()
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
			element.SetAttribute("max", "1970-01-01T12:00Z");
			element.SetAttribute("value", "80-01-01T12:00Z");
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputDatetime7()
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
			element.SetAttribute("max", "2000-01-01T12:00:00Z");
			element.SetAttribute("value", "2001-01-01T13:00:00Z");
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(true, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputDatetime8()
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
			element.SetAttribute("max", "2000-01-01T12:00:00.1Z");
			element.SetAttribute("value", "2000-01-01T12:00:00.2Z");
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(true, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputDatetime9()
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
			element.SetAttribute("max", "2000-01-01T12:00:00.01Z");
			element.SetAttribute("value", "2000-01-01T12:00:00.02Z");
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(true, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputDatetime10()
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
			element.SetAttribute("max", "2000-01-01T12:00:00.001Z");
			element.SetAttribute("value", "2000-01-01T12:00:00.002Z");
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(true, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputDatetime11()
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
			element.SetAttribute("max", "2000-01-01T12:00:00");
			element.SetAttribute("value", "9999-01-01T12:00:00");
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(true, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputDatetime12()
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
			element.SetAttribute("max", "8592-01-01T02:09+02:09");
			element.SetAttribute("value", "8593-01-01T02:09+02:09");
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(true, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputDate1()
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
			element.SetAttribute("max", "");
			element.SetAttribute("value", "2000-01-01");
			Assert.AreEqual("date", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputDate2()
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
			element.SetAttribute("max", "2000-01-01");
			element.SetAttribute("value", "");
			Assert.AreEqual("date", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputDate3()
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
			element.SetAttribute("max", "2000/01/01");
			element.SetAttribute("value", "2002-01-01");
			Assert.AreEqual("date", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputDate4()
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
			element.SetAttribute("max", "2000-01-01");
			element.SetAttribute("value", "2000-2-2");
			Assert.AreEqual("date", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputDate5()
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
			element.SetAttribute("max", "987-01-01");
			element.SetAttribute("value", "988-01-01");
			Assert.AreEqual("date", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputDate6()
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
			element.SetAttribute("max", "2000-01-01");
			element.SetAttribute("value", "2000-13-01");
			Assert.AreEqual("date", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputDate7()
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
			element.SetAttribute("max", "2000-01-01");
			element.SetAttribute("value", "2000-02-30");
			Assert.AreEqual("date", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputDate8()
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
			element.SetAttribute("max", "2000-12-01");
			element.SetAttribute("value", "2000-01-01");
			Assert.AreEqual("date", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputDate9()
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
			element.SetAttribute("max", "2000-12-01");
			element.SetAttribute("value", "2001-01-01");
			Assert.AreEqual("date", element.Type);
			Assert.AreEqual(true, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputDate10()
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
			element.SetAttribute("max", "9999-01-01");
			element.SetAttribute("value", "9999-01-02");
			Assert.AreEqual("date", element.Type);
			Assert.AreEqual(true, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputMonth1()
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
			element.SetAttribute("max", "");
			element.SetAttribute("value", "2000-01");
			Assert.AreEqual("month", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputMonth2()
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
			element.SetAttribute("max", "2000-01");
			element.SetAttribute("value", "");
			Assert.AreEqual("month", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputMonth3()
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
			element.SetAttribute("max", "2000/01");
			element.SetAttribute("value", "2001-02");
			Assert.AreEqual("month", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputMonth4()
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
			element.SetAttribute("max", "2000-01");
			element.SetAttribute("value", "2000-1");
			Assert.AreEqual("month", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputMonth5()
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
			element.SetAttribute("max", "987-01");
			element.SetAttribute("value", "988-01");
			Assert.AreEqual("month", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputMonth6()
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
			element.SetAttribute("max", "2000-01");
			element.SetAttribute("value", "2000-13");
			Assert.AreEqual("month", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputMonth7()
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
			element.SetAttribute("max", "2000-12");
			element.SetAttribute("value", "2000-01");
			Assert.AreEqual("month", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputMonth8()
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
			element.SetAttribute("max", "2000-01");
			element.SetAttribute("value", "2000-12");
			Assert.AreEqual("month", element.Type);
			Assert.AreEqual(true, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputMonth9()
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
			element.SetAttribute("max", "9999-01");
			element.SetAttribute("value", "9999-02");
			Assert.AreEqual("month", element.Type);
			Assert.AreEqual(true, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputWeek1()
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
			element.SetAttribute("max", "");
			element.SetAttribute("value", "2000-W01");
			Assert.AreEqual("week", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputWeek2()
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
			element.SetAttribute("max", "2000-W01");
			element.SetAttribute("value", "");
			Assert.AreEqual("week", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputWeek3()
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
			element.SetAttribute("max", "2000/W01");
			element.SetAttribute("value", "2001-W02");
			Assert.AreEqual("week", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputWeek4()
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
			element.SetAttribute("max", "2000-W01");
			element.SetAttribute("value", "2000-W2");
			Assert.AreEqual("week", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputWeek5()
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
			element.SetAttribute("max", "2000-W01");
			element.SetAttribute("value", "2000-w02");
			Assert.AreEqual("week", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputWeek6()
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
			element.SetAttribute("max", "987-W01");
			element.SetAttribute("value", "988-W01");
			Assert.AreEqual("week", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputWeek7()
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
			element.SetAttribute("max", "2000-W01");
			element.SetAttribute("value", "2000-W57");
			Assert.AreEqual("week", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputWeek8()
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
			element.SetAttribute("max", "2000-W12");
			element.SetAttribute("value", "2000-W01");
			Assert.AreEqual("week", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputWeek9()
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
			element.SetAttribute("max", "2000-W01");
			element.SetAttribute("value", "2000-W12");
			Assert.AreEqual("week", element.Type);
			Assert.AreEqual(true, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputWeek10()
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
			element.SetAttribute("max", "9999-W01");
			element.SetAttribute("value", "9999-W02");
			Assert.AreEqual("week", element.Type);
			Assert.AreEqual(true, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputTime1()
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
			element.SetAttribute("max", "");
			element.SetAttribute("value", "12:00:00");
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputTime2()
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
			element.SetAttribute("max", "12:00:00");
			element.SetAttribute("value", "");
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputTime3()
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
			element.SetAttribute("max", "12.00.00");
			element.SetAttribute("value", "12:00:01");
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputTime4()
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
			element.SetAttribute("max", "12:00:00");
			element.SetAttribute("value", "12.00.01");
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputTime5()
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
			element.SetAttribute("max", "23:59:59");
			element.SetAttribute("value", "24:00:00");
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputTime6()
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
			element.SetAttribute("max", "23:59:59");
			element.SetAttribute("value", "23:60:00");
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputTime7()
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
			element.SetAttribute("max", "23:59:59");
			element.SetAttribute("value", "23:59:60");
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputTime8()
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
			element.SetAttribute("max", "13:00:00");
			element.SetAttribute("value", "12:00:00");
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputTime9()
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
			element.SetAttribute("max", "12:00:00");
			element.SetAttribute("value", "13");
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputTime10()
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
			element.SetAttribute("max", "12:00:00");
			element.SetAttribute("value", "12:00:02");
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(true, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputTime11()
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
			element.SetAttribute("max", "12:00:00.1");
			element.SetAttribute("value", "12:00:00.2");
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(true, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputTime12()
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
			element.SetAttribute("max", "12:00:00.01");
			element.SetAttribute("value", "12:00:00.02");
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(true, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputTime13()
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
			element.SetAttribute("max", "12:00:00.001");
			element.SetAttribute("value", "12:00:00.002");
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(true, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputTime14()
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
			element.SetAttribute("max", "12:00:00");
			element.SetAttribute("value", "12:01");
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(true, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputNumber1()
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
			element.SetAttribute("max", "");
			element.SetAttribute("value", "10");
			Assert.AreEqual("number", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputNumber2()
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
			element.SetAttribute("max", "5");
			element.SetAttribute("value", "");
			Assert.AreEqual("number", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputNumber3()
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
			element.SetAttribute("max", "5");
			element.SetAttribute("value", "4");
			Assert.AreEqual("number", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputNumber4()
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
			element.SetAttribute("max", "-5.5");
			element.SetAttribute("value", "-5.6");
			Assert.AreEqual("number", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputNumber5()
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
			element.SetAttribute("max", "-0");
			element.SetAttribute("value", "0");
			Assert.AreEqual("number", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputNumber6()
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
			element.SetAttribute("max", "5");
			element.SetAttribute("value", "1abc");
			Assert.AreEqual("number", element.Type);
			Assert.AreEqual(false, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputNumber7()
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
			element.SetAttribute("max", "5");
			element.SetAttribute("value", "6");
			Assert.AreEqual("number", element.Type);
			Assert.AreEqual(true, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputNumber8()
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
			element.SetAttribute("max", "-5.5");
			element.SetAttribute("value", "-5.4");
			Assert.AreEqual("number", element.Type);
			Assert.AreEqual(true, element.Validity.IsRangeOverflow);
		}
		
		[Test]
		public void TestRangeoverflowInputNumber9()
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
			element.SetAttribute("max", "-5e-1");
			element.SetAttribute("value", "5e+2");
			Assert.AreEqual("number", element.Type);
			Assert.AreEqual(true, element.Validity.IsRangeOverflow);
		}
		
	}
}