namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class SelectSubmissionTests
    {
        [TestCase("", "<option value=a selected>A</option><option value=b>B</option>", true, "before=1&after=2")]
        [TestCase("multiple", "<option value=a selected>A</option><option value=b>B</option>", true, "before=1&after=2")]
        [TestCase("multiple", "<option value=a>A</option><option value=b>B</option>", false, "before=1&after=2")]
        [TestCase("size=2", "<option value=a>A</option><option value=b>B</option>", false, "before=1&after=2")]
        [TestCase("", "<option value=a selected disabled>A</option><option value=b>B</option>", false, "before=1&after=2")]
        [TestCase("", "<optgroup disabled><option value=a selected>A</option></optgroup><option value=b>B</option>", false, "before=1&after=2")]
        [TestCase("multiple", "<optgroup disabled><option value=a selected>A</option></optgroup><option value=b selected>B</option><option value=c selected>C</option>", false, "before=1&choice=b&choice=c&after=2")]
        [TestCase("", "<option value=a>A</option><option value=b>B</option>", false, "before=1&choice=a&after=2")]
        public void SubmissionIncludesOnlySelectedEnabledOptions(String attributes, String options, Boolean clearSelection, String expectedQuery)
        {
            var document = ("<form action='https://example.com/submit'><input name=before value=1>" +
                "<select name=choice " + attributes + ">" + options + "</select>" +
                "<input name=after value=2></form>").ToHtmlDocument();
            var select = document.QuerySelector<IHtmlSelectElement>("select");
            var form = document.QuerySelector<IHtmlFormElement>("form");

            if (clearSelection)
            {
                select.Value = "missing";
            }

            var request = form.GetSubmission();

            Assert.IsNotNull(request);
            Assert.AreEqual(expectedQuery, request.Target.Query);
        }
    }
}
