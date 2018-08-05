using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using XForm.Binding;
using XForm.Fields.Interfaces;

namespace XForm.Forms
{
    public abstract class Form : BindableBase
    {
        protected static Func<Form> FormCreateFunc;
        private bool _enabled = true;

        public static Form Create(IEnumerable<IField> fields = null)
        {
            if (FormCreateFunc == null)
                throw new ArgumentException("No platform form class registered.");

            var form = FormCreateFunc();

            form.RegisterFieldViews(form.FieldViewLocator);
            form.SetFields(fields);

            return form;
        }

        protected Form()
        {
            FieldViewLocator = new FieldViewLocator();
        }

        public ObservableCollection<IField> Fields { get; private set; }

        public FieldViewLocator FieldViewLocator { get; }

        public bool Enabled
        {
            get => _enabled;
            set => Set(ref _enabled, value);
        }

        #region Manipulate fields

        private void SetFields(IEnumerable<IField> fields)
        {
            var fieldList = fields?.ToList() ?? new List<IField>();

            foreach (var field in fieldList)
            {
                field.Form = this;
            }

            Fields = new ObservableCollection<IField>(fieldList);
        }

        public void InsertField(int index, IField field)
        {
            if (field.Form != null)
                throw new ArgumentException("Field can be only part of one form");
            
            field.Form = this;
            Fields.Insert(index, field);
        }

        public void RemovedField(IField field)
        {
            if (field.Form != this || !Fields.Remove(field))
                throw new ArgumentException("Field is not part of this form");
            
            field.Form = null;
        }

        #endregion

        protected virtual void RegisterFieldViews(FieldViewLocator locator)
        {
        }
    }
}