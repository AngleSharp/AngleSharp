namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Core.Tests.Mocks;
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// The registry of custom elements and when it tells a host about elements, which is what
    /// the scripts of a page would get to see. How a script does that is for AngleSharp.Js.
    /// </summary>
    [TestFixture]
    public class CustomElementsTests
    {
        private sealed class Host : ICustomElementHost
        {
            public List<String> Calls { get; } = new List<String>();

            public String[] Observed { get; set; } = Array.Empty<String>();

            public Boolean IsFailing { get; set; }

            public Action<IElement> OnConstruct { get; set; }

            public Action<IElement, String> OnCall { get; set; }

            public CustomElementDescription Describe(IDocument document, Object constructor)
            {
                if (constructor is not String)
                {
                    throw new InvalidOperationException("Not a constructor.");
                }

                return new CustomElementDescription(Observed, new Dictionary<String, Object>
                {
                    { "connectedCallback", "connected" },
                    { "disconnectedCallback", "disconnected" },
                    { "attributeChangedCallback", "attribute" },
                    { "adoptedCallback", "adopted" },
                });
            }

            public Boolean Construct(IElement element, CustomElementDefinition definition)
            {
                Calls.Add($"construct {Describe(element)}");
                OnConstruct?.Invoke(element);
                return !IsFailing;
            }

            public void Invoke(IDocument document, IElement element, Object callback, params Object[] arguments)
            {
                var values = String.Join(",", arguments.Select(m => m is IDocument ? "document" : m ?? "null"));
                Calls.Add($"{callback} {Describe(element)}{(values.Length > 0 ? " " + values : "")}");
                OnCall?.Invoke(element, (String)callback);
            }

            private static String Describe(IElement element) => String.IsNullOrEmpty(element.Id) ? element.LocalName : element.Id;
        }

        private static async Task<(IDocument Document, Host Host, CustomElementRegistry Registry)> OpenAsync(String html = "")
        {
            var host = new Host();
            var context = BrowsingContext.New(Configuration.Default.With<ICustomElementHost>(_ => host));
            var document = await context.OpenAsync(m => m.Content(html));
            return (document, host, document.DefaultView.CustomElements());
        }

        [TestCase("my-element")]
        [TestCase("a-b")]
        [TestCase("x-1")]
        [TestCase("my-élément")]
        [TestCase("a-.b_c")]
        public void ValidNames(String name) => Assert.IsTrue(CustomElementRegistry.IsValidName(name));

        [TestCase("")]
        [TestCase(null)]
        [TestCase("nohyphen")]
        [TestCase("My-Element")]
        [TestCase("my-Element")]
        [TestCase("1-a")]
        [TestCase("-a")]
        [TestCase("a b-c")]
        [TestCase("font-face")]
        [TestCase("annotation-xml")]
        public void InvalidNames(String name) => Assert.IsFalse(CustomElementRegistry.IsValidName(name));

        [Test]
        public async Task DefineWithAnInvalidNameIsASyntaxError()
        {
            var (_, _, registry) = await OpenAsync();

            var ex = Assert.Throws<DomException>(() => registry.Define("nohyphen", "ctor"));

            Assert.AreEqual((Int32)DomError.Syntax, ex.Code);
        }

        [Test]
        public async Task DefiningANameTwiceIsNotSupported()
        {
            var (_, _, registry) = await OpenAsync();
            registry.Define("my-el", "first");

            var ex = Assert.Throws<DomException>(() => registry.Define("my-el", "second"));

            Assert.AreEqual((Int32)DomError.NotSupported, ex.Code);
        }

        [Test]
        public async Task DefiningAConstructorTwiceIsNotSupported()
        {
            var (_, _, registry) = await OpenAsync();
            registry.Define("my-el", "ctor");

            var ex = Assert.Throws<DomException>(() => registry.Define("my-other", "ctor"));

            Assert.AreEqual((Int32)DomError.NotSupported, ex.Code);
        }

        [Test]
        public async Task WhatTheHostRefusesIsNotDefined()
        {
            var (_, _, registry) = await OpenAsync();

            Assert.Throws<InvalidOperationException>(() => registry.Define("my-el", 42));

            Assert.IsNull(registry.Get("my-el"));
        }

        [Test]
        public async Task WithoutAHostNothingCanBeDefined()
        {
            var document = await BrowsingContext.New().OpenNewAsync();

            var ex = Assert.Throws<DomException>(() => document.DefaultView.CustomElements().Define("my-el", "ctor"));

            Assert.AreEqual((Int32)DomError.NotSupported, ex.Code);
        }

        [Test]
        public async Task GetAndGetNameTellTheDefinition()
        {
            var (_, _, registry) = await OpenAsync();
            registry.Define("my-el", "ctor");

            Assert.AreEqual("ctor", registry.Get("my-el"));
            Assert.IsNull(registry.Get("other-el"));
            Assert.AreEqual("my-el", registry.GetName("ctor"));
            Assert.IsNull(registry.GetName("other"));
        }

        [Test]
        public async Task WhenDefinedWaitsForTheDefinition()
        {
            var (_, _, registry) = await OpenAsync();
            var waiting = registry.WhenDefined("my-el");
            Assert.IsFalse(waiting.IsCompleted);

            registry.Define("my-el", "ctor");

            Assert.AreEqual("ctor", await waiting);
            Assert.AreEqual("ctor", await registry.WhenDefined("my-el"));
        }

        [Test]
        public async Task WhenDefinedRefusesInvalidNames()
        {
            var (_, _, registry) = await OpenAsync();

            var task = registry.WhenDefined("nohyphen");

            Assert.IsTrue(task.IsFaulted);
            Assert.IsInstanceOf<DomException>(task.Exception.GetBaseException());
        }

        [Test]
        public async Task DefineUpgradesTheElementsInTheDocumentInTreeOrder()
        {
            var (_, host, registry) = await OpenAsync("<my-el id=a></my-el><div><my-el id=b></my-el></div><other-el id=c></other-el>");

            registry.Define("my-el", "ctor");

            Assert.AreEqual(new[] { "construct a", "connected a", "construct b", "connected b" }, host.Calls);
        }

        [Test]
        public async Task UpgradesOfAParseCountAsOneChangeOfTheVersionWhenItIsDone()
        {
            var host = new Host();
            var versions = new List<Int64>();
            var scripting = new CallbackScriptEngine(options =>
            {
                if (versions.Count == 0)
                {
                    options.Document.DefaultView.CustomElements().Define("my-el", "ctor");
                }

                versions.Add(((Document)options.Document).MutationVersion);
            });
            var config = Configuration.Default.With<ICustomElementHost>(_ => host).WithScripts(scripting);
            var context = BrowsingContext.New(config);
            var html = "<script type='c-sharp'>//</script><my-el></my-el><my-el></my-el><my-el></my-el><script type='c-sharp'>//</script>";

            var document = (Document)await context.OpenAsync(m => m.Content(html));

            Assert.AreEqual(3, host.Calls.Count(m => m.StartsWith("construct")));
            Assert.AreEqual(versions[0], versions[1]);
            Assert.AreEqual(versions[0] + 1, document.MutationVersion);
        }

        [Test]
        public async Task UpgradeAfterTheParseAdvancesTheVersion()
        {
            var (document, _, registry) = await OpenAsync();
            registry.Define("my-el", "ctor");
            var before = ((Document)document).MutationVersion;

            document.CreateElement("my-el");

            Assert.AreNotEqual(before, ((Document)document).MutationVersion);
        }

        [Test]
        public async Task ParsedElementsAreUpgradedWhenItIsDefinedAlready()
        {
            var (document, host, registry) = await OpenAsync();
            registry.Define("my-el", "ctor");

            document.Body.InnerHtml = "<my-el id=a><span></span></my-el>";

            Assert.AreEqual(new[] { "construct a", "connected a" }, host.Calls);
        }

        [Test]
        public async Task CreatedElementIsConstructedRightAwayAndConnectedWhenInserted()
        {
            var (document, host, registry) = await OpenAsync();
            registry.Define("my-el", "ctor");

            var element = document.CreateElement("my-el");
            Assert.AreEqual(new[] { "construct my-el" }, host.Calls);

            document.Body.AppendChild(element);
            Assert.AreEqual(new[] { "construct my-el", "connected my-el" }, host.Calls);
        }

        [Test]
        public async Task ElementInADisconnectedTreeIsNotTold()
        {
            var (document, host, registry) = await OpenAsync();
            registry.Define("my-el", "ctor");
            var parent = document.CreateElement("div");

            parent.InnerHtml = "<my-el id=a></my-el>";

            Assert.IsEmpty(host.Calls);

            document.Body.AppendChild(parent);
            Assert.AreEqual(new[] { "construct a", "connected a" }, host.Calls);
        }

        [Test]
        public async Task RemovedElementIsTold()
        {
            var (document, host, registry) = await OpenAsync("<div id=parent><my-el id=a></my-el></div>");
            registry.Define("my-el", "ctor");
            host.Calls.Clear();

            document.GetElementById("parent").Remove();

            Assert.AreEqual(new[] { "disconnected a" }, host.Calls);
        }

        [Test]
        public async Task MovedElementIsToldBoth()
        {
            var (document, host, registry) = await OpenAsync("<my-el id=a></my-el><div id=other></div>");
            registry.Define("my-el", "ctor");
            host.Calls.Clear();

            document.GetElementById("other").AppendChild(document.GetElementById("a"));

            Assert.AreEqual(new[] { "disconnected a", "connected a" }, host.Calls);
        }

        [Test]
        public async Task ObservedAttributesAreTold()
        {
            var (document, host, registry) = await OpenAsync("<my-el id=a></my-el>");
            host.Observed = new[] { "color" };
            registry.Define("my-el", "ctor");
            var element = document.GetElementById("a");
            host.Calls.Clear();

            element.SetAttribute("color", "red");
            element.SetAttribute("color", "blue");
            element.SetAttribute("size", "big");
            element.RemoveAttribute("color");

            Assert.AreEqual(new[]
            {
                "attribute a color,null,red,null",
                "attribute a color,red,blue,null",
                "attribute a color,blue,null,null",
            }, host.Calls);
        }

        [Test]
        public async Task UpgradeTellsTheAttributesItHasBeforeItIsConnected()
        {
            var (_, host, registry) = await OpenAsync("<my-el id=a color=red size=big></my-el>");
            host.Observed = new[] { "color" };

            registry.Define("my-el", "ctor");

            Assert.AreEqual(new[] { "construct a", "attribute a color,null,red,null", "connected a" }, host.Calls);
        }

        [Test]
        public async Task FailedElementIsNotToldAnythingAnymore()
        {
            var (document, host, registry) = await OpenAsync("<my-el id=a></my-el>");
            host.IsFailing = true;

            registry.Define("my-el", "ctor");
            document.GetElementById("a").Remove();

            Assert.AreEqual(new[] { "construct a" }, host.Calls);
            Assert.IsNull(document.QuerySelector("my-el:defined"));
        }

        [Test]
        public async Task ElementThatExtendsNeedsToBeOfThatElementAndSayIs()
        {
            var (_, host, registry) = await OpenAsync("<button id=a is=my-button></button><button id=b></button><div id=c is=my-button></div>");

            registry.Define("my-button", "ctor", new ElementDefinitionOptions { Extends = "button" });

            Assert.AreEqual(new[] { "construct a", "connected a" }, host.Calls);
        }

        [TestCase("my-other")]
        [TestCase("nothing")]
        public async Task ExtendingWhatCannotBeExtendedIsNotSupported(String extends)
        {
            var (_, _, registry) = await OpenAsync();

            var ex = Assert.Throws<DomException>(() => registry.Define("my-button", "ctor", new ElementDefinitionOptions { Extends = extends }));

            Assert.AreEqual((Int32)DomError.NotSupported, ex.Code);
        }

        [Test]
        public async Task ShadowTreesAreLookedInto()
        {
            var (document, host, registry) = await OpenAsync("<div id=host></div>");
            var shadow = document.GetElementById("host").AttachShadow();
            shadow.InnerHtml = "<my-el id=a></my-el>";

            registry.Define("my-el", "ctor");

            Assert.AreEqual(new[] { "construct a", "connected a" }, host.Calls);
        }

        [Test]
        public async Task ReactionChangingTheTreeDoesNotGetInTheWay()
        {
            var (document, host, registry) = await OpenAsync("<my-el id=a></my-el><my-el id=b></my-el>");
            host.OnCall = (element, callback) =>
            {
                if (callback == "connected" && element.Id == "a")
                {
                    document.GetElementById("b").Remove();
                }
            };

            registry.Define("my-el", "ctor");
            var fragment = document.CreateDocumentFragment();
            fragment.AppendChild(document.CreateElement("my-el"));
            fragment.AppendChild(document.CreateElement("div"));
            document.Body.AppendChild(fragment);

            Assert.AreEqual(3, document.Body.ChildElementCount);
        }

        [Test]
        public async Task ConstructorDoesNotRunAgainWhileRunning()
        {
            var (document, host, registry) = await OpenAsync("<my-el id=a></my-el>");
            var count = 0;
            host.OnConstruct = element =>
            {
                count++;
                document.Body.AppendChild(element);
            };

            registry.Define("my-el", "ctor");

            Assert.AreEqual(1, count);
        }

        [Test]
        public async Task CreateElementForAConstructorDoesNotRunIt()
        {
            var (document, host, registry) = await OpenAsync();
            registry.Define("my-el", "ctor");

            var element = registry.CreateElement(registry.FindDefinition("ctor"));

            Assert.IsEmpty(host.Calls);
            document.Body.AppendChild(element);
            Assert.AreEqual(new[] { "connected my-el" }, host.Calls);
            Assert.IsNotNull(document.QuerySelector("my-el:defined"));
        }

        [Test]
        public async Task DefinedTellsWhatIsNotWaitingForADefinition()
        {
            var (document, _, registry) = await OpenAsync("<div id=d></div><my-el id=a></my-el><span id=s></span>");
            Assert.AreEqual("d,s", String.Join(",", document.QuerySelectorAll("body > :defined").Select(m => m.Id)));

            registry.Define("my-el", "ctor");

            Assert.AreEqual("d,a,s", String.Join(",", document.QuerySelectorAll("body > :defined").Select(m => m.Id)));
        }

        [Test]
        public async Task IsConnectedTellsIfANodeIsInADocument()
        {
            var (document, _, _) = await OpenAsync("<div id=host></div>");
            var created = document.CreateElement("div");
            var shadow = document.GetElementById("host").AttachShadow();
            var inShadow = document.CreateElement("span");
            shadow.AppendChild(inShadow);

            Assert.IsFalse(created.IsConnected());
            Assert.IsTrue(document.IsConnected());
            Assert.IsTrue(document.GetElementById("host").IsConnected());
            Assert.IsTrue(inShadow.IsConnected());

            document.Body.AppendChild(created);
            Assert.IsTrue(created.IsConnected());

            created.Remove();
            Assert.IsFalse(created.IsConnected());
        }

        [Test]
        public async Task ToggleAttributeAddsAndRemoves()
        {
            var (document, _, _) = await OpenAsync();
            var element = document.CreateElement("div");

            Assert.IsTrue(element.ToggleAttribute("a"));
            Assert.AreEqual(String.Empty, element.GetAttribute("a"));
            Assert.IsFalse(element.ToggleAttribute("a"));
            Assert.IsFalse(element.HasAttribute("a"));
            Assert.IsFalse(element.ToggleAttribute("a", false));
            Assert.IsFalse(element.HasAttribute("a"));
            Assert.IsTrue(element.ToggleAttribute("a", true));
            Assert.IsTrue(element.ToggleAttribute("a", true));
            Assert.IsTrue(element.HasAttribute("a"));
            Assert.Throws<DomException>(() => element.ToggleAttribute("1 2"));
        }

        [Test]
        public async Task DisposedDocumentDoesNotTellItsElements()
        {
            var (document, host, registry) = await OpenAsync("<my-el id=a></my-el>");
            registry.Define("my-el", "ctor");
            host.Calls.Clear();

            document.Dispose();

            Assert.IsEmpty(host.Calls);
        }

        [Test]
        public async Task DisposedContextDoesNotTellItsElements()
        {
            var (document, host, registry) = await OpenAsync("<my-el id=a></my-el>");
            registry.Define("my-el", "ctor");
            host.Calls.Clear();

            document.Context.Dispose();

            Assert.IsEmpty(host.Calls);
        }

        [Test]
        public async Task RemovingAnAncestorTellsItsElements()
        {
            var (document, host, registry) = await OpenAsync("<my-el id=a></my-el>");
            registry.Define("my-el", "ctor");
            host.Calls.Clear();

            document.Body.Remove();

            Assert.AreEqual(new[] { "disconnected a" }, host.Calls);
        }

        [Test]
        public async Task ContainsOfNothingIsFalse()
        {
            var (document, _, _) = await OpenAsync();

            Assert.IsFalse(document.Body.Contains(null));
            Assert.IsTrue(document.Body.Contains(document.Body));
        }

        [Test]
        public async Task HeldBackReactionsRunWhenPopped()
        {
            var (document, host, registry) = await OpenAsync();
            registry.Define("my-el", "ctor");
            var element = document.CreateElement("my-el");
            host.Calls.Clear();

            CustomElementRegistry.PushReactions();
            document.Body.AppendChild(element);
            Assert.IsEmpty(host.Calls);
            CustomElementRegistry.PopReactions();

            Assert.AreEqual(new[] { "connected my-el" }, host.Calls);
        }

        [Test]
        public async Task HeldBackUpgradeSeesTheWholeTree()
        {
            var (document, host, registry) = await OpenAsync();
            registry.Define("my-el", "ctor");
            var children = -1;
            host.OnConstruct = _ => children = document.Body.ChildElementCount;

            CustomElementRegistry.PushReactions();
            document.Body.InnerHtml = "<my-el id=a></my-el><div></div><div></div>";
            CustomElementRegistry.PopReactions();

            Assert.AreEqual(3, children);
            Assert.AreEqual(new[] { "construct a", "connected a" }, host.Calls);
        }

        [Test]
        public async Task ReactionsOfAnElementRunTogether()
        {
            var (document, host, registry) = await OpenAsync("<my-el id=a></my-el><my-el id=b></my-el>");
            host.Observed = new[] { "x" };
            registry.Define("my-el", "ctor");
            var a = document.GetElementById("a");
            var b = document.GetElementById("b");
            host.Calls.Clear();

            CustomElementRegistry.PushReactions();
            a.SetAttribute("x", "1");
            b.SetAttribute("x", "1");
            a.SetAttribute("x", "2");
            CustomElementRegistry.PopReactions();

            Assert.AreEqual(new[] { "attribute a x,null,1,null", "attribute a x,1,2,null", "attribute b x,null,1,null" }, host.Calls);
        }

        [Test]
        public async Task HeldBackScopesAreNested()
        {
            var (document, host, registry) = await OpenAsync();
            registry.Define("my-el", "ctor");
            var first = document.CreateElement("my-el");
            first.Id = "first";
            var second = document.CreateElement("my-el");
            second.Id = "second";
            host.Calls.Clear();

            CustomElementRegistry.PushReactions();
            document.Body.AppendChild(first);
            CustomElementRegistry.PushReactions();
            document.Body.AppendChild(second);
            CustomElementRegistry.PopReactions();
            Assert.AreEqual(new[] { "connected second" }, host.Calls);
            CustomElementRegistry.PopReactions();

            Assert.AreEqual(new[] { "connected second", "connected first" }, host.Calls);
        }

        [Test]
        public async Task CreatedElementIsConstructedEvenIfReactionsAreHeldBack()
        {
            var (document, host, registry) = await OpenAsync();
            registry.Define("my-el", "ctor");

            CustomElementRegistry.PushReactions();
            document.CreateElement("my-el");
            Assert.AreEqual(new[] { "construct my-el" }, host.Calls);
            CustomElementRegistry.PopReactions();
        }

        [Test]
        public async Task ElementCreatedAsAnotherIsConstructedAndDefined()
        {
            var (document, host, registry) = await OpenAsync();
            registry.Define("my-button", "ctor", new ElementDefinitionOptions { Extends = "button" });

            var element = document.CreateElement("button", new ElementCreationOptions { Is = "my-button" });

            Assert.AreEqual(new[] { "construct button" }, host.Calls);
            Assert.IsNull(element.GetAttribute("is"));
            Assert.AreEqual("<button is=\"my-button\"></button>", element.OuterHtml);
            document.Body.AppendChild(element);
            Assert.IsNotNull(document.QuerySelector("button:defined"));
        }

        [Test]
        public async Task ElementCreatedAsNothingKnownWaitsForItsDefinition()
        {
            var (document, host, registry) = await OpenAsync();
            var element = document.CreateElement("button", new ElementCreationOptions { Is = "my-button" });
            document.Body.AppendChild(element);
            Assert.IsEmpty(document.QuerySelectorAll("button:defined"));

            registry.Define("my-button", "ctor", new ElementDefinitionOptions { Extends = "button" });

            Assert.AreEqual(new[] { "construct button", "connected button" }, host.Calls);
        }

        [Test]
        public async Task ClonedElementIsConstructed()
        {
            var (document, host, registry) = await OpenAsync("<my-el id=a><my-el id=b></my-el></my-el>");
            registry.Define("my-el", "ctor");
            host.Calls.Clear();

            var clone = document.GetElementById("a").Clone(true);

            Assert.AreEqual(2, host.Calls.Count(m => m.StartsWith("construct")));
            Assert.IsFalse(host.Calls.Any(m => m.StartsWith("connected")));
            Assert.IsNotNull(clone);
        }

        [Test]
        public async Task CloneKeepsWhatItWasCreatedAs()
        {
            var (document, _, registry) = await OpenAsync();
            registry.Define("my-button", "ctor", new ElementDefinitionOptions { Extends = "button" });

            var clone = (IElement)document.CreateElement("button", new ElementCreationOptions { Is = "my-button" }).Clone();

            Assert.AreEqual("<button is=\"my-button\"></button>", clone.OuterHtml);
        }

        [Test]
        public async Task DocumentWithoutCustomElementsHasNoRegistryForItsElements()
        {
            var document = (Document)await BrowsingContext.New().OpenAsync(m => m.Content("<my-el id=a></my-el><div is=x-y></div>"));
            var element = document.GetElementById("a");

            element.SetAttribute("title", "x");
            var clone = element.Clone();
            document.Body.AppendChild(clone);
            Assert.IsNotNull(document.Body.InnerHtml);
            Assert.AreEqual(0, document.QuerySelectorAll(":defined").OfType<IElement>().Count(m => m.LocalName == "my-el"));

            Assert.IsNull(document.CustomElements);
        }

        [Test]
        public async Task ElementCreatedWithIsKeepsItThroughCloningAndAdoption()
        {
            var (document, _, _) = await OpenAsync();
            var other = (Document)await BrowsingContext.New().OpenAsync(m => m.Content(""));
            var element = document.CreateElement("div", new ElementCreationOptions { Is = "my-el" });

            Assert.IsTrue(element.Clone().ToHtml().Contains("is=\"my-el\""));

            other.Body.AppendChild(other.Adopt(element));

            Assert.IsTrue(other.Body.InnerHtml.Contains("is=\"my-el\""));
            Assert.IsFalse(document.Body.InnerHtml.Contains("my-el"));
        }

        [Test]
        public async Task StateOfAnAdoptedElementGoesWithIt()
        {
            var (document, _, registry) = await OpenAsync();
            registry.Define("my-el", "ctor");
            var element = document.CreateElement("my-el");
            var other = (Document)await BrowsingContext.New().OpenAsync(m => m.Content(""));

            Assert.IsTrue(element.Matches(":defined"));

            other.Body.AppendChild(other.Adopt(element));

            Assert.IsTrue(element.Matches(":defined"));
        }

        [Test]
        public async Task AdoptedElementIsToldAndKeepsBeingTold()
        {
            var (document, host, registry) = await OpenAsync("<my-el id=a></my-el>");
            host.Observed = new[] { "x" };
            registry.Define("my-el", "ctor");
            var element = document.GetElementById("a");
            var other = await BrowsingContext.New().OpenAsync(m => m.Content("<div></div>"));
            host.Calls.Clear();

            other.Body.AppendChild(element);

            Assert.AreEqual(new[] { "disconnected a", "adopted a document,document", "connected a" }, host.Calls);
            host.Calls.Clear();

            element.SetAttribute("x", "1");
            element.Remove();

            Assert.AreEqual(new[] { "attribute a x,null,1,null", "disconnected a" }, host.Calls);
        }

        [Test]
        public async Task PagesWithoutDefinitionsAreNotAffected()
        {
            var (document, host, _) = await OpenAsync("<my-el id=a></my-el>");

            document.Body.AppendChild(document.CreateElement("my-el"));
            document.GetElementById("a").SetAttribute("x", "y");
            document.GetElementById("a").Remove();

            Assert.IsEmpty(host.Calls);
        }
    }
}
