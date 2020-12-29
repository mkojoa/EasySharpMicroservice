using EasySharp.Logging.Options;
using System.Collections.Generic;

namespace EasySharp.Logging 
{
    public class LoggerOptions
    {
        public string ApplicationName { get; set; }
        public string Level { get; set; }
        public FileOptions File { get; set; }
        public SeqOptions Seq { get; set; }
        public IDictionary<string, string> MinimumLevelOverrides { get; set; }
        public IEnumerable<string> ExcludePaths { get; set; }
        public IEnumerable<string> ExcludeProperties { get; set; }
        public IDictionary<string, object> Tags { get; set; }
    }
}
