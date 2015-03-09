using AngleSharp.Dom.Css;
using AngleSharp.Parser.Css;
using NUnit.Framework;

namespace AngleSharp.Core.Tests.Css
{
    [TestFixture]
    public class CssObjectSizingTests
    {
        [Test]
        public void CssObjectFitNoneLegal()
        {
            var snippet = "object-fit : none";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("object-fit", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssObjectFitProperty>(property);
            var concrete = (CssObjectFitProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(property.IsAnimatable);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("none", concrete.Value.CssText);
            //Assert.AreEqual(ObjectFitting.None, concrete.Fitting);
        }

        [Test]
        public void ObjectFitScaledownIllegal()
        {
            var snippet = "object-fit : scaledown";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("object-fit", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssObjectFitProperty>(property);
            var concrete = (CssObjectFitProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsFalse(property.IsAnimatable);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
            //Assert.AreEqual(ObjectFitting.Fill, concrete.Fitting);
        }

        [Test]
        public void ObjectFitScaleDownLegal()
        {
            var snippet = "object-fit : scale-DOWN";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("object-fit", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssObjectFitProperty>(property);
            var concrete = (CssObjectFitProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(property.IsAnimatable);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("scale-down", concrete.Value.CssText);
            //Assert.AreEqual(ObjectFitting.ScaleDown, concrete.Fitting);
        }

        [Test]
        public void CssObjectFitCoverLegal()
        {
            var snippet = "object-fit : cover";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("object-fit", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssObjectFitProperty>(property);
            var concrete = (CssObjectFitProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(property.IsAnimatable);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("cover", concrete.Value.CssText);
            //Assert.AreEqual(ObjectFitting.Cover, concrete.Fitting);
        }

        [Test]
        public void CssObjectFitContainLegal()
        {
            var snippet = "object-fit : contain";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("object-fit", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssObjectFitProperty>(property);
            var concrete = (CssObjectFitProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(property.IsAnimatable);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("contain", concrete.Value.CssText);
            //Assert.AreEqual(ObjectFitting.Contain, concrete.Fitting);
        }

        [Test]
        public void CssObjectPositionCenterLegal()
        {
            var snippet = "object-position : center";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("object-position", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssObjectPositionProperty>(property);
            var concrete = (CssObjectPositionProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsTrue(property.IsAnimatable);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("center", concrete.Value.CssText);
        }

        [Test]
        public void ObjectPositionTopLeftIllegal()
        {
            var snippet = "object-position : top-left";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("object-position", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssObjectPositionProperty>(property);
            var concrete = (CssObjectPositionProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsTrue(property.IsAnimatable);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void ObjectPositionTopLeftLegal()
        {
            var snippet = "object-position : top left";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("object-position", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssObjectPositionProperty>(property);
            var concrete = (CssObjectPositionProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsTrue(property.IsAnimatable);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("top left", concrete.Value.CssText);
        }

        [Test]
        public void CssObjectPosition5050Legal()
        {
            var snippet = "object-position : 50%   50% ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("object-position", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssObjectPositionProperty>(property);
            var concrete = (CssObjectPositionProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsTrue(property.IsAnimatable);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("50% 50%", concrete.Value.CssText);
        }

        [Test]
        public void CssObjectPositionLeft30Legal()
        {
            var snippet = "object-position : left  30px";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("object-position", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssObjectPositionProperty>(property);
            var concrete = (CssObjectPositionProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsTrue(property.IsAnimatable);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("left 30px", concrete.Value.CssText);
        }
    }
}
