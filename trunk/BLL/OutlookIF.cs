using System;
using Microsoft.Office.Interop.Outlook;
using Microsoft.Office.Core;
using TaskLeader.GUI;

namespace TaskLeader
{
    public class OutlookIF
    {
        // Variable locale pour la référence à l'application Outlook
        private Application outlook;

        // Constructeur
        public OutlookIF()
        {
            // Création de l'object Outlook 
            this.outlook = new ApplicationClass();

            // Récupération de l'évènement ItemContextMenuDisplay
            this.outlook.ItemContextMenuDisplay += new ApplicationEvents_11_ItemContextMenuDisplayEventHandler(addEntrytoContextMenu);
        }

        // Méthode jouée si contextmenu affiché sur une entrée
        private void addEntrytoContextMenu(CommandBar menu, Selection Selection)
        {
            if (Selection.Count == 1 && Selection[1] is MailItem)
            {
                CommandBarControl item = menu.Controls.Add(MsoControlType.msoControlButton, 1, "", Type.Missing, true);
                item.Caption = "Créer une action";
                item.Visible = true;

                CommandBarButton itemClickHandler = (CommandBarButton)item;

                itemClickHandler.Click += new Microsoft.Office.Core._CommandBarButtonEvents_ClickEventHandler(getSelectedItem); // declare event handler 
            }
        }

        // Récupération des informations du mail
        private void getSelectedItem(CommandBarButton Ctrl, ref bool CancelDefault)
        {
            try
            {
                // Récupération de l'item sélectionné
                MailItem item = (MailItem)outlook.ActiveExplorer().Selection[1];
                // Et du sujet
                String sujet = item.Subject;

                // Récupération de la liste des propriétés
                PropertyAccessor props = item.PropertyAccessor;
                // Et du PR_INTERNET_MESSAGE_ID
                String id = (String)props.GetProperty("http://schemas.microsoft.com/mapi/proptag/0x1035001E");

                // On affiche la fenêtre nouvelle action Outlook
                TrayIcon.newActionOutlook(sujet, id);
            }
            catch (System.Exception Ex)
            {
                // On affiche l'erreur.
                TrayIcon.afficheMessage("Exception sur click Outlook", Ex.Message+"\n"+"Thread actuel:"+System.Threading.Thread.CurrentThread.GetApartmentState().ToString());
            }
        }

        private void search(String id)
        {
            outlook.ActiveExplorer().Search("",Microsoft.Office.Interop.Outlook.OlSearchScope.olSearchScopeAllFolders);
        }
    }
}
