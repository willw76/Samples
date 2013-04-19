using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Sample.Framework.DI
{
    public abstract class DiContext
    {
        public abstract string ContextName { get; }
        public abstract Type ContextType { get; }
        internal abstract MethodCallExpression InitExpression { get; }
        internal abstract IList<DiPropertyContext> InjectedProperties { get; }
        internal abstract object CreateInstance();

        public static DiContext<TType> Register<TType>(string name)
        {
            return new DiContext<TType>(name);
        }
        public static DiContext<TType> Register<TType>()
        {
            var type = typeof(TType);
            if (type.IsInterface)
                throw new TypeLoadException(string.Format("Type {0} is an interface type", type));
            return new DiContext<TType>(type.FullName);
        }
    }

    public class DiContext<T> : DiContext, IDisposable
    {
        private readonly string _contextName;
        private readonly Type _contextType;
        private MethodCallExpression _initExpression;
        private readonly IList<DiPropertyContext> _injectedProperties = new List<DiPropertyContext>();
        
        internal DiContext(string name)
        {
            _contextName = name;
            _injectedProperties = new List<DiPropertyContext>();
            _contextType = typeof (T);
        }

        public override string ContextName { get { return _contextName; } }
        public override Type ContextType { get { return _contextType; } }
        internal override MethodCallExpression InitExpression { get { return _initExpression; } }
        internal override IList<DiPropertyContext> InjectedProperties { get { return _injectedProperties; } } 
        
        public DiContext<T> RegisterInit(Expression<Action<T>> initExpression)
        {
            _initExpression = initExpression.Body as MethodCallExpression;
            return this;
        }

        public DiPropertyContext InjectProperty (Expression<Func<T, object>> property)
        {
            var p = DiPropertyContext.Create(this, ObtainMemberName(property.Body));
            _injectedProperties.Add(p);
            return p;
        }

        public void Dispose()
        {
            _initExpression = null;
            _injectedProperties.Clear();
        }

        private static string ObtainMemberName (Expression expression)
        {
            var memberExpression = expression as MemberExpression;
            if (memberExpression!=null)
            {
                if (memberExpression.Expression.NodeType == ExpressionType.MemberAccess)
                    return string.Format("{0}.{1}", ObtainMemberName(memberExpression.Expression),
                                         memberExpression.Member.Name);
                return memberExpression.Member.Name;
            }

            var unaryExpression = expression as UnaryExpression;
            if (unaryExpression!=null)
            {
                if (unaryExpression.NodeType != ExpressionType.Convert)
                    throw new InvalidOperationException(string.Format("Unable to interpret member from {0}", expression));
                return ObtainMemberName(unaryExpression.Operand);
            }

            throw new InvalidOperationException(string.Format("Could not determine member from {0}", expression));
        }
        
        internal override object CreateInstance()
        {
            var constructor = _contextType.GetConstructor(new Type[] { });
            if (constructor == null)
                throw new TypeLoadException(string.Format("Unable to obtain default constructor of type {0}", _contextType));
            object instance = constructor.Invoke(null);

            // propery injections
            foreach (var propertyContext in _injectedProperties)
            {
                if (!propertyContext.ValueType.HasValue) continue;

                var propertyInfo = _contextType.GetProperty(propertyContext.PropertyName);

                object value;
                if (propertyContext.ValueType.Value == DiPropertyValueType.DefaultContextValue)
                    value = DiFactory.GetContext(propertyContext.ContextValue);
                else if (propertyContext.ValueType.Value == DiPropertyValueType.NamedContextValue)
                    value = DiFactory.GetContext(propertyContext.ContextName);
                else value = propertyContext.Value;
                propertyInfo.SetValue(instance, value, null);
            }

            // init 
            if (_initExpression != null)
                _initExpression.Method.Invoke(instance, null);

            return instance;
        }
    }
}