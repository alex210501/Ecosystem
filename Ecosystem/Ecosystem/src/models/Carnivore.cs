using System;
using System.Collections.Generic;
using System.Text;

namespace Ecosystem.src.models
{
    public abstract class Carnivore : Animal
    {
        private int damage;

        protected Carnivore(int visionZoneRadius, int contactZoneRadius, float speed, int damage) : base(visionZoneRadius, contactZoneRadius, speed)
        {
            this.damage = damage;
        }

        // TODO: Implement the Eat method when the Meat is created
        public override void Eat(IEatable food)
        {
            return;
        }

        // TODO: Implement the CanHeat method when the Meat is created
        public bool CanEat(IEatable food)
        {
            return false;
        }

        // TODO: Implement the Attack method when the Herbivore is created
        public void Attack(Animal prey)
        {
            return;
        }

        // TODO: Implement the CanAttack method when the Herbivore is created
        public bool CanAttack(Animal prey)
        {
            return false;
        }
    }
}
