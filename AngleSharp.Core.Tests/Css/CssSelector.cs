using AngleSharp;
using AngleSharp.Dom;
using NUnit.Framework;
using System;

namespace AngleSharp.Core.Tests
{
    [TestFixture]
    public class CssSelectorTests
    {
        IDocument document;

        [SetUp]
        public void Setup()
        {
            document = Assets.selectors.ToHtmlDocument();
        }

        string GetAttributeValue(INode node, String attrName)
        {
            var element = node as IElement;

            if (element != null)
                return element.GetAttribute(attrName);

            return null;
        }

        IHtmlCollection<IElement> RunQuery(String query)
        {
            return document.QuerySelectorAll(query);
        }

        [Test]
        public void PseudoSelectorFirstChild()
        {
            Assert.AreEqual(7, RunQuery("*:first-child").Length);
            Assert.AreEqual(1, RunQuery("p:first-child").Length);
        }

        [Test]
        public void PseudoSelectorLastChild()
        {
            Assert.AreEqual(7, RunQuery("*:last-child").Length);
            Assert.AreEqual(2, RunQuery("p:last-child").Length);
        }
        
        [Test]
        public void PseudoSelectorOnlyChild()
        {
            Assert.AreEqual(3, RunQuery("*:only-child").Length);
            Assert.AreEqual(1, RunQuery("p:only-child").Length);
        }

        [Test]
        public void PseudoSelectorEmpty()
        {
            var results = RunQuery("*:empty");
            Assert.AreEqual(2, results.Length);
            Assert.AreEqual("head", results[0].GetTagName());
            Assert.AreEqual("input", results[1].GetTagName());
        }
        
        [Test]
        public void NthChildNoPrefixWithDigit()
        {
            var result = RunQuery(":nth-child(2)");

            Assert.AreEqual(4, result.Length);
            Assert.AreEqual("body", result[0].GetTagName());
            Assert.AreEqual("p", result[1].GetTagName());
            Assert.AreEqual("span", result[2].GetTagName());
            Assert.AreEqual("p", result[3].GetTagName());
        }

        [Test]
        public void NthChildStarPrefixWithDigit()
        {
            var result = RunQuery("*:nth-child(2)");

            Assert.AreEqual(4, result.Length);
            Assert.AreEqual("body", result[0].GetTagName());
            Assert.AreEqual("p", result[1].GetTagName());
            Assert.AreEqual("span", result[2].GetTagName());
            Assert.AreEqual("p", result[3].GetTagName());
        }

        [Test]
        public void NthChildElementPrefixWithDigit()
        {
            var result = RunQuery("p:nth-child(2)");

            Assert.AreEqual(2, result.Length);
            Assert.AreEqual("p", result[0].GetTagName());
            Assert.AreEqual("p", result[1].GetTagName());
        }

        [Test]
        public void NthLastChildNoPrefixWithDigit()
        {
            var result = RunQuery(":nth-last-child(2)");

            Assert.AreEqual(4, result.Length);
            Assert.AreEqual("head", result[0].GetTagName());
            Assert.AreEqual("div", result[1].GetTagName());
            Assert.AreEqual("span", result[2].GetTagName());
            Assert.AreEqual("div", result[3].GetTagName());
        }
        
        [Test]
        public void NthLastChildIdPrefixWithDigit()
        {
            var result = RunQuery("#myDiv :nth-last-child(2)");

            Assert.AreEqual(2, result.Length);
            Assert.AreEqual("div", result[0].GetTagName());
            Assert.AreEqual("span", result[1].GetTagName());
        }

        [Test]
        public void NthLastChildElementPrefixWithDigit()
        {
            var result = RunQuery("span:nth-last-child(3)");

            Assert.AreEqual(0, result.Length);
        }

        [Test]
        public void NthLastChildElementPrefixWithDigit2()
        {
            var result = RunQuery("span:nth-last-child(2)");

            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("span", result[0].GetTagName());
        }
        
        [Test]
        public void MultipleSelectorsCommaSupportWithNoSpace()
        {
            var result = RunQuery("p.hiclass,a");

            Assert.AreEqual(3, result.Length);
            Assert.AreEqual("a", result[0].GetTagName());
            Assert.AreEqual("p", result[1].GetTagName());
        }

