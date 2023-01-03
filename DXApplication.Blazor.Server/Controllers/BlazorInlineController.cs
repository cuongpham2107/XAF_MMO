using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Blazor.SystemModule;
using DevExpress.ExpressApp.SystemModule;
using DXApplication.Module.Extension;

namespace DXApplication.Module.Server.Controllers;

/// <summary>
/// TODO: Tạo view inline hoàn toàn
/// </summary>
public class BlazorInlineController : ViewController<ListView> {

    public BlazorInlineController() {
        TargetObjectType = typeof(IListViewInline);
    }

    private NewObjectViewController _newController;
    private ListViewProcessCurrentObjectController _currentObjectController;

    protected override void OnActivated() {
        base.OnActivated();

        //TODO: đặt InlineEditMode cho list view
        if (View.Editor.Model is IModelListViewBlazor model && View.AllowEdit) {
            model.InlineEditMode = InlineEditMode.EditForm;

            _newController = Frame.GetController<NewObjectViewController>();
            _currentObjectController = Frame.GetController<ListViewProcessCurrentObjectController>();

            _newController.NewObjectAction.Executing += NewObjectAction_Executing;
            _currentObjectController.ProcessCurrentObjectAction.Executing += ProcessCurrentObjectAction_Executing;
        }
    }

    private async void ProcessCurrentObjectAction_Executing(object sender, System.ComponentModel.CancelEventArgs e) {
        if (View.AllowEdit) {
            e.Cancel = true;
            var gridListEditor = View.Editor as DxGridListEditor;
            var grid = gridListEditor.GetGridAdapter().GridInstance;
            await grid?.StartEditDataItemAsync(View.CurrentObject);
        }
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
        if (_newController != null)
            _newController.NewObjectAction.Executing -= NewObjectAction_Executing;
        if (_currentObjectController != null)
            _currentObjectController.ProcessCurrentObjectAction.Executing -= ProcessCurrentObjectAction_Executing;
        base.OnDeactivated();
    }
}
