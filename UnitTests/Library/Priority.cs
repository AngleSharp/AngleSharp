using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngleSharp;

namespace UnitTests.Library
{
    [TestClass]
    public class PriorityTests
    {
        [TestMethod]
        public void PriorityIdHigherThanClassAndTag()
        {
            var a = Priority.OneId;
            var b = Priority.OneClass;
            var c = Priority.OneTag;
            var d = Priority.Custom;
            var e = Priority.Inline;

            Assert.IsTrue(a > b);
            Assert.IsTrue(a > c);
            Assert.IsTrue(a < d);
            Assert.IsTrue(a < e);

            Assert.IsTrue(a >= b);
            Assert.IsTrue(a >= c);
            Assert.IsTrue(a <= d);
            Assert.IsTrue(a <= e);

            Assert.IsTrue(a == Priority.OneId);
            Assert.AreEqual(Priority.OneId, a);
        }

        [TestMethod]
        public void PriorityInlineHigherThanIdAndClassAndTag()
        {
            var a = Priority.Inline;
            var b = Priority.OneId;
            var c = Priority.OneClass;
            var d = Priority.OneTag;
            var e = Priority.Custom;

            Assert.IsTrue(a > b);
            Assert.IsTrue(a > c);
            Assert.IsTrue(a > d);
            Assert.IsTrue(a < e);

            Assert.IsTrue(a >= b);
            Assert.IsTrue(a >= c);
            Assert.IsTrue(a >= d);
            Assert.IsTrue(a <= e);

            Assert.IsTrue(a == Priority.Inline);
            Assert.AreEqual(Priority.Inline, a);
        }

        [TestMethod]
        public void PriorityCustomHigherAll()
        {
            var a = Priority.Custom;
            var b = Priority.OneId;
            var c = Priority.OneClass;
            var d = Priority.OneTag;
            var e = Priority.Inline;
            var f = Priority.Important;

            Assert.IsTrue(a > b);
            Assert.IsTrue(a > c);
            Assert.IsTrue(a > d);
            Assert.IsTrue(a > e);
            Assert.IsTrue(a > f);

            Assert.IsTrue(a >= b);
            Assert.IsTrue(a >= c);
            Assert.IsTrue(a >= d);
            Assert.IsTrue(a >= e);
            Assert.IsTrue(a >= f);

            Assert.IsTrue(a == Priority.Custom);
            Assert.AreEqual(Priority.Custom, a);
        }

        [TestMethod]
        public void PriorityImportantHigherAllExcludedCustom()
        {
            var a = Priority.Important;
            var b = Priority.OneId;
            var c = Priority.OneClass;
            var d = Priority.OneTag;
            var e = Priority.Inline;
            var f = Priority.Custom;

            Assert.IsTrue(a > b);
            Assert.IsTrue(a > c);
            Assert.IsTrue(a > d);
            Assert.IsTrue(a > e);
            Assert.IsTrue(a < f);

            Assert.IsTrue(a >= b);
            Assert.IsTrue(a >= c);
            Assert.IsTrue(a >= d);
            Assert.IsTrue(a >= e);
            Assert.IsTrue(a <= f);

            Assert.IsTrue(a == Priority.Important);
            Assert.AreEqual(Priority.Important, a);
        }

        [TestMethod]
        public void PriorityAddSeveralWithoutInline()
        {
            var a = Priority.Zero;
            a += Priority.OneClass;
            a += Priority.OneId;
            a += Priority.OneId;
            a += Priority.OneTag;

            Assert.IsTrue(a >= Priority.OneClass);
            Assert.IsTrue(a >= Priority.OneId);
            Assert.IsTrue(a >= Priority.OneTag);
            Assert.IsTrue(a >= Priority.Zero);

            var result = new Priority(0, 2, 1, 1);
            Assert.IsTrue(a == result);
            Assert.AreEqual(result, a);
        }

        [TestMethod]
        public void PriorityAddSeveralWithInline()
        {
            var a = Priority.Inline;
            a += Priority.OneClass;
            a += Priority.OneId;
            a += Priority.OneId;
            a += Priority.OneTag;

            Assert.IsTrue(a > Priority.Inline);
            Assert.IsTrue(a > Priority.OneClass);
            Assert.IsTrue(a > Priority.OneId);
            Assert.IsTrue(a > Priority.OneTag);
            Assert.IsTrue(a > Priority.Zero);

            var result = new Priority(1, 2, 1, 1);
            Assert.IsTrue(a == result);
            Assert.AreEqual(result, a);
        }

        [TestMethod]
        public void PriorityCheckProperties()
        {
            var a = Priority.Inline;
            a += Priority.OneClass;
            a += Priority.OneId;
            a += Priority.OneId;

            byte inlines = 1;
            byte ids = 2;
            byte classes = 1;
            byte tags = 0;

            var result = new Priority(inlines, ids, classes, tags);
            Assert.IsTrue(a == result);

            Assert.AreEqual(inlines, a.Inlines);
            Assert.AreEqual(ids, a.Ids);
            Assert.AreEqual(classes, a.Classes);
            Assert.AreEqual(tags, a.Tags);
            Assert.AreEqual(result, a);
        }
    }
}
