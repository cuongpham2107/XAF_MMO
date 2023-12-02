using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using DXApplication.Module.Extension;
using DXApplication.Module.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXApplication.Module.BusinessObjects;

//[DefaultClassOptions]
[XafDisplayName("")]
//[NavigationItem(Menu.MenuBusiness)]
public class Monitoring : BaseObject, IListViewInline {
    public Monitoring(Session session) : base(session) { }


}
