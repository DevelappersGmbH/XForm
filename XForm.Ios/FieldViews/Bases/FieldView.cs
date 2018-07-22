using System;
using System.ComponentModel;
using UIKit;
using XForm.Fields.Bases;
using XForm.Fields.Interfaces;
using XForm.FieldViews;
using XForm.Ios.ContentViews.Interfaces;

namespace XForm.Ios.FieldViews.Bases
{
    public abstract class FieldView : UITableViewCell, IFieldView
    {
        protected FieldView(IntPtr handle) : base(handle)
        {
        }
        
        public abstract void BindTo(IField field);
        
        public event EventHandler ContentHeightChanged;

        public void NotifyContentHeightChanged()
        {
            ContentHeightChanged?.Invoke(this, new EventArgs());
        }
    }
    
    public abstract class FieldView<TFieldContent> : FieldView 
        where TFieldContent : IFieldContent
    {
        protected FieldView(IntPtr handle, Func<TFieldContent> createContent) : base(handle)
        {
            Content = createContent();
            
            Setup();
        }

        private void Setup()
        {
            var view = Content.ContentView;
            
            view.PreservesSuperviewLayoutMargins = true;
            view.TranslatesAutoresizingMaskIntoConstraints = false;
            
            AddSubview(view);
            
            NSLayoutConstraint.ActivateConstraints(new[]
            {
                LeftAnchor.ConstraintEqualTo(view.LeftAnchor),
                RightAnchor.ConstraintEqualTo(view.RightAnchor),
                TopAnchor.ConstraintEqualTo(view.TopAnchor),
                BottomAnchor.ConstraintEqualTo(view.BottomAnchor)
            });
            
            SelectionStyle = UITableViewCellSelectionStyle.None;  
        }

        protected TFieldContent Content { get; }

        public sealed override void AwakeFromNib()
        {
            base.AwakeFromNib();
        }
    }
    
    public abstract class FieldView<TField, TFieldContent> : FieldView<TFieldContent>
        where TField : Field
        where TFieldContent : IFieldContent
    {
        protected FieldView(IntPtr handle, Func<TFieldContent> createContent) : base(handle, createContent)
        {
        }

        ~FieldView()
        {
            if (Field != null)
                Field.PropertyChanged -= FieldPropertyChanged;
        }

        public TField Field { get; private set; }

        public sealed override void BindTo(IField field)
        {
            BindTo((TField) field);
        }

        protected virtual void BindTo(TField field)
        {
            if (Equals(Field, field))
                return;

            if (Field != null) 
                Field.PropertyChanged -= FieldPropertyChanged;
            
            Field = field;

            if (Field == null) 
                return;
            
            Field.PropertyChanged += FieldPropertyChanged;
            TitleChanged(Field?.Title);
            EnabledChanged(Field.Enabled);
        }

        private void FieldPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            FieldPropertyChanged(e.PropertyName);
        }

        protected virtual void FieldPropertyChanged(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(Field.Title):
                    TitleChanged(Field.Title);
                    break;
                case nameof(Field.Enabled):
                    EnabledChanged(Field.Enabled);
                    break;
            }
        }

        protected virtual void TitleChanged(string value)
        {
        }

        protected virtual void EnabledChanged(bool value)
        {
        }
    }
}