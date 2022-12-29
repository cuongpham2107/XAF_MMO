using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.XtraScheduler.Native;
using DXApplication.Module.Extension;
using System;
using System.Linq;
using System.Reflection;

namespace DXApplication.Module.Extension;

public class ModelNodeController : ModelNodesGeneratorUpdater<ModelViewsNodesGenerator> {
    public override void UpdateNode(ModelNode viewsNode) {
        //TODO: dùng custom attribute để điều chỉnh việc sinh application model
        CustomDetailView(viewsNode); // phải tạo detail view trước
        CustomNestedListView(viewsNode);
        CustomListView(viewsNode);
        Readonly(viewsNode);
    }

    void Readonly(ModelNode viewsNode) {
        foreach (var modelClass in viewsNode.Application.BOModel.Where(bom => bom.Name.Contains("BusinessObjects"))) {
            var type = Type.GetType(modelClass.Name);
            if (type != null) {
                // nếu attribute áp dụng với properties
                var props = type.GetProperties().Where(pi => pi.GetCustomAttribute<ReadonlyAttribute>() != null);
                foreach (var prop in props) {
                    modelClass.FindOwnMember(prop.Name).AllowEdit = false;
                }
                // nếu attribute áp dụng với class
                var attr = type.GetCustomAttribute<ReadonlyAttribute>();
                if (attr != null) {
                    if (attr.IsReversed) {
                        foreach (var member in modelClass.OwnMembers) {
                            if (attr.Fields.Contains(member.Name))
                                member.AllowEdit = true;
                            else
                                member.AllowEdit = false;
                        }
                    } else {
                        foreach (string field in attr.Fields) {
                            modelClass.FindOwnMember(field).AllowEdit = false;
                        }
                    }
                }
            }
        }
    }

    void CustomListView(ModelNode viewsNode) {
        foreach (var modelClass in viewsNode.Application.BOModel.Where(bom => bom.Name.Contains("BusinessObjects"))) {
            var type = Type.GetType(modelClass.Name);
            if (type != null) {
                var attrs = type.GetCustomAttributes(typeof(CustomListViewAttribute));
                foreach (CustomListViewAttribute attr in attrs.Cast<CustomListViewAttribute>()) {
                    //var attr = type.GetCustomAttribute<CustomListViewAttribute>();
                    if (attr != null) {
                        //var bom = viewsNode.Application.BOModel.GetClass(type);
                        IModelListView listviewNode;
                        if (string.IsNullOrEmpty(attr.ViewId)) {
                            listviewNode = modelClass.DefaultListView;
                        } else {
                            listviewNode = viewsNode.AddNode<IModelListView>(attr.ViewId);
                            listviewNode.ModelClass = modelClass;
                        }

                        if (listviewNode != null) {                           

                            listviewNode.AllowDelete = attr.AllowDelete;
                            listviewNode.AllowEdit = attr.AllowEdit;
                            listviewNode.AllowNew = attr.AllowNew;
                            listviewNode.AllowLink = attr.AllowLink;
                            listviewNode.AllowUnlink = attr.AllowUnlink;

                            if (!string.IsNullOrEmpty(attr.DetailViewId)) {
                                var detailView = viewsNode.GetNode(attr.DetailViewId) as IModelDetailView;
                                listviewNode.DetailView = detailView;
                            }                            

                            foreach (var f in attr.FieldsToHide)
                                listviewNode.Columns[f].Index = -1;                            

                            for (var i = 0; i < attr.FieldsToSort.Length; i++)
                                listviewNode.Columns[attr.FieldsToSort[i]].SortIndex = i;

                            for (var i = 0; i < attr.FieldsToGroup.Length; i++)
                                listviewNode.Columns[attr.FieldsToGroup[i]].GroupIndex = i;

                            foreach (var f in attr.FieldsToRemove)
                                listviewNode.Columns[f].Remove();
                        }
                    }
                }

            }
        }
    }

