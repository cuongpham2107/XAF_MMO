using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXApplication.Module.Extension {
    public class ApplicationController {
        public static void ListViewCreating(object sender, ListViewCreatingEventArgs e) {
            //TODO: mặc định cho phép edit inline
            if (((XafApplication)sender).FindModelView(e.ViewID) is IModelListView model) {
                var type = e.CollectionSource.ObjectTypeInfo.Type;
                if (type.IsAssignableTo(typeof(IListViewInline)) && e.IsRoot)
                    model.AllowEdit = true;
                if (type.IsAssignableTo(typeof(INestedListViewInline)) && !e.IsRoot)
                    model.AllowEdit = true;
            }
        }

        public static void DetailViewCreating(object sender, DetailViewCreatingEventArgs e) {
            //if (((XafApplication)sender).FindModelView(e.ViewID) is IModelDetailView model) {
            //    if (e.Obj is IDetailViewReadonly)
            //        model.AllowEdit = false;
            //}
        }
    }
}
