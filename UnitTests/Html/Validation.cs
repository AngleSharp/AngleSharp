using AngleSharp;
using AngleSharp.DOM.Html;
using NUnit.Framework;

namespace UnitTests.Html
{
    [TestFixture]
    public class ValidationTests
    {
        [Test]
        public void BasicInvalidFormValidation()
        {
            var doc = DocumentBuilder.Html(@"<form id=""fm1"">
  <fieldset id=""test0"">
    <input type=""text"" required value="""" id=""test1"">
  </fieldset>
  <input type=""email"" value=""abc"" id=""test2"">
  <button id=""test3"">TEST</button>
  <select id=""test4""></select>
  <textarea id=""test5""></textarea>
  <output id=""test6""></output>
</form>");
            Assert.AreEqual(1, doc.Forms.Length);
            var form = doc.Forms[0] as IHtmlFormElement;
            Assert.IsNotNull(form);
            Assert.IsFalse(form.CheckValidity());
        }

        [Test]
        public void BasicValidFormValidation()
        {
            var doc = DocumentBuilder.Html(@"<form id=""fm2"">
  <fieldset>
    <input type=""text"" required value=""abc"">
  </fieldset>
  <input type=""email"" value=""test@example.com"">
  <button>TEST</button>
  <select></select>
  <textarea></textarea>
  <output></output>
</form>");
            Assert.AreEqual(1, doc.Forms.Length);
            var form = doc.Forms[0] as IHtmlFormElement;
            Assert.IsNotNull(form);
            Assert.IsTrue(form.CheckValidity());
        }

        [Test]
        public void InvalidFormValidationWithChanges()
        {
            var doc = DocumentBuilder.Html(@"<form id=""fm3"">
  <fieldset id=""fs"">
    <legend><input type=""text"" id=""inp1""></legend>
    <input type=""text"" required value="""" id=""inp2"">
  </fieldset>
</form>");
            Assert.AreEqual(1, doc.Forms.Length);
            var form = doc.Forms[0] as IHtmlFormElement;
            Assert.IsNotNull(form);
            Assert.IsFalse(form.CheckValidity());
            var fieldSet = doc.GetElementById("fs") as IHtmlFieldSetElement;
            Assert.IsNotNull(fieldSet);
            fieldSet.IsDisabled = true;
            Assert.IsTrue(form.CheckValidity());
            var input = doc.GetElementById("inp1") as IHtmlInputElement;
            Assert.IsNotNull(input);
            input.Value = "aaa";
            input.Type = "url";
            Assert.IsFalse(form.CheckValidity());
        }

        [Test]
        public void TestCustomErrorInput1()
        {
            var document = DocumentBuilder.Html("");
            var element = document.CreateElement("input") as HTMLInputElement;
            Assert.IsNotNull(element);
            element.SetCustomValidity("My custom error");
            Assert.AreEqual(true, element.Validity.IsCustomError);
            Assert.AreEqual("My custom error", element.ValidationMessage);
        }

        [Test]
        public void TestCustomErrorInput2()
        {
            var document = DocumentBuilder.Html("");
            var element = document.CreateElement("input") as HTMLInputElement;
            Assert.IsNotNull(element);
            element.SetCustomValidity("");
            Assert.AreEqual(false, element.Validity.IsCustomError);
            Assert.AreEqual("", element.ValidationMessage);
        }

        [Test]
        public void TestCustomErrorButton1()
        {
            var document = DocumentBuilder.Html("");
            var element = document.CreateElement("button") as HTMLButtonElement;
            Assert.IsNotNull(element);
            element.SetCustomValidity("My custom error");
            Assert.AreEqual(true, element.Validity.IsCustomError);
            Assert.AreEqual("My custom error", element.ValidationMessage);
        }

        [Test]
        public void TestCustomErrorButton2()
        {
            var document = DocumentBuilder.Html("");
            var element = document.CreateElement("button") as HTMLButtonElement;
            Assert.IsNotNull(element);
            element.SetCustomValidity("");
            Assert.AreEqual(false, element.Validity.IsCustomError);
            Assert.AreEqual("", element.ValidationMessage);
        }

        [Test]
        public void TestCustomErrorSelect1()
        {
            var document = DocumentBuilder.Html("");
            var element = document.CreateElement("select") as HTMLSelectElement;
            Assert.IsNotNull(element);
            var option1 = document.CreateElement<IHtmlOptionElement>();
            option1.Text = "test1";
            option1.Value = "";
            var option2 = document.CreateElement<IHtmlOptionElement>();
            option2.Text = "test1";
            option2.Value = "1";
            element.AddOption(option1);
            element.AddOption(option2);
            element.SetCustomValidity("My custom error");
            Assert.AreEqual(true, element.Validity.IsCustomError);
            Assert.AreEqual("My custom error", element.ValidationMessage);
        }

        [Test]
        public void TestCustomErrorSelect2()
        {
            var document = DocumentBuilder.Html("");
            var element = document.CreateElement("select") as HTMLSelectElement;
            Assert.IsNotNull(element);
            var option1 = document.CreateElement<IHtmlOptionElement>();
            option1.Text = "test1";
            option1.Value = "";
            var option2 = document.CreateElement<IHtmlOptionElement>();
            option2.Text = "test1";
            option2.Value = "1";
            element.AddOption(option1);
            element.AddOption(option2);
            element.SetCustomValidity("");
            Assert.AreEqual(false, element.Validity.IsCustomError);
            Assert.AreEqual("", element.ValidationMessage);
        }

        [Test]
        public void TestCustomErrorTextarea1()
        {
            var document = DocumentBuilder.Html("");
            var element = document.CreateElement("textarea") as HTMLTextAreaElement;
            Assert.IsNotNull(element);
            element.SetCustomValidity("My custom error");
            Assert.AreEqual(true, element.Validity.IsCustomError);
            Assert.AreEqual("My custom error", element.ValidationMessage);
        }

        [Test]
        public void TestCustomErrorTextarea2()
        {
            var document = DocumentBuilder.Html("");
            var element = document.CreateElement("textarea") as HTMLTextAreaElement;
            Assert.IsNotNull(element);
            element.SetCustomValidity("");
            Assert.AreEqual(false, element.Validity.IsCustomError);
            Assert.AreEqual("", element.ValidationMessage);
        }

        [Test]
        public void TestTypemismatchInputEmail1()
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
            element.SetAttribute("value", "");
            Assert.AreEqual("email", element.Type);
            Assert.AreEqual(false, element.Validity.IsTypeMismatch);
        }

