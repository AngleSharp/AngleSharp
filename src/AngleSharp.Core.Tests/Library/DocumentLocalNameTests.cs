namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Dom;
    using NUnit.Framework;
    using System;
    using System.Text.RegularExpressions;

    [TestFixture]
    public class DocumentLocalNameTests
    {
        [TestCase("div", TestName = "LocalNameCase01")]
        [TestCase("f<oo", TestName = "LocalNameCase02")]
        [TestCase("foo}", TestName = "LocalNameCase03")]
        [TestCase("a=b", TestName = "LocalNameCase04")]
        [TestCase("a\u0001\u000b\u007f", TestName = "LocalNameCase05")]
        [TestCase(":", TestName = "LocalNameCase06")]
        [TestCase("_", TestName = "LocalNameCase07")]
        [TestCase(":a0-._:", TestName = "LocalNameCase08")]
        [TestCase("\u0080name", TestName = "LocalNameCase09")]
        [TestCase("\u00a0name", TestName = "LocalNameCase10")]
        [TestCase("\uffffname", TestName = "LocalNameCase11")]
        [TestCase("\U0010ffffname", TestName = "LocalNameCase12")]
        public void CreateElementAcceptsValidLocalName(String name)
        {
            var document = "".ToHtmlDocument();
            Assert.AreEqual(name, document.CreateElement(name).LocalName);
        }

        [TestCase("data-value", TestName = "LocalNameCase13")]
        [TestCase("1", TestName = "LocalNameCase14")]
        [TestCase("@", TestName = "LocalNameCase15")]
        [TestCase("<", TestName = "LocalNameCase16")]
        [TestCase("\"'", TestName = "LocalNameCase17")]
        [TestCase("\u0001\u000b\u007f", TestName = "LocalNameCase18")]
        [TestCase("\u0080\u00a0\uffff", TestName = "LocalNameCase19")]
        [TestCase("\U0010ffff", TestName = "LocalNameCase20")]
        public void CreateAttributeAcceptsValidLocalName(String name)
        {
            var document = "".ToHtmlDocument();
            Assert.AreEqual(name, document.CreateAttribute(name).LocalName);
        }

        [TestCase("", TestName = "LocalNameCase21")]
        [TestCase(null, TestName = "LocalNameCase22")]
        [TestCase("a\0b", TestName = "LocalNameCase23")]
        [TestCase("a\tb", TestName = "LocalNameCase24")]
        [TestCase("a\nb", TestName = "LocalNameCase25")]
        [TestCase("a\fb", TestName = "LocalNameCase26")]
        [TestCase("a\rb", TestName = "LocalNameCase27")]
        [TestCase("a b", TestName = "LocalNameCase28")]
        [TestCase("a/b", TestName = "LocalNameCase29")]
        [TestCase("a>b", TestName = "LocalNameCase30")]
        public void FactoriesRejectForbiddenLocalNames(String name)
        {
            var document = "".ToHtmlDocument();
            AssertInvalidCharacter(() => document.CreateElement(name));
            AssertInvalidCharacter(() => document.CreateAttribute(name));
        }

        [TestCase("1foo", TestName = "LocalNameCase31")]
        [TestCase("-foo", TestName = "LocalNameCase32")]
        [TestCase(".foo", TestName = "LocalNameCase33")]
        [TestCase("\u007ffoo", TestName = "LocalNameCase34")]
        [TestCase(":a<b", TestName = "LocalNameCase35")]
        [TestCase("_a=b", TestName = "LocalNameCase36")]
        [TestCase("\u0080a}", TestName = "LocalNameCase37")]
        [TestCase("\U0010ffffa\u000b", TestName = "LocalNameCase38")]
        public void CreateElementRejectsInvalidLocalName(String name)
        {
            var document = "".ToHtmlDocument();
            AssertInvalidCharacter(() => document.CreateElement(name));
        }

        [Test]
        public void FactoriesAcceptUnpairedSurrogates()
        {
            var document = "".ToHtmlDocument();

            // Construct these at runtime: custom-attribute strings use UTF-8.
            foreach (var c in new[] { (Char)0xd800, (Char)0xdfff })
            {
                var name = c.ToString();
                Assert.AreEqual(name, document.CreateElement(name).LocalName);
                Assert.AreEqual(name, document.CreateAttribute(name).LocalName);
            }
        }

        [Test]
        public void CreateAttributeRejectsEqualsSign()
        {
            var document = "".ToHtmlDocument();
            AssertInvalidCharacter(() => document.CreateAttribute("a=b"));
        }

        [Test]
        public void FactoriesValidateAsciiNamesAgainstDomRules()
        {
            // The DOM Standard's regex, also used by dom/nodes/name-validation.html.
            var elementPattern = new Regex(@"\A(?:[A-Za-z][^\x00\t\n\f\r />]*|[:_\u0080-\uffff][A-Za-z0-9.:_\u0080-\uffff-]*)\z");
            var attributePattern = new Regex(@"\A[^\x00\t\n\f\r />=]+\z");
            var document = "".ToHtmlDocument();

            for (var first = 0; first < 128; first++)
            {
                for (var second = 0; second < 128; second++)
                {
                    var name = new String(new[] { (Char)first, (Char)second });

                    if (elementPattern.IsMatch(name))
                    {
                        Assert.DoesNotThrow(() => document.CreateElement(name));
                    }
                    else
                    {
                        AssertInvalidCharacter(() => document.CreateElement(name));
                    }

                    if (attributePattern.IsMatch(name))
                    {
                        Assert.DoesNotThrow(() => document.CreateAttribute(name));
                    }
                    else
                    {
                        AssertInvalidCharacter(() => document.CreateAttribute(name));
                    }
                }
            }
        }

        [Test]
        public void NamespaceFactoriesStillValidateQualifiedNames()
        {
            var document = "".ToHtmlDocument();
            AssertInvalidCharacter(() => document.CreateElement("urn:test", "f<oo"));
            AssertInvalidCharacter(() => document.CreateAttribute("urn:test", "@"));
            Assert.AreEqual("item", document.CreateElement("urn:test", "p:item").LocalName);
            Assert.AreEqual("item", document.CreateAttribute("urn:test", "p:item").LocalName);
            Assert.AreEqual((Int32)DomError.Namespace, Assert.Throws<DomException>(() => document.CreateElement(null, "p:item")).Code);
            Assert.AreEqual((Int32)DomError.Namespace, Assert.Throws<DomException>(() => document.CreateAttribute(null, "p:item")).Code);
        }

        private static void AssertInvalidCharacter(TestDelegate action)
        {
            Assert.AreEqual((Int32)DomError.InvalidCharacter, Assert.Throws<DomException>(action).Code);
        }
    }
}
