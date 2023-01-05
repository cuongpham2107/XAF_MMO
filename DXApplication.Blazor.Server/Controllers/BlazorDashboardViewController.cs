using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor.SystemModule;
using DevExpress.ExpressApp.Dashboards.Blazor.Controllers;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Persistent.BaseImpl;
using DXApplication.Blazor.BusinessObjects;

namespace DXApplication.Blazor.Server.Controllers;

public class BlazorDashboardViewController : ViewController {
    public BlazorDashboardViewController() {
        TargetObjectType = typeof(DashboardData);
        TargetViewType = ViewType.DetailView;
    }

    protected override void OnActivated() {
        base.OnActivated();
        //TODO chỉ áp dụng cho My Dashboard
        if (View.CurrentObject is DashboardData dashboard) {
            if (dashboard.Title != "My Dashboard") return;
        }
        //TODO nếu sử dụng class khác thay cho ApplicationUseer thì cần thay ở đây
        if (SecuritySystem.CurrentUser is ApplicationUser user) {
            if (!user.Roles.Any(r => r.IsAdministrative)) {
                var workingModeSwitch = Frame.GetController<WorkingModeSwitchController>();
                var exportToXml = Frame.GetController<ExportToXmlController>();
                var closeDetailView = Frame.GetController<CloseDetailViewController>();
                var newObject = Frame.GetController<NewObjectViewController>();
                if (workingModeSwitch != null) workingModeSwitch.Active["Disable"] = false;
                if (exportToXml != null) exportToXml.Active["Disable"] = false;
                if (closeDetailView != null) closeDetailView.Active["Disable"] = false;
                if (newObject != null) newObject.Active["Disable"] = false;
            }
        }
    }
}