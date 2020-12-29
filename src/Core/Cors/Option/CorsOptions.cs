using System;
using System.Collections.Generic;
using System.Text;

namespace EasySharp.Core.Cors.Option
{
    public class CorsOptions
    {
        public bool Enabled { get; set; } = false;
        public string Name { get; set; } = "EasySharp";

        public string[] Links { get; set; }
    }
}
