using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;
using DXApplication.Module.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXApplication.Blazor.Extension {
    public class ApplicationController {
        public static void ListViewCreating(object sender, ListViewCreatingEventArgs e) {
            
            if (((XafApplication)sender).FindModelView(e.ViewID) is IModelListView model) {
                var type = e.CollectionSource.ObjectTypeInfo.Type;
                //TODO: cho phép edit inline riêng rẽ với root và nested list view
                if (type.IsAssignableTo(typeof(IListViewInline)) && e.IsRoot)
                    model.AllowEdit = true;
                if (type.IsAssignableTo(typeof(INestedListViewInline)) && !e.IsRoot)
                    model.AllowEdit = true;
                //TODO: tắt chức năng xóa và thêm mới trên list view
                if (type.IsAssignableTo(typeof(IModelDisableDeleteAction)))
                    model.AllowDelete = false;
                if (type.IsAssignableTo(typeof(IModelDisableNewAction)))
                    model.AllowNew = false;                
            }
        }

        public static void DetailViewCreating(object sender, DetailViewCreatingEventArgs e) {
            if (((XafApplication)sender).FindModelView(e.ViewID) is IModelDetailView model) {
                //TODO: tắt chức năng xóa và thêm mới trên detail view
                //if (e.Obj is IDetailViewReadonly)
                //    model.AllowEdit = false; 
                if (e.Obj is IModelDisableDeleteAction)
                    model.AllowDelete = false;
                if (e.Obj is IModelDisableNewAction)
                    model.AllowNew = false;
            }
        }
    }
}
