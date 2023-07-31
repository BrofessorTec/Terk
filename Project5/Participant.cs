using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project5
{
    public abstract class Participant
    {
        protected string name;
        protected int hp;
        protected int hpMax;
        protected double atkChance;
        protected int damage;
        public abstract int Attack();
        public abstract string GetName();
        public abstract int GetHealth();
        public abstract int GetDamage();

    }
}
