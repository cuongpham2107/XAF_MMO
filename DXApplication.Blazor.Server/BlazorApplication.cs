using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ApplicationBuilder;
using DevExpress.ExpressApp.Blazor;
using DevExpress.ExpressApp.Blazor.Templates;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.ClientServer;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Xpo;
using DXApplication.Blazor.Server.Services;

namespace DXApplication.Blazor.Server;

public class DXApplicationBlazorApplication : BlazorApplication {
    public DXApplicationBlazorApplication() {
        ApplicationName = "DXApplication";
        CheckCompatibilityType = DevExpress.ExpressApp.CheckCompatibilityType.DatabaseSchema;
        DatabaseVersionMismatch += DXApplicationBlazorApplication_DatabaseVersionMismatch;

        // TODO: đặt kích thước popup windows bằng 60% bề rộng màn hình
        CustomizeTemplate += (s, e) => {
            if (e.Template is IPopupWindowTemplateSize size) {
                size.MaxWidth = "70vw"; // bề rộng popup luôn bằng 60% bề rộng khuôn hình
                //size.Width = "1000px";
                //size.MaxHeight = "70vh";
                //size.Height = "800px";
            }
        };
    }

    //TODO: sử dụng template tự tạo
    protected override IFrameTemplate CreateDefaultTemplate(TemplateContext context) {
        if (context == TemplateContext.ApplicationWindow) return new Templates.MypeApplicationWindowTemplate { AboutInfoString = AboutInfo.Instance.GetAboutInfoString(this) };
        if (context == TemplateContext.NestedFrame) return new Templates.MypeNestedFrameTemplate();
        if (context == TemplateContext.PopupWindow) return new Templates.MypePopupWindowTemplate();
        if(context == TemplateContext.LogonWindow) return new Templates.MyLogonWindowTemplate();
        return base.CreateDefaultTemplate(context);
    }

    protected override void OnSetupStarted() {
        base.OnSetupStarted();
#if DEBUG
        if (System.Diagnostics.Debugger.IsAttached && CheckCompatibilityType == CheckCompatibilityType.DatabaseSchema) {
            DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;
        }
#endif
    }
    private void DXApplicationBlazorApplication_DatabaseVersionMismatch(object sender, DatabaseVersionMismatchEventArgs e) {
#if EASYTEST
        e.Updater.Update();
        e.Handled = true;
#else
        if (System.Diagnostics.Debugger.IsAttached) {
            e.Updater.Update();
            e.Handled = true;
        } else {
            string message = "Không thể kết nối tới cơ sở dữ liệu. Cơ sở dữ liệu có thể chưa được tạo, cũ hơn, hoặc có cấu trúc khác biệt với bản ứng dụng đang dùng ";

            if (e.CompatibilityError != null && e.CompatibilityError.Exception != null) {
                message += "\r\n\r\nInner exception: " + e.CompatibilityError.Exception.Message;
            }
            throw new InvalidOperationException(message);
        }
#endif
    }
}
