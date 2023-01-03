using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using DXApplication.Module.Extension;

namespace DXApplication.Blazor.BusinessObjects;

[DefaultClassOptions]
//[CustomListView(AllowEdit = true, AllowDelete = false, AllowNew = true)]
//[CustomDetailView(null, AllowDelete = false, AllowNew = false, AllowEdit = true)]
//[CustomDetailView("", FieldsToRemove = new[] {"People"})]
public class Division : BaseObject {
    public Division(Session session) : base(session) { }

    string _name;
    [XafDisplayName("")]
    public string Name {
        get => _name;
        set => SetPropertyValue(nameof(Name), ref _name, value);
    }

    string _office;
    [XafDisplayName("")]
    public string Office {
        get => _office;
        set => SetPropertyValue(nameof(Office), ref _office, value);
    }

    [XafDisplayName("")]
    [Association("Division-People")]
    [CustomNestedListView(
        AllowEdit = true, AllowLink = false, AllowUnlink = false,
        FieldsToSort = new[] { "FullName.", "DateOfBirth", "Phone." },
        FieldsToGroup = new[] { "Division" })]

    public XPCollection<Personnel> People {
        get {
            return GetCollection<Personnel>(nameof(People));
        }
    }
}

[DefaultClassOptions]
//[CustomDetailView("Personnel_DetailView_NoJobs", FieldsToRemove = new[] { "Jobs" }, AllowEdit = false)]
//[CustomListView(AllowDelete = true, AllowNew = true)]
//[CustomDetailView(null, FieldsToRemove = new[] { "Jobs" })]
//[CustomListView(FieldsToGroup = new[] { "Division" }, FieldsToSort = new[] { "DateOfBirth", "Address" })]
//[Readonly(Fields = new[] {"FullName", "DateOfBirth", "Address"}, IsReversed = false)]
//[CustomDetailView(FieldsReadonly = new[] {"FullName", "Address"})]
[CustomListView(ViewId = "Personnel_ListView_New")]
public class Personnel : BaseObject, IListViewPopup {

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
    public string Phone {
        get => _phone;
        set => SetPropertyValue(nameof(Phone), ref _phone, value);
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
    [CustomNestedListView(AllowEdit = true)]
    public XPCollection<Job> Jobs {
        get {
            return GetCollection<Job>(nameof(Jobs));
        }
    }
}

[DefaultClassOptions]
public class Job : BaseObject, IListViewInline {
    public Job(Session session) : base(session) { }

    string _name;
    [XafDisplayName("")]
    public string Name {
        get => _name;
        set => SetPropertyValue(nameof(Name), ref _name, value);
    }

    string _description;
    [XafDisplayName("")]
    public string Description {
        get => _description;
        set => SetPropertyValue(nameof(Description), ref _description, value);
    }

    DateTime _dueDate;
    [XafDisplayName("")]
    public DateTime DueDate {
        get => _dueDate;
        set => SetPropertyValue(nameof(DueDate), ref _dueDate, value);
    }

    bool _completed;
    [XafDisplayName("")]
    public bool Completed {
        get => _completed;
        set => SetPropertyValue(nameof(Completed), ref _completed, value);
    }

    Personnel _personnel;
    [XafDisplayName("")]
    [Association("Personnel-Jobs")]
    public Personnel Personnel {
        get => _personnel;
        set => SetPropertyValue(nameof(Personnel), ref _personnel, value);
    }
}
