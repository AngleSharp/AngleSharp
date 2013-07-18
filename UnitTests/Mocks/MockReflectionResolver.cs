using System;
using System.Collections.Generic;

namespace UnitTests.Mocks
{
    public class MockReflectionResolver
    {
        public Func<object> GetInstanceDelegate = null;
        public Func<IEnumerable<object>> GetAllInstancesDelegate = null;

        public object GetInstance(Type requestedService)
        {
            return GetInstanceDelegate();
        }

        public IEnumerable<object> GetAllInstances(Type requestedService)
        {
            return GetAllInstancesDelegate();
        }
    }
}