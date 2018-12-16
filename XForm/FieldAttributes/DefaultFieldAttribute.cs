using System;
using System.Reflection;
using XForm.Fields.Bases;
using XForm.Forms;

namespace XForm.FieldAttributes
{
    public abstract class DefaultFieldAttribute : FieldAttribute
    {
        public Func<Field> FieldCreator { get; }
        
        public override PropertyInfo BindedFieldProperty { get; }

        protected DefaultFieldAttribute(Func<Field> fieldCreator, PropertyInfo bindedFieldProperty)
        {
            FieldCreator = fieldCreator;
            BindedFieldProperty = bindedFieldProperty;
        }

        public override Field CreateField(FormModel formModel, PropertyInfo propertyInfo)
        {
            return FieldCreator();
        }
    }
}