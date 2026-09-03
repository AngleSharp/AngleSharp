namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Attributes;
    using AngleSharp.Dom;
    using NUnit.Framework;
    using System;
    using System.Reflection;

    [TestFixture]
    public class DomAnnotationTests
    {
        [Test]
        public void AdjacentPositionIsAnEnumerationOfLiterals()
        {
            var attribute = typeof(AdjacentPosition).GetCustomAttribute<DomLiteralsAttribute>();
            Assert.IsNotNull(attribute);
        }

        [TestCase(AdjacentPosition.BeforeBegin, "beforebegin")]
        [TestCase(AdjacentPosition.AfterBegin, "afterbegin")]
        [TestCase(AdjacentPosition.BeforeEnd, "beforeend")]
        [TestCase(AdjacentPosition.AfterEnd, "afterend")]
        public void AdjacentPositionCarriesTheLiteralOfTheSpecification(AdjacentPosition position, String literal)
        {
            var field = typeof(AdjacentPosition).GetField(position.ToString());
            var attribute = field.GetCustomAttribute<DomNameAttribute>();
            Assert.IsNotNull(attribute);
            Assert.AreEqual(literal, attribute.OfficialName);
        }
    }
}
