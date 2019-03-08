using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using MinecraftServerLauncher.Servers.Versions;

namespace MinecraftServerLauncher.Settings
{
    class SettingsHandler
    {
        public string FileLocation { get; set; }
        public XDocument Settings { get; set; }

        private readonly string _servers = "Servers";
        private readonly string _server = "Server";
        private readonly string _name = "Name";
        private readonly string _serverType = "ServerType";
        private readonly string _version = "Version";
        private readonly string _location = "Location";
        private readonly string _jar = "Jar";
        private readonly string _arguments = "Arguments";
        private readonly string _argument = "Argument";

        public SettingsHandler()
        {
            FileLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings.xml");
            Settings = XDocument.Load(FileLocation);
        }

        //TODO switch to XmlSerializer
        public void Save(ServerElement serverElement)
        {
            var location = new XElement(_location);
            location.SetAttributeValue("Path", serverElement.Path);

            var jar = new XElement(_jar);
            jar.SetAttributeValue("Name", serverElement.JarName);

            var arguments = new XElement(_arguments);
            foreach (var serverElementArg in serverElement.Args)
            {
                var argument = new XElement(_argument);
                argument.SetAttributeValue("Key", serverElementArg.Key);
                argument.SetAttributeValue("Value", serverElementArg.Value);
                arguments.Add(argument);
            }
            
            XDocument srcTree = new XDocument(
                new XElement(_servers,
                    new XElement(_server,
                        new XElement(_name, serverElement.ServerName),
                        new XElement(_serverType, serverElement.ProjectType),
                        new XElement(_version, $"{serverElement.Version}_{serverElement.Build}"),
                        location,
                        jar,
                        arguments
                    )
                )
            );
            Debug.WriteLine(srcTree);
        }

        public void GetServers()
        {
            //Debug.WriteLine(Settings.Nodes());
            /*foreach (XNode node in Settings.Nodes().)
            {
                Debug.WriteLine(node);
            }*/
        }
    }
}