using CsvHelper;
using CsvHelper.Configuration;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.BaseImpl;
using System;
using System.Globalization;
using System.Linq;

namespace DXApplication.Module.Extension;
public class MypeViewController : ViewController {

    /// <summary>
    /// Enum for 3 types of action
    /// </summary>
    public enum ActionType { Simple, SingleChoice, Popup }

    /// <summary>
    /// Create one of 3 types of action
    /// </summary>    
    protected ActionBase CreateAction(
    ActionType actionType,
    string caption,
    string description = "",
    string category = "Edit",
    string image = Image.Action_SimpleAction,
    string toolTip = "",
    string confirmation = "",
    Type objectType = null,
    string criteria = "",
    TargetObjectsCriteriaMode criteriaMode = TargetObjectsCriteriaMode.TrueForAll,
    SelectionDependencyType selectionType = SelectionDependencyType.Independent,
    ViewType viewType = ViewType.Any,
    Nesting nesting = Nesting.Any,
    ActionItemPaintStyle paintStyle = ActionItemPaintStyle.Default) {

        var id = $"{GetType()}-{caption}-{description}";
        ActionBase action = actionType switch {
            ActionType.Simple => new SimpleAction(this, id, category),
            ActionType.SingleChoice => new SingleChoiceAction(this, id, category),
            ActionType.Popup => new PopupWindowShowAction(this, id, category),
            _ => new SimpleAction(this, id, category)
        };

        action.Caption = caption;
        action.ImageName = image;
        action.TargetObjectsCriteriaMode = criteriaMode;
        action.SelectionDependencyType = selectionType;
        action.TargetViewNesting = nesting;
        action.TargetViewType = viewType;
        action.PaintStyle = paintStyle;
        if (!string.IsNullOrEmpty(criteria)) action.TargetObjectsCriteria = criteria;
        if (!string.IsNullOrEmpty(toolTip)) action.ToolTip = toolTip;
        if (!string.IsNullOrEmpty(confirmation)) action.ConfirmationMessage = confirmation;
        if (objectType != null) action.TargetObjectType = objectType;

        return action;
    }

    /// <summary>
    /// Add items to single choice action
    /// </summary>    
    protected static void SetSingleChoiceAction(SingleChoiceAction action,
        ChoiceActionItem[] items) {
        action.Items.AddRange(items);
        action.ItemType = SingleChoiceActionItemType.ItemIsOperation;
        action.ShowItemsOnClick = true;
    }

    /// <summary>
    /// Create detail view from object
    /// </summary>    
    protected DetailView CreateDetailView<T>(T item) where T : class {
        var os = Application.CreateObjectSpace(typeof(T));
        var obj = os.GetObject(item);
        return Application.CreateDetailView(os, obj);
    }

    /// <summary>
    /// Create detail view of domain component (picker) from object
    /// </summary>    
    protected DetailView CreatePickerDetailView<T>(T item) where T : class {
        var npos = (NonPersistentObjectSpace)Application.CreateObjectSpace(typeof(T));
        return Application.CreateDetailView(npos, item);
    }

    /// <summary>
    /// Create detail view of domain component
    /// </summary>    
    protected DetailView CreatePickerDetailView<T>() where T : class {
        var npos = (NonPersistentObjectSpace)Application.CreateObjectSpace(typeof(T));
        var item = Activator.CreateInstance(typeof(T));
        return Application.CreateDetailView(npos, item);
    }

    /// <summary>
    /// Create lookup list view (and return source)
    /// </summary>    
    protected ListView CreateLookupListView<T>(out CollectionSourceBase source) where T : class {
        var os = Application.CreateObjectSpace(typeof(T));
        var viewId = Application.FindLookupListViewId(typeof(T));
        source = Application.CreateCollectionSource(os, typeof(T), viewId);
        return Application.CreateListView(viewId, source, true);
    }

    /// <summary>
    /// Create list view (and return source)
    /// </summary>    
    protected ListView CreateListView<T>(out CollectionSourceBase source) where T : class {
        var os = Application.CreateObjectSpace(typeof(T));
        var viewId = Application.FindListViewId(typeof(T));
        source = Application.CreateCollectionSource(os, typeof(T), viewId);
        return Application.CreateListView(viewId, source, true);
    }

    /// <summary>
    /// Import data from csv data
    /// </summary>    
    protected void ImportFromValue(string csvData, Action<CsvReader> action) {
        using StringReader stringReader = new(csvData);
        CsvConfiguration config = new(CultureInfo.InvariantCulture) {
            DetectDelimiter = true
        };
        using CsvReader csvReader = new(stringReader, config);
        csvReader.Read();
        csvReader.ReadHeader();
        try {
            while (csvReader.Read()) {
                action?.Invoke(csvReader);
            }
        } catch {
            Application.ShowViewStrategy.ShowMessage("Có lỗi trong quá trình import", InformationType.Error);
        }
    }

