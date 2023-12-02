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
    [ImageName("worldwide")]
    [NavigationItem(Menu.MenuData)]
    public class Channels : BaseObject
    { 
        public Channels(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
           
        }

        string channel;

        public string Channel
        {
            get => channel;
            set => SetPropertyValue(nameof(Channel), ref channel, value);
        }
        [Association("Driver-Channels")]
        public XPCollection<Driver> Drivers
        {
            get
            {
                return GetCollection<Driver>(nameof(Drivers));
            }
        }

    }
}