using System.Windows.Forms;

namespace TaskLeader.GUI
{
    public partial class InputDialog : Form
    {
        public InputDialog()
        {
            InitializeComponent();

            /* code perso relatif à mon implémentation, en gros on veut pas que ça soit redimensionable mais il faut pouvoir fermer */
            MaximizeBox = MinimizeBox = false;
            //CanResize = false;
            //CanMove = true;
        }

        public static DialogResult ShowInputDialog(string Title, string Caption, ref string selected_text)
        {
            using (InputDialog dlg = new InputDialog())
            {
                /*
                dlg.captionText.Text = Caption;
                dlg.Text = Title;
                dlg.valueBox.Text = selected_text;
                */
                
                DialogResult result = dlg.ShowDialog();
                
                /*
                if (result == DialogResult.OK)
                    selected_text = dlg.valueBox.Text;
                */

                return result;
                 
            }
        }
    }
}
