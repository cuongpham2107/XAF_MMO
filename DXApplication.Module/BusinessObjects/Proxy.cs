using DevExpress.ExpressApp.DC;
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
[ImageName("vpn")]
[NavigationItem(Menu.MenuBusiness)]
[XafDefaultProperty(nameof(Ip))]
[CustomListViewColumnWidth(new[] {"Port:100"})]
public class Proxy : BaseObject, IListViewPopup {
    public Proxy(Session session) : base(session) { }

    protected override void OnLoaded()
    {
        base.OnLoaded();
        ProfileCount();
    }

    protected override void OnSaved()
    {
        base.OnSaved();
        ProfileCount();
    }
    private void ProfileCount()
    {
        Count = Profiles.Count;
    }

    int count;
    string ip;
    [XafDisplayName(""), ToolTip("")]
    public string Ip
    {
        get => ip; set => SetPropertyValue(nameof(Ip), ref ip, value);
    }

    int port;
    [XafDisplayName(""), ToolTip("")]
    public int Port
    {
        get => port; set => SetPropertyValue(nameof(Port), ref port, value);
    }

    string user;
    [XafDisplayName(""), ToolTip("")]
    public string User
    {
        get => user; set => SetPropertyValue(nameof(User), ref user, value);
    }

    string password;
    [XafDisplayName(""), ToolTip("")]
    public string Password
    {
        get => password; set => SetPropertyValue(nameof(Password), ref password, value);
    }

    DateTime createdOn;
    [XafDisplayName(""), ToolTip("")]
    public DateTime CreatedOn
    {
        get => createdOn; set => SetPropertyValue(nameof(CreatedOn), ref createdOn, value);
    }

    DateTime expiredOn;
    [XafDisplayName(""), ToolTip("")]
    public DateTime ExpiredOn
    {
        get => expiredOn; set => SetPropertyValue(nameof(ExpiredOn), ref expiredOn, value);
    }
    [XafDisplayName(""), ToolTip("")]
    public int Count
    {
        get => count;
        set
        {
            SetPropertyValue(nameof(Count), ref count, value);
        }
         
    }
    
    [Association("Proxy-Profiles")]
    public XPCollection<Profile> Profiles
    {
        get
        {
            return GetCollection<Profile>(nameof(Profiles));
        }
    }
}
