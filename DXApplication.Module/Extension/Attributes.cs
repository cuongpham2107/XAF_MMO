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
    public string ViewId { get; set; } = null;
    public string[] FieldsToRemove { get; set; } = Array.Empty<string>();
    public string[] FieldsReadonly { get; set; } = Array.Empty<string>();
    public bool AllowEdit { get; set; } = true;
    public bool AllowDelete { get; set; } = true;
    public bool AllowNew { get; set; } = true;
}

/// <summary>
/// Chỉ định detail view cho nested listview
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class CustomNestedListViewAttribute : Attribute {
    /// <summary>
    /// Chỉ định detail view nào sẽ được hiển thị khi click vào nested list view
    /// </summary>
    public string DetailViewId { get; set; }

    public string ViewId { get; set; } = null;

    public string[] FieldsToHide { get; set; } = Array.Empty<string>();
    public string[] FieldsToRemove { get; set; } = Array.Empty<string>();
    public string[] FieldsToSort { get; set; } = Array.Empty<string>();
    public string[] FieldsToGroup { get; set; } = Array.Empty<string>();

    public bool AllowEdit { get; set; } = true;
    public bool AllowDelete { get; set; } = true;
    public bool AllowNew { get; set; } = true;
    public bool AllowLink { get; set; } = true;
    public bool AllowUnlink { get; set; } = true;
}

/// <summary>
/// Chỉ định các đặc tính của listview
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class CustomListViewAttribute : Attribute {
    /// <summary>
    /// Chỉ định detail view nào sẽ được hiển thị khi click vào nested list view
    /// </summary>
    public string DetailViewId { get; set; }

    public string ViewId { get; set; } = null;

    public string[] FieldsToHide { get; set; } = Array.Empty<string>();
    public string[] FieldsToRemove { get; set; } = Array.Empty<string>();
    public string[] FieldsToSort { get; set; } = Array.Empty<string>();
    public string[] FieldsToGroup { get; set; } = Array.Empty<string>();

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
    public bool IsReversed { get; set; } = false;
    public string[] Fields { get; set; } = Array.Empty<string>();
}