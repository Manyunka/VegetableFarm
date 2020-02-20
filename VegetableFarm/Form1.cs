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

        public Form1()
        {
            InitializeComponent();

            game = new Game();
            game.SetField(panel1);

            timer1.Start();
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (sender as CheckBox);
            if (cb.Checked)
                game.Plant(cb);
            else if (game.Field[cb].state == CellState.Overgrow ||
                game.Field[cb].state == CellState.Mature ||
                game.Field[cb].state == CellState.Immature)
                game.Harvest(cb);
            else cb.Checked = true;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            foreach (CheckBox cb in panel1.Controls)
                game.NextStep(cb);

            moneyLabel.Text = "Money: " + game.Money + "$";

            timeLabel.Text = "Time: " + game.Time.Duration();

            if (game.IsGameOver())
            {
                timer1.Stop();
                MessageBox.Show("Game Over");
                Application.Restart();
                Environment.Exit(0);
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

        /*private void Plant(CheckBox cb)
        {
            field[cb].Plant();
            UpdateBox(cb);
        }

        private void Harvest(CheckBox cb)
        {
            field[cb].Harvest();
            UpdateBox(cb);
        }

        private void NextStep(CheckBox cb)
        {
            field[cb].NextStep();
            UpdateBox(cb);
        }

        private void UpdateBox(CheckBox cb)
        {
            Color c = Color.White;
            switch (field[cb].state)
            {
                case CellState.Planted:
                    c = Color.Black;
                    break;
                case CellState.Green:
                    c = Color.Green;
                    break;
                case CellState.Immature:
                    c = Color.Yellow;
                    break;
                case CellState.Mature:
                    c = Color.Red;
                    break;
                case CellState.Overgrow:
                    c = Color.Brown;
                    break;
            }
            cb.BackColor = c;
        }
    }

    enum CellState
    {
        Empty,
        Planted,
        Green,
        Immature,
        Mature,
        Overgrow
    }

    class Cell
    {
        public CellState state = CellState.Empty;
        public int progress = 0;

        private const int prPlanted = 20;
        private const int prGreen = 100;
        private const int prImmature = 120;
        private const int prMature = 140;

        public void Plant()
        {
            state = CellState.Planted;
            progress = 1;
        }

        public void Harvest()
        {
            state = CellState.Empty;
            progress = 0;
        }

        public void NextStep()
        {
            if ((state != CellState.Empty) && (state != CellState.Overgrow))
            {
                progress++;
                if (progress < prPlanted) state = CellState.Planted;
                else if (progress < prGreen) state = CellState.Green;
                else if (progress < prImmature) state = CellState.Immature;
                else if (progress < prMature) state = CellState.Mature;
                else state = CellState.Overgrow;
            }
        }*/
    }
}
