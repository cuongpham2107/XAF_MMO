using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Blazor.SystemModule;
using DevExpress.ExpressApp.SystemModule;
using DXApplication.Module.Extension;

namespace DXApplication.Blazor.Server.Controllers;

//TODO: dành cho view cụ thể (có View ID)
//public class BlazorViewInlineController : BlazorInlineController {
//    public BlazorViewInlineController() {
//        TargetViewId = "";
//    }
//}

/// <summary>
/// TODO: Tạo nested list view inline
/// </summary>
public class BlazorNestedInlineController : ViewController<ListView> {
    public BlazorNestedInlineController() {
        TargetObjectType = typeof(INestedListViewInline);
        TargetViewNesting = Nesting.Nested;
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
            if (View.Editor is DxGridListEditor gridListEditor) {
                var grid = gridListEditor.GetGridAdapter().GridInstance;
                if (grid != null) {
                    await grid.StartEditDataItemAsync(View.CurrentObject);
                }
            }
        }
    }

    private async void NewObjectAction_Executing(object sender, System.ComponentModel.CancelEventArgs e) {
        if (View.AllowEdit) {
            e.Cancel = true;
            if (View.Editor is DxGridListEditor gridListEditor) {
                var grid = gridListEditor.GetGridAdapter().GridInstance;
                if (grid != null) {
                    await grid.StartEditNewRowAsync();
                }
            }
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

/// <summary>
/// TODO: Tạo view inline hoàn toàn
/// </summary>
public class BlazorInlineController : ViewController<ListView> {

    public BlazorInlineController() {
        TargetObjectType = typeof(IListViewInline);
        TargetViewNesting = Nesting.Root;
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
            if (View.Editor is DxGridListEditor gridListEditor) {
                var grid = gridListEditor.GetGridAdapter().GridInstance;
                if (grid != null) {
                    await grid.StartEditDataItemAsync(View.CurrentObject);
                }
            }
        }
    }

    private async void NewObjectAction_Executing(object sender, System.ComponentModel.CancelEventArgs e) {
        if (View.AllowEdit) {
            e.Cancel = true;
            if (View.Editor is DxGridListEditor gridListEditor) {
                var grid = gridListEditor.GetGridAdapter().GridInstance;
                if (grid != null) {
                    await grid.StartEditNewRowAsync();
                }
            }
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
