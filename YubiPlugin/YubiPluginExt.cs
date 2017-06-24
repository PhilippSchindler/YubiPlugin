using System;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using KeePass;
using KeePass.Plugins;
using KeePass.Forms;
using KeePass.UI;
using System.Security.Cryptography;
using System.Drawing;

namespace YubiPlugin
{
    public class YubiPluginExt : Plugin
    {
        private KeyPromptForm _keyPromtForm;
        private Button _keyPromtForm_okButton;
        private CheckBox _keyPromtForm_usePw;
        private SecureEdit _keyPromtForm_secureEdit;

        private KeyCreationForm _createKeyForm;
        private Button _createKeyForm_okButton;
        private CheckBox _createKeyForm_usePw;
        private PwInputControlGroup _createKeyForm_pwInputGroup;

        private bool _restoreUIFlags = false;
        private ulong _backupUIFlags;


        public override bool Initialize(IPluginHost host)
        {
            GlobalWindowManager.WindowAdded += GlobalWindowManager_WindowAdded;
            return base.Initialize(host);
        }


        private void GlobalWindowManager_WindowAdded(object sender, GwmWindowEventArgs e)
        {
            // get references for password fields on unlocking and creating a database
            // if a yubikey is inserted, the password field is overwriten with a value 
            // derived from the password and the yubikey-challenge-response

            if (e.Form is KeyPromptForm)
            {
                _keyPromtForm = (KeyPromptForm)e.Form;
                _keyPromtForm_okButton = (Button)_keyPromtForm.Controls.Find("m_btnOK", false)[0];
                _keyPromtForm_usePw = (CheckBox)_keyPromtForm.Controls.Find("m_cbPassword", false)[0];
                _keyPromtForm_secureEdit = (SecureEdit)typeof(KeyPromptForm)
                    .GetField("m_secPassword", BindingFlags.NonPublic | BindingFlags.Instance)
                    .GetValue(_keyPromtForm);

                injectHandler(_keyPromtForm_okButton, keyPromtForm_onConfirmClick);
            }

            else if (e.Form is KeyCreationForm)
            {
                _createKeyForm = (KeyCreationForm)e.Form;
                _createKeyForm_okButton = (Button)_createKeyForm.Controls.Find("m_btnCreate", false)[0];
                _createKeyForm_usePw = (CheckBox)_createKeyForm.Controls.Find("m_cbPassword", false)[0];
                _createKeyForm_pwInputGroup = (PwInputControlGroup)typeof(KeyCreationForm)
                    .GetField("m_icgPassword", BindingFlags.NonPublic | BindingFlags.Instance)
                    .GetValue(_createKeyForm);

                _createKeyForm.VisibleChanged += _createKeyForm_VisibleChanged;

                injectHandler(_createKeyForm_okButton, keyCreateForm_onConfirmClick);
            }
        }


        private void injectHandler(Button button, EventHandler handler)
        {
            EventHandlerList events = (EventHandlerList)typeof(Component).GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Instance)
                                     .GetValue(button, null);

            var clickEventKey = typeof(Control).GetField("EventClick", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);
            var originalHandlers = events[clickEventKey].GetInvocationList();
            foreach (EventHandler h in originalHandlers)
                button.Click -= handler;

            button.Click += (sender, e) =>
            {
                handler(sender, e);
                foreach (EventHandler h in originalHandlers)
                    h(sender, e);
            };
        }


        void _createKeyForm_VisibleChanged(object sender, EventArgs e)
        {
            if (!_createKeyForm.Visible && _restoreUIFlags)
            {
                KeePass.Program.Config.UI.UIFlags = _backupUIFlags;
                _restoreUIFlags = false;
            }
        }


        private void keyPromtForm_onConfirmClick(object sender, EventArgs e)
        {
            if (!_keyPromtForm_usePw.Checked)
                return;

            // use the password from the input field as challange for the Yubikey
            string challenge = Encoding.UTF8.GetString(_keyPromtForm_secureEdit.ToUtf8());
            string response;

            if (yubiChallengeResponse(challenge, out response))
            {
                string password = deriveMasterPassword(challenge, response);
                _keyPromtForm_secureEdit.SetPassword(Encoding.UTF8.GetBytes(password));
            }            
        }


        private void keyCreateForm_onConfirmClick(object sender, EventArgs e)
        {
            if (!_createKeyForm_usePw.Checked)
                return;

            if (!_createKeyForm_pwInputGroup.ValidateData(false))
                return;

            // password should be used, passwords are equal and 
            // yubikey is connected at this point in the code
            string challenge = Encoding.UTF8.GetString(_createKeyForm_pwInputGroup.GetPasswordUtf8());
            string response;

            if (yubiChallengeResponse(challenge, out response))
            {
                // workaround for harmless bug
                _backupUIFlags = KeePass.Program.Config.UI.UIFlags;
                Program.Config.UI.UIFlags &= ~(ulong)KeePass.App.Configuration.AceUIFlags.HidePwQuality;
                _restoreUIFlags = true;

                string password = deriveMasterPassword(challenge, response);
                _createKeyForm_pwInputGroup.SetPassword(Encoding.UTF8.GetBytes(password), true);

                new YubiPluginForm(password).ShowDialog();
            }
        }


        static bool yubiChallengeResponse(string challenge, out string response)
        {
            response = null;

            const int SLOT = 2; 
            string args = string.Format("-{0} \"{1}\"", SLOT, challenge);

            string path = null;
            if (System.IO.File.Exists("yubico\\ykchalresp.exe"))
                path = "yubico\\ykchalresp.exe";
            else if (System.IO.File.Exists("Plugins\\yubico\\ykchalresp.exe"))
                path = "Plugins\\yubico\\ykchalresp.exe";
            else
            {
                MessageBox.Show("YubiPlugin installation failed, binaries from Yubico are missing.\n" + 
                    "Please see documentation at: https://github.com/PhilippSchindler/YubiPlugin.",
                    "YubiPlugin Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.FileName = path;
            p.StartInfo.Arguments = args;

            p.Start();
            string stdout = p.StandardOutput.ReadToEnd().Trim();
            string stderr = p.StandardError.ReadToEnd().Trim();
            p.WaitForExit();

            if (stderr == "")
            {
                response = stdout;
                return true;
            }
            return false;
        }
        

        static string deriveMasterPassword(string password, string response)
        {
            // hex(SHA256(password || HMAC-SHA1@yubikey(password)))
            return BitConverter.ToString(
                        SHA256.Create().ComputeHash(
                            Encoding.UTF8.GetBytes(password + response)
                        )
                    ).Replace("-", string.Empty).ToLower();
        }


        public override Image SmallIcon {
            get { return Properties.Resources.icon_yubico.ToBitmap(); }
        }
        

        public override string UpdateUrl {
            get { return "https://github.com/PhilippSchindler/YubiPlugin/version_info.txt"; }
        }
    }
}
