using System;
using System.Diagnostics;
using System.Management;
using System.Linq;
using System.Runtime.InteropServices;
using Outlook = Microsoft.Office.Interop.Outlook;
using Microsoft.Office.Core;
using TaskLeader.GUI;
using TaskLeader.BO;

namespace TaskLeader
{
    public class OutlookIF
    {
        // Variable locale pour la référence à l'application Outlook
        private Outlook.Application outlook = null;
        private String messageIDParam = "http://schemas.microsoft.com/mapi/proptag/0x1035001E";
        
        // Variable locale pour stocker une référence vers l'instance
        private static OutlookIF v_instance = null;

        // Renvoie l'instance ou la crée
        public static OutlookIF Instance
        {
            get
            {
                if (v_instance == null)
                    v_instance = new OutlookIF();

                return v_instance;
            }
        }
        
        // Vérification de la présence d'un process Outlook running
        private bool outlookIsLaunched = (Process.GetProcessesByName("OUTLOOK").Count() > 0);
        
        // Ajout du menu contextuel à Outlook s'il est ouvert
        public void hookOutlook()
        {
            // Dans le cas suivant: this.outlookIsLaunched && this.outlook != null
            // En théorie tout est ok sauf si on a pas releasé l'objet Application

            if (this.outlookIsLaunched)
            {
                this.outlook = Marshal.GetActiveObject("Outlook.Application") as Outlook.Application;
                this.outlook.ItemContextMenuDisplay += new Outlook.ApplicationEvents_11_ItemContextMenuDisplayEventHandler(addEntrytoContextMenu);
                //this.outlook.Quit += Outlook.ApplicationEvents_11_QuitEventHandler(); //Compléter avec le nom de la méthode
            }
            else
            {
                ManagementEventWatcher startWatch =
                    new ManagementEventWatcher(new WqlEventQuery("SELECT * FROM Win32_ProcessStartTrace WHERE ProcessName = 'OUTLOOK.EXE'"));
                startWatch.EventArrived += new EventArrivedEventHandler(startWatch_EventArrived);
                startWatch.Start();
            }
        }

        private void startWatch_EventArrived(object sender, EventArrivedEventArgs e)
        {
            TrayIcon.afficheMessage("ProcessWatch", "Ouverture d'Outlook");
            this.hookOutlook();
            // On arrête le watch ?
        }

        // Méthode jouée si contextmenu affiché sur une entrée
        private void addEntrytoContextMenu(CommandBar menu, Outlook.Selection Selection)
        {
            if (Selection.Count == 1 && Selection[1] is Outlook.MailItem)
            {
                //TODO: récupérer d'abord Controls pour pouvoir le libérer ensuite
                CommandBarButton item = (CommandBarButton)menu.Controls.Add(MsoControlType.msoControlButton, 1, "", Type.Missing, true);
                item.Caption = "Créer une action";
                item.Visible = true;
                item.Click += new Microsoft.Office.Core._CommandBarButtonEvents_ClickEventHandler(getSelectedItem);
                //TODO: libérer la ressource CommandBarButton  
            }
        }

        // Récupération des informations du mail
        private void getSelectedItem(CommandBarButton Ctrl, ref bool CancelDefault)
        {
            try
            {
                // Récupération de l'item sélectionné
                Outlook.MailItem item = (Outlook.MailItem)outlook.ActiveExplorer().Selection[1];

                // Création de l'action scratchpad
                TLaction action = new TLaction(item.Subject);

                //Récupération du store ID
                Outlook.MAPIFolder store = (Outlook.MAPIFolder)item.Parent;
                action.StoreID = store.StoreID;

                // Récupération de l'entryID
                action.EntryID = item.EntryID;

                // Récupération du message-ID (PR_INTERNET_MESSAGE_ID)
                action.MessageID = (String)item.PropertyAccessor.GetProperty(this.messageIDParam);

                action.freezeInitState();

                // On affiche la fenêtre nouvelle action Outlook
                TrayIcon.newActionOutlook(action);
                
              //TODO: libérer le MAPIFolder + le MailItem
            }
            catch (System.Runtime.InteropServices.COMException e)
            {
                // On affiche l'erreur
                TrayIcon.afficheMessage("Exception sur récupération Outlook", e.Message+"\n"+e.ErrorCode.ToString());
            }
        }
        
        // Affichage d'un mail à partir de son ID
        public void displayMail(TLaction action)
        {
            // Récupération de l'objet Application
            if (this.outlook == null && this.outlookIsLaunched)
                    outlook = Marshal.GetActiveObject("Outlook.Application") as Outlook.Application;
            else if (this.outlook == null && !this.outlookIsLaunched)  
                    outlook = new Outlook.ApplicationClass(); //Cela n'affiche pas l'application
            else if (this.outlook != null && !this.outlookIsLaunched)
            {
                //TODO: l'objet application doit être relaché
                outlook = new Outlook.ApplicationClass();
            }                    

            /*
            Look into the database for all records that have value of our data equal to x.
            For each such record:
                Find email in Outlook by record’s EntryId (fast). If found, done.
                If not found it means the email has been moved or deleted:
                    Find email in Outlook by record’s MessageId.
                    If found, update the EntryId of the record in the database so that future searches are fast.
                    If not found, the email does not exist (was deleted).
            */
            
            Outlook.MailItem mail;

            if (tryGetMailFromId(action.EntryID, action.StoreID, out mail))
                mail.Display();
            else
            {
                // Le mail a été déplacé où supprimé
                TrayIcon.afficheMessage("Recherche", "çà va être long");
                /*
                const String PROPTAG = "http://schemas.microsoft.com/mapi/proptag/";
                const String PR_INTERNETID = "0x1035001E";
                String propName = PROPTAG + PR_INTERNETID;

                String query = propName + "='" + messageID + "'";

                String scope = "'" + outlook.ActiveExplorer().CurrentFolder.FolderPath + "'";
                // Hook de l'évènement AdvancedSearchComplete
                this.outlook.AdvancedSearchComplete += new Outlook.ApplicationEvents_11_AdvancedSearchCompleteEventHandler(outlook_AdvancedSearchComplete);
                // Perform the search, asynchronously.
                outlook.AdvancedSearch(scope, query, true, "SearchMail");
               */
            }      
        }
        
        // Récupération d'un mail en fonction de son entryID dans le store storeID
        private bool tryGetMailFromId(String entryId, String storeID, out Outlook.MailItem mail)
        {
            try
            {
                mail = outlook.Session.GetItemFromID(entryId, storeID) as Outlook.MailItem;
                return true;
            }
            catch (System.Runtime.InteropServices.COMException e)
            {
                if ((uint)e.ErrorCode == 0x80040107)
                {
                    // MAPI_E_NOT_FOUND
                    mail = null;
                    return false;
                }
                throw;
            }
        }

        private void outlook_AdvancedSearchComplete(Outlook.Search searchObject)
        {
            // Vérification du nom de la recherche
            if (searchObject.Tag == "SearchMail" && searchObject.Results.Count > 0)
            {
                Outlook.MailItem mail = (Outlook.MailItem)searchObject.Results.GetFirst();
                mail.Display();//Affichage du mail
            }
            else
                TrayIcon.afficheMessage("Recherche Outlook", "pas de résultats");
 
            outlook.AdvancedSearchComplete -= outlook_AdvancedSearchComplete;
        }
    }
}
