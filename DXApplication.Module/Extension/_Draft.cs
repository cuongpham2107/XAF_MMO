/// <summary>
/// Xử lý CustomNestedListViewAttribute
/// </summary>
/// <param name="modelClass"></param>
/// <param name="type"></param>
/// <param name="viewsNode"></param>
void CustomNestedListView(IModelClass modelClass, Type type, ModelNode viewsNode) {
    var props = type.GetProperties().Where(pi => pi.GetCustomAttribute<CustomNestedListViewAttribute>() != null);
    //var bom = viewsNode.Application.BOModel.GetClass(type);
    foreach (var prop in props) {
        if (prop != null) {
            var attrs = prop.GetCustomAttributes(typeof(CustomNestedListViewAttribute), true);
            foreach (CustomNestedListViewAttribute attr in attrs.Cast<CustomNestedListViewAttribute>()) {
                //var attr = prop.GetCustomAttribute<CustomNestedListViewAttribute>();
                IModelListView listviewNode;
                var listviewId = $"{type.Name}_{prop.Name}_ListView";
                listviewNode = viewsNode.GetNode(listviewId) as IModelListView;

                if (listviewNode != null) {
                    //TODO đặt detail view khi click row
                    if (!string.IsNullOrEmpty(attr.DetailViewId)) {
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

                    //for (var i = 0; i < attr.FieldsToSort.Length; i++)
                    //    listviewNode.Columns[attr.FieldsToSort[i]].SortIndex = i;
                    for (var i = 0; i < attr.FieldsToSort.Length; i++) {
                        var field = attr.FieldsToSort[i];
                        if (field.EndsWith(".")) {
                            var f = field.TrimEnd('.');
                            listviewNode.Columns[f].SortIndex = i;
                            listviewNode.Columns[f].SortOrder = ColumnSortOrder.Descending;
                        } else listviewNode.Columns[field].SortIndex = i;
                    }

                    for (var i = 0; i < attr.FieldsToGroup.Length; i++)
                        listviewNode.Columns[attr.FieldsToGroup[i]].GroupIndex = i;

                    foreach (var f in attr.FieldsToRemove)
                        listviewNode.Columns[f].Remove();
                }
            }

        }
    }
}


/// <summary>
/// Điều chỉnh nested listview
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class CustomNestedListViewAttribute : Attribute {
    /// <summary>
    /// Chỉ định detail view nào sẽ được hiển thị khi click vào nested list view
    /// </summary>
    public string DetailViewId { get; set; }

    /// <summary>
    /// Nếu null sẽ tác động đến list view mặc định.<br/>
    /// Nếu khác null sẽ tạo list view mới."
    /// </summary>
    //public string ViewId { get; set; } = null;

    /// <summary>
    /// Những trường cần ẩn sẽ có index = -1<br/>.
    /// Trường ẩn vẫn xuất hiện trên column chooser (và có thể cho hiển thị lại)
    /// </summary>
    public string[] FieldsToHide { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Sẽ loại bỏ các trường này hoàn toàn, ở runtime không cho hiện lại được
    /// </summary>
    public string[] FieldsToRemove { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Mặc định sẽ xếp tăng dần. Nếu muốn xếp giảm dần thêm dấu chấm (.) vào sau tên trường
    /// </summary>
    public string[] FieldsToSort { get; set; } = Array.Empty<string>();
    public string[] FieldsToGroup { get; set; } = Array.Empty<string>();

    /// <summary>
    /// <see langword="true"/> - bật inline edit
    /// </summary>
    public bool AllowEdit { get; set; } = false;
    public bool AllowDelete { get; set; } = true;
    public bool AllowNew { get; set; } = true;
    public bool AllowLink { get; set; } = false;
    public bool AllowUnlink { get; set; } = false;
}