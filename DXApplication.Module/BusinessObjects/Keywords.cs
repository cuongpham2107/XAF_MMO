using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DXApplication.Module.Extension;
using DXApplication.Module.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DXApplication.Module.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("")]
    [ImageName("key")]
    [NavigationItem(Menu.MenuData)]

    public class Keywords : BaseObject, IListViewPopup
    {
        public Keywords(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();

        }

        string keyword;

        public string Keyword
        {
            get => keyword;
            set => SetPropertyValue(nameof(Keyword), ref keyword, value);
        }
        [Association("Driver-Keywords")]
        public XPCollection<Driver> Drivers
        {
            get
            {
                return GetCollection<Driver>(nameof(Drivers));
            }
        }
    }
}