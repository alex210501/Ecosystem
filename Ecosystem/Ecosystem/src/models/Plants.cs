using System;
using System.Collections.Generic;
using System.Text;

namespace Ecosystem
{
    public class Plants : LifeForm, IEatable
    {
        private readonly int RootZoneRadius = 100;//allow a value
        private static readonly int SeedingZoneRadius = 50;//allow a value
        private float eatingEnergy = 100;
        private float satiationPoint = 20;
        private bool hasBeenEaten = false;

        public Plants() : base()
        {
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

        public int ROOTZoneRadius
        {
            get { return RootZoneRadius; }
        }

        public int SEEDINGZoneRadius
        {
            get { return SeedingZoneRadius; }
        }
        public override bool CanEat(IEatable food)
        {
            return (food is OrganicWaste);
        }

        public bool WantsExpands()
        {
            return (ReproductionDesire >= reproductionDesireThreshold);
        }

        public void Expands()
        {
            // Reset the reproduction desire
            ReproductionDesire = 0;
        }

        public bool IsInRootZone(Entity entity)
        {
            return IsInZone(entity, RootZoneRadius);
        }
    }
}