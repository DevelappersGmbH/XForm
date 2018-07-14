using System;
using System.Collections.Generic;
using XForm.Fields.Interfaces;
using XForm.Forms;

namespace XForm.Tests.Mocks
{
    public class MockFieldViewLocator : FieldViewLocator
    {
        public Dictionary<string, Type> PublicViewTypes => ViewTypes;

        public Type PublicViewTypeForField(IField field) => ViewTypeForField(field);
    }
}