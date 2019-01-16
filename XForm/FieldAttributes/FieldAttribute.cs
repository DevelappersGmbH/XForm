using System;
using System.Reflection;
using XForm.Fields.Bases;
using XForm.Forms;

namespace XForm.FieldAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class FieldAttribute : Attribute
    {
        public abstract PropertyInfo BindedFieldProperty { get; }

        public abstract Field CreateField(FormModel formModel, PropertyInfo propertyInfo);
    }
}