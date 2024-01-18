namespace AngleSharp.Core.Tests.Library;

using System;
using System.Linq;
using Common;
using NUnit.Framework;

[TestFixture]
public class StringOrMemoryTests
{
    [Test]
    [TestCase(-1)]
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(4)]
    [TestCase(5)]
    [TestCase(6)]
    [TestCase(7)]
    public void SameStringsSameHash(Int32 length)
    {
        var chars = Guid.NewGuid().ToString().ToCharArray();
        if (length >= 0)
        {
            chars = chars.Take(length).ToArray();
        }

        var strA = new StringOrMemory(new String(chars));
        var strB = new StringOrMemory(new String(chars));

        Assert.AreEqual(strA.GetHashCode(), strB.GetHashCode(), "Input: " + new String(chars));
    }

    [Test]
    [TestCase(-1)]
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(4)]
    [TestCase(5)]
    [TestCase(6)]
    [TestCase(7)]
    public void SameStringsValuesEquality(Int32 length)
    {
        var chars = Guid.NewGuid().ToString().ToCharArray();
        if (length >= 0)
        {
            chars = chars.Take(length).ToArray();
        }
        var strA = new StringOrMemory(new String(chars));
        var strB = new StringOrMemory(new String(chars));

        Assert.IsTrue(strA.Equals(strB), "Input: " + new String(chars));
    }
}