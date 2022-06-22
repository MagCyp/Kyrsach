using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kyrsach2
{
    public partial class Form : System.Windows.Forms.Form
    {
        private const string adminLogin = "admin";
        private const string adminPassword = "admin";
        public Form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.ToString().Equals(adminLogin) && textBox2.Text.ToString().Equals(adminPassword))
            {
                Form1 form1 = new Form1();
                this.Hide();
                form1.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Not correct");
                textBox1.Text = null;
                textBox2.Text = null;
            }
        }
    }
}
