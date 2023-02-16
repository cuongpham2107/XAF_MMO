using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor;
using Microsoft.JSInterop;
using static System.Net.Mime.MediaTypeNames;

namespace DXApplication.Blazor.Server.Controllers {
    public class JsHelper {
        public static void CopyToClipboard(XafApplication Application, IJSRuntime js, string text, string message = "Văn bản trống") {
            if (string.IsNullOrEmpty(text)) {
                Application.ShowViewStrategy.ShowMessage(message, InformationType.Warning);
                return;
            }
            _ = (js?.InvokeVoidAsync("navigator.clipboard.writeText", text));
            Application.ShowViewStrategy.ShowMessage($"Copied to clipboard: {text}", InformationType.Success);
        }

        public static void CopyToClipboard(XafApplication Application, string text, string message = "Văn bản trống") {
            var js = GetJSRuntime(Application);
            CopyToClipboard(Application, js, text, message);
        }

        public static void OpenLink(XafApplication Application, IJSRuntime js, string url, string message = "Url trống") {
            if (string.IsNullOrEmpty(url)) {
                Application.ShowViewStrategy.ShowMessage(message, InformationType.Warning);
                return;
            }
            _ = (js?.InvokeVoidAsync("open", url, "_blank"));
        }

        public static void OpenLink(XafApplication Application, string url, string message = "Url trống") {
            var js = GetJSRuntime(Application);
            OpenLink(Application, js, url, message);
        }

        public static IJSRuntime GetJSRuntime(XafApplication Application) {
            return (IJSRuntime)((BlazorApplication)Application).ServiceProvider.GetService(typeof(IJSRuntime));
        }
    }
}
