using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project5
{
    public class Weapon
    {
        protected string name;
        protected int damage;

        public Weapon(string name, int damage)
        {
            this.name = name;
            this.damage = damage;
        }

        public string GetName()
        { 
            return name; 
        }

        public int GetDamage()
        {
            return damage;
        }

    }
}
