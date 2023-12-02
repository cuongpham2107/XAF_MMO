using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DXApplication.Module.Extension;
using DXApplication.Module.Shared;
using System;
using System.Linq;

namespace DXApplication.Module.BusinessObjects;

[DefaultClassOptions]
[XafDisplayName("")]
[ImageName("profile")]
[NavigationItem(Menu.MenuBusiness)]
[CustomListViewColumnWidth(new[] {"Default:100", "Name:250"})]
public class GologinAccount : BaseObject, IListViewInline {
    public GologinAccount(Session session) : base(session) { }

    string name;
    [XafDisplayName(""), ToolTip("")]
    public string Name {
        get => name; set => SetPropertyValue(nameof(Name), ref name, value);
    }

    string accessToken;
    [XafDisplayName(""), ToolTip("")]
    [Size(SizeAttribute.Unlimited)]
    public string AccessToken {
        get => accessToken; set => SetPropertyValue(nameof(AccessToken), ref accessToken, value);
    }

    bool @default;
    [XafDisplayName(""), ToolTip("")]
    public bool Default {
        get => @default; set => SetPropertyValue(nameof(Default), ref @default, value);
    }
}
