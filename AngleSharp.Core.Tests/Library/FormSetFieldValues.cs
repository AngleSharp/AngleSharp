using AngleSharp.Dom.Html;
using AngleSharp.Extensions;
using AngleSharp.Parser.Html;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AngleSharp.Core.Tests.Library
{
    [TestFixture]
    public sealed class FormSetFieldValues
    {
        private static IHtmlDocument CreateSampleDocument()
        {
            const string formHtml = @"
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
            var document = parser.Parse(formHtml);
            return document;
        }

        [Test]
        public void SetValueOfTextInput()
        {
            const string inputId = "user";
            const string myName = "yehudah";

            var document = CreateSampleDocument();
            document.Forms[0].SetFieldValues(new Dictionary<string, string>()
            {
                [inputId] = myName
            });

            var input = document.GetElementById(inputId) as IHtmlInputElement;
            Assert.AreEqual(myName, input.Value);
        }

        [Test]
        public void SetValueOfSelect()
        {
            var document = CreateSampleDocument();
            document.Forms[0].SetFieldValues(new Dictionary<string, string>()
            {
                ["city"] = "2"
            });

            var jerusalemOption = document.GetElementById("cityOption0") as IHtmlOptionElement;
            Assert.IsFalse(jerusalemOption.IsSelected);

            var newYorkOption = document.GetElementById("cityOption1") as IHtmlOptionElement;
            Assert.IsFalse(newYorkOption.IsSelected);

            var londonOption = document.GetElementById("cityOption2") as IHtmlOptionElement;
            Assert.IsTrue(londonOption.IsSelected);
        }

        [Test]
        public void SetValueOfRadioInput()
        {
            var document = CreateSampleDocument();
            document.Forms[0].SetFieldValues(new Dictionary<string, string>()
            {
                ["userType"] = "Guest"
            });

            var guestOption = document.GetElementById("guestOption") as IHtmlInputElement;
            Assert.IsTrue(guestOption.IsChecked);

            var memberOption = document.GetElementById("memberOption") as IHtmlInputElement;
            Assert.IsFalse(memberOption.IsChecked);

            var managerOption = document.GetElementById("managerOption") as IHtmlInputElement;
            Assert.IsFalse(managerOption.IsChecked);
        }

        [Test]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void ThrowExcptionIfFieldNotFound()
        {
            var document = CreateSampleDocument();
            document.Forms[0].SetFieldValues(new Dictionary<string, string>()
            {
                ["noExistName"] = "X"
            });
        }

        [Test]
        public void CreateNewInputIfFieldNotFound()
        {
            const string newFieldName = "phone";
            const string fieldValue = "1234";

            var document = CreateSampleDocument();
            document.Forms[0].SetFieldValues(new Dictionary<string, string>()
            {
                [newFieldName] = fieldValue
            }, createInputIfNoFound: true);

            var newField = document.Forms[0]
                .GetElements<IHtmlInputElement>()
                .SingleOrDefault(x => x.Name == newFieldName);

            Assert.NotNull(newFieldName);
            Assert.AreEqual(fieldValue, newField.Value);
        }
    }
}