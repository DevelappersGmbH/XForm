using XForm.Tests.Mocks;
using Xunit;

namespace XForm.Tests.Tests
{
    public class FieldViewCreatorTests
    {
        [Fact]
        public void TestCreateFieldView()
        {
            var creator = new MockFieldViewCreator();
            var view = creator.CreateFieldView(typeof(MockLabelFieldView));
            
            Assert.NotNull(view);
            Assert.IsType<MockLabelFieldView>(view);
        }
    }
}