        [Test]
        public void MultipleSelectorsCommaSupportWithPostPendedSpace()
        {
            var result = RunQuery("p.hiclass, a");

            Assert.AreEqual(3, result.Length);
            Assert.AreEqual("a", result[0].GetTagName());
            Assert.AreEqual("p", result[1].GetTagName());
        }

        [Test]
        public void MultipleSelectorsCommaSupportWithPrepostpendedSpace()
        {
            var result = RunQuery("p.hiclass , a");

            Assert.AreEqual(3, result.Length);
            Assert.AreEqual("a", result[0].GetTagName());
            Assert.AreEqual("p", result[1].GetTagName());
        }
        
        [Test]
        public void MultipleSelectorsCommaSupportWithPrependedSpace()
        {
            var result = RunQuery("p.hiclass ,a");

            Assert.AreEqual(3, result.Length);
            Assert.AreEqual("a", result[0].GetTagName());
            Assert.AreEqual("p", result[1].GetTagName());
        }

        [Test]
        public void IdSelectorBasicSelector()
        {
            var result = RunQuery("#myDiv");

            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("div", result[0].GetTagName());
        }

        [Test]
        public void IdSelectorWithElement()
        {
            var result = RunQuery("div#myDiv");

            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("div", result[0].GetTagName());
        }

        [Test]
        public void IdSelectorWithExistingIdDescendant()
        {
            var result = RunQuery("#theBody #myDiv");

            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("div", result[0].GetTagName());
        }

        [Test]
        public void IdSelectorWithNonExistantIdDescendant()
        {
            var result = RunQuery("#theBody #whatwhatwhat");

            Assert.AreEqual(0, result.Length);
        }

        [Test]
        public void IdSelectorWithNonExistantIdAncestor()
        {
            var result = RunQuery("#whatwhatwhat #someOtherDiv");

            Assert.AreEqual(0, result.Length);
        }
        
        [Test]
        public void IdSelectorAllDescendantsOfId()
        {
            var result = RunQuery("#myDiv *");

            Assert.AreEqual(5, result.Length);
            Assert.AreEqual("div", result[0].GetTagName());
            Assert.AreEqual("p", result[1].GetTagName());
        }

        [Test]
        public void IdSelectorChildId()
        {
            var result = RunQuery("#theBody>#myDiv");

            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("div", result[0].GetTagName());
        }

        [Test]
        public void IdSelectorNotAChildId()
        {
            var result = RunQuery("#theBody>#someOtherDiv");

            Assert.AreEqual(0, result.Length);
        }

        [Test]
        public void IdSelectorAllChildrenOfId()
        {
            var result = RunQuery("#myDiv>*");

            Assert.AreEqual(2, result.Length);
            Assert.AreEqual("div", result[0].GetTagName());
            Assert.AreEqual("p", result[1].GetTagName());
        }

        [Test]
        public void IdSelectorAllChildrenofIdWithNoChildren()
        {
            var result = RunQuery("#someOtherDiv>*");

            Assert.AreEqual(0, result.Length);
        }

        [Test]
        public void ElementSelectorStar()
        {
            Assert.AreEqual(16, RunQuery("*").Length);
        }

        [Test]
        public void ElementSelectorSingleTagName()
        {
            Assert.AreEqual(1, RunQuery("body").Length);
            Assert.AreEqual("body", RunQuery("body")[0].GetTagName());
        }

        [Test]
        public void ElementSelectorSingleTagNameMatchingMultipleElements()
        {
            Assert.AreEqual(3, RunQuery("p").Length);
            Assert.AreEqual("p", RunQuery("p")[0].GetTagName());
            Assert.AreEqual("p", RunQuery("p")[1].GetTagName());
            Assert.AreEqual("p", RunQuery("p")[2].GetTagName());
        }

        [Test]
        public void ElementSelectorBasicNegativePrecedence()
        {
            Assert.AreEqual(0, RunQuery("head p").Length);
        }

        [Test]
        public void ElementSelectorBasicPositivePrecedenceTwoTags()
        {
            Assert.AreEqual(2, RunQuery("div p").Length);
        }

