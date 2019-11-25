using System;
using System.Collections.Generic;
using System.Text;

namespace AWS.Lambda.Worker.Models
{
    public class MySNSMessage
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
