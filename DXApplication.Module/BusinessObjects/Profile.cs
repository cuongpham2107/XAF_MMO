using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using DXApplication.Module.Extension;
using System;
using System.Linq;

namespace DXApplication.Module.BusinessObjects;

//[DefaultClassOptions]
[XafDisplayName("")]
[ImageName("chrome")]
//[NavigationItemProcessId
[CustomListViewColumnWidth(new[] { "PointX:60", "PointY:60", "ProcessId:80","Status:100" })]
[Appearance("Running",BackColor ="LightSkyBlue",FontColor ="Black",Criteria = "[Status] = ##Enum#DXApplication.Module.BusinessObjects.Profile+StatusProfile,Running#", TargetItems ="Status",Context ="ListView")]
[Appearance("Stop", BackColor = "Brown", FontColor = "White", Criteria = "[Status] = ##Enum#DXApplication.Module.BusinessObjects.Profile+StatusProfile,Stop#", TargetItems = "Status", Context = "ListView")]
public class Profile : BaseObject {
    public Profile(Session session) : base(session) { }
    public override void AfterConstruction()
    {
        base.AfterConstruction();
        Status = StatusProfile.Stop;
    }
    int pointY;
    int pointX;
    Proxy proxy;
    string id;
    string processId;
    [XafDisplayName(""), ToolTip("")]
    public string Id
    {
        get => id; set => SetPropertyValue(nameof(Id), ref id, value);
    }

    Driver driver;
    [XafDisplayName("")]
    [Association("Driver-Profiles")]
    public Driver Driver
    {
        get => driver; set => SetPropertyValue(nameof(Driver), ref driver, value);
    }
    [XafDisplayName("")]
    [Association("Proxy-Profiles")]
    public Proxy Proxy
    {
        get => proxy;
        set => SetPropertyValue(nameof(Proxy), ref proxy, value);
    }
    Gmail gmail;
    [XafDisplayName(""), ToolTip("")]
    public Gmail Gmail
    {
        get => gmail; set => SetPropertyValue(nameof(Gmail), ref gmail, value);
    }
    [XafDisplayName(""), ToolTip("")]
    public int PointX
    {
        get => pointX;
        set => SetPropertyValue(nameof(PointX), ref pointX, value);
    }
    [XafDisplayName(""), ToolTip("")]
    public int PointY
    {
        get => pointY;
        set => SetPropertyValue(nameof(PointY), ref pointY, value);
    }
    public string ProcessId
    {
        get => processId;
        set => SetPropertyValue(nameof(ProcessId), ref processId, value);
    }
    StatusProfile status;
    [XafDisplayName(""), ToolTip("")]
    public StatusProfile Status
    {
        get => status;
        set => SetPropertyValue(nameof(Status), ref status, value);
    }

    
    
    
    public enum StatusProfile
    {
        [XafDisplayName("Running...")]
        Running = 0,
        [XafDisplayName("Stop")]
        Stop = 1,
    }
    public enum Script
    {
        [XafDisplayName("Channels")]
        Channels = 0,
        [XafDisplayName("Urls")]
        Urls = 1,
    }

}