        [Test]
        public void ElementSelectorBasicPositivePrecedenceTwoTagsWithGrandchildDescendant()
        {
            Assert.AreEqual(2, RunQuery("div a").Length);
        }

        [Test]
        public void ElementSelectorBasicPositivePrecedenceThreeTags()
        {
            Assert.AreEqual(1, RunQuery("div p a").Length);
            Assert.AreEqual("a", RunQuery("div p a")[0].GetTagName());
        }

        [Test]
        public void ElementSelectorBasicPositivePrecedenceWithSameTags()
        {
            Assert.AreEqual(1, RunQuery("div div").Length);
        }

        [Test]
        public void ElementSelectorBasicPositivePrecedenceWithinForm()
        {
            Assert.AreEqual(1, RunQuery("form input").Length);
        }

        [Test]
        public void ClassSelectorBasic()
        {
            var result = RunQuery(".checkit");

            Assert.AreEqual(2, result.Length);
            Assert.AreEqual("div", result[0].GetTagName());
            Assert.AreEqual("div", result[1].GetTagName());
        }

        [Test]
        public void ClassSelectorChained()
        {
            var result = RunQuery(".omg.ohyeah");

            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("p", result[0].GetTagName());
            Assert.AreEqual("eeeee", result[0].TextContent);
        }

        [Test]
        public void ClassSelectorWithElement()
        {
            var result = RunQuery("p.ohyeah");

            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("p", result[0].GetTagName());
            Assert.AreEqual("eeeee", result[0].TextContent);
        }

        [Test]
        public void ClassSelectorParentClassSelector()
        {
            var result = RunQuery("div .ohyeah");

            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("p", result[0].GetTagName());
            Assert.AreEqual("eeeee", result[0].TextContent);
        }

        [Test]
        public void ComplexSelectorChildWithPreAndPostSpace()
        {
            Assert.AreEqual(2, RunQuery("div > p").Length);
        }

        [Test]
        public void ComplexSelectorChildWithPostSpace()
        {
            Assert.AreEqual(2, RunQuery("div> p").Length);
        }

        [Test]
        public void ComplexSelectorChildWithPreSpace()
        {
            Assert.AreEqual(2, RunQuery("div >p").Length);
        }

        [Test]
        public void ComplexSelectorChildWithNoSpace()
        {
            Assert.AreEqual(2, RunQuery("div>p").Length);
        }

        [Test]
        public void ComplexSelectorChildWithClass()
        {
            Assert.AreEqual(1, RunQuery("div > p.ohyeah").Length);
        }

        [Test]
        public void ComplexSelectorAllChildren()
        {
            Assert.AreEqual(3, RunQuery("p > *").Length);
        }

        [Test]
        public void ComplexSelectorAllGrandChildren()
        {
            Assert.AreEqual(3, RunQuery("div > * > *").Length);
        }

        [Test]
        public void ComplexSelectorAdjacentWithPreAndPostSpace()
        {
            Assert.AreEqual(1, RunQuery("a + span").Length);
        }

        [Test]
        public void ComplexSelectorAdjacentWithPostSpace()
        {
            Assert.AreEqual(1, RunQuery("a+ span").Length);
        }

        [Test]
        public void ComplexSelectorAdjacentWithPreSpace()
        {
            Assert.AreEqual(1, RunQuery("a +span").Length);
        }

        [Test]
        public void ComplexSelectorAdjacentWithNoSpace()
        {
            Assert.AreEqual(1, RunQuery("a+span").Length);
        }

        [Test]
        public void ComplexSelectorCommaChildAndAdjacent()
        {
            Assert.AreEqual(3, RunQuery("a + span, div > p").Length);
        }

        [Test]
        public void ComplexSelectorGeneralSiblingCombinator()
        {
            var result = RunQuery("div ~ form");
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("form", result[0].GetTagName());
        }

        [Test]
        public void AttributeSelectorElementAttrExists()
        {
            var results = RunQuery("div[id]");

            Assert.AreEqual(2, results.Length);
            Assert.AreEqual("div", results[0].GetTagName());
            Assert.AreEqual("div", results[1].GetTagName());
        }

