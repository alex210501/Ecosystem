using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Ecosystem
{
    public abstract class Herbivore : Animal
    {
        protected Herbivore(AnimalSex sex, int visionZoneRadius, int contactZoneRadius, float speed, Vector2 destination) : 
            base (sex, visionZoneRadius, contactZoneRadius, speed, destination) { }

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
            return (food is Plants);
        }
    }
}
