using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Gladiator
{
    public class Person// Parent for player and opponent
    {
        public int Health { get; set; }
        public int Defense { get; set; }
        public int Damage { get; set; }
        public int Energy { get; set; }


    }

    public class Player : Person // Sets the players stats
    {

         public Player()           
        {


            Health = statPlayer();
            Damage = statPlayer();
            Defense = statPlayer();
            Energy = statPlayer();

        }

        private int statPlayer() // rolls random number for stat generation
        {

            Random random = new Random();
            return random.Next(3, 7);

        }
    }
    
    public class Opponent : Person
    {
        readonly int Level;
        

        public Opponent(int level) // Sets the difficulty of the opponent
        {
            Level = level;
            Health = statRoll() + Level;
            Damage = statRoll() + Level;
            Defense = statRoll() + Level;
            Energy = statRoll() + Level;
            
        }

        private int statRoll() // rolls random number for stat generation
        {
            Random random = new Random();
            return random.Next(1, 3);
            
        }        
    }
}
