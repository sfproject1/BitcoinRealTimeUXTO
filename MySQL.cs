using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.IO;

namespace DGSERVICE
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
    public class MySqlEd
    {
        //Процедура связи с файлом базы данных
        //
        readonly MySqlConnection con = new MySqlConnection("SERVER=127.0.0.1; port=3306; Database=zabivator; UID=steel; PASSWORD=Getright2617271;Charset=utf8; Persist Security Info=True;");        //   public MySqlConnection Con = new MySqlConnection("SERVER=" + Set.SERVER + "; Database=" + Set.Database + "; UID=" + Set.UID + "; PASSWORD=" + Set.PASSWORD + ";");


        Random r = new Random();
        private String[,] value;
        /// <summary>
        /// Рандом
        /// </summary>
        /// <param name="x">Максимальное значение x</param>
        /// <returns>Вывод значения int random</returns> 
        public int rand(int x)
        {
            int sd;
            sd = r.Next(x);
            if (sd == 0)
            {
                sd = sd + 1;
            }
            return sd;
        }
        /// <summary>
        /// Считываем таблицу SQL и помещаем в массив данных
        /// </summary>
        /// <param name="CommandText">Текст с SQL командой "SELECT * FROM `user_pr` where loguser='"+textBox1.Text+"'"</param>
        /// <returns>Вывод значения value[]</returns> 
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Проверка запросов SQL на уязвимости безопасности")]
        public String[,] MySqlReader(string CommandText)
        {
            int n = 0;

            try
            {
                int x = MySql_Num_Rows(CommandText);
                con.Open();
                MySqlCommand myCommand = new MySqlCommand(CommandText, con);
                MySqlDataReader DataReader;
                DataReader = myCommand.ExecuteReader();
                int c = DataReader.FieldCount;
                value = new String[c, x];
                while (DataReader.Read())
                {
                    for (int j = 0; j < c; j++)
                    {

                        if (!DataReader.IsDBNull(j))
                            value[j, n] = DataReader.GetString(j);
                    }
                    n++;
                }
                DataReader.Close();
            }
            //catch (MySqlException)
            //{


            //}
            finally
            {
                con.Close();
            }
            return value;
        }
        /// <summary>
        /// Считываем таблицу SQL и выводит количество строк в таблице
        /// </summary>
        /// <param name="CommandText">Текст с SQL команндой "SELECT * FROM `shop` where catalog LIKE 'id1'"</param>
        /// <returns>Вывод значения </returns> 
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Проверка запросов SQL на уязвимости безопасности")]
        public int MySql_Num_Rows(string CommandText)
        {
            int n = 0;
            try
            {
                con.Open();
                MySqlCommand myCommand = new MySqlCommand(CommandText, con);
                MySqlDataReader MyDataReader;
                MyDataReader = myCommand.ExecuteReader();
                //SELECT COUNT(*) FROM table
                while (MyDataReader.Read())
                {
                    n++;
                }
                MyDataReader.Close();
            }
            catch (MySqlException)
            {

            }
            finally
            {
                con.Close();
            }
            return n;
        }
        /// <summary>
        /// записываем данные в таблицу SQL 
        /// </summary>
        /// <param name="CommandText">Текст с SQL команндой "insert into database (data, name, txt) values ('data','name','txt')"</param>
        /// <returns>Вывод значения </returns> 
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Проверка запросов SQL на уязвимости безопасности")]
        public int MySql_Command(string CommandText,int a=0)
        {
            int UspeshnoeIzmenenie = 0;
            MySqlCommand myCommand;
            //System.IO.StreamWriter writer = new System.IO.StreamWriter(@"C:\inetpub\wwwroot\reports\html\" + "test" + ".xyz", true);
            //writer.WriteLine(CommandText);
            //  writer.Close();      
            con.Open();
            try
            {
                myCommand = new MySqlCommand(CommandText, con);
                UspeshnoeIzmenenie = myCommand.ExecuteNonQuery();
            }
            catch (MySqlException)
            {
                MySqlEd MySqlz = new MySqlEd();
               MySqlz.MySql_Command("INSERT INTO `bitcoinindex`.`errorsblocks` VALUES("+a+")");

            }
            finally
            {
                con.Close();
            }
            return UspeshnoeIzmenenie;
        }
    }
}
