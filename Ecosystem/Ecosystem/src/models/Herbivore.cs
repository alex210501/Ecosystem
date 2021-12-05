using System;
using System.Collections.Generic;
using System.Text;

namespace Ecosystem
{
    public abstract class Herbivore : Animal
    {
        protected Herbivore(AnimalSex sex, int visionZoneRadius, int contactZoneRadius, float speed) : 
            base (sex, visionZoneRadius, contactZoneRadius, speed) { }

        public override void Eat(IEatable food)
        {
            if (CanEat(food))
            {
                Energy += food.EatingEnergy;
                Hunger += food.SatiationPoint;
            }
        }

        public bool CanEat(IEatable food)
        {
            return (food is Plants);
        }
    }
}
