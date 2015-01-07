using AngleSharp;
using AngleSharp.DOM;
using NUnit.Framework;
using System.Linq;

namespace UnitTests.Library
{
    [TestFixture]
    public class MutationObserverTests
    {
        [Test]
        public void ConnectMutationObserverTriggerManually()
        {
            var called = false;

            var observer = new MutationObserver((mut, obs) =>
            {
                called = mut.Length == 1 && mut[0].Added != null && mut[0].Added.Length == 1;
            });

            var document = DocumentBuilder.Html("");

            observer.Connect(document.Body, new MutationObserverInit
            {
                ObserveTargetChildNodes = true
            });

            document.Body.AppendChild(document.CreateElement("span"));
            observer.TriggerWith(observer.Flush().ToArray());
            Assert.IsTrue(called);
        }
    }
}
