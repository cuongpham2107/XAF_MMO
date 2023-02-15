using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DXApplication.Module.Extension;

namespace DXApplication.Module.Controllers {
    public class SampleController : MypeViewController {
        public SampleController() {
            AddSimpleAction(caption: "Press me!", toolTip: "You can press me now", execution: (s, e) => {
                Application.ShowViewStrategy.ShowMessage("Hi! You've pressed me!");
            });
        }
    }
}
