using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI;
using SpecialServices;
using WebServiceStudioExpress.Properties;

namespace WebServiceStudioExpress
{
    public partial class frmMain : Form
    {
        ToolStripProfessionalRenderer _defaultRenderer;
        //Riferimento all'oggetto che tiene traccia dell'elenco dei file recenti.
        private RecentFiles _recentFiles;

        public frmMain(string[] args)
        {
            InitializeComponent();

            //Apply a gray professional renderer as a default renderer
            _defaultRenderer = new ToolStripProfessionalRenderer(new PropertyGridEx.CustomColorScheme());
            ToolStripManager.Renderer = _defaultRenderer;
            _defaultRenderer.RoundedEdges = false;

            dockPanel.ActiveAutoHideContent = null;
            dockPanel.Parent = this;
            VS2005Style.Extender.SetSchema(dockPanel, VS2005Style.Extender.Schema.FromBase);

            //Inizializza la descrizione dei menu.
            this.InitMenuDescription();
            //Inizializza l'elenco dei file recenti.
            _recentFiles = new RecentFiles(mnuFile, mnuFile.DropDownItems.Count - 1,
                                    Settings.Default.RecentFilesNumber,
                                    Settings.Default.ShowRecentFiles,
                                    RecentFile_Click);

            //if (args.Length > 0)
            //{
            //    //Sono stati specificati file da riga di comando: li apre.
            //    foreach (string file in args)
            //        this.OpenFile(file);
            //}
            //else
            //{
            //    //Non sono stati specificati file da riga di comando: crea il primo documento vuoto.
            //    frmDocument frm = new frmDocument(tabDocumenti);
            //    this.AddNewPage(frm);
            //}

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmWebService s = new frmWebService(this);
            s.TabText = "Test";
            s.Text = s.TabText;
            s.Show(dockPanel);
        }

        #region Status Bar Message Methods

        private void InitMenuDescription()
        {
            foreach (ToolStripMenuItem menu in mnuMenu.Items)
            {
                menu.MouseEnter += new EventHandler(menu_MouseEnter);
                menu.MouseLeave += new EventHandler(menu_MouseLeave);
                foreach (ToolStripItem Item in menu.DropDownItems)
                {
                    Item.MouseEnter += new EventHandler(menu_MouseEnter);
                    Item.MouseLeave += new EventHandler(menu_MouseLeave);
                    if (Item is ToolStripMenuItem)
                        this.AddStatusBarMenuMessage((ToolStripMenuItem)Item);
                }
            }
        }

        private void AddStatusBarMenuMessage(ToolStripMenuItem menu)
        {
            foreach (ToolStripItem subItem in menu.DropDownItems)
            {
                subItem.MouseEnter += new EventHandler(menu_MouseEnter);
                subItem.MouseLeave += new EventHandler(menu_MouseLeave);
                if (subItem is ToolStripMenuItem)
                    this.AddStatusBarMenuMessage((ToolStripMenuItem)subItem);
            }
        }

        private void menu_MouseEnter(object sender, EventArgs e)
        {
            ToolStripItem menu = ((ToolStripItem)sender);
            if (menu.Tag != null)
            {
                string message = menu.Tag.ToString();
                if (!message.EndsWith("."))
                    message += ".";
                this.stlMessaggio.Text = message;
            }
            else
            {
                this.stlMessaggio.Text = string.Empty;
            }
        }

        private void menu_MouseLeave(object sender, EventArgs e)
        {
            this.stlMessaggio.Text = string.Empty;
        }

        #endregion

        private void RecentFile_Click(RecentFiles sender, string FileName)
        {
            //Apre il file recente selezionato.
            //this.OpenFile(FileName);
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Salva il file di configurazione dell'applicazione.
            Settings.Default.Save();
        }

        public void SetStatusBarMessage(string Message)
        {
            stlMessaggio.Text = Message;
            stsBarra.Refresh();
        }
    }
}