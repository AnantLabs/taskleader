using System;
using System.Windows.Forms;
using ActionsList.GUI;

namespace ActionsList
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
    
            //Affichage de la TrayIcon
            Application.Run(new TrayIcon());          
        }
    }
}
