using System;
using XForm.Fields.Interfaces;
using XForm.FieldViews;
using XForm.Helpers;

namespace XForm.Forms
{
    public class FieldViewLocator
    {
        private readonly TypeRegister<Type> _register;

        public FieldViewLocator()
        {
            _register = new TypeRegister<Type>();
        }

        public void Register<TField, TFieldView>() where TField: IField where TFieldView: IFieldView
        {
            _register.Register<TField>(typeof(TFieldView));
        }

        public Type ViewTypeForField(IField field)
        {
            return _register.Value(field.GetType());
        }
    }
}