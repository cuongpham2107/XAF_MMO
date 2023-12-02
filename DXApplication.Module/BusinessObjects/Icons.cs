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
    [ImageName("list")]
    [NavigationItem(Menu.MenuData)]
    public class Icons : BaseObject
    { 
        public Icons(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            
        }

        string icon;

        public string Icon
        {
            get => icon;
            set => SetPropertyValue(nameof(Icon), ref icon, value);
        }
        [Association("Driver-Icons")]
        public XPCollection<Driver> Drivers
        {
            get
            {
                return GetCollection<Driver>(nameof(Drivers));
            }
        }
    }
}