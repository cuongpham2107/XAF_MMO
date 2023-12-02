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
using DevExpress.XtraGauges.Core.Drawing;
using Mype.Mqtt.Rpc;
using Mype.Mqtt;

namespace DXApplication.Module.Controllers;

public class DriverController : MypeViewController {

    public DriverController() {
        TargetObjectType = typeof(Driver);
        //FakeData();
        Reset();
        CreateFileDriverId();
        Authenticate();
        Ping();
        SetConfigFile();
        CreateProfile();
        try {
            key = Common.Extension.GetKey();
        } catch (Exception) {
            Common.Extension.CreateKey();
            key = Common.Extension.GetKey();
        }
    }
    
    void Reset() {
        var action = CreateAction(ActionType.Simple, "Reset") as SimpleAction;
        action.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;
        action.Execute += (s, e) => {
            var current = View.CurrentObject as Driver;
            var gologin = ObjectSpace.GetObjects<GologinAccount>().FirstOrDefault(a => a.Default);
            current.GologinAccount = gologin;
            var Config = ObjectSpace.GetObjects<Configuration>().FirstOrDefault();
            current.WidthScreen = Config.WidthScreen;
            current.HightScreen = Config.HightScreen;
            current.ApiBaseUrl = Config?.ApiBaseUrl;
            current.ApiCreateProfile = Config?.ApiCreateProfile;
            current.ApiUpdateProfile = Config?.ApiUpdateProfile;
            current.ApiStartProfile = Config?.ApiStartProfile;
            current.ApiDeleteProfile = Config?.ApiDeleteProfile;
            current.DefaultWorkingPath = Config?.DefaultWorkingPath;
            current.DefaultProxyProfile = Config == null ? Config.DefaultProxyProfile : 10;
            current.DefaultGologinPath = Config?.DefaultGologinPath;
            current.PathOrbita = Config?.PathOrbita;
            current.PathProfile = Config?.PathProfile;
            current.PathCookieExtension = Config?.PathCookieExtension;
            current.PathPasswordExtension = Config?.PathPasswordExtension;
            current.PathProxyExtension = Config?.PathProxyExtension;
            current.ChromeCommandLine = Config?.ChromeCommandLine;
            current.PathSlave = Config?.PathSlave;
            current.PathWorker = Config?.PathWorker;
            current.MqttUri = Config?.MqttUri;
            current.MqttPort = Config.MqttPort;
            current.WidthScreen = Config.WidthScreen;
            current.HightScreen = Config.HightScreen;
        };
    }
    void CreateFileDriverId()
    {
        var action = CreateAction(ActionType.Simple, "Tạo DriverId") as SimpleAction;
        action.SelectionDependencyType = SelectionDependencyType.RequireMultipleObjects;
        action.Execute += (s, e) =>
        {
            foreach (Driver d in View.SelectedObjects)
            {
                d.DriverIdFile = CreateFile(".driverid", sw => sw.WriteLine(d.Oid));
            }
        };
    }
    class Info {
        public string Name { get; set; }
        public string IPs { get; set; }
    }

    readonly string key;
    void Ping() {
        var action = CreateAction(ActionType.Simple, "Ping") as SimpleAction;
        action.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;

        action.Execute += async (s, e) => {
            var current = View.CurrentObject as Driver;
            var topic = $"{key}\\remote\\ping\\{current.Oid}";
            try {
                var response = await Blazor.Mqtt.Commander.RpcCallNoPayloadWithResponse(topic);
                Application.ShowViewStrategy.ShowMessage(response.Message);
            } catch (Exception) {
                Application.ShowViewStrategy.ShowMessage("The driver does not response", InformationType.Error);
            }
        };
    }
    

        void Authenticate() {
        var action = CreateAction(ActionType.Simple, "Authenticate") as SimpleAction;
        action.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;

        action.Execute += async (s, e) => {
            var current = View.CurrentObject as Driver;
            var topic = $"remote\\authenticate\\{current.Oid}";
            try {
                var response = await Blazor.Mqtt.Commander.RpcCallStringWithResponse(topic, Common.Extension.GetKey());
                if (response.Message.Contains("OK"))
                {
                    Application.ShowViewStrategy.ShowMessage("Already authenticated. Dont do again.");
                }
                else
                {
                    var infor = JsonSerializer.Deserialize<Info>(response.Message);
                    current.MachineName = infor.Name;
                    current.Ip = infor.IPs;
                    current.Status = DriverStatus.Authenticated;
                    ObjectSpace.CommitChanges();
                    Application.ShowViewStrategy.ShowMessage(response.Message);
                }
                
            } catch (Exception) {
                Application.ShowViewStrategy.ShowMessage("Error authenticating driver. The driver does not response", InformationType.Error);
            }
        };
    }

    void FakeData() {
        var action = CreateAction(ActionType.Simple, "Fake") as SimpleAction;
        action.Execute += (s, e) => {
            for (int i = 0; i < 255; i++) {
                var obj = ObjectSpace.CreateObject<Driver>();
                obj.Ip = $"192.168.1.{i}";
                obj.MachineName = Mype.Common.Extension.RandomString(15);
            }
            ObjectSpace.CommitChanges();
        };
    }

