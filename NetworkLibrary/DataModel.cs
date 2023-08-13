using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLibrary
{
    class DataModel
    {
        public DataModel()
        {
        }

        public long ID { get; set; }
        public long Sent { get; set; }
        public long Received { get; set; }
        public string Date { get; set; }

        public static DataModel Create(IDataRecord record)
        {
            return new DataModel
            {
                ID = (long)record["id"],
                Sent = (long)record["sent"],
                Received = (long)record["received"]
            };
        }
    }
}
