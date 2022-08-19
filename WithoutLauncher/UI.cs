using System.Diagnostics;

namespace WithoutLauncher
{
    public partial class Core : Form
    {

        string koPath = "";

        public Core()
        {
            InitializeComponent();
            koPath = textBox1.Text;
            button2.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog OF = new OpenFileDialog();
            OF.Filter = "KnightOnline (*.exe) | *exe";
            OF.CheckPathExists = true;
            OF.Title = "Select KnightOnline.exe";

            if (OF.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                koPath = (OF.FileName);
                textBox1.Text = koPath;
            }
        }

        void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox1.Text == null)
            {
                MessageBox.Show("Please select or type the directory path!");
                return;
            }

            Process Process = new Process();
            FileInfo FileInfo = new FileInfo(koPath);

            if (!FileInfo.Exists)
            {
                MessageBox.Show("KnightOnline.exe not found in the specified file path.");
                return;
            }
            
            Process.StartInfo = new ProcessStartInfo(FileInfo.FullName)
            {
                WorkingDirectory = FileInfo.Directory.FullName,
                Arguments = $"{Environment.ProcessId}",
                Verb = "runas",
                UseShellExecute = true
            };
            
            if (FileInfo.Name == "KnightOnline.exe")
            {
                Process.Start();
            }

            else
            {
                MessageBox.Show("Please select KnightOnline.exe");
                return;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //Shows a warning if one or more characters are selected and then deleted with the space bar.
            var textBox1 = (TextBox)sender;
            if (textBox1.Text.StartsWith(" "))
            {
                MessageBox.Show("Can not have spaces in the First Position");
                return;
            }
            //
            if (!string.IsNullOrEmpty(koPath = textBox1.Text))
            {
                button2.Enabled = true;
            }
         
        }

        //If first character begin space, program crash.
        void SpaceValidation(KeyPressEventArgs e)
        {
            if (e.KeyChar == 32 && ActiveControl.Text.Length == 0)
                e.Handled = true;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            SpaceValidation(e);
        }
    }        
}