using AngleSharp;
using AngleSharp.DOM.Html;
using NUnit.Framework;
using System;

namespace UnitTests.Html
{
    [TestFixture]
    public class ValidationTests
    {
        #region Forms

        static readonly String Form1 = @"<form id=""fm1"">
  <fieldset id=""test0"">
    <input type=""text"" required value="""" id=""test1"">
  </fieldset>
  <input type=""email"" value=""abc"" id=""test2"">
  <button id=""test3"">TEST</button>
  <select id=""test4""></select>
  <textarea id=""test5""></textarea>
  <output id=""test6""></output>
</form>";

        static readonly String Form2 = @"<form id=""fm2"">
  <fieldset>
    <input type=""text"" required value=""abc"">
  </fieldset>
  <input type=""email"" value=""test@example.com"">
  <button>TEST</button>
  <select></select>
  <textarea></textarea>
  <output></output>
</form>";

        static readonly String Form3 = @"<form id=""fm3"">
  <fieldset id=""fs"">
    <legend><input type=""text"" id=""inp1""></legend>
    <input type=""text"" required value="""" id=""inp2"">
  </fieldset>
</form>";

        #endregion

        [Test]
        public void BasicInvalidFormValidation()
        {
            var doc = DocumentBuilder.Html(Form1);
            Assert.AreEqual(1, doc.Forms.Length);
            var form = doc.Forms[0] as IHtmlFormElement;
            Assert.IsNotNull(form);
            Assert.IsFalse(form.CheckValidity());
        }

        [Test]
        public void BasicValidFormValidation()
        {
            var doc = DocumentBuilder.Html(Form2);
            Assert.AreEqual(1, doc.Forms.Length);
            var form = doc.Forms[0] as IHtmlFormElement;
            Assert.IsNotNull(form);
            Assert.IsTrue(form.CheckValidity());
        }

        [Test]
        public void InvalidFormValidationWithChanges()
        {
            var doc = DocumentBuilder.Html(Form3);
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
    }
}
