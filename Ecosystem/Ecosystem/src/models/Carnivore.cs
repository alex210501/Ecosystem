using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Ecosystem
{
    public abstract class Carnivore : Animal
    {
        private float damage; // Damages per seconds

        protected Carnivore(AnimalSex sex, int visionZoneRadius, int contactZoneRadius, float speed, float damage) : 
            base(sex, visionZoneRadius, contactZoneRadius, speed)
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
            if (CanEat(food))
            {
                Energy += food.EatingEnergy;
                Hunger += food.SatiationPoint;
            }
        }

        public override bool CanEat(IEatable food)
        {
            return (food is Meat);
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
