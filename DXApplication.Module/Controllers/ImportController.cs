using DevExpress.ExpressApp;
using DXApplication.Module.BusinessObjects;
using DXApplication.Module.Extension;
using System;
using System.Linq;

namespace DXApplication.Module.Controllers;
public class ImportController : MypeViewController {
    public ImportController() {
        TargetViewNesting = Nesting.Root;
        TargetViewType = ViewType.ListView;

        ImportGmail();
        ImportProxy();
        ImportLink();
        ImportKeyword();
        ImportChannel();
        ImportUrl();
        ImportComment();
        ImportIcon();
    }
    void ImportKeyword()
    {
        ImportAction<Keywords>((i, r) =>
        {
            Get<string>("Keyword", r, o => i.Keyword = o);
        },
        "Keywords", new() { Header = "Keyword" });
    }
    void ImportChannel()
    {
        ImportAction<Channels>((i, r) =>
        {
            Get<string>("Channel", r, o => i.Channel = o);
        },
        "Channels", new() { Header = "Channel" });
    }
    void ImportUrl()
    {
        ImportAction<Urls>((i, r) =>
        {
            Get<string>("Url", r, o => i.Url = o);
        },
        "Urls", new() { Header = "Url" });
    }
    void ImportComment()
    {
        ImportAction<Comments>((i, r) =>
        {
            Get<string>("Comment", r, o => i.Comment = o);
        },
        "Comments", new() { Header = "Comment" });
    }
    void ImportIcon()
    {
        ImportAction<Icons>((i, r) =>
        {
            Get<string>("Icon", r, o => i.Icon = o);
        },
        "Icons", new() { Header = "Icon" });
    }
    private void Get<T>(object value)
    {
        throw new NotImplementedException();
    }
    void ImportGmail() {
        ImportAction<Gmail>((i, r) => {            
            Get<string>("Password", r, o => i.Password = o);
            Get<string>("RecoveryEmail", r, o => i.RecoveryEmail = o);
            Get<string>("Email", r, o => i.Email = o);
        }, "Gmail", new() { Header = "Email; Password; RecoveryEmail" });
    }

    void ImportProxy() {
        ImportAction<Proxy>((i, r) => {
            Get<string>("Ip", r, o => i.Ip = o);
            Get<int>("Port", r, o => i.Port = o);
            Get<string>("User", r, o => i.User = o);
            Get<string>("Password", r, o => i.Password = o);
            Get<DateTime>("CreatedOn", r, o => i.CreatedOn = o);
            Get<DateTime>("ExpiredOn", r, o => i.ExpiredOn = o);
        }, "Proxy", new() { Header = "Ip, Port, User, Password, CreatedOn, ExpiredOn" });
    }

    void ImportLink() {
        ImportAction<BusinessObjects.Link>((i, r) => {
            
        }, "Link", new() { Header = ""});
    }
}
