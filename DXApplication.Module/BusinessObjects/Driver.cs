using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using DXApplication.Module.Extension;
using DXApplication.Module.Shared;
using System;
using System.Linq;

namespace DXApplication.Module.BusinessObjects;
[DefaultClassOptions]
[XafDisplayName("")]
[ImageName("google-drive")]
[NavigationItem(Menu.MenuBusiness)]
[XafDefaultProperty(nameof(DisplayName))]
[CustomDetailView(Tabbed = true)]
[CustomListViewColumnWidth(new[] {"DefaultProfile:50", "DefaultProxyProfile:50", "ProfileCount:50", "Status:200" })]
public class Driver : BaseObject {
    public Driver(Session session) : base(session) { }

    public override void AfterConstruction() {
        base.AfterConstruction();
        var gologin = Session.Query<GologinAccount>().FirstOrDefault(a => a.Default);
        GologinAccount = gologin;
        var Config = Session.Query<Configuration>().FirstOrDefault();
        WidthScreen = Config.WidthScreen;
        HightScreen = Config.HightScreen;
        ApiBaseUrl = Config?.ApiBaseUrl;
        ApiCreateProfile = Config?.ApiCreateProfile;
        ApiUpdateProfile = Config?.ApiUpdateProfile;
        ApiStartProfile = Config?.ApiStartProfile;
        ApiDeleteProfile = Config?.ApiDeleteProfile;
        DefaultWorkingPath = Config?.DefaultWorkingPath;
        DefaultProxyProfile = Config == null ? Config.DefaultProxyProfile : 10;
        DefaultProfile = Config == null ? Config.DefaultProfile : 5;
        DefaultGologinPath = Config?.DefaultGologinPath;
        PathOrbita = Config?.PathOrbita;
        PathProfile = Config?.PathProfile;
        PathCookieExtension = Config?.PathCookieExtension;
        PathPasswordExtension = Config?.PathPasswordExtension;
        PathProxyExtension = Config?.PathProxyExtension;
        ChromeCommandLine = Config?.ChromeCommandLine;
        PathSlave = Config?.PathSlave;
        PathWorker = Config?.PathWorker;
        MqttUri = Config?.MqttUri;
        MqttPort = Config.MqttPort;
        WidthScreen = Config.WidthScreen;
        HightScreen = Config.HightScreen;

    }

    [VisibleInListView(false)]
    public string DisplayName => $"{Oid} - {MachineName}";

    int defaultProfile;
    FileData driverIdFile;
    int mqttPort;
    string mqttUri;
    string apiDeleteProfile;
    string apiStartProfile;
    string apiUpdateProfile;
    string apiCreateProfile;
    string pathWorker;
    string pathSlave;
    int hightScreen;
    int widthScreen;
    string apiBaseUrl;
    string machineName;
    [XafDisplayName("")]
    public string MachineName
    {
        get => machineName; set => SetPropertyValue(nameof(MachineName), ref machineName, value);
    }
    string ip;
    [XafDisplayName("")]
    public string Ip
    {
        get => ip; set => SetPropertyValue(nameof(Ip), ref ip, value);
    }
    int defaultProxyProfile;
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
    [PersistentAlias("")]
    public int ProfileCount => Profiles.Count;

    DriverStatus status;
    [XafDisplayName("")]
    [ModelDefault("AllowEdit", "False")]
    public DriverStatus Status
    {
        get => status; set
        {
            status = value;
            OnChanged(nameof(Status));
        }
    }
    [XafDisplayName("")]
    [ModelDefault("AllowEdit", "False")]
    public string MqttUri
    {
        get => mqttUri;
        set => SetPropertyValue(nameof(MqttUri), ref mqttUri, value);
    }
    [XafDisplayName("")]
    [ModelDefault("AllowEdit", "False")]
    public int MqttPort
    {
        get => mqttPort;
        set => SetPropertyValue(nameof(MqttPort), ref mqttPort, value);
    }
    [XafDisplayName("")]

    public int WidthScreen
    {
        get => widthScreen;
        set => SetPropertyValue(nameof(WidthScreen), ref widthScreen, value);
    }
    [XafDisplayName("")]
    public int HightScreen
    {
        get => hightScreen;
        set => SetPropertyValue(nameof(HightScreen), ref hightScreen, value);
    }
    [XafDisplayName(""), ToolTip("")]
    [VisibleInListView(false)]
    public string ApiBaseUrl
    {
        get => apiBaseUrl;
        set => SetPropertyValue(nameof(ApiBaseUrl), ref apiBaseUrl, value);
    }
    [XafDisplayName(""), ToolTip("")]
    [VisibleInListView(false)]
    public string ApiCreateProfile
    {
        get => apiCreateProfile;
        set => SetPropertyValue(nameof(ApiCreateProfile), ref apiCreateProfile, value);
    }
    [XafDisplayName(""), ToolTip("")]
    [VisibleInListView(false)]
    public string ApiUpdateProfile
    {
        get => apiUpdateProfile;
        set => SetPropertyValue(nameof(ApiUpdateProfile), ref apiUpdateProfile, value);
    }
    [XafDisplayName(""), ToolTip("")]
    [VisibleInListView(false)]
    public string ApiStartProfile
    {
        get => apiStartProfile;
        set => SetPropertyValue(nameof(ApiStartProfile), ref apiStartProfile, value);
    }
    [XafDisplayName(""), ToolTip("")]
    [VisibleInListView(false)]
    public string ApiDeleteProfile
    {
        get => apiDeleteProfile;
        set => SetPropertyValue(nameof(ApiDeleteProfile), ref apiDeleteProfile, value);
    }
    string defaultWorkingPath;
    [XafDisplayName(""), ToolTip("")]
    [VisibleInListView(false)]
    public string DefaultWorkingPath
    {
        get => defaultWorkingPath; set => SetPropertyValue(nameof(DefaultWorkingPath), ref defaultWorkingPath, value);
    }

