using System;
using System.Collections.Generic;
using System.Linq;

namespace AngleSharp
{
    class DefaultDependencyResolver : IDependencyResolver
    {
        public Object GetService(Type serviceType)
        {
            if (serviceType.IsInterface || serviceType.IsAbstract)
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
