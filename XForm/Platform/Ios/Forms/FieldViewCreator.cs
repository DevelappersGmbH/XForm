using System;
using System.Collections.Generic;
using Foundation;
using UIKit;
using XForm.Helpers;
using XForm.Ios.ContentViews.Bases;
using XForm.Ios.ContentViews.Interfaces;
using XForm.Ios.FieldViews.Bases;

namespace XForm.Ios.Forms
{
    internal class FieldViewCreator
    {
        private readonly UITableView _tableView;
        private readonly List<string> _registeredTypes = new List<string>();
        private readonly TypeRegister<Func<FieldContent>> _fieldContentCreatorRegister = new TypeRegister<Func<FieldContent>>();

        public FieldViewCreator(UITableView tableView)
        {
            _tableView = tableView;
        }

        public void RegisterFieldContentCreator<TFieldContent>(Func<FieldContent> creator) where TFieldContent : IFieldContent
        {
            _fieldContentCreatorRegister.Register<TFieldContent>(creator);
        }

        public FieldView CreateOrGetFieldView(Type fieldViewType, NSIndexPath indexPath)
        {
            RegisterTypeIfNeeded(fieldViewType);
            var fieldView = DequeueType(fieldViewType, indexPath);

            if (fieldView.NeedsSetup())
            {
                CreateFieldContent(fieldView);
                fieldView.Setup();
            }

            return fieldView;
        }

        private void CreateFieldContent(FieldView fieldView)
        {
            var fieldContentType = fieldView.ContentType;

            if (_fieldContentCreatorRegister.TryValue(fieldContentType, out var customFieldContentCreator))
                fieldView.CreateFieldContent(customFieldContentCreator);
            else
                fieldView.CreateFieldContent();
        }

        private void RegisterTypeIfNeeded(Type fieldViewType)
        {
            if (_registeredTypes.Contains(KeyForType(fieldViewType)))
                return;

            _tableView.RegisterClassForCellReuse(fieldViewType, KeyForType(fieldViewType));
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
    }
}