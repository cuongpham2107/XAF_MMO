using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using DXApplication.Module.Extension;

namespace DXApplication.Blazor.Server.Controllers;


public class DomainComponentController : ViewController {
    public DomainComponentController() {
        TargetObjectType = typeof(IDomainComponent);
    }
    protected override void OnActivated() {
        base.OnActivated();
        var modification = Frame.GetController<ModificationsController>();
        if (modification != null) modification.Active["Active"] = false;
        var refresh = Frame.GetController<RefreshController>();
        if (refresh != null) refresh.Active["Active"] = false;
        var newController = Frame.GetController<NewObjectViewController>();
        if (newController != null) newController.Active["Active"] = false;
        var deleteController = Frame.GetController<DeleteObjectsViewController>();
        if (deleteController != null) deleteController.Active["Active"] = false;
    }
}