namespace SampleApi.Util.Config
{
    /// <summary>
    /// カスタム設定ファイルクラス
    /// </summary>
    public class MyConfig: IMyConfig
    {
        IConfigurationRoot _configurationRoot;

        public MyConfig()
        {
            _configurationRoot = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("mysettings.json")
                .Build();
        }


        public IConfigurationRoot GetConfigurationRoot()
        {
            return _configurationRoot;
        }

    }
}
