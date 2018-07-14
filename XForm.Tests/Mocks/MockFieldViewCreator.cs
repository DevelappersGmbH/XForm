using System;
using XForm.FieldViews;
using XForm.Forms;

namespace XForm.Tests.Mocks
{
    public class MockFieldViewCreator : FieldViewCreator
    {
        public override IFieldView CreateFieldView(Type fieldViewType)
        {
            return (IFieldView) Activator.CreateInstance(fieldViewType);
        }
    }
}