using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
			ToolTip keyToolTip = new ToolTip();
			keyToolTip.ShowAlways = true;
			keyToolTip.SetToolTip(comboBox1, "Select a Key to bind the command to");

			ToolTip commandTip = new ToolTip();
			commandTip.ShowAlways = true;
			commandTip.SetToolTip(commandTextBox, "Type the command you want to bind. Eg kill");

			ToolTip isAdminTip = new ToolTip();
			isAdminTip.ShowAlways = true;
			isAdminTip.SetToolTip(checkBox1, "If the command is an Admin command check this box");

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
			switch (checkBox1.Checked)
			{
				case true:
					richTextBox1.Text += $"{UnityKeyCodes.Keycodes.Where(x => x.Key == comboBox1.Text).FirstOrDefault().Value}:/{commandTextBox.Text}\n";
					break;
				case false:
					richTextBox1.Text += $"{UnityKeyCodes.Keycodes.Where(x => x.Key == comboBox1.Text).FirstOrDefault().Value}:.{commandTextBox.Text}\n";
					break;
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
				MessageBox.Show("Saved!", "All good!", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
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

				}
			}
		}
	}
}
