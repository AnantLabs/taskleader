using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Outlook = Microsoft.Office.Interop.Outlook;
using Microsoft.Office.Core;
using TaskLeader.GUI;
using TaskLeader.BO;

namespace TaskLeader.BLL
{
    // Classe de conversion d'image pour affichage dans Outlook
    public class ConvertImage : System.Windows.Forms.AxHost
    {
        private ConvertImage() : base(null){}

        public static stdole.IPictureDisp Convert(System.Drawing.Image image)
        {
            return (stdole.IPictureDisp)System.Windows.Forms.AxHost.GetIPictureDispFromPicture(image);
        }
    }

    public class OutlookIF
    {
        // Attributs métiers       
        private Outlook.Application outlook = null;
        private CommandBarButton addActionButton;
        private String messageIDParam = "http://schemas.microsoft.com/mapi/proptag/0x1035001E";

        // Static Initialization Singleton Pattern
        private static readonly OutlookIF v_instance = new OutlookIF();
        public static OutlookIF Instance{get{return v_instance;}}
        
        private OutlookIF(){}

        public bool connectionNeeded{get{return Process.GetProcessesByName("outlook").Length != 0 && this.outlook == null;}}

        //Tentative de connexion à Outlook
        public void tryHook(bool forceCreation)
        {
            if (this.connectionNeeded || forceCreation)
            {
                this.outlook = new Outlook.ApplicationClass();
                this.outlook.ItemContextMenuDisplay += new Outlook.ApplicationEvents_11_ItemContextMenuDisplayEventHandler(addEntrytoContextMenu);
                ((Outlook.ApplicationEvents_11_Event)outlook).Quit += new Outlook.ApplicationEvents_11_QuitEventHandler(clean);
            }
        }
        
        // Méthode jouée si contextmenu affiché sur une entrée
        private void addEntrytoContextMenu(CommandBar menu, Outlook.Selection Selection)
        {
            if (Selection.Count == 1 && Selection[1] is Outlook.MailItem)
            {
                CommandBarControls controls = menu.Controls;
                this.addActionButton = (CommandBarButton)controls.Add(MsoControlType.msoControlButton, 1, "", Type.Missing, true);
                this.addActionButton.Caption = "Créer une action";
                //item.Style = MsoButtonStyle.msoButtonIconAndCaption;               
                //item.Picture = ConvertImage.Convert(TaskLeader.Properties.Resources.task_coach.ToBitmap());
                //Clipboard.SetDataObject(TaskLeader.Properties.Resources.task_coach.ToBitmap(), false);
                //item.PasteFace();
                this.addActionButton.Click += new Microsoft.Office.Core._CommandBarButtonEvents_ClickEventHandler(getSelectedItem);
                this.addActionButton.Visible = true;

                Marshal.FinalReleaseComObject(controls);
            }
        }

        // Méthode de nettoyage de l'objet COM Outlook Application
        private void clean()
        {
            Marshal.FinalReleaseComObject(this.addActionButton);
            this.addActionButton = null;
            Marshal.FinalReleaseComObject(this.outlook);
            this.outlook = null;
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

                Marshal.FinalReleaseComObject(store);
                Marshal.FinalReleaseComObject(item);
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
            if (this.outlook == null)
                this.tryHook(true);   
            
            Outlook.MailItem mail;

            if (tryGetMailFromId(action.EntryID, action.StoreID, out mail))
                mail.Display();
            else
            {
                // Le mail a été déplacé où supprimé
                TrayIcon.afficheMessage("I/F Outlook", "Le mail a été déplacé ou supprimé.");
                /*
                //TODO:Find email in Outlook by record’s MessageId
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

            if(mail !=null)
                Marshal.FinalReleaseComObject(mail);
        }
        
        // Récupération d'un mail en fonction de son entryID dans le store storeID
        private bool tryGetMailFromId(String entryId, String storeID, out Outlook.MailItem mail)
        {
            try
            {
                mail = outlook.Session.GetItemFromID(entryId, storeID) as Outlook.MailItem;
                return true;
            }
            catch
            {
                //Quelle que soit l'erreur le mail n'a pas été trouvé
                mail = null;
                return false;
            }
        }

        // Méthode permettant de catcher la fin de la recherche
        private void outlook_AdvancedSearchComplete(Outlook.Search searchObject)
        {
            // Vérification du nom de la recherche
            if (searchObject.Tag == "SearchMail" && searchObject.Results.Count > 0)
            {
                Outlook.MailItem mail = (Outlook.MailItem)searchObject.Results.GetFirst();
                mail.Display();//Affichage du mail
                //TODO: If found, update the EntryId of the record in the database so that future searches are fast.
            }
            else
                TrayIcon.afficheMessage("Recherche Outlook", "pas de résultats");

            // On sé désabonne de toutes les recherches avancées qui pourront être effectuées
            outlook.AdvancedSearchComplete -= outlook_AdvancedSearchComplete;
        }
    }
}
