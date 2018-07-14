using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Foundation;
using UIKit;
using XForm.Fields.Interfaces;
using XForm.Ios.FieldViews;
using XForm.Ios.FieldViews.Bases;
using XForm.Ios.Forms;
using XForm.Ios.FormViews;

namespace XForm.Ios.Sources
{
    internal class FormTableViewSource: UITableViewSource {
        private readonly FormView _formView;
        private readonly FieldViewCreator _fieldViewCreator;
        
        private ObservableCollection<IField> _fields;

        public FormTableViewSource(FormView formView)
        {
            _formView = formView;
            _fieldViewCreator = new FieldViewCreator(formView);
            
            formView.RegisterNibForCellReuse(LabelFieldView.Nib, nameof(LabelFieldView));
        }

        public ObservableCollection<IField> Fields
        {
            get => _fields;
            set
            {
                if (_fields == value)
                    return;
                
                if (_fields != null)
                    _fields.CollectionChanged -= FieldsCollectionChanged;
                
                _fields = value;
                _fields.CollectionChanged += FieldsCollectionChanged;
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
            
            view.BindTo(field);

            return view;
        }

        #endregion
    }
}