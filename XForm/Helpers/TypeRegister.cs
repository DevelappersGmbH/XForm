using System;
using System.Collections.Generic;
using System.Linq;

namespace XForm.Helpers
{
    public class TypeRegister<T>
    {
        private readonly Dictionary<string, T> _register;

        public TypeRegister()
        {
            _register = new Dictionary<string, T>();
        }

        public void Register<TType>(T value)
        {
            Type registeredType;
            if ((registeredType = RegisteredType(typeof(TType), out _)) != null)
                throw new ArgumentException($"Type already registered as {registeredType.FullName}");

            Register(typeof(TType).FullName, value);
        }

        private void Register(string key, T value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            _register.Add(key, value);
        }

        public T Value<TType>()
        {
            return Value(typeof(TType));
        }

        public T Value(Type keyType)
        {
            if (TryValue(keyType, out var value))
                return value;

            throw new ArgumentException($"Value for type {keyType.FullName} not registered");
        }

        public bool TryValue<TType>(out T value)
        {
            return TryValue(typeof(TType), out value);
        }

        public bool TryValue(Type keyType, out T value)
        {
            return RegisteredType(keyType, out value) != null;
        }

        private Type RegisteredType(Type keyType, out T value)
        {
            T resolvedValue;

            // Try resolve by field's full name
            if (TryResolveTypeForKey(keyType.FullName, out resolvedValue))
            {
                value = resolvedValue;
                return keyType;
            }

            // Try resolve by field's interfaces
            var interfaces = keyType.GetInterfaces();
            
            Type interfaceType;
            if ((interfaceType = interfaces.FirstOrDefault(type => TryResolveTypeForKey(type.FullName, out resolvedValue))) != null)
            {
                value = resolvedValue;
                return interfaceType;
            }

            value = default(T);
            return null;
        }

        private bool TryResolveTypeForKey(string key, out T type)
        {
            if (key == null || !_register.ContainsKey(key))
            {
                type = default(T);
                return false;
            }

            type = _register[key];
            return true;
        }
    }
}