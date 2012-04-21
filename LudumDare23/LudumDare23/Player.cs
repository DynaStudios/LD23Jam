using System;
using DynaStudios.IO;
using DynaStudios.Blocks;
using OpenTK.Input;
using LudumDare23.Entities.Weapons;
using System.Collections.Generic;

namespace LudumDare23
{
	public class Player : IWorldObject
	{
		public Direction Direction { get; set; }
        public WorldPosition Position { get; set; }
        private InputDevice input;

        public int Health { get; set; }
        public List<IWeapon> Weapons { get; set; }
        public int WeaponSelectIndex { get; set; }

		// keys currently pressed

        // turn (up, down, counter-clock-wise, clock-wise)
		private bool k_up;
        private bool k_dw;
        private bool kConterClockWise;
        private bool kClockWise;
        // move (forewards, backwards, to the left, to the right)
        private bool keyForward;
        private bool keyBackwards;
        private bool k_lf, k_rg; 
		
		public Player (InputDevice input)
		{
			k_up=false;k_dw=false;kConterClockWise=false;kClockWise=false;
			keyForward=false;keyBackwards=false;k_lf=false;k_rg=false;
            this.input = input;
            Direction = new Direction();
            Position = new WorldPosition();

            Health = 20;

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
                    keyForward=true;
                    break;
                case (Key.S):
                    keyBackwards=true;
                    break;
                case (Key.Left):
                    kConterClockWise=true;
                    break;
                case (Key.Right):
                    kClockWise=true;
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
                    keyForward=false;
                    break;
                case (Key.S):
                    keyBackwards=false;
                    break;
                case (Key.Left):
                    kConterClockWise=false;
                    break;
                case (Key.Right):
                    kClockWise=false;
                    break;
                case (Key.Up):
                    k_up=false;
                    break;
                case (Key.Down):
                    k_dw=false;
                    break;
            }
        }
		private double sin (double ang) { return Math.Sin (ang); }
		private double cos (double ang) { return Math.Cos (ang); }
        private static double speed = 1.0;
		public void doMovement(TimeSpan timePast)
		{
            Console.WriteLine("x:" + Position.x + " y: " + Position.y);
            double distance = (speed * timePast.TotalMilliseconds) / 1000;
			// use short variable and function names
			//(better overview)
			//double dx=Direction.X, dy=Direction.Y,
			//       px=Position.x, py=Position.y, pz=Position.z;
			// turn
			if (k_up) Direction.X-=0.05;
			if (k_dw) Direction.X+=0.05;
			if (kClockWise) Direction.Y+=0.05;
			if (kConterClockWise) Direction.Y-=0.05;
			// move
            if (k_lf)
            {
                Position.x -= cos(Direction.Y) * distance;
                Position.z -= sin(Direction.Y) * distance;
            }
            if (k_rg)
            {
                Position.x += cos(Direction.Y) * distance;
                Position.y += sin(Direction.Y) * distance;
            }
		}
    }
}

