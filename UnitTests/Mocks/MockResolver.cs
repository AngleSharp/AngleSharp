using System;
using System.Collections.Generic;
using AngleSharp;

namespace UnitTests.Mocks
{
    public class MockResolver : IDependencyResolver
    {
        public Func<object> GetServiceDelegate = null;
        public Func<IEnumerable<object>> GetServicesDelegate = null;

        public object GetService(Type requestedService)
        {
            return GetServiceDelegate();
        }

        public IEnumerable<object> GetServices(Type requestedService)
        {
            return GetServicesDelegate();
        }
    }
}