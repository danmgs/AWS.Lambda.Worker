using System;
using System.Collections.Generic;
using System.Text;

namespace AWS.Lambda.Worker.Models
{
    public class MyMessageDocument
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Id => Guid.NewGuid().ToString();
        public DateTime CreationDateTime => DateTime.UtcNow;
    }
}
