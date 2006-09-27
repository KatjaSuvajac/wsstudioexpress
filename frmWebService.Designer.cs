namespace WebServiceStudioExpress
{
    partial class frmWebService
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWebService));
            this.lblUrl = new System.Windows.Forms.Label();
            this.cmbUrl = new System.Windows.Forms.ComboBox();
            this.btnVai = new System.Windows.Forms.Button();
            this.tbcSchede = new System.Windows.Forms.TabControl();
            this.tbpOrigineWSDL = new System.Windows.Forms.TabPage();
            this.Browser = new System.Windows.Forms.WebBrowser();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.tbcSchede.SuspendLayout();
            this.tbpOrigineWSDL.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblUrl
            // 
            this.lblUrl.AutoSize = true;
            this.lblUrl.Location = new System.Drawing.Point(2, 9);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(32, 13);
            this.lblUrl.TabIndex = 0;
            this.lblUrl.Text = "URL:";
            // 
            // cmbUrl
            // 
            this.cmbUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbUrl.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbUrl.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cmbUrl.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbUrl.FormattingEnabled = true;
            this.cmbUrl.Location = new System.Drawing.Point(40, 6);
            this.cmbUrl.Name = "cmbUrl";
            this.cmbUrl.Size = new System.Drawing.Size(352, 21);
            this.cmbUrl.TabIndex = 1;
            this.cmbUrl.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbUrl_KeyPress);
            this.cmbUrl.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cmbUrl_KeyUp);
            // 
            // btnVai
            // 
            this.btnVai.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVai.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnVai.Image = ((System.Drawing.Image)(resources.GetObject("btnVai.Image")));
            this.btnVai.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVai.Location = new System.Drawing.Point(398, 6);
            this.btnVai.Name = "btnVai";
            this.btnVai.Size = new System.Drawing.Size(54, 21);
            this.btnVai.TabIndex = 2;
            this.btnVai.Text = "Vai";
            this.btnVai.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnVai.UseVisualStyleBackColor = true;
            this.btnVai.Click += new System.EventHandler(this.btnVai_Click);
            // 
            // tbcSchede
            // 
            this.tbcSchede.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbcSchede.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tbcSchede.Controls.Add(this.tbpOrigineWSDL);
            this.tbcSchede.Controls.Add(this.tabPage2);
            this.tbcSchede.Controls.Add(this.tabPage3);
            this.tbcSchede.HotTrack = true;
            this.tbcSchede.ImageList = this.imageList;
            this.tbcSchede.Location = new System.Drawing.Point(5, 33);
            this.tbcSchede.Multiline = true;
            this.tbcSchede.Name = "tbcSchede";
            this.tbcSchede.SelectedIndex = 0;
            this.tbcSchede.Size = new System.Drawing.Size(447, 398);
            this.tbcSchede.TabIndex = 3;
            // 
            // tbpOrigineWSDL
            // 
            this.tbpOrigineWSDL.Controls.Add(this.Browser);
            this.tbpOrigineWSDL.ImageKey = "WebService.ico";
            this.tbpOrigineWSDL.Location = new System.Drawing.Point(4, 26);
            this.tbpOrigineWSDL.Name = "tbpOrigineWSDL";
            this.tbpOrigineWSDL.Padding = new System.Windows.Forms.Padding(3);
            this.tbpOrigineWSDL.Size = new System.Drawing.Size(439, 368);
            this.tbpOrigineWSDL.TabIndex = 0;
            this.tbpOrigineWSDL.Text = "Origine WSDL";
            this.tbpOrigineWSDL.UseVisualStyleBackColor = true;
            // 
            // Browser
            // 
            this.Browser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Browser.IsWebBrowserContextMenuEnabled = false;
            this.Browser.Location = new System.Drawing.Point(3, 3);
            this.Browser.MinimumSize = new System.Drawing.Size(20, 20);
            this.Browser.Name = "Browser";
            this.Browser.Size = new System.Drawing.Size(433, 362);
            this.Browser.TabIndex = 0;
            this.Browser.WebBrowserShortcutsEnabled = false;
            this.Browser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.Browser_DocumentCompleted);
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(439, 368);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 26);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(439, 368);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "WebService.ico");
            // 
            // frmWebService
            // 
            this.ClientSize = new System.Drawing.Size(456, 433);
            this.Controls.Add(this.tbcSchede);
            this.Controls.Add(this.btnVai);
            this.Controls.Add(this.cmbUrl);
            this.Controls.Add(this.lblUrl);
            this.Name = "frmWebService";
            this.ShowHint = WeifenLuo.WinFormsUI.DockState.Document;
            this.TabText = "Test";
            this.Text = "Test";
            this.Resize += new System.EventHandler(this.frmWebService_Resize);
            this.tbcSchede.ResumeLayout(false);
            this.tbpOrigineWSDL.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUrl;
        private System.Windows.Forms.ComboBox cmbUrl;
        private System.Windows.Forms.Button btnVai;
        private System.Windows.Forms.TabControl tbcSchede;
        private System.Windows.Forms.TabPage tbpOrigineWSDL;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.WebBrowser Browser;
    }
}
