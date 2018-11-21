using System;
using Android.Content;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using XForm.Android.Adapters;
using XForm.Android.FieldViews.Bases;
using XForm.Android.Forms;
using XForm.FieldViews;
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
                _adapter.Fields = value?.VisibleFields;
            }
        }

        public FieldViewLocator FieldViewLocator
        {
            get => _fieldViewLocator ?? Form?.FieldViewLocator;
            set => _fieldViewLocator = value;
        }

        public FieldViewCreator FieldViewCreator { get; } = new FieldViewCreator();

        private void Initialize(Context context)
        {
            DescendantFocusability = DescendantFocusability.BeforeDescendants;
            
            _layoutManager = new LinearLayoutManager(context);
            SetLayoutManager(_layoutManager);
            
            _adapter = new FormAdapter(this);
            SetAdapter(_adapter);
        }

        public void CreateView<TFieldView>(Func<ViewGroup, FieldView> viewCreator) where TFieldView : IFieldView
        {
            FieldViewCreator.RegisterCustomCreator<TFieldView>(viewCreator);
        }
    }
}