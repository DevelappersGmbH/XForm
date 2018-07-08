using XForm.Binding;
using XForm.Fields.Interfaces;
using XForm.Forms;

namespace XForm.Fields.Bases
{
    public abstract class Field: BindableBase, IField
    {
        private bool _enabled = true;
        private string _title;

        protected Field(string title)
        {
            Title = title;
        }

        public Form Form { get; set; }

        public bool Enabled
        {
            get => _enabled;
            set => Set(ref _enabled, value);
        }

        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }
    }
}