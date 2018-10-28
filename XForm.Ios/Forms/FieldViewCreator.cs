using System;
using System.Collections.Generic;
using Foundation;
using UIKit;
using XForm.Ios.FieldViews.Bases;

namespace XForm.Ios.Forms
{
    internal class FieldViewCreator : XForm.Forms.FieldViewCreator
    {
        private readonly UITableView _tableView;
        private readonly List<string> _registeredTypes = new List<string>(); 

        public FieldViewCreator(UITableView tableView)
        {
            _tableView = tableView;
        }

        public FieldView CreateOrGetFieldView(Type fieldViewType, NSIndexPath indexPath)
        {
            RegisterTypeIfNeeded(fieldViewType);
            return DequeueType(fieldViewType, indexPath);
        }

        private void RegisterTypeIfNeeded(Type fieldViewType)
        {
            if (_registeredTypes.Contains(KeyForType(fieldViewType)))
                return;
            
            var nib = NibFromType(fieldViewType); // TODO: Check if can be removed

            // Has static property nib?
            if (nib != null)
            {
                // Register nib
                _tableView.RegisterNibForCellReuse(nib, KeyForType(fieldViewType));
            }
            else
            {
                // Register class
                _tableView.RegisterClassForCellReuse(fieldViewType, KeyForType(fieldViewType));
            }
            
            _registeredTypes.Add(KeyForType(fieldViewType));
        }

        private FieldView DequeueType(Type fieldViewType, NSIndexPath indexPath)
        {
            return (FieldView) _tableView.DequeueReusableCell(KeyForType(fieldViewType), indexPath);
        }

        private static string KeyForType(Type fieldViewType)
        {
            return fieldViewType.FullName;
        }

        private static UINib NibFromType(Type fieldViewType)
        {
            var nibProperty = fieldViewType.GetField("Nib");

            if (nibProperty == null)
                return null;

            return (UINib) nibProperty.GetValue(null);
        }
    }
}