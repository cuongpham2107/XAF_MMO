using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
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
    [ImageName("url")]
    [NavigationItem(Menu.MenuData)]
    public class Urls : BaseObject
    { 
        public Urls(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            
        }

        string url;

        public string Url
        {
            get => url;
            set => SetPropertyValue(nameof(Url), ref url, value);
        }
        [Association("Driver-Urls")]
        public XPCollection<Driver> Drivers
        {
            get
            {
                return GetCollection<Driver>(nameof(Drivers));
            }
        }

    }
}