        [Test]
        public void AttributeSelectorElementAttrEqualsWithDoubleQuotes()
        {
            var results = RunQuery("div[id=\"someOtherDiv\"]");

            Assert.AreEqual(1, results.Length);
            Assert.AreEqual("div", results[0].GetTagName());
        }

        [Test]
        public void AttributeSelectorElementAttrSpaceSeparatedWithDoubleQuotes()
        {
            var results = RunQuery("p[class~=\"ohyeah\"]");

            Assert.AreEqual(1, results.Length);
            Assert.AreEqual("p", results[0].GetTagName());
            Assert.AreEqual("eeeee", results[0].TextContent);
        }

        [Test]
        public void AttributeSelectorElementAttrSpaceSeparatedWithEmptyValue()
        {
            Assert.AreEqual(0, RunQuery("p[class~='']").Length);
        }

        [Test]
        public void AttributeSelectorElementAttrHyphenSeparatedWithDoubleQuotes()
        {
            var results = RunQuery("span[class|=\"separated\"]");
            Assert.AreEqual(0, results.Length);
        }

        [Test]
        public void AttributeSelectorImplicitStarAttrExactWithDoubleQuotes()
        {
            var results = RunQuery("[class=\"checkit\"]");

            Assert.AreEqual(2, results.Length);
            Assert.AreEqual("div", results[0].GetTagName());
            Assert.AreEqual("woooeeeee", results[0].TextContent);
            Assert.AreEqual("div", results[1].GetTagName());
            Assert.AreEqual("woootooowe", results[1].TextContent);
        }

        [Test]
        public void AttributeSelectorStarAttrExactWithDoubleQuotes()
        {
            var results = RunQuery("*[class=\"checkit\"]");

            Assert.AreEqual(2, results.Length);
            Assert.AreEqual("div", results[0].GetTagName());
            Assert.AreEqual("woooeeeee", results[0].TextContent);
            Assert.AreEqual("div", results[1].GetTagName());
            Assert.AreEqual("woootooowe", results[1].TextContent);
        }

        [Test]
        public void AttributeSelectorStarAttrPrefix()
        {
            var results = RunQuery("*[class^=check]");

            Assert.AreEqual(2, results.Length);
            Assert.AreEqual("div", results[0].GetTagName());
            Assert.AreEqual("woooeeeee", results[0].TextContent);
            Assert.AreEqual("div", results[1].GetTagName());
            Assert.AreEqual("woootooowe", results[1].TextContent);
        }

        [Test]
        public void AttributeSelectorStarAttrPrefixWithEmptyValue()
        {
            Assert.AreEqual(0, RunQuery("*[class^='']").Length);
        }

        [Test]
        public void AttributeSelectorStarAttrSuffix()
        {
            var results = RunQuery("*[class$=it]");

            Assert.AreEqual(2, results.Length);
            Assert.AreEqual("div", results[0].GetTagName());
            Assert.AreEqual("woooeeeee", results[0].TextContent);
            Assert.AreEqual("div", results[1].GetTagName());
            Assert.AreEqual("woootooowe", results[1].TextContent);
        }

        [Test]
        public void AttributeSelectorStarAttrSuffixWithEmptyValue()
        {
            Assert.AreEqual(0, RunQuery("*[class$='']").Length);
        }

        [Test]
        public void AttributeSelectorStarAttrSubstring()
        {
            var results = RunQuery("*[class*=heck]");

            Assert.AreEqual(2, results.Length);
            Assert.AreEqual("div", results[0].GetTagName());
            Assert.AreEqual("woooeeeee", results[0].TextContent);
            Assert.AreEqual("div", results[1].GetTagName());
            Assert.AreEqual("woootooowe", results[1].TextContent);
        }

        [Test]
        public void AttributeSelectorStarAttrSubstringWithEmptyValue()
        {
            Assert.AreEqual(0, RunQuery("*[class*='']").Length);
        }

        [Test]
        public void AttributeSelectorElementAttrNotEqual()
        {
            var results = RunQuery("p[class!='hiclass']");
            Assert.AreEqual(2, results.Length);
            Assert.IsNull(GetAttributeValue(results[0], "class"));
            Assert.AreEqual("eeeee", results[1].TextContent);
        }

