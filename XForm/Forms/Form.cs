using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using XForm.Binding;
using XForm.EventSubscription;
using XForm.Fields.Interfaces;

namespace XForm.Forms
{
    public abstract class Form : BindableBase
    {
        protected static Func<Form> FormCreateFunc;

        private bool _enabled = true;

        private List<IField> _fields;
        private Dictionary<IField, IDisposable> _fieldHiddenChangedSubscriptions = new Dictionary<IField, IDisposable>();

        public static Form Create(IEnumerable<IField> fields = null)
        {
            if (FormCreateFunc == null)
                throw new ArgumentException("No platform form class registered.");

            var form = FormCreateFunc();

            form.RegisterFieldViews(form.FieldViewLocator);
            form.SetFields(fields);        
            form.UpdateVisibleFields();

            return form;
        }

        protected Form()
        {
            FieldViewLocator = new FieldViewLocator();

            _fields = new List<IField>();
            VisibleFields = new ObservableCollection<IField>();
        }

        public ObservableCollection<IField> VisibleFields { get; private set; }

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

            _fields = new List<IField>(fieldList);
            
            // Subscribe to field's hidden changed
            _fieldHiddenChangedSubscriptions = _fields.ToDictionary(f => f, f => (IDisposable) f.WeakSubscribe(nameof(f.Hidden), FieldHiddenChanged));
        }

        public void InsertField(int index, IField field)
        {
            if (field.Form != null)
                throw new ArgumentException("Field can be only part of one form");

            _fieldHiddenChangedSubscriptions[field] = field.WeakSubscribe(nameof(field.Hidden), FieldHiddenChanged);

            field.Form = this;
            _fields.Insert(index, field);
            UpdateVisibleFields(field);
        }

        public void RemoveField(IField field)
        {
            if (field.Form != this || !_fields.Remove(field))
                throw new ArgumentException("Field is not part of this form");

            _fieldHiddenChangedSubscriptions[field] = null;

            field.Form = null;
            UpdateVisibleFields(field);
        }

        #endregion

        protected virtual void RegisterFieldViews(FieldViewLocator locator)
        {
        }

        private void FieldHiddenChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateVisibleFields((IField) sender);
        }

        private void UpdateVisibleFields(IField field = null)
        {
            if (field == null)
            {
                VisibleFields = new ObservableCollection<IField>(_fields.Where(f => !f.Hidden));
                return;
            }

            if (field.Hidden) // Just remove
                VisibleFields.Remove(field);
            else
            {
                var visibleFields = _fields.Where(f => !f.Hidden).ToList();

                if (visibleFields.Contains(field))
                    VisibleFields.Insert(visibleFields.IndexOf(field), field);
                else
                    VisibleFields.Remove(field);
            }
        }
    }
}