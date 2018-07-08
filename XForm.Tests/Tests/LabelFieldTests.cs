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
            
            Assert.Equal(label.Title, "Title");
            Assert.Equal(label.Value, "Text");
        }
    }
}