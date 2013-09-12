using System.Collections.Generic;
using System.Linq;
using AngleSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTests.Mocks;

namespace UnitTests
{
    [TestClass]
    public class ReflectionResolverTests
    {
        MockReflectionResolver mockResolver;
        MockService mockService;
        List<MockService> mockServices;

        private void CreateResolver()
        {
            mockService = new MockService();
            mockServices = new List<MockService>
                           {
                               mockService
                           };

            mockResolver = new MockReflectionResolver();
            mockResolver.GetInstanceDelegate = () => mockService;
            mockResolver.GetAllInstancesDelegate = () => mockServices;
        }

        [TestInitialize]
        public void SetUp()
        {
            CreateResolver();
        }

        [TestCleanup]
        public void Cleanup()
        {
            Configuration.Reset();
        }

        [TestMethod]
        public void SetsInternalResolver()
        {
            // Act
            Configuration.SetDependencyResolver(mockResolver);

            // Assert
            Assert.IsNotNull(Configuration.CurrentResolver);
            Assert.IsInstanceOfType(Configuration.CurrentResolver, typeof(DelegateBasedDependencyResolver));
        }

        [TestMethod]
        public void GetServiceReturnsService()
        {
            // Arrange
            Configuration.SetDependencyResolver(mockResolver);
            var service = Configuration.CurrentResolver.GetService<MockService>();

            // Assert
            Assert.AreEqual(mockService, service);
        }

        [TestMethod]
        public void GetServicesReturnsServices()
        {
            // Arrange
            Configuration.SetDependencyResolver(mockResolver);
            var services = Configuration.CurrentResolver.GetServices<MockService>();

            // Assert
            Assert.AreEqual(1, services.Count());
            Assert.AreEqual(mockService, services.FirstOrDefault());
        }
    }
}
