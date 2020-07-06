using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Reflection;

namespace WebStore_GB.Logger
{
    public static class Log4NetExtensions 
    {
        public static ILoggerFactory AddLog4Net(this ILoggerFactory factory, string configurationFile = "log4net.config")
        {
            if (!Path.IsPathRooted(configurationFile))
            {
                var assembly = Assembly.GetEntryAssembly() ?? throw new InvalidOperationException("Не определена сборка с точкой входа в приложение");
                var dir = Path.GetDirectoryName(assembly.Location) ?? throw new InvalidOperationException("Не удалось определить путь к дирректории исполнительной сборки");
                configurationFile = Path.Combine(dir, configurationFile);
            }

            factory.AddProvider(new Log4NetLoggerProvider(configurationFile));

            return factory;
        }
    }
}
