using DevExpress.CodeParser;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using DXApplication.Module.Extension;
using System.Linq.Expressions;

namespace DXApplication.Blazor.BusinessObjects;

/// <summary>
/// Class for personnel
/// </summary>
[DefaultClassOptions]
[CustomDetailView(Tabbed = true)]
//[CustomListViewColumnWidth(new[] { $"{nameof(FullName)}:20%", "Age:30", "DateOfBirth:80" })]
//[CustomDetailView(FieldsToRemove = new[] { nameof(Jobs), nameof(Resources) })]
//[CustomDetailView(ViewId = $"{nameof(Personnel)}_DetailView_Full", Tabbed = true)]
//[CustomListView(DetailViewId = $"{nameof(Personnel)}_DetailView_Full",
//    FieldsToSum = new[] { "FullName:Count", "DateOfBirth:Min,Max", "Age:Min,Max" })]
//[CustomNestedListView(nameof(Jobs))]
//[CustomNestedListView(nameof(Resources))]
public class Personnel : BaseObject {

    public Personnel(Session session) : base(session) { }

    string _fullName;
    [XafDisplayName("")]
    public string FullName {
        get => _fullName;
        set => SetPropertyValue(nameof(FullName), ref _fullName, value);
    }

    DateTime _dateOfBirth;
    [XafDisplayName("")]
    public DateTime DateOfBirth {
        get => _dateOfBirth;
        set => SetPropertyValue(nameof(DateOfBirth), ref _dateOfBirth, value);
    }

    [XafDisplayName("")]
    public int Age => DateTime.Now.Year - DateOfBirth.Year;

    string _address;
    [XafDisplayName("")]
    public string Address {
        get => _address;
        set => SetPropertyValue(nameof(Address), ref _address, value);
    }

    string _phone;
    [XafDisplayName("")]
    [DetailViewLayout("Contact", 1)]
    public string Phone {
        get => _phone;
        set => SetPropertyValue(nameof(Phone), ref _phone, value);
    }

    string _facebook;
    [XafDisplayName("")]
    [DetailViewLayout("Contact", 1)]
    public string Facebook {
        get => _facebook;
        set => SetPropertyValue(nameof(Facebook), ref _facebook, value);
    }
    string _email;
    [XafDisplayName("")]
    [DetailViewLayout("Contact", 1)]
    public string Email {
        get => _email;
        set => SetPropertyValue(nameof(Email), ref _email, value);
    }

    string _twitter;
    [XafDisplayName("")]
    [DetailViewLayout("Contact", 1)]
    public string Twitter {
        get => _twitter;
        set => SetPropertyValue(nameof(Twitter), ref _twitter, value);
    }

    string _telegram;
    [XafDisplayName("")]
    [DetailViewLayout("Contact", 1)]
    public string Telegram {
        get => _telegram;
        set => SetPropertyValue(nameof(Telegram), ref _telegram, value);
    }

    Division _division;
    [XafDisplayName("")]
    [Association("Division-People")]
    public Division Division {
        get => _division;
        set => SetPropertyValue(nameof(Division), ref _division, value);
    }

    [XafDisplayName("")]
    [Association("Personnel-Jobs")]
    public XPCollection<Job> Jobs {
        get {
            return GetCollection<Job>(nameof(Jobs));
        }
    }

    [XafDisplayName("")]
    [Association("Personnel-Resources")]
    public XPCollection<Document> Resources {
        get {
            return GetCollection<Document>(nameof(Resources));
        }
    }

    string _aboutMe;
    [XafDisplayName("")]
    [DetailViewLayout("Tabs", LayoutGroupType.TabbedGroup)]
    [Size(-1)]
    public string AboutMe {
        get => _aboutMe;
        set => SetPropertyValue(nameof(AboutMe), ref _aboutMe, value);
    }
}
