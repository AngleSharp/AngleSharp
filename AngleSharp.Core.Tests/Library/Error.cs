using AngleSharp.Core.Tests.Mocks;
using AngleSharp.Events;
using AngleSharp.Parser.Html;
using NUnit.Framework;

namespace AngleSharp.Core.Tests.Library
{
    [TestFixture]
    public class ErrorTests
    {
        [Test]
        public void GivenLineNumbersShouldBeCorrect()
        {
            var source = @"<article class=""grid-item large"">
    <div class=""grid-image""><a href=""/News/Page/298/cpp-mva-course""><img src=""/img/news/maxresdefault700x240.png"" alt=""Icon"" title=""C++ MVA Course"" /></a></div>
    <div class=""grid-title""><a href=""/News/Page/298/cpp-mva-course"">C++ MVA Course</a></div>
    <div class=""grid-abstract"">My Microsoft Virtual Academy course about modern C++ is now available.</div>
    <div class=""grid-date"">6/5/2015</div>
        <div class=""grid-admin"">        <a href=""/Page/Delete/298"">Delete</a> | <a href=""/Page/Edit/298"">Edit</a> | <a href=""/Page/Create?parentId=1"">Create New</a>
</div>
    </article>
<article class=""grid-item medium"">
    <div class=""grid-image""><a href=""/News/Page/297/dotnet-developer-conference""><img src=""/img/news/8640.pic23150x150.png"" alt=""Icon"" title="".NET Developer Conference"" /></a></div>
    <div class=""grid-title""><a href=""/News/Page/297/dotnet-developer-conference"">.NET Developer Conference</a></div>
    <div class=""grid-abstract"">The sixth edition of the DDC features many interesting talks.</div>
    <div class=""grid-date"">5/28/2015</div>
        <div class=""grid-admin"">        <a href=""/Page/Delete/297"">Delete</a> | <a href=""/Page/Edit/297"">Edit</a> | <a href=""/Page/Create?parentId=1"">Create New</a>
</div>
    </article>
<article class=""grid-item normal even"">
    <div class=""grid-image""><a href=""/News/Page/296/spider-language""><img src=""/img/news/programming-languages_2150x150.png"" alt=""Icon"" title=""Spider Language"" /></a></div>
    <div class=""grid-title""><a href=""/News/Page/296/spider-language"">Spider Language</a></div>
    <div class=""grid-abstract"">Bored of JavaScript&#39;s slow evolution? Maybe its time to try Spider.</div>
        <div class=""grid-date"">5/25/2015</div>
        <div class=""grid-admin"">        <a href=""/Page/Delete/296"">Delete</a> | <a href=""/Page/Edit/296"">Edit</a> | <a href=""/Page/Create?parentId=1"">Create New</a>
</div>
    </article>";
            var parseErrors = new EventReceiver<HtmlParseErrorEvent>();
            var config = new Configuration(events: parseErrors);
            var document = source.ToHtmlDocument(config);

            Assert.AreEqual(4, parseErrors.Received.Count);
            Assert.AreEqual((int)HtmlParseError.DoctypeMissing, parseErrors.Received[0].Code);
            Assert.AreEqual(1, parseErrors.Received[0].Position.Column);
            Assert.AreEqual(1, parseErrors.Received[0].Position.Line);

            Assert.AreEqual((int)HtmlParseError.TagCannotBeSelfClosed, parseErrors.Received[1].Code);
            Assert.AreEqual(69, parseErrors.Received[1].Position.Column);
            Assert.AreEqual(2, parseErrors.Received[1].Position.Line);

            Assert.AreEqual((int)HtmlParseError.TagCannotBeSelfClosed, parseErrors.Received[2].Code);
            Assert.AreEqual(82, parseErrors.Received[2].Position.Column);
            Assert.AreEqual(10, parseErrors.Received[2].Position.Line);

            Assert.AreEqual((int)HtmlParseError.TagCannotBeSelfClosed, parseErrors.Received[3].Code);
            Assert.AreEqual(70, parseErrors.Received[3].Position.Column);
            Assert.AreEqual(18, parseErrors.Received[3].Position.Line);
        }
    }
}