    string defaultGologinPath;
    [XafDisplayName(""), ToolTip("")]
    [VisibleInListView(false)]
    public string DefaultGologinPath
    {
        get => defaultGologinPath; set => SetPropertyValue(nameof(DefaultGologinPath), ref defaultGologinPath, value);
    }

    string pathOrbita;
    [XafDisplayName(""), ToolTip("")]
    [VisibleInListView(false)]
    public string PathOrbita
    {
        get => pathOrbita; set => SetPropertyValue(nameof(PathOrbita), ref pathOrbita, value);
    }

    string pathProfile;
    [XafDisplayName(""), ToolTip("")]
    [VisibleInListView(false)]
    public string PathProfile
    {
        get => pathProfile; set => SetPropertyValue(nameof(PathProfile), ref pathProfile, value);
    }
    string pathCookieExtension;
    [XafDisplayName(""), ToolTip("")]
    [VisibleInListView(false)]
    public string PathCookieExtension
    {
        get => pathCookieExtension; set => SetPropertyValue(nameof(PathCookieExtension), ref pathCookieExtension, value);
    }
    string pathPasswordExtension;
    [XafDisplayName(""), ToolTip("")]
    [VisibleInListView(false)]
    public string PathPasswordExtension
    {
        get => pathPasswordExtension; set => SetPropertyValue(nameof(PathPasswordExtension), ref pathPasswordExtension, value);
    }

    string pathProxyExtension;
    [XafDisplayName(""), ToolTip("")]
    [VisibleInListView(false)]
    public string PathProxyExtension
    {
        get => pathProxyExtension; set => SetPropertyValue(nameof(PathProxyExtension), ref pathProxyExtension, value);
    }

    string chromeCommandLine;
    [XafDisplayName(""), ToolTip("")]
    [Size(SizeAttribute.Unlimited)]
    [VisibleInListView(false)]
    public string ChromeCommandLine
    {
        get => chromeCommandLine; set => SetPropertyValue(nameof(ChromeCommandLine), ref chromeCommandLine, value);
    }

    GologinAccount gologinAccount;
    [XafDisplayName(""), ToolTip("")]
    public GologinAccount GologinAccount
    {
        get => gologinAccount; set => SetPropertyValue(nameof(GologinAccount), ref gologinAccount, value);
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

    [XafDisplayName(""), ToolTip("")]
    public FileData DriverIdFile
    {
        get => driverIdFile;
        set => SetPropertyValue(nameof(DriverIdFile), ref driverIdFile, value);
    }

    //bool running;
    //[XafDisplayName(""), ToolTip("")]
    //[ModelDefault("AllowEdit", "False")]
    //[PersistentAlias("")]
    //public bool Running {
    //    get => running; set {
    //        running = value;
    //        OnChanged(nameof(Running));
    //    }
    //}

    [XafDisplayName("")]
    [Association("Driver-Profiles")]
    [VisibleInListView(false)]
    public XPCollection<Profile> Profiles {
        get {
            return GetCollection<Profile>(nameof(Profiles));
        }
    }
    [Association("Driver-Keywords")]
    [VisibleInListView(false)]
    public XPCollection<Keywords> Keywords
    {
        get
        {
            return GetCollection<Keywords>(nameof(Keywords));
        }
    }
    [Association("Driver-Urls")]
    [VisibleInListView(false)]
    public XPCollection<Urls> Urls
    {
        get
        {
            return GetCollection<Urls>(nameof(Urls));
        }
    }
    [Association("Driver-Channels")]
    [VisibleInListView(false)]
    public XPCollection<Channels> Channels
    {
        get
        {
            return GetCollection<Channels>(nameof(Channels));
        }
    }
    [Association("Driver-Comments")]
    [VisibleInListView(false)]
    public XPCollection<Comments> Comments
    {
        get
        {
            return GetCollection<Comments>(nameof(Comments));
        }
    }
    [Association("Driver-Icons")]
    [VisibleInListView(false)]
    public XPCollection<Icons> Icons
    {
        get
        {
            return GetCollection<Icons>(nameof(Icons));
        }
    }
}

public enum DriverStatus {
    [XafDisplayName("New")]
    New,
    [XafDisplayName("Authenticated")]
    Authenticated,
    [XafDisplayName("Config Successfully")]
    ConfigSuccessfully,
}
