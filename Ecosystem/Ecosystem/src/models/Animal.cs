using System;
using System.Collections.Generic;
using System.Text;

namespace Ecosystem
{
    public enum AnimalSex
    {
        Male = 0,
        Female
    }

    // TODO: Think for IsInVisionZone(), IsInContactZone(), Walk()
    public abstract class Animal : LifeForm
    {
        private static readonly float reproductionDesireThreshold = 70;
        private static readonly float poopThreshold = 70;
        private static readonly float hungerThreshold = 70;

        private readonly AnimalSex sex;
        private int visionZoneRadius;
        private int contactZoneRadius;
        private bool isPregnant = false;
        private readonly float speed = 100; // Pixel per second
        private float hunger = 0;
        private float poopDesire = 0;

        protected Animal(int visionZoneRadius, int contactZoneRadius, float speed)
        {
            this.visionZoneRadius = visionZoneRadius;
            this.contactZoneRadius = contactZoneRadius;
            this.speed = speed;
        }

        public AnimalSex Sex
        {
            get { return sex; }
        }

        public int VisionZoneRadius
        {
            get { return visionZoneRadius; }
        }

        public int ContactZoneRadius
        {
            get { return contactZoneRadius; }
        }

        public bool IsPregnant
        {
            get { return isPregnant;  }
            set { isPregnant = value; }
        }

        public float Speed
        {
            get { return (speed * (Health / 100) * (Energy / 100)); }
        }

        public float Hunger
        {
            get { return hunger; }
            private set { hunger = value; }
        }

        public float PoopDesire
        {
            get { return poopDesire; }
            private set { poopDesire = value; }
        }

        public abstract void Reproduction(Animal animal);

        // TODO: Implement the CanGiveBirht function
        public bool CanGiveBirth()
        {
            return true;
        }

        public bool IsHungry()
        {
            return (hunger >= hungerThreshold);
        }

        public bool WantReproduce()
        {
            return (ReproductionDesire >= reproductionDesireThreshold);
        }

        public bool WantPoop()
        {
            return (poopDesire >= poopThreshold);
        }


    }
}
