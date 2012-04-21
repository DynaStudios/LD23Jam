using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DynaStudios.Blocks;

namespace LudumDare23.Entities
{
    public abstract class Enemy
    {

        public WorldPosition WorldPosition { get; set; }
        public Direction Direction { get; set; }

        public int Health { get; set; }

        public virtual int ApplyDamage()
        {
            return 0;
        }

        public virtual void ReceiveDamage(Player player)
        {
            //Look for PlayerWeapon
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

        }

    }
}
