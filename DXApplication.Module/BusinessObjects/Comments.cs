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
    [ImageName("comments")]
    [NavigationItem(Menu.MenuData)]
    public class Comments : BaseObject
    { 
        public Comments(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            
        }

        string comment;

        public string Comment
        {
            get => comment;
            set => SetPropertyValue(nameof(Comment), ref comment, value);
        }
        [Association("Driver-Comments")]
        public XPCollection<Driver> Drivers
        {
            get
            {
                return GetCollection<Driver>(nameof(Drivers));
            }
        }
    }
}