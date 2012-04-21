using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LudumDare23.Entities.Weapons
{
    public interface IWeapon
    {
        /// <summary>
        /// Returns Weapondamage
        /// </summary>
        /// <returns>WeoponDamage (Max = 20, Min = 1)</returns>
        int ApplyDamage();

        /// <summary>
        /// Returns Left Ammo
        /// </summary>
        /// <returns>Ammo</returns>
        int AmmoLeft();

        /// <summary>
        /// Amount of Ammo to Increase
        /// </summary>
        /// <param name="amount"></param>
        void IncreaseAmmo(int amount);

        /// <summary>
        /// Amount of Ammo to Decrease
        /// </summary>
        /// <param name="amount"></param>
        void DecreaseAmmo(int amount);

    }
}
