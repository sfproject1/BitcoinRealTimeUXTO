using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinRealTime
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class ScriptSig
    {
        public string asm { get; set; }
        public string hex { get; set; }
    }

    public class Vin
    {
        public string coinbase { get; set; }
        public object sequence { get; set; }
        public string txid { get; set; }
        public int vout { get; set; }
        public ScriptSig scriptSig { get; set; }
    }

    public class ScriptPubKey
    {
        public string asm { get; set; }
        public string hex { get; set; }
        public int reqSigs { get; set; }
        public string type { get; set; }
        public List<string> addresses { get; set; }
    }

    public class Vout
    {
        public double value { get; set; }
        public int n { get; set; }
        public ScriptPubKey scriptPubKey { get; set; }
    }

    public class Rawtx
    {
        public string hex { get; set; }
        public string txid { get; set; }
        public string hash { get; set; }
        public int size { get; set; }
        public int vsize { get; set; }
        public int weight { get; set; }
        public int version { get; set; }
        public decimal locktime { get; set; }
        public List<Vin> vin { get; set; }
        public List<Vout> vout { get; set; }
        public string blockhash { get; set; }
        public int confirmations { get; set; }
        public int time { get; set; }
        public int blocktime { get; set; }
    }

    public class Result
    {
        public string hash { get; set; }
        public int confirmations { get; set; }
        public int strippedsize { get; set; }
        public int size { get; set; }
        public int weight { get; set; }
        public int height { get; set; }
        public int version { get; set; }
        public string versionHex { get; set; }
        public string merkleroot { get; set; }
        public List<Rawtx> rawtx { get; set; }
        public int time { get; set; }
        public long nonce { get; set; }
        public string bits { get; set; }
        public double difficulty { get; set; }
        public string previousblockhash { get; set; }
        public string nextblockhash { get; set; }
    }

    public class getBlockFull
    {
        public Result result { get; set; }
        public object error { get; set; }
        public string id { get; set; }
    }
}
