using System;
using System.Collections.Generic;
using System.Text;

namespace EasySharp.MessageBrokers
{
    public class MessageBrokersOptions
    {
        public bool Enable { get; set; } = false;
        public string MessageBrokerType { get; set; }
    }
}