        [Test]
        public void HasSelectorSimple()
        {
            var source = @"<div><p>Hello in a paragraph</p></div>
<div>Hello again! (with no paragraph)</div>";

            var document = source.ToHtmlDocument();
            var selector = "div:has(p)";
            var result = document.QuerySelectorAll(selector);
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(document.Body.ChildNodes[0], result[0]);
        }

        [Test]
        public void HasSelectorNegated()
        {
            var source = @"<div><section id=first><div><h1></h1></div></section><section id=second></section><section><h5></h5></section></div>";

            var document = source.ToHtmlDocument();
            var selector = "section:not(:has(h1, h2, h3, h4, h5, h6))";
            var result = document.QuerySelectorAll(selector);
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("second", result[0].Id);
        }

        [Test]
        public void HasSelectorNegatedSwapped()
        {
            var source = @"<div><section id=first><div><h1></h1></div></section><section id=second></section><section><h5></h5></section></div>";

            var document = source.ToHtmlDocument();
            var selector = "section:has(:not(h1, h2, h3, h4, h5, h6))";
            var result = document.QuerySelectorAll(selector);
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("first", result[0].Id);
        }

        [Test]
        public void MatchesWithTwoElements()
        {
            var source = @"<div><h1></h1></div><main><h1></h1></main><section><h1></h1></section><footer><h1></h1></footer>";

            var document = source.ToHtmlDocument();
            var selector = ":matches(div, section) > h1";
            var result = document.QuerySelectorAll(selector);
            Assert.AreEqual(2, result.Length);
            Assert.AreEqual("h1", result[0].GetTagName());
            Assert.AreEqual("h1", result[1].GetTagName());
            Assert.AreEqual("div", result[0].Parent.GetTagName());
            Assert.AreEqual("section", result[1].Parent.GetTagName());
        }

        [Test]
        public void MatchesWithClasses()
        {
            var source = @"<span>1</span><span class=italic>2</span><span class=this>3</span><span>4</span><span class=that>5</span><span class=this>6</span>";

            var document = source.ToHtmlDocument();
            var selector = "span:matches(.this, .that)";
            var result = document.QuerySelectorAll(selector);
            Assert.AreEqual(3, result.Length);
            Assert.AreEqual("span", result[0].GetTagName());
            Assert.AreEqual("span", result[1].GetTagName());
            Assert.AreEqual("span", result[2].GetTagName());
            Assert.AreEqual("this", result[0].ClassName);
            Assert.AreEqual("that", result[1].ClassName);
            Assert.AreEqual("this", result[2].ClassName);
            Assert.AreEqual("3", result[0].TextContent);
            Assert.AreEqual("5", result[1].TextContent);
            Assert.AreEqual("6", result[2].TextContent);
        }

        [Test]
        public void MatchesDoubleElements()
        {
            var source = @"<div><h1></h1></div><article><h2></h2></article><section><h2></h2><article><h3></h3></article></section><aside><h3></h3><h3></h3></aside><nav><div><h4></h4></div></nav>";
            var selector = @":matches(section, article, aside, nav) :matches(h1, h2, h3, h4, h5, h6)";
            var equivalent = @"section h1, section h2, section h3, section h4, section h5, section h6, 
article h1, article h2, article h3, article h4, article h5, article h6, 
aside h1, aside h2, aside h3, aside h4, aside h5, aside h6, 
nav h1, nav h2, nav h3, nav h4, nav h5, nav h6";

            var document = source.ToHtmlDocument();
            var actual = document.QuerySelectorAll(selector);
            var expected = document.QuerySelectorAll(equivalent);
            Assert.AreEqual(6, actual.Length);
            Assert.AreEqual(expected.Length, actual.Length);

            for (int i = 0; i < 6; i++)
                Assert.AreSame(expected[i], actual[i]);
        }

