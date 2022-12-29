using DevExpress.ExpressApp.Blazor.Editors.Adapters;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;

namespace DXApplication.Blazor.Server.Editors {
    [PropertyEditor(typeof(DateTime), false)]
    public class CustomDateTimePropertyEditor : DateTimePropertyEditor {
        public CustomDateTimePropertyEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model) { }
        protected override void OnControlCreated() {
            base.OnControlCreated();
            if (Control is DxDateEditAdapter adapter) {
                adapter.ComponentModel.TimeSectionVisible = true;
            }
        }
    }
}
