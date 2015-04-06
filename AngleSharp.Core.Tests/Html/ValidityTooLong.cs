using System;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using NUnit.Framework;

namespace AngleSharp.Core.Tests.Html
{
    /// <summary>
    /// Tests generated according to the W3C-Test.org page:
    /// http://www.w3c-test.org/html/semantics/forms/constraints/form-validation-validity-tooLong.html
    /// </summary>
    [TestFixture]
	public class ValidityTooLongTests
    {
        static IDocument Html(String code)
        {
            return code.ToHtmlDocument();
        }

        [Test]
		public void TestToolongInputText1()
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
			element.SetAttribute("maxLength", "");
			element.Value = "abc";
			Assert.AreEqual("text", element.Type);
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputText2()
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
			element.Value = "";
			Assert.AreEqual("text", element.Type);
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputText3()
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
			element.Value = "abc";
			Assert.AreEqual("text", element.Type);
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputText4()
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
			element.Value = "abcd";
			Assert.AreEqual("text", element.Type);
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputText5()
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
			element.Value = "abcde";
			Assert.AreEqual("text", element.Type);
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputText6()
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
			element.Value = "abc";
			element.IsDirty = true;
			Assert.AreEqual("text", element.Type);
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputText7()
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
			element.Value = "AAA";
			element.IsDirty = true;
			Assert.AreEqual("text", element.Type);
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputText8()
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
			element.Value = "abcd";
			element.IsDirty = true;
			Assert.AreEqual("text", element.Type);
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputText9()
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
			element.Value = "abcde";
			element.IsDirty = true;
			Assert.AreEqual("text", element.Type);
			Assert.AreEqual(true, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputSearch1()
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
			element.SetAttribute("maxLength", "");
			element.Value = "abc";
			Assert.AreEqual("search", element.Type);
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputSearch2()
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
			element.Value = "";
			Assert.AreEqual("search", element.Type);
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputSearch3()
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
			element.Value = "abc";
			Assert.AreEqual("search", element.Type);
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputSearch4()
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
			element.Value = "abcd";
			Assert.AreEqual("search", element.Type);
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputSearch5()
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
			element.Value = "abcde";
			Assert.AreEqual("search", element.Type);
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputSearch6()
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
			element.Value = "abc";
			element.IsDirty = true;
			Assert.AreEqual("search", element.Type);
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputSearch7()
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
			element.Value = "AAA";
			element.IsDirty = true;
			Assert.AreEqual("search", element.Type);
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputSearch8()
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
			element.Value = "abcd";
			element.IsDirty = true;
			Assert.AreEqual("search", element.Type);
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputSearch9()
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
			element.Value = "abcde";
			element.IsDirty = true;
			Assert.AreEqual("search", element.Type);
			Assert.AreEqual(true, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputTel1()
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
			element.SetAttribute("maxLength", "");
			element.Value = "abc";
			Assert.AreEqual("tel", element.Type);
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputTel2()
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
			element.Value = "";
			Assert.AreEqual("tel", element.Type);
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputTel3()
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
			element.Value = "abc";
			Assert.AreEqual("tel", element.Type);
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputTel4()
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
			element.Value = "abcd";
			Assert.AreEqual("tel", element.Type);
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputTel5()
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
			element.Value = "abcde";
			Assert.AreEqual("tel", element.Type);
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputTel6()
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
			element.Value = "abc";
			element.IsDirty = true;
			Assert.AreEqual("tel", element.Type);
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputTel7()
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
			element.Value = "AAA";
			element.IsDirty = true;
			Assert.AreEqual("tel", element.Type);
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputTel8()
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
			element.Value = "abcd";
			element.IsDirty = true;
			Assert.AreEqual("tel", element.Type);
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputTel9()
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
			element.Value = "abcde";
			element.IsDirty = true;
			Assert.AreEqual("tel", element.Type);
			Assert.AreEqual(true, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputUrl1()
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
			element.SetAttribute("maxLength", "");
			element.Value = "abc";
			Assert.AreEqual("url", element.Type);
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputUrl2()
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
			element.SetAttribute("maxLength", "4");
			element.Value = "";
			Assert.AreEqual("url", element.Type);
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputUrl3()
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
			element.SetAttribute("maxLength", "4");
			element.Value = "abc";
			Assert.AreEqual("url", element.Type);
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputUrl4()
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
			element.SetAttribute("maxLength", "4");
			element.Value = "abcd";
			Assert.AreEqual("url", element.Type);
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputUrl5()
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
			element.SetAttribute("maxLength", "4");
			element.Value = "abcde";
			Assert.AreEqual("url", element.Type);
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputUrl6()
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
			element.SetAttribute("maxLength", "4");
			element.Value = "abc";
			element.IsDirty = true;
			Assert.AreEqual("url", element.Type);
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputUrl7()
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
			element.SetAttribute("maxLength", "4");
			element.Value = "AAA";
			element.IsDirty = true;
			Assert.AreEqual("url", element.Type);
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputUrl8()
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
			element.SetAttribute("maxLength", "4");
			element.Value = "abcd";
			element.IsDirty = true;
			Assert.AreEqual("url", element.Type);
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputUrl9()
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
			element.SetAttribute("maxLength", "4");
			element.Value = "abcde";
			element.IsDirty = true;
			Assert.AreEqual("url", element.Type);
			Assert.AreEqual(true, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputEmail1()
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
			element.SetAttribute("maxLength", "");
			element.Value = "abc";
			Assert.AreEqual("email", element.Type);
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputEmail2()
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
			element.SetAttribute("maxLength", "4");
			element.Value = "";
			Assert.AreEqual("email", element.Type);
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputEmail3()
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
			element.SetAttribute("maxLength", "4");
			element.Value = "abc";
			Assert.AreEqual("email", element.Type);
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputEmail4()
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
			element.SetAttribute("maxLength", "4");
			element.Value = "abcd";
			Assert.AreEqual("email", element.Type);
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputEmail5()
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
			element.SetAttribute("maxLength", "4");
			element.Value = "abcde";
			Assert.AreEqual("email", element.Type);
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputEmail6()
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
			element.SetAttribute("maxLength", "4");
			element.Value = "abc";
			element.IsDirty = true;
			Assert.AreEqual("email", element.Type);
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputEmail7()
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
			element.SetAttribute("maxLength", "4");
			element.Value = "AAA";
			element.IsDirty = true;
			Assert.AreEqual("email", element.Type);
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputEmail8()
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
			element.SetAttribute("maxLength", "4");
			element.Value = "abcd";
			element.IsDirty = true;
			Assert.AreEqual("email", element.Type);
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputEmail9()
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
			element.SetAttribute("maxLength", "4");
			element.Value = "abcde";
			element.IsDirty = true;
			Assert.AreEqual("email", element.Type);
			Assert.AreEqual(true, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputPassword1()
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
			element.SetAttribute("maxLength", "");
			element.Value = "abc";
			Assert.AreEqual("password", element.Type);
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputPassword2()
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
			element.Value = "";
			Assert.AreEqual("password", element.Type);
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputPassword3()
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
			element.Value = "abc";
			Assert.AreEqual("password", element.Type);
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputPassword4()
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
			element.Value = "abcd";
			Assert.AreEqual("password", element.Type);
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputPassword5()
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
			element.Value = "abcde";
			Assert.AreEqual("password", element.Type);
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputPassword6()
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
			element.Value = "abc";
			element.IsDirty = true;
			Assert.AreEqual("password", element.Type);
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputPassword7()
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
			element.Value = "AAA";
			element.IsDirty = true;
			Assert.AreEqual("password", element.Type);
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputPassword8()
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
			element.Value = "abcd";
			element.IsDirty = true;
			Assert.AreEqual("password", element.Type);
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongInputPassword9()
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
			element.Value = "abcde";
			element.IsDirty = true;
			Assert.AreEqual("password", element.Type);
			Assert.AreEqual(true, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongTextarea1()
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
			element.SetAttribute("maxLength", "");
			element.Value = "abc";
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongTextarea2()
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
			element.SetAttribute("maxLength", "4");
			element.Value = "";
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongTextarea3()
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
			element.SetAttribute("maxLength", "4");
			element.Value = "abc";
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongTextarea4()
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
			element.SetAttribute("maxLength", "4");
			element.Value = "abcd";
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongTextarea5()
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
			element.SetAttribute("maxLength", "4");
			element.Value = "abcde";
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongTextarea6()
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
			element.SetAttribute("maxLength", "4");
			element.Value = "abc";
			element.IsDirty = true;
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongTextarea7()
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
			element.SetAttribute("maxLength", "4");
			element.Value = "\r\n";
			element.IsDirty = true;
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongTextarea8()
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
			element.SetAttribute("maxLength", "4");
			element.Value = "abcd";
			element.IsDirty = true;
			Assert.AreEqual(false, element.Validity.IsTooLong);
		}
		
		[Test]
		public void TestToolongTextarea9()
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
			element.SetAttribute("maxLength", "4");
			element.Value = "abcde";
			element.IsDirty = true;
			Assert.AreEqual(true, element.Validity.IsTooLong);
		}
		
	}
}