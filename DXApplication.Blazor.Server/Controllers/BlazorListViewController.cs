﻿using DevExpress.Blazor;
using DevExpress.DashboardCommon;
using DevExpress.DashboardCommon.Native;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Blazor.Editors.Adapters;
using DevExpress.ExpressApp.Blazor.SystemModule;
using DevExpress.ExpressApp.Dashboards.Blazor.Controllers;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Persistent.BaseImpl;
using DXApplication.Blazor.BusinessObjects;

namespace DXApplication.Blazor.Server.Controllers;

/// <summary>
/// TODO: điều chỉnh chung cho tất cả blazor list view
/// </summary>
public class BlazorListViewController : ViewController<ListView> {

    protected override void OnViewControlsCreated() {
        base.OnViewControlsCreated();

        //TODO: điều chỉnh cách hiển thị của DxGridListEditor
        if (View.Editor is DxGridListEditor gridListEditor) {
            IDxGridAdapter dataGridAdapter = gridListEditor.GetGridAdapter();
            dataGridAdapter.GridModel.ColumnResizeMode = GridColumnResizeMode.NextColumn;
            dataGridAdapter.GridModel.FooterDisplayMode = GridFooterDisplayMode.Auto;
            dataGridAdapter.GridModel.AutoExpandAllGroupRows = true;
            dataGridAdapter.GridModel.ShowFilterRow = true;
            dataGridAdapter.GridModel.ShowGroupPanel = true;
            dataGridAdapter.GridModel.ShowSearchBox = true;
            dataGridAdapter.GridModel.EditNewRowPosition = GridEditNewRowPosition.Top;
            dataGridAdapter.GridCommandColumnModel.Visible = true;
            dataGridAdapter.GridCommandColumnModel.ShowInColumnChooser = true;
            dataGridAdapter.GridSelectionColumnModel.ShowInColumnChooser = true;
            dataGridAdapter.GridSelectionColumnModel.AllowSelectAll = true;
        }
    }

    protected override void OnActivated() {
        base.OnActivated();

        // mặc định ẩn full text search vì đã có searchbox của blazor grid
        var _filterController = Frame.GetController<FilterController>();
        _filterController?.FullTextFilterAction.Active.SetItemValue("Disable", false);
    }
}
