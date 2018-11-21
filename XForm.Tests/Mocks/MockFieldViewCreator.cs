using System;
using XForm.FieldViews;

namespace XForm.Tests.Mocks
{
    public class MockFieldViewCreator : FieldViewCreator
    {
        public IFieldView CreateFieldView(Type fieldViewType)
        {
            return (IFieldView) Activator.CreateInstance(fieldViewType);
        }
    }
}