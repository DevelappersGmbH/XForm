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
        private readonly TypeRegister<Func<ViewGroup, FieldView>> _fieldViewCreatorRegister = new TypeRegister<Func<ViewGroup, FieldView>>();

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
        
        public void RegisterFieldViewCreator<TFieldView>(Func<ViewGroup, FieldView> creator) where TFieldView: IFieldView
        {
            _fieldViewCreatorRegister.Register<TFieldView>(creator);
        }
        
        private FieldView CreateFieldView(ViewGroup parent, Type fieldViewType)
        {
            if (_fieldViewCreatorRegister.TryValue(fieldViewType, out var fieldViewCreator))
                return fieldViewCreator.Invoke(parent);
            
            return (FieldView) Activator.CreateInstance(fieldViewType, parent);
        }
    }
}