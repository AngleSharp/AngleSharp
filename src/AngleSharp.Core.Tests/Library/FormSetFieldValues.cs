namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using AngleSharp.Html.Parser;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [TestFixture]
    public sealed class FormSetFieldValuesTests
    {
        private static IHtmlDocument CreateSampleDocument()
        {
            const String formHtml = @"
<html>
<body>
    <form>
        <!-- text input -->
        <input type='text' name='user' id='user' value='X' />

        <!-- radio input -->
        <input type='radio' name='userType' id='memberOption' value='Member' />
        <input type='radio' name='userType' id='managerOption' value='Manager' checked='checked' />
        <input type='radio' name='userType' id='guestOption' value='Guest' />

        <!-- select -->
        <select name='city' id='city'>
            <option value='0' id='cityOption0'>Jerusalem</option>
            <option value='1' id='cityOption1' selected='selected'>New york</option>
            <option value='2' id='cityOption2'>London</option>
        </select>
    </form>
</body>
</html>";
            var parser = new HtmlParser();
            var document = parser.ParseDocument(formHtml);
            return document;
        }

        [Test]
        public void SetValueOfTextInput()
        {
            const String inputId = "user";
            const String myName = "yehudah";

            var document = CreateSampleDocument();
            document.Forms[0].SetValues(new Dictionary<String, String>()
            {
                { inputId, myName }
            });

            var input = document.GetElementById(inputId) as IHtmlInputElement;
            Assert.AreEqual(myName, input.Value);
        }

        [Test]
        public void SetValueOfSelect()
        {
            var document = CreateSampleDocument();
            document.Forms[0].SetValues(new Dictionary<String, String>()
            {
                { "city", "2" }
            });

            var jerusalemOption = document.GetElementById("cityOption0") as IHtmlOptionElement;
            var newYorkOption = document.GetElementById("cityOption1") as IHtmlOptionElement;
            var londonOption = document.GetElementById("cityOption2") as IHtmlOptionElement;

            Assert.IsFalse(jerusalemOption.IsSelected);
            Assert.IsFalse(newYorkOption.IsSelected);
            Assert.IsTrue(londonOption.IsSelected);
        }

        [Test]
        public void SetValueOfRadioInput()
        {
            var document = CreateSampleDocument();
            document.Forms[0].SetValues(new Dictionary<String, String>()
            {
                { "userType", "Guest" }
            });

            var guestOption = document.GetElementById("guestOption") as IHtmlInputElement;
            var memberOption = document.GetElementById("memberOption") as IHtmlInputElement;
            var managerOption = document.GetElementById("managerOption") as IHtmlInputElement;

            Assert.IsTrue(guestOption.IsChecked);
            Assert.IsFalse(memberOption.IsChecked);
            Assert.IsFalse(managerOption.IsChecked);
        }

        [Test]
        public void ThrowExcptionIfFieldNotFound()
        {
            var document = CreateSampleDocument();

            Assert.Catch<KeyNotFoundException>(() =>
            {
                document.Forms[0].SetValues(new Dictionary<String, String>()
                {
                    { "noExistName", "X" }
                });
            });
        }

        [Test]
        public void CreateNewInputIfFieldNotFound()
        {
            const String newFieldName = "phone";
            const String fieldValue = "1234";

            var document = CreateSampleDocument();
            document.Forms[0].SetValues(new Dictionary<String, String>()
            {
                { newFieldName, fieldValue }
            }, createMissing: true);

            var newField = document.Forms[0]
                .GetNodes<IHtmlInputElement>()
                .SingleOrDefault(x => x.Name == newFieldName);

            Assert.NotNull(newFieldName);
            Assert.AreEqual(fieldValue, newField.Value);
        }
    }
}