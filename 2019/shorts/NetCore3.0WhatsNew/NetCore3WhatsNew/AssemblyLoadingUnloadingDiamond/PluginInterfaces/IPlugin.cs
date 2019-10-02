using System;

namespace PluginInterfaces
{
    public interface IPlugin
    {
        public string Name { get; }
        public string LoadData();
    }
}
