using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXApplication.Module.Common;
public class Extension {
    public static string GetKey() {
        var sr = File.OpenText(".key");
        var key = sr.ReadToEnd();
        sr.Close();
        return key;
    }

    public static void CreateKey() {
        if (!File.Exists(".key")) {
            var sw = File.CreateText(".key");
            sw.Write(Mype.Common.Extension.RandomString(32, false));
            sw.Flush();
            sw.Close();
        }
    }
}
