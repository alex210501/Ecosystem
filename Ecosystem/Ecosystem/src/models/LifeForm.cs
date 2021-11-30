using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;


namespace Ecosystem
{
    // TODO: Update the diagram for constant variable
    public abstract class LifeForm
    {
        private static readonly float maxHealth = 100;
        private static readonly float maxEnergy = 100;
        private static readonly float maxReproductionDesire = 100;
        private static readonly float healthToGivePerSecond = 2; // Life to give per second, when we convert life to energy

        private float health = 100;
        private float energy = 100;
        private float reproductionDesire = 0;
        private bool isAlive = true;
        
        public LifeForm() { }

        public float Health
        {
            get { return health; }
            set
            {
                health += value;

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
                energy += energy;

                if (energy > maxEnergy) energy = maxEnergy;

                if (energy < 0) energy = 0;
            }
        }

        public float ReproductionDesire
        {
            get { return reproductionDesire; }
            set
            {
                reproductionDesire += value;

                if (reproductionDesire > maxReproductionDesire) reproductionDesire = maxReproductionDesire;

                if (reproductionDesire < 0) reproductionDesire = 0;
            }
        }

        public bool IsAlive
        {
            get { return isAlive; }
            set { isAlive = value; }
        }

        // TODO: Create the IEatable interface
        public abstract void Eat(IEatable food);

        void LifeToEnergy(float timeElapsed)
        {
            float healthToGive = healthToGivePerSecond * timeElapsed;

            Energy += healthToGive;
            Health -= healthToGive;
        }
    }
}
