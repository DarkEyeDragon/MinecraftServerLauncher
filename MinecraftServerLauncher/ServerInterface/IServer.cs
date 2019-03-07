using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftServerLauncher.ServerInterface
{
    interface IServer
    {
        void Init();
        void Start();
        void Stop();
        void Output(string output);
        void Input(string input);

    }
}