    protected void ImportFromFile(FileData fileData, Action<CsvReader> action) {
        Stream stream = new MemoryStream();
        fileData.SaveToStream(stream);
        stream.Position = 0;
        StreamReader streamReader = new(stream);
        CsvConfiguration config = new(CultureInfo.InvariantCulture) {
            DetectDelimiter = true
        };
        using CsvReader csvReader = new(streamReader, config);
        csvReader.Read();
        csvReader.ReadHeader();
        try {
            while (csvReader.Read()) {
                action?.Invoke(csvReader);
            }
        } catch (Exception e) {
            Application.ShowViewStrategy.ShowMessage($"Có lỗi trong quá trình import. {e.Message}", InformationType.Error);
        }
    }

    protected void ImportFromValue<T>(string csvData, Action<T, CsvReader> action) {
        using StringReader stringReader = new(csvData);
        CsvConfiguration config = new(CultureInfo.InvariantCulture) {
            DetectDelimiter = true
        };
        using CsvReader csvReader = new(stringReader, config);
        csvReader.Read();
        csvReader.ReadHeader();
        try {
            while (csvReader.Read()) {
                var record = ObjectSpace.CreateObject<T>();
                action?.Invoke(record, csvReader);
            }
        } catch (Exception e) {
            Application.ShowViewStrategy.ShowMessage($"Có lỗi trong quá trình import. {e.Message}", InformationType.Error);
        }
    }

    protected void ImportFromFile<T>(FileData csvFile, Action<T, CsvReader> action) {
        Stream stream = new MemoryStream();
        csvFile.SaveToStream(stream);
        stream.Position = 0;
        StreamReader streamReader = new(stream);
        CsvConfiguration config = new(CultureInfo.InvariantCulture) {
            DetectDelimiter = true
        };
        using CsvReader csvReader = new(streamReader, config);
        csvReader.Read();
        csvReader.ReadHeader();
        try {
            while (csvReader.Read()) {
                var record = ObjectSpace.CreateObject<T>();
                action?.Invoke(record, csvReader);
            }
        } catch (Exception e) {
            Application.ShowViewStrategy.ShowMessage($"Có lỗi trong quá trình import. {e.Message}", InformationType.Error);
        }
    }

    protected void ImportAction<T>(Action<T, CsvReader> operation, string description, CsvImporter viewObject = null, string caption = "Nhập từ csv") {
        var action = CreateAction(ActionType.Popup, caption, description, category: "Tools") as PopupWindowShowAction;
        action.ImageName = "Action_Export_ToCSV";
        action.PaintStyle = ActionItemPaintStyle.Image;
        action.SelectionDependencyType = SelectionDependencyType.Independent;
        action.TargetObjectType = typeof(T);

        action.CustomizePopupWindowParams += (s, e) => {
            viewObject ??= new CsvImporter();
            e.View = CreatePickerDetailView(viewObject);
        };

        action.Execute += (s, e) => {
            var importer = e.PopupWindowViewCurrentObject as CsvImporter;

            if (!string.IsNullOrEmpty(importer.Value)) {
                ImportFromValue(importer.Value, operation);
            } else if (importer.FileData != null) {
                ImportFromFile(importer.FileData, operation);
            }

            ObjectSpace.CommitChanges();
            View.RefreshDataSource();
        };
    }

    protected void ImportAction<T1, T2>(Action<T1, CsvReader, T2> operation, string description, CsvImporter viewObject = null, string caption = "Nhập từ csv") where T2 : CsvImporter {
        var action = CreateAction(ActionType.Popup, caption, description, category: "Tools") as PopupWindowShowAction;
        action.ImageName = "Action_Export_ToCSV";
        action.PaintStyle = ActionItemPaintStyle.Image;
        action.SelectionDependencyType = SelectionDependencyType.Independent;
        action.TargetObjectType = typeof(T1);

        action.CustomizePopupWindowParams += (s, e) => {
            viewObject ??= new CsvImporter();
            e.View = CreatePickerDetailView(viewObject);
        };

        action.Execute += (s, e) => {
            var importer = e.PopupWindowViewCurrentObject as T2;

            if (!string.IsNullOrEmpty(importer.Value)) {
                ImportFromValue<T1>(importer.Value, (i, r) => operation?.Invoke(i, r, importer));
            } else if (importer.FileData != null) {
                ImportFromFile<T1>(importer.FileData, (i, r) => operation?.Invoke(i, r, importer));
            }

            ObjectSpace.CommitChanges();
            View.RefreshDataSource();
        };
    }

    protected virtual void Get<T>(string name, CsvReader r, Action<T> a) {
        if (r.TryGetField<T>(name, out T value)) a.Invoke(value);
    }

    protected virtual FileData CreateFile(string fileName, Action<StreamWriter> action) {
        var file = ObjectSpace.CreateObject<FileData>();
        MemoryStream ms = new();
        StreamWriter sw = new(ms) { AutoFlush = true };
        action.Invoke(sw);
        ms.Position = 0;
        file.LoadFromStream(fileName, ms);
        sw.Close();
        return file;
    }
}
