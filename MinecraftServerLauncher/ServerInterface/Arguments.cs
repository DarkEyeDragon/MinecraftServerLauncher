using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftServerLauncher.ServerInterface
{
    class Arguments : Dictionary<string, string>
    {
        public void Add(string arg)
        {
            Add(arg, "");
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder(Count);
            foreach (var key in Keys)
            {
                stringBuilder.Append(" -");
                stringBuilder.Append(key);
                if(base[key] != string.Empty)
                    stringBuilder.Append(base[key]);
            }

            return stringBuilder.ToString();
        }
    }
}
