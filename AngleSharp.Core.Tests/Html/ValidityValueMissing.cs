using System;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using NUnit.Framework;

namespace AngleSharp.Core.Tests.Html
{
    /// <summary>
    /// Tests generated according to the W3C-Test.org page:
    /// http://www.w3c-test.org/html/semantics/forms/constraints/form-validation-validity-valueMissing.html
    /// </summary>
	[TestFixture]
	public class ValidityValueMissingTests
    {
        static IDocument Html(String code)
        {
            return code.ToHtmlDocument();
        }

        [Test]
		public void TestValuemissingInputText1()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "text";
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
			element.SetAttribute("required", null);
			element.Value = "";
			Assert.AreEqual("text", element.Type);
			Assert.AreEqual(false, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputText2()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "text";
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
			element.SetAttribute("required", "required");
			element.Value = "abc";
			Assert.AreEqual("text", element.Type);
			Assert.AreEqual(false, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputText3()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "text";
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
			element.SetAttribute("required", "required");
			element.Value = "";
			Assert.AreEqual("text", element.Type);
			Assert.AreEqual(true, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputSearch1()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "search";
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
			element.SetAttribute("required", null);
			element.Value = "";
			Assert.AreEqual("search", element.Type);
			Assert.AreEqual(false, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputSearch2()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "search";
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
			element.SetAttribute("required", "required");
			element.Value = "abc";
			Assert.AreEqual("search", element.Type);
			Assert.AreEqual(false, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputSearch3()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "search";
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
			element.SetAttribute("required", "required");
			element.Value = "";
			Assert.AreEqual("search", element.Type);
			Assert.AreEqual(true, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputTel1()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "tel";
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
			element.SetAttribute("required", null);
			element.Value = "";
			Assert.AreEqual("tel", element.Type);
			Assert.AreEqual(false, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputTel2()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "tel";
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
			element.SetAttribute("required", "required");
			element.Value = "abc";
			Assert.AreEqual("tel", element.Type);
			Assert.AreEqual(false, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputTel3()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "tel";
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
			element.SetAttribute("required", "required");
			element.Value = "";
			Assert.AreEqual("tel", element.Type);
			Assert.AreEqual(true, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputUrl1()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "url";
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
			element.SetAttribute("required", null);
			element.Value = "";
			Assert.AreEqual("url", element.Type);
			Assert.AreEqual(false, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputUrl2()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "url";
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
			element.SetAttribute("required", "required");
			element.Value = "abc";
			Assert.AreEqual("url", element.Type);
			Assert.AreEqual(false, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputUrl3()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "url";
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
			element.SetAttribute("required", "required");
			element.Value = "";
			Assert.AreEqual("url", element.Type);
			Assert.AreEqual(true, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputEmail1()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "email";
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
			element.SetAttribute("required", null);
			element.Value = "";
			Assert.AreEqual("email", element.Type);
			Assert.AreEqual(false, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputEmail2()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "email";
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
			element.SetAttribute("required", "required");
			element.Value = "abc";
			Assert.AreEqual("email", element.Type);
			Assert.AreEqual(false, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputEmail3()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "email";
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
			element.SetAttribute("required", "required");
			element.Value = "";
			Assert.AreEqual("email", element.Type);
			Assert.AreEqual(true, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputPassword1()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "password";
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
			element.SetAttribute("required", null);
			element.Value = "";
			Assert.AreEqual("password", element.Type);
			Assert.AreEqual(false, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputPassword2()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "password";
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
			element.SetAttribute("required", "required");
			element.Value = "abc";
			Assert.AreEqual("password", element.Type);
			Assert.AreEqual(false, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputPassword3()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "password";
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
			element.SetAttribute("required", "required");
			element.Value = "";
			Assert.AreEqual("password", element.Type);
			Assert.AreEqual(true, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputDatetime1()
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
			element.SetAttribute("required", null);
			element.Value = "";
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(false, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputDatetime2()
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
			element.SetAttribute("required", "required");
			element.Value = "2000-12-10T12:00:00Z";
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(false, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputDatetime3()
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
			element.SetAttribute("required", "required");
			element.Value = "2000-12-10 12:00Z";
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(false, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputDatetime4()
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
			element.SetAttribute("required", "required");
			element.Value = "1979-10-14T12:00:00.001-04:00";
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(false, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputDatetime5()
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
			element.SetAttribute("required", "required");
			element.Value = "8592-01-01T02:09+02:09";
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(false, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputDatetime6()
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
			element.SetAttribute("required", "required");
			element.Value = "1234567";
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(true, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputDatetime7()
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
			element.SetAttribute("required", "required");
			element.Value = "2015-01-01T14:11:21.695Z";
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(false, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputDatetime8()
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
			element.SetAttribute("required", "required");
			element.Value = "1979-10-99 99:99Z";
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(true, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputDatetime9()
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
			element.SetAttribute("required", "required");
			element.Value = "1979-10-14 12:00:00";
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(true, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputDatetime10()
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
			element.SetAttribute("required", "required");
			element.Value = "2001-12-21  12:00Z";
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(true, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputDatetime11()
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
			element.SetAttribute("required", "required");
			element.Value = "abc";
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(true, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputDatetime12()
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
			element.SetAttribute("required", "required");
			element.Value = "";
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(true, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputDate1()
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
			element.SetAttribute("required", null);
			element.Value = "";
			Assert.AreEqual("date", element.Type);
			Assert.AreEqual(false, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputDate2()
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
			element.SetAttribute("required", "required");
			element.Value = "2000-12-10";
			Assert.AreEqual("date", element.Type);
			Assert.AreEqual(false, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputDate3()
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
			element.SetAttribute("required", "required");
			element.Value = "9999-01-01";
			Assert.AreEqual("date", element.Type);
			Assert.AreEqual(false, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputDate4()
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
			element.SetAttribute("required", "required");
			element.Value = "1234567";
			Assert.AreEqual("date", element.Type);
			Assert.AreEqual(true, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputDate5()
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
			element.SetAttribute("required", "required");
			element.Value = "2015-01-01T14:11:21.695Z";
			Assert.AreEqual("date", element.Type);
			Assert.AreEqual(true, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputDate6()
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
			element.SetAttribute("required", "required");
			element.Value = "9999-99-99";
			Assert.AreEqual("date", element.Type);
			Assert.AreEqual(true, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputDate7()
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
			element.SetAttribute("required", "required");
			element.Value = "37/01/01";
			Assert.AreEqual("date", element.Type);
			Assert.AreEqual(true, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputDate8()
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
			element.SetAttribute("required", "required");
			element.Value = "2000/01/01";
			Assert.AreEqual("date", element.Type);
			Assert.AreEqual(true, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputDate9()
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
			element.SetAttribute("required", "required");
			element.Value = "";
			Assert.AreEqual("date", element.Type);
			Assert.AreEqual(true, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputMonth1()
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
			element.SetAttribute("required", null);
			element.Value = "";
			Assert.AreEqual("month", element.Type);
			Assert.AreEqual(false, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputMonth2()
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
			element.SetAttribute("required", "required");
			element.Value = "2000-12";
			Assert.AreEqual("month", element.Type);
			Assert.AreEqual(false, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputMonth3()
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
			element.SetAttribute("required", "required");
			element.Value = "9999-01";
			Assert.AreEqual("month", element.Type);
			Assert.AreEqual(false, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputMonth4()
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
			element.SetAttribute("required", "required");
			element.Value = "1234567";
			Assert.AreEqual("month", element.Type);
			Assert.AreEqual(true, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputMonth5()
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
			element.SetAttribute("required", "required");
			element.Value = "2015-01-01T14:11:21.695Z";
			Assert.AreEqual("month", element.Type);
			Assert.AreEqual(true, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputMonth6()
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
			element.SetAttribute("required", "required");
			element.Value = "2000-99";
			Assert.AreEqual("month", element.Type);
			Assert.AreEqual(true, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputMonth7()
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
			element.SetAttribute("required", "required");
			element.Value = "37-01";
			Assert.AreEqual("month", element.Type);
			Assert.AreEqual(true, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputMonth8()
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
			element.SetAttribute("required", "required");
			element.Value = "2000/01";
			Assert.AreEqual("month", element.Type);
			Assert.AreEqual(true, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputMonth9()
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
			element.SetAttribute("required", "required");
			element.Value = "";
			Assert.AreEqual("month", element.Type);
			Assert.AreEqual(true, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputWeek1()
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
			element.SetAttribute("required", null);
			element.Value = "";
			Assert.AreEqual("week", element.Type);
			Assert.AreEqual(false, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputWeek2()
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
			element.SetAttribute("required", "required");
			element.Value = "2000-W12";
			Assert.AreEqual("week", element.Type);
			Assert.AreEqual(false, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputWeek3()
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
			element.SetAttribute("required", "required");
			element.Value = "9999-W01";
			Assert.AreEqual("week", element.Type);
			Assert.AreEqual(false, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputWeek4()
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
			element.SetAttribute("required", "required");
			element.Value = "1234567";
			Assert.AreEqual("week", element.Type);
			Assert.AreEqual(true, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputWeek5()
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
			element.SetAttribute("required", "required");
			element.Value = "2015-01-01T14:11:21.695Z";
			Assert.AreEqual("week", element.Type);
			Assert.AreEqual(true, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputWeek6()
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
			element.SetAttribute("required", "required");
			element.Value = "2000-W99";
			Assert.AreEqual("week", element.Type);
			Assert.AreEqual(true, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputWeek7()
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
			element.SetAttribute("required", "required");
			element.Value = "2000-W00";
			Assert.AreEqual("week", element.Type);
			Assert.AreEqual(true, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputWeek8()
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
			element.SetAttribute("required", "required");
			element.Value = "2000-w01";
			Assert.AreEqual("week", element.Type);
			Assert.AreEqual(true, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputWeek9()
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
			element.SetAttribute("required", "required");
			element.Value = "";
			Assert.AreEqual("week", element.Type);
			Assert.AreEqual(true, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputTime1()
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
			element.SetAttribute("required", null);
			element.Value = "";
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(false, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputTime2()
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
			element.SetAttribute("required", "required");
			element.Value = "12:00:00";
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(false, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputTime3()
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
			element.SetAttribute("required", "required");
			element.Value = "12:00";
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(false, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputTime4()
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
			element.SetAttribute("required", "required");
			element.Value = "12:00:00.001";
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(false, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputTime5()
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
			element.SetAttribute("required", "required");
			element.Value = "12:00:00.01";
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(false, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputTime6()
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
			element.SetAttribute("required", "required");
			element.Value = "12:00:00.1";
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(false, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputTime7()
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
			element.SetAttribute("required", "required");
			element.Value = "1234567";
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(true, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputTime8()
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
			element.SetAttribute("required", "required");
			element.Value = "2015-01-01T14:11:21.695Z";
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(true, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputTime9()
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
			element.SetAttribute("required", "required");
			element.Value = "25:00:00";
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(true, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputTime10()
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
			element.SetAttribute("required", "required");
			element.Value = "12:60:00";
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(true, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputTime11()
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
			element.SetAttribute("required", "required");
			element.Value = "12:00:60";
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(true, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputTime12()
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
			element.SetAttribute("required", "required");
			element.Value = "12:00:00:001";
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(true, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputTime13()
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
			element.SetAttribute("required", "required");
			element.Value = "";
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(true, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputNumber1()
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
			element.SetAttribute("required", null);
			element.Value = "";
			Assert.AreEqual("number", element.Type);
			Assert.AreEqual(false, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputNumber2()
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
			element.SetAttribute("required", "required");
			element.Value = "123";
			Assert.AreEqual("number", element.Type);
			Assert.AreEqual(false, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputNumber3()
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
			element.SetAttribute("required", "required");
			element.Value = "-123.45";
			Assert.AreEqual("number", element.Type);
			Assert.AreEqual(false, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputNumber4()
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
			element.SetAttribute("required", "required");
			element.Value = "123.01e-10";
			Assert.AreEqual("number", element.Type);
			Assert.AreEqual(false, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputNumber5()
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
			element.SetAttribute("required", "required");
			element.Value = "123.01E+10";
			Assert.AreEqual("number", element.Type);
			Assert.AreEqual(false, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputNumber6()
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
			element.SetAttribute("required", "required");
			element.Value = "-0";
			Assert.AreEqual("number", element.Type);
			Assert.AreEqual(false, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputNumber7()
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
			element.SetAttribute("required", "required");
			element.Value = " 123 ";
			Assert.AreEqual("number", element.Type);
			Assert.AreEqual(true, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputNumber8()
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
			element.SetAttribute("required", "required");
			element.Value = "null";
			Assert.AreEqual("number", element.Type);
			Assert.AreEqual(true, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputNumber9()
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
			element.SetAttribute("required", "required");
			element.Value = "null";
			Assert.AreEqual("number", element.Type);
			Assert.AreEqual(true, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputNumber10()
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
			element.SetAttribute("required", "required");
			element.Value = "abc";
			Assert.AreEqual("number", element.Type);
			Assert.AreEqual(true, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputNumber11()
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
			element.SetAttribute("required", "required");
			element.Value = "";
			Assert.AreEqual("number", element.Type);
			Assert.AreEqual(true, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputCheckbox1()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "checkbox";
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
			element.SetAttribute("required", null);
			element.SetAttribute("checked", null);
			element.SetAttribute("name", "test1");
			Assert.AreEqual("checkbox", element.Type);
			Assert.AreEqual(false, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputCheckbox2()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "checkbox";
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
			element.SetAttribute("required", "required");
			element.SetAttribute("checked", "checked");
			element.SetAttribute("name", "test1");
			Assert.AreEqual("checkbox", element.Type);
			Assert.AreEqual(false, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputCheckbox3()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "checkbox";
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
			element.SetAttribute("required", "required");
			element.SetAttribute("checked", null);
			element.SetAttribute("name", "test1");
			Assert.AreEqual("checkbox", element.Type);
			Assert.AreEqual(true, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputRadio1()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "radio";
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
			element.SetAttribute("required", null);
			element.SetAttribute("checked", null);
			element.SetAttribute("name", "test1");
			Assert.AreEqual("radio", element.Type);
			Assert.AreEqual(false, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputRadio2()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "radio";
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
			element.SetAttribute("required", "required");
			element.SetAttribute("checked", "checked");
			element.SetAttribute("name", "test1");
			Assert.AreEqual("radio", element.Type);
			Assert.AreEqual(false, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputRadio3()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "radio";
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
			element.SetAttribute("required", "required");
			element.SetAttribute("checked", null);
			element.SetAttribute("name", "test1");
			Assert.AreEqual("radio", element.Type);
			Assert.AreEqual(true, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputFile1()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "file";
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
			element.SetAttribute("required", null);
			element.SetAttribute("files", "null");
			Assert.AreEqual("file", element.Type);
			Assert.AreEqual(false, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingInputFile2()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "file";
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
			element.SetAttribute("required", "required");
			element.SetAttribute("files", "null");
			Assert.AreEqual("file", element.Type);
			Assert.AreEqual(true, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingSelect1()
		{
			var document = Html("");
			var element = document.CreateElement("select") as HtmlSelectElement;
			Assert.IsNotNull(element);
			var option1 = document.CreateElement<IHtmlOptionElement>();
			option1.Text = "test1";
			option1.Value = "";
			var option2 = document.CreateElement<IHtmlOptionElement>();
			option2.Text = "test1";
			option2.Value = "1";
			element.AddOption(option1);
			element.AddOption(option2);
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
			element.SetAttribute("required", null);
			element.Value = "";
			Assert.AreEqual(false, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingSelect2()
		{
			var document = Html("");
			var element = document.CreateElement("select") as HtmlSelectElement;
			Assert.IsNotNull(element);
			var option1 = document.CreateElement<IHtmlOptionElement>();
			option1.Text = "test1";
			option1.Value = "";
			var option2 = document.CreateElement<IHtmlOptionElement>();
			option2.Text = "test1";
			option2.Value = "1";
			element.AddOption(option1);
			element.AddOption(option2);
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
			element.SetAttribute("required", "required");
			element.Value = "1";
			Assert.AreEqual(false, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingSelect3()
		{
			var document = Html("");
			var element = document.CreateElement("select") as HtmlSelectElement;
			Assert.IsNotNull(element);
			var option1 = document.CreateElement<IHtmlOptionElement>();
			option1.Text = "test1";
			option1.Value = "";
			var option2 = document.CreateElement<IHtmlOptionElement>();
			option2.Text = "test1";
			option2.Value = "1";
			element.AddOption(option1);
			element.AddOption(option2);
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
			element.SetAttribute("required", "required");
			element.Value = "";
			Assert.AreEqual(true, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingTextarea1()
		{
			var document = Html("");
			var element = document.CreateElement("textarea") as HtmlTextAreaElement;
			Assert.IsNotNull(element);
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
			element.SetAttribute("required", null);
            element.Value = "";
			Assert.AreEqual(false, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingTextarea2()
		{
			var document = Html("");
			var element = document.CreateElement("textarea") as HtmlTextAreaElement;
			Assert.IsNotNull(element);
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
			element.SetAttribute("required", "required");
			element.Value = "abc";
			Assert.AreEqual(false, element.Validity.IsValueMissing);
		}
		
		[Test]
		public void TestValuemissingTextarea3()
		{
			var document = Html("");
			var element = document.CreateElement("textarea") as HtmlTextAreaElement;
			Assert.IsNotNull(element);
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
			element.SetAttribute("required", "required");
			element.Value = "";
			Assert.AreEqual(true, element.Validity.IsValueMissing);
		}
		
	}
}