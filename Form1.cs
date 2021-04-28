using DGSERVICE;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BitcoinRealTime
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LastBlock =  Properties.Settings.Default.LastBlock;
        }
        public static int LastBlock;
        private void button1_Click(object sender, EventArgs e)
        {
            MySqlEd MySql = new MySqlEd();
            StringBuilder Insert1 = new StringBuilder();
            StringBuilder Insert2 = new StringBuilder();

            StringBuilder InsertON1 = new StringBuilder();
            StringBuilder InsertON2 = new StringBuilder();

            StringBuilder InsertON3 = new StringBuilder();
            StringBuilder InsertON4 = new StringBuilder();
            int Isdata1 = 0;

            //for (int a = LastBlock+1; a < 650001; a++)
            for (int a = LastBlock + 1; a < 650001; a++)
            {
                string hash = getBlockHash.Get(a).result;
                getBlockFull trans = JsonConvert.DeserializeObject<getBlockFull>(RPCX.Request.InvokeMethod("getblock", hash, 2).ToString());
      


                Insert1.Append("INSERT INTO `bitcoinindex`.`uxto_copy` (`TxId`,`Index`,`FullTxId`,`Block`,`Address`,`Value`,`BlockTime`) values " + Environment.NewLine);
                InsertON1.Append("UPDATE `bitcoinindex`.`uxto_copy` SET `Spent` = CASE " + Environment.NewLine);
                InsertON2.Append("END WHERE `FullTxId`  in (");
                InsertON3.Append("UPDATE `bitcoinindex`.`uxto_copy` SET `SpendingBlockIndex` = CASE " + Environment.NewLine);
                InsertON4.Append("END WHERE `FullTxId`  in (");
                for (int i = 0; i < trans.result.rawtx.Count; i++)
                {
                    for (int k = 0; k < trans.result.rawtx[i].vout.Count; k++)
                    {
                        if (trans.result.rawtx[i].vout[k].scriptPubKey.addresses == null)
                        {
                            List<string> addresses = new List<string>();
                            addresses.Add("OP_RETURN");
                            trans.result.rawtx[i].vout[k].scriptPubKey.addresses = addresses;
                        }
                        trans.result.rawtx[i].vout[k].scriptPubKey.addresses.Add("OP_RETURN");
                        if (trans.result.rawtx[i].vout[k].scriptPubKey.addresses.Count > 1)
                        {

                           


                        }

                        if (k == trans.result.rawtx[i].vout.Count - 1 && i == trans.result.rawtx.Count - 1)
                            Insert2.Append("('" + trans.result.rawtx[i].txid + "'," + trans.result.rawtx[i].vout[k].n + ",'" + trans.result.rawtx[i].txid + "-" + trans.result.rawtx[i].vout[k].n + "'," + trans.result.height + ",'" + trans.result.rawtx[i].vout[k].scriptPubKey.addresses[0] + "'," + trans.result.rawtx[i].vout[k].value.ToString().Replace(",", ".") + "," + trans.result.time + ");" + Environment.NewLine);
                        else
                            Insert2.Append("('" + trans.result.rawtx[i].txid + "'," + trans.result.rawtx[i].vout[k].n + ",'" + trans.result.rawtx[i].txid + "-" + trans.result.rawtx[i].vout[k].n + "'," + trans.result.height + ",'" + trans.result.rawtx[i].vout[k].scriptPubKey.addresses[0] + "'," + trans.result.rawtx[i].vout[k].value.ToString().Replace(",", ".") + "," + trans.result.time + ")," + Environment.NewLine);

                    }

                    for (int k = 0; k < trans.result.rawtx[i].vin.Count; k++)
                    {
                        if (i != 0)
                        {
                            if (k == trans.result.rawtx[i].vin.Count - 1 && i == trans.result.rawtx.Count - 1)
                            {
                                InsertON1.Append("WHEN `FullTxId` = '" + trans.result.rawtx[i].vin[k].txid + "-" + trans.result.rawtx[i].vin[k].vout + "' THEN '" + trans.result.rawtx[i].txid + "' " + Environment.NewLine + "ELSE `Spent`" + Environment.NewLine);
                                InsertON2.Append("'" + trans.result.rawtx[i].vin[k].txid + "-" + trans.result.rawtx[i].vin[k].vout + "');");
                                InsertON3.Append("WHEN `FullTxId` = '" + trans.result.rawtx[i].vin[k].txid + "-" + trans.result.rawtx[i].vin[k].vout + "' THEN " + trans.result.height + " " + Environment.NewLine + "ELSE `SpendingBlockIndex`" + Environment.NewLine);
                                InsertON4.Append("'" + trans.result.rawtx[i].vin[k].txid + "-" + trans.result.rawtx[i].vin[k].vout + "');");
                                Isdata1++;

                           
                            }
                            else
                            {
                                InsertON1.Append("WHEN `FullTxId` = '" + trans.result.rawtx[i].vin[k].txid + "-" + trans.result.rawtx[i].vin[k].vout + "' THEN '" + trans.result.rawtx[i].txid + "' " + Environment.NewLine);
                                InsertON2.Append("'" + trans.result.rawtx[i].vin[k].txid + "-" + trans.result.rawtx[i].vin[k].vout + "',");
                                InsertON3.Append("WHEN `FullTxId` = '" + trans.result.rawtx[i].vin[k].txid + "-" + trans.result.rawtx[i].vin[k].vout + "' THEN " + trans.result.height + " " + Environment.NewLine);
                                InsertON4.Append("'" + trans.result.rawtx[i].vin[k].txid + "-" + trans.result.rawtx[i].vin[k].vout + "',");
                                Isdata1++;
                            }
                        }

                    }


                    }

                MySql.MySql_Command(Insert1.ToString() + Insert2.ToString(),a);
                if (Isdata1 > 0)
                {
                    MySql.MySql_Command(InsertON1.ToString() + InsertON2.ToString(), a);
                    MySql.MySql_Command(InsertON3.ToString() + InsertON4.ToString(), a);
                }
                string x = InsertON1.ToString() + InsertON2.ToString();
                string z = InsertON3.ToString() + InsertON4.ToString();

                MySql.MySql_Command("UPDATE `bitcoinindex`.`realtimeparams` SET `LastOutIndexed` =" + a + ";");
                Insert1.Clear();
                Insert2.Clear();
                InsertON1.Clear();
                InsertON2.Clear();
                InsertON3.Clear();
                InsertON4.Clear();
                Isdata1 = 0;

                Properties.Settings.Default.LastBlock = a;
                Properties.Settings.Default.Save();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string hash = getBlockHash.Get(600000).result;
            string x = RPCX.Request.InvokeMethod("getblock", hash, 2).ToString();
            getBlockFull trans = JsonConvert.DeserializeObject<getBlockFull>(x);
        }
    }
}