        [Test]
        public void NthChildEvenWorking()
        {
            var source = @"<span>1</span><span class=italic>2</span><span class=this>3</span><span>4</span><span class=that>5</span><span class=this>6</span>";

            var document = source.ToHtmlDocument();
            var selector = "span:nth-child(even)";
            var result = document.QuerySelectorAll(selector);
            Assert.AreEqual(3, result.Length);
            Assert.AreEqual("span", result[0].GetTagName());
            Assert.AreEqual("span", result[1].GetTagName());
            Assert.AreEqual("span", result[2].GetTagName());
            Assert.AreEqual("italic", result[0].ClassName);
            Assert.AreEqual(null, result[1].ClassName);
            Assert.AreEqual("this", result[2].ClassName);
            Assert.AreEqual("2", result[0].TextContent);
            Assert.AreEqual("4", result[1].TextContent);
            Assert.AreEqual("6", result[2].TextContent);
        }

        [Test]
        public void NthChildNegativeOffsetLargeSlopeWorking()
        {
            var source = @"<span>1</span><span class=italic>2</span><span class=this>3</span><span>4</span><span class=that>5</span><span class=this>6</span>";

            var document = source.ToHtmlDocument();
            var selector = "span:nth-child(10n-1) ";
            var result = document.QuerySelectorAll(selector);
            Assert.AreEqual(0, result.Length);
        }

        [Test]
        public void NthChildPositiveOffsetLargeSlopeWorking()
        {
            var source = @"<span>1</span><span class=italic>2</span><span class=this>3</span><span>4</span><span class=that>5</span><span class=this>6</span>";

            var document = source.ToHtmlDocument();
            var selector = "span:nth-child(10n+1) ";
            var result = document.QuerySelectorAll(selector);
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("span", result[0].GetTagName());
            Assert.AreEqual(null, result[0].ClassName);
            Assert.AreEqual("1", result[0].TextContent);
        }

        [Test]
        public void NthChildInvalidSelector()
        {
            var source = @"<span>1</span><span class=italic>2</span><span class=this>3</span><span>4</span><span class=that>5</span><span class=this>6</span>";

            var document = source.ToHtmlDocument();
            var selector = "span:nth-child(10n+-1) ";

            Assert.Catch<DomException>(() =>
            {
                var result = document.QuerySelectorAll(selector);
            });
        }

        [Test]
        public void NthChildAllNegativeN()
        {
            var source = @"<span>1</span><span class=italic>2</span><span class=this>3</span><span>4</span><span class=that>5</span><span class=this>6</span>";

            var document = source.ToHtmlDocument();
            var selector = "*:nth-child(-n+3)";
            var result = document.QuerySelectorAll(selector);
            Assert.AreEqual(5, result.Length);
            Assert.AreEqual("head", result[0].GetTagName());
            Assert.AreEqual("body", result[1].GetTagName());
            Assert.AreEqual("span", result[2].GetTagName());
            Assert.AreEqual("span", result[3].GetTagName());
            Assert.AreEqual("span", result[4].GetTagName());
            Assert.AreEqual("1", result[2].TextContent);
            Assert.AreEqual("2", result[3].TextContent);
            Assert.AreEqual("3", result[4].TextContent);
        }

        [Test]
        public void NthChildOfSpanThis()
        {
            var source = @"<span>1</span><span class=italic>2</span><span class=this>3</span><span>4</span><span class=that>5</span><span class=this>6</span>";

            var document = source.ToHtmlDocument();
            var selector = "*:nth-child(-n+3 of span.this)";
            var result = document.QuerySelectorAll(selector);
            Assert.AreEqual(2, result.Length);
            Assert.AreEqual("span", result[0].GetTagName());
            Assert.AreEqual("this", result[0].ClassName);
            Assert.AreEqual("3", result[0].TextContent);

            Assert.AreEqual("span", result[1].GetTagName());
            Assert.AreEqual("this", result[1].ClassName);
            Assert.AreEqual("6", result[1].TextContent);
        }

        [Test]
        public void NthChildSpanThisApplied()
        {
            var source = @"<span>1</span><span class=italic>2</span><span class=this>3</span><span>4</span><span class=that>5</span><span class=this>6</span>";

            var document = source.ToHtmlDocument();
            var selector = "span.this:nth-child(-n+3)";
            var result = document.QuerySelectorAll(selector);
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("span", result[0].GetTagName());
            Assert.AreEqual("this", result[0].ClassName);
            Assert.AreEqual("3", result[0].TextContent);
        }
    }
}
