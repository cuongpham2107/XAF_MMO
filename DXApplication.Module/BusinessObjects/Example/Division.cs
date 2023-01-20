using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using DXApplication.Module.Extension;
using System.Linq.Expressions;

namespace DXApplication.Blazor.BusinessObjects;

/// <summary>
/// Class for company division
/// </summary>
[DefaultClassOptions]
[CustomDetailView(FieldsToRemove = new[] { nameof(People) })]
[CustomDetailView(ViewId = $"{nameof(Division)}_DetailView_Full", Tabbed = true)]
[CustomRootListView(DetailViewId = $"{nameof(Division)}_DetailView_Full")]
[CustomNestedListView(nameof(People), FieldsToSum = new[] { "FullName:Count" })]
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
