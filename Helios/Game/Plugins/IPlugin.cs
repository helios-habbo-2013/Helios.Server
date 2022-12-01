using System;
using System.Collections.Generic;
using System.Text;

namespace Helios.Game
{
    public interface IPlugin
    {
        void onEnable();
        void onDisable();
    }
}
