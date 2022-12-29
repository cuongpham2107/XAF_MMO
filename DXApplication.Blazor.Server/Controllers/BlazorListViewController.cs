using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Blazor.Editors.Adapters;
using DevExpress.ExpressApp.SystemModule;

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
            dataGridAdapter.GridModel.ColumnResizeMode = DevExpress.Blazor.GridColumnResizeMode.NextColumn;
            dataGridAdapter.GridModel.ShowGroupPanel = true;
            dataGridAdapter.GridModel.FooterDisplayMode = DevExpress.Blazor.GridFooterDisplayMode.Auto;
            dataGridAdapter.GridModel.AutoExpandAllGroupRows = true;
            dataGridAdapter.GridModel.ShowFilterRow = true;
            dataGridAdapter.GridModel.ShowSearchBox = true;
            dataGridAdapter.GridModel.EditNewRowPosition = DevExpress.Blazor.GridEditNewRowPosition.Top;
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
