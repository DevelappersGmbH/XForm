using System;
using XForm.Fields;
using XForm.Tests.Mocks;
using Xunit;

namespace XForm.Tests.Tests
{
    public class FieldViewLocatorTests
    {
        [Fact]
        public void TestRegisterFieldView()
        {
            var locator = new MockFieldViewLocator();
            
            locator.Register<LabelField, MockLabelFieldView>();
            
            Assert.NotEmpty(locator.PublicViewTypes);
            Assert.Contains(typeof(LabelField).FullName, locator.PublicViewTypes.Keys);
        }

        [Fact]
        public void TestViewForField()
        {
            var locator = new MockFieldViewLocator();
            var field = new LabelField("", "");

            Assert.Throws<ArgumentException>(() => locator.PublicViewTypeForField(field));
            
            locator.Register<LabelField, MockLabelFieldView>();

            var viewType = locator.PublicViewTypeForField(field);
            
            Assert.Equal(viewType.FullName, typeof(MockLabelFieldView).FullName);
        }
    }
}