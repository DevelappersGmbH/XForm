using System;
using System.Collections.Generic;
using Android.Views;
using XForm.Droid.FieldViews.Bases;
using XForm.FieldViews;
using XForm.Helpers;

namespace XForm.Droid.Forms
{
    public class FieldViewCreator
    {
        private readonly List<Type> _registeredTypes = new List<Type>();
        private readonly TypeRegister<Func<ViewGroup, FieldView>> _register = new TypeRegister<Func<ViewGroup, FieldView>>();

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
            return CreateFieldView(parent, fieldViewType);
        }
        
        public void RegisterCustomCreator<TFieldView>(Func<ViewGroup, FieldView> creator) where TFieldView: IFieldView
        {
            _register.Register<TFieldView>(creator);
        }
        
        private FieldView CreateFieldView(ViewGroup parent, Type fieldViewType)
        {
            if (_register.TryValue(fieldViewType, out var viewCreator))
                return viewCreator.Invoke(parent);
            
            return (FieldView) Activator.CreateInstance(fieldViewType, parent);
        }
    }
}