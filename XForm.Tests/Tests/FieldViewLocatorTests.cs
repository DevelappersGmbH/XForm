using System;
using XForm.Fields;
using XForm.Fields.Interfaces;
using XForm.Forms;
using XForm.Tests.Mocks;
using Xunit;

namespace XForm.Tests.Tests
{
    public class FieldViewLocatorTests
    {
        [Fact]
        public void TestRegisterFieldView()
        {
            var locator = new FieldViewLocator();
            var field = new LabelField("", "");

            Assert.Throws<ArgumentException>(() => locator.ViewTypeForField(field));
            
            locator.Register<LabelField, MockLabelFieldView>();

            Assert.Equal(typeof(MockLabelFieldView), locator.ViewTypeForField(field));
        }

        [Fact]
        public void TestRegisterInterfaces()
        {
            var locator = new FieldViewLocator();
            var field = new LabelField("", "");

            Assert.Throws<ArgumentException>(() => locator.ViewTypeForField(field));
            
            locator.Register<IField, MockLabelFieldView>();
            Assert.Equal(typeof(MockLabelFieldView), locator.ViewTypeForField(field));
            
            // Throws for multiple candidates?
            Assert.Throws<ArgumentException>(() => locator.Register<LabelField, MockLabelFieldView>());
        }
    }
}