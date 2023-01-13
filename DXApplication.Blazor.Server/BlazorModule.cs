using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Updating;
using DevExpress.Persistent.BaseImpl;
using DXApplication.Blazor.Server.Controllers;
using System.ComponentModel;


namespace DXApplication.Blazor.Server;

[ToolboxItemFilter("Xaf.Platform.Blazor")]
public sealed class DXApplicationBlazorModule : ModuleBase {
    //private void Application_CreateCustomModelDifferenceStore(object sender, CreateCustomModelDifferenceStoreEventArgs e) {
    //    e.Store = new ModelDifferenceDbStore((XafApplication)sender, typeof(ModelDifference), true, "Blazor");
    //    e.Handled = true;
    //}
    private void Application_CreateCustomUserModelDifferenceStore(object sender, CreateCustomModelDifferenceStoreEventArgs e) {
        e.Store = new ModelDifferenceDbStore((XafApplication)sender, typeof(ModelDifference), false, "Blazor");
        e.Handled = true;
    }
    public DXApplicationBlazorModule() {
    }
    public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB) {
        return ModuleUpdater.EmptyModuleUpdaters;
    }
    public override void Setup(XafApplication application) {
        base.Setup(application);
        //application.CreateCustomModelDifferenceStore += Application_CreateCustomModelDifferenceStore;
        application.CreateCustomUserModelDifferenceStore += Application_CreateCustomUserModelDifferenceStore;

        // TODO: tạo composite object space (để trộn lẫn domain component với business persistent class)
        application.ObjectSpaceCreated += (sender, e) => {
            if (e.ObjectSpace is CompositeObjectSpace compositeObjectSpace) {
                if (compositeObjectSpace.Owner is not CompositeObjectSpace) {
                    compositeObjectSpace.PopulateAdditionalObjectSpaces((XafApplication)sender);
                }
            }
        };

        // TODO: link object với với object cha ngay sau khi khởi tạo
        application.LinkNewObjectToParentImmediately = true;

        // TODO: thay đổi listview và detailview trong application model (thay vì dùng controller)
        application.ListViewCreating += ApplicationController.ListViewCreating;
        //application.DetailViewCreating += ApplicationController.DetailViewCreating;        
    }
}
