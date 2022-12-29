using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.BaseImpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXApplication.Blazor.Controllers;
public class TestController : ViewController {
	public TestController() {
		SimpleAction();

    }

	private void SimpleAction() {
		var action = new SimpleAction(this, "test_action", "Edit") {
			Caption = "Test", 
			SelectionDependencyType = SelectionDependencyType.RequireSingleObject
		};
		action.Execute += (s, e) => {
			if(View.CurrentObject != null) {
				var obj = View.CurrentObject as BaseObject;
				obj.Delete();
			}
		};
	}
}
