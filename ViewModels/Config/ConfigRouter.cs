namespace CallAssistant.ViewModels.Config
{
    public static class ConfigRouter
    {
        public static string GetVar(string name, IConfiguration config)
        {
            var val = config.GetValue<string>(name);

            if(string.IsNullOrEmpty(val))
            {
                val = Environment.GetEnvironmentVariable(name);
            }

            return val;
        }
    }
}
