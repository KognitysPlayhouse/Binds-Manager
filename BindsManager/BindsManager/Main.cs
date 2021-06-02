using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static BindsManager.Classes.InputBox;

namespace BindsManager
{
	public partial class Main : Form
	{
		public string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SCP Secret Laboratory", "cmdbinding.txt");
        public string import_folder_path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "BindsManager");

        public Main(string file)
		{
			InitializeComponent();
            using (StreamReader reader = new StreamReader(file))
            {
                richTextBox1.Text = reader.ReadToEnd().Replace(Environment.NewLine, "\n");
                MessageBox.Show("This preset has been succesfully loaded!!", "Opened preset!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } 
        }

		private void Main_Load(object sender, EventArgs e)
		{
            if (!File.Exists(Path.Combine(Classes.FileManagerUtils.StartMenu, "BindsManager")))
            {
                Classes.FileManagerUtils.AddToStartmenu();
            }
			ToolTip keyToolTip = new ToolTip();
			keyToolTip.ShowAlways = true;
			keyToolTip.SetToolTip(comboBox1, "Select a Key to bind the command to.");

			ToolTip commandTip = new ToolTip();
			commandTip.ShowAlways = true;
			commandTip.SetToolTip(commandTextBox, "Type the command you want to bind, e.g. kill.");

			richTextBox1.Text = File.ReadAllText(path);

			richTextBox1.SelectionStart = richTextBox1.Text.Length;
			richTextBox1.Focus();

			foreach (var key in UnityKeyCodes.Keycodes.Keys)
			{
				comboBox1.Items.Add(key);
			}

            Directory.CreateDirectory(import_folder_path); // Create (internally ignores if already exists.)
		}

		private void clearButton_Click(object sender, EventArgs e)
		{
			richTextBox1.Text = "";
		}

		private void autoFillButton_Click(object sender, EventArgs e)
		{
			richTextBox1.Text += "49:.ability1\n50:.ability2\n51:.ability3\n52:.ability4\n102:.zfe\n";
		}

		private void addNewBindButton_Click(object sender, EventArgs e)
		{
			if (radioButton1.Checked) // its a . command
			{
				richTextBox1.Text += $"{UnityKeyCodes.Keycodes.Where(x => x.Key == comboBox1.Text).FirstOrDefault().Value}:.{commandTextBox.Text}\n";
			}
			else if (radioButton2.Checked) // it is an admin command
			{
				richTextBox1.Text += $"{UnityKeyCodes.Keycodes.Where(x => x.Key == comboBox1.Text).FirstOrDefault().Value}:/{commandTextBox.Text}\n";
			}
			else if (radioButton3.Checked) // it is a game console command
			{
				richTextBox1.Text += $"{UnityKeyCodes.Keycodes.Where(x => x.Key == comboBox1.Text).FirstOrDefault().Value}:{commandTextBox.Text}\n";
			}
		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("https://discord.gg/kognity");
		}

		private void pictureBox2_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("https://discord.gg/kognity");
		}

		private void saveButton_Click(object sender, EventArgs e)
		{
			using (StreamWriter outputFile = new StreamWriter(path))
			{
				outputFile.Write(richTextBox1.Text);
				MessageBox.Show("Saved your binds!", "All good!", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

        private void importButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.InitialDirectory = import_folder_path;
                dialog.Filter = "Bind preset files (*.kog)|*.kog|All files (*.*)|*.*";
                dialog.FilterIndex = 2;
                dialog.RestoreDirectory = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var path = dialog.FileName;
                    var file = dialog.OpenFile();

                    using (StreamReader reader = new StreamReader(path))
                    {
                        richTextBox1.Text = reader.ReadToEnd().Replace(Environment.NewLine, "\n");
                        MessageBox.Show("Remember to press the Save button!", "Opened preset!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }


        private void exportButton_Click(object sender, EventArgs e)
        {
            /*
             using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.SelectedPath = import_folder_path;
                
                if (dialog.ShowDialog() == DialogResult.OK) {
                    var result = AskInput("What is the name of this bind?", "Kognity's Bind Manager");

                    var path = Path.Combine(import_folder_path, $"{result}.txt");

                    var file = File.Create(path);

                    using (StreamWriter writer = new StreamWriter(file))
                    {
                        writer.Write(richTextBox1.Text);
                        MessageBox.Show($"The file was successfully exported as {result}!", "Exported file!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            
            */

            // Readme!
            //
            // Use code above instead if want it to be possible to choose folder (delete code below if enable code above) | Tested, works perfectly but seems like an extra unnecessary struggle for the user..
            //
            
            var result = AskInput("What do you want to name this bind preset?", "Bind Manager");

            if (result == "")
            {
                MessageBox.Show("You must specify a preset name!", "Bind Manager Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var path = Path.Combine(import_folder_path, $"{result}.kog");

            var file = File.Create(path);

            using (StreamWriter writer = new StreamWriter(file))
            {
                var newText = richTextBox1.Text;
                writer.Write(newText.Replace("\n", Environment.NewLine));
                MessageBox.Show($"The preset was successfully saved under name '{result}'!", "Saved preset!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
		{
			richTextBox2.Text = "";
			foreach(var line in richTextBox1.Text.Split('\n'))
			{
				try
				{
					richTextBox2.Text += line.Replace(line.Split(':')[0], UnityKeyCodes.Keycodes.Where(x => x.Value.ToString() == line.Split(':')[0]).FirstOrDefault().Key) + Environment.NewLine;
				}
				catch
				{
                    return;
				}
			}
		}

        private void openButton_Click(object sender, EventArgs e)
        {
            Process.Start(@import_folder_path);
        }

		private void helpbutton_Click(object sender, EventArgs e)
		{
			MessageBox.Show("There are 3 major types of commands in SCP:SL\n\nFirstly are Remote Admin Commands. They are used either in the text based RA or typed in the game console with a / prefix (\"/tut Kognity\" as an example).\n\nSecondly are Client Commands. They are typed exlusively in the game console and are prefixed with a . (\".ability1\" as an example)\n\nLastly are Game Console Commands. They are locally run commands and are not sent to the server and have no prefix (\"ar\", \"rc\", \"connect SOMEIPADDRESS\" are some notable examples)", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
	}
}
