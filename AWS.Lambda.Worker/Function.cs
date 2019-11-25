using Amazon.Lambda.Core;
using Amazon.Lambda.SNSEvents;
using AWS.Lambda.Worker.Helpers;
using AWS.Lambda.Worker.Models;
using Nest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AWS.Lambda.Worker
{
    public class Function
    {
        private readonly IElasticClient _client;

        public Function()
        {
            _client = ElasticSearchHelper.GetInstance(ConfigurationHelper.Instance);
        }

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public string FunctionUpperHandler(string input, ILambdaContext context)
        {
            return input?.ToUpper();
        }

        /// <summary>
        /// A simple function to log SNS messages into CloudWatch
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        public void FunctionLogHandler(SNSEvent snsEvent, ILambdaContext context)
        {
            foreach (var record in snsEvent.Records)
            {
                context.Logger.LogLine(record.Sns.Message);
            }
        }

        /// <summary>
        /// A simple function to index documents (SNS messages) into AWS Elastic Search Service
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns>A Task</returns>
        public async Task<bool> FunctionIndexingHandler(SNSEvent snsEvent, ILambdaContext context)
        {
            LogEnvironmentVariables(context);

            foreach (var record in snsEvent.Records)
            {
                context.Logger.LogLine(record.Sns.Message);
                context.Logger.LogLine("before DeserializeObject");
                var message = JsonConvert.DeserializeObject<MySNSMessage>(record.Sns.Message);
                context.Logger.LogLine("after DeserializeObject");
                var document = MappingHelper.Map(message);
                context.Logger.LogLine("done document");
                var indexResponse = await _client.IndexDocumentAsync(document);
                context.Logger.LogLine(indexResponse.Result.ToString());
                context.Logger.LogLine(indexResponse.DebugInformation);
            }

            return true;
        }

        private void LogEnvironmentVariables(ILambdaContext context)
        {
            var env = Environment.GetEnvironmentVariable("ENV");
            if (!string.IsNullOrEmpty(env))
                context.Logger.LogLine($"-> Environment variable ENV={env}");
            else
                context.Logger.LogLine($"-> Environment variable ENV is not set !");

            var envESurl = Environment.GetEnvironmentVariable("ESurl");
            if (!string.IsNullOrEmpty(envESurl))
                context.Logger.LogLine($"-> Environment variable ESurl={envESurl}");
            else
                context.Logger.LogLine($"-> Environment variable ESurl is not set !");

            var envESindexName = Environment.GetEnvironmentVariable("ESindexName");
            if (!string.IsNullOrEmpty(envESindexName))
                context.Logger.LogLine($"-> Environment variable ESindexName={envESindexName}");
            else
                context.Logger.LogLine($"-> Environment variable ESindexName is not set !");
        }
    }
}