    void SetConfigFile() {
        var action = CreateAction(ActionType.Simple, "Gán Config") as SimpleAction;
        action.SelectionDependencyType = SelectionDependencyType.RequireMultipleObjects;
        action.Execute += async (s, e) => {
            var Config = ObjectSpace.GetObjectsQuery<Configuration>().FirstOrDefault();
            foreach (Driver d in View.SelectedObjects) {

                var obj = new {
                    DriverId = d.Oid,
                    GologinAccessToken = d.GologinAccount?.AccessToken,
                    DefaultWorkingPath = d.DefaultWorkingPath,
                    PathGologin = d.DefaultGologinPath,
                    ApiBaseUrl = d?.ApiBaseUrl,
                    ApiCreateProfile = d?.ApiCreateProfile,
                    ApiUpdateProfile = d?.ApiUpdateProfile,
                    ApiStartProfile = d?.ApiStartProfile,
                    ApiDeleteProfile = d?.ApiDeleteProfile,
                    PathOrbita = d?.PathOrbita,
                    PathCookieExtension = d?.PathCookieExtension,
                    PathPasswordExtension = d?.PathPasswordExtension,
                    PathProxyExtension = d?.PathProxyExtension,
                    ChromeCommandLine = d?.ChromeCommandLine,
                    PathSlave = d?.PathSlave,
                    PathWorker = d?.PathWorker,
                    MqttUri = Config?.MqttUri,
                    MqttPort = Config.MqttPort,
                   
                };
                var topic = $"{key}\\remote\\setconfig\\{d.Oid}";
                try
                {
                    var response = await Blazor.Mqtt.Commander.RpcCallObjectWithResponse(topic, obj);
                    Application.ShowViewStrategy.ShowMessage(response.Message);
                    d.Status = DriverStatus.ConfigSuccessfully;
                }
                catch (Exception)
                {
                    Application.ShowViewStrategy.ShowMessage("Error authenticating driver. The driver does not response", InformationType.Error);
                }
            }
            ObjectSpace.CommitChanges();


        };
    }

    void CreateProfile()
    {
        var action = CreateAction(ActionType.Simple, "Tạo Profile") as SimpleAction;
        action.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;
       
        action.Execute += async ( s, e) =>
        {
            
            try
            {
                var current = View.CurrentObject as Driver;
                int proxy = current.DefaultProxyProfile;
                var listProxy = ObjectSpace.GetObjects<Proxy>()
                .Where(x => x.Count == 0)
                .Take(proxy)
                .Select(b => new {
                    Oid = b.Oid,
                    Driver = current.Oid,
                    Host = b.Ip,
                    Port = b.Port,
                    Username = b.User,
                    Password = b.Password
                })
                .ToList();
                var data = new
                {
                    CountProfile = current.DefaultProfile,
                    ListProxy = listProxy,
                };
                //string payload = listProxy.ToBase64();
                string payload = data.ToBase64();
                var topic = $"{key}\\remote\\worker\\startprofile\\{current.Oid}";
                var response = await Blazor.Mqtt.Commander.RpcCallStringWithResponse(topic, payload);
                Application.ShowViewStrategy.ShowMessage(response.Message);
            }
            catch (Exception)
            {
                Application.ShowViewStrategy.ShowMessage("The driver does not response", InformationType.Error);
            }
        };
    }
    //[ActivatorUtilitiesConstructor]
    //public DriverController(IServiceProvider serviceProvider) : this() {
    //    synchronizationContextHelper = serviceProvider.GetRequiredService<SynchronizationContextHelper>();
    //}

    //IMqttClient client = new MqttFactory().CreateMqttClient();
    //private readonly SynchronizationContextHelper synchronizationContextHelper;

    //protected override void OnActivated() {
    //    base.OnActivated();

    //    client.ApplicationMessageReceivedAsync += Client_ApplicationMessageReceivedAsync;
    //    var options = new MqttClientOptionsBuilder()
    //            .WithTcpServer("broker.hivemq.com", 1883)
    //            .WithCleanSession()
    //            .Build();
    //    client.ConnectAsync(options).Wait();
    //    var topicFilter = new MqttTopicFilterBuilder()
    //        .WithTopic("stationstatus")
    //        .Build();
    //    client.SubscribeAsync(topicFilter).Wait();
    //}

    //private async Task Client_ApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs args) {
    //    await synchronizationContextHelper.InvokeAsync(() => {
    //        // var result = Encoding.UTF8.GetString(args.ApplicationMessage.PayloadSegment);
    //        Application.ShowViewStrategy.ShowMessage($"Data has come at {DateTime.Now.ToString("F")}");
    //        var data = View.ObjectSpace.GetObjectsQuery<Driver>();
    //        foreach (var obj in data) {
    //            obj.Running = true;
    //        }
    //        return Task.CompletedTask;
    //    });
    //}

    //protected override void OnDeactivated() {
    //    base.OnDeactivated();
    //    client.DisconnectAsync().Wait();
    //}
}

