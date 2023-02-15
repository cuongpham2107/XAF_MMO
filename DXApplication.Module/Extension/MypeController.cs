using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXApplication.Module.Extension {
    public class MypeViewController : ViewController {
        protected void AddSimpleAction(string caption,
            SimpleActionExecuteEventHandler execution,
            string category = "Edit",
            string image = Image.Action_SimpleAction,
            string toolTip = "",
            Type objectType = null,
            string criteria = "",
            string confirmation = "",
            TargetObjectsCriteriaMode criteriaMode = TargetObjectsCriteriaMode.TrueForAll,
            SelectionDependencyType selectionDependencyType = SelectionDependencyType.Independent,
            ViewType viewType = ViewType.Any,
            Nesting nesting = Nesting.Any) {
            SimpleAction action = new(this, $"{GetType()}-{caption}", category) {
                Caption = caption,
                ImageName = image,
                TargetObjectsCriteriaMode = criteriaMode,
                SelectionDependencyType = selectionDependencyType,
                TargetViewNesting = nesting,
                TargetViewType = viewType,
            };
            if (!string.IsNullOrEmpty(criteria)) action.TargetObjectsCriteria = criteria;
            if (!string.IsNullOrEmpty(toolTip)) action.ToolTip = toolTip;
            if (!string.IsNullOrEmpty(confirmation)) action.ConfirmationMessage = confirmation;
            if (objectType != null) action.TargetObjectType = objectType;

            action.Execute += execution;
        }
    }
}
