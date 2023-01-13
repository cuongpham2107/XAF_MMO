using System;
using System.Linq;

namespace DXApplication.Module.Extension;

/// <summary>
/// Chuyển root list view thành dạng inline (không mở detail view)
/// </summary>
public interface IListViewInline { }

/// <summary>
/// Chuyển nested list view thành dạng inline (không mở detail view)
/// </summary>
public interface INestedListViewInline { }

/// <summary>
/// Chuyển root list view thành dạng popup (mở detail view trên popup)
/// </summary>
public interface IListViewPopup { }

/// <summary>
/// Ẩn các controller không cần thiết trên domain component
/// </summary>
public interface IDomainComponent { }

