using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.SystemModule;

namespace DXApplication.Module.Extension;

public class DetailViewController : ViewController<DetailView> {
    protected override void OnActivated() {
        base.OnActivated();

        var _newController = Frame.GetController<NewObjectViewController>();
        var _deleteController = Frame.GetController<DeleteObjectsViewController>();

        if (View.ObjectTypeInfo.Type.IsAssignableTo(typeof(IDetailViewDisableNewAction)))
            _newController?.NewObjectAction.Active.SetItemValue("Disable", false);

        if (View.ObjectTypeInfo.Type.IsAssignableTo(typeof(IDetailViewDisableDeleteAction)))
            _deleteController?.DeleteAction.Active.SetItemValue("Disable", false);

        if (Frame.Context == TemplateContext.PopupWindow && View.ObjectTypeInfo.Type.IsAssignableTo(typeof(IDetailViewPopupReadonly)))
            View.AllowEdit["ReadOnly"] = false;

        if (View.ObjectTypeInfo.Type.IsAssignableTo(typeof(IDetailViewReadonly)))
            View.AllowEdit["ReadOnly"] = false;
    }
}
