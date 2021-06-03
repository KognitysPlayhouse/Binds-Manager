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

namespace BindsManager.Classes
{
    public static class UIHelper # Switched to 'UIHelper' for future use with better UI
    {
        public static string AskInput(string title, string description) {
            Form inputBox = new Form()
            {
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = description,
                StartPosition = FormStartPosition.CenterScreen
            };

            Label text = new Label()
            {
                Font = new Font("Open Sans", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                AutoSize = true,
                Location = new Point(12, 22),
                Text = title
            };

            TextBox input = new TextBox()
            {
                Font = new Font("Open Sans", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                Location = new Point(15, 45),
                Size = new Size(440, 20)
            };

            Button ok = new Button()
            {
                Font = new Font("Open Sans", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                Text = "Ok",
                Location = new Point(380, 76),
                Size = new Size(75, 23),
                DialogResult = DialogResult.OK,
            };

            
            ok.Click += (sender, e) => { inputBox.Close();  }; // There's no need to make a method for just 1 line.

            inputBox.Controls.Add(text);
            inputBox.Controls.Add(input);
            inputBox.Controls.Add(ok);

            inputBox.AcceptButton = ok;

            return inputBox.ShowDialog() == DialogResult.OK ? input.Text : "";
        }
    }
}
