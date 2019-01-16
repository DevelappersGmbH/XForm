using XForm.Fields;
using Xunit;

namespace XForm.Tests.Tests
{
    public class LabelFieldTests
    {
        [Fact]
        public void TestCreateLabel()
        {
            var label = new LabelField("Title", "Text");
            
            Assert.Equal("Title", label.Title);
            Assert.Equal("Text", label.Value);
        }
    }
}