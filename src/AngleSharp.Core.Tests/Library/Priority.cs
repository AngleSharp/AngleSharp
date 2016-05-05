namespace AngleSharp.Core.Tests.Library
{
    using System;
    using AngleSharp.Css;
    using NUnit.Framework;

    [TestFixture]
    public class PriorityTests
    {
        [Test]
        public void PriorityIdHigherThanClassAndTag()
        {
            var a = Priority.OneId;
            var b = Priority.OneClass;
            var c = Priority.OneTag;
            var d = new Priority(UInt32.MaxValue);
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

        [Test]
        public void PriorityInlineHigherThanIdAndClassAndTag()
        {
            var a = Priority.Inline;
            var b = Priority.OneId;
            var c = Priority.OneClass;
            var d = Priority.OneTag;
            var e = new Priority(UInt32.MaxValue);

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

        [Test]
        public void PriorityCustomHigherAll()
        {
            var a = new Priority(UInt32.MaxValue);
            var b = Priority.OneId;
            var c = Priority.OneClass;
            var d = Priority.OneTag;
            var e = Priority.Inline;

            Assert.IsTrue(a > b);
            Assert.IsTrue(a > c);
            Assert.IsTrue(a > d);
            Assert.IsTrue(a > e);

            Assert.IsTrue(a >= b);
            Assert.IsTrue(a >= c);
            Assert.IsTrue(a >= d);
            Assert.IsTrue(a >= e);

            Assert.IsTrue(a == new Priority(UInt32.MaxValue));
        }

        [Test]
        public void PriorityImportantHigherAllExcludedCustom()
        {
            var a = new Priority(UInt32.MaxValue - 1);
            var b = Priority.OneId;
            var c = Priority.OneClass;
            var d = Priority.OneTag;
            var e = Priority.Inline;
            var f = new Priority(UInt32.MaxValue);

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

            Assert.IsTrue(a == new Priority(UInt32.MaxValue - 1));
        }

        [Test]
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

        [Test]
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

        [Test]
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
