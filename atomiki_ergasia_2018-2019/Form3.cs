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
    public partial class Form3 : Form
    {
        Random r;
        String username, newL, oldL;
        List<String> lines;
        String[] columns;
        int level, highScore, topScore, x, y, time = 30, score = 0;
        bool newHighScore;

        public Form3(String username,int level, int highScore, int topScore)
        {
            InitializeComponent();
            this.username = username;
            this.level = level;
            this.highScore = highScore;
            this.topScore = topScore;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            r = new Random();
            newHighScore = false;
            label1.Text = "Username: " + username;
            label2.Text = "Level: " + level;
            label3.Text = "High Score: " + highScore;
            label4.Text = "Time: " + time--;
            label6.Text = "Top Score: " + topScore;
            if (level > 2) pictureBox1.Image = Image.FromFile("gifs\\banana2.gif");
            timer1.Interval = 2000 - (level - 1) * 500;
            timer1.Start();
            timer2.Start();
        }

        //Checks if the user is already in the file
        private bool userIsSigned() {
            foreach (String line in lines)
            {
                columns = line.Split(' ');
                if (columns[0] == username) return true;
            }
            return false;
        }

        //Updates the "highScores.txt" file
        private void updateHighScores() {
            lines = System.IO.File.ReadAllLines("highScores.txt").ToList<String>();
            newL = "";
            if (userIsSigned())
            {
                foreach (String l in lines)
                {
                    columns = l.Split(' ');
                    if (columns[0] == username)
                    {
                        newL += username + " ";
                        for (int i = 1; i <= 4; i++) newL += ((i == level) ? score.ToString() : columns[i]) + ((i != 4) ? " " : "\n");
                        oldL = l;
                    }
                }
                lines.Remove(oldL);
            }
            else {
                newL += username + " ";
                for (int i = 1; i <= 4; i++) newL += ((i == level) ? score.ToString() : "0") + ((i != 4) ? " " : "\n");
            }

            lines.Add(newL);
            System.IO.File.WriteAllLines("highScores.txt", lines);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            label4.Text = "Time: " + time--;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            score += 10;
            label5.Text = "Score: " + score;
            if (score > highScore) {
                newHighScore = true;
                highScore = score;
                label3.Text = "High Score: " + highScore;
                if (score > topScore) {
                    topScore = score;
                    label6.Text = "Top Score: " + topScore;
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (time > 0)
            {
                x = r.Next(panel1.Width - pictureBox1.Width);
                y = r.Next(panel1.Height - pictureBox1.Height);
                pictureBox1.Location = new Point(x, y);
            }
            else {
                timer1.Stop();
                timer2.Stop();
                label4.Text = "Time: 0";
                if (newHighScore) updateHighScores();
                MessageBox.Show("Game Over!");
                this.Hide();
                Form2 f = new Form2(username, level);
                f.ShowDialog();
                this.Close();
            }
        }
    }
}
