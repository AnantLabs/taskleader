﻿using System;
using System.Windows.Forms;
using System.Configuration;
using ActionsList.BLL;

namespace ActionsList.GUI
{
    public class TrayIcon: ApplicationContext
    {

        private static NotifyIcon trayIcon = new NotifyIcon();
        private ContextMenuStrip trayContext = new ContextMenuStrip();
        private ToolStripMenuItem newActionItem = new ToolStripMenuItem();
        private ToolStripMenuItem closeItem = new ToolStripMenuItem();
        private ToolStripMenuItem maximItem = new ToolStripMenuItem();

        // Déclaration de tous les composants
        private void loadComponents()
        {
            // trayIcon
            trayIcon.ContextMenuStrip = this.trayContext;
            trayIcon.Icon = Properties.Resources.task_coach;
            trayIcon.Text = "GTD";
            trayIcon.Visible = true;
            trayIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);

            // Menu contextuel de la trayIcon
            this.trayContext.Items.AddRange(new ToolStripItem[] { this.newActionItem, this.maximItem, this.closeItem });
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
                this.displayToolbox(); // Affichage de la Toolbox
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
            ManipAction fenetre = new ManipAction();
            fenetre.Show();
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
