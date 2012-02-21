using System;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Windows.Forms;
using TaskLeader.GUI;
using TaskLeader.DAL;

namespace TaskLeader.BLL
{
    public class Init
    {
        // Variable locale pour stocker une référence vers l'instance
        private static Init v_instance = null;

        // Renvoie l'instance ou la crée
        public static Init Instance
        {
            get
            {
                // Si pas d'instance existante on en crée une...
                if (v_instance == null)
                    v_instance = new Init();

                // On retourne l'instance de MonSingleton
                return v_instance;
            }
        }

        public bool canLaunch()
        {
            //TODO: il faudrait vérifier s'il n'y a pas de doublons dans la liste des DBs

            bool defaultDBok = false;

            // Récupération de la liste des databases
            NameValueCollection dbData = (NameValueCollection)ConfigurationManager.GetSection("Databases");
            String defaultDBname = ConfigurationManager.AppSettings["defaultDB"];

            foreach (String dbName in dbData)
            {
                if(!File.Exists(dbData[dbName]))
                    MessageBox.Show("Base " + dbName + " introuvable\nVérifier fichier de conf", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                {
                    TrayIcon.dbs.Add(dbName,new DB(dbData[dbName],dbName));
                    if (!DBiscompatible(dbName))
                        TrayIcon.dbs.Remove(dbName);
                    else if (dbName == defaultDBname)
                        defaultDBok = true;
                }
            }

            return defaultDBok; // Lancement si la DB par défaut est au moins ok
        }

        // Migration
        private bool DBiscompatible(String dbName)
        {
            // On vérifie que la version de la GUI est bien dans la base
            bool baseCompatible = TrayIcon.dbs[dbName].isVersionComp(Application.ProductVersion.Substring(0, 3));

            if (!baseCompatible)
                if (TrayIcon.dbs[dbName].getLastVerComp() == "0.6")
                    return tryMigration("06-07", dbName);
                else
                {
                    MessageBox.Show("La base est trop ancienne pour une migration", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            else
                return true; // La base est compatible, rien à faire.
        }

        private bool tryMigration(String change, String dbName)
        {
            // Copie de sauvegarde du fichier db avant toute manip
            TrayIcon.afficheMessage("Migration", "Copie de sauvegarde de la base");
            String sourceFile = TrayIcon.dbs[dbName].path;
            String backupFile = sourceFile.Substring(0, sourceFile.Length - 4) + DateTime.Now.ToString("_Back-ddMMyyyy") + ".db3";
            System.IO.File.Copy(sourceFile, backupFile, true);

            // Récupération du script de migration
            try
            {
                String script = System.IO.File.ReadAllText(@"../Migration/" + change + ".sql", System.Text.Encoding.UTF8);

                // Exécution du script
                TrayIcon.afficheMessage("Migration", "Exécution du script de migration");
                TrayIcon.dbs[dbName].execSQL(script);

                // Nettoyage de la base
                TrayIcon.dbs[dbName].execSQL("VACUUM;");
                TrayIcon.afficheMessage("Migration", "Migration de la base effectuée");

                return true;
            }
            catch
            {
                //TODO:affiner le pourquoi
                TrayIcon.afficheMessage("Migration", "Fichier de migration introuvable");

                return false;
            }
        }
    }
}
