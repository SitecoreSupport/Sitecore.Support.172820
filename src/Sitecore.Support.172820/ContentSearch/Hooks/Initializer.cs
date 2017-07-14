namespace Sitecore.Support.ContentSearch.Hooks
{
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.Diagnostics;
    using Sitecore.ContentSearch.Instrumentation;
    using Sitecore.ContentSearch.Maintenance;
    using Sitecore.ContentSearch.Utilities;
    using Sitecore.Diagnostics;
    using Sitecore.Events.Hooks;
    public class Initializer : IHook
    {
        private readonly IContentSearchConfigurationSettings settings;

        #region Public Methods and Operators

        public Initializer()
        {
            this.settings = ContentSearchManager.Locator.GetInstance<IContentSearchConfigurationSettings>();
        }

        public Initializer(IContentSearchConfigurationSettings settings)
        {
            this.settings = settings;
        }

        public virtual void Initialize()
        {
            if (!this.settings.ContentSearchEnabled())
            {
                // these two lines are needed to run static constructors of SearchLog and CrawlingLog.
                SearchLog.Log.Debug("Content Search feature disabled");
                CrawlingLog.Log.Debug("Content Search feature disabled");

                return;
            }

            EventHub.Initialize();

            Assert.IsNotNull(ContentSearchManager.SearchConfiguration, "SearchConfiguration");

            if (this.settings.VerboseLogging())
            {
                VerboseLogger.Initialize(CrawlingLog.Log);
            }

            /* Fix for #172820
            var pipeline = ContentSearchManager.Locator.GetInstance<ICorePipeline>();

            SearchLog.Log.Info("Warming and Caching your search indexes");
            var cleanListOfWarmupQuiries = new List<string>();
            QueryWarmupPipeline.Run(pipeline, new QueryWarmupArgs(cleanListOfWarmupQuiries));
            */
            ContentSearchManager.Initialize();
        }

        #endregion
    }
}
 