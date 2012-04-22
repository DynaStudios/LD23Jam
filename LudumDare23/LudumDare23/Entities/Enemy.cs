using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DynaStudios;
using DynaStudios.Blocks;

namespace LudumDare23.Entities
{
    public abstract class Enemy
    {

        public WorldScene Scene { get; set; }

        public WorldPosition WorldPosition { get; set; }
        public Direction Direction { get; set; }

        public int Health { get; set; }
        public int Damage { get; set; }

        public float AggroRange { get; set; }
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

        public virtual void doMovement()
        {
            //Look were Player is. Check if he is in Aggro range. If yes == start using chasing him
            checkAggro();

            if (IsAggro)
            {
                //Chase Player
            }
            else
            {
                //Do some random Movement
            }
        }

        public abstract void doRender();

        private void checkAggro() {

            var PlayerWorld = Scene.Engine.Camera.WorldObject;

            /* 
             * 3D Distace [d = sqrt((Px - Ex)² + (Py - Ey)² + (Pz - Ez)²)]
             * P = Player | E = Enemy 
             */
            double range = Math.Sqrt(Math.Pow(PlayerWorld.Position.x - WorldPosition.x, 2) + Math.Pow(PlayerWorld.Position.y - WorldPosition.y, 2) + Math.Pow(PlayerWorld.Position.z - WorldPosition.z, 2));
            IsAggro = (range > AggroRange);

        }

    }
}
