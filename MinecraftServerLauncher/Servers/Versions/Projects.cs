using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MinecraftServerLauncher.Servers.Versions
{
    public enum Project
    {
        Paper,
        Waterfall,
        Travertine
    };

    class Projects
    {
        private static string _baseUrl = "https://papermc.io/api/v1/";

        public static string[] GetVersions(Project project)
        {
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString($"{_baseUrl}{project.ToString().ToLower()}");
                Dictionary<string, object> result = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                foreach (var keyValuePair in result)
                {
                    if (keyValuePair.Key.Contains("version"))
                    {
                        JArray array = (JArray) keyValuePair.Value;
                        return array.Select(jv => (string) jv).ToArray();
                    }
                }
            }

            return null;
        }

        public static string[] GetBuild(Project project, string version)
        {
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString($"{_baseUrl}{project.ToString().ToLower()}/{version}");
                Dictionary<string, object> result = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                foreach (var keyValuePair in result)
                {
                    if (keyValuePair.Key.Contains("builds"))
                    {
                        var buildNum =
                            JsonConvert.DeserializeObject<Dictionary<string, object>>(keyValuePair.Value.ToString());
                        foreach (var valuePair in buildNum)
                        {
                            if (valuePair.Key.Equals("all"))
                            {
                                JArray array = (JArray)valuePair.Value;
                                return array.Select(jv => (string)jv).ToArray();
                            }
                        }
                    }
                }
            }

            return null;
        }

        public static void DownloadJar(Project project, string version, string build)
        {
            using (WebClient wc = new WebClient())
            {
                wc.DownloadFileAsync(new Uri($"{_baseUrl}{project.ToString().ToLower()}/{version}/{build}/download"),
                    $"{project}-{version}_{build}.jar");
                wc.DownloadProgressChanged += DownloadProgressChanged;
            }
        }

        private static void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Debug.WriteLine($"{e.BytesReceived}/{e.TotalBytesToReceive}");
        }
    }
}