using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;
using DXApplication.Module.Extension;

namespace DXApplication.Blazor.Server.Controllers;

public class ApplicationController {
    public static void ListViewCreating(object s, ListViewCreatingEventArgs e) {
        if (((XafApplication)s).FindModelView(e.ViewID) is IModelListView model) {
            var type = e.CollectionSource.ObjectTypeInfo.Type;
            //TODO: cho phép edit inline với root list view
            if (type.IsAssignableTo(typeof(IListViewInline)) && e.IsRoot)
                model.AllowEdit = true;
            //TODO: cho phép edit inline với nested list view
            if (type.IsAssignableTo(typeof(INestedListViewInline)) && !e.IsRoot)
                model.AllowEdit = true;
        }
    }

}
