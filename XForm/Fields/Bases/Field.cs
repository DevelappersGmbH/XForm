using System.ComponentModel;
using XForm.Binding;
using XForm.Fields.Interfaces;
using XForm.Forms;

namespace XForm.Fields.Bases
{
    public abstract class Field: BindableBase, IField
    {
        private bool _enabled = true;
        private string _title;
        private Form _form;

        protected Field(string title)
        {
            Title = title;
        }

        public Form Form
        {
            get => _form;
            set
            {
                if (!Set(ref _form, value))
                    return;

                _form.PropertyChanged += FormPropertyChanged;
            }
        }

        public bool Enabled
        {
            get => Form.Enabled && _enabled;
            set => Set(ref _enabled, value);
        }

        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        private void FormPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            switch (args.PropertyName)
            {
                case nameof(Form.Enabled):
                    RaisePropertyChanged(nameof(Enabled));
                    break;
                default:
                    return;
            }
        }
    }
}