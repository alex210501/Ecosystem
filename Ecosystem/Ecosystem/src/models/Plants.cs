using System;
using System.Collections.Generic;
using System.Text;

namespace Ecosystem
{
    public class Plants : LifeForm, IEatable
    {
        private readonly int RootZoneRadius;//allow a value
        private readonly int SeedingZoneRadius;//allow a value
        private float eatingEnergy = 100;
        private float satiationPoint = 100;
        private bool hasBeenEaten = false;

        public Plants(int RootZoneRadius, int SeedingZoneRadius) : base()
        {
            this.RootZoneRadius = RootZoneRadius;
            this.SeedingZoneRadius = SeedingZoneRadius;
        }

        public float EatingEnergy
        {
            get { return eatingEnergy; }
            set { eatingEnergy = value; }
        }

        public float SatiationPoint
        {
            get { return satiationPoint; }
            set { satiationPoint = value; }
        }

        public bool HasBeenEaten
        {
            get { return hasBeenEaten; }
            set { hasBeenEaten = value; }
        }

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
            return (food is OrganicWaste);
        }

        public bool WantsExpands(IEatable food)
        {
            return (ReproductionDesire >= reproductionDesireThreshold);
        }

        public void Expands()
        {
            // Reset the reproduction desire
            ReproductionDesire = 0;
        }
    }
}