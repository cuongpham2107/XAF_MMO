using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXApplication.Module.Extension;

/// <summary>
/// Chỉ định tạo thêm detail view mới
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class CustomDetailViewAttribute : Attribute {
    readonly string _viewId;
    public CustomDetailViewAttribute(string viewId) {
        _viewId = viewId;
    }
    public string ViewId => _viewId;
    public string[] FieldsToRemove { get; set; } = Array.Empty<string>();
    public bool AllowEdit { get; set; } = true;
    public bool AllowDelete { get; set; } = true;
    public bool AllowNew { get; set; } = true;
}

/// <summary>
/// Chỉ định detail view cho nested listview
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class CustomNestedListViewAttribute : Attribute {
    readonly string _viewId;
    public CustomNestedListViewAttribute(string viewId) {
        _viewId = viewId;
    }

    public string DetailViewId => _viewId;

    public bool AllowEdit { get; set; } = true;
    public bool AllowDelete { get; set; } = true;
    public bool AllowNew { get; set; } = true;
    public bool AllowLink { get; set; } = true;
    public bool AllowUnlink { get; set; } = true;
}

/// <summary>
/// Chỉ định các đặc tính của listview
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class CustomListViewAttribute : Attribute {
    public bool AllowEdit { get; set; } = true;
    public bool AllowDelete { get; set; } = true;
    public bool AllowNew { get; set; } = true;
    public bool AllowLink { get; set; } = true;
    public bool AllowUnlink { get; set; } = true;
}

/// <summary>
/// Chỉ định một field là readonly
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
public class ReadonlyAttribute : Attribute {
    private readonly bool _reversed;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="reversed">
    /// Chỉ có tác dụng khi dùng cho class:
    ///  -true => cả class thành readonly, trừ các trường chỉ định
    ///  -false (mặc định) => cả class bình thường, các trường chỉ định thành readonly
    /// </param>
    public ReadonlyAttribute(bool reversed = false) {
        _reversed = reversed;
    }
    public bool IsReversed => _reversed;
    public string[] Fields { get; set; } = Array.Empty<string>();
}