        [Test]
        public void TestTypemismatchInputEmail2()
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
            element.SetAttribute("value", "test@example.com");
            Assert.AreEqual("email", element.Type);
            Assert.AreEqual(false, element.Validity.IsTypeMismatch);
        }

        [Test]
        public void TestTypemismatchInputEmail3()
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
            element.SetAttribute("value", @"
     test@example.com
    ");
            Assert.AreEqual("email", element.Type);
            Assert.AreEqual(false, element.Validity.IsTypeMismatch);
        }

        [Test]
        public void TestTypemismatchInputEmail4()
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
            Assert.AreEqual(true, element.Validity.IsTypeMismatch);
        }

        [Test]
        public void TestTypemismatchInputEmail5()
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
            element.SetAttribute("value", "test1@example.com,test2@example.com");
            Assert.AreEqual("email", element.Type);
            Assert.AreEqual(true, element.Validity.IsTypeMismatch);
        }

        [Test]
        public void TestTypemismatchInputEmail6()
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
            element.SetAttribute("multiple", "multiple");
            element.SetAttribute("value", "test1@example.com,test2@example.com");
            Assert.AreEqual("email", element.Type);
            Assert.AreEqual(false, element.Validity.IsTypeMismatch);
        }

        [Test]
        public void TestTypemismatchInputEmail7()
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
            element.SetAttribute("multiple", "multiple");
            element.SetAttribute("value", "test1@example.com;test2@example.com");
            Assert.AreEqual("email", element.Type);
            Assert.AreEqual(true, element.Validity.IsTypeMismatch);
        }

        [Test]
        public void TestTypemismatchInputUrl1()
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
            element.SetAttribute("value", "");
            Assert.AreEqual("url", element.Type);
            Assert.AreEqual(false, element.Validity.IsTypeMismatch);
        }

        [Test]
        public void TestTypemismatchInputUrl2()
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
            element.SetAttribute("value", "http://www.example.com");
            Assert.AreEqual("url", element.Type);
            Assert.AreEqual(false, element.Validity.IsTypeMismatch);
        }

        [Test]
        public void TestTypemismatchInputUrl3()
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
            element.SetAttribute("value", @"
     http://www.example.com 
     ");
            Assert.AreEqual("url", element.Type);
            Assert.AreEqual(false, element.Validity.IsTypeMismatch);
        }

        [Test]
        public void TestTypemismatchInputUrl4()
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
            Assert.AreEqual(true, element.Validity.IsTypeMismatch);
        }

        [Test]
        public void TestBadinputInputEmail1()
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
            element.SetAttribute("multiple", null);
            element.SetAttribute("value", "");
            Assert.AreEqual("email", element.Type);
            Assert.AreEqual(false, element.Validity.IsBadInput);
        }

        [Test]
        public void TestBadinputInputEmail2()
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
            element.SetAttribute("multiple", null);
            element.SetAttribute("value", "test1@example.com");
            Assert.AreEqual("email", element.Type);
            Assert.AreEqual(false, element.Validity.IsBadInput);
        }

        [Test]
        public void TestBadinputInputEmail3()
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
            element.SetAttribute("multiple", "multiple");
            element.SetAttribute("value", "test1@example.com,test2@eample.com");
            Assert.AreEqual("email", element.Type);
            Assert.AreEqual(false, element.Validity.IsBadInput);
        }

        [Test]
        public void TestBadinputInputEmail4()
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
            element.SetAttribute("multiple", "multiple");
            element.SetAttribute("value", "test,1@example.com");
            Assert.AreEqual("email", element.Type);
            Assert.AreEqual(true, element.Validity.IsBadInput);
        }

        [Test]
        public void TestBadinputInputDatetime1()
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
            element.SetAttribute("value", "");
            Assert.AreEqual("datetime", element.Type);
            Assert.AreEqual(false, element.Validity.IsBadInput);
        }

        [Test]
        public void TestBadinputInputDatetime2()
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
            element.SetAttribute("value", "2000-01-01T12:00:00Z");
            Assert.AreEqual("datetime", element.Type);
            Assert.AreEqual(false, element.Validity.IsBadInput);
        }

        [Test]
        public void TestBadinputInputDatetime3()
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
            element.SetAttribute("value", "abc");
            Assert.AreEqual("datetime", element.Type);
            Assert.AreEqual(true, element.Validity.IsBadInput);
        }

        [Test]
        public void TestBadinputInputColor1()
        {
            var document = DocumentBuilder.Html("");
            var element = document.CreateElement("input") as HTMLInputElement;
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
            element.SetAttribute("value", "");
            Assert.AreEqual("color", element.Type);
            Assert.AreEqual(true, element.Validity.IsBadInput);
        }

        [Test]
        public void TestBadinputInputColor2()
        {
            var document = DocumentBuilder.Html("");
            var element = document.CreateElement("input") as HTMLInputElement;
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
            element.SetAttribute("value", "#000000");
            Assert.AreEqual("color", element.Type);
            Assert.AreEqual(false, element.Validity.IsBadInput);
        }

        [Test]
        public void TestBadinputInputColor3()
        {
            var document = DocumentBuilder.Html("");
            var element = document.CreateElement("input") as HTMLInputElement;
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
            element.SetAttribute("value", "#FFFFFF");
            Assert.AreEqual("color", element.Type);
            Assert.AreEqual(false, element.Validity.IsBadInput);
        }

        [Test]
        public void TestBadinputInputColor4()
        {
            var document = DocumentBuilder.Html("");
            var element = document.CreateElement("input") as HTMLInputElement;
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
            element.SetAttribute("value", "abc");
            Assert.AreEqual("color", element.Type);
            Assert.AreEqual(true, element.Validity.IsBadInput);
        }

        [Test]
        public void TestIsvalidInputText1()
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
            element.SetAttribute("maxLength", "4");
            element.SetAttribute("value", "abcdef");
            element.IsDirty = true;
            Assert.AreEqual("text", element.Type);
            Assert.AreEqual(false, element.Validity.IsValid);
        }

        [Test]
        public void TestIsvalidInputText2()
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
            element.SetAttribute("pattern", "[A-Z]");
            element.SetAttribute("value", "abc");
            Assert.AreEqual("text", element.Type);
            Assert.AreEqual(false, element.Validity.IsValid);
        }

        [Test]
        public void TestIsvalidInputText3()
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
            element.SetAttribute("required", "required");
            element.SetAttribute("value", "");
            Assert.AreEqual("text", element.Type);
            Assert.AreEqual(false, element.Validity.IsValid);
        }

        [Test]
        public void TestIsvalidInputSearch1()
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
            element.SetAttribute("maxLength", "4");
            element.SetAttribute("value", "abcdef");
            element.IsDirty = true;
            Assert.AreEqual("search", element.Type);
            Assert.AreEqual(false, element.Validity.IsValid);
        }

        [Test]
        public void TestIsvalidInputSearch2()
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
            element.SetAttribute("pattern", "[A-Z]");
            element.SetAttribute("value", "abc");
            Assert.AreEqual("search", element.Type);
            Assert.AreEqual(false, element.Validity.IsValid);
        }

        [Test]
        public void TestIsvalidInputSearch3()
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
            element.SetAttribute("required", "required");
            element.SetAttribute("value", "");
            Assert.AreEqual("search", element.Type);
            Assert.AreEqual(false, element.Validity.IsValid);
        }

        [Test]
        public void TestIsvalidInputTel1()
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
            element.SetAttribute("maxLength", "4");
            element.SetAttribute("value", "abcdef");
            element.IsDirty = true;
            Assert.AreEqual("tel", element.Type);
            Assert.AreEqual(false, element.Validity.IsValid);
        }

        [Test]
        public void TestIsvalidInputTel2()
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
            element.SetAttribute("pattern", "[A-Z]");
            element.SetAttribute("value", "abc");
            Assert.AreEqual("tel", element.Type);
            Assert.AreEqual(false, element.Validity.IsValid);
        }

        [Test]
        public void TestIsvalidInputTel3()
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
            element.SetAttribute("required", "required");
            element.SetAttribute("value", "");
            Assert.AreEqual("tel", element.Type);
            Assert.AreEqual(false, element.Validity.IsValid);
        }

        [Test]
        public void TestIsvalidInputPassword1()
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
            element.SetAttribute("maxLength", "4");
            element.SetAttribute("value", "abcdef");
            element.IsDirty = true;
            Assert.AreEqual("password", element.Type);
            Assert.AreEqual(false, element.Validity.IsValid);
        }

        [Test]
        public void TestIsvalidInputPassword2()
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
            element.SetAttribute("pattern", "[A-Z]");
            element.SetAttribute("value", "abc");
            Assert.AreEqual("password", element.Type);
            Assert.AreEqual(false, element.Validity.IsValid);
        }

        [Test]
        public void TestIsvalidInputPassword3()
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
            element.SetAttribute("required", "required");
            element.SetAttribute("value", "");
            Assert.AreEqual("password", element.Type);
            Assert.AreEqual(false, element.Validity.IsValid);
        }

        [Test]
        public void TestIsvalidInputUrl1()
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
            element.SetAttribute("maxLength", "20");
            element.SetAttribute("value", "http://www.example.com");
            element.IsDirty = true;
            Assert.AreEqual("url", element.Type);
            Assert.AreEqual(false, element.Validity.IsValid);
        }

        [Test]
        public void TestIsvalidInputUrl2()
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
            element.SetAttribute("pattern", "http://www.example.com");
            element.SetAttribute("value", "http://www.example.net");
            Assert.AreEqual("url", element.Type);
            Assert.AreEqual(false, element.Validity.IsValid);
        }

        [Test]
        public void TestIsvalidInputUrl3()
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
            Assert.AreEqual(false, element.Validity.IsValid);
        }

        [Test]
        public void TestIsvalidInputUrl4()
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
            element.SetAttribute("required", "required");
            element.SetAttribute("value", "");
            Assert.AreEqual("url", element.Type);
            Assert.AreEqual(false, element.Validity.IsValid);
        }

        [Test]
        public void TestIsvalidInputEmail1()
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
            element.SetAttribute("maxLength", "10");
            element.SetAttribute("value", "test@example.com");
            element.IsDirty = true;
            Assert.AreEqual("email", element.Type);
            Assert.AreEqual(false, element.Validity.IsValid);
        }

        [Test]
        public void TestIsvalidInputEmail2()
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
            element.SetAttribute("pattern", "test@example.com");
            element.SetAttribute("value", "test@example.net");
            Assert.AreEqual("email", element.Type);
            Assert.AreEqual(false, element.Validity.IsValid);
        }

        [Test]
        public void TestIsvalidInputEmail3()
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
            Assert.AreEqual(false, element.Validity.IsValid);
        }

        [Test]
        public void TestIsvalidInputEmail4()
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
            element.SetAttribute("required", "required");
            element.SetAttribute("value", "");
            Assert.AreEqual("email", element.Type);
            Assert.AreEqual(false, element.Validity.IsValid);
        }

        [Test]
        public void TestIsvalidInputDatetime1()
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
            element.SetAttribute("value", "2001-01-01T12:00:00Z");
            Assert.AreEqual("datetime", element.Type);
            Assert.AreEqual(false, element.Validity.IsValid);
        }

        [Test]
        public void TestIsvalidInputDatetime2()
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
            element.SetAttribute("min", "2001-01-01T12:00:00Z");
            element.SetAttribute("value", "2000-01-01T12:00:00Z");
            Assert.AreEqual("datetime", element.Type);
            Assert.AreEqual(false, element.Validity.IsValid);
        }

        [Test]
        public void TestIsvalidInputDatetime3()
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
            element.SetAttribute("value", "2001-01-01T12:03:00Z");
            Assert.AreEqual("datetime", element.Type);
            Assert.AreEqual(false, element.Validity.IsValid);
        }

        [Test]
        public void TestIsvalidInputDatetime4()
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
            element.SetAttribute("required", "required");
            element.SetAttribute("value", "");
            Assert.AreEqual("datetime", element.Type);
            Assert.AreEqual(false, element.Validity.IsValid);
        }

        [Test]
        public void TestIsvalidInputDate1()
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
            element.SetAttribute("value", "2001-01-01");
            Assert.AreEqual("date", element.Type);
            Assert.AreEqual(false, element.Validity.IsValid);
        }

        [Test]
        public void TestIsvalidInputDate2()
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
            element.SetAttribute("value", "2000-01-01");
            Assert.AreEqual("date", element.Type);
            Assert.AreEqual(false, element.Validity.IsValid);
        }

        [Test]
        public void TestIsvalidInputDate3()
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
            element.SetAttribute("step", "3");
            element.SetAttribute("value", "2000-01-03");
            Assert.AreEqual("date", element.Type);
            Assert.AreEqual(false, element.Validity.IsValid);
        }

        [Test]
        public void TestIsvalidInputDate4()
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
            element.SetAttribute("required", "required");
            element.SetAttribute("value", "");
            Assert.AreEqual("date", element.Type);
            Assert.AreEqual(false, element.Validity.IsValid);
        }

        [Test]
        public void TestIsvalidInputMonth1()
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
            element.SetAttribute("value", "2001-01");
            Assert.AreEqual("month", element.Type);
            Assert.AreEqual(false, element.Validity.IsValid);
        }

        [Test]
        public void TestIsvalidInputMonth2()
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
            element.SetAttribute("value", "2000-01");
            Assert.AreEqual("month", element.Type);
            Assert.AreEqual(false, element.Validity.IsValid);
        }

        [Test]
        public void TestIsvalidInputMonth3()
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
            element.SetAttribute("step", "3");
            element.SetAttribute("value", "2001-03");
            Assert.AreEqual("month", element.Type);
            Assert.AreEqual(false, element.Validity.IsValid);
        }

        [Test]
        public void TestIsvalidInputMonth4()
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
            element.SetAttribute("required", "required");
            element.SetAttribute("value", "");
            Assert.AreEqual("month", element.Type);
            Assert.AreEqual(false, element.Validity.IsValid);
        }

        [Test]
        public void TestIsvalidInputWeek1()
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
            element.SetAttribute("value", "2001-W01");
            Assert.AreEqual("week", element.Type);
            Assert.AreEqual(false, element.Validity.IsValid);
        }

        [Test]
        public void TestIsvalidInputWeek2()
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
            element.SetAttribute("value", "2000-W01");
            Assert.AreEqual("week", element.Type);
            Assert.AreEqual(false, element.Validity.IsValid);
        }

        [Test]
        public void TestIsvalidInputWeek3()
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
            element.SetAttribute("value", "2001-W03");
            Assert.AreEqual("week", element.Type);
            Assert.AreEqual(false, element.Validity.IsValid);
        }

        [Test]
        public void TestIsvalidInputWeek4()
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
            element.SetAttribute("required", "required");
            element.SetAttribute("value", "");
            Assert.AreEqual("week", element.Type);
            Assert.AreEqual(false, element.Validity.IsValid);
        }

        [Test]
        public void TestIsvalidInputTime1()
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
            element.SetAttribute("value", "13:00:00");
            Assert.AreEqual("time", element.Type);
            Assert.AreEqual(false, element.Validity.IsValid);
        }

        [Test]
        public void TestIsvalidInputTime2()
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
            element.SetAttribute("value", "11:00:00");
            Assert.AreEqual("time", element.Type);
            Assert.AreEqual(false, element.Validity.IsValid);
        }

        [Test]
        public void TestIsvalidInputTime3()
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
            Assert.AreEqual(false, element.Validity.IsValid);
        }

        [Test]
        public void TestIsvalidInputTime4()
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
            element.SetAttribute("required", "required");
            element.SetAttribute("value", "");
            Assert.AreEqual("time", element.Type);
            Assert.AreEqual(false, element.Validity.IsValid);
        }

        [Test]
        public void TestIsvalidInputNumber1()
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
            Assert.AreEqual(false, element.Validity.IsValid);
        }

        [Test]
        public void TestIsvalidInputNumber2()
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
            element.SetAttribute("value", "4");
            Assert.AreEqual("number", element.Type);
            Assert.AreEqual(false, element.Validity.IsValid);
        }

        [Test]
        public void TestIsvalidInputNumber3()
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
            Assert.AreEqual(false, element.Validity.IsValid);
        }

        [Test]
        public void TestIsvalidInputNumber4()
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
            element.SetAttribute("required", "required");
            element.SetAttribute("value", "");
            Assert.AreEqual("number", element.Type);
            Assert.AreEqual(false, element.Validity.IsValid);
        }

        [Test]
        public void TestIsvalidInputCheckbox1()
        {
            var document = DocumentBuilder.Html("");
            var element = document.CreateElement("input") as HTMLInputElement;
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
            Assert.AreEqual(false, element.Validity.IsValid);
        }

        [Test]
        public void TestIsvalidInputRadio1()
        {
            var document = DocumentBuilder.Html("");
            var element = document.CreateElement("input") as HTMLInputElement;
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
            Assert.AreEqual(false, element.Validity.IsValid);
        }

        [Test]
        public void TestIsvalidInputFile1()
        {
            var document = DocumentBuilder.Html("");
            var element = document.CreateElement("input") as HTMLInputElement;
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
            Assert.AreEqual("file", element.Type);
            Assert.AreEqual(false, element.Validity.IsValid);
        }

        [Test]
        public void TestIsvalidSelect1()
        {
            var document = DocumentBuilder.Html("");
            var element = document.CreateElement("select") as HTMLSelectElement;
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
            element.SetAttribute("value", "");
            Assert.AreEqual(false, element.Validity.IsValid);
        }

        [Test]
        public void TestIsvalidTextarea1()
        {
            var document = DocumentBuilder.Html("");
            var element = document.CreateElement("textarea") as HTMLTextAreaElement;
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
            Assert.AreEqual(false, element.Validity.IsValid);
        }
    }
}
