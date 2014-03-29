using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngleSharp;

namespace UnitTests.Library
{
    [TestClass]
    public class OptimizationPoolTests
    {
        [TestMethod]
        public void RecycleStringBuilderReused()
        {
            var str = "Test";
            var sb1 = Pool.NewStringBuilder();
            sb1.Append(str);
            Assert.AreEqual(str, sb1.ToString());
            var sb2 = Pool.NewStringBuilder();
            Assert.AreEqual(String.Empty, sb2.ToString());
            Assert.ReferenceEquals(sb1, sb2);
        }

        [TestMethod]
        public void RecycleStringBuilderGetString()
        {
            var str = "Test";
            var sb1 = Pool.NewStringBuilder();
            sb1.Append(str);
            Assert.AreEqual(str, sb1.ToPool());
            var sb2 = Pool.NewStringBuilder();
            Assert.AreEqual(String.Empty, sb2.ToPool());
        }
    }
}
