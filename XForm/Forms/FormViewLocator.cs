using System;
using System.Collections.Generic;
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

        protected Type ViewTypeForField(IField field)
        {
            var fieldKey = field.GetType().FullName;
            
            if (fieldKey == null)
                throw new ArgumentNullException(nameof(fieldKey));
            
            if (!ViewTypes.ContainsKey(fieldKey))
                throw new ArgumentException($"Field view for field {fieldKey} not registered");
            
            return ViewTypes[fieldKey];
        }
    }
}