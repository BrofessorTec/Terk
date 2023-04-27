/**
* ------------------------------------------------------------------------
* File Name: Project5
* Project Name: Zork Game
* ------------------------------------------------------------------------
* Author's Name and Email: Tyler Campbell, tcampbell5@etsu.edu
* Course-Section: CSCI-1260-002
* Creation Date: 4/12/23
* ------------------------------------------------------------------------
* */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project5
{
    public class Monster : Participant
    {
        private string name;
        private int hp;
        private double atkChance;
        private int damage;

        public Monster(string name)
        {
            this.name = name;
            hp = 20;
            atkChance = 0.8;   //need to calculate this into the combat
            damage = 4;
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

        public void SetHealth(int damageTaken)
        {
            this.hp -= damageTaken;
        }
    }
}
