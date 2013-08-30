using AngleSharp.Interfaces;
using System;
using System.Collections.Generic;

namespace AngleSharp
{
    class DelegateBasedDependencyResolver : IDependencyResolver
    {
        readonly Func<Type, Object> _getService;
        readonly Func<Type, IEnumerable<Object>> _getServices;

        public DelegateBasedDependencyResolver(Func<Type, Object> getService, Func<Type, IEnumerable<Object>> getServices)
        {
            _getService = getService;
            _getServices = getServices;
        }

        public Object GetService(Type type)
        {
            try
            {
                return _getService.Invoke(type);
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<Object> GetServices(Type type)
        {
            return _getServices(type);
        }
    }
}
