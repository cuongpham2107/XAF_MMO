using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DXApplication.Module.Shared;
using System;
using System.Linq;

namespace DXApplication.Module.BusinessObjects;

[DefaultClassOptions]
[ImageName("settings")]
[NavigationItem(Menu.MenuBusiness)]
[RuleObjectExists("AnotherSingletonExists", DefaultContexts.Save, "True", InvertResult = true,
    CustomMessageTemplate = "Chỉ tạo được một bản ghi.")]
//[RuleCriteria("CannotDeleteSingleton", DefaultContexts.Delete, "False", CustomMessageTemplate = "Không thể xóa cấu hình.")]
public class Configuration : BaseObject {
    public Configuration(Session session) : base(session) { }

    public override void AfterConstruction() {
        base.AfterConstruction();
       
        ApiBaseUrl = "https://app.kennatech.vn/mmo-api/api/";
        ApiCreateProfile = "https://api.gologin.com/browser";
        ApiUpdateProfile = "https://api.gologin.com/browser/fingerprint/multi";
        ApiStartProfile = "http://localhost:36912/browser/start-profile";
        ApiDeleteProfile = "https://api.gologin.com/browser/{id}";
        DefaultWorkingPath = "D:";
        DefaultProxyProfile = 10;
        DefaultProfile = 5;
        MqttUri = "app.kennatech.vn";
        MqttPort = 1883;
        WidthScreen = 1920;
        HightScreen = 1080;
        DefaultGologinPath = "{DefaultWorkingPath}\\Gologin";
        PathOrbita = "{PathGologin}\\Browser\\orbita-browser-115\\chrome.exe";
        PathProfile = "{PathGologin}\\Profiles\\{id}";
        PathCookieExtension = "{PathGologin}\\Extensions\\cookies-ext\\{id}";
        PathPasswordExtension = "{PathGologin}\\Extensions\\passwords-ext\\{id}";
        PathProxyExtension = "{PathGologin}\\Extensions\\chrome-extensions\\{id}";
        ChromeCommandLine = "--user-data-dir=\"{PathProfile}\" --disable-encryption --donut-pie=undefined --font-masking-mode=2 --load-extension=\"{PathCookieExtension},{PathPasswordExtension},{PathProxyExtension}\" --lang=en-US --restore-last-session --flag-switches-begin --flag-switches-end";
        PathSlave = "{DefaultWorkingPath}\\Release\\Slave\\Slave.exe";
        PathWorker = "{DefaultWorkingPath}\\Release\\Slave\\Worker.exe";
    }
    int defaultProfile;
    string pathWorker;
    string pathSlave;
    string chromeCommandLine;
    string pathOrbita;
    string pathProfile;
    string pathProxyExtension;
    string pathPasswordExtension;
    string pathCookieExtension;
    string apiUpdateProfile;
    string defaultGologinPath;
    string apiBaseUrl;
    string apiCreateProfile;
    string apiStartProfile;
    string apiDeleteProfile;
    string defaultWorkingPath;
    string mqttUri;
    int mqttPort;
    int hightScreen;
    int widthScreen;
    int defaultProxyProfile;

    [XafDisplayName(""), ToolTip("")]
    public string ApiCreateProfile
    {
        get => apiCreateProfile; set => SetPropertyValue(nameof(ApiCreateProfile), ref apiCreateProfile, value);
    }
    [XafDisplayName(""), ToolTip("")]
    public string ApiUpdateProfile
    {
        get => apiUpdateProfile;
        set => SetPropertyValue(nameof(ApiUpdateProfile), ref apiUpdateProfile, value);
    }
    [XafDisplayName(""), ToolTip("")]
    public string ApiStartProfile
    {
        get => apiStartProfile; set => SetPropertyValue(nameof(ApiStartProfile), ref apiStartProfile, value);
    }
    [XafDisplayName(""), ToolTip("")]
    public string ApiDeleteProfile
    {
        get => apiDeleteProfile; set => SetPropertyValue(nameof(ApiDeleteProfile), ref apiDeleteProfile, value);
    }
    [XafDisplayName(""), ToolTip("")]
    public string DefaultWorkingPath
    {
        get => defaultWorkingPath; set => SetPropertyValue(nameof(DefaultWorkingPath), ref defaultWorkingPath, value);
    }
    [XafDisplayName(""), ToolTip("")]
    public string DefaultGologinPath
    {
        get => defaultGologinPath; set => SetPropertyValue(nameof(DefaultGologinPath), ref defaultGologinPath, value);
    }
    [XafDisplayName(""), ToolTip("")]
    public int DefaultProxyProfile
    {
        get => defaultProxyProfile; set => SetPropertyValue(nameof(DefaultProxyProfile), ref defaultProxyProfile, value);
    }
    
    public int DefaultProfile
    {
        get => defaultProfile;
        set => SetPropertyValue(nameof(DefaultProfile), ref defaultProfile, value);
    }
    [XafDisplayName(""), ToolTip("")]
    public string ApiBaseUrl
    {
        get => apiBaseUrl;
        set => SetPropertyValue(nameof(ApiBaseUrl), ref apiBaseUrl, value);
    }
    [XafDisplayName(""), ToolTip("")]
    public string MqttUri
    {
        get => mqttUri; set => SetPropertyValue(nameof(MqttUri), ref mqttUri, value);
    }
    [XafDisplayName(""), ToolTip("")]
    public int MqttPort
    {
        get => mqttPort; set => SetPropertyValue(nameof(MqttPort), ref mqttPort, value);
    }
    [XafDisplayName(""), ToolTip("")]
    public int WidthScreen
    {
        get => widthScreen;
        set => SetPropertyValue(nameof(WidthScreen), ref widthScreen, value);
    }
    [XafDisplayName(""), ToolTip("")]
    public int HightScreen
    {
        get => hightScreen;
        set => SetPropertyValue(nameof(HightScreen), ref hightScreen, value);
    }
    [XafDisplayName(""), ToolTip("")]
    public string PathOrbita
    {
        get => pathOrbita; set => SetPropertyValue(nameof(PathOrbita), ref pathOrbita, value);
    }
    [XafDisplayName(""), ToolTip("")]
    public string PathProfile
    {
        get => pathProfile; set => SetPropertyValue(nameof(PathProfile), ref pathProfile, value);
    }
    [XafDisplayName(""), ToolTip("")]
    public string PathCookieExtension
    {
        get => pathCookieExtension;
        set => SetPropertyValue(nameof(PathCookieExtension), ref pathCookieExtension, value);
    }
    [XafDisplayName(""), ToolTip("")]
    public string PathPasswordExtension
    {
        get => pathPasswordExtension;
        set => SetPropertyValue(nameof(PathPasswordExtension), ref pathPasswordExtension, value);
    }
    [XafDisplayName(""), ToolTip("")]
    public string PathProxyExtension
    {
        get => pathProxyExtension;
        set => SetPropertyValue(nameof(PathProxyExtension), ref pathProxyExtension, value);
    }
    [XafDisplayName(""), ToolTip("")]
    [Size(-1)]
    public string ChromeCommandLine
    {
        get => chromeCommandLine;
        set => SetPropertyValue(nameof(ChromeCommandLine), ref chromeCommandLine, value);
    }
    [XafDisplayName(""), ToolTip("")]
    public string PathSlave
    {
        get => pathSlave;
        set => SetPropertyValue(nameof(PathSlave), ref pathSlave, value);
    }
    [XafDisplayName(""), ToolTip("")]
    public string PathWorker
    {
        get => pathWorker;
        set => SetPropertyValue(nameof(PathWorker), ref pathWorker, value);
    }
}