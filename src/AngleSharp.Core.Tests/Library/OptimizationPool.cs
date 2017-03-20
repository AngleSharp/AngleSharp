namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Text;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;

    [TestFixture]
    public class OptimizationPoolTests
    {
        private Int32 _defaultCount;
        private Int32 _defaultLimit;

        [SetUp]
        public void InitStringBuilders()
        {
            _defaultCount = StringBuilderPool.MaxCount;
            _defaultLimit = StringBuilderPool.SizeLimit;
        }

        [TearDown]
        public void ClearStringBuilders()
        {
            var builderField = typeof(StringBuilderPool).GetField("_builder", BindingFlags.Static | BindingFlags.NonPublic);
            var builder = builderField.GetValue(null) as Stack<StringBuilder>;
            builder.Clear();

            StringBuilderPool.MaxCount = _defaultCount;
            StringBuilderPool.SizeLimit = _defaultLimit;
        }

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

        [Test]
        public void DiscardLargeStringBuilderInstances()
        {
            var newLimit = _defaultLimit / 2;
            var sbO = StringBuilderPool.Obtain();
            var defaultCapacity = sbO.Capacity;
            sbO.Capacity = newLimit;
            sbO.ToPool();
            var sbR = StringBuilderPool.Obtain();
            Assert.AreEqual(newLimit, sbR.Capacity);
            sbR.Capacity = _defaultLimit * 2;
            sbR.ToPool();
            var sbF = StringBuilderPool.Obtain();
            Assert.AreEqual(defaultCapacity, sbF.Capacity);
        }

        [Test]
        public void StringBuilderPoolMaxInstancesIsRespected()
        {
            StringBuilderPool.MaxCount = 2;
            var sb1 = StringBuilderPool.Obtain();
            var sb2 = StringBuilderPool.Obtain();
            var sb3 = StringBuilderPool.Obtain();
            sb1.ToPool();
            sb2.ToPool();
            sb3.ToPool();
            Assert.AreEqual(sb2, StringBuilderPool.Obtain());
            Assert.AreEqual(sb1, StringBuilderPool.Obtain());
            Assert.AreNotEqual(sb3, StringBuilderPool.Obtain());
        }

        [Test]
        public void StringBuilderDropsOneWithLeastCapacity()
        {
            StringBuilderPool.MaxCount = 4;
            var sb1 = StringBuilderPool.Obtain();
            var sb2 = StringBuilderPool.Obtain();
            var sb3 = StringBuilderPool.Obtain();
            var sb4 = StringBuilderPool.Obtain();
            var sb5 = StringBuilderPool.Obtain();
            sb1.Capacity = 1024;
            sb1.ToPool();
            sb2.Capacity = 512;
            sb2.ToPool();
            sb3.Capacity = 2048;
            sb3.ToPool();
            sb4.Capacity = 8192;
            sb4.ToPool();
            sb5.Capacity = 16384;
            sb5.ToPool();
            Assert.AreEqual(16384, StringBuilderPool.Obtain().Capacity);
            Assert.AreEqual(8192, StringBuilderPool.Obtain().Capacity);
            Assert.AreEqual(2048, StringBuilderPool.Obtain().Capacity);
            Assert.AreEqual(1024, StringBuilderPool.Obtain().Capacity);
        }

        [Test]
        public void StringBuilderDoesNotAddSmallerInstances()
        {
            StringBuilderPool.MaxCount = 4;
            var sb1 = StringBuilderPool.Obtain();
            var sb2 = StringBuilderPool.Obtain();
            var sb3 = StringBuilderPool.Obtain();
            var sb4 = StringBuilderPool.Obtain();
            sb1.Capacity = 1024;
            sb1.ToPool();
            sb2.Capacity = 2048;
            sb2.ToPool();
            sb3.Capacity = 8192;
            sb3.ToPool();
            sb4.Capacity = 4096;
            sb4.ToPool();
            Assert.AreEqual(8192, StringBuilderPool.Obtain().Capacity);
            Assert.AreEqual(2048, StringBuilderPool.Obtain().Capacity);
            Assert.AreEqual(1024, StringBuilderPool.Obtain().Capacity);
        }

        [Test]
        public void StringBuilderPreservesOrderWhenRebuilding()
        {
            StringBuilderPool.MaxCount = 4;
            var sb0 = StringBuilderPool.Obtain();
            var sb1 = StringBuilderPool.Obtain();
            var sb2 = StringBuilderPool.Obtain();
            var sb3 = StringBuilderPool.Obtain();
            var sb4 = StringBuilderPool.Obtain();
            sb0.Capacity = 512;
            sb0.ToPool();
            sb1.Capacity = 1024;
            sb1.ToPool();
            sb2.Capacity = 2048;
            sb2.ToPool();
            sb3.Capacity = 4096;
            sb3.ToPool();
            sb4.Capacity = 8192;
            sb4.ToPool();
            Assert.AreEqual(8192, StringBuilderPool.Obtain().Capacity);
            Assert.AreEqual(4096, StringBuilderPool.Obtain().Capacity);
            Assert.AreEqual(2048, StringBuilderPool.Obtain().Capacity);
            Assert.AreEqual(1024, StringBuilderPool.Obtain().Capacity);
        }

        [Test]
        public void StringBuilderDoesNotRebuildIfLeastOneIsAdded()
        {
            StringBuilderPool.MaxCount = 4;
            var sb1 = StringBuilderPool.Obtain();
            var sb2 = StringBuilderPool.Obtain();
            var sb3 = StringBuilderPool.Obtain();
            var sb4 = StringBuilderPool.Obtain();
            var sb5 = StringBuilderPool.Obtain();
            sb1.Capacity = 1024;
            sb1.ToPool();
            sb2.Capacity = 2048;
            sb2.ToPool();
            sb3.Capacity = 4096;
            sb3.ToPool();
            sb4.Capacity = 8192;
            sb4.ToPool();
            sb5.Capacity = 512;
            sb5.ToPool();
            Assert.AreEqual(8192, StringBuilderPool.Obtain().Capacity);
            Assert.AreEqual(4096, StringBuilderPool.Obtain().Capacity);
            Assert.AreEqual(2048, StringBuilderPool.Obtain().Capacity);
            Assert.AreEqual(1024, StringBuilderPool.Obtain().Capacity);
        }
    }
}
