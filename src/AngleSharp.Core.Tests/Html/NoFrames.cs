namespace AngleSharp.Core.Tests.Html
{
    using AngleSharp.Html.Parser;
    using NUnit.Framework;

    [TestFixture]
    public class NoFramesTests
    {
        [Test]
        public void ParsingWithNoSupportForFramesShouldRespectNoFrames()
        {
            var source = @"  <frameset cols=""30%,*"">
    <frame src=""frame1.php"" name=""frame1"">
    <frame src=""frame2.php"" name=""frame2"">
    <noframes>
      <body>
        <p>This text will appear only if the browser does not support frames.</p>
        <p><img src=""..\..\assets\img\city10.png"" alt=""city10""></p>
      </body>
    </noframes>
  </frameset>";
            var parser = new HtmlParser(new HtmlParserOptions
            {
                IsNotSupportingFrames = true
            });
            var document = parser.ParseDocument(source);
            var noFramesParagraphs = document.QuerySelectorAll("p");
            Assert.AreEqual(2, noFramesParagraphs.Length);
        }

        [Test]
        public void ParsingWithSupportForFramesShouldIgnoreNoFrames()
        {
            var source = @"  <frameset cols=""30%,*"">
    <frame src=""frame1.php"" name=""frame1"">
    <frame src=""frame2.php"" name=""frame2"">
    <noframes>
      <body>
        <p>This text will appear only if the browser does not support frames.</p>
        <p><img src=""..\..\assets\img\city10.png"" alt=""city10""></p>
      </body>
    </noframes>
  </frameset>";
            var parser = new HtmlParser(new HtmlParserOptions
            {
                IsNotSupportingFrames = false
            });
            var document = parser.ParseDocument(source);
            var noFramesParagraphs = document.QuerySelectorAll("p");
            Assert.AreEqual(0, noFramesParagraphs.Length);
        }

        [Test]
        public void ParsingWithNoSupportForFramesShouldPlaceContentInBody()
        {
            var source = @"  <frameset>
    <noframes>
      <p>This text will appear only if the browser does not support frames.</p>
      <p><img src=""..\..\assets\img\city10.png"" alt=""city10""></p>
    </noframes>
  </frameset>";
            var parser = new HtmlParser(new HtmlParserOptions
            {
                IsNotSupportingFrames = true
            });
            var document = parser.ParseDocument(source);
            var noFramesParagraphs = document.QuerySelectorAll("p");
            Assert.AreEqual(2, noFramesParagraphs.Length);
        }
    }
}
