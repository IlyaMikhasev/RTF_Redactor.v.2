using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime;


namespace RTF_Redactor.v._2
{
    internal class darkForms : Form
    {

        public bool _exelen_correspond = true;
        private System.Windows.Forms.Label _label_timeFrom;
        private System.Windows.Forms.Label _label_timeTo;
        private System.Windows.Forms.MaskedTextBox _maskedTextFrom;
        private System.Windows.Forms.MaskedTextBox _maskedTextTo;
        private System.Windows.Forms.Button _buttonOk;
        public darkForms() { 
            this.Text = "Setting time theme";
            this.Size = new System.Drawing.Size(180, 210);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            this._label_timeFrom = new System.Windows.Forms.Label();
            this._label_timeFrom.Location = new System.Drawing.Point(5, 5);
            this._label_timeFrom.Text = "Time from";
            this._maskedTextFrom = new System.Windows.Forms.MaskedTextBox();
            this._maskedTextFrom.Location = new System.Drawing.Point(5, 35);
            this._maskedTextFrom.Mask = "00:00";
            this._label_timeTo = new System.Windows.Forms.Label();
            this._label_timeTo.Location = new System.Drawing.Point(5, 65);
            this._label_timeTo.Text = "Time to";
            this._maskedTextTo = new System.Windows.Forms.MaskedTextBox();
            this._maskedTextTo.Location = new System.Drawing.Point(5, 95);
            this._maskedTextTo.Mask = "00:00";
            this._buttonOk = new System.Windows.Forms.Button();
            this._buttonOk.Location = new System.Drawing.Point(15, 125);
            this._buttonOk.Text = "OK";
            this._buttonOk.DialogResult = DialogResult.OK;
            this._buttonOk.Click += button_ok_click;
            this.Controls.Add(this._buttonOk);
            this.Controls.Add(this._label_timeFrom);
            this.Controls.Add(this._label_timeTo);
            this.Controls.Add(this._maskedTextFrom);
            this.Controls.Add(this._maskedTextTo);

        }
        private void button_ok_click(object sender, EventArgs e) {
            var timeFrom =  _maskedTextFrom.Text.Split(':');
            var timeTo = _maskedTextTo.Text.Split(':');
            var timeNow = DateTime.Now.ToString("HH:mm").Split(':');
            if (timeFrom.Length < 2 || timeTo.Length < 2) {
                MessageBox.Show("uncurrent time");
                return;
            }
            try
            {
                int h_from = int.Parse(timeFrom[0]);
                int h_to = int.Parse(timeTo[0]);
                int m_from = int.Parse(timeFrom[1]);
                int m_to = int.Parse(timeTo[1]);
                int h_now = int.Parse(timeNow[0]);
                int m_now = int.Parse(timeNow[1]);
                if (h_from < h_now & m_from < m_now || h_now < h_to & m_now < m_to)
                    _exelen_correspond = true;
                else 
                    _exelen_correspond = false;
                this.Close();
            }
            catch 
            {
                MessageBox.Show("uncurrent time");
            }
            

        }

    }
}
