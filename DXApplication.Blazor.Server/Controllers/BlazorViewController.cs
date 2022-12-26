using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Blazor.SystemModule;
using DevExpress.ExpressApp.SystemModule;
using DXApplication.Module.Extension;

namespace DXApplication.Blazor.Server.Controllers;
public class BlazorListViewController : ViewController<ListView> {

    private NewObjectViewController _newController;
    private ListViewProcessCurrentObjectController _currentObjectController;

    protected override void OnViewControlsCreated() {
        base.OnViewControlsCreated();

        //TODO: điều chỉnh cách hiển thị của DxGridListEditor
        if (View.Editor is DxGridListEditor gridListEditor) {
            IDxGridAdapter dataGridAdapter = gridListEditor.GetGridAdapter();
            dataGridAdapter.GridModel.ColumnResizeMode = DevExpress.Blazor.GridColumnResizeMode.NextColumn;
            dataGridAdapter.GridModel.ShowGroupPanel = true;
            dataGridAdapter.GridModel.FooterDisplayMode = DevExpress.Blazor.GridFooterDisplayMode.Auto;
            dataGridAdapter.GridModel.AutoExpandAllGroupRows = true;
            dataGridAdapter.GridModel.ShowFilterRow = true;
            dataGridAdapter.GridModel.ShowSearchBox = true;
            dataGridAdapter.GridModel.EditNewRowPosition = DevExpress.Blazor.GridEditNewRowPosition.Top;
            dataGridAdapter.GridCommandColumnModel.Visible = true;
            dataGridAdapter.GridCommandColumnModel.ShowInColumnChooser = true;
            dataGridAdapter.GridSelectionColumnModel.ShowInColumnChooser = true;
            dataGridAdapter.GridSelectionColumnModel.AllowSelectAll = true;
        }
    }

    protected override void OnActivated() {
        base.OnActivated();

        _newController = Frame.GetController<NewObjectViewController>();
        _currentObjectController = Frame.GetController<ListViewProcessCurrentObjectController>();

        //TODO: nút New mở form inline thay vì mở detail view
        if (View.AllowEdit == true && 
            (
            (View.ObjectTypeInfo.Type.IsAssignableTo(typeof(IListViewNewActionInline)) && View.IsRoot) ||
            (View.ObjectTypeInfo.Type.IsAssignableTo(typeof(INestedListViewNewActionInline)) && !View.IsRoot)
            ))
            _newController.NewObjectAction.Executing += NewObjectAction_Executing;

        if (View.AllowEdit == true && (
            (View.ObjectTypeInfo.Type.IsAssignableTo(typeof(IListViewEditActionInline)) && View.IsRoot) ||
            (View.ObjectTypeInfo.Type.IsAssignableTo(typeof(INestedListViewEditActionInline)) && !View.IsRoot)
            ))
            _currentObjectController.ProcessCurrentObjectAction.Executing += ProcessCurrentObjectAction_Executing;

        //TODO: đặt InlineEditMode cho root list view
        if (View.Editor.Model is IModelListViewBlazor model && View.AllowEdit && View.IsRoot) {
            if (View.ObjectTypeInfo.Type.IsAssignableTo(typeof(IListViewFormEdit)))
                model.InlineEditMode = InlineEditMode.EditForm;
            else if (View.ObjectTypeInfo.Type.IsAssignableTo(typeof(IListViewInlineEdit)))
                model.InlineEditMode = InlineEditMode.Inline;
            else if (View.ObjectTypeInfo.Type.IsAssignableTo(typeof(IListViewPopupEdit)))
                model.InlineEditMode = InlineEditMode.PopupEditForm;
            else model.InlineEditMode = InlineEditMode.EditForm;
        }

        //TODO: đặt InlineEditMode cho nested list view
        if (View.Editor.Model is IModelListViewBlazor modelnested && View.AllowEdit && !View.IsRoot) {
            if (View.ObjectTypeInfo.Type.IsAssignableTo(typeof(INestedListViewFormEdit)))
                modelnested.InlineEditMode = InlineEditMode.EditForm;
            else if (View.ObjectTypeInfo.Type.IsAssignableTo(typeof(INestedListViewInlineEdit)))
                modelnested.InlineEditMode = InlineEditMode.Inline;
            else if (View.ObjectTypeInfo.Type.IsAssignableTo(typeof(INestedListViewPopupEdit)))
                modelnested.InlineEditMode = InlineEditMode.PopupEditForm;
            else modelnested.InlineEditMode = InlineEditMode.EditForm;
        }
    }

    private void ProcessCurrentObjectAction_Executing(object sender, System.ComponentModel.CancelEventArgs e) {
        e.Cancel = true;
        var gridListEditor = View.Editor as DxGridListEditor;
        var grid = gridListEditor.GetGridAdapter().GridInstance;
        grid.StartEditDataItemAsync(View.CurrentObject);
    }

    private async void NewObjectAction_Executing(object sender, System.ComponentModel.CancelEventArgs e) {
        if (View.AllowEdit) {
            e.Cancel = true;
            var gridListEditor = View.Editor as DxGridListEditor;
            var grid = gridListEditor.GetGridAdapter().GridInstance;
            await grid?.StartEditNewRowAsync();
        }
    }

    protected override void OnDeactivated() {
        if (_newController != null) {
            _newController.NewObjectAction.Executing -= NewObjectAction_Executing;
            _currentObjectController.ProcessCurrentObjectAction.Executing -= ProcessCurrentObjectAction_Executing;
        }
        base.OnDeactivated();
    }
}
