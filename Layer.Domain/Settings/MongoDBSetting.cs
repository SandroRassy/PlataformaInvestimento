using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Domain.Settings
{
    public sealed class MongoDBSetting
    {
        public string DatabaseName { get; set; }
        public List<string> CollectionName { get; set; }
        public string ConnectionString { get; set; }
    }
}
