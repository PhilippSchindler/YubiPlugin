namespace YubiPlugin
{
    partial class YubiPluginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(YubiPluginForm));
            this.buttonShowHide = new System.Windows.Forms.Button();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.buttonClipboard = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.buttonOK = new System.Windows.Forms.Button();
            this.richTextBoxMasterPassword = new System.Windows.Forms.RichTextBox();
            this.labelInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonShowHide
            // 
            this.buttonShowHide.Location = new System.Drawing.Point(12, 187);
            this.buttonShowHide.Name = "buttonShowHide";
            this.buttonShowHide.Size = new System.Drawing.Size(131, 23);
            this.buttonShowHide.TabIndex = 0;
            this.buttonShowHide.Text = "Show recovery phrase";
            this.buttonShowHide.UseVisualStyleBackColor = true;
            this.buttonShowHide.Click += new System.EventHandler(this.buttonShowHide_Click);
            // 
            // buttonClipboard
            // 
            this.buttonClipboard.Location = new System.Drawing.Point(149, 187);
            this.buttonClipboard.Name = "buttonClipboard";
            this.buttonClipboard.Size = new System.Drawing.Size(131, 23);
            this.buttonClipboard.TabIndex = 1;
            this.buttonClipboard.Text = "Copy to Clipboard";
            this.buttonClipboard.UseVisualStyleBackColor = true;
            this.buttonClipboard.Click += new System.EventHandler(this.buttonClipboard_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(434, 187);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // richTextBoxMasterPassword
            // 
            this.richTextBoxMasterPassword.BackColor = System.Drawing.Color.White;
            this.richTextBoxMasterPassword.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxMasterPassword.Location = new System.Drawing.Point(12, 138);
            this.richTextBoxMasterPassword.Name = "richTextBoxMasterPassword";
            this.richTextBoxMasterPassword.ReadOnly = true;
            this.richTextBoxMasterPassword.Size = new System.Drawing.Size(497, 43);
            this.richTextBoxMasterPassword.TabIndex = 3;
            this.richTextBoxMasterPassword.Text = "e3b0c442 98fc1c14 9afbf4c8 996fb924\n27ae41e4 649b934c a495991b 7852b855";
            // 
            // labelInfo
            // 
            this.labelInfo.Location = new System.Drawing.Point(9, 9);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(499, 123);
            this.labelInfo.TabIndex = 4;
            this.labelInfo.Text = @"YubiPlugin is used to secure this database.
To unlock your database plugin in your Yubikey and unlock Keypass as usual.

YubiPlugin secures your database in the background. 
If you try to unlock lock database without your Yubikey you simply get a 'invalid password' error.


In case you ever loose your Yubikey, you can unlock your database with the master password* below.
*Do not use any spaces or newlines in this case.";
            // 
            // YubiPluginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 222);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.richTextBoxMasterPassword);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonClipboard);
            this.Controls.Add(this.buttonShowHide);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "YubiPluginForm";
            this.ShowInTaskbar = false;
            this.Text = "YubiPlugin - The easy 2nd factor solution for Keepass";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonShowHide;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Button buttonClipboard;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.RichTextBox richTextBoxMasterPassword;
        private System.Windows.Forms.Label labelInfo;
    }
}