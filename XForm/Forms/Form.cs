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

            form.SetFields(fields);
            
            return form;
        }

        public ObservableCollection<IField> Fields { get; private set; }

        private void SetFields(IEnumerable<IField> fields)
        {
            var fieldList = fields?.ToList() ?? new List<IField>();
            
            foreach (var field in fieldList)
            {
                field.Form = this;
            }
            
            Fields = new ObservableCollection<IField>(fieldList);
        }
    }
}