using System;
using System.Configuration;
using System.Windows.Forms;
using TaskLeader.BLL;
using System.Threading;

namespace TaskLeader.GUI
{
    public class TrayIcon: ApplicationContext
    {
        // Déclaration des composants IHM
        private static NotifyIcon trayIcon = new NotifyIcon();
        private ContextMenuStrip trayContext = new ContextMenuStrip();
        private ToolStripMenuItem newActionItem = new ToolStripMenuItem();
        private ToolStripMenuItem newOutlookActionItem = new ToolStripMenuItem();
        private ToolStripMenuItem closeItem = new ToolStripMenuItem();
        private ToolStripMenuItem maximItem = new ToolStripMenuItem();
        
        // Déclaration des composants métiers
        OutlookIF outlook;
        ManipAction guiAction = new ManipAction();

        // Déclaration de tous les composants
        private void loadComponents()
        {
            // trayIcon
            trayIcon.ContextMenuStrip = this.trayContext;
            trayIcon.Icon = Properties.Resources.task_coach;
            trayIcon.Text = "TaskLeader";
            trayIcon.Visible = true;
            trayIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);

            // Menu contextuel de la trayIcon
            this.trayContext.Items.AddRange(new ToolStripItem[] { this.newActionItem, this.newOutlookActionItem, this.maximItem, this.closeItem });
            this.trayContext.Name = "trayContext";
            this.trayContext.ShowImageMargin = false;
            this.trayContext.Size = new System.Drawing.Size(126, 70);

            // Item "nouvelle action" du menu contextuel
            this.newActionItem.Name = "newActionItem";
            this.newActionItem.ShowShortcutKeys = false;
            this.newActionItem.Size = new System.Drawing.Size(125, 22);
            this.newActionItem.Text = "Nouvelle action";
            this.newActionItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.newActionItem.Click += new System.EventHandler(this.ajoutAction);

            // Item "nouvelle action Outlook" du menu contextuel
            this.newOutlookActionItem.Name = "newOutlookActionItem";
            this.newOutlookActionItem.ShowShortcutKeys = false;
            this.newOutlookActionItem.Size = new System.Drawing.Size(125, 22);
            this.newOutlookActionItem.Text = "Nouvelle action Outlook";
            this.newOutlookActionItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.newOutlookActionItem.Click += new System.EventHandler(this.ajoutAction);

            // Item "afficher Toolbox" du menu contextuel
            this.maximItem.Name = "maximItem";
            this.maximItem.Size = new System.Drawing.Size(125, 22);
            this.maximItem.Text = "Afficher toolbox";
            this.maximItem.Click += new System.EventHandler(this.maximItem_Click);

            // Item "fermer" du menu contextuel
            this.closeItem.Name = "closeItem";
            this.closeItem.Size = new System.Drawing.Size(125, 22);
            this.closeItem.Text = "Fermer";
            this.closeItem.Click += new System.EventHandler(this.closeItem_Click);
        }

        // Constructeur de la NotifyIcon
        public TrayIcon()
        {
            // On charge tous les composants
            this.loadComponents();

            // Vérification de démarrage
            if (Init.Instance.canLaunch())
            {
                this.displayToolbox(); // Affichage de la Toolbox
                this.outlook = new OutlookIF();
                outlook.NewActionEvent += new NewActionHandler(outlook_NewActionEvent);
                trayContext.CreateControl();
            }
            else
                this.closeApp(); // On ferme l'appli
        }

        private Toolbox v_toolbox = null;

        // Méthode générique d'affichage de la Toolbox
        private void displayToolbox()
        {
            if (v_toolbox == null || v_toolbox.IsDisposed) // Si la fenêtre n'a jamais été ouverte ou fermée, on l'ouvre
            {
                v_toolbox = new Toolbox();
                v_toolbox.WindowState = FormWindowState.Maximized;
                v_toolbox.Show();
            }
            else
                v_toolbox.BringToFront(); // Sinon on l'affiche au premier plan     
        }

        // Double-click sur la trayIcon
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.displayToolbox();
        }

        // Ajout d'action
        private void ajoutAction(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            ManipAction fenetre;

            if (item.Name=="newActionItem")
                fenetre = new ManipAction();
            else // C'est une nouvelle action Outlook
            {
                //String[] outlookData = OutlookIF.Instance.getSelectedItem(null,null);
                String[] outlookData = { "Titi", "" };
                fenetre = new ManipAction(outlookData[0], outlookData[1]); //O:Sujet, 1:IDMail
            }
                
            fenetre.Show();
        }
        

        private void outlook_NewActionEvent(object sender, NewActionFromOutlook e)
        {

          this.guiAction.Invoke(new newActionfromOulookCallback(afficheActionForm), new object[] { e.sujet, e.id });
            
        }

        delegate void newActionfromOulookCallback(String sujet, String id);

        private void afficheActionForm(String sujet, String id)
        {
            this.guiAction.TopMost = true;
            this.guiAction.updateField(sujet, id);
            this.guiAction.Show();           
        }
        

        // Demande d'affichage de la Toolbox via le ContextMenuStrip
        private void maximItem_Click(object sender, EventArgs e)
        {
            this.displayToolbox();
        }

        // Méthode générique de fermeture de l'appli
        private void closeApp()
        {
            trayIcon.Visible = false; 
            Application.Exit();
        }

        // Demande de fermeture via le ContextMenuStrip
        private void closeItem_Click(object sender, EventArgs e)
        {
            this.closeApp();
        }

        // Méthode pour affichage de message
        public static void afficheMessage(String titre, String info)
        {
            // Récupération de la durée d'affichage de la tooltip
            int duree = int.Parse(ConfigurationManager.AppSettings["dureeTooltip"]);

            // Affichage du bilan en tooltip de la tray icon
            trayIcon.ShowBalloonTip(duree, titre, info, ToolTipIcon.Info);
        }
    }
}
