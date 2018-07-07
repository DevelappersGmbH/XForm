using System;

namespace XForm.Forms
{
    public abstract class Form
    {
        protected static Func<Form> FormCreateFunc;

        public static Form Create()
        {
            if (FormCreateFunc == null)
                throw new ArgumentException("No platform form class registered.");
            
            return FormCreateFunc();
        }
    }
}