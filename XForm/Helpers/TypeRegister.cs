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
            T resolvedValue;

            // Try resolve by field's full name
            if (TryResolveViewTypeForFieldKey(keyType.FullName, out resolvedValue))
            {
                value = resolvedValue;
                return true;
            }

            // Try resolve by field's interfaces
            if (keyType.GetInterfaces().Any(interfaceType => TryResolveViewTypeForFieldKey(interfaceType.FullName, out resolvedValue)))
            {
                value = resolvedValue;
                return true;
            }

            value = default(T);
            return false;
        }

        private bool TryResolveViewTypeForFieldKey(string fieldKey, out T viewType)
        {
            if (fieldKey == null || !_register.ContainsKey(fieldKey))
            {
                viewType = default(T);
                return false;
            }

            viewType = _register[fieldKey];
            return true;
        }
    }
}