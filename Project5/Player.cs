﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project5
{
    public class Player : Participant
    {
        /*private string name;
        private int hp;
        private double atkChance;
        private int damage;*/

        public Player(string name)
        {
            this.name = name;
            hp = 100;
            atkChance = 0.9;   //need to calculate this into the combat
            damage = 5;
        }

        public override int Attack()
        {
            Random random = new Random();
            if (random.NextDouble() < atkChance)
            {
                return GetDamage();
            }
            else
            {
                return 0;
            }
        }

        public override string GetName()
        {
            return this.name;
        }

        public override int GetHealth()
        {
            return this.hp;
        }
        public override int GetDamage()
        {
            return this.damage;
        }

        public void SetDamage(int damageIncrease)
        { 
            this.damage += damageIncrease;
        }

        public void SetHealth(int damageTaken)
        {
            this.hp -= damageTaken;
            if (this.hp < 0)
            {
                this.hp = 0;
            }
            else if (this.hp > 100)
            {
                this.hp = 100;
            }
        }
    }
}
