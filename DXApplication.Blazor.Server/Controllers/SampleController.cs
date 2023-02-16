
using DevExpress.ExpressApp.Actions;
using DXApplication.Module.Extension;

namespace DXApplication.Blazor.Server.Controllers {
    public class SampleController : MypeViewController {
        public SampleController() {
            var action1 = CreateAction(ActionType.Simple, "Copy to clipboard") as SimpleAction;
            action1.Execute += (s, e) => {
                //var js = JsHelper.GetJSRuntime(Application);
                JsHelper.CopyToClipboard(Application, "Hello world");
            };

            var action2 = CreateAction(ActionType.Simple, "Go to link") as SimpleAction;
            action2.Execute += (s, e) => {
                var js = JsHelper.GetJSRuntime(Application);
                JsHelper.OpenLink(Application, js, "https://vnexpress.net");
            };
        }
    }
}
