using System;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using NUnit.Framework;

namespace AngleSharp.Core.Tests.Html
{
    /// <summary>
    /// Tests generated according to the W3C-Test.org page:
    /// http://www.w3c-test.org/html/semantics/forms/constraints/form-validation-checkValidity.html
    /// </summary>
    [TestFixture]
	public class ValidityCheckTests
    {
        static IDocument Html(String code)
        {
            return code.ToHtmlDocument();
        }

        [Test]
		public void TestCheckvalidityInputText1()
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
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("text", element.Type);
			Assert.AreEqual(true, element.CheckValidity());
			Assert.AreEqual(true, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputText2()
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
			element.SetAttribute("maxLength", "4");
			element.Value = "abcdef";
			element.IsDirty = true;
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true) as HtmlInputElement;
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			element2.IsDirty = true;
			Assert.AreEqual("text", element.Type);
			Assert.AreEqual(false, element.CheckValidity());
			Assert.AreEqual(false, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputText3()
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
			element.SetAttribute("pattern", "[A-Z]");
			element.Value = "abc";
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("text", element.Type);
			Assert.AreEqual(false, element.CheckValidity());
			Assert.AreEqual(false, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputText4()
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
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("text", element.Type);
			Assert.AreEqual(false, element.CheckValidity());
			Assert.AreEqual(false, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputSearch1()
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
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("search", element.Type);
			Assert.AreEqual(true, element.CheckValidity());
			Assert.AreEqual(true, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputSearch2()
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
			element.SetAttribute("maxLength", "4");
			element.Value = "abcdef";
			element.IsDirty = true;
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true) as HtmlInputElement;
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			element2.IsDirty = true;
			Assert.AreEqual("search", element.Type);
			Assert.AreEqual(false, element.CheckValidity());
			Assert.AreEqual(false, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputSearch3()
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
			element.SetAttribute("pattern", "[A-Z]");
			element.Value = "abc";
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("search", element.Type);
			Assert.AreEqual(false, element.CheckValidity());
			Assert.AreEqual(false, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputSearch4()
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
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("search", element.Type);
			Assert.AreEqual(false, element.CheckValidity());
			Assert.AreEqual(false, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputTel1()
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
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("tel", element.Type);
			Assert.AreEqual(true, element.CheckValidity());
			Assert.AreEqual(true, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputTel2()
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
			element.SetAttribute("maxLength", "4");
			element.Value = "abcdef";
			element.IsDirty = true;
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true) as HtmlInputElement;
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			element2.IsDirty = true;
			Assert.AreEqual("tel", element.Type);
			Assert.AreEqual(false, element.CheckValidity());
			Assert.AreEqual(false, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputTel3()
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
			element.SetAttribute("pattern", "[A-Z]");
			element.Value = "abc";
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("tel", element.Type);
			Assert.AreEqual(false, element.CheckValidity());
			Assert.AreEqual(false, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputTel4()
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
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("tel", element.Type);
			Assert.AreEqual(false, element.CheckValidity());
			Assert.AreEqual(false, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputPassword1()
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
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("password", element.Type);
			Assert.AreEqual(true, element.CheckValidity());
			Assert.AreEqual(true, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputPassword2()
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
			element.SetAttribute("maxLength", "4");
			element.Value = "abcdef";
			element.IsDirty = true;
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true) as HtmlInputElement;
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			element2.IsDirty = true;
			Assert.AreEqual("password", element.Type);
			Assert.AreEqual(false, element.CheckValidity());
			Assert.AreEqual(false, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputPassword3()
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
			element.SetAttribute("pattern", "[A-Z]");
			element.Value = "abc";
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("password", element.Type);
			Assert.AreEqual(false, element.CheckValidity());
			Assert.AreEqual(false, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputPassword4()
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
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("password", element.Type);
			Assert.AreEqual(false, element.CheckValidity());
			Assert.AreEqual(false, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputUrl1()
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
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("url", element.Type);
			Assert.AreEqual(true, element.CheckValidity());
			Assert.AreEqual(true, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputUrl2()
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
			element.SetAttribute("maxLength", "20");
			element.Value = "http://www.example.com";
			element.IsDirty = true;
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true) as HtmlInputElement;
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			element2.IsDirty = true;
			Assert.AreEqual("url", element.Type);
			Assert.AreEqual(false, element.CheckValidity());
			Assert.AreEqual(false, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputUrl3()
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
			element.SetAttribute("pattern", "http://www.example.com");
			element.Value = "http://www.example.net";
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("url", element.Type);
			Assert.AreEqual(false, element.CheckValidity());
			Assert.AreEqual(false, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputUrl4()
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
			element.Value = "abc";
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("url", element.Type);
			Assert.AreEqual(false, element.CheckValidity());
			Assert.AreEqual(false, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputUrl5()
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
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("url", element.Type);
			Assert.AreEqual(false, element.CheckValidity());
			Assert.AreEqual(false, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputEmail1()
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
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("email", element.Type);
			Assert.AreEqual(true, element.CheckValidity());
			Assert.AreEqual(true, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputEmail2()
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
			element.SetAttribute("maxLength", "10");
			element.Value = "test@example.com";
			element.IsDirty = true;
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true) as HtmlInputElement;
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			element2.IsDirty = true;
			Assert.AreEqual("email", element.Type);
			Assert.AreEqual(false, element.CheckValidity());
			Assert.AreEqual(false, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputEmail3()
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
			element.SetAttribute("pattern", "test@example.com");
			element.Value = "test@example.net";
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("email", element.Type);
			Assert.AreEqual(false, element.CheckValidity());
			Assert.AreEqual(false, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputEmail4()
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
			element.Value = "abc";
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("email", element.Type);
			Assert.AreEqual(false, element.CheckValidity());
			Assert.AreEqual(false, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputEmail5()
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
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("email", element.Type);
			Assert.AreEqual(false, element.CheckValidity());
			Assert.AreEqual(false, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputDatetime1()
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
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(true, element.CheckValidity());
			Assert.AreEqual(true, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputDatetime2()
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
			element.SetAttribute("max", "2000-01-01T12:00:00Z");
			element.Value = "2001-01-01T12:00:00Z";
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(false, element.CheckValidity());
			Assert.AreEqual(false, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputDatetime3()
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
			element.SetAttribute("min", "2001-01-01T12:00:00Z");
			element.Value = "2000-01-01T12:00:00Z";
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(false, element.CheckValidity());
			Assert.AreEqual(false, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputDatetime4()
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
			element.SetAttribute("step", "120000");
			element.Value = "2001-01-01T12:03:00Z";
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(false, element.CheckValidity());
			Assert.AreEqual(false, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputDatetime5()
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
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("datetime", element.Type);
			Assert.AreEqual(false, element.CheckValidity());
			Assert.AreEqual(false, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputDate1()
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
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("date", element.Type);
			Assert.AreEqual(true, element.CheckValidity());
			Assert.AreEqual(true, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputDate2()
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
			element.SetAttribute("max", "2000-01-01");
			element.Value = "2001-01-01";
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("date", element.Type);
			Assert.AreEqual(false, element.CheckValidity());
			Assert.AreEqual(false, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputDate3()
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
			element.SetAttribute("min", "2001-01-01");
			element.Value = "2000-01-01";
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("date", element.Type);
			Assert.AreEqual(false, element.CheckValidity());
			Assert.AreEqual(false, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputDate4()
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
			element.SetAttribute("step", "172800000");
			element.Value = "2001-01-03";
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("date", element.Type);
			Assert.AreEqual(false, element.CheckValidity());
			Assert.AreEqual(false, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputDate5()
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
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("date", element.Type);
			Assert.AreEqual(false, element.CheckValidity());
			Assert.AreEqual(false, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputMonth1()
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
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("month", element.Type);
			Assert.AreEqual(true, element.CheckValidity());
			Assert.AreEqual(true, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputMonth2()
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
			element.SetAttribute("max", "2000-01");
			element.Value = "2001-01";
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("month", element.Type);
			Assert.AreEqual(false, element.CheckValidity());
			Assert.AreEqual(false, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputMonth3()
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
			element.SetAttribute("min", "2001-01");
			element.Value = "2000-01";
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("month", element.Type);
			Assert.AreEqual(false, element.CheckValidity());
			Assert.AreEqual(false, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputMonth4()
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
			element.SetAttribute("step", "3");
			element.Value = "2001-03";
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("month", element.Type);
			Assert.AreEqual(false, element.CheckValidity());
			Assert.AreEqual(false, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputMonth5()
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
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("month", element.Type);
			Assert.AreEqual(false, element.CheckValidity());
			Assert.AreEqual(false, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputWeek1()
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
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("week", element.Type);
			Assert.AreEqual(true, element.CheckValidity());
			Assert.AreEqual(true, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputWeek2()
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
			element.SetAttribute("max", "2000-W01");
			element.Value = "2001-W01";
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("week", element.Type);
			Assert.AreEqual(false, element.CheckValidity());
			Assert.AreEqual(false, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputWeek3()
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
			element.SetAttribute("min", "2001-W01");
			element.Value = "2000-W01";
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("week", element.Type);
			Assert.AreEqual(false, element.CheckValidity());
			Assert.AreEqual(false, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputWeek4()
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
			element.SetAttribute("step", "1209600000");
			element.Value = "2001-W03";
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("week", element.Type);
			Assert.AreEqual(false, element.CheckValidity());
			Assert.AreEqual(false, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputWeek5()
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
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("week", element.Type);
			Assert.AreEqual(false, element.CheckValidity());
			Assert.AreEqual(false, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputTime1()
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
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(true, element.CheckValidity());
			Assert.AreEqual(true, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputTime2()
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
			element.SetAttribute("max", "12:00:00");
			element.Value = "13:00:00";
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(false, element.CheckValidity());
			Assert.AreEqual(false, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputTime3()
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
			element.SetAttribute("min", "12:00:00");
			element.Value = "11:00:00";
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(false, element.CheckValidity());
			Assert.AreEqual(false, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputTime4()
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
			element.SetAttribute("step", "120000");
			element.Value = "12:03:00";
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(false, element.CheckValidity());
			Assert.AreEqual(false, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputTime5()
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
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("time", element.Type);
			Assert.AreEqual(false, element.CheckValidity());
			Assert.AreEqual(false, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputNumber1()
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
			element.SetAttribute("max", "5");
			element.Value = "6";
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("number", element.Type);
			Assert.AreEqual(false, element.CheckValidity());
			Assert.AreEqual(false, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputNumber2()
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
			element.SetAttribute("min", "5");
			element.Value = "4";
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("number", element.Type);
			Assert.AreEqual(false, element.CheckValidity());
			Assert.AreEqual(false, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputNumber3()
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
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("number", element.Type);
			Assert.AreEqual(false, element.CheckValidity());
			Assert.AreEqual(false, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputNumber4()
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
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("number", element.Type);
			Assert.AreEqual(false, element.CheckValidity());
			Assert.AreEqual(false, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputCheckbox1()
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
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("checkbox", element.Type);
			Assert.AreEqual(true, element.CheckValidity());
			Assert.AreEqual(true, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputCheckbox2()
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
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("checkbox", element.Type);
			Assert.AreEqual(false, element.CheckValidity());
			Assert.AreEqual(false, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputRadio1()
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
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("radio", element.Type);
			Assert.AreEqual(true, element.CheckValidity());
			Assert.AreEqual(true, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputRadio2()
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
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("radio", element.Type);
			Assert.AreEqual(false, element.CheckValidity());
			Assert.AreEqual(false, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputFile1()
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
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("file", element.Type);
			Assert.AreEqual(true, element.CheckValidity());
			Assert.AreEqual(true, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityInputFile2()
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
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual("file", element.Type);
			Assert.AreEqual(false, element.CheckValidity());
			Assert.AreEqual(false, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvaliditySelect1()
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
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual(true, element.CheckValidity());
			Assert.AreEqual(true, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvaliditySelect2()
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
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual(false, element.CheckValidity());
			Assert.AreEqual(false, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityTextarea1()
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
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual(true, element.CheckValidity());
			Assert.AreEqual(true, fm.CheckValidity());
		}
		
		[Test]
		public void TestCheckvalidityTextarea2()
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
			var fm = document.CreateElement("form") as IHtmlFormElement;
			var element2 = element.Clone(true);
			fm.AppendChild(element2);
			document.Body.AppendChild(fm);
			Assert.AreEqual(false, element.CheckValidity());
			Assert.AreEqual(false, fm.CheckValidity());
		}
		
	}
}