using XForm.Binding;
using Xunit;
using Xunit.Sdk;

namespace XForm.Tests.Tests
{
    public class BindableBaseTests
    {
        private class TestObject : BindableBase
        {
            private bool _testProperty;

            public bool TestProperty
            {
                get => _testProperty;
                set => Set(ref _testProperty, value);
            }
        } 
        
        [Fact]
        public void TestEventOnPropertyChanged()
        {
            var testObject = new TestObject();

            var ex = Record.Exception(() =>
            {
                Assert.PropertyChanged(testObject, "TestProperty", () => testObject.TestProperty = !testObject.TestProperty);
            });
            
            Assert.Null(ex);
        }
        
        [Fact]
        public void TestNoEventOnSameValue()
        {
            var testObject = new TestObject();

            var ex = Record.Exception(() =>
            {
                Assert.PropertyChanged(testObject, "TestProperty", () => testObject.TestProperty = testObject.TestProperty);
            });

            Assert.IsType<PropertyChangedException>(ex);
        }
    }
}