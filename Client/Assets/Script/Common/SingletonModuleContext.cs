using System;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    class SingletonModuleContext
    {
        static private List<InterfaceSingletonModule> Modules = new List<InterfaceSingletonModule>();
        static internal void RegisterModule(InterfaceSingletonModule module)
        {
            Modules.Add(module);
        }
        static public void InitModules()
        {
            foreach (InterfaceSingletonModule s in Modules)
            {
                s.Init();
            }
        }
        static public void CleanupModules()
        {
            foreach (InterfaceSingletonModule s in Modules)
            {
                s.Cleanup();
            }
        }
    }
}