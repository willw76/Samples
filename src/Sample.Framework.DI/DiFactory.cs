using System;
using System.Collections.Generic;

namespace Sample.Framework.DI
{
    public static class DiFactory
    {
        private static readonly IDictionary<string, DiContext> _namedContexts = new Dictionary<string, DiContext>();

        /// <summary>
        /// Registers the <paramref name="context"/> object with the framework.
        /// </summary>
        /// <param name="context"></param>
        public static void RegisterContext(DiContext context)
        {
            var c = context;

            if (_namedContexts.ContainsKey(c.ContextName))
                throw new InvalidOperationException(string.Format("Duplicate registration of named context: {0}", c.ContextName));
            _namedContexts.Add(c.ContextName, c);
            // now, get all the interface types and register them with context as well
            foreach (var interfaceType in c.ContextType.GetInterfaces())
                _namedContexts.Add(interfaceType.FullName, c);
        }

        public static T GetContext<T>()
        {
            Type type = typeof(T);
            return (T)GetContext(type);
        }
        public static object GetContext(Type type)
        {
            DiContext context;
            if (!_namedContexts.TryGetValue(type.FullName, out context))
                throw new KeyNotFoundException(string.Format("Context of type {0} was not found in the registry.", type));

            return context.CreateInstance();
        }

        public static T GetContext<T>(string contextName)
        {
            return (T)GetContext(contextName);
        }
        public static object GetContext(string contextName)
        {
            DiContext context;
            if (!_namedContexts.TryGetValue(contextName, out context))
                throw new KeyNotFoundException(string.Format("Context of name {0} was not found in the registry.", contextName));

            return context.CreateInstance();
        }
    }
}
