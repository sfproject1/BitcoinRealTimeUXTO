using BitcoinRealTime.RPC.BTCD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinRealTime
{
    class getBlockHash
    {
        public string result { get; set; }
        public object error { get; set; }
        public string id { get; set; }
        public static getBlockHash Get(int num)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<getBlockHash>(RPCX.Request.InvokeMethod("getblockhash", num).ToString());
        }

    }
}
