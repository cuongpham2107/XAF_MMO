using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Templates;
using System;
using System.Linq;

namespace DXApplication.Module.Extension {
    public class MypeViewController : ViewController {

        /// <summary>
        /// Enum for 3 types of action
        /// </summary>
        public enum ActionType { Simple, SingleChoice, Popup }

        /// <summary>
        /// Create one of 3 types of action
        /// </summary>
        /// <param name="actionType"></param>
        /// <param name="caption"></param>
        /// <param name="category"></param>
        /// <param name="image"></param>
        /// <param name="toolTip"></param>
        /// <param name="confirmation"></param>
        /// <param name="objectType"></param>
        /// <param name="criteria"></param>
        /// <param name="criteriaMode"></param>
        /// <param name="selectionType"></param>
        /// <param name="viewType"></param>
        /// <param name="nesting"></param>
        /// <param name="paintStyle"></param>
        /// <returns></returns>
        protected ActionBase CreateAction(
        ActionType actionType,
        string caption,
        string category = "Edit",
        string image = Image.Action_SimpleAction,
        string toolTip = "",
        string confirmation = "",
        Type objectType = null,
        string criteria = "",
        TargetObjectsCriteriaMode criteriaMode = TargetObjectsCriteriaMode.TrueForAll,
        SelectionDependencyType selectionType = SelectionDependencyType.Independent,
        ViewType viewType = ViewType.Any,
        Nesting nesting = Nesting.Any,
        ActionItemPaintStyle paintStyle = ActionItemPaintStyle.Default) {

            var id = $"{GetType()}-{caption}";
            ActionBase action = actionType switch {
                ActionType.Simple => new SimpleAction(this, id, category),
                ActionType.SingleChoice => new SingleChoiceAction(this, id, category),
                ActionType.Popup => new PopupWindowShowAction(this, id, category),
                _ => new SimpleAction(this, id, category)
            };

            action.Caption = caption;
            action.ImageName = image;
            action.TargetObjectsCriteriaMode = criteriaMode;
            action.SelectionDependencyType = selectionType;
            action.TargetViewNesting = nesting;
            action.TargetViewType = viewType;
            action.PaintStyle = paintStyle;
            if (!string.IsNullOrEmpty(criteria)) action.TargetObjectsCriteria = criteria;
            if (!string.IsNullOrEmpty(toolTip)) action.ToolTip = toolTip;
            if (!string.IsNullOrEmpty(confirmation)) action.ConfirmationMessage = confirmation;
            if (objectType != null) action.TargetObjectType = objectType;

            return action;
        }

        /// <summary>
        /// Add items to single choice action
        /// </summary>
        /// <param name="action"></param>
        /// <param name="items"></param>
        protected static void SetSingleChoiceAction(SingleChoiceAction action,
            ChoiceActionItem[] items) {
            action.Items.AddRange(items);
            action.ItemType = SingleChoiceActionItemType.ItemIsOperation;
            action.ShowItemsOnClick = true;
        }

        /// <summary>
        /// Create detail view from object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        protected DetailView CreateDetailView<T>(T item) where T : class {
            var os = Application.CreateObjectSpace(typeof(T));
            var obj = os.GetObject(item);
            return Application.CreateDetailView(os, obj);
        }

        /// <summary>
        /// Create detail view of domain component (picker) from object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        protected DetailView CreatePickerDetailView<T>(T item) where T : class {
            var npos = (NonPersistentObjectSpace)Application.CreateObjectSpace(typeof(T));
            return Application.CreateDetailView(npos, item);
        }

        /// <summary>
        /// Create detail view of domain component
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected DetailView CreatePickerDetailView<T>() where T : class {
            var npos = (NonPersistentObjectSpace)Application.CreateObjectSpace(typeof(T));
            var item = Activator.CreateInstance(typeof(T));
            return Application.CreateDetailView(npos, item);
        }

        /// <summary>
        /// Create lookup list view (and return source)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">Returned source to set, etc., filters</param>
        /// <returns></returns>
        protected ListView CreateLookupListView<T>(out CollectionSourceBase source) where T : class {
            var os = Application.CreateObjectSpace(typeof(T));
            var viewId = Application.FindLookupListViewId(typeof(T));
            source = Application.CreateCollectionSource(os, typeof(T), viewId);
            return Application.CreateListView(viewId, source, true);
        }

        /// <summary>
        /// Create list view (and return source)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">Returned source to set, etc., filters</param>
        /// <returns></returns>
        protected ListView CreateListView<T>(out CollectionSourceBase source) where T : class {
            var os = Application.CreateObjectSpace(typeof(T));
            var viewId = Application.FindListViewId(typeof(T));
            source = Application.CreateCollectionSource(os, typeof(T), viewId);
            return Application.CreateListView(viewId, source, true);
        }
    }
}
