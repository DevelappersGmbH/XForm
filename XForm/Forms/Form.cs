using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using XForm.Fields.Interfaces;

namespace XForm.Forms
{
    public abstract class Form
    {
        protected static Func<Form> FormCreateFunc;

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

        private void SetFields(IEnumerable<IField> fields)
        {
            var fieldList = fields?.ToList() ?? new List<IField>();

            foreach (var field in fieldList)
            {
                field.Form = this;
            }

            Fields = new ObservableCollection<IField>(fieldList);
        }

        protected virtual void RegisterFieldViews(FieldViewLocator locator)
        {
        }
    }
}