using XForm.Fields;
using XForm.Forms;
using XForm.Tests.Mocks;
using Xunit;

namespace XForm.Tests.Tests
{
    public class ButtonFieldTests
    {
        public ButtonFieldTests()
        {
            MockForm.Register();
        }
        
        [Fact]
        public void TestEnabled()
        {
            var command = new MockCommand();
            var field = new ButtonField("Title", command);
            
            Form.Create(new[]
            {
                field
            });
            
            Assert.Equal(false, field.Enabled);

            command.Enabled = true;
            
            Assert.Equal(true, field.Enabled);
        }
    }
}