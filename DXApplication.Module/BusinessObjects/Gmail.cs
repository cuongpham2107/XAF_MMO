using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using DevExpress.XtraPrinting.Native;
using DXApplication.Module.Extension;
using DXApplication.Module.Shared;
using System;
using System.Linq;

namespace DXApplication.Module.BusinessObjects;

[DefaultClassOptions]
[XafDisplayName("")]
[ImageName("gmail")]
[XafDefaultProperty(nameof(Email))]
[NavigationItem(Menu.MenuBusiness)]

[Appearance("Live", BackColor = "DodgerBlue", FontColor = "White",FontStyle =System.Drawing.FontStyle.Bold, TargetItems = "Status", Context = "Any", Priority = 1, Criteria = "[Status] = ##Enum#DXApplication.Module.BusinessObjects.GmailStatus,Live#")]
[Appearance("Dead", BackColor = "Red", FontColor = "Black", FontStyle = System.Drawing.FontStyle.Bold, TargetItems = "Status", Context = "Any", Priority = 2, Criteria = "[Status] = ##Enum#DXApplication.Module.BusinessObjects.GmailStatus,Dead#")]
public class Gmail : BaseObject, IListViewPopup {
    public Gmail(Session session) : base(session) { }

    string email;
    [XafDisplayName(""), ToolTip("")]
    public string Email {
        get => email; set => SetPropertyValue(nameof(Email), ref email, value);
    }

    string password;
    [XafDisplayName(""), ToolTip("")]
    public string Password {
        get => password; set => SetPropertyValue(nameof(Password), ref password, value);
    }

    string recoveryEmail;
    [XafDisplayName(""), ToolTip("")]
    public string RecoveryEmail {
        get => recoveryEmail; set => SetPropertyValue(nameof(RecoveryEmail), ref recoveryEmail, value);
    }

    GmailStatus status;
    [XafDisplayName(""), ToolTip("")]
    public GmailStatus Status {
        get => status; set => SetPropertyValue(nameof(Status), ref status, value);
    }

    string profileId;
    [XafDisplayName(""), ToolTip("")]
    public string ProfileId {
        get => profileId; set => SetPropertyValue(nameof(ProfileId), ref profileId, value);
    }
}

public enum GmailStatus {
    Live,
    Dead
}
