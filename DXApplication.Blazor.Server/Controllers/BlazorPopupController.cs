using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.SystemModule;
using DXApplication.Module.Extension;

namespace DXApplication.Blazor.Server.Controllers; 
public class BlazorPopupController : ViewController<ListView> {
    public BlazorPopupController() {
        TargetObjectType = typeof(IListViewPopup);
    }

    private NewObjectViewController _newController;
    private ListViewProcessCurrentObjectController _currentObjectController;

    protected override void OnViewControlsCreated() {
        base.OnViewControlsCreated();
        if (View.Editor is DxGridListEditor gridListEditor) {
            IDxGridAdapter dataGridAdapter = gridListEditor.GetGridAdapter();
            dataGridAdapter.GridCommandColumnModel.Visible = false;
        }
    }

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
