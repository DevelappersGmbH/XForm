using System.Collections.Generic;
using XForm.Fields;
using XForm.Fields.Interfaces;
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
        public void TestCreateEmptyForm()
        {
            var form = Form.Create();
            
            Assert.NotNull(form);
            Assert.IsType<MockForm>(form);
            
            Assert.NotNull(form.Fields);
            Assert.Empty(form.Fields);
        }
        
        [Fact]
        public void TestCreateFormWithLabels()
        {
            var form = Form.Create(new List<IField>
            {
                new LabelField("Label", "Label Value"),
                new LabelField("Label 2", "Label value 2")
            });
            
            Assert.NotNull(form.Fields);
            Assert.NotEmpty(form.Fields);
            
            Assert.Collection(form.Fields, 
                              field => AssertLabelView(field, "Label", "Label Value"),
                              field => AssertLabelView(field, "Label 2", "Label value 2"));
        }

        [Fact]
        public void TestFieldInsert()
        {
            var form = CreateDefaultForm();
            
            form.InsertField(0, new LabelField("Label 1", "Label Value"));
            form.InsertField(2, new LabelField("Label 2", "Label Value"));
            
            Assert.Collection(form.Fields,
                              field => AssertLabelView(field, "Label 1", "Label Value"),
                              field => { },
                              field => AssertLabelView(field, "Label 2", "Label Value"));
        }
        
        [Fact]
        public void TestEnabled()
        {
            var form = (MockForm) CreateDefaultForm();
            var field = (MockField) form.Fields[0];

            Assert.Equal(true, form.Enabled);
            Assert.Collection(form.Fields, f => Assert.Equal(true, f.Enabled));
            
            Assert.Equal(0, form.EnabledChangedCalledCount);
            Assert.Equal(0, field.EnabledChangedCalledCount);

            form.Enabled = false;
            
            Assert.Equal(false, form.Enabled);
            Assert.Collection(form.Fields, f => Assert.Equal(false, f.Enabled));
            
            Assert.Equal(1, form.EnabledChangedCalledCount);
            Assert.Equal(1, field.EnabledChangedCalledCount);
        }

        private static void AssertLabelView(IField field, string title, string value)
        {
            Assert.IsType<LabelField>(field);
            Assert.NotNull(field.Form);
            
            Assert.Equal(field.Title, title);
            Assert.Equal((field as LabelField)?.Value, value);
        }

        private static Form CreateDefaultForm()
        {
            return Form.Create(new []
            {
                new MockField("Field 1") 
            });
        }
    }
}