using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTests.Mocks;

namespace UnitTests
{
    [TestClass]
    public class ReflectionResolverTests
    {
        private MockReflectionResolver mockResolver;
        private MockService mockService;
        private List<MockService> mockServices;

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

        [TestMethod]
        public void SetsInternalResolver()
        {
            // Act
            DependencyResolver.SetResolver(mockResolver);

            // Assert
            Assert.IsNotNull(DependencyResolver.Current);
            Assert.IsInstanceOfType(DependencyResolver.Current, typeof(DelegateBasedDependencyResolver));
        }

        [TestMethod]
        public void GetServiceReturnsService()
        {
            // Arrange
            DependencyResolver.SetResolver(mockResolver);
            var service = DependencyResolver.Current.GetService<MockService>();

            // Assert
            Assert.AreEqual(mockService, service);
        }

        [TestMethod]
        public void GetServicesReturnsServices()
        {
            // Arrange
            DependencyResolver.SetResolver(mockResolver);
            var services = DependencyResolver.Current.GetServices<MockService>();

            // Assert
            Assert.AreEqual(1, services.Count());
            Assert.AreEqual(mockService, services.FirstOrDefault());
        }
    }
}
