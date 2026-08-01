namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Geometry;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class DomGeometryTests
    {
        [Test]
        public void DomRectReadOnlyComputesEdgesForNegativeSize()
        {
            var rect = new DomRectReadOnly(10.0, 20.0, -3.0, -4.0);

            Assert.AreEqual(7.0, rect.Left);
            Assert.AreEqual(10.0, rect.Right);
            Assert.AreEqual(16.0, rect.Top);
            Assert.AreEqual(20.0, rect.Bottom);
        }

        [Test]
        public void DomQuadBoundsReflectCornerPoints()
        {
            var quad = new DomQuad(
                new DomPointInit { X = 4.0, Y = 8.0 },
                new DomPointInit { X = 14.0, Y = 6.0 },
                new DomPointInit { X = 13.0, Y = 22.0 },
                new DomPointInit { X = 2.0, Y = 21.0 });

            var bounds = quad.GetBounds();

            Assert.AreEqual(2.0, bounds.X);
            Assert.AreEqual(6.0, bounds.Y);
            Assert.AreEqual(12.0, bounds.Width);
            Assert.AreEqual(16.0, bounds.Height);
        }

        [Test]
        public void DomMatrixTransformPointWith2dMatrixWorks()
        {
            var matrix = new DomMatrix(new[] { 2.0, 0.0, 0.0, 2.0, 10.0, 10.0 });
            var point = matrix.TransformPoint(new DomPointInit { X = 5.0, Y = 4.0 });

            Assert.AreEqual(20.0, point.X);
            Assert.AreEqual(18.0, point.Y);
            Assert.AreEqual(0.0, point.Z);
            Assert.AreEqual(1.0, point.W);
        }

        [Test]
        public void DomMatrixSetMatrixValueSupportsMatrixAndMatrix3d()
        {
            var matrix2d = new DomMatrix();
            matrix2d.SetMatrixValue("matrix(1, 0, 0, 1, 5, 7)");

            Assert.IsTrue(matrix2d.Is2D);
            Assert.AreEqual(5.0, matrix2d.M41);
            Assert.AreEqual(7.0, matrix2d.M42);

            var matrix3d = new DomMatrix();
            matrix3d.SetMatrixValue("matrix3d(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 3, 4, 5, 1)");

            Assert.IsFalse(matrix3d.Is2D);
            Assert.AreEqual(3.0, matrix3d.M41);
            Assert.AreEqual(4.0, matrix3d.M42);
            Assert.AreEqual(5.0, matrix3d.M43);
        }

        [Test]
        public void DomMatrixStringifierRejectsNonFiniteValues()
        {
            var matrix = new DomMatrix(new[]
            {
                1.0, 0.0, 0.0, 0.0,
                0.0, 1.0, 0.0, 0.0,
                0.0, 0.0, 1.0, 0.0,
                0.0, 0.0, 0.0, Double.NaN,
            });

            Assert.Throws<DomException>(() => _ = matrix.ToString());
        }
    }
}