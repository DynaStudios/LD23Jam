using System;
using System.IO;
using DynaStudios.Blocks;
using OpenTK.Graphics.OpenGL;

namespace LudumDare23.Entities
{
    public abstract class Enemy : AbstractDrawable
    {

        public WorldScene Scene { get; set; }
        
        public int Health { get; set; }
        public int Damage { get; set; }

        public float AggroRange = 10.0f;
        public bool IsAggro { get; set; }

        public virtual int ApplyDamage()
        {
            return Damage;
        }

        public virtual void ReceiveDamage()
        {
            //Look for PlayerWeapon
            var player = Scene.Engine.Camera.WorldObject as Player;
            int incDamage = player.Weapons[player.WeaponSelectIndex].ApplyDamage();
            if (incDamage > Health)
            {
                Die();
            }
            else
            {
                Health -= incDamage;
            }
        }

        public virtual void Die()
        {
            //Drop the loop
            dropLoot();
        }

        public virtual void dropLoot()
        {

        }

        public override void render()
        {

            if (Scene != null) { 
            doMovement();

            GL.BindTexture(TextureTarget.Texture2D, Scene.Engine.TextureManager.getTexture(Path.Combine("Images", "Game", "menuselection.png")));
            GL.Begin(BeginMode.Quads);

            GL.TexCoord2(0, 1); GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.TexCoord2(1, 1); GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.TexCoord2(1, 0); GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.TexCoord2(0, 0); GL.Vertex3(-1.0f, 1.0f, 1.0f);

            GL.End();
            }
        }

        public virtual void doMovement()
        {
            //Look were Player is. Check if he is in Aggro range. If yes == start using chasing him
            checkAggro();

            if (IsAggro)
            {
                //Chase Player
                
                var playerPos = Scene.Engine.Camera.WorldObject.Position;

                double aab =  playerPos.x * Position.x + playerPos.y * Position.y + playerPos.z - Position.z;
                double playerPow = Math.Sqrt(Math.Pow(playerPos.x, 2) + Math.Pow(playerPos.y, 2) + Math.Pow(playerPos.z, 2));
                double enemyPow = Math.Sqrt(Math.Pow(Position.x, 2) + Math.Pow(Position.y, 2) + Math.Pow(Position.z, 2));

                double rotation = Math.Acos( aab / (playerPow * enemyPow) ) / Math.PI * 360;
                
                base.Direction.Y = rotation;
                Console.WriteLine(rotation);
            }
            else
            {
                //Do some random Movement
            }
        }

        private void checkAggro()
        {

            if (Scene != null)
            {
                var PlayerWorld = Scene.Engine.Camera.WorldObject;

                /* 
                 * 3D Distace [d = sqrt((Px - Ex)² + (Py - Ey)² + (Pz - Ez)²)]
                 * P = Player | E = Enemy 
                 */
                double range = Math.Sqrt(Math.Pow(PlayerWorld.Position.x - Position.x, 2) + Math.Pow(PlayerWorld.Position.y - Position.y, 2) + Math.Pow(PlayerWorld.Position.z - Position.z, 2));
                IsAggro = (range < AggroRange);
            }
        }

    }
}
