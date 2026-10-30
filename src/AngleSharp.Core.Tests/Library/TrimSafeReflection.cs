namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Common;
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using AngleSharp.Io;
    using AngleSharp.Svg.Dom;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// The extensions that used to reflect over runtime types, rewritten so the library is safe
    /// under trimming and Native AOT. The tests calling them with anonymous objects also show that
    /// those calls bind to the generic overloads: warnings are errors here, so a call landing on an
    /// obsolete Object overload would not compile.
    /// </summary>
    [TestFixture]
    public class TrimSafeReflectionTests
    {
        [Test]
        public void CreateElementPicksTheTypeTheAssemblyScanPickedForEveryElementInterface()
        {
            // CreateElement used to scan the assembly for the first type implementing the interface;
            // it now reads a fixed table. Replaying the scan here pins the table to it - and fails for
            // a new element interface until the table has an entry for it. The general interfaces
            // are left out on purpose: the scan gave them an arbitrary element.
            var general = new[] { typeof(IElement), typeof(IHtmlElement), typeof(ISvgElement) };
            var document = "".ToHtmlDocument();
            var types = typeof(BrowsingContext).Assembly.GetTypes();
            var create = typeof(DocumentExtensions).GetMethod(nameof(DocumentExtensions.CreateElement));
            var interfaces = types.Where(m => m.IsPublic && m.IsInterface && typeof(IElement).IsAssignableFrom(m)).ToArray();
            Assert.Greater(interfaces.Length, 70);

            foreach (var elementInterface in interfaces)
            {
                var expected = types.FirstOrDefault(m => !m.IsAbstract && m.GetInterfaces().Contains(elementInterface));
                var invoke = () => create.MakeGenericMethod(elementInterface).Invoke(null, new Object[] { document });

                // The scan found nothing for these, or a constructor it could not call (the unknown
                // element needs a local name); both ended in an ArgumentException, as the general
                // interfaces now do.
                if (expected is null || elementInterface == typeof(IHtmlUnknownElement) || general.Contains(elementInterface))
                {
                    var ex = Assert.Throws<TargetInvocationException>(() => invoke(), elementInterface.Name);
                    Assert.IsInstanceOf<ArgumentException>(ex.InnerException, elementInterface.Name);
                }
                else
                {
                    var element = (IElement)invoke();
                    Assert.AreEqual(expected, element.GetType(), elementInterface.Name);
                    Assert.AreSame(document, element.Owner, elementInterface.Name);
                }
            }
        }

        [Test]
        public void ToDictionaryReadsTheDeclaredTypesProperties()
        {
            var values = new { name = "angle", count = 2 }.ToDictionary();
            Assert.AreEqual(2, values.Count);
            Assert.AreEqual("angle", values["name"]);
            Assert.AreEqual("2", values["count"]);
        }

        [Test]
        public void ToDictionaryCopiesADictionaryInsteadOfReflectingOverIt()
        {
            var source = new Dictionary<String, String> { ["name"] = "angle" };
            var values = source.ToDictionary();
            Assert.AreEqual(1, values.Count);
            Assert.AreEqual("angle", values["name"]);
            Assert.AreNotSame(source, values);
        }

        [Test]
        public void ToDictionaryOfNullIsEmpty()
        {
            var values = default(Uri).ToDictionary();
            Assert.AreEqual(0, values.Count);
        }

        [Test]
        public void GenericToDictionaryRejectsAValueDeclaredAsObject()
        {
            // Only reachable with an explicit type argument: a value declared as Object binds to
            // the obsolete Object overload instead.
            Object values = new { name = "angle" };
            Assert.Throws<ArgumentException>(() => values.ToDictionary<Object>());
        }

#pragma warning disable CS0618 // The obsolete Object overloads are what this test checks
        [Test]
        public void ObsoleteObjectOverloadsStillReadTheRuntimeType()
        {
            // Code compiled against the Object overloads keeps calling them, and a value declared
            // as Object still binds to them, so they must behave as before.
            Object fields = new { q = "angle" };

            Assert.AreEqual("angle", fields.ToDictionary()["q"]);

            var document = "<form><input name=q></form><ul><li>a</li></ul>".ToHtmlDocument();
            document.QuerySelector<IHtmlFormElement>("form").SubmitAsync(fields);
            Assert.AreEqual("angle", document.QuerySelector<IHtmlInputElement>("input").Value);

            var items = document.QuerySelectorAll("li");
            items.Attr((Object)new { title = "item" });
            Assert.AreEqual(new[] { "item" }, items.Attr("title").ToArray());

            var response = VirtualResponse.Create(r => r.Headers((Object)new { foo = "1" }));
            Assert.AreEqual("1", response.Headers["foo"]);
        }
#pragma warning restore CS0618

        [Test]
        public void AttrSetsAttributesFromAnAnonymousObjectAndFromADictionary()
        {
            var document = "<ul><li>a</li><li>b</li></ul>".ToHtmlDocument();
            var items = document.QuerySelectorAll("li");

            items.Attr(new { title = "item" });
            items.Attr(new Dictionary<String, String> { ["lang"] = "en" });

            Assert.AreEqual(new[] { "item", "item" }, items.Attr("title").ToArray());
            Assert.AreEqual(new[] { "en", "en" }, items.Attr("lang").ToArray());
        }

        [Test]
        public void VirtualResponseHeadersFromAnAnonymousObjectAndFromADictionary()
        {
            var response = VirtualResponse.Create(r => r
                .Headers(new { foo = "1" })
                .Headers(new Dictionary<String, String> { ["bar"] = "2" }));

            Assert.AreEqual("1", response.Headers["foo"]);
            Assert.AreEqual("2", response.Headers["bar"]);
        }

        [Test]
        public void SubmitAsyncAcceptsADictionaryThroughTheGenericOverload()
        {
            var document = "<form><input name=q></form>".ToHtmlDocument();
            var form = document.QuerySelector<IHtmlFormElement>("form");

            // The generic overload is the better match for a Dictionary; the fields must still be
            // copied rather than reflected over, or the dictionary's own properties would be sent.
            form.SubmitAsync(new Dictionary<String, String> { ["q"] = "angle" });

            Assert.AreEqual("angle", document.QuerySelector<IHtmlInputElement>("input").Value);
        }
    }
}
