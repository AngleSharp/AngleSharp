namespace AngleSharp.Core.Tests.Text
{
    using AngleSharp.Text;
    using NUnit.Framework;
    using System;
    using System.Text;

    [TestFixture]
    public class ContiguousTextSourceTests
    {
        [Test]
        public void CharArrayWindowStartsAtCurrentIndex()
        {
            AssertWindow(new CharArrayTextSource("abc".ToCharArray(), 3));
        }

        [Test]
        public void MemoryWindowStartsAtCurrentIndex()
        {
            AssertWindow(new ReadOnlyMemoryTextSource("abc".AsMemory()));
        }

        [Test]
        public void StringWindowStartsAtCurrentIndex()
        {
            AssertWindow(new StringTextSource("abc"));
        }

#if NET8_0_OR_GREATER
        [Test]
        public void ByteWindowStartsAtCurrentIndex()
        {
            AssertWindow(new ReadOnlyByteTextSource("abc"u8.ToArray(), Encoding.UTF8));
        }
#endif

        private static void AssertWindow(IContiguousTextSource source)
        {
            using (source)
            {
                source.Index = 1;

                Assert.IsTrue(source.TryGetRemainingSpan(out var remaining));
                Assert.AreEqual("bc", remaining.ToString());

                source.Index += 1;
                Assert.AreEqual('c', source.ReadCharacter());
                Assert.IsFalse(source.TryGetRemainingSpan(out remaining));
                Assert.IsTrue(remaining.IsEmpty);
            }
        }
    }
}
