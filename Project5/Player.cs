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
    public class Player : Participant
    {
        private string name;
        private int hp;
        private double atkChance;
        private int damage;

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

        public override int GetDamage()
        {
            return this.damage;
        }

        public override void SetDamage()
        { 
            this.damage = 10;
        }

        public void SetHealth(int damageTaken)
        {
            this.hp -= damageTaken;
        }


    }
}
