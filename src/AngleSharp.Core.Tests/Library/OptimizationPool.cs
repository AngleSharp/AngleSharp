namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Text;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class OptimizationPoolTests
    {
        [Test]
        public void RecycleStringBuilderReused()
        {
            var str = "Test";
            var sb1 = StringBuilderPool.Obtain();
            sb1.Append(str);
            Assert.AreEqual(str, sb1.ToString());
            var sb2 = StringBuilderPool.Obtain();
            Assert.AreEqual(String.Empty, sb2.ToString());
            Assert.AreNotSame(sb1, sb2);
            sb1.ToPool();
            sb2.ToPool();
        }

        [Test]
        public void RecycleStringBuilderGetString()
        {
            var str = "Test";
            var sb1 = StringBuilderPool.Obtain();
            sb1.Append(str);
            Assert.AreEqual(str, sb1.ToPool());
            var sb2 = StringBuilderPool.Obtain();
            Assert.AreEqual(String.Empty, sb2.ToPool());
            Assert.AreSame(sb1, sb2);
        }

        [Test]
        public void RecycleStringBuilderGetStringReturned()
        {
            var str = "Test";
            var sb1 = StringBuilderPool.Obtain();
            sb1.Append(str);
            Assert.AreEqual(str, sb1.ToPool());
            var sb2 = StringBuilderPool.Obtain();
            Assert.AreSame(sb1, sb2);
            sb2.Append(str);
            Assert.AreEqual(str, sb2.ToString());
            var sb3 = StringBuilderPool.Obtain();
            Assert.AreNotEqual(sb1, sb3);
            Assert.AreEqual(String.Empty, sb3.ToPool());
            sb2.ToPool();
        }
    }
}
