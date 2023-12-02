using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;

namespace DXApplication.Module.Extension;

[DomainComponent]
[XafDisplayName("Nhập giá trị")]
public class IntPicker : IDomainComponent {
    [XafDisplayName("Giá trị")]
    public int Value { get; set; }
}

[DomainComponent]
[XafDisplayName("Nhập giá trị")]
public class DecimalPicker : IDomainComponent {
    [XafDisplayName("Giá trị")]
    public decimal Value { get; set; }
}

[DomainComponent]
[XafDisplayName("Nhập giá trị")]
public class DoublePicker : IDomainComponent {
    [XafDisplayName("Giá trị")]
    public double Value { get; set; }
}

[DomainComponent]
[XafDisplayName("Chọn giá trị")]
public class BoolPicker : IDomainComponent {
    [XafDisplayName("Giá trị")]
    public bool Value { get; set; }
}

[DomainComponent]
[XafDisplayName("Chọn thời gian")]
public class DateTimePicker : IDomainComponent {
    [XafDisplayName("Thời gian")]
    public DateTime Value { get; set; } = DateTime.Now;
}

[DomainComponent]
[XafDisplayName("Nhập giá trị")]
public class StringPicker : IDomainComponent {
    [XafDisplayName("Giá trị")]
    public string Value { get; set; }
}

[DomainComponent]
[XafDisplayName("Nhập dữ liệu")]
public class TextPicker : IDomainComponent {
    [XafDisplayName("Dữ liệu")]
    [FieldSize(FieldSizeAttribute.Unlimited)]
    public string Value { get; set; }
}

[DomainComponent]
[XafDisplayName("Chọn file")]
public class FilePicker : IDomainComponent {

    [XafDisplayName("Chọn file")]
    public FileData? File { get; set; }
}

[DomainComponent]
[XafDisplayName("Nhập dữ liệu")]
public class CsvImporter : IDomainComponent {
    [XafDisplayName("Chọn file")]
    public FileData? FileData { get; set; }

    [XafDisplayName("Dữ liệu")]
    [FieldSize(FieldSizeAttribute.Unlimited)]
    public string Value { get; set; }

    [ModelDefault(Model.PropAllowEdit, Model.ValueFalse)]
    [XafDisplayName("Csv Header")]
    public string Header { get; init; }
}
