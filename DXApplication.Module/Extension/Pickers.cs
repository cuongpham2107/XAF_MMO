using DevExpress.ExpressApp.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXApplication.Module.Extension {
    [DomainComponent]
    [XafDisplayName("Chọn giá trị")]
    public class IntPicker : IDomainComponent {
        [XafDisplayName("Chọn giá trị")]
        public int Value { get; set; }
    }

    [DomainComponent]
    [XafDisplayName("Chọn giá trị")]
    public class DecimalPicker : IDomainComponent {
        [XafDisplayName("Chọn giá trị")]
        public decimal Value { get; set; }
    }

    [DomainComponent]
    [XafDisplayName("Chọn giá trị")]
    public class DoublePicker : IDomainComponent {
        [XafDisplayName("Chọn giá trị")]
        public double Value { get; set; }
    }

    [DomainComponent]
    [XafDisplayName("Chọn giá trị")]
    public class BoolPicker : IDomainComponent {
        [XafDisplayName("Chọn giá trị")]
        public bool Value { get; set; }
    }

    [DomainComponent]
    [XafDisplayName("Nhập giá trị")]
    public class StringPicker : IDomainComponent {
        [XafDisplayName("Nhập giá trị")]
        public string Value { get; set; }
    }

    [DomainComponent]
    [XafDisplayName("Chọn giá trị")]
    public class DateTimePicker : IDomainComponent {
        [XafDisplayName("Chọn giá trị")]
        public DateTime Value { get; set; } = DateTime.Now;
    }
}
