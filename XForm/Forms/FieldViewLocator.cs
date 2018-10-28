using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using XForm.Fields.Interfaces;
using XForm.FieldViews;

namespace XForm.Forms
{
    public class FieldViewLocator
    {
        protected readonly Dictionary<string, Type> ViewTypes;

        public FieldViewLocator()
        {
            ViewTypes = new Dictionary<string, Type>();
        }

        public void Register<TField, TFieldView>() where TField: IField where TFieldView: IFieldView
        {
            Register(typeof(TField).FullName, typeof(TFieldView));
        }

        private void Register(string fieldKey, Type fieldViewType)
        {
            if (fieldKey == null)
                throw new ArgumentNullException(nameof(fieldKey));
            
            ViewTypes.Add(fieldKey, fieldViewType);
        }

        public Type ViewTypeForField(IField field)
        {
            Type type;
            
            // Try resolve by field's full name
            if (TryResolveViewTypeForFieldKey(field.GetType().FullName, out type))
                return type;
            
            // Try resolve by field's interfaces
            if (field.GetType().GetInterfaces().Any(interfaceType => TryResolveViewTypeForFieldKey(interfaceType.FullName, out type)))
                return type;
            
            throw new ArgumentException($"Field view for field {field.GetType().FullName} not registered");
        }

        private bool TryResolveViewTypeForFieldKey(string fieldKey, out Type viewType) 
        {
            if (fieldKey == null || !ViewTypes.ContainsKey(fieldKey))
            {
                viewType = null;
                return false;
            }
            
            viewType = ViewTypes[fieldKey];
            return true;
        }
    }
}