using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeDatas.Oracle
{
    class EmployeMysql
    {
        private String host;
        private int port;
        private String db;
        private String login;
        private String pwd;
        private MySqlConnection connexion;

        public EmployeMysql(string host, int port, string db, string login, string pwd)
        {
            this.host = host;
            this.port = port;
            this.db = db;
            this.login = login;
            this.pwd = pwd;
            this.connexion = new MySqlConnection($"Server = {host}; Port = {port}; Database = {db}; Uid = {login}; Pwd = {pwd};");
        }

        public void Ouvrir()
        {
            this.connexion.Open();
        }

        public void Fermer()
        {
            this.connexion.Close();
        }
    }
}
