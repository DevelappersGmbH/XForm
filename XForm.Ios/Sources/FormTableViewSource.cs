using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Foundation;
using UIKit;
using XForm.EventSubscription;
using XForm.Fields.Interfaces;
using XForm.Ios.FieldViews.Bases;
using XForm.Ios.Forms;
using XForm.Ios.FormViews;

namespace XForm.Ios.Sources
{
    internal class FormTableViewSource: UITableViewSource {
        private readonly FormView _formView;
        private readonly FieldViewCreator _fieldViewCreator;

        private IDisposable _fieldsCollectionChangedSubscription;
        
        private ObservableCollection<IField> _fields;

        public FormTableViewSource(FormView formView)
        {
            _formView = formView;
            _fieldViewCreator = new FieldViewCreator(formView);
        }

        public ObservableCollection<IField> Fields
        {
            get => _fields;
            set
            {
                if (Equals(_fields, value))
                    return;

                _fields = value;

                _fieldsCollectionChangedSubscription?.Dispose();
                _fieldsCollectionChangedSubscription = _fields?.WeakSubscribe(FieldsCollectionChanged);
                
                FieldsCollectionChanged(this, null);
            }
        }

        #region Data source

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return Fields?.Count ?? 0;
        }
        
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            return CreateOrGetFieldView(indexPath, FieldAt(indexPath));
        }

        #endregion

        #region Event handlers

        private void FieldsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            _formView.ReloadData();
        }

        #endregion

        #region Helpers

        private IField FieldAt(NSIndexPath indexPath)
        {
            return Fields[indexPath.Row];
        }
        
        private FieldView CreateOrGetFieldView(NSIndexPath indexPath, IField field)
        {
            var viewType = _formView.FieldViewLocator.ViewTypeForField(field);
            var view = _fieldViewCreator.CreateOrGetFieldView(viewType, indexPath);

            view.ContentHeightChanged -= ViewContentHeightChanged;
            
            view.BindTo(field);

            view.ContentHeightChanged += ViewContentHeightChanged;

            return view;
        }

        private void ViewContentHeightChanged(object sender, EventArgs e)
        {
            _formView.BeginUpdates();
            _formView.EndUpdates();
        }

        #endregion
    }
}