using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace VegetableFarm
{
	class Game
	{
		public Dictionary<CheckBox, Cell> Field { get; }
		public int Money { get; private set; }
		public double Speed { get; private set; }
		public TimeSpan Time { get; private set; }

		const double speedStep = 0.25;

		public Game()
		{
			Field = new Dictionary<CheckBox, Cell>();
			Money = 50;
			Speed = 1;
			Time = new TimeSpan();
		}

		public bool IsGameOver()
		{
			if (Money >= 2) return false;
			else if (Money >= 1)
				foreach (CheckBox cb in Field.Keys)
				{
					if (Field[cb].state != CellState.Empty)
						return false;
				}
			else
				foreach (CheckBox cb in Field.Keys)
				{
					if (Field[cb].state != CellState.Empty && Field[cb].state != CellState.Overgrow)
						return false;
				}
			return true;
		}

		public void NewGame()
		{
			Money = 50;
			Speed = 1;
			Time = new TimeSpan();

			foreach (CheckBox cb in Field.Keys)
			{
				Field[cb].state = CellState.Empty;
				UpdateBox(cb);
				cb.Checked = false;
			}
		}

		public void SetField(Panel panel)
		{
			foreach (CheckBox cb in panel.Controls)
				Field.Add(cb, new Cell());
		}

		public void UpSpeed()
		{
			if (Speed < 5) Speed += speedStep;
		}

		public void DownSpeed()
		{
			if (Speed > 0.25) Speed -= speedStep;
		}

		private void TickGame()
		{
			Time = Time.Add(new TimeSpan(0, 0, 1));
		}

		public void ChangeCell(CheckBox cb)
		{
			if (cb.Checked)
				Plant(cb);
			else if (Field[cb].state == CellState.Overgrow ||
				Field[cb].state == CellState.Mature ||
				Field[cb].state == CellState.Immature)
				Harvest(cb);
			else cb.Checked = true;
		}

		private void Plant(CheckBox cb)
		{
			if (Money >= 2)
			{
				Money -= 2;

				Field[cb].Plant();
				UpdateBox(cb);
			}
			else cb.Checked = false;
		}

		private void Harvest(CheckBox cb)
		{
			if (Field[cb].state != CellState.Overgrow || Money >= 1)
			{
				switch (Field[cb].state)
				{
					case CellState.Immature:
						Money += 3;
						break;
					case CellState.Mature:
						Money += 5;
						break;
					case CellState.Overgrow:
						Money -= 1;
						break;
				}

				Field[cb].Harvest();
				UpdateBox(cb);
			}
		}

		public void NextStep(CheckBox cb)
		{
			Field[cb].NextStep();
			UpdateBox(cb);
			TickGame();
		}

		private void UpdateBox(CheckBox cb)
		{
			Color c = Color.White;
			switch (Field[cb].state)
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
}
