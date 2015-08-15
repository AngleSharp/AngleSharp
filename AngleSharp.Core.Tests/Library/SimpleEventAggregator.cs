namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Core.Tests.Mocks;
    using AngleSharp.Events;
    using AngleSharp.Events.Default;
    using NUnit.Framework;
    using System.Threading.Tasks;

    [TestFixture]
    public class SimpleEventAggregatorTests
    {
        [Test]
        public void CreateSimpleEventAggregatorUseExtensionMethod()
        {
            var aggregator = new SimpleEventAggregator();
            var config = Configuration.Default.SetEvents(aggregator);
            Assert.AreEqual(aggregator, config.Events);
        }

        [Test]
        public void AddSubscriberToPreviouslyUnitializedEventAggregatorOverExtensionMethod()
        {
            var count = 0;
            var subscriber = new MockSubscriber<object>(_ => ++count);
            var config = Configuration.Default.SetHandler(subscriber);
            Assert.IsNotNull(config.Events);
            config.Events.Publish(default(object));
            Assert.AreEqual(1, count);
        }

        [Test]
        public void AddSubscriberToPreviouslyInitializedEventAggregatorOverExtensionMethod()
        {
            var count = 0;
            var aggregator = new SimpleEventAggregator();
            var subscriber = new MockSubscriber<object>(_ => ++count);
            var config = Configuration.Default.SetEvents(aggregator).SetHandler(subscriber);
            Assert.AreEqual(aggregator, config.Events);
            config.Events.Publish(default(object));
            Assert.AreEqual(1, count);
        }

        [Test]
        public async Task ListenForHtmlErrorsWithMissingDoctype()
        {
            var count = 0;
            var htmlContent = "<html></html>";
            var subscriber = new MockSubscriber<HtmlParseErrorEvent>(_ => ++count);
            var config = Configuration.Default.SetHandler(subscriber);
            var context = await BrowsingContext.New(config).OpenAsync(m => m.Content(htmlContent));
            Assert.AreEqual(1, count);
        }
    }
}
