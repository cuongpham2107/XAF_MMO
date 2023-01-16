using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace DXApplication.Blazor.BusinessObjects;

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