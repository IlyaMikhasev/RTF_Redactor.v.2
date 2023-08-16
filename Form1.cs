using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RTF_Redactor.v._2
{
    public partial class Form1 : Form
    {
        WR_File file;
        Notes notes;
        bool timeDay = true;
        private List<WR_File> _wR_Files = new List<WR_File>();
        private List<Notes> _saved_offers = new List<Notes>();
        darkForms darkForms;
        public Form1()
        {
             InitializeComponent();
            WR_File.Deserealize_it("richtext.xml", out _wR_Files);
            updateHistory();
            deserialize_notes();
            ChangeTheme();
        }
        private void deserialize_notes() {
            Notes.Deserealize_it("notes.xml", out _saved_offers);
            int i = 0;
            if (_saved_offers.Count > 0)
            {
                foreach (object component in templatesToolStripMenuItem.DropDownItems)
                {
                    ((ToolStripTextBox)component).Text = _saved_offers[i]._note;
                    i++;
                }
            }
        }
        private void updateHistory() {
            toolStripComboBox1.Items.Clear();
            if (_wR_Files.Count > 10) {
                _wR_Files.RemoveAt(0);
            }
            foreach (var item in _wR_Files)
            {
                int indexWord = item._path.LastIndexOf('\\');
                if (indexWord > 0)
                {
                    toolStripComboBox1.Items.Add(item._path.Substring(indexWord));
                }
            }
        }
        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                Add_in_Text(sender, e);
            }
        }
        private void Add_in_Text(object sender , EventArgs e) {
            richTextBox1.Text += ((ToolStripTextBox)sender).Text;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            file = new WR_File();
            file._path = file.SaveFile(richTextBox1);
            _wR_Files.Add(file);
            updateHistory() ;
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            file = new WR_File();
            string path = file.OpenFile();
            if (path != "none")
            {
                richTextBox1.LoadFile(path);
            }
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            WR_File.Serealize_it("richtext.xml", _wR_Files);
            _saved_offers.Clear();
            foreach (object component in templatesToolStripMenuItem.DropDownItems) {
                    notes = new Notes(((ToolStripTextBox)component).Text);
                    _saved_offers.Add(notes);                           
            }
            Notes.Serealize_it("notes.xml", _saved_offers);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowColor = true;
            if (fontDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            richTextBox1.SelectionFont = fontDialog1.Font;
            richTextBox1.SelectionColor = fontDialog1.Color;
        }

        private void colorBackgraundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.FullOpen = true;
            colorDialog1.Color = this.BackColor;
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            richTextBox1.BackColor = colorDialog1.Color;
        }

        private void colorSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.FullOpen = true;
            colorDialog1.Color = this.BackColor;
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            richTextBox1.SelectionBackColor = colorDialog1.Color;
        }
        private void ChangeTheme() {
            if (timeDay)
            {
                ligthTheme();
            }
            else
            {
                darkTheme();
            }
            
        }
        private void darkTheme() {
            this.BackColor = SystemColors.ControlDarkDark;
            menuStrip1.BackColor = SystemColors.ControlDarkDark;
            richTextBox1.BackColor = SystemColors.ControlDarkDark;
            this.ForeColor = SystemColors.ControlLight;
            menuStrip1.ForeColor = SystemColors.ControlLight;
            richTextBox1.ForeColor = SystemColors.ControlLight;
            foreach (ToolStripMenuItem item in fileToolStripMenuItem.DropDownItems)
            {
                item.BackColor = SystemColors.ControlDarkDark;
                item.ForeColor = SystemColors.ControlLight;
            }
            foreach (ToolStripMenuItem item in visualSettingToolStripMenuItem.DropDownItems)
            {
                item.BackColor = SystemColors.ControlDarkDark;
                item.ForeColor = SystemColors.ControlLight;
            }
            foreach (ToolStripTextBox item in templatesToolStripMenuItem.DropDownItems)
            {
                item.BackColor = SystemColors.ControlDarkDark;
                item.ForeColor = SystemColors.ControlLight;
            }

        }
        private void ligthTheme() {
            this.BackColor = SystemColors.ControlLight;
            menuStrip1.BackColor = SystemColors.ControlLight;
            richTextBox1.BackColor = SystemColors.ControlLight;
            this.ForeColor = SystemColors.ControlText;
            menuStrip1.ForeColor = SystemColors.ControlText;
            richTextBox1.ForeColor = SystemColors.ControlText;
            foreach (ToolStripMenuItem item in fileToolStripMenuItem.DropDownItems)
            {
                item.BackColor = SystemColors.ControlLight;
                item.ForeColor = SystemColors.ControlDark;
            }
            foreach (ToolStripMenuItem item in visualSettingToolStripMenuItem.DropDownItems)
            {
                item.BackColor = SystemColors.ControlLight;
                item.ForeColor = SystemColors.ControlDark;
            }
            foreach (ToolStripTextBox item in templatesToolStripMenuItem.DropDownItems)
            {
                item.BackColor = SystemColors.ControlLight;
                item.ForeColor = SystemColors.ControlDark;
            }
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            file = new WR_File();
            foreach (var item in _wR_Files)
            {
                if (item._path.Contains(((ToolStripComboBox)sender).SelectedItem.ToString())){
                    try
                    {
                        richTextBox1.LoadFile(item._path);
                    }
                    catch (Exception exep)
                    {
                        MessageBox.Show(exep.Message);
                    }
                }
            }
            
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.IsMdiContainer = true;
            darkForms = new darkForms();
            if (darkForms.ShowDialog() == DialogResult.OK)
            {
                timeDay = darkForms._exelen_correspond;
                ChangeTheme();
            }            
        }
    }
}
