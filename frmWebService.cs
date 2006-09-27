using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WebServiceStudioExpress.Properties;
using System.Collections.Specialized;

namespace WebServiceStudioExpress
{
    public partial class frmWebService : WeifenLuo.WinFormsUI.DockContent
    {
        private StringCollection _recentUrls;
        private frmMain _parentForm;

        public frmWebService(frmMain parentForm)
        {
            InitializeComponent();

            _parentForm = parentForm;
            _recentUrls = Settings.Default.RecentUrls;
            if (_recentUrls == null)
                _recentUrls = new StringCollection();

            AutoCompleteStringCollection autoCompleteSource = new AutoCompleteStringCollection();
            foreach (string url in _recentUrls)
                autoCompleteSource.Add(url);
            cmbUrl.AutoCompleteCustomSource = autoCompleteSource;
        }

        private void cmbUrl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                e.Handled = true;
                btnVai.PerformClick();
            }
            
        }

        private void cmbUrl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
                e.Handled = true;
        }

        private void btnVai_Click(object sender, EventArgs e)
        {
            string url = cmbUrl.Text.Trim();
            string message = string.Format("Apertura pagina {0}...", url);
            _parentForm.SetStatusBarMessage(message);

            Browser.Navigate(url);
            Browser.Refresh();

            if (!cmbUrl.Items.Contains(url))
            {
                if (_recentUrls.Count == 32)
                {
                    _recentUrls.RemoveAt(0);
                    cmbUrl.AutoCompleteCustomSource.RemoveAt(0);
                }
                _recentUrls.Add(url);
                cmbUrl.AutoCompleteCustomSource.Add(url);
                cmbUrl.Items.Add(url);
                Settings.Default.RecentUrls = _recentUrls;
            }
        }

        private void frmWebService_Resize(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void Browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            _parentForm.SetStatusBarMessage("Operazione completata.");
        }
    }
}

