using System;
using System.Collections.Generic;
using System.Text;

namespace EasySharp.Cache.Option
{
    public class Cacheable
    {
        public RedisOptions Redis { get; set; }
        public LocalStorageOptions LocalStorage { get; set; }
    }
}
