using System;
using AWS.Lambda.Worker.Models;

namespace AWS.Lambda.Worker.Helpers
{
    public static class MappingHelper
    {
        public static MyMessageDocument Map(MySNSMessage message)
        {
            var doc = new MyMessageDocument
            {
                Title = message.Title,
                Content = message.Content
            };
            return doc;
        }
    }
}