namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Attributes;
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Reflection;

    [TestFixture]
    public class DomSameObjectTests
    {
        private static PropertyInfo[] GetAnnotatedProperties()
        {
            return typeof(IElement).Assembly.GetTypes()
                .SelectMany(m => m.GetProperties())
                .Where(m => m.GetCustomAttribute<DomSameObjectAttribute>() != null)
                .ToArray();
        }

        [Test]
        public void SameObjectMembersAreReadOnlyPropertiesOfAReferenceType()
        {
            var properties = GetAnnotatedProperties();

            Assert.IsNotEmpty(properties);

            foreach (var property in properties)
            {
                var name = property.DeclaringType.Name + "." + property.Name;
                Assert.IsFalse(property.PropertyType.IsValueType, name + " must return a reference type");
                Assert.IsNotNull(property.GetMethod, name + " must be readable");
                Assert.IsNull(property.SetMethod, name + " must be read only");
                Assert.IsNotEmpty(property.GetCustomAttributes<DomNameAttribute>(), name + " must be a DOM member");
            }
        }

        [TestCase(typeof(INode), nameof(INode.ChildNodes))]
        [TestCase(typeof(IElement), nameof(IElement.Attributes))]
        [TestCase(typeof(IElement), nameof(IElement.ClassList))]
        [TestCase(typeof(IParentNode), nameof(IParentNode.Children))]
        [TestCase(typeof(IDocument), nameof(IDocument.Implementation))]
        [TestCase(typeof(IDocument), nameof(IDocument.Forms))]
        [TestCase(typeof(IDocumentStyle), nameof(IDocumentStyle.StyleSheets))]
        [TestCase(typeof(IStyleSheet), nameof(IStyleSheet.Media))]
        [TestCase(typeof(IHtmlElement), nameof(IHtmlElement.Dataset))]
        [TestCase(typeof(IHtmlFormElement), nameof(IHtmlFormElement.Elements))]
        [TestCase(typeof(IHtmlSelectElement), nameof(IHtmlSelectElement.Options))]
        [TestCase(typeof(IHtmlTableElement), nameof(IHtmlTableElement.Rows))]
        [TestCase(typeof(IHtmlTableRowElement), nameof(IHtmlTableRowElement.Cells))]
        [TestCase(typeof(IHtmlAnchorElement), nameof(IHtmlAnchorElement.RelationList))]
        [TestCase(typeof(IHtmlOutputElement), nameof(IHtmlOutputElement.HtmlFor))]
        public void MemberIsMarkedAsSameObject(Type type, String name)
        {
            var property = type.GetProperty(name);

            Assert.IsNotNull(property);
            Assert.IsNotNull(property.GetCustomAttribute<DomSameObjectAttribute>());
        }

        [TestCase(typeof(IHtmlTemplateElement), nameof(IHtmlTemplateElement.Content))]
        [TestCase(typeof(IHtmlInputElement), nameof(IHtmlInputElement.Files))]
        [TestCase(typeof(IValidation), nameof(IValidation.Validity))]
        public void MemberIsNotMarkedAsSameObject(Type type, String name)
        {
            var property = type.GetProperty(name);

            Assert.IsNotNull(property);
            Assert.IsNull(property.GetCustomAttribute<DomSameObjectAttribute>());
        }
    }
}
