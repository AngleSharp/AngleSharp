namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class InputCloneTests
    {
        [TestCase("checkbox")]
        [TestCase("radio")]
        public void ClonePreservesIndeterminateState(String type)
        {
            var document = ("<input type=" + type + ">").ToHtmlDocument();
            var source = document.QuerySelector<IHtmlInputElement>("input");
            source.IsIndeterminate = true;

            var clone = (IHtmlInputElement)source.Clone();

            Assert.IsTrue(clone.IsIndeterminate);
            clone.IsIndeterminate = false;
            Assert.IsTrue(source.IsIndeterminate);
        }
    }
}
