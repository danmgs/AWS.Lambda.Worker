using System;
using AWS.Lambda.Worker.Models;
using Microsoft.Extensions.Configuration;
using Nest;

namespace AWS.Lambda.Worker.Helpers
{
    /// <summary>
    /// Elastic Search Client Helper.
    /// Index documents by default in the specified index name + create the index if not exists.
    /// </summary>
    public static class ElasticSearchHelper
    {
        private static IElasticClient _client;

        public static IElasticClient GetInstance(IConfiguration config)
        {
            if (_client == null)
            {
                var url = Environment.GetEnvironmentVariable("ESurl") ?? config.GetSection("ES").GetValue<string>("url");
                var indexName = Environment.GetEnvironmentVariable("ESindexName") ?? config.GetSection("ES").GetValue<string>("indexName");

                var settings = new ConnectionSettings(new Uri(url))
                    .DefaultIndex(indexName)
                    .DefaultMappingFor<MyMessageDocument>(m => m.IdProperty(x => x.Id));

                _client = new ElasticClient(settings);
            }

            return _client;
        }
    }
}