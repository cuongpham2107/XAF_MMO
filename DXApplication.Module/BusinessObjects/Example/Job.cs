using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using DXApplication.Module.Extension;

namespace DXApplication.Blazor.BusinessObjects;

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
