using DevExpress.Blazor;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Blazor.Templates;
using DevExpress.ExpressApp.Blazor.Templates.Toolbar.ActionControls;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Templates.ActionControls;
using DevExpress.Persistent.Base;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace DXApplication.Blazor.Server.Templates;
public class MypeNestedFrameTemplate_23x : FrameTemplate, ISupportActionsToolbarVisibility, ISelectionDependencyToolbar {
    public MypeNestedFrameTemplate_23x() {
        IsActionsToolbarVisible = true;
        Toolbar = new DxToolbarAdapter(new DxToolbarModel());
        Toolbar.AddActionContainer(nameof(PredefinedCategory.ObjectsCreation));
        Toolbar.AddActionContainer("Link");
        Toolbar.AddActionContainer(nameof(PredefinedCategory.Edit));
        Toolbar.AddActionContainer(nameof(PredefinedCategory.RecordEdit));
        Toolbar.AddActionContainer(nameof(PredefinedCategory.View), ToolbarItemAlignment.Right);
        Toolbar.AddActionContainer(nameof(PredefinedCategory.Reports), ToolbarItemAlignment.Right);
        Toolbar.AddActionContainer(nameof(PredefinedCategory.Diagnostic), ToolbarItemAlignment.Right);
        Toolbar.AddActionContainer(nameof(PredefinedCategory.Filters), ToolbarItemAlignment.Right);
        Toolbar.AddActionContainer(nameof(PredefinedCategory.FullTextSearch), ToolbarItemAlignment.Right);
        Toolbar.AddActionContainer(nameof(PredefinedCategory.Export), ToolbarItemAlignment.Right);
        Toolbar.AddActionContainer(nameof(PredefinedCategory.Unspecified), ToolbarItemAlignment.Right);
    }
    protected override IEnumerable<IActionControlContainer> GetActionControlContainers() => Toolbar.ActionContainers;
    protected override RenderFragment CreateComponent() => MypeNestedFrameTemplate_23xComponent.Create(this);
    protected override void BeginUpdate() {
        base.BeginUpdate();
        ((ISupportUpdate)Toolbar).BeginUpdate();
    }
    protected override void EndUpdate() {
        ((ISupportUpdate)Toolbar).EndUpdate();
        base.EndUpdate();
    }
    public bool IsActionsToolbarVisible { get; private set; }
    public DxToolbarAdapter Toolbar { get; }
    void ISupportActionsToolbarVisibility.SetVisible(bool isVisible) => IsActionsToolbarVisible = isVisible;
}
