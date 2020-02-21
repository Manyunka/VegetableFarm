using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VegetableFarm
{
	public partial class Form1 : Form
	{
        Game game;
        TimeSpan gameLimit;

        public Form1()
        {
            InitializeComponent();

            game = new Game();
            game.SetField(panel1);

            gameLimit = new TimeSpan(3, 0, 0);

            timer1.Start();
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (sender as CheckBox);

            game.ChangeCell(cb);
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            foreach (CheckBox cb in panel1.Controls)
                game.NextStep(cb);

            moneyLabel.Text = "Money: " + game.Money + "$";

            timeLabel.Text = "Time: " + game.Time.Duration();

            if (game.IsGameOver() || game.Time == gameLimit)
            {
                timer1.Stop();
                MessageBox.Show("Game Over\nResult: " + game.Money);

                game.NewGame();
                ChangeTick();
                timer1.Start();
            }
        }

        private void UpSpeedButton_Click(object sender, EventArgs e)
        {
            game.UpSpeed();

            ChangeTick();
            
        }

        private void DownSpeedButton_Click(object sender, EventArgs e)
        {
            game.DownSpeed();

            ChangeTick();
        }

        private void ChangeTick()
        {
            speedLabel.Text = "Speed: x" + game.Speed.ToString("0.00");

            timer1.Interval = (int)(100 / game.Speed);
        }
    }
}
