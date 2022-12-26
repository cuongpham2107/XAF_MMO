using DevExpress.Blazor;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Blazor.Templates;
using DevExpress.ExpressApp.Blazor.Templates.Toolbar.ActionControls;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Templates.ActionControls;
using DevExpress.Persistent.Base;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;

namespace DXApplication.Blazor.Server.Templates;
public class MypePopupWindowTemplate : WindowTemplateBase, ISupportActionsToolbarVisibility, IPopupWindowTemplateSize {
    public MypePopupWindowTemplate() {
        IsActionsToolbarVisible = true;
        Toolbar = new DxToolbarAdapter(new DxToolbarModel());
        Toolbar.AddActionContainer(nameof(PredefinedCategory.ObjectsCreation));

        Toolbar.AddActionContainer(nameof(PredefinedCategory.Save));
        Toolbar.AddActionContainer(nameof(PredefinedCategory.UndoRedo));
        Toolbar.AddActionContainer(nameof(PredefinedCategory.Edit));
        Toolbar.AddActionContainer(nameof(PredefinedCategory.RecordEdit));

        Toolbar.AddActionContainer(nameof(PredefinedCategory.Tools), DevExpress.Blazor.ToolbarItemAlignment.Right);
        Toolbar.AddActionContainer(nameof(PredefinedCategory.RecordsNavigation), DevExpress.Blazor.ToolbarItemAlignment.Right);
        Toolbar.AddActionContainer(nameof(PredefinedCategory.Export), DevExpress.Blazor.ToolbarItemAlignment.Right);
        Toolbar.AddActionContainer(nameof(PredefinedCategory.Reports), DevExpress.Blazor.ToolbarItemAlignment.Right);

        Toolbar.AddActionContainer(nameof(PredefinedCategory.Search), ToolbarItemAlignment.Right);
        Toolbar.AddActionContainer(nameof(PredefinedCategory.Filters), ToolbarItemAlignment.Right);
        Toolbar.AddActionContainer(nameof(PredefinedCategory.FullTextSearch), ToolbarItemAlignment.Right);

        Toolbar.AddActionContainer(nameof(PredefinedCategory.View), DevExpress.Blazor.ToolbarItemAlignment.Right);
        Toolbar.AddActionContainer(nameof(PredefinedCategory.Unspecified), DevExpress.Blazor.ToolbarItemAlignment.Right);

        BottomToolbar = new DxToolbarAdapter(new DxToolbarModel());
        BottomToolbar.AddActionContainer("Diagnostic", ToolbarItemAlignment.Right);
        BottomToolbar.AddActionContainer(DialogController.DialogActionContainerName, ToolbarItemAlignment.Right);
        BottomToolbar.ToolbarModel.SizeMode = SizeMode.Large;
        BottomToolbar.ToolbarModel.CssClass= "tool-bar";
    }
    protected override IEnumerable<IActionControlContainer> GetActionControlContainers() =>
        Toolbar.ActionContainers.Concat(BottomToolbar.ActionContainers);
    protected override RenderFragment CreateComponent() => MypePopupWindowTemplateComponent.Create(this);

    public bool IsActionsToolbarVisible { get; private set; } = true;
    public DxToolbarAdapter Toolbar { get; }
    public DxToolbarAdapter BottomToolbar { get; }
    public string MinWidth { get; set; }
    public string MinHeight { get; set; }
    public string Width { get; set; }
    public string Height { get; set; }
    public string MaxWidth { get; set; }
    public string MaxHeight { get; set; }

    void ISupportActionsToolbarVisibility.SetVisible(bool isVisible) => IsActionsToolbarVisible = isVisible;
}
