using System;
using System.Windows.Forms;
using TaskLeader.BO;

namespace TaskLeader.GUI
{
    public partial class SaveLink : Form
    {
        public Link lien;

        public SaveLink()
        {
            InitializeComponent();
        }

        private void fileBut_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                String path = openFileDialog.FileName;
                this.pathBox.Text = path;
                this.titreBox.Text = path.Substring(path.LastIndexOf(@"\")+1);
            }
        }

        private void saveBut_Click(object sender, EventArgs e)
        {
            lien = new Link(titreBox.Text, pathBox.Text);
            this.DialogResult = DialogResult.OK;
        }
    }
}
