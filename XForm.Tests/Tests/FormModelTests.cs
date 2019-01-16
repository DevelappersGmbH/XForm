using System;
using System.Collections.Generic;
using System.Windows.Input;
using XForm.Fields;
using XForm.Fields.Bases;
using XForm.Forms;
using XForm.Tests.Helpers;
using XForm.Tests.Mocks;
using Xunit;

namespace XForm.Tests.Tests
{
    public class FormModelTests
    {
        public FormModelTests()
        {
            MockForm.Register();
        }

        private class FormModel : Forms.FormModel
        {
            private string _text2;

            [LabelField("Text")]
            public string Text { get; set; } = "Test";

            [LabelField("Text 2")]
            public string Text2
            {
                get => _text2;
                set => Set(ref _text2, value);
            }
        }

        [Fact]
        public void TestCreateFormFormValidFormModel()
        {
            var formModel = new FormModel();
            var form = Form.Create(formModel);

            Assert.NotNull(form.VisibleFields);
            Assert.NotEmpty(form.VisibleFields);

            Assert.Collection(form.VisibleFields,
                              field => Helper.AssertLabelView(field, "Text", "Test"),
                              field => Helper.AssertLabelView(field, "Text 2", null));
        }

        private class AllFieldsFormModel : Forms.FormModel
        {
            private double _decimal = 1;
            private string _email = "text@text";
            private string _label = "text";
            private int _number = 1;
            private string _option = "Option 1";
            private string _password = "1234";
            private string _text = "text";

            public AllFieldsFormModel(ICommand command)
            {
                Command = command;
            }

            [ButtonField("Command")]
            public ICommand Command { get; }

            [DecimalInputField("Decimal")]
            public double Decimal
            {
                get => _decimal;
                set => Set(ref _decimal, value);
            }

            [EmailAddressTextField("Email")]
            public string Email
            {
                get => _email;
                set => Set(ref _email, value);
            }

            [LabelField("Label")]
            public string Label
            {
                get => _label;
                set => Set(ref _label, value);
            }

            [NumberInputField("Number")]
            public int Number
            {
                get => _number;
                set => Set(ref _number, value);
            }

            [OptionPickerField("Picker")]
            public string Option
            {
                get => _option;
                set => Set(ref _option, value);
            }

            [PasswordTextField("Password")]
            public string Password
            {
                get => _password;
                set => Set(ref _password, value);
            }

            [SingleLineTextField("Text")]
            public string Text
            {
                get => _text;
                set => Set(ref _text, value);
            }

            public override IList<object> GetOptionsForField(string propertyName)
            {
                if (Equals(nameof(Option), propertyName))
                    return new object[] {"Option 1", "Option 2"};

                return null;
            }
        }

        [Fact]
        public void TestFieldTypes()
        {
            var command = new MockCommand();

            var formModel = new AllFieldsFormModel(command);
            var form = Form.Create(formModel);

            Assert.Collection(form.VisibleFields,
                              field => Helper.AssertValueField<ButtonField, ICommand>(field, "Command", command),
                              field => Helper.AssertValueField<DecimalInputField, double?>(field, "Decimal", 1),
                              field => Helper.AssertValueField<EmailAddressTextField, string>(field, "Email", "text@text"),
                              field => Helper.AssertValueField<LabelField, string>(field, "Label", "text"),
                              field => Helper.AssertValueField<NumberInputField, int?>(field, "Number", 1),
                              field => Helper.AssertValueField<OptionPickerField<string>, int?>(field, "Picker", 0),
                              field => Helper.AssertValueField<PasswordTextField, string>(field, "Password", "1234"),
                              field => Helper.AssertValueField<SingleLineTextField, string>(field, "Text", "text"));

            formModel.Decimal = 2.5;
            formModel.Email = "email@email";
            formModel.Label = "text 2";
            formModel.Number = 2;
            formModel.Option = "Option 2";
            formModel.Password = "kkkkkk";
            formModel.Text = "text 2";

            Assert.Collection(form.VisibleFields,
                              field => Helper.AssertValueField<ButtonField, ICommand>(field, "Command", command),
                              field => Helper.AssertValueField<DecimalInputField, double?>(field, "Decimal", 2.5),
                              field => Helper.AssertValueField<EmailAddressTextField, string>(field, "Email", "email@email"),
                              field => Helper.AssertValueField<LabelField, string>(field, "Label", "text 2"),
                              field => Helper.AssertValueField<NumberInputField, int?>(field, "Number", 2),
                              field => Helper.AssertValueField<OptionPickerField<string>, int?>(field, "Picker", 1),
                              field => Helper.AssertValueField<PasswordTextField, string>(field, "Password", "kkkkkk"),
                              field => Helper.AssertValueField<SingleLineTextField, string>(field, "Text", "text 2"));

            ((ValueField<double?>) form.VisibleFields[1]).Value = 3.5;
            ((ValueField<string>) form.VisibleFields[2]).Value = "test@email";
            ((ValueField<string>) form.VisibleFields[3]).Value = "text 3";
            ((ValueField<int?>) form.VisibleFields[4]).Value = 3;
            ((ValueField<int?>) form.VisibleFields[5]).Value = 0;
            ((ValueField<string>) form.VisibleFields[6]).Value = "123456";
            ((ValueField<string>) form.VisibleFields[7]).Value = "text 3";

            Assert.Equal(3.5, formModel.Decimal);
            Assert.Equal("test@email", formModel.Email);
            Assert.Equal("text 3", formModel.Label);
            Assert.Equal(3, formModel.Number);
            Assert.Equal("Option 1", formModel.Option);
            Assert.Equal("123456", formModel.Password);
            Assert.Equal("text 3", formModel.Text);
        }

        private class MissingOptionsFormModel : FormModel
        {
            [OptionPickerField("Picker")]
            public string Option { get; set; }
        }
        
        private class WrongTypeOptionsFormModel : FormModel
        {
            [OptionPickerField("Picker")]
            public int Option { get; set; }
            
            public override IList<object> GetOptionsForField(string propertyName)
            {
                if (Equals(nameof(Option), propertyName))
                    return new object[] {1, "Option 2"};

                return null;
            }
        }

        private class WellDefinedOptionsFormModel : FormModel
        {
            [OptionPickerField("Picker")]
            public string Option { get; set; }
            
            public override IList<object> GetOptionsForField(string propertyName)
            {
                if (Equals(nameof(Option), propertyName))
                    return new object[] {"Option 1", "Option 2"};

                return null;
            }
        }

        [Fact]
        public void TestOptionsFields()
        {
            Assert.NotNull(Form.Create(new WellDefinedOptionsFormModel()));
            Assert.Throws<ArgumentException>(() => Form.Create(new MissingOptionsFormModel()));
            Assert.Throws<ArgumentException>(() => Form.Create(new WrongTypeOptionsFormModel()));
        }
    }
}