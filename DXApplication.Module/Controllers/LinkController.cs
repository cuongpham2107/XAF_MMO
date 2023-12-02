using DevExpress.ExpressApp.Actions;
using DXApplication.Module.BusinessObjects;
using DXApplication.Module.Extension;
using System;
using System.Linq;

namespace DXApplication.Module.Controllers;
public class LinkController : MypeViewController {
    public LinkController() {
        TargetObjectType = typeof(Link);
        TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
        TargetViewType = DevExpress.ExpressApp.ViewType.ListView;

        FakeData();
        ClearData();
    }

    void ClearData() {
        var action = CreateAction(ActionType.Simple, "Clear Data") as SimpleAction;
        action.SelectionDependencyType = SelectionDependencyType.Independent;
        action.Execute += (s, e) => {
            ObjectSpace.Delete(ObjectSpace.GetObjects<Link>());
            ObjectSpace.CommitChanges();
            View.Refresh();
        };
    }

    void FakeData() {
        var action = CreateAction(ActionType.Simple, "Fake Data") as SimpleAction;
        action.SelectionDependencyType = SelectionDependencyType.Independent;
        action.Execute += (s, e) => {
            for (int i = 0; i < 1000; i++) {
                var obj = ObjectSpace.CreateObject<Link>();
                obj.Url = $"https://youtube.com/watch?v={Mype.Common.Extension.RandomString(11)}";
                obj.Label = Mype.Common.Extension.RandomString(20);
            }
            ObjectSpace.CommitChanges();
            //View.Refresh();
        };
    }    
}
