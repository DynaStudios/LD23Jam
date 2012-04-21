using System;
using DynaStudios.IO;
using DynaStudios.UI;
using DynaStudios.Utils;
using DynaStudios.Blocks;
using DynaStudios;
using OpenTK.Input;

namespace LudumDare23
{
	public class Player
	{
		public Direction Direction { get; set; }
        public WorldPosition Position { get; set; }
        private InputDevice input;
		
		// keys currently pressed
		private bool k_up,k_dw,k_cc,k_cw, // turn (up, down, counter-clock-wise, clock-wise)
		             k_fr,k_bk,k_lf,k_rg; // move (forewards, backwards, to the left, to the right)
		
		public Player (InputDevice input)
		{
			k_up=false;k_dw=false;k_cc=false;k_cw=false;
			k_fr=false;k_bk=false;k_lf=false;k_rg=false;
            this.input = input;
            Direction = new Direction();
            Position = new WorldPosition();
            input.Keyboard.KeyDown += Keyboard_KeyDown;
			input.Keyboard.KeyUp   += Keyboard_KeyUp;
		}

        void Keyboard_KeyDown(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
        {
            // TODO: movement must be relative to the direction
            switch (e.Key)
            {
                case (Key.A):
                    k_lf=true;
                    break;
                case (Key.D):
                    k_rg=true;
                    break;
                case (Key.W):
                    k_fr=true;
                    break;
                case (Key.S):
                    k_bk=true;
                    break;
                case (Key.Left):
                    k_cc=true;
                    break;
                case (Key.Right):
                    k_cw=true;
                    break;
                case (Key.Up):
                    k_up=true;
                    break;
                case (Key.Down):
                    k_dw=true;
                    break;
            }
        }
        void Keyboard_KeyUp(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
        {
            // TODO: movement must be relative to the direction
            switch (e.Key)
            {
                case (Key.A):
                    k_lf=false;
                    break;
                case (Key.D):
                    k_rg=false;
                    break;
                case (Key.W):
                    k_fr=false;
                    break;
                case (Key.S):
                    k_bk=false;
                    break;
                case (Key.Left):
                    k_cc=false;
                    break;
                case (Key.Right):
                    k_cw=false;
                    break;
                case (Key.Up):
                    k_up=false;
                    break;
                case (Key.Down):
                    k_dw=false;
                    break;
            }
        }
		public void doMovement()
		{
			// turn
			if (k_up) Direction.X-=0.05;
			if (k_dw) Direction.X+=0.05;
			if (k_cw) Direction.Y+=0.05;
			if (k_cc) Direction.Y-=0.05;
			// move
		}
    }
}

