using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.SystemModule;

namespace DXApplication.Module.Extension;
public class ListViewController : ViewController<ListView> {

    protected override void OnActivated() {
        base.OnActivated();

        var _deleteController = Frame.GetController<DeleteObjectsViewController>();
        var _linkunlinkController = Frame.GetController<LinkUnlinkController>();
        var _currentObjectController = Frame.GetController<ListViewProcessCurrentObjectController>();
        var _newController = Frame.GetController<NewObjectViewController>();

        if (View.ObjectTypeInfo.Type.IsAssignableTo(typeof(IListViewDisableNewAction)) && View.IsRoot)
            _newController?.NewObjectAction.Active.SetItemValue("Disable", false);

        if (View.ObjectTypeInfo.Type.IsAssignableTo(typeof(IListViewDisableDeleteAction)) && View.IsRoot)
            _deleteController?.DeleteAction.Active.SetItemValue("Disable", false);

        if (View.ObjectTypeInfo.Type.IsAssignableTo(typeof(INestedListViewDisableLinkAction)))
            _linkunlinkController?.LinkAction.Active.SetItemValue("Disable", false);

        if (View.ObjectTypeInfo.Type.IsAssignableTo(typeof(INestListViewDisableUnlinkAction)))
            _linkunlinkController?.UnlinkAction.Active.SetItemValue("Disable", false);

        if (View.ObjectTypeInfo.Type.IsAssignableTo(typeof(INestedListViewDisableDeleteAction)) && !View.IsRoot)
            _deleteController?.DeleteAction.Active.SetItemValue("Disable", false);

        if (View.ObjectTypeInfo.Type.IsAssignableTo(typeof(INestedListViewDisableNewAction)) && !View.IsRoot)
            _newController?.NewObjectAction.Active.SetItemValue("Disable", false);

        if (View.ObjectTypeInfo.Type.IsAssignableTo(typeof(IDetailViewDisable))) {
            _currentObjectController?.ProcessCurrentObjectAction.Active.SetItemValue("Disable", false);
        }
    }
}


