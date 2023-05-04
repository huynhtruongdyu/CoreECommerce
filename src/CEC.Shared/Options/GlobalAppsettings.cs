using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEC.Shared.Options
{
    public class GlobalAppsettings
    {
        public Logging Logging { get; set; }
        public Databasesettings DatabaseSettings { get; set; }
    }

    public class Logging
    {
        public Loglevel LogLevel { get; set; }
    }

    public class Loglevel
    {
        public string Default { get; set; }
        public string MicrosoftAspNetCore { get; set; }
    }

    public class Databasesettings
    {
        public string DefaultProvider { get; set; }
        public Provider[] Providers { get; set; }
    }

    public class Provider
    {
        public string Name { get; set; }
        public string ConnectionString { get; set; }
    }
}