 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TaskLeader.BO;
using TaskLeader.DAL;
using TaskLeader.GUI;

namespace TaskLeader.BLL
{
    public class DataManager
    {
        // Variable locale pour stocker une référence vers l'instance
        private static DataManager v_instance = null;

        // Renvoie l'instance ou la crée
        public static DataManager Instance
        {
            get
            {
                // Si pas d'instance existante on en crée une...
                if (v_instance == null)
                    v_instance = new DataManager();

                // On retourne l'instance de MonSingleton
                return v_instance;
            }
        }

        // Renvoie la DataTable des actions
        public DataTable getActions(Filtre filtre)
        {
            // Stockage du filtre
            Filtre.CurrentFilter = filtre;

            // Récupération de la liste d'actions
            DataTable data = ReadDB.Instance.getActions(filtre.criteria);

            return data;
        }
        
        // Méthode commune à la création et à l'édition d'action pour vérification nouveauté
        private String verifAndInsert(String contexte, String subject, String destinataire)
        {
            String bilan = "";
            int resultat;

            if (contexte != "") // Test uniquement si contexte entré
                if (ReadDB.Instance.isNvoContexte(contexte)) // Si on a un nouveau contexte
                {
                    resultat = WriteDB.Instance.insertContexte(contexte); // On récupère le nombre de lignes insérées
                    if (resultat == 1)
                        bilan += "Nouveau contexte enregistré\n";
                }

            if (subject != "")
                if (ReadDB.Instance.isNvoSujet(contexte, subject)) //TODO: il y a un cas foireux si le contexte est vide
                {
                    resultat = WriteDB.Instance.insertSujet(contexte, subject);
                    if (resultat == 1)
                        bilan += "Nouveau sujet enregistré\n";
                }

            if (destinataire != "")
                if (ReadDB.Instance.isNvoDest(destinataire))
                {
                    resultat = WriteDB.Instance.insertDest(destinataire);
                    if (resultat == 1)
                        bilan += "Nouveau destinataire enregistré\n";
                }

            return bilan;
        }

        // Création d'une nouvelle action
        //public void createAction(String contexte, String subject, String desAction, bool dateNotChosen, DateTime date, String destinataire, String mailID, String stat)
        public void createAction(TLaction action)
        {
            String bilan = "";
            int resultat;

            // Vérification des nouveautés
            bilan += verifAndInsert(action.Texte, action.Sujet, action.Destinataire);               

            if (action.Texte != "")
            {
                resultat = WriteDB.Instance.insertAction(action);
                if (resultat == 1)
                    bilan += "Nouvelle action enregistrée\n";
            }

            // On affiche un message de statut sur la TrayIcon
            if (bilan.Length > 0) // On n'affiche un bilan que s'il s'est passé qqchose
                TrayIcon.afficheMessage("Bilan création",bilan.Substring(0, bilan.Length - 1)); // On supprime le dernier \n
        }

        // Update d'une action
        public void updateAction(String contexte, String subject, String action, bool dateNotChosen, DateTime date, String destinataire, bool statutChanged, String stat, String id)
        {
            String desAction = action;

            // On rajoute une ligne d'historique si le statut est différent de Ouverte et si le statut a changé
            if (stat != ReadDB.Instance.getDefaultStatus() && statutChanged)
                desAction += Environment.NewLine+"Action "+stat+" le: " + DateTime.Now.ToString("dd-MM-yyyy");              
            //TODO: griser le bouton Sauvegarder si rien n'a été édité

            String bilan = "";
            int resultat;

            // Vérification des nouveautés
            bilan += verifAndInsert(contexte, subject, destinataire);

            String dueDate;
            if (dateNotChosen)
                dueDate = "";
            else
                dueDate = date.ToString("yyyy-MM-dd"); // Mise au format SQL

            if (desAction != "")
            {
                resultat = WriteDB.Instance.updateAction(true,contexte, true,subject, true,desAction, true, dueDate, true,destinataire, statutChanged,stat,id);
                if (resultat == 1)
                    bilan += "Action mise à jour\n";
            }

            // On affiche un message de statut sur la TrayIcon
            if (bilan.Length > 0) // On n'affiche un bilan que s'il s'est passé qqchose
                TrayIcon.afficheMessage("Bilan modification",bilan.Substring(0, bilan.Length - 1)); // On supprime le dernier \n
        }
        
        // Update d'une méthode
        public void updateAction(String action, String stat, String id)
        {
            // On rajoute une ligne d'historique car on est sûr que le statut a changé
            action += Environment.NewLine+"Action "+stat+" le: " + DateTime.Now.ToString("dd-MM-yyyy");              

            int resultat;
            String bilan = "";

            resultat = WriteDB.Instance.updateAction(false,"",false,"",true,action,false,"",false,"",true,stat,id);
            if (resultat == 1)
                bilan = "Action mise à jour";
            
            // On affiche un message de statut sur la TrayIcon
            if (bilan.Length > 0) // On n'affiche un bilan que s'il s'est passé qqchose
                TrayIcon.afficheMessage("Bilan modification",bilan);
            
        }
    }
}
