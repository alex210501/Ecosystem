using System;
using System.Collections.Generic;
using System.Text;

namespace Ecosystem
{
    public abstract class Carnivore : Animal
    {
        private float damage; // Damages per seconds

        protected Carnivore(int visionZoneRadius, int contactZoneRadius, float speed, float damage) : base(visionZoneRadius, contactZoneRadius, speed)
        {
            this.damage = damage;
        }

        public float Damage
        {
            get { return damage; }
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

        public void Attack(Animal prey)
        {
            if (CanAttack(prey))
                prey.Health -= damage;
        }

        public bool CanAttack(Animal prey)
        {
            return (prey is Herbivore);
        }
    }
}
