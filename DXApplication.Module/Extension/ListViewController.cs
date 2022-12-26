using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.SystemModule;

namespace DXApplication.Module.Extension;

public class ListViewController : ViewController<ListView> {
    private FilterController _filterController;
    private NewObjectViewController _newController;
    private DeleteObjectsViewController _deleteController;
    private LinkUnlinkController _linkunlinkController;
    private ListViewProcessCurrentObjectController _currentObjectController;

    protected override void OnActivated() {
        base.OnActivated();

        _deleteController = Frame.GetController<DeleteObjectsViewController>();
        _linkunlinkController = Frame.GetController<LinkUnlinkController>();
        _currentObjectController = Frame.GetController<ListViewProcessCurrentObjectController>();
        _filterController = Frame.GetController<FilterController>();
        _newController = Frame.GetController<NewObjectViewController>();

        // mặc định ẩn full text search vì đã có searchbox của blazor grid
        _filterController?.FullTextFilterAction.Active.SetItemValue("Disable", false);

        if (View.ObjectTypeInfo.Type.IsAssignableTo(typeof(INestedListViewDisableLinkAction)))
            _linkunlinkController?.LinkAction.Active.SetItemValue("Disable", false);

        if (View.ObjectTypeInfo.Type.IsAssignableTo(typeof(INestListViewDisableUnlinkAction)))
            _linkunlinkController?.UnlinkAction.Active.SetItemValue("Disable", false);

        if (View.ObjectTypeInfo.Type.IsAssignableTo(typeof(IListViewDisableNewAction)) && View.IsRoot)
            _newController?.NewObjectAction.Active.SetItemValue("Disable", false);

        if (View.ObjectTypeInfo.Type.IsAssignableTo(typeof(IListViewDisableDeleteAction)) && View.IsRoot)
            _deleteController?.DeleteAction.Active.SetItemValue("Disable", false);

        if (View.ObjectTypeInfo.Type.IsAssignableTo(typeof(INestedListViewDisableDeleteAction)) && !View.IsRoot)
            _deleteController?.DeleteAction.Active.SetItemValue("Disable", false);

        if (View.ObjectTypeInfo.Type.IsAssignableTo(typeof(INestedListViewDisableNewAction)) && !View.IsRoot)
            _newController?.NewObjectAction.Active.SetItemValue("Disable", false);

        if (View.ObjectTypeInfo.Type.IsAssignableTo(typeof(INestedListViewDisableNewAction)) && !View.IsRoot)
            _newController?.NewObjectAction.Active.SetItemValue("Disable", false);

        if (View.ObjectTypeInfo.Type.IsAssignableTo(typeof(IDetailViewDisable))) {
            _currentObjectController?.ProcessCurrentObjectAction.Active.SetItemValue("Disable", false);
        }
            

        if (View.ObjectTypeInfo.Type.IsAssignableTo(typeof(IDetailViewShowInPopup))) {
            if (_newController != null)
                _newController.NewObjectAction.Execute += NewObjectAction_Execute;
            if (_currentObjectController != null)
                _currentObjectController.ProcessCurrentObjectAction.Execute += ProcessCurrentObjectAction_Execute;
        }
    }       

    private void ProcessCurrentObjectAction_Execute(object sender, SimpleActionExecuteEventArgs e) {
        e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
        e.ShowViewParameters.CreateAllControllers = true;
    }

    private void NewObjectAction_Execute(object sender, SingleChoiceActionExecuteEventArgs e) {
        e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
        e.ShowViewParameters.CreateAllControllers = true;
    }

    protected override void OnDeactivated() {
        base.OnDeactivated();
        _newController.NewObjectAction.Execute -= NewObjectAction_Execute;
        _currentObjectController.ProcessCurrentObjectAction.Execute -= ProcessCurrentObjectAction_Execute;
    }
}


