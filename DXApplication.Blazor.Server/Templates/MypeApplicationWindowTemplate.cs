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

namespace DXApplication.Blazor.Server.Templates;

public class MypeApplicationWindowTemplate : WindowTemplateBase, ISupportActionsToolbarVisibility, ISelectionDependencyToolbar {
    public MypeApplicationWindowTemplate() {
        NavigateBackActionControl = new NavigateBackActionControl();
        AddActionControl(NavigateBackActionControl);
        AccountComponent = new AccountComponentAdapter();
        AddActionControls(AccountComponent.ActionControls);
        ShowNavigationItemActionControl = new ShowNavigationItemActionControl();
        AddActionControl(ShowNavigationItemActionControl);

        IsActionsToolbarVisible = true;

        // TODO: thay đổi vị trí các container trên thanh action bar của List view
        Toolbar = new DxToolbarAdapter(new DxToolbarModel());
        Toolbar.ToolbarModel.CssClass = "tool-bar";
        //Toolbar.ToolbarModel.SizeMode = DevExpress.Blazor.SizeMode.Large;

        // các khu vực nằm bên tay trái
        Toolbar.AddActionContainer("Close"); // nút Close chuyển lên đầu tiên
        Toolbar.AddActionContainer(nameof(PredefinedCategory.ObjectsCreation));
        Toolbar.AddActionContainer(nameof(PredefinedCategory.Save));
        Toolbar.AddActionContainer(nameof(PredefinedCategory.UndoRedo));
        Toolbar.AddActionContainer(nameof(PredefinedCategory.Edit));
        Toolbar.AddActionContainer(nameof(PredefinedCategory.RecordEdit));

        // các khu vực chuyển sang bên tay phải
        Toolbar.AddActionContainer(nameof(PredefinedCategory.Tools), DevExpress.Blazor.ToolbarItemAlignment.Right);
        Toolbar.AddActionContainer(nameof(PredefinedCategory.RecordsNavigation), DevExpress.Blazor.ToolbarItemAlignment.Right);
        Toolbar.AddActionContainer(nameof(PredefinedCategory.Export), DevExpress.Blazor.ToolbarItemAlignment.Right);
        Toolbar.AddActionContainer(nameof(PredefinedCategory.Reports), DevExpress.Blazor.ToolbarItemAlignment.Right);
        Toolbar.AddActionContainer(nameof(PredefinedCategory.Search), DevExpress.Blazor.ToolbarItemAlignment.Right);
        Toolbar.AddActionContainer(nameof(PredefinedCategory.Filters), DevExpress.Blazor.ToolbarItemAlignment.Right);
        Toolbar.AddActionContainer(nameof(PredefinedCategory.FullTextSearch), DevExpress.Blazor.ToolbarItemAlignment.Right);
        Toolbar.AddActionContainer("Diagnostic", DevExpress.Blazor.ToolbarItemAlignment.Right);
        Toolbar.AddActionContainer(nameof(PredefinedCategory.View), DevExpress.Blazor.ToolbarItemAlignment.Right);
        Toolbar.AddActionContainer(nameof(PredefinedCategory.Unspecified), DevExpress.Blazor.ToolbarItemAlignment.Right);
    }
    protected override IEnumerable<IActionControlContainer> GetActionControlContainers() => Toolbar.ActionContainers;
    protected override RenderFragment CreateComponent() => MypeApplicationWindowTemplateComponent.Create(this);
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
    public string AboutInfoString { get; set; }
    void ISupportActionsToolbarVisibility.SetVisible(bool isVisible) => IsActionsToolbarVisible = isVisible;
}
