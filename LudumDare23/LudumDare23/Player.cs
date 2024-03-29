using System;
using DynaStudios.IO;
using DynaStudios.Blocks;
using OpenTK.Input;
using LudumDare23.Entities.Weapons;
using System.Collections.Generic;

namespace LudumDare23
{
    class MouseDelta
    {
        public double x;
        public double y;
    }

	public class Player : IWorldObject
	{
		public Direction Direction { get; set; }
        public WorldPosition Position { get; set; }
        private InputDevice input;

        public int Health { get; set; }
        public List<IWeapon> Weapons { get; set; }
        public int WeaponSelectIndex { get; set; }

        private double _mouse_speed = 1.0;
        private MouseDelta _mouseDeltas = new MouseDelta();
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

        double sqrt2 = Math.Sqrt(2.0) * 0.5;
		
		// some settings and variables for control
        private static double speed = 7.0;
		public bool capture_mouse;
		
		public Player (InputDevice input)
		{
			keyUp=false;keyDown=false;keyCounter=false;keyClock=false;
			keyFore=false;keyBack=false;keyLeft=false;keyRight=false;
			capture_mouse=false;
            this.input = input;
            Direction = new Direction();
            Position = new WorldPosition();

            Health = 20;

            input.Keyboard.KeyDown += Keyboard_KeyDown;
			input.Keyboard.KeyUp   += Keyboard_KeyUp;
            input.Mouse.WheelChanged += new EventHandler<MouseWheelEventArgs>(Mouse_WheelChanged);
            input.Mouse.Move += new EventHandler<MouseMoveEventArgs>(Mouse_Move);
		}

        void Mouse_Move(object sender, MouseMoveEventArgs e)
        {
			if (capture_mouse) {
                lock (_mouseDeltas)
                {
                    _mouseDeltas.y = e.YDelta * _mouse_speed;
                    _mouseDeltas.x = e.XDelta * _mouse_speed;
                }
            	//System.Windows.Forms.Cursor.Position = new System.Drawing.Point(500,500);
			}
        }

        void Mouse_WheelChanged(object sender, MouseWheelEventArgs e)
        {
            #region LOL Better Don't look to long at this crap here

            if (e.Delta == 1)
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
            else
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
		public void doMovement(TimeSpan timePast)
		{
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

            lock (_mouseDeltas)
            {
                Direction.X += _mouseDeltas.y;
                _mouseDeltas.y = 0.0;

                Direction.Y += _mouseDeltas.x;
                _mouseDeltas.x = 0.0;
            }
			
			if (Direction.X<-100.0) Direction.X=-100.0;
			if (Direction.X> 100.0) Direction.X= 100.0;
			
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

