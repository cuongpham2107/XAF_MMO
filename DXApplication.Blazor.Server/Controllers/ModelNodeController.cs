using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.Model.NodeGenerators;
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
        foreach (var m in viewsNode.Application.BOModel.Where(bom => bom.Name.Contains("BusinessObjects"))) {
            var type = Type.GetType(m.Name);
            if (type != null) {
                var attr = type.GetCustomAttribute<CustomListViewAttribute>();
                if (attr != null) {
                    //var bom = viewsNode.Application.BOModel.GetClass(type);
                    var listviewNode = m.DefaultListView;
                    if (listviewNode != null) {
                        listviewNode.AllowDelete = attr.AllowDelete;
                        listviewNode.AllowEdit = attr.AllowEdit;
                        listviewNode.AllowNew = attr.AllowNew;
                        listviewNode.AllowLink = attr.AllowLink;
                        listviewNode.AllowUnlink = attr.AllowUnlink;
                    }
                }
            }
        }
    }

    protected void CustomNestedListView(ModelNode viewsNode) {
        foreach (var m in viewsNode.Application.BOModel.Where(bom => bom.Name.Contains("BusinessObjects"))) {
            var type = Type.GetType(m.Name);
            if (type != null) {
                var props = type.GetProperties().Where(pi => pi.GetCustomAttribute<CustomNestedListViewAttribute>() != null);
                //var bom = viewsNode.Application.BOModel.GetClass(type);
                foreach (var prop in props) {
                    if (prop != null) {
                        var listviewId = $"{type.Name}_{prop.Name}_ListView";
                        var listviewNode = viewsNode.GetNode(listviewId) as IModelListView;
                        var attr = prop.GetCustomAttribute<CustomNestedListViewAttribute>();

                        if (listviewNode != null) {
                            var detailView = viewsNode.GetNode(attr.DetailViewId) as IModelDetailView ?? m.DefaultDetailView;
                            listviewNode.DetailView = detailView;
                            listviewNode.AllowDelete = attr.AllowDelete;
                            listviewNode.AllowEdit = attr.AllowEdit;
                            listviewNode.AllowNew = attr.AllowNew;
                            listviewNode.AllowLink = attr.AllowLink;
                            listviewNode.AllowUnlink = attr.AllowUnlink;
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
                        var detailViewNode = string.IsNullOrEmpty(attr.ViewId) ?
                            modelClass.DefaultDetailView :
                            viewsNode.AddNode<IModelDetailView>(attr.ViewId);

                        detailViewNode.ModelClass = modelClass;
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
