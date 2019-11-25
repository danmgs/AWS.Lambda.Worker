using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;

using AWS.Lambda.Worker;
using Amazon.Lambda.SNSEvents;
using static Amazon.Lambda.SNSEvents.SNSEvent;
using AWS.Lambda.Worker.Models;
using Newtonsoft.Json;

namespace AWS.Lambda.Worker.Tests
{
    public class FunctionTest
    {
        [Fact]
        public void FunctionUpperHandlerTest()
        {

            // Invoke the lambda function and confirm the string was upper cased.
            var function = new Function();
            var context = new TestLambdaContext();
            var upperCase = function.FunctionUpperHandler("hello world", context);

            Assert.Equal("HELLO WORLD", upperCase);
        }

        [Fact]
        public async void FunctionIndexingHandlerTest()
        {

            // Invoke the lambda function and confirm the string was upper cased.
            var function = new Function();
            var context = new TestLambdaContext();
            var snsEvent = CreateDummySnsEvent();
            bool result = await function.FunctionIndexingHandler(snsEvent, context);
            Assert.True(result);
        }

        private static SNSEvent CreateDummySnsEvent()
        {
            SNSEvent snsEvent = new SNSEvent();
            var r = new SNSRecord();
            r.Sns = new SNSMessage() { Message = CreateDummyMessageJson() };
            snsEvent.Records = new List<SNSRecord>()
            {
                 r
            };
            return snsEvent;
        }

        private static string CreateDummyMessageJson()
        {
            MySNSMessage message = new MySNSMessage() { Title = "Welcome", Content = "Hello World" };
            var json = JsonConvert.SerializeObject(message);
            return json;
        }
    }
}
