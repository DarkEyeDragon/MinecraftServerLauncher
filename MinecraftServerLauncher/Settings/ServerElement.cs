using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinecraftServerLauncher.ServerInterface;
using MinecraftServerLauncher.Servers.Versions;

namespace MinecraftServerLauncher.Settings
{
    class ServerElement
    {
        public Project ProjectType { get; set; }
        public string ServerName { get; set; }
        public string Version { get; set; }
        public string Build { get; set; }
        public string Path { get; set; }
        public string JarName { get; set; }
        public Arguments Args { get; set; }

        /// <summary>
        /// Create a new ServerElement instance.
        /// </summary>
        public ServerElement()
        {
        }


        /// <summary>
        /// Create a server object for XML serialization and  deserialization
        /// </summary>
        /// <param name="projectType">An enum of the type <see cref="Project"/>.</param>
        /// <param name="serverName">The servers name</param>
        /// <param name="version">the version of the server</param>
        /// <param name="build">the build number of the server</param>
        /// <param name="path">the relative path of the working directory</param>
        /// <param name="jarName">The name of the jar (without extension)</param>
        /// <param name="args">Java arguments of type <see cref="Arguments"/></param>
        public ServerElement(Project projectType, string serverName, string version, string build, string path,
            string jarName, Arguments args)
        {
            ProjectType = projectType;
            ServerName = serverName;
            Version = version;
            Build = build;
            Path = path;
            JarName = jarName;
            Args = args;
        }
    }
}