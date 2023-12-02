using DXApplication.Module.BusinessObjects;
using DXApplication.Module.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXApplication.Module.Controllers;
public class GmailController : MypeViewController {
    public GmailController() {
        TargetObjectType = typeof(Gmail);
    }
}
