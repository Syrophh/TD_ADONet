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
            this.connexion = new OracleConnection($"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={host})(PORT={port}))(CONNECT_DATA=(SERVICE_NAME={db})));User Id={login};Password={pwd};");
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
                    Console.WriteLine("Numéro du cours = : {0}\n Libellé du cours : {1}\n Nombre de jours : {2}", numCours, libelleCours, nbJours);
                }
                Fermer();
            }
            catch(OracleException ex)
            {
                Console.WriteLine("Erreur Oracle " + ex.Message);
            }
        }

        public void afficherNbProjets()
        {
            string requete = "select count(*) as nbProjet from projet";
            try
            {
                Ouvrir();
                OracleCommand commande = new OracleCommand(requete, this.connexion);
                var unReader = commande.ExecuteReader();
                while (unReader.Read())
                {
                    int nbProjet = unReader.GetInt32(0);
                    Console.WriteLine("Le nombre de projet : {0}\n", nbProjet);
                }
                Fermer();
            }
            catch(OracleException ex)
            {
                Console.WriteLine("Erreur Oracle " + ex.Message);
            }
        }

        public void afficherSalaireMoyenParProjet()
        {
            string requete = "select coalesce(projet.codeprojet, 'aucun') as codeprojet, projet.nomprojet, avg(employe.salaire) as moyenne " +
                "from projet inner join employe on employe.codeprojet = projet.codeprojet " +
                "group by projet.codeprojet, projet.nomprojet";
            try
            {
                Ouvrir();
                OracleCommand commande = new OracleCommand(requete, this.connexion);
                var unReader = commande.ExecuteReader();
                while (unReader.Read())
                {
                    String codeProjet = unReader.GetString(0);
                    String nomProjet = unReader.GetString(1);
                    int moyenne = unReader.GetInt32(2);
                    Console.WriteLine("Code du projet : {0}\n Nom du projet : {1}\n Moyenne des salaires par projet : {2}\n", codeProjet, nomProjet, moyenne);
                }
                Fermer();
            }
            catch (OracleException ex)
            {
                Console.WriteLine("Erreur Oracle " + ex.Message);
            }
        }

        public void augmenterSalaireCurseur()
        {
            string requete = "select * from employe where codeprojet = 'PR1'; ";
            try
            {
                Ouvrir();
                OracleCommand command = new OracleCommand(requete, this.connexion);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                var unReader = command.ExecuteReader();
                while (unReader.Read())
                {
                    requete = "update employe set salaire = salaire * 1.03";
                    command.CommandText = requete;
                    Console.WriteLine("Employes modifiés");
                }
                Fermer();
            }
            catch (OracleException ex)
            {
                Console.WriteLine("Erreur Oracle " + ex.Message);
            }
        }

        public void afficherEmployesSalaire()
        {
            string requete = "select numemp, nomemp, prenomemp, salaire from employe where salaire< 10000";
             try
             {
                 Ouvrir();
                 OracleCommand commande = new OracleCommand(requete, this.connexion);
                 var unReader = commande.ExecuteReader();
                 while (unReader.Read())
                 {
                    String numEmp = unReader.GetString(0);
                    String nomEmp = unReader.GetString(1);
                    String prenomEmp = unReader.GetString(2);
                    int salaire = unReader.GetInt32(3);
                    Console.WriteLine("Numero de l'employe : {0}\n Nom de l'employe : {1}\n Prenom de l'employe : {2}\n salaire de l'employe : {3}", numEmp, nomEmp, prenomEmp, salaire);
                 }
                 Fermer();
             }
             catch (OracleException ex)
             {
                 Console.WriteLine("Erreur Oracle " + ex.Message);
             }
        }

        public void afficherSalaireEmploye()
        {
            string requete = "select numemp, nomemp, prenomemp, salaire from employe where numemp = 76";
            try
            {
                Ouvrir();
                OracleCommand commande = new OracleCommand(requete, this.connexion);
                var unReader = commande.ExecuteReader();
                while (unReader.Read())
                {
                    String numEmp = unReader.GetString(0);
                    String nomEmp = unReader.GetString(1);
                    String prenomEmp = unReader.GetString(2);
                    int salaire = unReader.GetInt32(3);
                    Console.WriteLine("Numero de l'employe : {0}\n Nom de l'employe : {1}\n Prenom de l'employe : {2}\n salaire de l'employe : {3}", numEmp, nomEmp, prenomEmp, salaire);
                }
                Fermer();
            }
            catch (OracleException ex)
            {
                Console.WriteLine("Erreur Oracle " + ex.Message);
            }
        }

        public static void insereCours(EmployeOracle employeOracle, string codeCours, string libelleCours, int nbJours)
        {
            string requete = "insert into cours (codecours, libellecours, nbjours) values('BR099', 'Apprentissage', 4)";
            try
            {
                OracleCommand commande = new OracleCommand(requete, employeOracle.connexion);

            }
        }
    }
}
