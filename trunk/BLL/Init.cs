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
            int index;

            // Récupération de la liste des databases
            NameValueCollection dbData = (NameValueCollection)ConfigurationManager.GetSection("Databases");
            String defaultDBname = ConfigurationManager.AppSettings["defaultDB"];
            ArrayList databases = new ArrayList();

            foreach (String dbName in dbData)
            {
                if(!File.Exists(dbData[dbName]))
                     MessageBox.Show("Base " + dbName + " introuvable\nVérifier fichier de conf", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                {
                    index = databases.Add(new DB(dbName,dbData[dbName]));
                    if (!checkMigration((DB)databases[index]))
                        databases.RemoveAt(index);
                    else if (dbName == defaultDBname)
                        databases.Reverse(); // On passe la dernière DB ajoutée en tête de liste
                }
            }

            TrayIcon.dbs = databases.ToArray();
            return (databases.Count > 0); // Lancement si au moins une DB est valide   
        }


        // Migration
        private bool checkMigration(DB db)
        {
            // On vérifie que la version de la GUI est bien dans la base
            bool baseCompatible = db.isVersionComp(Application.ProductVersion.Substring(0, 3));

            if (!baseCompatible)
                if (TrayIcon.defaultDB.getLastVerComp() == "0.6")
                    return migrationOK("06-07");
                else
                {
                    MessageBox.Show("La base est trop ancienne pour une migration", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }         
            else
                return true; // La base est compatible, rien à faire.
        }

        private bool migrationOK(String change)
        {
            // Copie de sauvegarde du fichier db avant toute manip
            TrayIcon.afficheMessage("Migration","Copie de sauvegarde de la base");
            String sourceFile = ConfigurationManager.AppSettings["cheminDB"];
            String backupFile = sourceFile.Substring(0, sourceFile.Length - 4) + DateTime.Now.ToString("_Back-ddMMyyyy") + ".db3";
            System.IO.File.Copy(sourceFile, backupFile,true);

            // Récupération du script de migration
            try
            {
                String script = System.IO.File.ReadAllText(@"../Migration/" + change + ".sql", System.Text.Encoding.UTF8);

                // Exécution du script
                TrayIcon.afficheMessage("Migration", "Exécution du script de migration");
                TrayIcon.defaultDB.execSQL(script);

                // Nettoyage de la base
                TrayIcon.defaultDB.execSQL("VACUUM;");
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
