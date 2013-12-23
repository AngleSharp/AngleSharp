using AngleSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AngleSharp
{
    class DefaultDependencyResolver : IDependencyResolver
    {
        public Object GetService(Type serviceType)
        {
            var serviceTypeInfo = serviceType.GetTypeInfo();

            if (serviceTypeInfo.IsInterface || serviceTypeInfo.IsAbstract)
                return null;

            try
            {
                return Activator.CreateInstance(serviceType);
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<Object> GetServices(Type serviceType)
        {
            return Enumerable.Empty<Object>();
        }
    }
}
