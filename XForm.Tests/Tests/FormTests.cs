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
            
            Assert.NotNull(form.VisibleFields);
            Assert.Empty(form.VisibleFields);
        }
        
        [Fact]
        public void TestCreateFormWithLabels()
        {
            var form = Form.Create(new List<IField>
            {
                new LabelField("Label", "Label Value"),
                new LabelField("Label 2", "Label value 2")
            });
            
            Assert.NotNull(form.VisibleFields);
            Assert.NotEmpty(form.VisibleFields);
            
            Assert.Collection(form.VisibleFields, 
                              field => AssertLabelView(field, "Label", "Label Value"),
                              field => AssertLabelView(field, "Label 2", "Label value 2"));
        }

        [Fact]
        public void TestFieldInsert()
        {
            var form = CreateDefaultForm();
            
            form.InsertField(0, new LabelField("Label 1", "Label Value"));
            form.InsertField(2, new LabelField("Label 2", "Label Value"));
            
            Assert.Collection(form.VisibleFields,
                              field => AssertLabelView(field, "Label 1", "Label Value"),
                              field => { },
                              field => AssertLabelView(field, "Label 2", "Label Value"));
        }

        [Fact]
        public void TestFieldRemove()
        {
            var field1 = new MockField("Field 1");
            var field2 = new MockField("Field 2");
            
            var form = Form.Create(new []
            {
                field1, field2
            });
            
            form.RemoveField(field1);
            
            Assert.Collection(form.VisibleFields, f => Assert.Equal("Field 2", f.Title));
            Assert.Null(field1.Form);
        }
        
        [Fact]
        public void TestEnabled()
        {
            var form = (MockForm) CreateDefaultForm();
            var field = (MockField) form.VisibleFields[0];

            Assert.Equal(true, form.Enabled);
            Assert.Collection(form.VisibleFields, f => Assert.True(f.Enabled));
            
            Assert.Equal(0, form.EnabledChangedCalledCount);
            Assert.Equal(0, field.EnabledChangedCalledCount);

            form.Enabled = false;
            
            Assert.Equal(false, form.Enabled);
            Assert.Collection(form.VisibleFields, f => Assert.False(f.Enabled));
            
            Assert.Equal(1, form.EnabledChangedCalledCount);
            Assert.Equal(1, field.EnabledChangedCalledCount);
            
            form.RemoveField(field);
            form.Enabled = true;
            
            Assert.Equal(2, form.EnabledChangedCalledCount);
            Assert.Equal(1, field.EnabledChangedCalledCount);
        }

        [Fact]
        public void TestVisibleFields()
        {
            var field1 = new MockField("Field 1");
            var field2 = new MockField("Field 2");
            
            var form = Form.Create(new []
            {
                 field1, field2
            });
            
            Assert.Collection(form.VisibleFields, f => Assert.Equal("Field 1", f.Title), f => Assert.Equal("Field 2", f.Title));

            field2.Hidden = true;
            
            Assert.Collection(form.VisibleFields, f => Assert.Equal("Field 1", f.Title));

            field1.Hidden = true;
            field2.Hidden = false;
            
            Assert.Collection(form.VisibleFields, f => Assert.Equal("Field 2", f.Title));

            field1.Hidden = false;
            
            Assert.Collection(form.VisibleFields, f => Assert.Equal("Field 1", f.Title), f => Assert.Equal("Field 2", f.Title));
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