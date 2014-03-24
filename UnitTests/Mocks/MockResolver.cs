using System;
using System.Collections.Generic;
using AngleSharp;

namespace UnitTests.Mocks
{
    public class MockResolver : IDependencyResolver
    {
        public Func<Object> GetServiceDelegate = null;
        public Func<IEnumerable<Object>> GetServicesDelegate = null;

        public Object GetService(Type requestedService)
        {
            return GetServiceDelegate();
        }

        public IEnumerable<Object> GetServices(Type requestedService)
        {
            return GetServicesDelegate();
        }
    }
}