using NLog;
using NLog.Config;
using NLog.Targets;
using System.Reflection;

namespace WeatherForecatRestApi.Logging
{
    public static class NLogConfiguration
    {
        public static void RegisterNLogConfiguration()
        {
            var assembly = Assembly.Load("NLog.Web.AspNetCore");
            ConfigurationItemFactory.Default.RegisterItemsFromAssembly(assembly);

            // Step 1. Create configuration object 
            var config = new LoggingConfiguration();

            // Step 2. Create targets and add them to the configuration 
            var allLogsFileTarget = new FileTarget();
            config.AddTarget("all-logs", allLogsFileTarget);

            // Step 3. Set target properties 
            allLogsFileTarget.FileName = "${basedir}/logs/all/all-logs-${shortdate}.txt";
            allLogsFileTarget.Layout = "${longdate}|${event-properties:item=EventId_Id}|${uppercase:${level}}|${logger}|${message} ${exception:format=tostring}";

            var weatherForecastFileTarget = new FileTarget();
            config.AddTarget("weatherforecast-logs", weatherForecastFileTarget);
            
            // Step 3. Set target properties 
            weatherForecastFileTarget.FileName = "${basedir}/logs/weatherforecast/weatherforecast-logs-${shortdate}.txt";
            weatherForecastFileTarget.Layout = "${longdate}|${event-properties:item=EventId_Id}|${uppercase:${level}}|${logger}|${message} ${exception:format=tostring}" +
                "${when:when=length('${aspnet-request-url}') > 0:inner= | uri = ${aspnet-request-url:IncludePort=true:IncludeQueryString=true:whenEmpty=}" +
                "${when:when=length('${aspnet-mvc-action}') > 0:inner= | action = ${aspnet-request-method} ${aspnet-mvc-action}";

            // Step 4. Define rules
            var allItemsRule = new LoggingRule("*", NLog.LogLevel.Trace, allLogsFileTarget);
            config.LoggingRules.Add(allItemsRule);

            var allCustomItemsRule = new LoggingRule("WeatherForecatRestApi.*", NLog.LogLevel.Trace, weatherForecastFileTarget);
            config.LoggingRules.Add(allCustomItemsRule);

            // Step 5. Activate the configuration
            LogManager.Configuration = config;
        }
    }
}
