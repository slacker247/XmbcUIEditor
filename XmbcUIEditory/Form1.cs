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
using System.Xml;

namespace XmbcUIEditory
{
    public partial class Form1 : Form
    {
        Skin m_Skin = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if(dlg.ShowDialog(this) == DialogResult.OK)
            {
                FileInfo file = new FileInfo(dlg.FileName);
                m_Skin = new Skin(file.FullName);

                tmr_Update.Enabled = true;
            }
        }

        private void tmr_Update_Tick(object sender, EventArgs e)
        {
            tmr_Update.Enabled = false;
            if(pbx_View.Image != null)
                pbx_View.Image.Dispose();
            pbx_View.Image = m_Skin.render();
            tmr_Update.Enabled = true;
        }
    }
}
