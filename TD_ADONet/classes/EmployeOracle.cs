using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeDatas.Oracle
{
    class EmployeOracle
    {
        private String host;
        private int port;
        private String db;
        private String login;
        private String pwd;
        private OracleConnection connexion;

        public EmployeOracle(string host, int port, string db, string login, string pwd)
        {
            this.host = host;
            this.port = port;
            this.db = db;
            this.login = login;
            this.pwd = pwd;
            this.connexion = new OracleConnection($"SERVER=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={host})(PORT={port}))(CONNECT_DATA=(SERVICE_NAME=MyOracleSID)));uid={login};pwd={pwd};");
        }

        public void Ouvrir()
        {
            this.connexion.Open();
        }

        public void Fermer()
        {
            this.connexion.Close();
        }

        public void afficherTousLesCours()
        {
            string requete = "select * from cours";
            try
            {
                Ouvrir();
                OracleCommand commande = new OracleCommand(requete, this.connexion);
                var unReader = commande.ExecuteReader();
                while (unReader.Read())
                {
                    String numCours = unReader.GetString(0);
                    String libelleCours = unReader.GetString(1);
                    int nbJours = unReader.GetInt32(2);
                    Console.WriteLine("Numéro du cours = : {0}\n Libellé du cours : {1}\n Nombre de jours : {2}", numCours);
                }
                Fermer();
            }
            catch(OracleException ex)
            {
                Console.WriteLine("Erreur Oracle " + ex.Message);
            }
        }
    }
}
