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
    }
}
