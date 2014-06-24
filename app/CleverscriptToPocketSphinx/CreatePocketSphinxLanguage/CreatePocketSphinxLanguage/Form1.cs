using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CreatePocketSphinxLanguage
{
    public partial class Form1 : Form
    {
      private string pathName = string.Empty;
      private string newPathName = string.Empty;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            string safeFileName = string.Empty;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select CSV";
            ofd.Filter = "CSV File |*.csv";
            
            if(ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                pathName = ofd.FileName;
                safeFileName = ofd.SafeFileName;
                newPathName = this.trimPathName(safeFileName) + "corpus.txt";
            }
            txtOpen.Text = pathName;
        }

        private void txtOpen_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

        }

        private string trimPathName(string safefileName)
        {
            if (pathName != string.Empty)
            {
              return pathName.Trim(safefileName.ToCharArray());
            }
            return string.Empty;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string line;
            CreateCorpus cc = new CreateCorpus(newPathName);
            using(StreamReader sr = new StreamReader(pathName))
            {
            while((line = sr.ReadLine()) != null)
            {
                cc.getInput(line);
            }
            cc.makeCorpus();
            }
        }
    }
}
