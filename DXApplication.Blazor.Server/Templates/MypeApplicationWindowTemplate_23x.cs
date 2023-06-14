using DevExpress.Blazor;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Blazor.Templates;
using DevExpress.ExpressApp.Blazor.Templates.Navigation.ActionControls;
using DevExpress.ExpressApp.Blazor.Templates.Security.ActionControls;
using DevExpress.ExpressApp.Blazor.Templates.Toolbar.ActionControls;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Templates.ActionControls;
using DevExpress.Persistent.Base;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;

namespace DXApplication.Blazor.Server.Templates;
public class MypeApplicationWindowTemplate_23x : WindowTemplateBase, ISupportActionsToolbarVisibility, ISelectionDependencyToolbar {
    public MypeApplicationWindowTemplate_23x() {
        NavigateBackActionControl = new NavigateBackActionControl();
        AddActionControl(NavigateBackActionControl);
        AccountComponent = new AccountComponentAdapter();
        AddActionControls(AccountComponent.ActionControls);
        ShowNavigationItemActionControl = new ShowNavigationItemActionControl();
        AddActionControl(ShowNavigationItemActionControl);

        IsActionsToolbarVisible = true;
        Toolbar = new DxToolbarAdapter(new DxToolbarModel());
        Toolbar.AddActionContainer(nameof(PredefinedCategory.ObjectsCreation));
        Toolbar.AddActionContainer(nameof(PredefinedCategory.RecordsNavigation), ToolbarItemAlignment.Right);
        Toolbar.AddActionContainer(nameof(PredefinedCategory.SaveOptions), ToolbarItemAlignment.Right, isDropDown: true, defaultActionId: "SaveAndNew", autoChangeDefaultAction: true);
        Toolbar.AddActionContainer(nameof(PredefinedCategory.Save), ToolbarItemAlignment.Right);
        Toolbar.AddActionContainer(nameof(PredefinedCategory.Close));
        Toolbar.AddActionContainer(nameof(PredefinedCategory.UndoRedo));
        Toolbar.AddActionContainer(nameof(PredefinedCategory.Edit));
        Toolbar.AddActionContainer(nameof(PredefinedCategory.RecordEdit));
        Toolbar.AddActionContainer(nameof(PredefinedCategory.View), ToolbarItemAlignment.Right);
        Toolbar.AddActionContainer(nameof(PredefinedCategory.Reports), ToolbarItemAlignment.Right);
        Toolbar.AddActionContainer(nameof(PredefinedCategory.Search), ToolbarItemAlignment.Right);
        Toolbar.AddActionContainer(nameof(PredefinedCategory.Filters), ToolbarItemAlignment.Right);
        Toolbar.AddActionContainer(nameof(PredefinedCategory.FullTextSearch), ToolbarItemAlignment.Right);
        Toolbar.AddActionContainer(nameof(PredefinedCategory.Tools), ToolbarItemAlignment.Right);
        Toolbar.AddActionContainer(nameof(PredefinedCategory.Export), ToolbarItemAlignment.Right);
        Toolbar.AddActionContainer(nameof(PredefinedCategory.Unspecified), ToolbarItemAlignment.Right);

        HeaderToolbar = new DxToolbarAdapter(new DxToolbarModel() {
            CssClass = "pe-2"
        });
        HeaderToolbar.AddActionContainer(nameof(PredefinedCategory.QuickAccess), ToolbarItemAlignment.Right);
        HeaderToolbar.AddActionContainer(nameof(PredefinedCategory.Notifications), ToolbarItemAlignment.Right);
        HeaderToolbar.AddActionContainer(nameof(PredefinedCategory.Diagnostic), ToolbarItemAlignment.Right);
    }
    protected override IEnumerable<IActionControlContainer> GetActionControlContainers() => Toolbar.ActionContainers.Union(HeaderToolbar.ActionContainers);
    protected override RenderFragment CreateComponent() => MypeApplicationWindowTemplate_23xComponent.Create(this);
    protected override void BeginUpdate() {
        base.BeginUpdate();
        ((ISupportUpdate)Toolbar).BeginUpdate();
    }
    protected override void EndUpdate() {
        ((ISupportUpdate)Toolbar).EndUpdate();
        base.EndUpdate();
    }
    public bool IsActionsToolbarVisible { get; private set; }
    public NavigateBackActionControl NavigateBackActionControl { get; }
    public AccountComponentAdapter AccountComponent { get; }
    public ShowNavigationItemActionControl ShowNavigationItemActionControl { get; }
    public DxToolbarAdapter Toolbar { get; }
    public DxToolbarAdapter HeaderToolbar { get; }
    public string AboutInfoString { get; set; }
    void ISupportActionsToolbarVisibility.SetVisible(bool isVisible) => IsActionsToolbarVisible = isVisible;
}
