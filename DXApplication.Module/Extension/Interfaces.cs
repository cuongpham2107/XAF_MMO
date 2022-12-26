using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXApplication.Module.Extension;

// List View interfaces
public interface IListViewInline { }
public interface IListViewInlineEdit : IListViewInline { }
public interface IListViewFormEdit : IListViewInline { }
public interface IListViewPopupEdit : IListViewInline { }
public interface IListViewNewActionInline : IListViewInline { }
public interface IListViewEditActionInline : IListViewInline { }
public interface IListViewDisableNewAction { }
public interface IListViewDisableDeleteAction { }

// Nested List View interfaces
public interface INestedListViewInline { }
public interface INestedListViewInlineEdit : INestedListViewInline { }
public interface INestedListViewFormEdit : INestedListViewInline { }
public interface INestedListViewPopupEdit : INestedListViewInline { }
public interface INestedListViewNewActionInline : INestedListViewInline { }
public interface INestedListViewEditActionInline : INestedListViewInline { }
public interface INestedListViewDisableNewAction { }
public interface INestedListViewDisableDeleteAction { }
public interface INestedListViewDisableLinkAction { }
public interface INestListViewDisableUnlinkAction { }

// Detail View interfaces
public interface IDetailViewDisableNewAction { }
public interface IDetailViewDisableDeleteAction { }
public interface IDetailViewPopupReadonly { }
public interface IDetailViewReadonly { }
public interface IDetailViewDisable { }
public interface IDetailViewShowInPopup { }

// Model interfaces
public interface IModelDisableNewAction { }
public interface IModelDisableDeleteAction { }

// Some composite interfaces
public interface IFullInline :    
    IListViewNewActionInline, 
    IListViewEditActionInline,    
    INestedListViewNewActionInline,
    INestedListViewEditActionInline { }

public interface IFullPopup : IDetailViewShowInPopup { }