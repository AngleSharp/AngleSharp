using System;
using AngleSharp.Dom;
using NUnit.Framework;

namespace AngleSharp.Core.Tests
{
    static class TestExtensions
    {
        public static String GetTagName(this INode node)
        {
            var element = node as IElement;
            Assert.IsNotNull(element);
            Assert.IsNull(element.Prefix);
            return element.LocalName;
        }
    }
}
