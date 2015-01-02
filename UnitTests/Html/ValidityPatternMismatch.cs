using AngleSharp;
using AngleSharp.DOM.Html;
using NUnit.Framework;

namespace UnitTests.Html
{
    /// <summary>
    /// Tests generated according to the W3C-Test.org page:
    /// http://www.w3c-test.org/html/semantics/forms/constraints/form-validation-validity-patternMismatch.html
    /// </summary>
	[TestFixture]
	public class ValidityPatternMismatchTests
	{
		[Test]
		public void TestPatternmismatchInputText1()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("value", "abc");
			Assert.AreEqual("text", element.Type);
			Assert.AreEqual(false, element.Validity.IsPatternMismatch);
		}
		
		[Test]
		public void TestPatternmismatchInputText2()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("pattern", "[A-Z]+");
			element.SetAttribute("value", "");
			Assert.AreEqual("text", element.Type);
			Assert.AreEqual(false, element.Validity.IsPatternMismatch);
		}
		
		[Test]
		public void TestPatternmismatchInputText3()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("pattern", "[A-Z]{1}");
			element.SetAttribute("value", "A");
			Assert.AreEqual("text", element.Type);
			Assert.AreEqual(false, element.Validity.IsPatternMismatch);
		}
		
		[Test]
		public void TestPatternmismatchInputText4()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("pattern", "[A-Z]+");
			element.SetAttribute("value", "ABC");
			Assert.AreEqual("text", element.Type);
			Assert.AreEqual(false, element.Validity.IsPatternMismatch);
		}
		
		[Test]
		public void TestPatternmismatchInputText5()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("pattern", "[a-z]{3,}");
			element.SetAttribute("value", "ABCD");
			Assert.AreEqual("text", element.Type);
			Assert.AreEqual(true, element.Validity.IsPatternMismatch);
		}
		
		[Test]
		public void TestPatternmismatchInputSearch1()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("value", "abc");
			Assert.AreEqual("search", element.Type);
			Assert.AreEqual(false, element.Validity.IsPatternMismatch);
		}
		
		[Test]
		public void TestPatternmismatchInputSearch2()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("pattern", "[A-Z]+");
			element.SetAttribute("value", "");
			Assert.AreEqual("search", element.Type);
			Assert.AreEqual(false, element.Validity.IsPatternMismatch);
		}
		
		[Test]
		public void TestPatternmismatchInputSearch3()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("pattern", "[A-Z]{1}");
			element.SetAttribute("value", "A");
			Assert.AreEqual("search", element.Type);
			Assert.AreEqual(false, element.Validity.IsPatternMismatch);
		}
		
		[Test]
		public void TestPatternmismatchInputSearch4()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("pattern", "[A-Z]+");
			element.SetAttribute("value", "ABC");
			Assert.AreEqual("search", element.Type);
			Assert.AreEqual(false, element.Validity.IsPatternMismatch);
		}
		
		[Test]
		public void TestPatternmismatchInputSearch5()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("pattern", "[a-z]{3,}");
			element.SetAttribute("value", "ABCD");
			Assert.AreEqual("search", element.Type);
			Assert.AreEqual(true, element.Validity.IsPatternMismatch);
		}
		
		[Test]
		public void TestPatternmismatchInputTel1()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("value", "abc");
			Assert.AreEqual("tel", element.Type);
			Assert.AreEqual(false, element.Validity.IsPatternMismatch);
		}
		
		[Test]
		public void TestPatternmismatchInputTel2()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("pattern", "[A-Z]+");
			element.SetAttribute("value", "");
			Assert.AreEqual("tel", element.Type);
			Assert.AreEqual(false, element.Validity.IsPatternMismatch);
		}
		
		[Test]
		public void TestPatternmismatchInputTel3()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("pattern", "[A-Z]{1}");
			element.SetAttribute("value", "A");
			Assert.AreEqual("tel", element.Type);
			Assert.AreEqual(false, element.Validity.IsPatternMismatch);
		}
		
		[Test]
		public void TestPatternmismatchInputTel4()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("pattern", "[A-Z]+");
			element.SetAttribute("value", "ABC");
			Assert.AreEqual("tel", element.Type);
			Assert.AreEqual(false, element.Validity.IsPatternMismatch);
		}
		
		[Test]
		public void TestPatternmismatchInputTel5()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("pattern", "[a-z]{3,}");
			element.SetAttribute("value", "ABCD");
			Assert.AreEqual("tel", element.Type);
			Assert.AreEqual(true, element.Validity.IsPatternMismatch);
		}
		
		[Test]
		public void TestPatternmismatchInputUrl1()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("value", "abc");
			Assert.AreEqual("url", element.Type);
			Assert.AreEqual(false, element.Validity.IsPatternMismatch);
		}
		
		[Test]
		public void TestPatternmismatchInputUrl2()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("pattern", "[A-Z]+");
			element.SetAttribute("value", "");
			Assert.AreEqual("url", element.Type);
			Assert.AreEqual(false, element.Validity.IsPatternMismatch);
		}
		
		[Test]
		public void TestPatternmismatchInputUrl3()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("pattern", "[A-Z]{1}");
			element.SetAttribute("value", "A");
			Assert.AreEqual("url", element.Type);
			Assert.AreEqual(false, element.Validity.IsPatternMismatch);
		}
		
		[Test]
		public void TestPatternmismatchInputUrl4()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("pattern", "[A-Z]+");
			element.SetAttribute("value", "ABC");
			Assert.AreEqual("url", element.Type);
			Assert.AreEqual(false, element.Validity.IsPatternMismatch);
		}
		
		[Test]
		public void TestPatternmismatchInputUrl5()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("pattern", "[a-z]{3,}");
			element.SetAttribute("value", "ABCD");
			Assert.AreEqual("url", element.Type);
			Assert.AreEqual(true, element.Validity.IsPatternMismatch);
		}
		
		[Test]
		public void TestPatternmismatchInputEmail1()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("value", "abc");
			Assert.AreEqual("email", element.Type);
			Assert.AreEqual(false, element.Validity.IsPatternMismatch);
		}
		
		[Test]
		public void TestPatternmismatchInputEmail2()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("pattern", "[A-Z]+");
			element.SetAttribute("value", "");
			Assert.AreEqual("email", element.Type);
			Assert.AreEqual(false, element.Validity.IsPatternMismatch);
		}
		
		[Test]
		public void TestPatternmismatchInputEmail3()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("pattern", "[A-Z]{1}");
			element.SetAttribute("value", "A");
			Assert.AreEqual("email", element.Type);
			Assert.AreEqual(false, element.Validity.IsPatternMismatch);
		}
		
		[Test]
		public void TestPatternmismatchInputEmail4()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("pattern", "[A-Z]+");
			element.SetAttribute("value", "ABC");
			Assert.AreEqual("email", element.Type);
			Assert.AreEqual(false, element.Validity.IsPatternMismatch);
		}
		
		[Test]
		public void TestPatternmismatchInputEmail5()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("pattern", "[a-z]{3,}");
			element.SetAttribute("value", "ABCD");
			Assert.AreEqual("email", element.Type);
			Assert.AreEqual(true, element.Validity.IsPatternMismatch);
		}
		
		[Test]
		public void TestPatternmismatchInputPassword1()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("value", "abc");
			Assert.AreEqual("password", element.Type);
			Assert.AreEqual(false, element.Validity.IsPatternMismatch);
		}
		
		[Test]
		public void TestPatternmismatchInputPassword2()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("pattern", "[A-Z]+");
			element.SetAttribute("value", "");
			Assert.AreEqual("password", element.Type);
			Assert.AreEqual(false, element.Validity.IsPatternMismatch);
		}
		
		[Test]
		public void TestPatternmismatchInputPassword3()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("pattern", "[A-Z]{1}");
			element.SetAttribute("value", "A");
			Assert.AreEqual("password", element.Type);
			Assert.AreEqual(false, element.Validity.IsPatternMismatch);
		}
		
		[Test]
		public void TestPatternmismatchInputPassword4()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("pattern", "[A-Z]+");
			element.SetAttribute("value", "ABC");
			Assert.AreEqual("password", element.Type);
			Assert.AreEqual(false, element.Validity.IsPatternMismatch);
		}
		
		[Test]
		public void TestPatternmismatchInputPassword5()
		{
			var document = DocumentBuilder.Html("");
			var element = document.CreateElement("input") as HTMLInputElement;
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
			element.SetAttribute("pattern", "[a-z]{3,}");
			element.SetAttribute("value", "ABCD");
			Assert.AreEqual("password", element.Type);
			Assert.AreEqual(true, element.Validity.IsPatternMismatch);
		}
		
	}
}