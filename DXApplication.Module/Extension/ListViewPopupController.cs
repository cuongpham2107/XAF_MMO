using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.SystemModule;

namespace DXApplication.Module.Extension; 
public class ListViewPopupController : ViewController<ListView> {
    public ListViewPopupController() {
        TargetObjectType = typeof(IListViewPopup);
    }

    private NewObjectViewController _newController;
    private ListViewProcessCurrentObjectController _currentObjectController;

    protected override void OnActivated() {
        base.OnActivated();
        _newController = Frame.GetController<NewObjectViewController>();
        _currentObjectController = Frame.GetController<ListViewProcessCurrentObjectController>();

        if (_newController != null)
            _newController.NewObjectAction.Execute += NewObjectAction_Execute;
        if (_currentObjectController != null)
            _currentObjectController.ProcessCurrentObjectAction.Execute += ProcessCurrentObjectAction_Execute;
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


