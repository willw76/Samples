using System;

namespace Sample.Framework.DI
{
    public class DiPropertyContext : IDisposable
    {
        private DiContext _parent;
        private readonly string _propertyName;
        private object _value;
        private Type _contextValue;
        private string _contextName;
        private DiPropertyValueType? _propertyValueType;

        private DiPropertyContext(DiContext parent, string propertyName)
        {
            _propertyName = propertyName;
            _parent = parent;
        }

        internal static DiPropertyContext Create(DiContext parent, string propertyName)
        {
            return new DiPropertyContext(parent, propertyName);
        }

        public DiContext Parent { get { return _parent; } }
        public string PropertyName { get { return _propertyName; } }
        public object Value { get { return _value; } }
        public Type ContextValue { get { return _contextValue; } }
        public string ContextName { get { return _contextName; } }
        public DiPropertyValueType? ValueType { get { return _propertyValueType; } }

        public DiContext WithValue(object value)
        {
            _propertyValueType = DiPropertyValueType.Value;
            _value = value;
            return _parent;
        }
        public DiContext WithContextValue<TContext>()
        {
            // Inject with another context, context will be created automatically
            _propertyValueType = DiPropertyValueType.DefaultContextValue;
            _contextValue = typeof(TContext);
            return _parent;
        }
        public DiContext WithContextValue<TContext>(string contextName)
        {
            _propertyValueType = DiPropertyValueType.NamedContextValue;
            _contextName = contextName;
            _contextValue = typeof(TContext);
            return _parent;
        }
        public void Dispose()
        {
            _parent = null;
        }
    }
}