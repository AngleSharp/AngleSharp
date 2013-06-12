using AngleSharp;
using AngleSharp.DOM;
using AngleSharp.DOM.Collections;
using AngleSharp.DOM.Html;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class CssSelectors
    {
        HTMLDocument document;

        public CssSelectors()
        {
            document = DocumentBuilder.Html(Assets.SelectorsWebpage);
        }

        string GetAttributeValue(Node node, string attrName)
        {
            var element = node as Element;

            if (element != null)
                return element.GetAttribute(attrName);

            return null;
        }

        HTMLCollection RunQuery(string query)
        {
            return document.QuerySelectorAll(query);
        }

        [TestMethod]
        public void PseudoSelectorFirstChild()
        {
            Assert.AreEqual(8, RunQuery("*:first-child").Length);
            Assert.AreEqual(1, RunQuery("p:first-child").Length);
        }

        [TestMethod]
        public void PseudoSelectorLastChild()
        {
            Assert.AreEqual(7, RunQuery("*:last-child").Length);
            Assert.AreEqual(2, RunQuery("p:last-child").Length);
        }
        
        [TestMethod]
        public void PseudoSelectorOnlyChild()
        {
            Assert.AreEqual(3, RunQuery("*:only-child").Length);
            Assert.AreEqual(1, RunQuery("p:only-child").Length);
        }

        [TestMethod]
        public void PseudoSelectorEmpty()
        {
            var results = RunQuery("*:empty");
            Assert.AreEqual(2, results.Length);
            Assert.AreEqual("head", results[0].NodeName);
            Assert.AreEqual("input", results[1].NodeName);
        }
        
        [TestMethod]
        public void NthChildNoPrefixWithDigit()
        {
            var result = RunQuery(":nth-child(2)");

            Assert.AreEqual(4, result.Length);
            Assert.AreEqual("body", result[0].NodeName);
            Assert.AreEqual("p", result[1].NodeName);
            Assert.AreEqual("span", result[2].NodeName);
            Assert.AreEqual("p", result[3].NodeName);
        }

        [TestMethod]
        public void NthChildStarPrefixWithDigit()
        {
            var result = RunQuery("*:nth-child(2)");

            Assert.AreEqual(4, result.Length);
            Assert.AreEqual("body", result[0].NodeName);
            Assert.AreEqual("p", result[1].NodeName);
            Assert.AreEqual("span", result[2].NodeName);
            Assert.AreEqual("p", result[3].NodeName);
        }

        [TestMethod]
        public void NthChildElementPrefixWithDigit()
        {
            var result = RunQuery("p:nth-child(2)");

            Assert.AreEqual(2, result.Length);
            Assert.AreEqual("p", result[0].NodeName);
            Assert.AreEqual("p", result[1].NodeName);
        }

        [TestMethod]
        public void NthLastChildNoPrefixWithDigit()
        {
            var result = RunQuery(":nth-last-child(2)");

            Assert.AreEqual(4, result.Length);
            Assert.AreEqual("head", result[0].NodeName);
            Assert.AreEqual("div", result[1].NodeName);
            Assert.AreEqual("span", result[2].NodeName);
            Assert.AreEqual("div", result[3].NodeName);
        }
        
        [TestMethod]
        public void NthLastChildIdPrefixWithDigit()
        {
            var result = RunQuery("#myDiv :nth-last-child(2)");

            Assert.AreEqual(2, result.Length);
            Assert.AreEqual("div", result[0].NodeName);
            Assert.AreEqual("span", result[1].NodeName);
        }

        [TestMethod]
        public void NthLastChildElementPrefixWithDigit()
        {
            var result = RunQuery("span:nth-last-child(3)");

            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void NthLastChildElementPrefixWithDigit2()
        {
            var result = RunQuery("span:nth-last-child(2)");

            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("span", result[0].NodeName);
        }
        
        [TestMethod]
        public void MultipleSelectorsCommaSupportWithNoSpace()
        {
            var result = RunQuery("p.hiclass,a");

            Assert.AreEqual(3, result.Length);
            Assert.AreEqual("a", result[0].NodeName);
            Assert.AreEqual("p", result[1].NodeName);
        }

        [TestMethod]
        public void MultipleSelectorsCommaSupportWithPostPendedSpace()
        {
            var result = RunQuery("p.hiclass, a");

            Assert.AreEqual(3, result.Length);
            Assert.AreEqual("a", result[0].NodeName);
            Assert.AreEqual("p", result[1].NodeName);
        }

        [TestMethod]
        public void MultipleSelectorsCommaSupportWithPrepostpendedSpace()
        {
            var result = RunQuery("p.hiclass , a");

            Assert.AreEqual(3, result.Length);
            Assert.AreEqual("a", result[0].NodeName);
            Assert.AreEqual("p", result[1].NodeName);
        }
        
        [TestMethod]
        public void MultipleSelectorsCommaSupportWithPrependedSpace()
        {
            var result = RunQuery("p.hiclass ,a");

            Assert.AreEqual(3, result.Length);
            Assert.AreEqual("a", result[0].NodeName);
            Assert.AreEqual("p", result[1].NodeName);
        }

        [TestMethod]
        public void IdSelectorBasicSelector()
        {
            var result = RunQuery("#myDiv");

            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("div", result[0].NodeName);
        }

        [TestMethod]
        public void IdSelectorWithElement()
        {
            var result = RunQuery("div#myDiv");

            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("div", result[0].NodeName);
        }

        [TestMethod]
        public void IdSelectorWithExistingIdDescendant()
        {
            var result = RunQuery("#theBody #myDiv");

            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("div", result[0].NodeName);
        }

        [TestMethod]
        public void IdSelectorWithNonExistantIdDescendant()
        {
            var result = RunQuery("#theBody #whatwhatwhat");

            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void IdSelectorWithNonExistantIdAncestor()
        {
            var result = RunQuery("#whatwhatwhat #someOtherDiv");

            Assert.AreEqual(0, result.Length);
        }
        
        [TestMethod]
        public void IdSelectorAllDescendantsOfId()
        {
            var result = RunQuery("#myDiv *");

            Assert.AreEqual(5, result.Length);
            Assert.AreEqual("div", result[0].NodeName);
            Assert.AreEqual("p", result[1].NodeName);
        }

        [TestMethod]
        public void IdSelectorChildId()
        {
            var result = RunQuery("#theBody>#myDiv");

            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("div", result[0].NodeName);
        }

        [TestMethod]
        public void IdSelectorNotAChildId()
        {
            var result = RunQuery("#theBody>#someOtherDiv");

            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void IdSelectorAllChildrenOfId()
        {
            var result = RunQuery("#myDiv>*");

            Assert.AreEqual(2, result.Length);
            Assert.AreEqual("div", result[0].NodeName);
            Assert.AreEqual("p", result[1].NodeName);
        }

        [TestMethod]
        public void IdSelectorAllChildrenofIdWithNoChildren()
        {
            var result = RunQuery("#someOtherDiv>*");

            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void ElementSelectorStar()
        {
            Assert.AreEqual(16, RunQuery("*").Length);
        }

        [TestMethod]
        public void ElementSelectorSingleTagName()
        {
            Assert.AreEqual(1, RunQuery("body").Length);
            Assert.AreEqual("body", RunQuery("body")[0].NodeName);
        }

        [TestMethod]
        public void ElementSelectorSingleTagNameMatchingMultipleElements()
        {
            Assert.AreEqual(3, RunQuery("p").Length);
            Assert.AreEqual("p", RunQuery("p")[0].NodeName);
            Assert.AreEqual("p", RunQuery("p")[1].NodeName);
            Assert.AreEqual("p", RunQuery("p")[2].NodeName);
        }

        [TestMethod]
        public void ElementSelectorBasicNegativePrecedence()
        {
            Assert.AreEqual(0, RunQuery("head p").Length);
        }

        [TestMethod]
        public void ElementSelectorBasicPositivePrecedenceTwoTags()
        {
            Assert.AreEqual(2, RunQuery("div p").Length);
        }

        [TestMethod]
        public void ElementSelectorBasicPositivePrecedenceTwoTagsWithGrandchildDescendant()
        {
            Assert.AreEqual(2, RunQuery("div a").Length);
        }

        [TestMethod]
        public void ElementSelectorBasicPositivePrecedenceThreeTags()
        {
            Assert.AreEqual(1, RunQuery("div p a").Length);
            Assert.AreEqual("a", RunQuery("div p a")[0].NodeName);
        }

        [TestMethod]
        public void ElementSelectorBasicPositivePrecedenceWithSameTags()
        {
            Assert.AreEqual(1, RunQuery("div div").Length);
        }

        [TestMethod]
        public void ElementSelectorBasicPositivePrecedenceWithinForm()
        {
            Assert.AreEqual(1, RunQuery("form input").Length);
        }

        [TestMethod]
        public void ClassSelectorBasic()
        {
            var result = RunQuery(".checkit");

            Assert.AreEqual(2, result.Length);
            Assert.AreEqual("div", result[0].NodeName);
            Assert.AreEqual("div", result[1].NodeName);
        }

        [TestMethod]
        public void ClassSelectorChained()
        {
            var result = RunQuery(".omg.ohyeah");

            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("p", result[0].NodeName);
            Assert.AreEqual("eeeee", result[0].TextContent);
        }

        [TestMethod]
        public void ClassSelectorWithElement()
        {
            var result = RunQuery("p.ohyeah");

            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("p", result[0].NodeName);
            Assert.AreEqual("eeeee", result[0].TextContent);
        }

        [TestMethod]
        public void ClassSelectorParentClassSelector()
        {
            var result = RunQuery("div .ohyeah");

            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("p", result[0].NodeName);
            Assert.AreEqual("eeeee", result[0].TextContent);
        }

        [TestMethod]
        public void ComplexSelectorChildWithPreAndPostSpace()
        {
            Assert.AreEqual(2, RunQuery("div > p").Length);
        }

        [TestMethod]
        public void ComplexSelectorChildWithPostSpace()
        {
            Assert.AreEqual(2, RunQuery("div> p").Length);
        }

        [TestMethod]
        public void ComplexSelectorChildWithPreSpace()
        {
            Assert.AreEqual(2, RunQuery("div >p").Length);
        }

        [TestMethod]
        public void ComplexSelectorChildWithNoSpace()
        {
            Assert.AreEqual(2, RunQuery("div>p").Length);
        }

        [TestMethod]
        public void ComplexSelectorChildWithClass()
        {
            Assert.AreEqual(1, RunQuery("div > p.ohyeah").Length);
        }

        [TestMethod]
        public void ComplexSelectorAllChildren()
        {
            Assert.AreEqual(3, RunQuery("p > *").Length);
        }

        [TestMethod]
        public void ComplexSelectorAllGrandChildren()
        {
            Assert.AreEqual(3, RunQuery("div > * > *").Length);
        }

        [TestMethod]
        public void ComplexSelectorAdjacentWithPreAndPostSpace()
        {
            Assert.AreEqual(1, RunQuery("a + span").Length);
        }

        [TestMethod]
        public void ComplexSelectorAdjacentWithPostSpace()
        {
            Assert.AreEqual(1, RunQuery("a+ span").Length);
        }

        [TestMethod]
        public void ComplexSelectorAdjacentWithPreSpace()
        {
            Assert.AreEqual(1, RunQuery("a +span").Length);
        }

        [TestMethod]
        public void ComplexSelectorAdjacentWithNoSpace()
        {
            Assert.AreEqual(1, RunQuery("a+span").Length);
        }

        [TestMethod]
        public void ComplexSelectorCommaChildAndAdjacent()
        {
            Assert.AreEqual(3, RunQuery("a + span, div > p").Length);
        }

        [TestMethod]
        public void ComplexSelectorGeneralSiblingCombinator()
        {
            var result = RunQuery("div ~ form");
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("form", result[0].NodeName);
        }

        [TestMethod]
        public void AttributeSelectorElementAttrExists()
        {
            var results = RunQuery("div[id]");

            Assert.AreEqual(2, results.Length);
            Assert.AreEqual("div", results[0].NodeName);
            Assert.AreEqual("div", results[1].NodeName);
        }

        [TestMethod]
        public void AttributeSelectorElementAttrEqualsWithDoubleQuotes()
        {
            var results = RunQuery("div[id=\"someOtherDiv\"]");

            Assert.AreEqual(1, results.Length);
            Assert.AreEqual("div", results[0].NodeName);
        }

        [TestMethod]
        public void AttributeSelectorElementAttrSpaceSeparatedWithDoubleQuotes()
        {
            var results = RunQuery("p[class~=\"ohyeah\"]");

            Assert.AreEqual(1, results.Length);
            Assert.AreEqual("p", results[0].NodeName);
            Assert.AreEqual("eeeee", results[0].TextContent);
        }

        [TestMethod]
        public void AttributeSelectorElementAttrSpaceSeparatedWithEmptyValue()
        {
            Assert.AreEqual(0, RunQuery("p[class~='']").Length);
        }

        [TestMethod]
        public void AttributeSelectorElementAttrHyphenSeparatedWithDoubleQuotes()
        {
            var results = RunQuery("span[class|=\"separated\"]");

            Assert.AreEqual(1, results.Length);
            Assert.AreEqual("span", results[0].NodeName);
            Assert.AreEqual("test", results[0].TextContent);
        }

        [TestMethod]
        public void AttributeSelectorImplicitStarAttrExactWithDoubleQuotes()
        {
            var results = RunQuery("[class=\"checkit\"]");

            Assert.AreEqual(2, results.Length);
            Assert.AreEqual("div", results[0].NodeName);
            Assert.AreEqual("woooeeeee", results[0].TextContent);
            Assert.AreEqual("div", results[1].NodeName);
            Assert.AreEqual("woootooowe", results[1].TextContent);
        }

        [TestMethod]
        public void AttributeSelectorStarAttrExactWithDoubleQuotes()
        {
            var results = RunQuery("*[class=\"checkit\"]");

            Assert.AreEqual(2, results.Length);
            Assert.AreEqual("div", results[0].NodeName);
            Assert.AreEqual("woooeeeee", results[0].TextContent);
            Assert.AreEqual("div", results[1].NodeName);
            Assert.AreEqual("woootooowe", results[1].TextContent);
        }

        [TestMethod]
        public void AttributeSelectorStarAttrPrefix()
        {
            var results = RunQuery("*[class^=check]");

            Assert.AreEqual(2, results.Length);
            Assert.AreEqual("div", results[0].NodeName);
            Assert.AreEqual("woooeeeee", results[0].TextContent);
            Assert.AreEqual("div", results[1].NodeName);
            Assert.AreEqual("woootooowe", results[1].TextContent);
        }

        [TestMethod]
        public void AttributeSelectorStarAttrPrefixWithEmptyValue()
        {
            Assert.AreEqual(0, RunQuery("*[class^='']").Length);
        }

        [TestMethod]
        public void AttributeSelectorStarAttrSuffix()
        {
            var results = RunQuery("*[class$=it]");

            Assert.AreEqual(2, results.Length);
            Assert.AreEqual("div", results[0].NodeName);
            Assert.AreEqual("woooeeeee", results[0].TextContent);
            Assert.AreEqual("div", results[1].NodeName);
            Assert.AreEqual("woootooowe", results[1].TextContent);
        }

        [TestMethod]
        public void AttributeSelectorStarAttrSuffixWithEmptyValue()
        {
            Assert.AreEqual(0, RunQuery("*[class$='']").Length);
        }

        [TestMethod]
        public void AttributeSelectorStarAttrSubstring()
        {
            var results = RunQuery("*[class*=heck]");

            Assert.AreEqual(2, results.Length);
            Assert.AreEqual("div", results[0].NodeName);
            Assert.AreEqual("woooeeeee", results[0].TextContent);
            Assert.AreEqual("div", results[1].NodeName);
            Assert.AreEqual("woootooowe", results[1].TextContent);
        }

        [TestMethod]
        public void AttributeSelectorStarAttrSubstringWithEmptyValue()
        {
            Assert.AreEqual(0, RunQuery("*[class*='']").Length);
        }

        [TestMethod]
        public void AttributeSelectorElementAttrNotEqual()
        {
            var results = RunQuery("p[class!='hiclass']");
            Assert.AreEqual(2, results.Length);
            Assert.IsNull(GetAttributeValue(results[0], "class"));
            Assert.AreEqual("eeeee", results[1].TextContent);
        }
    }
}
