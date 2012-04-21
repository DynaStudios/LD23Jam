using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LudumDare23.Entities.Weapons
{
    public class Pistol : IWeapon
    {

        private int _ammo;
        private int _damage = 3;

        public Pistol()
        {

            _ammo = 5;
        }

        public int ApplyDamage()
        {
            return _damage;
        }

        public int AmmoLeft()
        {
            return _ammo;
        }

        public void IncreaseAmmo(int amount)
        {
            
        }

        public void DecreaseAmmo(int amount)
        {
            
        }
    }
}
