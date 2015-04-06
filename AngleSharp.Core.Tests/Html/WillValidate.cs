using System;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using NUnit.Framework;

namespace AngleSharp.Core.Tests.Html
{
    /// <summary>
    /// Tests generated according to the W3C-Test.org page:
    /// http://www.w3c-test.org/html/semantics/forms/constraints/form-validation-willValidate.html
    /// </summary>
	[TestFixture]
	public class ValidityTests
    {
        static IDocument Html(String code)
        {
            return code.ToHtmlDocument();
        }

        [Test]
		public void TestWillvalidateInputHidden1()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "hidden";
			element.RemoveAttribute("required");
			element.RemoveAttribute("pattern");
			element.RemoveAttribute("step");
			element.RemoveAttribute("max");
			element.RemoveAttribute("min");
			element.RemoveAttribute("maxlength");
			element.RemoveAttribute("value");
			element.RemoveAttribute("multiple");
			element.RemoveAttribute("checked");
			element.RemoveAttribute("selected");
			Assert.AreEqual("hidden", element.Type);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputButton1()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "button";
			element.RemoveAttribute("required");
			element.RemoveAttribute("pattern");
			element.RemoveAttribute("step");
			element.RemoveAttribute("max");
			element.RemoveAttribute("min");
			element.RemoveAttribute("maxlength");
			element.RemoveAttribute("value");
			element.RemoveAttribute("multiple");
			element.RemoveAttribute("checked");
			element.RemoveAttribute("selected");
			Assert.AreEqual("button", element.Type);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputReset1()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "reset";
			element.RemoveAttribute("required");
			element.RemoveAttribute("pattern");
			element.RemoveAttribute("step");
			element.RemoveAttribute("max");
			element.RemoveAttribute("min");
			element.RemoveAttribute("maxlength");
			element.RemoveAttribute("value");
			element.RemoveAttribute("multiple");
			element.RemoveAttribute("checked");
			element.RemoveAttribute("selected");
			Assert.AreEqual("reset", element.Type);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateButtonButton1()
		{
			var document = Html("");
			var element = document.CreateElement("button") as HtmlButtonElement;
			Assert.IsNotNull(element);
			element.Type = "button";
			element.RemoveAttribute("required");
			element.RemoveAttribute("pattern");
			element.RemoveAttribute("step");
			element.RemoveAttribute("max");
			element.RemoveAttribute("min");
			element.RemoveAttribute("maxlength");
			element.RemoveAttribute("value");
			element.RemoveAttribute("multiple");
			element.RemoveAttribute("checked");
			element.RemoveAttribute("selected");
			Assert.AreEqual("button", element.Type);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateButtonReset1()
		{
			var document = Html("");
			var element = document.CreateElement("button") as HtmlButtonElement;
			Assert.IsNotNull(element);
			element.Type = "reset";
			element.RemoveAttribute("required");
			element.RemoveAttribute("pattern");
			element.RemoveAttribute("step");
			element.RemoveAttribute("max");
			element.RemoveAttribute("min");
			element.RemoveAttribute("maxlength");
			element.RemoveAttribute("value");
			element.RemoveAttribute("multiple");
			element.RemoveAttribute("checked");
			element.RemoveAttribute("selected");
			Assert.AreEqual("reset", element.Type);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateFieldset1()
		{
			var document = Html("");
			var element = document.CreateElement("fieldset") as HtmlFieldSetElement;
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
			Assert.AreEqual(true, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateOutput1()
		{
			var document = Html("");
			var element = document.CreateElement("output") as HtmlOutputElement;
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
			Assert.AreEqual(true, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateObject1()
		{
			var document = Html("");
			var element = document.CreateElement("object") as HtmlObjectElement;
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
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateKeygen1()
		{
			var document = Html("");
			var element = document.CreateElement("keygen") as HtmlKeygenElement;
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
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputText1()
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
			element.SetAttribute("disabled", "disabled");
			Assert.AreEqual("text", element.Type);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputText2()
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
			element.SetAttribute("disabled", null);
			element.SetAttribute("readOnly", null);
			Assert.AreEqual("text", element.Type);
			Assert.AreEqual(true, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputText3()
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
			element.SetAttribute("readOnly", "readOnly");
			Assert.AreEqual("text", element.Type);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputText4()
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
			element.SetAttribute("disabled", null);
			element.SetAttribute("readOnly", null);
			var dl = document.CreateElement("datalist");
			dl.AppendChild(element);
			Assert.AreEqual("text", element.Type);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputSearch1()
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
			element.SetAttribute("disabled", "disabled");
			Assert.AreEqual("search", element.Type);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputSearch2()
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
			element.SetAttribute("disabled", null);
			element.SetAttribute("readOnly", null);
			Assert.AreEqual("search", element.Type);
			Assert.AreEqual(true, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputSearch3()
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
			element.SetAttribute("readOnly", "readOnly");
			Assert.AreEqual("search", element.Type);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputSearch4()
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
			element.SetAttribute("disabled", null);
			element.SetAttribute("readOnly", null);
			var dl = document.CreateElement("datalist");
			dl.AppendChild(element);
			Assert.AreEqual("search", element.Type);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputTel1()
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
			element.SetAttribute("disabled", "disabled");
			Assert.AreEqual("tel", element.Type);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputTel2()
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
			element.SetAttribute("disabled", null);
			element.SetAttribute("readOnly", null);
			Assert.AreEqual("tel", element.Type);
			Assert.AreEqual(true, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputTel3()
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
			element.SetAttribute("readOnly", "readOnly");
			Assert.AreEqual("tel", element.Type);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputTel4()
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
			element.SetAttribute("disabled", null);
			element.SetAttribute("readOnly", null);
			var dl = document.CreateElement("datalist");
			dl.AppendChild(element);
			Assert.AreEqual("tel", element.Type);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputUrl1()
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
			element.SetAttribute("disabled", "disabled");
			Assert.AreEqual("url", element.Type);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputUrl2()
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
			element.SetAttribute("disabled", null);
			element.SetAttribute("readOnly", null);
			Assert.AreEqual("url", element.Type);
			Assert.AreEqual(true, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputUrl3()
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
			element.SetAttribute("readOnly", "readOnly");
			Assert.AreEqual("url", element.Type);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputUrl4()
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
			element.SetAttribute("disabled", null);
			element.SetAttribute("readOnly", null);
			var dl = document.CreateElement("datalist");
			dl.AppendChild(element);
			Assert.AreEqual("url", element.Type);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputEmail1()
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
			element.SetAttribute("disabled", "disabled");
			Assert.AreEqual("email", element.Type);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputEmail2()
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
			element.SetAttribute("disabled", null);
			element.SetAttribute("readOnly", null);
			Assert.AreEqual("email", element.Type);
			Assert.AreEqual(true, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputEmail3()
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
			element.SetAttribute("readOnly", "readOnly");
			Assert.AreEqual("email", element.Type);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputEmail4()
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
			element.SetAttribute("disabled", null);
			element.SetAttribute("readOnly", null);
			var dl = document.CreateElement("datalist");
			dl.AppendChild(element);
			Assert.AreEqual("email", element.Type);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputPassword1()
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
			element.SetAttribute("disabled", "disabled");
			Assert.AreEqual("password", element.Type);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputPassword2()
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
			element.SetAttribute("disabled", null);
			element.SetAttribute("readOnly", null);
			Assert.AreEqual("password", element.Type);
			Assert.AreEqual(true, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputPassword3()
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
			element.SetAttribute("readOnly", "readOnly");
			Assert.AreEqual("password", element.Type);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputPassword4()
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
			element.SetAttribute("disabled", null);
			element.SetAttribute("readOnly", null);
			var dl = document.CreateElement("datalist");
			dl.AppendChild(element);
			Assert.AreEqual("password", element.Type);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputDatetime1()
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
			element.SetAttribute("disabled", "disabled");
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputDatetime2()
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
			element.SetAttribute("disabled", null);
			element.SetAttribute("readOnly", null);
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(true, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputDatetime3()
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
			element.SetAttribute("readOnly", "readOnly");
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputDatetime4()
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
			element.SetAttribute("disabled", null);
			element.SetAttribute("readOnly", null);
			var dl = document.CreateElement("datalist");
			dl.AppendChild(element);
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputDate1()
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
			element.SetAttribute("disabled", "disabled");
			Assert.AreEqual("date", element.Type);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputDate2()
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
			element.SetAttribute("disabled", null);
			element.SetAttribute("readOnly", null);
			Assert.AreEqual("date", element.Type);
			Assert.AreEqual(true, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputDate3()
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
			element.SetAttribute("readOnly", "readOnly");
			Assert.AreEqual("date", element.Type);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputDate4()
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
			element.SetAttribute("disabled", null);
			element.SetAttribute("readOnly", null);
			var dl = document.CreateElement("datalist");
			dl.AppendChild(element);
			Assert.AreEqual("date", element.Type);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputMonth1()
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
			element.SetAttribute("disabled", "disabled");
			Assert.AreEqual("month", element.Type);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputMonth2()
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
			element.SetAttribute("disabled", null);
			element.SetAttribute("readOnly", null);
			Assert.AreEqual("month", element.Type);
			Assert.AreEqual(true, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputMonth3()
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
			element.SetAttribute("readOnly", "readOnly");
			Assert.AreEqual("month", element.Type);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputMonth4()
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
			element.SetAttribute("disabled", null);
			element.SetAttribute("readOnly", null);
			var dl = document.CreateElement("datalist");
			dl.AppendChild(element);
			Assert.AreEqual("month", element.Type);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputWeek1()
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
			element.SetAttribute("disabled", "disabled");
			Assert.AreEqual("week", element.Type);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputWeek2()
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
			element.SetAttribute("disabled", null);
			element.SetAttribute("readOnly", null);
			Assert.AreEqual("week", element.Type);
			Assert.AreEqual(true, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputWeek3()
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
			element.SetAttribute("readOnly", "readOnly");
			Assert.AreEqual("week", element.Type);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputWeek4()
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
			element.SetAttribute("disabled", null);
			element.SetAttribute("readOnly", null);
			var dl = document.CreateElement("datalist");
			dl.AppendChild(element);
			Assert.AreEqual("week", element.Type);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputTime1()
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
			element.SetAttribute("disabled", "disabled");
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputTime2()
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
			element.SetAttribute("disabled", null);
			element.SetAttribute("readOnly", null);
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(true, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputTime3()
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
			element.SetAttribute("readOnly", "readOnly");
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputTime4()
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
			element.SetAttribute("disabled", null);
			element.SetAttribute("readOnly", null);
			var dl = document.CreateElement("datalist");
			dl.AppendChild(element);
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputColor1()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "color";
			element.RemoveAttribute("required");
			element.RemoveAttribute("pattern");
			element.RemoveAttribute("step");
			element.RemoveAttribute("max");
			element.RemoveAttribute("min");
			element.RemoveAttribute("maxlength");
			element.RemoveAttribute("value");
			element.RemoveAttribute("multiple");
			element.RemoveAttribute("checked");
			element.RemoveAttribute("selected");
			element.SetAttribute("disabled", "disabled");
			Assert.AreEqual("color", element.Type);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputColor2()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "color";
			element.RemoveAttribute("required");
			element.RemoveAttribute("pattern");
			element.RemoveAttribute("step");
			element.RemoveAttribute("max");
			element.RemoveAttribute("min");
			element.RemoveAttribute("maxlength");
			element.RemoveAttribute("value");
			element.RemoveAttribute("multiple");
			element.RemoveAttribute("checked");
			element.RemoveAttribute("selected");
			element.SetAttribute("disabled", null);
			element.SetAttribute("readOnly", null);
			Assert.AreEqual("color", element.Type);
			Assert.AreEqual(true, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputColor3()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "color";
			element.RemoveAttribute("required");
			element.RemoveAttribute("pattern");
			element.RemoveAttribute("step");
			element.RemoveAttribute("max");
			element.RemoveAttribute("min");
			element.RemoveAttribute("maxlength");
			element.RemoveAttribute("value");
			element.RemoveAttribute("multiple");
			element.RemoveAttribute("checked");
			element.RemoveAttribute("selected");
			element.SetAttribute("readOnly", "readOnly");
			Assert.AreEqual("color", element.Type);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputColor4()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "color";
			element.RemoveAttribute("required");
			element.RemoveAttribute("pattern");
			element.RemoveAttribute("step");
			element.RemoveAttribute("max");
			element.RemoveAttribute("min");
			element.RemoveAttribute("maxlength");
			element.RemoveAttribute("value");
			element.RemoveAttribute("multiple");
			element.RemoveAttribute("checked");
			element.RemoveAttribute("selected");
			element.SetAttribute("disabled", null);
			element.SetAttribute("readOnly", null);
			var dl = document.CreateElement("datalist");
			dl.AppendChild(element);
			Assert.AreEqual("color", element.Type);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputFile1()
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
			element.SetAttribute("disabled", "disabled");
			Assert.AreEqual("file", element.Type);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputFile2()
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
			element.SetAttribute("disabled", null);
			element.SetAttribute("readOnly", null);
			Assert.AreEqual("file", element.Type);
			Assert.AreEqual(true, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputFile3()
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
			element.SetAttribute("readOnly", "readOnly");
			Assert.AreEqual("file", element.Type);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputFile4()
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
			element.SetAttribute("disabled", null);
			element.SetAttribute("readOnly", null);
			var dl = document.CreateElement("datalist");
			dl.AppendChild(element);
			Assert.AreEqual("file", element.Type);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputSubmit1()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "submit";
			element.RemoveAttribute("required");
			element.RemoveAttribute("pattern");
			element.RemoveAttribute("step");
			element.RemoveAttribute("max");
			element.RemoveAttribute("min");
			element.RemoveAttribute("maxlength");
			element.RemoveAttribute("value");
			element.RemoveAttribute("multiple");
			element.RemoveAttribute("checked");
			element.RemoveAttribute("selected");
			element.SetAttribute("disabled", "disabled");
			Assert.AreEqual("submit", element.Type);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputSubmit2()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "submit";
			element.RemoveAttribute("required");
			element.RemoveAttribute("pattern");
			element.RemoveAttribute("step");
			element.RemoveAttribute("max");
			element.RemoveAttribute("min");
			element.RemoveAttribute("maxlength");
			element.RemoveAttribute("value");
			element.RemoveAttribute("multiple");
			element.RemoveAttribute("checked");
			element.RemoveAttribute("selected");
			element.SetAttribute("disabled", null);
			element.SetAttribute("readOnly", null);
			Assert.AreEqual("submit", element.Type);
			Assert.AreEqual(true, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputSubmit3()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "submit";
			element.RemoveAttribute("required");
			element.RemoveAttribute("pattern");
			element.RemoveAttribute("step");
			element.RemoveAttribute("max");
			element.RemoveAttribute("min");
			element.RemoveAttribute("maxlength");
			element.RemoveAttribute("value");
			element.RemoveAttribute("multiple");
			element.RemoveAttribute("checked");
			element.RemoveAttribute("selected");
			element.SetAttribute("readOnly", "readOnly");
			Assert.AreEqual("submit", element.Type);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateInputSubmit4()
		{
			var document = Html("");
			var element = document.CreateElement("input") as HtmlInputElement;
			Assert.IsNotNull(element);
			element.Type = "submit";
			element.RemoveAttribute("required");
			element.RemoveAttribute("pattern");
			element.RemoveAttribute("step");
			element.RemoveAttribute("max");
			element.RemoveAttribute("min");
			element.RemoveAttribute("maxlength");
			element.RemoveAttribute("value");
			element.RemoveAttribute("multiple");
			element.RemoveAttribute("checked");
			element.RemoveAttribute("selected");
			element.SetAttribute("disabled", null);
			element.SetAttribute("readOnly", null);
			var dl = document.CreateElement("datalist");
			dl.AppendChild(element);
			Assert.AreEqual("submit", element.Type);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateButtonSubmit1()
		{
			var document = Html("");
			var element = document.CreateElement("button") as HtmlButtonElement;
			Assert.IsNotNull(element);
			element.Type = "submit";
			element.RemoveAttribute("required");
			element.RemoveAttribute("pattern");
			element.RemoveAttribute("step");
			element.RemoveAttribute("max");
			element.RemoveAttribute("min");
			element.RemoveAttribute("maxlength");
			element.RemoveAttribute("value");
			element.RemoveAttribute("multiple");
			element.RemoveAttribute("checked");
			element.RemoveAttribute("selected");
			element.SetAttribute("disabled", "disabled");
			Assert.AreEqual("submit", element.Type);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateButtonSubmit2()
		{
			var document = Html("");
			var element = document.CreateElement("button") as HtmlButtonElement;
			Assert.IsNotNull(element);
			element.Type = "submit";
			element.RemoveAttribute("required");
			element.RemoveAttribute("pattern");
			element.RemoveAttribute("step");
			element.RemoveAttribute("max");
			element.RemoveAttribute("min");
			element.RemoveAttribute("maxlength");
			element.RemoveAttribute("value");
			element.RemoveAttribute("multiple");
			element.RemoveAttribute("checked");
			element.RemoveAttribute("selected");
			element.SetAttribute("disabled", null);
			Assert.AreEqual("submit", element.Type);
			Assert.AreEqual(true, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateButtonSubmit3()
		{
			var document = Html("");
			var element = document.CreateElement("button") as HtmlButtonElement;
			Assert.IsNotNull(element);
			element.Type = "submit";
			element.RemoveAttribute("required");
			element.RemoveAttribute("pattern");
			element.RemoveAttribute("step");
			element.RemoveAttribute("max");
			element.RemoveAttribute("min");
			element.RemoveAttribute("maxlength");
			element.RemoveAttribute("value");
			element.RemoveAttribute("multiple");
			element.RemoveAttribute("checked");
			element.RemoveAttribute("selected");
			element.SetAttribute("disabled", null);
			var dl = document.CreateElement("datalist");
			dl.AppendChild(element);
			Assert.AreEqual("submit", element.Type);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateSelect1()
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
			element.SetAttribute("disabled", "disabled");
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateSelect2()
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
			element.SetAttribute("disabled", null);
			Assert.AreEqual(true, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateSelect3()
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
			element.SetAttribute("disabled", null);
			var dl = document.CreateElement("datalist");
			dl.AppendChild(element);
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateTextarea1()
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
			element.SetAttribute("disabled", "disabled");
			Assert.AreEqual(false, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateTextarea2()
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
			element.SetAttribute("disabled", null);
			Assert.AreEqual(true, element.WillValidate);
		}
		
		[Test]
		public void TestWillvalidateTextarea3()
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
			element.SetAttribute("disabled", null);
			var dl = document.CreateElement("datalist");
			dl.AppendChild(element);
			Assert.AreEqual(false, element.WillValidate);
		}
	}
}