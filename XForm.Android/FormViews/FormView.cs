using System;
using Android.Content;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using XForm.Android.Adapters;
using XForm.Forms;

namespace XForm.Android.FormViews
{
    [Register("xform.android.formviews.FormView")]
    public class FormView: RecyclerView
    {
        private Form _form;
        
        private LinearLayoutManager _layoutManager;
        private FormAdapter _adapter;
        private FieldViewLocator _fieldViewLocator;

        protected FormView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public FormView(Context context) : base(context)
        {
            Initialize(context);
        }

        public FormView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize(context);
        }

        public FormView(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {
            Initialize(context);
        }
        
        public Form Form
        {
            get => _form;
            set
            {
                _form = value;
                _adapter.Fields = value?.Fields;
            }
        }

        public FieldViewLocator FieldViewLocator
        {
            get => _fieldViewLocator ?? Form?.FieldViewLocator;
            set => _fieldViewLocator = value;
        }

        private void Initialize(Context context)
        {
            _layoutManager = new LinearLayoutManager(context);
            SetLayoutManager(_layoutManager);
            
            _adapter = new FormAdapter(this);
            SetAdapter(_adapter);
        }
    }
}