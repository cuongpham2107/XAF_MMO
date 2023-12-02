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
[ImageName("hyperlink")]
[NavigationItem(Menu.MenuData)]
public class Link : BaseObject, IListViewPopup {
    public Link(Session session) : base(session) { }

    string url;
    [XafDisplayName(""), ToolTip("")]
    public string Url {
        get => url; set => SetPropertyValue(nameof(Url), ref url, value);
    }

    string label;
    [XafDisplayName(""), ToolTip("")]
    public string Label {
        get => label; set => SetPropertyValue(nameof(Label), ref label, value);
    }

    string keywords;
    [XafDisplayName(""), ToolTip("")]
    public string Keywords {
        get => keywords; set => SetPropertyValue(nameof(Keywords), ref keywords, value);
    }

    LinkType type;
    [XafDisplayName(""), ToolTip("")]
    public LinkType Type {
        get => type; set => SetPropertyValue(nameof(Type), ref type, value);
    }

    //string comments;
    //[XafDisplayName(""), ToolTip("")]
    //[Size(-1)]
    //public string Comments {
    //    get => comments; set => SetPropertyValue(nameof(Comments), ref comments, value);
    //}

    int processCount;
    [XafDisplayName(""), ToolTip("")]
    public int ProcessCount {
        get => processCount; set => SetPropertyValue(nameof(ProcessCount), ref processCount, value);
    }
}

public enum LinkType {
    Referral,
    Direct,
    Channel,
    Video,
    Playlist,
    Website,
    Other
}