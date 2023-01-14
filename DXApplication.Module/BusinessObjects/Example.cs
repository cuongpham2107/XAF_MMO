using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using DXApplication.Module.Extension;

namespace DXApplication.Blazor.BusinessObjects;

/// <summary>
/// Class for company division
/// </summary>
[DefaultClassOptions]
[CustomDetailView(FieldsToRemove = new[] { nameof(People) })]
[CustomDetailView(ViewId = $"{nameof(Division)}_DetailView_Full", Tabbed = true)]
[CustomListView(DetailViewId = $"{nameof(Division)}_DetailView_Full")]
[CustomNestedListView(nameof(People))]
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
    [DetailViewLayout("Tabs", LayoutGroupType.TabbedGroup)]
    public XPCollection<Personnel> People {
        get {
            return GetCollection<Personnel>(nameof(People));
        }
    }

    //[XafDisplayName("")]
    //[Association("Division-Properties")]
    //public XPCollection<Property> Properties {
    //    get {
    //        return GetCollection<Property>(nameof(Properties));
    //    }
    //}
}

/// <summary>
/// Class for personnel
/// </summary>
[DefaultClassOptions]
[CustomDetailView(FieldsToRemove = new[] { nameof(Jobs), nameof(Resources) })]
[CustomDetailView(ViewId = $"{nameof(Personnel)}_DetailView_Full", Tabbed = true)]
[CustomListView(DetailViewId = $"{nameof(Personnel)}_DetailView_Full")]
[CustomNestedListView(nameof(Jobs))]
[CustomNestedListView(nameof(Resources))]
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
}

/// <summary>
/// Job for person
/// </summary>
[DefaultClassOptions]
public class Job : BaseObject, IListViewPopup, INestedListViewInline {
    public Job(Session session) : base(session) { }

    string _name;
    [XafDisplayName("")]
    public string Name {
        get => _name;
        set => SetPropertyValue(nameof(Name), ref _name, value);
    }

    string _description;
    [XafDisplayName("")]
    [Size(-1)]
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

/// <summary>
/// Resource for person
/// </summary>
[DefaultClassOptions]
public class Document : BaseObject,IListViewPopup, INestedListViewInline {
    public Document(Session session) : base(session) { }

    string _name;
    [XafDisplayName("")]
    public string Name {
        get => _name;
        set => SetPropertyValue(nameof(Name), ref _name, value);
    }

    FileData _fileData;
    [XafDisplayName("")]
    public FileData FileData {
        get => _fileData;
        set => SetPropertyValue(nameof(FileData), ref _fileData, value);
    }

    string _link;
    [XafDisplayName("")]
    public string Link {
        get => _link;
        set => SetPropertyValue(nameof(Link), ref _link, value);
    }

    Personnel _personnel;
    [XafDisplayName("")]
    [Association("Personnel-Resources")]
    public Personnel Personnel {
        get => _personnel;
        set => SetPropertyValue(nameof(Personnel), ref _personnel, value);
    }

    string _description;
    [XafDisplayName("")]
    public string Description {
        get => _description;
        set => SetPropertyValue(nameof(Description), ref _description, value);
    }
}

[DefaultClassOptions]
public class Property : BaseObject {
    public Property(Session session) : base(session) { }

    string _name;
    [XafDisplayName("")]
    public string Name {
        get => _name;
        set => SetPropertyValue(nameof(Name), ref _name, value);
    }

    Division _division;
    [XafDisplayName("")]
    //[Association("Division-Properties")]
    public Division Division {
        get => _division;
        set => SetPropertyValue(nameof(Division), ref _division, value);
    }
}