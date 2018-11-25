using XForm.Fields;
using XForm.Fields.Interfaces;
using Xunit;

namespace XForm.Tests.Helpers
{
    public static class Helper
    {
        public static void AssertLabelView(IField field, string title, string value)
        {
            Assert.IsType<LabelField>(field);
            Assert.NotNull(field.Form);
            
            Assert.Equal(title, field.Title);
            Assert.Equal(value, (field as LabelField)?.Value);
        }

        public static void AssertValueField<TField, TValue>(IField field, string title, TValue value) where TField: IValueField<TValue>
        {
            Assert.IsType<TField>(field);
            Assert.NotNull(field.Form);
            
            Assert.Equal(title, field.Title);
            Assert.Equal(value, ((TField) field).Value);
        }
    }
}