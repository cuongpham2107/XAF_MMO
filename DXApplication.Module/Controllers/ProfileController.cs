using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.DC;
using DevExpress.Xpo;
using DXApplication.Module.BusinessObjects;
using DXApplication.Module.Extension;
using System;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using Mype.Common;
using DevExpress.XtraCharts;
using DevExpress.Data.Filtering;
using static DXApplication.Module.BusinessObjects.Profile;

namespace DXApplication.Module.Controllers;

public class ProfileController : MypeViewController
{
    readonly string key;
    public ProfileController()
    {
        TargetObjectType = typeof(Profile);
        TargetViewNesting = Nesting.Nested;
        UpdateGmail();
        SetPointScreen();
        RunProfile();
        KillProfile();
        try
        {
            key = Common.Extension.GetKey();
        }
        catch (Exception)
        {
            Common.Extension.CreateKey();
            key = Common.Extension.GetKey();
        }
    }
    int Width;
    int Height;
    void UpdateGmail()
    {
        var action = CreateAction(ActionType.Simple, "Thêm Gmail") as SimpleAction;
        action.SelectionDependencyType = SelectionDependencyType.RequireMultipleObjects;
        action.Execute += (s, e) =>
        {

            foreach (Profile item in View.SelectedObjects)
            {
                Gmail gmail = ObjectSpace.GetObjects<Gmail>().FirstOrDefault(x => x.ProfileId == null && x.Status == GmailStatus.Live) ;
                if (gmail != null)
                {
                    item.Gmail = gmail;
                    gmail.ProfileId = item.Oid.ToString();
                }
                else { Application.ShowViewStrategy.ShowMessage("Đã hết gmail đến gán cho profile", InformationType.Warning); }
            }
            ObjectSpace.CommitChanges();
        };
    }
    void SetPointScreen()
    {
        var action = CreateAction(ActionType.Simple, "Set Point") as SimpleAction;
        action.SelectionDependencyType = SelectionDependencyType.RequireMultipleObjects;
        action.TargetViewNesting = Nesting.Nested;
        action.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
        action.TargetObjectType = typeof(Profile);
        action.Execute += (s, e) =>
        {

            if (((DetailView)ObjectSpace.Owner).CurrentObject is Driver dv)
            {
                List<Point> points = new List<Point>();
                var _driver = ObjectSpace.GetObject(dv);
                Width = _driver.WidthScreen;
                Height = _driver.HightScreen;
                int CountWidthScreen = (int)Math.Floor((double)Width / 450);
                int CountHeightScreen = (int)Math.Floor((double)Height / 450);
                for (int i = 0; i < CountHeightScreen; i++)
                {
                    for (int j = 0; j < CountWidthScreen; j++)
                    {
                        points.Add(new Point(j * 500, i * 450));
                    }

                }
                int a = 0;
                foreach (Profile item in View.SelectedObjects)
                {
                    item.PointX = points[a].X;
                    item.PointY = points[a].Y;
                    a++;
                }
                ObjectSpace.CommitChanges();
            }
        };
    }
    void RunProfile()
    {
        var action = CreateAction(ActionType.Popup, "Run Profile") as PopupWindowShowAction;
        action.SelectionDependencyType = SelectionDependencyType.RequireMultipleObjects;
        action.TargetViewNesting = Nesting.Nested;
        action.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
        action.TargetObjectType = typeof(Profile);
        action.CustomizePopupWindowParams += (s, e) => {
            e.View = CreatePickerDetailView<ScriptParameters>();
        };
        action.Execute += async (object s, PopupWindowShowActionExecuteEventArgs e) =>
        {
            int script = (int)((ScriptParameters)e.PopupWindowViewCurrentObject).Script;
            int Time = ((ScriptParameters)e.PopupWindowViewCurrentObject).Time;
            if (((DetailView)ObjectSpace.Owner).CurrentObject is Driver dv)
            {
                var _driver = ObjectSpace.GetObject(dv);
                string[] keywords = _driver.Keywords.Select(a => a.Keyword).ToArray();
                string[] channels = _driver.Channels.Select(b => b.Channel).ToArray();
                string[] urls = _driver.Urls.Select(c => c.Url).ToArray();
                string[] comments = _driver.Comments.Select(d => d.Comment).ToArray();
                string[] icons = _driver.Icons.Select(e => e.Icon).ToArray();
                if(keywords == null || keywords.Length == 0)
                {
                    Application.ShowViewStrategy.ShowMessage("Chưa có danh sách keyword để tìm kiếm video !!!",InformationType.Warning,3000);
                }
                else if (channels == null || channels.Length == 0)
                {
                    Application.ShowViewStrategy.ShowMessage("Chưa có danh sách channels để tìm kiếm video !!!", InformationType.Warning, 3000);
                }
                else if(urls == null || urls.Length == 0)
                {
                    Application.ShowViewStrategy.ShowMessage("Chưa có danh sách urls để tìm kiếm video !!!", InformationType.Warning, 3000);
                }
                else if(comments == null || comments.Length == 0)
                {
                    Application.ShowViewStrategy.ShowMessage("Chưa có danh sách comments !!!", InformationType.Warning, 3000);
                }
                else if(icons == null || icons.Length == 0)
                { 
                    Application.ShowViewStrategy.ShowMessage("Chưa có danh sách icons !!!", InformationType.Warning, 3000);
                }
                else
                {
                    try
                    {
                        foreach (Profile item in View.SelectedObjects)
                        {
                            var data = new
                            {
                                ProfileId = item.Id,
                                PointX = item.PointX,
                                PointY = item.PointY,
                                Email = item.Gmail.Email,
                                Password = item.Gmail.Password,
                                TimeToWatchVideo = Time,
                                Keywords = keywords,
                                Channels = channels,
                                Urls = urls,
                                Comments = comments,
                                Icons = icons,
                                Script = script
                            };
                            string payload = data.ToBase64();
                            var topic = $"{key}\\remote\\slave\\run\\{item.Driver.Oid}";
                            var response = await Blazor.Mqtt.Commander.RpcCallStringWithResponse(topic, payload);
                            item.ProcessId = response.Message;
                            item.Status = StatusProfile.Running;
                            Application.ShowViewStrategy.ShowMessage("Đang khởi động Profile !!!");
                        }
                        ObjectSpace.CommitChanges();
                    }
                    catch (Exception)
                    {
                        Application.ShowViewStrategy.ShowMessage($"Đã xẩy ra lỗi", InformationType.Error);
                    }
                }
            }
            
        };
    }
    void KillProfile()
    {
        var action = CreateAction(ActionType.Simple, "Đóng Profile") as SimpleAction;
        action.ImageName = "State_Validation_Invalid";
        action.SelectionDependencyType = SelectionDependencyType.RequireMultipleObjects;
        action.Execute += async (s, e) =>
        {
            foreach (Profile item in View.SelectedObjects)
            {
                var topic = $"{key}\\remote\\slave\\kill\\{item.Driver.Oid}";
                var response = await Blazor.Mqtt.Commander.RpcCallStringWithResponse(topic, item.ProcessId);
                item.ProcessId = null;
                item.Status = StatusProfile.Stop;
                Application.ShowViewStrategy.ShowMessage(response.Message);
                Thread.Sleep(500);
            }
            ObjectSpace.CommitChanges();
        };
    }

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            this.X = x; this.Y = y;
        }
    }



}

[DomainComponent]
[XafDisplayName("Kịch bản")]
public class ScriptParameters : IDomainComponent
{
    [XafDisplayName("Loại kịch bản")]
    public Script Script { get; set; }

    [XafDisplayName("Thời gian xem video")]
    public int Time { get; set; } = 5;

}

