using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Blazor.Editors.Adapters;

namespace DXApplication.Blazor.Server.Controllers; 
public class CustomizeDateEditorController : ViewController<DetailView> {
    protected override void OnActivated() {
        base.OnActivated();
        View.CustomizeViewItemControl<DateTimePropertyEditor>(this, CustomizeDateTimeEditor);
    }
    private void CustomizeDateTimeEditor(DateTimePropertyEditor propertyEditor) {
        if (propertyEditor.Control is DxDateEditAdapter adapter) {
            adapter.ComponentModel.TimeSectionVisible = true;
        }
    }
}