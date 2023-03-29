using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD_ADONet
{
    class Program
    {
        static void Main(string[] args)
        {
            String host = "10.10.2.10";
            int port = 1521;
            string sid = "slam";
            string login = "banceado";
            string pwd = "sio";
            try
            {
                String cs = String.Format("Data Source= " + 
                    "(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = {0})(PORT = {1}))" +
                    "(CONNECT_DATA = (SERVICE_NAME = {2}))); User Id = {3}; Password = {4};", host, port, sid, login, pwd);
                OracleConnection cnOracle = new OracleConnection(cs);
                cnOracle.Open();
                Console.WriteLine("Connecté Oracle");
                cnOracle.Close();
                Console.WriteLine("Déconnecté Oracle");
            }
            catch(OracleException ex)
            {
                Console.WriteLine("Erreur Oracle " + ex.Message);
            }

            string hostMysql = "127.0.0.1";
            int portMysql = 3306;
            string baseMysql = "dbadonet";
            String uidMysql = "employeado";
            String pwdMysql = "employeado";
            try
            {
                String csMysql = String.Format("Server = {0}; Port = {1}; Database = {2}; " +
                    "Uid = {3}" + "Pwd = {4}", hostMysql, portMysql, baseMysql, uidMysql, pwdMysql);
                MySqlConnection cnMysql = new MySqlConnection(csMysql);
                cnMysql.Open();
                Console.WriteLine("connecté Mysql");
                cnMysql.Close();
                Console.WriteLine("déconnecté Mysql");
            }
            catch(MySqlException ex)
            {
                Console.WriteLine("Erreur Mysql " + ex.Message);
            }
            Console.ReadKey();
        }
    }
}
