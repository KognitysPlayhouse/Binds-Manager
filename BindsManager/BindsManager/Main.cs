using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BindsManager
{
	public partial class Main : Form
	{
		public string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SCP Secret Laboratory", "cmdbinding.txt");
		public Main()
		{
			InitializeComponent();
		}

		private void Main_Load(object sender, EventArgs e)
		{
			richTextBox1.Text = File.ReadAllText(path);

			richTextBox1.SelectionStart = richTextBox1.Text.Length;
			richTextBox1.Focus();

			foreach (var key in UnityKeyCodes.Keycodes.Keys)
			{
				comboBox1.Items.Add(key);
			}
		}

		private void clearButton_Click(object sender, EventArgs e)
		{
			richTextBox1.Text = "";
		}

		private void autoFillButton_Click(object sender, EventArgs e)
		{
			richTextBox1.Text = richTextBox1.Text + "49:.ability1\n50:.ability2\n51:.ability3\n52:.ability4\n102:.zfe\n";
		}

		private void addNewBindButton_Click(object sender, EventArgs e)
		{
			richTextBox1.Text += $"{UnityKeyCodes.Keycodes.Where(x => x.Key == comboBox1.Text).FirstOrDefault().Value}:.{commandTextBox.Text}\n";
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
				MessageBox.Show("Saved!", "All good!", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}
	}
}
