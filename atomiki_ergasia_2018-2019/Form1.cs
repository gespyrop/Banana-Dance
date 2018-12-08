using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace atomiki_ergasia_2018_2019
{
    public partial class Form1 : Form
    {
        String username;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            username = textBox1.Text.Trim();

            if (username != "" && !username.Contains(' ')) {
                this.Hide();
                Form2 f = new Form2(username);
                f.ShowDialog();
                this.Close();
            }
            else if(username.Contains(' ')) MessageBox.Show("Spaces are not allowed!");
            else MessageBox.Show("Can you please tell me your name?", "No name given!");
        }
    }
}
