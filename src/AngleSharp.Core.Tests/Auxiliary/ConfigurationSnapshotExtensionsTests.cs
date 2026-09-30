namespace AngleSharp.Core.Tests.Auxiliary
{
    using AngleSharp.Io;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [TestFixture]
    public class ConfigurationSnapshotExtensionsTests
    {
        [Test]
        public void DefaultConfigurationExtensionsKeepOrderAndOriginalServices()
        {
            var original = Configuration.Default;
            var first = new Object();
            var second = new Object();
            var configured = original.With(first).With(second).Without(first);

            Assert.That(configured.Services.First(), Is.SameAs(second));
            Assert.That(configured.Services.Contains(first), Is.False);
            Assert.That(original.Services.Contains(first), Is.False);
            Assert.That(configured.Services.Count(), Is.EqualTo(original.Services.Count() + 1));
        }

        [Test]
        public void TypedRemovalMatchesExistingDistinctAndCreatorSemantics()
        {
            var duplicate = new Object();
            var requester = new DefaultHttpRequester();
            Func<IBrowsingContext, IRequester> creator = _ => new DefaultHttpRequester();
            var configured = Configuration.Default.With(duplicate).With(duplicate)
                .With((IRequester)requester).With(creator).Without<IRequester>();

            Assert.That(configured.Services.Count(item => Object.ReferenceEquals(item, duplicate)), Is.EqualTo(1));
            Assert.That(configured.Services.OfType<IRequester>(), Is.Empty);
            Assert.That(configured.Services.OfType<Func<IBrowsingContext, IRequester>>(), Is.Empty);
        }

        [Test]
        public void CallerProvidedServiceSequencesStayDeferred()
        {
            var services = new List<Object>();
            var added = new Object();
            var configured = new Configuration(services).With(new Object());

            services.Add(added);

            Assert.That(configured.Services.Contains(added), Is.True);
        }

        [Test]
        public void RemovingOneServiceUsesIdentityRatherThanEquality()
        {
            var first = new EqualService();
            var second = new EqualService();
            var configured = Configuration.Default.With(first).With(second).Without(first);

            Assert.That(configured.Services.Any(item => Object.ReferenceEquals(item, second)), Is.True);
            Assert.That(configured.Services.Any(item => Object.ReferenceEquals(item, first)), Is.False);
        }

        private sealed class EqualService
        {
            public override Boolean Equals(Object obj) => obj is EqualService;
            public override Int32 GetHashCode() => 1;
        }
    }
}
