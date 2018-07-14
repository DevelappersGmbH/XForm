using System;
using XForm.FieldViews;

namespace XForm.Forms
{
    public abstract class FieldViewCreator
    {
        public abstract IFieldView CreateFieldView(Type fieldViewType);
    }
}