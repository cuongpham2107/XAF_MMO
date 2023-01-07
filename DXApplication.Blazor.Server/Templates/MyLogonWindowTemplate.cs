using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Blazor.Templates;
using DevExpress.ExpressApp.Blazor.Templates.Toolbar.ActionControls;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates.ActionControls;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;

namespace DXApplication.Blazor.Server.Templates;
public class MyLogonWindowTemplate : WindowTemplateBase {
    public MyLogonWindowTemplate() {
        Toolbar = new DxToolbarAdapter(new DxToolbarModel() { AdaptivityAutoHideRootItems = false, AdaptivityAutoCollapseItemsToIcons = false });
        Toolbar.AddActionContainer(DialogController.DialogActionContainerName);

        AdditionalToolbar = new DxToolbarAdapter(new DxToolbarModel());
        AdditionalToolbar.ImageSize = 16;
        AdditionalToolbar.ToolbarModel.CssClass = "logon-bottom-toolbar";
        AdditionalToolbar.AddActionContainer("AdditionalLogonActions");
    }
    protected override IEnumerable<IActionControlContainer> GetActionControlContainers() =>
        Toolbar.ActionContainers.Concat(AdditionalToolbar.ActionContainers);
    protected override RenderFragment CreateComponent() => MyLogonWindowTemplateComponent.Create(this);
    public DxToolbarAdapter Toolbar { get; }
    public DxToolbarAdapter AdditionalToolbar { get; }
    public string HeaderCaption { get; set; }
}
