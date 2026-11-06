namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Events;
    using AngleSharp.Dom.Geometry;
    using AngleSharp.Html.Dom;
    using AngleSharp.Html.Dom.Events;
    using AngleSharp.Html.Parser;
    using AngleSharp.Io;
    using NUnit.Framework;
    using System;
    using System.Linq;

    [TestFixture]
    public class DomMembersTests
    {
        private static IDocument Html(String code) => new HtmlParser().ParseDocument(code);

        [Test]
        public void KeyboardEventTellsItsModifiersFromAList()
        {
            var ev = new KeyboardEvent("keydown", true, true, null, 0, "a", KeyboardLocation.Standard, "Control Shift", false);

            Assert.IsTrue(ev.IsCtrlPressed);
            Assert.IsTrue(ev.IsShiftPressed);
            Assert.IsFalse(ev.IsAltPressed);
            Assert.IsFalse(ev.IsMetaPressed);
            Assert.IsTrue(ev.GetModifierState("Control"));
            Assert.IsFalse(ev.GetModifierState("Trol"));
        }

        [Test]
        public void KeyboardEventTakesTheMembersOfItsInit()
        {
            var ev = new KeyboardEvent("keydown", "KeyA", key: "a", ctrlKey: true, metaKey: true, repeat: true, isComposing: true);

            Assert.AreEqual("KeyA", ev.Code);
            Assert.AreEqual("a", ev.Key);
            Assert.IsTrue(ev.IsCtrlPressed);
            Assert.IsTrue(ev.IsMetaPressed);
            Assert.IsFalse(ev.IsAltPressed);
            Assert.IsTrue(ev.IsRepeated);
            Assert.IsTrue(ev.IsComposing);
        }

        [Test]
        public void KeyboardEventWithoutAnythingHasNothingPressed()
        {
            var ev = new KeyboardEvent("keydown");

            Assert.AreEqual(String.Empty, ev.Code);
            Assert.IsFalse(ev.IsCtrlPressed);
        }

        [TestCase("a", "KeyA")]
        [TestCase("Z", "KeyZ")]
        [TestCase("7", "Digit7")]
        [TestCase(" ", "Space")]
        [TestCase("Enter", "Enter")]
        [TestCase("ArrowUp", "ArrowUp")]
        [TestCase("ß", "")]
        public void KeyboardEventOfAHostTellsTheCodeOfItsKey(String key, String expected)
        {
            var ev = new KeyboardEvent("keydown", true, true, null, 0, key, KeyboardLocation.Standard, String.Empty, false);

            Assert.AreEqual(expected, ev.Code);
        }

        [Test]
        public void KeyboardEventFromInitKeepsTheCodeItWasGiven()
        {
            Assert.AreEqual(String.Empty, new KeyboardEvent("keydown", null, key: "a").Code);
            Assert.AreEqual("KeyB", new KeyboardEvent("keydown", "KeyB", key: "a").Code);
        }

        [Test]
        public void MouseEventTakesItsButtonsAndHasAnXAndY()
        {
            var ev = new MouseEvent("click", MouseButtons.Secondary, clientX: 3, clientY: 4, ctrlKey: true);

            Assert.AreEqual(MouseButtons.Secondary, ev.Buttons);
            Assert.AreEqual(3, ev.X);
            Assert.AreEqual(4, ev.Y);
            Assert.IsTrue(ev.IsCtrlPressed);
        }

        [Test]
        public void MouseEventsTakeMovementDeltas()
        {
            var ev = new MouseEvent("mousemove", MouseButtons.Primary, movementX: 2, movementY: -3);
            var pointer = new PointerEvent("pointermove", movementX: 5, movementY: 6);
            var wheel = new WheelEvent("wheel", movementX: 7, movementY: 8);

            Assert.AreEqual(2, ev.MovementX);
            Assert.AreEqual(-3, ev.MovementY);
            Assert.AreEqual(5, pointer.MovementX);
            Assert.AreEqual(6, pointer.MovementY);
            Assert.AreEqual(7, wheel.MovementX);
            Assert.AreEqual(8, wheel.MovementY);
            Assert.AreEqual(0, new MouseEvent("click").MovementX);
            Assert.AreEqual(0, new MouseEvent("click").MovementY);
        }

        [Test]
        public void SubmitEventKeepsItsSubmitter()
        {
            var document = Html("<form><button type='submit'>Save</button></form>");
            var button = document.QuerySelector<IHtmlElement>("button");
            var ev = new SubmitEvent("submit", true, true, button);

            Assert.AreSame(button, ev.Submitter);
            Assert.IsTrue(ev.IsBubbling);
            Assert.IsTrue(ev.IsCancelable);
            Assert.Throws<ArgumentException>(() => new SubmitEvent("submit", submitter: document.QuerySelector<IHtmlElement>("form")));
        }

        [Test]
        public void WheelEventTakesItsModifiersAndRelatedTarget()
        {
            var target = Html("").Body;
            var ev = new WheelEvent("wheel", MouseButtons.Primary, ctrlKey: true, shiftKey: true, relatedTarget: target, deltaY: 5);

            Assert.IsTrue(ev.IsCtrlPressed);
            Assert.IsTrue(ev.IsShiftPressed);
            Assert.IsFalse(ev.IsAltPressed);
            Assert.AreSame(target, ev.Target);
            Assert.AreEqual(5, ev.DeltaY);
            Assert.AreEqual(MouseButtons.Primary, ev.Buttons);
        }

        [Test]
        public void PointerEventIsAMouseEventWithMore()
        {
            var ev = new PointerEvent("pointerdown", true, clientX: 2, pointerId: 7, pointerType: "pen", pressure: 0.5, isPrimary: true);

            Assert.IsInstanceOf<MouseEvent>(ev);
            Assert.AreEqual(2, ev.ClientX);
            Assert.AreEqual(7, ev.PointerId);
            Assert.AreEqual("pen", ev.PointerType);
            Assert.AreEqual(0.5, ev.Pressure);
            Assert.IsTrue(ev.IsPrimary);
            Assert.AreEqual(1.0, ev.Width);
        }

        [Test]
        public void CancelBubbleAndReturnValueTellAndChangeTheEvent()
        {
            var ev = new Event("x", true, true);

            Assert.IsFalse(ev.CancelBubble);
            Assert.IsTrue(ev.ReturnValue);

            ev.CancelBubble = true;
            ev.ReturnValue = false;

            Assert.IsTrue(ev.CancelBubble);
            Assert.IsFalse(ev.ReturnValue);
            Assert.IsTrue(ev.IsDefaultPrevented);
        }

        [Test]
        public void RectAndPointSerializeTheirMembers()
        {
            var rect = new DomRect(1, 2, 3, 4).ToJson();
            var point = new DomPoint(1, 2).ToJson();

            Assert.AreEqual(1.0, rect["x"]);
            Assert.AreEqual(4.0, rect["right"]);
            Assert.AreEqual(6.0, rect["bottom"]);
            Assert.AreEqual(2.0, point["y"]);
            Assert.AreEqual(1.0, point["w"]);
        }

        [Test]
        public void AttributesAreListed()
        {
            var element = Html("<div a=1 data-b=2></div><p></p>").QuerySelector("div");

            Assert.AreEqual(new[] { "a", "data-b" }, element.GetAttributeNames());
            Assert.IsTrue(element.HasAttributes());
            Assert.IsFalse(element.Owner.QuerySelector("p").HasAttributes());
        }

        [TestCase("beforebegin", "<i></i><div id=\"d\"><b></b></div>")]
        [TestCase("afterbegin", "<div id=\"d\"><i></i><b></b></div>")]
        [TestCase("beforeend", "<div id=\"d\"><b></b><i></i></div>")]
        [TestCase("afterend", "<div id=\"d\"><b></b></div><i></i>")]
        public void ElementsAreInsertedAdjacent(String where, String expected)
        {
            var document = Html("<div id=d><b></b></div>");
            var inserted = document.CreateElement("i");

            Assert.AreSame(inserted, document.GetElementById("d").InsertAdjacentElement(where, inserted));
            Assert.AreEqual(expected, document.Body.InnerHtml);
        }

        [Test]
        public void TextIsInsertedAdjacentAsText()
        {
            var document = Html("<div id=d></div>");

            document.GetElementById("d").InsertAdjacentText("beforeend", "<b>");

            Assert.AreEqual("&lt;b&gt;", document.GetElementById("d").InnerHtml);
        }

        [Test]
        public void InsertingAdjacentWithoutAParentGivesNothingAndABadPositionThrows()
        {
            var document = Html("");
            var detached = document.CreateElement("div");

            Assert.IsNull(detached.InsertAdjacentElement("afterend", document.CreateElement("i")));
            Assert.Throws<DomException>(() => detached.InsertAdjacentElement("nowhere", document.CreateElement("i")));
        }

        [Test]
        public void InertReflectsItsAttribute()
        {
            var element = Html("<div></div>").QuerySelector("div");
            Assert.IsFalse(element.GetInert());

            element.SetInert(true);
            Assert.IsTrue(element.HasAttribute("inert"));
            Assert.IsTrue(element.GetInert());

            element.SetInert(false);
            Assert.IsFalse(element.HasAttribute("inert"));
        }

        [Test]
        public void TokensAreReplacedInPlace()
        {
            var element = Html("<div class='a b c'></div>").QuerySelector("div");

            Assert.IsTrue(element.ClassList.ReplaceToken("b", "x"));
            Assert.AreEqual("a x c", element.ClassName);
            Assert.IsFalse(element.ClassList.ReplaceToken("nope", "y"));
            Assert.AreEqual("a x c", element.ClassName);
        }

        [Test]
        public void ReplacingWithATokenThatIsThereKeepsOneOfThem()
        {
            var element = Html("<div class='a b c'></div>").QuerySelector("div");

            element.ClassList.ReplaceToken("a", "c");

            Assert.AreEqual("c b", element.ClassName);
        }

        [Test]
        public void TokenValueIsTheTextOfTheTokens()
        {
            var element = Html("<div class='a  b'></div>").QuerySelector("div");
            Assert.AreEqual("a b", element.ClassList.GetValue());

            element.ClassList.SetValue(" x  y ");

            Assert.AreEqual(new[] { "x", "y" }, element.ClassList.ToArray());
        }

        [Test]
        public void DocumentTellsItsState()
        {
            var document = Html("");

            Assert.AreSame(document.DocumentElement, document.GetScrollingElement());
            Assert.IsFalse(document.GetHidden());
            Assert.AreEqual("visible", document.GetVisibilityState());
            Assert.AreEqual(document.CharacterSet, document.GetCharset());
            Assert.IsTrue(document.IsSameNode(document));
            Assert.IsFalse(document.IsSameNode(document.Body));
        }

        [Test]
        public void SelectGivesItsOptionsByPosition()
        {
            var select = (IHtmlSelectElement)Html("<select><option>a<option>b</select>").QuerySelector("select");

            Assert.AreEqual("b", select.Item(1).Text);
            Assert.IsNull(select.Item(5));
        }

        [Test]
        public void ImageReflectsItsLoadingAttributes()
        {
            var image = (IHtmlImageElement)Html("<img>").QuerySelector("img");

            Assert.AreEqual("eager", image.GetLoading());
            image.SetLoading("lazy");
            Assert.AreEqual("lazy", image.GetAttribute("loading"));
            Assert.IsTrue(image.Decode().IsCompleted);
        }

        [Test]
        public void RequestSubmitFiresAnEventThatCanPreventIt()
        {
            var document = Html("<form><button name='save' value='yes'>Save</button></form>");
            var form = document.QuerySelector<IHtmlFormElement>("form");
            var button = document.QuerySelector<IHtmlElement>("button");
            var fired = 0;
            IHtmlElement submitter = null;
            form.AddEventListener("submit", (_, ev) =>
            {
                fired++;
                submitter = ((SubmitEvent)ev).Submitter;
                ev.Cancel();
            });

            form.RequestSubmit(button);

            Assert.AreEqual(1, fired);
            Assert.AreSame(button, submitter);
        }

        [Test]
        public void RequestSubmitWithoutSubmitterReportsNull()
        {
            var form = Html("<form></form>").QuerySelector<IHtmlFormElement>("form");
            IHtmlElement submitter = null;
            form.AddEventListener("submit", (_, ev) =>
            {
                submitter = ((SubmitEvent)ev).Submitter;
                ev.Cancel();
            });

            form.RequestSubmit();

            Assert.IsNull(submitter);
        }

        [Test]
        public void SubmitAsyncWithSubmitterDispatchesSubmitEvent()
        {
            var document = Html("<form><button>Save</button></form>");
            var form = document.QuerySelector<IHtmlFormElement>("form");
            var button = document.QuerySelector<IHtmlElement>("button");
            var fired = 0;
            form.AddEventListener("submit", (_, ev) =>
            {
                fired++;
                ev.Cancel();
            });

            var result = form.SubmitAsync(button);

            Assert.IsTrue(result.IsCompleted);
            Assert.AreEqual(1, fired);
        }

        [Test]
        public void RequestSubmitOfAnInvalidFormFiresNothing()
        {
            var form = (IHtmlFormElement)Html("<form><input required></form>").QuerySelector("form");
            var fired = 0;
            form.AddEventListener("submit", (_, _) => fired++);

            form.RequestSubmit();

            Assert.AreEqual(0, fired);
        }

        [Test]
        public void RequestSubmitHonorsFormNoValidate()
        {
            var document = Html("<form><input required><button formnovalidate>Save</button></form>");
            var form = document.QuerySelector<IHtmlFormElement>("form");
            var button = document.QuerySelector<IHtmlElement>("button");
            var fired = 0;
            form.AddEventListener("submit", (_, ev) =>
            {
                fired++;
                ev.Cancel();
            });

            form.RequestSubmit(button);

            Assert.AreEqual(1, fired);
        }

        [Test]
        public void RequestSubmitAcceptsAnImageInput()
        {
            var document = Html("<form><input type='image' name='map'></form>");
            var form = document.QuerySelector<IHtmlFormElement>("form");
            var image = document.QuerySelector<IHtmlElement>("input");
            IHtmlElement submitter = null;
            form.AddEventListener("submit", (_, ev) =>
            {
                submitter = ((SubmitEvent)ev).Submitter;
                ev.Cancel();
            });

            form.RequestSubmit(image);

            Assert.AreSame(image, submitter);
        }

        [Test]
        public void RequestSubmitValidatesItsSubmitter()
        {
            var document = Html("<form><div></div><button>Save</button></form><form><button>Other</button></form>");
            var form = document.QuerySelector<IHtmlFormElement>("form");
            var otherButton = document.QuerySelectorAll<IHtmlElement>("button").ElementAt(1);

            Assert.Throws<ArgumentException>(() => form.RequestSubmit(document.QuerySelector<IHtmlElement>("div")));
            var error = Assert.Throws<DomException>(() => form.RequestSubmit(otherButton));
            Assert.AreEqual(DomError.NotFound, (DomError)error.Code);
        }

        [Test]
        public void FormSubmissionUsesSubmitterOverridesAndData()
        {
            var document = Html("<form action='https://example.com/original' method='get'><input name='q' value='term'><button name='save' value='yes' formaction='https://example.com/override' formmethod='post' formenctype='text/plain'>Save</button></form>");
            var form = document.QuerySelector<IHtmlFormElement>("form");
            var button = document.QuerySelector<IHtmlElement>("button");
            var request = form.GetSubmission(button);

            Assert.AreEqual("https://example.com/override", request.Target.Href);
            Assert.AreEqual(HttpMethod.Post, request.Method);
            Assert.AreEqual("text/plain", request.MimeType);
            using (var reader = new System.IO.StreamReader(request.Body))
            {
                var body = reader.ReadToEnd();
                Assert.IsTrue(body.Contains("q=term"));
                Assert.IsTrue(body.Contains("save=yes"));
            }
        }
    }
}
