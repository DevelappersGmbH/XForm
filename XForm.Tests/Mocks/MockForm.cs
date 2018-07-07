using System;
using XForm.Forms;

namespace XForm.Tests.Mocks
{
    public class MockForm: Form
    {
        public static void Register()
        {
            FormCreateFunc = () => new MockForm();
        }
    }
}