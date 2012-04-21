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
		private bool keyUp;
        private bool keyDown;
        private bool keyCounter;
        private bool keyClock;
        // move (forewards, backwards, to the left, to the right)
        private bool keyFore;
        private bool keyBack;
        private bool keyLeft, keyRight; 
		
		public Player (InputDevice input)
		{
			keyUp=false;keyDown=false;keyCounter=false;keyClock=false;
			keyFore=false;keyBack=false;keyLeft=false;keyRight=false;
			sqrt2 = Math.Sqrt(2.0)*0.5;
            this.input = input;
            Direction = new Direction();
            Position = new WorldPosition();

            Health = 20;

            input.Keyboard.KeyDown += Keyboard_KeyDown;
			input.Keyboard.KeyUp   += Keyboard_KeyUp;
            input.Mouse.WheelChanged += new EventHandler<MouseWheelEventArgs>(Mouse_WheelChanged);
		}

        void Mouse_WheelChanged(object sender, MouseWheelEventArgs e)
        {
            #region LOL Better Don't look to long at this crap here

            if (e.Delta == 1)
            {
                if (WeaponSelectIndex != 3)
                {
                    WeaponSelectIndex++;
                }
                else
                {
                    WeaponSelectIndex = 0;
                }
            }
            else
            {
                if (WeaponSelectIndex != 0)
                {
                    WeaponSelectIndex--;
                }
                else
                {
                    WeaponSelectIndex = 3;
                }
            }
        #endregion
        }

        void Keyboard_KeyDown(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
        {
            // TODO: movement must be relative to the direction
            switch (e.Key)
            {
                case (Key.A):
                    keyLeft=true;
                    break;
                case (Key.D):
                    keyRight=true;
                    break;
                case (Key.W):
                    keyFore=true;
                    break;
                case (Key.S):
                    keyBack=true;
                    break;
                case (Key.Left):
                    keyCounter=true;
                    break;
                case (Key.Right):
                    keyClock=true;
                    break;
                case (Key.Up):
                    keyUp=true;
                    break;
                case (Key.Down):
                    keyDown=true;
                    break;
                case (Key.Number1):
                    WeaponSelectIndex = 0;
                    break;
                case (Key.Number2):
                    WeaponSelectIndex = 1;
                    break;
                case (Key.Number3):
                    WeaponSelectIndex = 2;
                    break;
                case (Key.Number4):
                    WeaponSelectIndex = 3;
                    break;
            }
        }
        void Keyboard_KeyUp(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
        {
            // TODO: movement must be relative to the direction
            switch (e.Key)
            {
                case (Key.A):
                    keyLeft=false;
                    break;
                case (Key.D):
                    keyRight=false;
                    break;
                case (Key.W):
                    keyFore=false;
                    break;
                case (Key.S):
                    keyBack=false;
                    break;
                case (Key.Left):
                    keyCounter=false;
                    break;
                case (Key.Right):
                    keyClock=false;
                    break;
                case (Key.Up):
                    keyUp=false;
                    break;
                case (Key.Down):
                    keyDown=false;
                    break;
            }
        }
		private double sin (double ang) { return Math.Sin (ang/180.0*Math.PI); }
		private double cos (double ang) { return Math.Cos (ang/180.0*Math.PI); }
        private static double speed = 7.0;
		double sqrt2;
		public void doMovement(TimeSpan timePast)
		{
            Console.WriteLine("x:" + Position.x + " y: " + Position.y);
            double distance = (speed * timePast.TotalMilliseconds) / 1000;
			bool fly=false;
			// use short variable and function names
			//(better overview)
			//double dx=Direction.X, dy=Direction.Y,
			//       px=Position.x, py=Position.y, pz=Position.z;
			// turn
			if (keyUp)      Direction.X-=2.5;
			if (keyDown)    Direction.X+=2.5;
			if (keyClock)   Direction.Y+=2.5;
			if (keyCounter) Direction.Y-=2.5;
			// move to the sides
            if (keyLeft)
            {
                Position.x -= cos(Direction.Y) * distance * (keyFore||keyBack?sqrt2:1.0);
                Position.z -= sin(Direction.Y) * distance * (keyFore||keyBack?sqrt2:1.0);
            }
            if (keyRight)
            {
                Position.x += cos(Direction.Y) * distance * (keyFore||keyBack?sqrt2:1.0);
                Position.z += sin(Direction.Y) * distance * (keyFore||keyBack?sqrt2:1.0);
            }
			// move on the floor
			if (!fly && keyFore)
			{
				Position.z -= cos (Direction.Y) * distance * (keyLeft||keyRight?sqrt2:1.0);
				Position.x += sin (Direction.Y) * distance * (keyLeft||keyRight?sqrt2:1.0);
			}
			if (!fly && keyBack)
			{
				Position.z += cos (Direction.Y) * distance * (keyLeft||keyRight?sqrt2:1.0);
				Position.x -= sin (Direction.Y) * distance * (keyLeft||keyRight?sqrt2:1.0);
			}
		}
    }
}

