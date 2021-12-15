using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using Microsoft.Xna.Framework;


namespace Ecosystem
{
    // TODO: Update the diagram for constant variable
    public abstract class LifeForm : Entity
    {
        protected static readonly float maxHealth = 100;
        protected static readonly float maxEnergy = 100;
        protected static readonly float maxHunger = 100;
        protected static readonly float maxReproductionDesire = 100;
        protected static readonly float healthToGive = 20; // Life to give, when we convert life to energy
        protected static readonly float reproductionDesireThreshold = 70;
        protected static readonly float hungerThreshold = 40;
        protected static readonly float reproductionDesirePerSecond = 10;
        protected static readonly float hungerPerSecond = 10;
        protected static readonly float energyPerSecond = 10;

        private float health = 100;
        private float energy = 100;
        private float hunger = 0;
        private float reproductionDesire = 0;
        private bool isAlive = true;
        
        public LifeForm() : base() { }
        
        public float Health
        {
            get { return health; }
            set
            {
                health = value;

                if (health > maxHealth) health = maxHealth;

                if (health < 0)
                {
                    health = 0;
                    IsAlive = false;
                }
            }
        }

        public float Energy
        {
            get { return energy; }
            set
            {
                energy = value;

                if (energy > maxEnergy) energy = maxEnergy;

                if (energy < 0) energy = 0;
            }
        }

        public float Hunger
        {
            get { return hunger; }
            protected set 
            {
                hunger = value;

                if (hunger > maxHunger) hunger = maxHunger;

                if (hunger < 0) hunger = 0; 
            }
        }

        public float ReproductionDesire
        {
            get { return reproductionDesire; }
            set
            {
                reproductionDesire = value;

                if (reproductionDesire > maxReproductionDesire) reproductionDesire = maxReproductionDesire;

                if (reproductionDesire < 0) reproductionDesire = 0;
            }
        }

        public bool IsAlive
        {
            get { return isAlive; }
            set { isAlive = value; }
        }

        public bool IsHungry()
        {
            return (Hunger >= hungerThreshold);
        }

        public abstract bool CanEat(IEatable food);
        public abstract void Eat(IEatable food);

        void LifeToEnergy()
        {
            Energy += healthToGive;
            Health -= healthToGive;
        }

        public override void Routine(GameTime gameTime)
        {
            float timeElapsed = ((float)gameTime.ElapsedGameTime.Milliseconds / 1000);

            // Time management of different parameters of a LifeForm
            Energy -= (energyPerSecond * timeElapsed);
            ReproductionDesire += (reproductionDesirePerSecond * timeElapsed);
            Hunger += (hungerPerSecond * timeElapsed);

            // If there's no more energy, we convert the healt into energy
            if (energy == 0)
                LifeToEnergy();
        }
    }
}
