using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXApplication.Module.Extension;

// List View interfaces
public interface IListViewInline { }
public interface IListViewPopup { }
public interface IListViewDisableNewAction { }
public interface IListViewDisableDeleteAction { }

// Nested List View interfaces
public interface INestedListViewInline { }
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
