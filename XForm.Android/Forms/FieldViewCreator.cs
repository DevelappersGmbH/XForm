using System;
using System.Collections.Generic;
using Android.Views;
using XForm.Android.FieldViews.Bases;

namespace XForm.Android.Forms
{
    public class FieldViewCreator : XForm.Forms.FieldViewCreator
    {
        private readonly List<Type> _registeredTypes = new List<Type>();

        public int ItemViewType(Type fieldViewType)
        {
            if (_registeredTypes.Contains(fieldViewType))
                return _registeredTypes.IndexOf(fieldViewType);
            
            _registeredTypes.Add(fieldViewType);
            return _registeredTypes.Count - 1;
        }

        public FieldView CreateFieldView(ViewGroup parent, int viewType)
        {
            var fieldViewType = _registeredTypes[viewType];
            return (FieldView) Activator.CreateInstance(fieldViewType, parent);
        }
    }
}