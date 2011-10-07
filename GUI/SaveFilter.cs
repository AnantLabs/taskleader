using System;
using System.Windows.Forms;
using TaskLeader.DAL;

namespace TaskLeader.GUI
{
    public partial class SaveFilter : Form
    {
        private String dbName;
        private DB db { get { return TrayIcon.dbs[dbName]; } }

        public SaveFilter()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            if (nameBox.Text == "")
            {
                resultLabel.Text = "Le nom du filtre ne peut être vide";
                resultLabel.Visible = true;               
            }
            else
            {
                if (!db.isNvo(db.filtre,nameBox.Text))
                {
                    resultLabel.Text = "Ce nom de filtre existe déjà.";
                    resultLabel.Visible = true;
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }           
        }

        public DialogResult getFilterName(ref string name, String database){

            this.dbName = database;

            DialogResult result = this.ShowDialog();
            if (result == DialogResult.OK)
                name = nameBox.Text;

            return result;
        }

        private void nameBox_Enter(object sender, System.EventArgs e)
        {
            // Dans le cas d'un n-ième essai on efface le message d'erreur.
            resultLabel.Visible = false;
        }
    }
}
