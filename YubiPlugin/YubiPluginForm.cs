using System;
using System.Windows.Forms;

namespace YubiPlugin
{
    public partial class YubiPluginForm : Form
    {
        string _password;

        public YubiPluginForm(string masterpassword)
        {
            _password = masterpassword;
            Icon = Properties.Resources.icon_yubico;

            InitializeComponent();
            hidePassword();
        }

        private void hidePassword()
        {
            richTextBoxMasterPassword.Clear();
            richTextBoxMasterPassword.Lines = new[]
            {
                "●●●●●●●● ●●●●●●●● ●●●●●●●● ●●●●●●●●",
                "●●●●●●●● ●●●●●●●● ●●●●●●●● ●●●●●●●●",
            };
        }

        private void showPassword()
        {
            richTextBoxMasterPassword.Clear();
            richTextBoxMasterPassword.Lines = new[]
            {
                _password.Substring( 0, 8) + ' ' + _password.Substring( 8, 8) + ' ' + _password.Substring(16, 8) + ' ' + _password.Substring(24, 8),
                _password.Substring(32, 8) + ' ' + _password.Substring(40, 8) + ' ' + _password.Substring(48, 8) + ' ' + _password.Substring(56, 8),
            };
        }

        private void buttonShowHide_Click(object sender, EventArgs e)
        {
            if (buttonShowHide.Text == "Show recovery phrase")
            {
                showPassword();
                buttonShowHide.Text = "Hide recovery phrase";
            }
            else
            {
                hidePassword();
                buttonShowHide.Text = "Show recovery phrase";
            }
        }

        private void buttonClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(_password);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
