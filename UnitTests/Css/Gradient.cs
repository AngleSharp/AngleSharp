namespace UnitTests.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.DOM.Css;
    using AngleSharp.Parser.Css;
    using NUnit.Framework;
    using System.Linq;

    [TestFixture]
    public class GradientTests
    {
        [Test]
        public void InLinearGradient()
        {
            var source = "linear-gradient(135deg, red, blue)";
            var value = CssParser.ParseValue(source);
            Assert.IsInstanceOf<CssFunction>(value);
            var function = (CssFunction)value;
            Assert.AreEqual("linear-gradient", function.Name);
            Assert.AreEqual(3, function.Arguments.Count);
            Assert.IsInstanceOf<Angle>(function.Arguments[0]);
            Assert.IsInstanceOf<CssIdentifier>(function.Arguments[1]);
            Assert.IsInstanceOf<CssIdentifier>(function.Arguments[2]);
        }

        [Test]
        public void InRadialGradient()
        {
            var source = "radial-gradient(ellipse farthest-corner at 45px 45px , #00FFFF, rgba(0, 0, 255, 0) 50%, #0000FF 95%)";
            var value = CssParser.ParseValue(source);
            Assert.IsInstanceOf<CssFunction>(value);
            var function = (CssFunction)value;
            Assert.AreEqual("radial-gradient", function.Name);
            Assert.AreEqual(4, function.Arguments.Count);
            Assert.IsInstanceOf<CssValueList>(function.Arguments[0]);
            Assert.IsInstanceOf<Color>(function.Arguments[1]);
            Assert.IsInstanceOf<CssValueList>(function.Arguments[2]);
            Assert.IsInstanceOf<CssValueList>(function.Arguments[3]);
        }

        [Test]
        public void BackgroundImageLinearGradientWithAngle()
        {
            var source = "background-image: linear-gradient(135deg, red, blue)";
            var property = CssParser.ParseDeclaration(source);
            Assert.IsInstanceOf<CSSBackgroundImageProperty>(property);
            var backgroundImage = property as CSSBackgroundImageProperty;
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInitial);
            Assert.AreEqual(1, backgroundImage.Images.Count());
            var image = backgroundImage.Images.First();
            Assert.IsInstanceOf<LinearGradient>(image);
            var gradient = image as LinearGradient;
            Assert.AreEqual(Angle.TripleHalfQuarter.Value, gradient.Angle);
            Assert.AreEqual(2, gradient.Stops.Count());
            Assert.AreEqual(Color.Red, gradient.Stops.First().Color);
            Assert.AreEqual(Color.Blue, gradient.Stops.Last().Color);
        }

        [Test]
        public void BackgroundImageLinearGradientWithSide()
        {
            var source = "background-image: linear-gradient(to right, red, orange, yellow, green, blue, indigo, violet)";
            var property = CssParser.ParseDeclaration(source);
            Assert.IsInstanceOf<CSSBackgroundImageProperty>(property);
            var backgroundImage = property as CSSBackgroundImageProperty;
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInitial);
            Assert.AreEqual(1, backgroundImage.Images.Count());
            var image = backgroundImage.Images.First();
            Assert.IsInstanceOf<LinearGradient>(image);
            var gradient = image as LinearGradient;
            Assert.AreEqual(Angle.Quarter.Value, gradient.Angle);
            var stops = gradient.Stops.ToArray();
            Assert.AreEqual(7, stops.Length);
            Assert.AreEqual(Colors.FromName("red").Value, stops[0].Color);
            Assert.AreEqual(Colors.FromName("orange").Value, stops[1].Color);
            Assert.AreEqual(Colors.FromName("yellow").Value, stops[2].Color);
            Assert.AreEqual(Colors.FromName("green").Value, stops[3].Color);
            Assert.AreEqual(Colors.FromName("blue").Value, stops[4].Color);
            Assert.AreEqual(Colors.FromName("indigo").Value, stops[5].Color);
            Assert.AreEqual(Colors.FromName("violet").Value, stops[6].Color);
        }

        [Test]
        public void BackgroundImageLinearGradientWithCornerAndRgba()
        {
            var source = "background-image: linear-gradient(to bottom right, red, rgba(255,0,0,0))";
            var property = CssParser.ParseDeclaration(source);
            Assert.IsInstanceOf<CSSBackgroundImageProperty>(property);
            var backgroundImage = property as CSSBackgroundImageProperty;
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInitial);
            Assert.AreEqual(1, backgroundImage.Images.Count());
            var image = backgroundImage.Images.First();
            Assert.IsInstanceOf<LinearGradient>(image);
            var gradient = image as LinearGradient;
            Assert.AreEqual(Angle.TripleHalfQuarter.Value, gradient.Angle);
            Assert.AreEqual(2, gradient.Stops.Count());
            Assert.AreEqual(Color.Red, gradient.Stops.First().Color);
            Assert.AreEqual(Color.FromRgba(255, 0, 0, 0), gradient.Stops.Last().Color);
        }

        [Test]
        public void BackgroundImageLinearGradientWithSideAndHsl()
        {
            var source = "background-image: linear-gradient(to bottom, hsl(0, 80%, 70%), #bada55)";
            var property = CssParser.ParseDeclaration(source);
            Assert.IsInstanceOf<CSSBackgroundImageProperty>(property);
            var backgroundImage = property as CSSBackgroundImageProperty;
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInitial);
            Assert.AreEqual(1, backgroundImage.Images.Count());
            var image = backgroundImage.Images.First();
            Assert.IsInstanceOf<LinearGradient>(image);
            var gradient = image as LinearGradient;
            Assert.AreEqual(Angle.Half.Value, gradient.Angle);
            Assert.AreEqual(2, gradient.Stops.Count());
            Assert.AreEqual(Color.FromHsl(0f, 0.8f, 0.7f), gradient.Stops.First().Color);
            Assert.AreEqual(Color.FromHex("bada55"), gradient.Stops.Last().Color);
        }

        [Test]
        public void BackgroundImageLinearGradientNoAngle()
        {
            var source = "background-image: linear-gradient(yellow, blue 20%, #0f0)";
            var property = CssParser.ParseDeclaration(source);
            Assert.IsInstanceOf<CSSBackgroundImageProperty>(property);
            var backgroundImage = property as CSSBackgroundImageProperty;
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInitial);
            Assert.AreEqual(1, backgroundImage.Images.Count());
            var image = backgroundImage.Images.First();
            Assert.IsInstanceOf<LinearGradient>(image);
            var gradient = image as LinearGradient;
            Assert.AreEqual(Angle.Half.Value, gradient.Angle);
            Assert.AreEqual(3, gradient.Stops.Count());
            Assert.AreEqual(Colors.FromName("yellow").Value, gradient.Stops.First().Color);
            Assert.AreEqual(Colors.FromName("blue").Value, gradient.Stops.Skip(1).First().Color);
            Assert.AreEqual(Color.FromRgb(0, 255, 0), gradient.Stops.Skip(2).First().Color);
        }

        [Test]
        public void BackgroundImageRadialGradientCircleFarthestCorner()
        {
            var source = "background-image: radial-gradient(circle farthest-corner at 45px 45px , #00FFFF 0%, rgba(0, 0, 255, 0) 50%, #0000FF 95%)";
            var property = CssParser.ParseDeclaration(source);
            Assert.IsInstanceOf<CSSBackgroundImageProperty>(property);
            var backgroundImage = property as CSSBackgroundImageProperty;
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInitial);
            Assert.AreEqual(1, backgroundImage.Images.Count());
            var image = backgroundImage.Images.First();
            Assert.IsInstanceOf<RadialGradient>(image);
            var gradient = image as RadialGradient;
            Assert.AreEqual(new Length(45f, Length.Unit.Px), gradient.X);
            Assert.AreEqual(new Length(45f, Length.Unit.Px), gradient.Y);
            var stops = gradient.Stops.ToArray();
            Assert.AreEqual(3, stops.Length);
            Assert.AreEqual(Color.FromRgb(0, 255, 255), stops[0].Color);
            Assert.AreEqual(Color.FromRgba(0, 0, 255, 0), stops[1].Color);
            Assert.AreEqual(Color.FromRgb(0, 0, 255), stops[2].Color);
        }

        [Test]
        public void BackgroundImageRadialGradientEllipseFarthestCorner()
        {
            var source = "background-image: radial-gradient(ellipse farthest-corner at 470px 47px , #FFFF80 20%, rgba(204, 153, 153, 0.4) 30%, #E6E6FF 60%)";
            var property = CssParser.ParseDeclaration(source);
            Assert.IsInstanceOf<CSSBackgroundImageProperty>(property);
            var backgroundImage = property as CSSBackgroundImageProperty;
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInitial);
            Assert.AreEqual(1, backgroundImage.Images.Count());
            var image = backgroundImage.Images.First();
            Assert.IsInstanceOf<RadialGradient>(image);
            var gradient = image as RadialGradient;
            Assert.AreEqual(new Length(470f, Length.Unit.Px), gradient.X);
            Assert.AreEqual(new Length(47f, Length.Unit.Px), gradient.Y);
            var stops = gradient.Stops.ToArray();
            Assert.AreEqual(3, stops.Length);
            Assert.AreEqual(Color.FromRgb(0xFF, 0xFF, 0x80), stops[0].Color);
            Assert.AreEqual(Color.FromRgba(204, 153, 153, 0.4f), stops[1].Color);
            Assert.AreEqual(Color.FromRgb(0xE6, 0xE6, 0xFF), stops[2].Color);
        }

        [Test]
        public void BackgroundImageRadialGradientFarthestCornerWithPoint()
        {
            var source = "background-image: radial-gradient(farthest-corner at 45px 45px , #FF0000 0%, #0000FF 100%)";
            var property = CssParser.ParseDeclaration(source);
            Assert.IsInstanceOf<CSSBackgroundImageProperty>(property);
            var backgroundImage = property as CSSBackgroundImageProperty;
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInitial);
            Assert.AreEqual(1, backgroundImage.Images.Count());
            var image = backgroundImage.Images.First();
            Assert.IsInstanceOf<RadialGradient>(image);
            var gradient = image as RadialGradient;
            Assert.AreEqual(new Length(45f, Length.Unit.Px), gradient.X);
            Assert.AreEqual(new Length(45f, Length.Unit.Px), gradient.Y);
            var stops = gradient.Stops.ToArray();
            Assert.AreEqual(2, stops.Length);
            Assert.AreEqual(Color.FromRgb(255, 0, 0), stops[0].Color);
            Assert.AreEqual(Color.FromRgb(0, 0, 255), stops[1].Color);
        }

        [Test]
        public void BackgroundImageRadialGradientSingleSize()
        {
            var source = "background-image: radial-gradient(16px at 60px 50% , #000000 0%, #000000 14px, rgba(0, 0, 0, 0.3) 18px, rgba(0, 0, 0, 0) 19px)";
            var property = CssParser.ParseDeclaration(source);
            Assert.IsInstanceOf<CSSBackgroundImageProperty>(property);
            var backgroundImage = property as CSSBackgroundImageProperty;
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInitial);
            Assert.AreEqual(1, backgroundImage.Images.Count());
            var image = backgroundImage.Images.First();
            Assert.IsInstanceOf<RadialGradient>(image);
            var gradient = image as RadialGradient;
            Assert.AreEqual(new Length(60f, Length.Unit.Px), gradient.X);
            Assert.AreEqual(Length.Half, gradient.Y);
            var stops = gradient.Stops.ToArray();
            Assert.AreEqual(4, stops.Length);
            Assert.AreEqual(Color.FromRgb(0, 0, 0), stops[0].Color);
            Assert.AreEqual(Color.FromRgb(0, 0, 0), stops[1].Color);
            Assert.AreEqual(Color.FromRgba(0, 0, 0, 0.3), stops[2].Color);
            Assert.AreEqual(Color.Transparent, stops[3].Color);
        }

        [Test]
        public void BackgroundImageRadialGradientCircle()
        {
            var source = "background-image: radial-gradient(circle, yellow, green)";
            var property = CssParser.ParseDeclaration(source);
            Assert.IsInstanceOf<CSSBackgroundImageProperty>(property);
            var backgroundImage = property as CSSBackgroundImageProperty;
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInitial);
            Assert.AreEqual(1, backgroundImage.Images.Count());
            var image = backgroundImage.Images.First();
            Assert.IsInstanceOf<RadialGradient>(image);
            var gradient = image as RadialGradient;
            Assert.AreEqual(Length.Half, gradient.X);
            Assert.AreEqual(Length.Half, gradient.Y);
            var stops = gradient.Stops.ToArray();
            Assert.AreEqual(2, stops.Length);
            Assert.AreEqual(Color.FromName("yellow").Value, stops[0].Color);
            Assert.AreEqual(Color.FromName("green").Value, stops[1].Color);
        }

        [Test]
        public void BackgroundImageRadialGradientOnlyGradientStops()
        {
            var source = "background-image: radial-gradient(yellow, green)";
            var property = CssParser.ParseDeclaration(source);
            Assert.IsInstanceOf<CSSBackgroundImageProperty>(property);
            var backgroundImage = property as CSSBackgroundImageProperty;
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInitial);
            Assert.AreEqual(1, backgroundImage.Images.Count());
            var image = backgroundImage.Images.First();
            Assert.IsInstanceOf<RadialGradient>(image);
            var gradient = image as RadialGradient;
            Assert.AreEqual(Length.Half, gradient.X);
            Assert.AreEqual(Length.Half, gradient.Y);
            var stops = gradient.Stops.ToArray();
            Assert.AreEqual(2, stops.Length);
            Assert.AreEqual(Color.FromName("yellow").Value, stops[0].Color);
            Assert.AreEqual(Color.FromName("green").Value, stops[1].Color);
        }

        [Test]
        public void BackgroundImageRadialGradientEllipseAtCenter()
        {
            var source = "background-image: radial-gradient(ellipse at center, yellow 0%, green 100%)";
            var property = CssParser.ParseDeclaration(source);
            Assert.IsInstanceOf<CSSBackgroundImageProperty>(property);
            var backgroundImage = property as CSSBackgroundImageProperty;
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInitial);
            Assert.AreEqual(1, backgroundImage.Images.Count());
            var image = backgroundImage.Images.First();
            Assert.IsInstanceOf<RadialGradient>(image);
            var gradient = image as RadialGradient;
            Assert.AreEqual(Length.Half, gradient.X);
            Assert.AreEqual(Length.Half, gradient.Y);
            var stops = gradient.Stops.ToArray();
            Assert.AreEqual(2, stops.Length);
            Assert.AreEqual(Color.FromName("yellow").Value, stops[0].Color);
            Assert.AreEqual(Color.FromName("green").Value, stops[1].Color);
        }

        [Test]
        public void BackgroundImageRadialGradientFarthestCornerWithoutPoint()
        {
            var source = "background-image: radial-gradient(farthest-corner, yellow, green)";
            var property = CssParser.ParseDeclaration(source);
            Assert.IsInstanceOf<CSSBackgroundImageProperty>(property);
            var backgroundImage = property as CSSBackgroundImageProperty;
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInitial);
            Assert.AreEqual(1, backgroundImage.Images.Count());
            var image = backgroundImage.Images.First();
            Assert.IsInstanceOf<RadialGradient>(image);
            var gradient = image as RadialGradient;
            Assert.AreEqual(Length.Half, gradient.X);
            Assert.AreEqual(Length.Half, gradient.Y);
            var stops = gradient.Stops.ToArray();
            Assert.AreEqual(2, stops.Length);
            Assert.AreEqual(Color.FromName("yellow").Value, stops[0].Color);
            Assert.AreEqual(Color.FromName("green").Value, stops[1].Color);
        }

        [Test]
        public void BackgroundImageRadialGradientClosestSideWithPoint()
        {
            var source = "background-image: radial-gradient(closest-side at 20px 30px, red, yellow, green)";
            var property = CssParser.ParseDeclaration(source);
            Assert.IsInstanceOf<CSSBackgroundImageProperty>(property);
            var backgroundImage = property as CSSBackgroundImageProperty;
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInitial);
            Assert.AreEqual(1, backgroundImage.Images.Count());
            var image = backgroundImage.Images.First();
            Assert.IsInstanceOf<RadialGradient>(image);
            var gradient = image as RadialGradient;
            Assert.AreEqual(new Length(20f, Length.Unit.Px), gradient.X);
            Assert.AreEqual(new Length(30f, Length.Unit.Px), gradient.Y);
            var stops = gradient.Stops.ToArray();
            Assert.AreEqual(3, stops.Length);
            Assert.AreEqual(Color.FromName("red").Value, stops[0].Color);
            Assert.AreEqual(Color.FromName("yellow").Value, stops[1].Color);
            Assert.AreEqual(Color.FromName("green").Value, stops[2].Color);
        }

        [Test]
        public void BackgroundImageRadialGradientSizeAndPoint()
        {
            var source = "background-image: radial-gradient(20px 30px at 20px 30px, red, yellow, green)";
            var property = CssParser.ParseDeclaration(source);
            Assert.IsInstanceOf<CSSBackgroundImageProperty>(property);
            var backgroundImage = property as CSSBackgroundImageProperty;
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInitial);
            Assert.AreEqual(1, backgroundImage.Images.Count());
            var image = backgroundImage.Images.First();
            Assert.IsInstanceOf<RadialGradient>(image);
            var gradient = image as RadialGradient;
            Assert.AreEqual(new Length(20f, Length.Unit.Px), gradient.X);
            Assert.AreEqual(new Length(30f, Length.Unit.Px), gradient.Y);
            var stops = gradient.Stops.ToArray();
            Assert.AreEqual(3, stops.Length);
            Assert.AreEqual(Color.FromName("red").Value, stops[0].Color);
            Assert.AreEqual(Color.FromName("yellow").Value, stops[1].Color);
            Assert.AreEqual(Color.FromName("green").Value, stops[2].Color);
        }

        [Test]
        public void BackgroundImageRadialGradientClosestSideCircleShuffledWithPoint()
        {
            var source = "background-image: radial-gradient(closest-side circle at 20px 30px, red, yellow, green)";
            var property = CssParser.ParseDeclaration(source);
            Assert.IsInstanceOf<CSSBackgroundImageProperty>(property);
            var backgroundImage = property as CSSBackgroundImageProperty;
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInitial);
            Assert.AreEqual(1, backgroundImage.Images.Count());
            var image = backgroundImage.Images.First();
            Assert.IsInstanceOf<RadialGradient>(image);
            var gradient = image as RadialGradient;
            Assert.AreEqual(new Length(20f, Length.Unit.Px), gradient.X);
            Assert.AreEqual(new Length(30f, Length.Unit.Px), gradient.Y);
            var stops = gradient.Stops.ToArray();
            Assert.AreEqual(3, stops.Length);
            Assert.AreEqual(Color.FromName("red").Value, stops[0].Color);
            Assert.AreEqual(Color.FromName("yellow").Value, stops[1].Color);
            Assert.AreEqual(Color.FromName("green").Value, stops[2].Color);
        }
    }
}
