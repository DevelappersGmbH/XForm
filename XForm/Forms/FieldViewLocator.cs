using System;
using XForm.Fields.Interfaces;
using XForm.FieldViews;
using XForm.Helpers;

namespace XForm.Forms
{
    /// <summary>
    /// Resolves field view for fields.
    /// </summary>
    public class FieldViewLocator
    {
        private readonly TypeRegister<Type> _register;

        public FieldViewLocator()
        {
            _register = new TypeRegister<Type>();
        }

        /// <summary>
        /// Register a field view for a field. 
        /// </summary>
        /// <typeparam name="TField">Type of field (can be an interface)</typeparam>
        /// <typeparam name="TFieldView">Type of field view</typeparam>
        public void Register<TField, TFieldView>() 
            where TField: IField 
            where TFieldView: IFieldView
        {
            _register.Register<TField>(typeof(TFieldView));
        }

        /// <summary>
        /// Resolves field view's type for an field.
        /// </summary>
        /// <param name="field">Field</param>
        /// <returns>Resolved field view's type</returns>
        public Type ViewTypeForField(IField field)
        {
            return _register.Value(field.GetType());
        }
    }
}