    protected void CustomNestedListView(ModelNode viewsNode) {
        foreach (var modelClass in viewsNode.Application.BOModel.Where(bom => bom.Name.Contains("BusinessObjects"))) {
            var type = Type.GetType(modelClass.Name);
            if (type != null) {
                var props = type.GetProperties().Where(pi => pi.GetCustomAttribute<CustomNestedListViewAttribute>() != null);
                //var bom = viewsNode.Application.BOModel.GetClass(type);
                foreach (var prop in props) {
                    if (prop != null) {
                        var attrs = prop.GetCustomAttributes(typeof(CustomNestedListViewAttribute), true);
                        foreach (CustomNestedListViewAttribute attr in attrs.Cast<CustomNestedListViewAttribute>()) {
                            //var attr = prop.GetCustomAttribute<CustomNestedListViewAttribute>();
                            IModelListView listviewNode;
                            if (string.IsNullOrEmpty(attr.ViewId)) {
                                var listviewId = $"{type.Name}_{prop.Name}_ListView";
                                listviewNode = viewsNode.GetNode(listviewId) as IModelListView;
                            } else {
                                listviewNode = viewsNode.AddNode<IModelListView>(attr.ViewId);
                                listviewNode.ModelClass = modelClass;
                            }

                            if (listviewNode != null) {
                                //TODO đặt detail view khi click row
                                if(!string.IsNullOrEmpty(attr.DetailViewId)) {
                                    var detailView = viewsNode.GetNode(attr.DetailViewId) as IModelDetailView;
                                    listviewNode.DetailView = detailView;
                                }                              

                                listviewNode.AllowDelete = attr.AllowDelete;
                                listviewNode.AllowEdit = attr.AllowEdit;
                                listviewNode.AllowNew = attr.AllowNew;
                                listviewNode.AllowLink = attr.AllowLink;
                                listviewNode.AllowUnlink = attr.AllowUnlink;                                

                                foreach (var f in attr.FieldsToHide)
                                    listviewNode.Columns[f].Index = -1;                                

                                for (var i = 0; i < attr.FieldsToSort.Length; i++)
                                    listviewNode.Columns[attr.FieldsToSort[i]].SortIndex = i;

                                for (var i = 0; i < attr.FieldsToGroup.Length; i++)
                                    listviewNode.Columns[attr.FieldsToGroup[i]].GroupIndex = i;

                                foreach (var f in attr.FieldsToRemove)
                                    listviewNode.Columns[f].Remove();
                            }
                        }

                    }
                }
            }
        }
    }

    protected void CustomDetailView(ModelNode viewsNode) {
        foreach (var modelClass in viewsNode.Application.BOModel.Where(bom => bom.Name.Contains("BusinessObjects"))) {
            var type = Type.GetType(modelClass.Name);
            if (type != null) {
                var attrs = type.GetCustomAttributes(typeof(CustomDetailViewAttribute));
                foreach (CustomDetailViewAttribute attr in attrs.Cast<CustomDetailViewAttribute>()) {
                    //var attr = type.GetCustomAttribute<AdditionalDetailViewAttribute>();
                    if (attr != null) {
                        //var bom = viewsNode.Application.BOModel.GetClass(type);
                        IModelDetailView detailViewNode;
                        if (string.IsNullOrEmpty(attr.ViewId)) {
                            detailViewNode = modelClass.DefaultDetailView;
                        } else {
                            detailViewNode = viewsNode.AddNode<IModelDetailView>(attr.ViewId);
                            detailViewNode.ModelClass = modelClass;
                        }

                        foreach (var f in attr.FieldsReadonly) {
                            detailViewNode.Items[f].SetValue("AllowEdit", false);
                        }

                        foreach (var f in attr.FieldsToRemove) {
                            detailViewNode.Items[f].Remove();
                        }

                        detailViewNode.AllowDelete = attr.AllowDelete;
                        detailViewNode.AllowEdit = attr.AllowEdit;
                        detailViewNode.AllowNew = attr.AllowNew;
                    }
                }
            }
        }
    }
}
