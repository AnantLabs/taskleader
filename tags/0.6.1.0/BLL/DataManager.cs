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

        // Sauvegarde d'une action en base
        public void saveAction(TLaction action)
        {
            String bilan = "";
            int resultat;

            // On rajoute une ligne d'historique si le statut est différent de Ouverte et si le statut a changé
            if (action.Statut != ReadDB.Instance.getDefaultStatus() && action.statusHasChanged)
                action.Texte += Environment.NewLine + "Action " + action.Statut + " le: " + DateTime.Now.ToString("dd-MM-yyyy");

            // Vérification des nouveautés
            if (action.ctxtHasChanged) // Test uniquement si contexte entré
                if (ReadDB.Instance.isNvoContexte(action.Contexte)) // Si on a un nouveau contexte
                {
                    resultat = WriteDB.Instance.insertContexte(action.Contexte); // On récupère le nombre de lignes insérées
                    if (resultat == 1)
                        bilan += "Nouveau contexte enregistré\n";
                }

            if (action.sujetHasChanged)
                if (ReadDB.Instance.isNvoSujet(action.Contexte, action.Sujet)) //TODO: il y a un cas foireux si le contexte est vide
                {
                    resultat = WriteDB.Instance.insertSujet(action.Contexte, action.Sujet);
                    if (resultat == 1)
                        bilan += "Nouveau sujet enregistré\n";
                }

            if (action.destHasChanged)
                if (ReadDB.Instance.isNvoDest(action.Destinataire))
                {
                    resultat = WriteDB.Instance.insertDest(action.Destinataire);
                    if (resultat == 1)
                        bilan += "Nouveau destinataire enregistré\n";
                }

            if (action.Texte != "")
            {
                if (action.isScratchpad)
                {
                    WriteDB.Instance.insertAction(action);
                    bilan += "Nouvelle action enregistrée\n";
                }
                else
                {
                    resultat = WriteDB.Instance.updateAction(action);
                    if (resultat == 1)
                        bilan += "Action mise à jour\n";
                }
            }

            // On affiche un message de statut sur la TrayIcon
            if (bilan.Length > 0) // On n'affiche un bilan que s'il s'est passé qqchose
                TrayIcon.afficheMessage("Bilan sauvegarde", bilan.Substring(0, bilan.Length - 1)); // On supprime le dernier \n            
        }
    }
}
