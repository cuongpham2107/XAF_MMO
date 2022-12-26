using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;

namespace DXApplication.Module.Extension;

public class ModelViewController : ViewController {
    private NewObjectViewController _newController;
    private DeleteObjectsViewController _deleteController;

    protected override void OnActivated() {
        base.OnActivated();
        _deleteController = Frame.GetController<DeleteObjectsViewController>();
        _newController = Frame.GetController<NewObjectViewController>();

        if (View.ObjectTypeInfo.Type.IsAssignableTo(typeof(IModelDisableDeleteAction)))
            _deleteController?.DeleteAction.Active.SetItemValue("Disable", false);

        if (View.ObjectTypeInfo.Type.IsAssignableTo(typeof(IModelDisableNewAction)))
            _newController?.NewObjectAction.Active.SetItemValue("Disable", false);
    }
}
