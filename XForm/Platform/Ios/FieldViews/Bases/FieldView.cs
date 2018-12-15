using System;
using System.ComponentModel;
using System.Linq;
using UIKit;
using XForm.EventSubscription;
using XForm.Fields.Interfaces;
using XForm.FieldViews;
using XForm.Ios.ContentViews.Bases;
using XForm.Ios.ContentViews.Interfaces;

namespace XForm.Ios.FieldViews.Bases
{
    public abstract class FieldView : UITableViewCell, IFieldView
    {
        protected FieldView(IntPtr handle) : base(handle)
        {
        }

        public event EventHandler ContentHeightChanged;
        
        internal abstract Type ContentType { get; }
        
        internal bool NeedsSetup()
        {
            return !ContentView.Subviews.Any();
        }

        internal abstract void CreateFieldContent(Func<FieldContent> customFieldContentCreator = null);

        internal virtual void Setup()
        {
        }
        
        public abstract void BindTo(IField field);

        protected void NotifyContentHeightChanged()
        {
            ContentHeightChanged?.Invoke(this, new EventArgs());
        }
    }
    
    public abstract class FieldView<TFieldContent> : FieldView 
        where TFieldContent : class, IFieldContent
    {
        protected FieldView(IntPtr handle) : base(handle)
        {
        }

        internal abstract Func<TFieldContent> DefaultContentCreator { get; }

        internal override Type ContentType => typeof(TFieldContent);
        
        internal TFieldContent Content { get; set; }

        internal override void CreateFieldContent(Func<FieldContent> customFieldContentCreator = null)
        {
            if (customFieldContentCreator != null)
                Content = customFieldContentCreator() as TFieldContent;
            else
                Content = DefaultContentCreator();
            
            
            if (Content == null)
                throw new ArgumentException("Could not create content");
        }

        internal override void Setup()
        {
            base.Setup();

            var view = Content.ContentView;
            
            view.PreservesSuperviewLayoutMargins = true;
            view.TranslatesAutoresizingMaskIntoConstraints = false;
            
            ContentView.AddSubview(view);

            var bottomConstraint = ContentView.BottomAnchor.ConstraintEqualTo(view.BottomAnchor);

            bottomConstraint.Priority = 999;
            
            NSLayoutConstraint.ActivateConstraints(new[]
            {
                ContentView.LeftAnchor.ConstraintEqualTo(view.LeftAnchor),
                ContentView.RightAnchor.ConstraintEqualTo(view.RightAnchor),
                ContentView.TopAnchor.ConstraintEqualTo(view.TopAnchor),
                bottomConstraint
            });
            
            SelectionStyle = UITableViewCellSelectionStyle.None;  
        }

        public sealed override void AwakeFromNib()
        {
            base.AwakeFromNib();
        }
    }
    
    public abstract class FieldView<TField, TFieldContent> : FieldView<TFieldContent>
        where TField : class, IField
        where TFieldContent : class, IFieldContent
    {
        private IDisposable _fieldPropertyChangedSubscription;
        
        protected FieldView(IntPtr handle) : base(handle)
        {
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
            
            Field = field;

            _fieldPropertyChangedSubscription?.Dispose();
            _fieldPropertyChangedSubscription = Field?.WeakSubscribe(FieldPropertyChanged);
            
            if (Field != null)
            {
                TitleChanged(Field.Title);
                EnabledChanged(Field.Enabled);
            }
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