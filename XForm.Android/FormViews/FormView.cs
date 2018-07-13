using System;
using Android.Content;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using XForm.Forms;

namespace XForm.Android.FormViews
{
    [Register("xform.android.formviews.FormView")]
    public class FormView: RecyclerView
    {
        private Form _form;
        
        private LinearLayoutManager _layoutManager;
        private Adapters.Adapter _adapter;

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

        private void Initialize(Context context)
        {
            _layoutManager = new LinearLayoutManager(context);
            SetLayoutManager(_layoutManager);
            
            _adapter = new Adapters.Adapter();
            SetAdapter(_adapter);
        }
    }
}