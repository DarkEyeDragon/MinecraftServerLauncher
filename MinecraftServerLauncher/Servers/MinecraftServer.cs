using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MinecraftServerLauncher.ServerInterface;

namespace MinecraftServerLauncher.Servers
{
    class MinecraftServer : IServer
    {

        public Arguments Args { get; set; }
        public Process Process { get; set; }
        public string JarPath { get; set; }

        public void Init()
        {
            //JarPath = Path.GetDirectoryName(Folder);
            Args.Add("xms", "1024M");
            Args.Add("xmx", "1024M");
            Args.Add("jar", JarPath);
            Args.Add("noGui");
        }
        public void Start()
        {
            Process = new Process
            {
                StartInfo =
                {
                    FileName = "java",
                    Arguments = Args.ToString(),
                    UseShellExecute = false,
                    //WorkingDirectory = Path.GetDirectoryName(),
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true,
                    CreateNoWindow = true,
                    RedirectStandardError = false
                }
            };
            Process.OutputDataReceived += (sender, args) => { Output(args.Data); };
            Process.Start();
            Process.BeginOutputReadLine();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void Output(string output)
        {
            throw new NotImplementedException();
        }

        public void Input(string input)
        {
            throw new NotImplementedException();
        }
    }
}
