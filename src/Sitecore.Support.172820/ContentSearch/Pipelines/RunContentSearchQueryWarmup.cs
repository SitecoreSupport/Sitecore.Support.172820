namespace Sitecore.Support.ContentSearch.Pipelines
{
    using System.Collections.Generic;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.Diagnostics;
    using Sitecore.ContentSearch.Pipelines.QueryWarmups;
    using Sitecore.Pipelines;
    using Sitecore.Abstractions;
    public class RunContentSearchQueryWarmup
    {
        public void Process(PipelineArgs args)
        {
            var pipeline = ContentSearchManager.Locator.GetInstance<ICorePipeline>();

            SearchLog.Log.Info("Warming and Caching your search indexes");
            var cleanListOfWarmupQuiries = new List<string>();
            QueryWarmupPipeline.Run(pipeline, new QueryWarmupArgs(cleanListOfWarmupQuiries));
        }
    }
}