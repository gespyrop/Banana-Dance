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
    public partial class Form2 : Form
    {
        String username;
        String[] columns;
        int level, topScore, highScore;
        SortedDictionary<int, List<String>> scores; //Using a list of strings as a value to store the names of users with the same score
        List<String> names;

        public Form2(String username)
        {
            InitializeComponent();
            this.username = username;
        }

        public Form2(String username, int level)
        {
            InitializeComponent();
            this.username = username;
            this.level = level;
            numericUpDown1.Value = level;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            label1.Text = "Welcome " + username;
            updateLevelAndScores();
            label4.Text = "Online Time: " + DateTime.Now.ToString("hh:mm:ss");
            timer1.Start();
        }

        //Updates the values of level,highScore and topScore
        private void updateLevelAndScores() {
            scores = new SortedDictionary<int, List<String>>();
            level = (int)numericUpDown1.Value;
            topScore = 0;
            highScore = 0;
            richTextBox1.Clear();
            foreach (String line in System.IO.File.ReadAllLines("highScores.txt")) {
                try
                {
                    columns = line.Split(' ');
                    if (columns[level] != "0")
                    {
                        if (scores.ContainsKey(int.Parse(columns[level])))
                        {
                            names = scores[int.Parse(columns[level])];
                            scores.Remove(int.Parse(columns[level]));
                        }
                        else {
                            names = new List<String>();
                        }

                        names.Add(columns[0]);
                        scores.Add(int.Parse(columns[level]), names);
                        if (columns[0] == username) highScore = int.Parse(columns[level]);  
                        if (int.Parse(columns[level]) > topScore) topScore = int.Parse(columns[level]);
                    }
                }
                catch (Exception e) { }

            }
            label6.Text = "High Score: " + highScore;
            label3.Text = "Top Score: " + topScore;
            scores.Reverse();
            foreach (int score in scores.Keys.Reverse())
                foreach(String n in scores[score]) 
                    richTextBox1.Text += n + " " + score + "\n";
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label4.Text = "Online Time: " + DateTime.Now.ToString("hh:mm:ss");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 f = new Form3(username, level, highScore, topScore);
            f.ShowDialog();
            this.Close();

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            updateLevelAndScores();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1();
            f.ShowDialog();
            this.Close();
        }
    }
}
