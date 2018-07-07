using System;
using XForm.Forms;
using XForm.Tests.Mocks;
using Xunit;

namespace XForm.Tests.Tests
{
    public class FormTests
    {
        public FormTests()
        {
            MockForm.Register();
        }
        
        [Fact]
        public void CreateFormTest()
        {
            var form = Form.Create();
            
            Assert.NotNull(form);
            Assert.IsType<MockForm>(form);
        }
    }
}