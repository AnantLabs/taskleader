using System;
using System.Threading;
using System.Windows.Forms;
using TaskLeader.GUI;
using TaskLeader.BLL;

namespace TaskLeader
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (Mutex mutex = new Mutex(false, @"Global\6fd1e7cc-9bb5-4154-9798-a36e6239d34d"))
            {
                if (!mutex.WaitOne(0, false))
                {
                    MessageBox.Show("Application déjà lancée", "TaskLeader");
                    return;
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                //Hook d'outlook si possible
                OutlookIF.Instance.tryHook(false);

                //Affichage de la TrayIcon
                Application.Run(new TrayIcon()); 
            }         
        }
    }
}
