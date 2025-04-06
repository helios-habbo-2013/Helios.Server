using Helios.Util;
using log4net;
using log4net.Config;
using System;
using System.IO;
using System.Reflection;
using Helios.Network;
using Helios.Game;
using Helios.Messages;
using System.Threading;
using Helios.Storage.Access;
using Helios.Storage;
using System.Linq;
using Newtonsoft.Json;

namespace Helios
{
    class Program
    {
        static void Main(string[] args)
        {
            Environment.Boot();
        }
    }
}